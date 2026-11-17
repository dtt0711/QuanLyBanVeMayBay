using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class NhanVienDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public NhanVienDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<NhanVienDTO> DocDSNV()
    {
        List<NhanVienDTO> dsnv = new List<NhanVienDTO>();
        try
        {
            string query = "SELECT MaNV,Ho,Ten,ChucVu FROM NhanVien";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ho = reader.GetString("Ho");
                string ten = reader.GetString("Ten");
                string chucVu = reader.IsDBNull(reader.GetOrdinal("ChucVu")) ? "" : reader.GetString("ChucVu");
                NhanVienDTO nv = new NhanVienDTO(ma, ho, ten, chucVu);
                dsnv.Add(nv);//add to arraylist dsnv
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin nhân viên!");
        }
        return dsnv;
    }

    public bool Them(NhanVienDTO nv)
    {
        try
        {
            string query = "INSERT INTO NhanVien VALUES ('" + nv.MaNV
                + "','" + nv.Ho + "','" + nv.Ten + "','" + nv.ChucVu + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin nhân viên!"); return false; }
    }

    public bool Sua(NhanVienDTO nv)
    {
        try
        {
            string query = "UPDATE NhanVien SET Ho='" + nv.Ho + "',Ten='" + nv.Ten
                + "',ChucVu='" + nv.ChucVu + "' WHERE MaNV='" + nv.MaNV + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật nhân viên!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM NhanVien WHERE MaNV='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa nhân viên!"); return false; }
    }
}
