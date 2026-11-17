using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class DatVeDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public DatVeDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<DatVeDTO> DocDSDV()
    {
        List<DatVeDTO> dsdv = new List<DatVeDTO>();
        try
        {
            string query = "SELECT MaDV,MaKH,MaNV,MaChuyenBay,NgayDat,TongTien,GiamGia,TrangThai,MaVoucher FROM DatVe";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maKH = reader.GetString("MaKH");
                string maNV = reader.IsDBNull(reader.GetOrdinal("MaNV")) ? "" : reader.GetString("MaNV");
                string maChuyenBay = reader.GetString("MaChuyenBay");
                DateTime ngayDat = reader.GetDateTime("NgayDat");
                decimal tongTien = reader.GetDecimal("TongTien");
                decimal giamGia = reader.GetDecimal("GiamGia");
                string trangThai = reader.GetString("TrangThai");
                string maVoucher = reader.IsDBNull(reader.GetOrdinal("MaVoucher")) ? "" : reader.GetString("MaVoucher");
                DatVeDTO dv = new DatVeDTO(ma, maKH, maNV, maChuyenBay, ngayDat, tongTien, giamGia, trangThai, maVoucher);
                dsdv.Add(dv);//add to arraylist dsdv
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin đặt vé!");
        }
        return dsdv;
    }

    public List<DatVeDTO> DocTheoKH(string maCha)
    {
        List<DatVeDTO> dsdv = new List<DatVeDTO>();
        try
        {
            string query = "SELECT MaDV,MaKH,MaNV,MaChuyenBay,NgayDat,TongTien,GiamGia,TrangThai,MaVoucher FROM DatVe WHERE MaKH='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maKH = reader.GetString("MaKH");
                string maNV = reader.IsDBNull(reader.GetOrdinal("MaNV")) ? "" : reader.GetString("MaNV");
                string maChuyenBay = reader.GetString("MaChuyenBay");
                DateTime ngayDat = reader.GetDateTime("NgayDat");
                decimal tongTien = reader.GetDecimal("TongTien");
                decimal giamGia = reader.GetDecimal("GiamGia");
                string trangThai = reader.GetString("TrangThai");
                string maVoucher = reader.IsDBNull(reader.GetOrdinal("MaVoucher")) ? "" : reader.GetString("MaVoucher");
                DatVeDTO dv = new DatVeDTO(ma, maKH, maNV, maChuyenBay, ngayDat, tongTien, giamGia, trangThai, maVoucher);
                dsdv.Add(dv);//add to arraylist dsdv
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc đặt vé theo khách hàng!");
        }
        return dsdv;
    }

    public List<DatVeDTO> DocTheoChuyenBay(string maCha)
    {
        List<DatVeDTO> dsdv = new List<DatVeDTO>();
        try
        {
            string query = "SELECT MaDV,MaKH,MaNV,MaChuyenBay,NgayDat,TongTien,GiamGia,TrangThai,MaVoucher FROM DatVe WHERE MaChuyenBay='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maKH = reader.GetString("MaKH");
                string maNV = reader.IsDBNull(reader.GetOrdinal("MaNV")) ? "" : reader.GetString("MaNV");
                string maChuyenBay = reader.GetString("MaChuyenBay");
                DateTime ngayDat = reader.GetDateTime("NgayDat");
                decimal tongTien = reader.GetDecimal("TongTien");
                decimal giamGia = reader.GetDecimal("GiamGia");
                string trangThai = reader.GetString("TrangThai");
                string maVoucher = reader.IsDBNull(reader.GetOrdinal("MaVoucher")) ? "" : reader.GetString("MaVoucher");
                DatVeDTO dv = new DatVeDTO(ma, maKH, maNV, maChuyenBay, ngayDat, tongTien, giamGia, trangThai, maVoucher);
                dsdv.Add(dv);//add to arraylist dsdv
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc đặt vé theo chuyến bay!");
        }
        return dsdv;
    }

    public bool Them(DatVeDTO dv)
    {
        try
        {
            string ngayDat = dv.NgayDat.ToString("yyyy-MM-dd HH:mm:ss");
            string tongTien = dv.TongTien.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string giamGia = dv.GiamGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string maNV = string.IsNullOrEmpty(dv.MaNV) ? "NULL" : "'" + dv.MaNV + "'";
            string maVoucher = string.IsNullOrEmpty(dv.MaVoucher) ? "NULL" : "'" + dv.MaVoucher + "'";
            string query = "INSERT INTO DatVe VALUES ('" + dv.MaDV
                + "','" + dv.MaKH + "'," + maNV + ",'" + dv.MaChuyenBay + "','" + ngayDat + "'," + tongTien + "," + giamGia
                + ",'" + dv.TrangThai + "'," + maVoucher + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            TinhTienDAO.DatVe(connection, null, dv.MaDV);
            TinhTienDAO.HoaDonTheoDatVe(connection, null, dv.MaDV);
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin đặt vé!"); return false; }
    }

    public bool Sua(DatVeDTO dv)
    {
        try
        {
            string ngayDat = dv.NgayDat.ToString("yyyy-MM-dd HH:mm:ss");
            string tongTien = dv.TongTien.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string giamGia = dv.GiamGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string maNV = string.IsNullOrEmpty(dv.MaNV) ? "NULL" : "'" + dv.MaNV + "'";
            string maVoucher = string.IsNullOrEmpty(dv.MaVoucher) ? "NULL" : "'" + dv.MaVoucher + "'";
            string query = "UPDATE DatVe SET MaKH='" + dv.MaKH + "',MaNV=" + maNV + ",MaChuyenBay='" + dv.MaChuyenBay
                + "',NgayDat='" + ngayDat + "',TongTien=" + tongTien + ",GiamGia=" + giamGia
                + ",TrangThai='" + dv.TrangThai + "',MaVoucher=" + maVoucher
                + " WHERE MaDV='" + dv.MaDV + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            TinhTienDAO.DatVe(connection, null, dv.MaDV);
            TinhTienDAO.HoaDonTheoDatVe(connection, null, dv.MaDV);
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật đặt vé!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM DatVe WHERE MaDV='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa đặt vé!"); return false; }
    }
}
