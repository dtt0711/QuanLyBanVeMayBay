using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class HanhKhachDAO
{
    public List<HanhKhachDTO> DocDSHK()
    {
        List<HanhKhachDTO> dshk = new List<HanhKhachDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaHK,HoTen,NgaySinh,CCCD,GioiTinh,MaKH,LoaiKH FROM HanhKhach";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string hoTen = reader.GetString("HoTen");
                DateTime ngaySinh = reader.GetDateTime("NgaySinh");
                string cccd = reader.GetString("CCCD");
                string gioiTinh = reader.GetString("GioiTinh");
                string maKH = reader.GetString("MaKH");
                string loaiKH = reader.GetString("LoaiKH");
                HanhKhachDTO hk = new HanhKhachDTO(ma, hoTen, ngaySinh, cccd, gioiTinh, maKH, loaiKH);
                dshk.Add(hk);//add to arraylist dshk
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc thông tin hành khách!");
        }
        finally
        {
            connection.Close();
        }
        return dshk;
    }

    public List<HanhKhachDTO> DocTheoKH(string maCha)
    {
        List<HanhKhachDTO> dshk = new List<HanhKhachDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaHK,HoTen,NgaySinh,CCCD,GioiTinh,MaKH,LoaiKH FROM HanhKhach WHERE MaKH='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string hoTen = reader.GetString("HoTen");
                DateTime ngaySinh = reader.GetDateTime("NgaySinh");
                string cccd = reader.GetString("CCCD");
                string gioiTinh = reader.GetString("GioiTinh");
                string maKH = reader.GetString("MaKH");
                string loaiKH = reader.GetString("LoaiKH");
                HanhKhachDTO hk = new HanhKhachDTO(ma, hoTen, ngaySinh, cccd, gioiTinh, maKH, loaiKH);
                dshk.Add(hk);//add to arraylist dshk
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc hành khách theo mã liên quan!");
        }
        finally
        {
            connection.Close();
        }
        return dshk;
    }

    public void Them(HanhKhachDTO hk)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = hk.MaHK;
            string hoTen = hk.HoTen;
            string ngaySinh = hk.NgaySinh.ToString("yyyy-MM-dd");
            string cccd = hk.CCCD;
            string gioiTinh = hk.GioiTinh;
            string maKH = hk.MaKH;
            string loaiKH = hk.LoaiKH;
            string query = "INSERT INTO HanhKhach(MaHK,HoTen,NgaySinh,CCCD,GioiTinh,MaKH,LoaiKH) VALUES ('" + ma + "','" + hoTen + "','" + ngaySinh + "','" + cccd + "','" + gioiTinh + "','" + maKH + "','" + loaiKH + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin hành khách!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(HanhKhachDTO hk)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = hk.MaHK;
            string hoTen = hk.HoTen;
            string ngaySinh = hk.NgaySinh.ToString("yyyy-MM-dd");
            string cccd = hk.CCCD;
            string gioiTinh = hk.GioiTinh;
            string maKH = hk.MaKH;
            string loaiKH = hk.LoaiKH;
            string query = "UPDATE HanhKhach SET HoTen='" + hoTen + "', NgaySinh='" + ngaySinh + "', CCCD='" + cccd + "', GioiTinh='" + gioiTinh + "', MaKH='" + maKH + "', LoaiKH='" + loaiKH + "' WHERE MaHK='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin hành khách!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Xoa(string ma)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "DELETE FROM HanhKhach WHERE MaHK='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin hành khách!");
        }
        finally
        {
            connection.Close();
        }
    }
}
