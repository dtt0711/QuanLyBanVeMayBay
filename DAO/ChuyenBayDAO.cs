using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class ChuyenBayDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public ChuyenBayDAO()
    {
        connection.Open();
    }

    public List<ChuyenBayDTO> DocDSCB()
    {
        List<ChuyenBayDTO> dscb = new List<ChuyenBayDTO>();
        try
        {
            string query = "SELECT MaCB,MaMayBay,TuyenBay,NgayBay,GiaVe,SoGheTrong FROM ChuyenBay";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maMayBay = reader.GetString("MaMayBay");
                string tuyenBay = reader.GetString("TuyenBay");
                DateTime ngayBay = reader.GetDateTime("NgayBay");
                decimal giaVe = reader.GetDecimal("GiaVe");
                int soGheTrong = reader.GetInt32("SoGheTrong");
                ChuyenBayDTO cb = new ChuyenBayDTO(ma, maMayBay, tuyenBay, ngayBay, giaVe, soGheTrong);
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
            string query = "SELECT MaCB,MaMayBay,TuyenBay,NgayBay,GiaVe,SoGheTrong FROM ChuyenBay WHERE MaMayBay='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maMayBay = reader.GetString("MaMayBay");
                string tuyenBay = reader.GetString("TuyenBay");
                DateTime ngayBay = reader.GetDateTime("NgayBay");
                decimal giaVe = reader.GetDecimal("GiaVe");
                int soGheTrong = reader.GetInt32("SoGheTrong");
                ChuyenBayDTO cb = new ChuyenBayDTO(ma, maMayBay, tuyenBay, ngayBay, giaVe, soGheTrong);
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

    public void Them(ChuyenBayDTO cb)
    {
        try
        {
            string ngayBay = cb.NgayBay.ToString("yyyy-MM-dd HH:mm:ss");
            string giaVe = cb.GiaVe.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "INSERT INTO ChuyenBay VALUES ('" + cb.MaCB
                + "','" + cb.MaMayBay + "','" + cb.TuyenBay + "','" + ngayBay + "'," + giaVe + "," + cb.SoGheTrong + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch { MessageBox.Show("Lỗi ghi thông tin chuyến bay!"); }
    }

    public void Sua(ChuyenBayDTO cb)
    {
        try
        {
            string ngayBay = cb.NgayBay.ToString("yyyy-MM-dd HH:mm:ss");
            string giaVe = cb.GiaVe.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "UPDATE ChuyenBay SET MaMayBay='" + cb.MaMayBay + "',TuyenBay='" + cb.TuyenBay
                + "',NgayBay='" + ngayBay + "',GiaVe=" + giaVe + ",SoGheTrong=" + cb.SoGheTrong
                + " WHERE MaCB='" + cb.MaCB + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch { MessageBox.Show("Lỗi cập nhật chuyến bay!"); }
    }

    public void Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM ChuyenBay WHERE MaCB='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch { MessageBox.Show("Lỗi xóa chuyến bay!"); }
    }
}
