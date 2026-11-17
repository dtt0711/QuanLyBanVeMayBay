using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class ChuyenBayDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public ChuyenBayDAO()
    {
        MyConnection.Mo(connection);
    }

    private ChuyenBayDTO DocDong(MySqlDataReader reader)
    {
        string ma = reader.GetString(0);
        string maHHK = reader.GetString("MaHHK");
        string maSanBayDi = reader.GetString("MaSanBayDi");
        string maSanBayDen = reader.GetString("MaSanBayDen");
        string maMayBay = reader.GetString("MaMayBay");
        DateTime ngayKhoiHanh = reader.GetDateTime("NgayKhoiHanh");
        DateTime ngayDen = reader.GetDateTime("NgayDen");
        int thoiGianBay = reader.IsDBNull(reader.GetOrdinal("ThoiGianBay")) ? 0 : reader.GetInt32("ThoiGianBay");
        string thoiGianDen = reader.IsDBNull(reader.GetOrdinal("ThoiGianDen")) ? "" : reader.GetTimeSpan("ThoiGianDen").ToString(@"hh\:mm\:ss");
        int tongSoGhe = reader.GetInt32("TongSoGhe");
        int soVeDaBan = reader.GetInt32("SoVeDaBan");
        decimal giaVe = reader.GetDecimal("GiaVe");
        string trangThai = reader.GetString("TrangThai");
        return new ChuyenBayDTO(ma, maHHK, maSanBayDi, maSanBayDen, maMayBay, ngayKhoiHanh, ngayDen, thoiGianBay, thoiGianDen, tongSoGhe, soVeDaBan, giaVe, trangThai);
    }

    public List<ChuyenBayDTO> DocDSCB()
    {
        List<ChuyenBayDTO> dscb = new List<ChuyenBayDTO>();
        try
        {
            string query = "SELECT MaChuyenBay,MaHHK,MaSanBayDi,MaSanBayDen,MaMayBay,NgayKhoiHanh,NgayDen,ThoiGianBay,ThoiGianDen,TongSoGhe,SoVeDaBan,GiaVe,TrangThai FROM ChuyenBay";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ChuyenBayDTO cb = DocDong(reader);
                dscb.Add(cb);//add to arraylist dscb
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin chuyến bay!");
        }
        return dscb;
    }

    public List<ChuyenBayDTO> DocTheoMayBay(string maCha)
    {
        List<ChuyenBayDTO> dscb = new List<ChuyenBayDTO>();
        try
        {
            string query = "SELECT MaChuyenBay,MaHHK,MaSanBayDi,MaSanBayDen,MaMayBay,NgayKhoiHanh,NgayDen,ThoiGianBay,ThoiGianDen,TongSoGhe,SoVeDaBan,GiaVe,TrangThai FROM ChuyenBay WHERE MaMayBay='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ChuyenBayDTO cb = DocDong(reader);
                dscb.Add(cb);//add to arraylist dscb
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc chuyến bay theo máy bay!");
        }
        return dscb;
    }

    public bool Them(ChuyenBayDTO cb)
    {
        try
        {
            string ngayKhoiHanh = cb.NgayKhoiHanh.ToString("yyyy-MM-dd HH:mm:ss");
            string ngayDen = cb.NgayDen.ToString("yyyy-MM-dd HH:mm:ss");
            string thoiGianDen = string.IsNullOrEmpty(cb.ThoiGianDen) ? "NULL" : "'" + cb.ThoiGianDen + "'";
            string giaVe = cb.GiaVe.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "INSERT INTO ChuyenBay VALUES ('" + cb.MaChuyenBay
                + "','" + cb.MaHHK + "','" + cb.MaSanBayDi + "','" + cb.MaSanBayDen
                + "','" + cb.MaMayBay + "','" + ngayKhoiHanh + "','" + ngayDen + "'," + cb.ThoiGianBay
                + "," + thoiGianDen + ",(SELECT COUNT(*) FROM Ghe WHERE MaMayBay='" + cb.MaMayBay + "'),0," + giaVe
                + ",'" + cb.TrangThai + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin chuyến bay!"); return false; }
    }

    public bool Sua(ChuyenBayDTO cb)
    {
        try
        {
            string ngayKhoiHanh = cb.NgayKhoiHanh.ToString("yyyy-MM-dd HH:mm:ss");
            string ngayDen = cb.NgayDen.ToString("yyyy-MM-dd HH:mm:ss");
            string thoiGianDen = string.IsNullOrEmpty(cb.ThoiGianDen) ? "NULL" : "'" + cb.ThoiGianDen + "'";
            string giaVe = cb.GiaVe.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "UPDATE ChuyenBay SET MaHHK='" + cb.MaHHK + "',MaSanBayDi='" + cb.MaSanBayDi
                + "',MaSanBayDen='" + cb.MaSanBayDen + "',MaMayBay='" + cb.MaMayBay
                + "',NgayKhoiHanh='" + ngayKhoiHanh + "',NgayDen='" + ngayDen
                + "',ThoiGianBay=" + cb.ThoiGianBay + ",ThoiGianDen=" + thoiGianDen
                + ",TongSoGhe=(SELECT COUNT(*) FROM Ghe WHERE MaMayBay='" + cb.MaMayBay + "')"
                + ",GiaVe=" + giaVe + ",TrangThai='" + cb.TrangThai
                + "' WHERE MaChuyenBay='" + cb.MaChuyenBay + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật chuyến bay!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM ChuyenBay WHERE MaChuyenBay='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa chuyến bay!"); return false; }
    }
}
