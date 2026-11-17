using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class SanBayDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public SanBayDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<SanBayDTO> DocDSSB()
    {
        List<SanBayDTO> dssb = new List<SanBayDTO>();
        try
        {
            string query = "SELECT MaSanBay,Ten,LoaiSanBay FROM SanBay";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString("Ten");
                string loai = reader.IsDBNull(reader.GetOrdinal("LoaiSanBay")) ? "" : reader.GetString("LoaiSanBay");
                SanBayDTO sb = new SanBayDTO(ma, ten, loai);
                dssb.Add(sb);//add to arraylist dssb
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin sân bay!");
        }
        return dssb;
    }

    public bool Them(SanBayDTO sb)
    {
        try
        {
            string loai = string.IsNullOrEmpty(sb.LoaiSanBay) ? "NULL" : "'" + sb.LoaiSanBay + "'";
            string query = "INSERT INTO SanBay VALUES ('" + sb.MaSanBay + "','" + sb.Ten + "'," + loai + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin sân bay!"); return false; }
    }

    public bool Sua(SanBayDTO sb)
    {
        try
        {
            string loai = string.IsNullOrEmpty(sb.LoaiSanBay) ? "NULL" : "'" + sb.LoaiSanBay + "'";
            string query = "UPDATE SanBay SET Ten='" + sb.Ten + "',LoaiSanBay=" + loai
                + " WHERE MaSanBay='" + sb.MaSanBay + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật sân bay!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM SanBay WHERE MaSanBay='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa sân bay!"); return false; }
    }
}
