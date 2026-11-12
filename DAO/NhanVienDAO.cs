using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class NhanVienDAO
{
    public List<NhanVienDTO> DocDSNV()
    {
        List<NhanVienDTO> dsnv = new List<NhanVienDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaNV,Ho,Ten,ChucVu FROM NhanVien";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ho = reader.GetString("Ho");
                string ten = reader.GetString("Ten");
                string chucVu = reader.GetString("ChucVu");
                NhanVienDTO nv = new NhanVienDTO(ma, ho, ten, chucVu);
                dsnv.Add(nv);//add to arraylist dsnv
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc thông tin nhân viên!");
        }
        finally
        {
            connection.Close();
        }
        return dsnv;
    }

    public void Them(NhanVienDTO nv)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = nv.MaNV;
            string ho = nv.Ho;
            string ten = nv.Ten;
            string chucVu = nv.ChucVu;
            string query = "INSERT INTO NhanVien(MaNV,Ho,Ten,ChucVu) VALUES ('" + ma + "','" + ho + "','" + ten + "','" + chucVu + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin nhân viên!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(NhanVienDTO nv)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = nv.MaNV;
            string ho = nv.Ho;
            string ten = nv.Ten;
            string chucVu = nv.ChucVu;
            string query = "UPDATE NhanVien SET Ho='" + ho + "', Ten='" + ten + "', ChucVu='" + chucVu + "' WHERE MaNV='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin nhân viên!");
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
            string query = "DELETE FROM NhanVien WHERE MaNV='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin nhân viên!");
        }
        finally
        {
            connection.Close();
        }
    }
}
