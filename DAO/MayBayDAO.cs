using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class MayBayDAO
{
    public List<MayBayDTO> DocDSMB()
    {
        List<MayBayDTO> dsmb = new List<MayBayDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaMayBay,MaHHK,LoaiMayBay,SoGhe FROM MayBay";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maHHK = reader.GetString("MaHHK");
                string loaiMayBay = reader.IsDBNull(reader.GetOrdinal("LoaiMayBay")) ? "" : reader.GetString("LoaiMayBay");
                int soGhe = reader.GetInt32("SoGhe");
                MayBayDTO mb = new MayBayDTO(ma, maHHK, loaiMayBay, soGhe);
                dsmb.Add(mb);//add to arraylist dsmb
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc thông tin máy bay!");
        }
        finally
        {
            connection.Close();
        }
        return dsmb;
    }

    public List<MayBayDTO> DocTheoHHK(string maCha)
    {
        List<MayBayDTO> dsmb = new List<MayBayDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaMayBay,MaHHK,LoaiMayBay,SoGhe FROM MayBay WHERE MaHHK='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maHHK = reader.GetString("MaHHK");
                string loaiMayBay = reader.IsDBNull(reader.GetOrdinal("LoaiMayBay")) ? "" : reader.GetString("LoaiMayBay");
                int soGhe = reader.GetInt32("SoGhe");
                MayBayDTO mb = new MayBayDTO(ma, maHHK, loaiMayBay, soGhe);
                dsmb.Add(mb);//add to arraylist dsmb
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc máy bay theo mã liên quan!");
        }
        finally
        {
            connection.Close();
        }
        return dsmb;
    }

    public void Them(MayBayDTO mb)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = mb.MaMayBay;
            string maHHK = mb.MaHHK;
            string loaiMayBay = mb.LoaiMayBay;
            int soGhe = mb.SoGhe;
            string query = "INSERT INTO MayBay(MaMayBay,MaHHK,LoaiMayBay,SoGhe) VALUES ('" + ma + "','" + maHHK + "','" + loaiMayBay + "'," + soGhe + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin máy bay!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(MayBayDTO mb)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = mb.MaMayBay;
            string maHHK = mb.MaHHK;
            string loaiMayBay = mb.LoaiMayBay;
            int soGhe = mb.SoGhe;
            string query = "UPDATE MayBay SET MaHHK='" + maHHK + "', LoaiMayBay='" + loaiMayBay + "', SoGhe=" + soGhe + " WHERE MaMayBay='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin máy bay!");
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
            string query = "DELETE FROM MayBay WHERE MaMayBay='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin máy bay!");
        }
        finally
        {
            connection.Close();
        }
    }
}
