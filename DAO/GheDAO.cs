using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class GheDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public GheDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<GheDTO> DocDSGhe()
    {
        List<GheDTO> dsghe = new List<GheDTO>();
        try
        {
            string query = "SELECT MaGhe,MaMayBay,MaHV FROM Ghe";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maMayBay = reader.GetString("MaMayBay");
                string maHV = reader.GetString("MaHV");
                GheDTO g = new GheDTO(ma, maMayBay, maHV);
                dsghe.Add(g);//add to arraylist dsghe
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin ghế!");
        }
        return dsghe;
    }

    public List<GheDTO> DocTheoMayBay(string maCha)
    {
        List<GheDTO> dsghe = new List<GheDTO>();
        try
        {
            string query = "SELECT MaGhe,MaMayBay,MaHV FROM Ghe WHERE MaMayBay='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maMayBay = reader.GetString("MaMayBay");
                string maHV = reader.GetString("MaHV");
                GheDTO g = new GheDTO(ma, maMayBay, maHV);
                dsghe.Add(g);//add to arraylist dsghe
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc ghế theo máy bay!");
        }
        return dsghe;
    }

    public List<GheDTO> DocTheoHV(string maCha)
    {
        List<GheDTO> dsghe = new List<GheDTO>();
        try
        {
            string query = "SELECT MaGhe,MaMayBay,MaHV FROM Ghe WHERE MaHV='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maMayBay = reader.GetString("MaMayBay");
                string maHV = reader.GetString("MaHV");
                GheDTO g = new GheDTO(ma, maMayBay, maHV);
                dsghe.Add(g);//add to arraylist dsghe
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc ghế theo hạng vé!");
        }
        return dsghe;
    }

    public bool Them(GheDTO g)
    {
        MySqlTransaction tran = null;
        try
        {
            tran = connection.BeginTransaction();
            string query = "INSERT INTO Ghe VALUES ('" + g.MaGhe + "','" + g.MaMayBay + "','" + g.MaHV + "')";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            command.ExecuteNonQuery();
            TinhTienDAO.TongSoGhe(connection, tran);
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi ghi thông tin ghế!");
            return false;
        }
    }

    public bool Sua(GheDTO g)
    {
        MySqlTransaction tran = null;
        try
        {
            tran = connection.BeginTransaction();
            string query = "UPDATE Ghe SET MaMayBay='" + g.MaMayBay + "',MaHV='" + g.MaHV
                + "' WHERE MaGhe='" + g.MaGhe + "'";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            command.ExecuteNonQuery();
            TinhTienDAO.TongSoGhe(connection, tran);
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi cập nhật ghế!");
            return false;
        }
    }

    public bool Xoa(string ma)
    {
        MySqlTransaction tran = null;
        try
        {
            tran = connection.BeginTransaction();
            string query = "DELETE FROM Ghe WHERE MaGhe='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            command.ExecuteNonQuery();
            TinhTienDAO.TongSoGhe(connection, tran);
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi xóa ghế (ghế đã bán vé thì không xóa được)!");
            return false;
        }
    }
}
