using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class HoaDonDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public HoaDonDAO()
    {
        MyConnection.Mo(connection);
    }

    private HoaDonDTO DocDong(MySqlDataReader reader)
    {
        string ma = reader.GetString(0);
        string maKH = reader.GetString("MaKH");
        string maNV = reader.IsDBNull(reader.GetOrdinal("MaNV")) ? "" : reader.GetString("MaNV");
        string maDV = reader.GetString("MaDV");
        DateTime ngayLap = reader.GetDateTime("NgayLap");
        decimal tongTien = reader.GetDecimal("TongTien");
        string phuongThucTT = reader.IsDBNull(reader.GetOrdinal("PhuongThucTT")) ? "" : reader.GetString("PhuongThucTT");
        string trangThai = reader.GetString("TrangThai");
        return new HoaDonDTO(ma, maKH, maNV, maDV, ngayLap, tongTien, phuongThucTT, trangThai);
    }

    public List<HoaDonDTO> DocDSHD()
    {
        List<HoaDonDTO> dshd = new List<HoaDonDTO>();
        try
        {
            string query = "SELECT MaHD,MaKH,MaNV,MaDV,NgayLap,TongTien,PhuongThucTT,TrangThai FROM HoaDon";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HoaDonDTO hd = DocDong(reader);
                dshd.Add(hd);//add to arraylist dshd
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin hóa đơn!");
        }
        return dshd;
    }

    public List<HoaDonDTO> DocTheoNV(string maCha)
    {
        List<HoaDonDTO> dshd = new List<HoaDonDTO>();
        try
        {
            string query = "SELECT MaHD,MaKH,MaNV,MaDV,NgayLap,TongTien,PhuongThucTT,TrangThai FROM HoaDon WHERE MaNV='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HoaDonDTO hd = DocDong(reader);
                dshd.Add(hd);//add to arraylist dshd
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc hóa đơn theo nhân viên!");
        }
        return dshd;
    }

    public List<HoaDonDTO> DocTheoKH(string maCha)
    {
        List<HoaDonDTO> dshd = new List<HoaDonDTO>();
        try
        {
            string query = "SELECT MaHD,MaKH,MaNV,MaDV,NgayLap,TongTien,PhuongThucTT,TrangThai FROM HoaDon WHERE MaKH='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HoaDonDTO hd = DocDong(reader);
                dshd.Add(hd);//add to arraylist dshd
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc hóa đơn theo khách hàng!");
        }
        return dshd;
    }

    public bool Them(HoaDonDTO hd)
    {
        try
        {
            string ngayLap = hd.NgayLap.ToString("yyyy-MM-dd HH:mm:ss");
            string tongTien = hd.TongTien.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string maNV = string.IsNullOrEmpty(hd.MaNV) ? "NULL" : "'" + hd.MaNV + "'";
            string phuongThucTT = string.IsNullOrEmpty(hd.PhuongThucTT) ? "NULL" : "'" + hd.PhuongThucTT + "'";
            string query = "INSERT INTO HoaDon VALUES ('" + hd.MaHD
                + "','" + hd.MaKH + "'," + maNV + ",'" + hd.MaDV + "','" + ngayLap + "'," + tongTien
                + "," + phuongThucTT + ",'" + hd.TrangThai + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            TinhTienDAO.HoaDon(connection, null, hd.MaHD);
            TinhTienDAO.TrangThaiDatVe(connection, null, hd.MaDV);
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin hóa đơn!"); return false; }
    }

    public bool Sua(HoaDonDTO hd)
    {
        try
        {
            string ngayLap = hd.NgayLap.ToString("yyyy-MM-dd HH:mm:ss");
            string maNV = string.IsNullOrEmpty(hd.MaNV) ? "NULL" : "'" + hd.MaNV + "'";
            string phuongThucTT = string.IsNullOrEmpty(hd.PhuongThucTT) ? "NULL" : "'" + hd.PhuongThucTT + "'";
            string query = "UPDATE HoaDon SET MaKH='" + hd.MaKH + "',MaNV=" + maNV
                + ",MaDV='" + hd.MaDV + "',NgayLap='" + ngayLap
                + "',PhuongThucTT=" + phuongThucTT + ",TrangThai='" + hd.TrangThai
                + "' WHERE MaHD='" + hd.MaHD + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            TinhTienDAO.HoaDon(connection, null, hd.MaHD);
            TinhTienDAO.TrangThaiDatVe(connection, null, hd.MaDV);
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật hóa đơn!"); return false; }
    }

    public bool Xoa(string ma)
    {
        // Xóa hóa đơn (chỉ hóa đơn chưa thanh toán): xóa chi tiết trước rồi xóa hóa đơn, trong 1 transaction
        MySqlTransaction tran = null;
        try
        {
            tran = connection.BeginTransaction();
            object maDV = new MySqlCommand("SELECT MaDV FROM HoaDon WHERE MaHD='" + ma + "'", connection, tran).ExecuteScalar();
            new MySqlCommand("DELETE FROM ChiTietHoaDon WHERE MaHD='" + ma + "'", connection, tran).ExecuteNonQuery();
            new MySqlCommand("DELETE FROM HoaDon WHERE MaHD='" + ma + "'", connection, tran).ExecuteNonQuery();
            if (maDV != null)
                TinhTienDAO.TrangThaiDatVe(connection, tran, maDV.ToString());
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi xóa hóa đơn!");
            return false;
        }
    }
}
