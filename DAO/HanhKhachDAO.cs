using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class HanhKhachDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public HanhKhachDAO()
    {
        MyConnection.Mo(connection);
    }

    private HanhKhachDTO DocDong(MySqlDataReader reader)
    {
        string ma = reader.GetString(0);
        string hoTen = reader.GetString("HoTen");
        DateTime ngaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? DateTime.MinValue : reader.GetDateTime("NgaySinh");
        string cccd = reader.IsDBNull(reader.GetOrdinal("CCCD")) ? "" : reader.GetString("CCCD");
        string gioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString("GioiTinh");
        string maKH = reader.GetString("MaKH");
        string loaiHK = reader.IsDBNull(reader.GetOrdinal("LoaiHK")) ? "" : reader.GetString("LoaiHK");
        return new HanhKhachDTO(ma, hoTen, ngaySinh, cccd, gioiTinh, maKH, loaiHK);
    }

    public List<HanhKhachDTO> DocDSHK()
    {
        List<HanhKhachDTO> dshk = new List<HanhKhachDTO>();
        try
        {
            string query = "SELECT MaHK,HoTen,NgaySinh,CCCD,GioiTinh,MaKH,LoaiHK FROM HanhKhach";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HanhKhachDTO hk = DocDong(reader);
                dshk.Add(hk);//add to arraylist dshk
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin hành khách!");
        }
        return dshk;
    }

    public List<HanhKhachDTO> DocTheoKH(string maCha)
    {
        List<HanhKhachDTO> dshk = new List<HanhKhachDTO>();
        try
        {
            string query = "SELECT MaHK,HoTen,NgaySinh,CCCD,GioiTinh,MaKH,LoaiHK FROM HanhKhach WHERE MaKH='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HanhKhachDTO hk = DocDong(reader);
                dshk.Add(hk);//add to arraylist dshk
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc hành khách theo khách hàng!");
        }
        return dshk;
    }

    public bool Them(HanhKhachDTO hk)
    {
        try
        {
            string ngaySinh = hk.NgaySinh == DateTime.MinValue ? "NULL" : "'" + hk.NgaySinh.ToString("yyyy-MM-dd") + "'";
            string cccd = string.IsNullOrEmpty(hk.CCCD) ? "NULL" : "'" + hk.CCCD + "'";
            string gioiTinh = string.IsNullOrEmpty(hk.GioiTinh) ? "NULL" : "'" + hk.GioiTinh + "'";
            string loaiHK = string.IsNullOrEmpty(hk.LoaiKH) ? "NULL" : "'" + hk.LoaiKH + "'";
            string query = "INSERT INTO HanhKhach VALUES ('" + hk.MaHK
                + "','" + hk.HoTen + "'," + ngaySinh + "," + cccd
                + "," + gioiTinh + ",'" + hk.MaKH + "'," + loaiHK + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin hành khách!"); return false; }
    }

    public bool Sua(HanhKhachDTO hk)
    {
        try
        {
            string ngaySinh = hk.NgaySinh == DateTime.MinValue ? "NULL" : "'" + hk.NgaySinh.ToString("yyyy-MM-dd") + "'";
            string cccd = string.IsNullOrEmpty(hk.CCCD) ? "NULL" : "'" + hk.CCCD + "'";
            string gioiTinh = string.IsNullOrEmpty(hk.GioiTinh) ? "NULL" : "'" + hk.GioiTinh + "'";
            string loaiHK = string.IsNullOrEmpty(hk.LoaiKH) ? "NULL" : "'" + hk.LoaiKH + "'";
            string query = "UPDATE HanhKhach SET HoTen='" + hk.HoTen + "',NgaySinh=" + ngaySinh
                + ",CCCD=" + cccd + ",GioiTinh=" + gioiTinh + ",MaKH='" + hk.MaKH
                + "',LoaiHK=" + loaiHK + " WHERE MaHK='" + hk.MaHK + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật hành khách!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM HanhKhach WHERE MaHK='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa hành khách!"); return false; }
    }
}
