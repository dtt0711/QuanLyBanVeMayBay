using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class KhachHangDAO
{
    public List<KhachHangDTO> DocDSKH()
    {
        List<KhachHangDTO> dskh = new List<KhachHangDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaKH,HoTen,CCCD,SDT,Email,TaiKhoan,MatKhau,DiaChi FROM KhachHang";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string hoTen = reader.GetString("HoTen");
                string cccd = reader.GetString("CCCD");
                string sdt = reader.GetString("SDT");
                string email = reader.GetString("Email");
                string taiKhoan = reader.GetString("TaiKhoan");
                string matKhau = reader.GetString("MatKhau");
                string diaChi = reader.GetString("DiaChi");
                KhachHangDTO kh = new KhachHangDTO(ma, hoTen, cccd, sdt, email, taiKhoan, matKhau, diaChi);
                dskh.Add(kh);//add to arraylist dskh
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc thông tin khách hàng!");
        }
        finally
        {
            connection.Close();
        }
        return dskh;
    }

    public void Them(KhachHangDTO kh)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = kh.MaKH;
            string hoTen = kh.HoTen;
            string cccd = kh.CCCD;
            string sdt = kh.SDT;
            string email = kh.Email;
            string taiKhoan = kh.TaiKhoan;
            string matKhau = kh.MatKhau;
            string diaChi = kh.DiaChi;
            string query = "INSERT INTO KhachHang(MaKH,HoTen,CCCD,SDT,Email,TaiKhoan,MatKhau,DiaChi) VALUES ('" + ma + "','" + hoTen + "','" + cccd + "','" + sdt + "','" + email + "','" + taiKhoan + "','" + matKhau + "','" + diaChi + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin khách hàng!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(KhachHangDTO kh)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = kh.MaKH;
            string hoTen = kh.HoTen;
            string cccd = kh.CCCD;
            string sdt = kh.SDT;
            string email = kh.Email;
            string taiKhoan = kh.TaiKhoan;
            string matKhau = kh.MatKhau;
            string diaChi = kh.DiaChi;
            string query = "UPDATE KhachHang SET HoTen='" + hoTen + "', CCCD='" + cccd + "', SDT='" + sdt + "', Email='" + email + "', TaiKhoan='" + taiKhoan + "', MatKhau='" + matKhau + "', DiaChi='" + diaChi + "' WHERE MaKH='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin khách hàng!");
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
            string query = "DELETE FROM KhachHang WHERE MaKH='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin khách hàng!");
        }
        finally
        {
            connection.Close();
        }
    }
}
