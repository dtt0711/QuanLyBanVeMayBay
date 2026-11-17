using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class MayBayDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public MayBayDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<MayBayDTO> DocDSMB()
    {
        List<MayBayDTO> dsmb = new List<MayBayDTO>();
        try
        {
            string query = "SELECT MaMayBay,MaHHK FROM MayBay";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maHHK = reader.GetString("MaHHK");
                MayBayDTO mb = new MayBayDTO(ma, maHHK, "", 0);
                dsmb.Add(mb);//add to arraylist dsmb
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin máy bay!");
        }
        return dsmb;
    }

    public List<MayBayDTO> DocTheoHHK(string maCha)
    {
        List<MayBayDTO> dsmb = new List<MayBayDTO>();
        try
        {
            string query = "SELECT MaMayBay,MaHHK FROM MayBay WHERE MaHHK='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maHHK = reader.GetString("MaHHK");
                MayBayDTO mb = new MayBayDTO(ma, maHHK, "", 0);
                dsmb.Add(mb);//add to arraylist dsmb
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc máy bay theo hãng hàng không!");
        }
        return dsmb;
    }

    public bool Them(MayBayDTO mb)
    {
        try
        {
            string query = "INSERT INTO MayBay VALUES ('" + mb.MaMayBay + "','" + mb.MaHHK + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin máy bay!"); return false; }
    }

    public bool Sua(MayBayDTO mb)
    {
        try
        {
            string query = "UPDATE MayBay SET MaHHK='" + mb.MaHHK + "' WHERE MaMayBay='" + mb.MaMayBay + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật máy bay!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM MayBay WHERE MaMayBay='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa máy bay!"); return false; }
    }
}
