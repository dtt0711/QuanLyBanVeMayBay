using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class HangVeDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public HangVeDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<HangVeDTO> DocDSHV()
    {
        List<HangVeDTO> dshv = new List<HangVeDTO>();
        try
        {
            string query = "SELECT MaHV,TenHV,HanhLyKg,HeSoGia FROM HangVe";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string tenHV = reader.GetString("TenHV");
                int hanhLyKg = reader.GetInt32("HanhLyKg");
                decimal heSoGia = reader.GetDecimal("HeSoGia");
                HangVeDTO hv = new HangVeDTO(ma, tenHV, hanhLyKg, heSoGia);
                dshv.Add(hv);//add to arraylist dshv
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin hạng vé!");
        }
        return dshv;
    }

    public List<HangVeDTO> DocTheoGhe(string maCha)
    {
        List<HangVeDTO> dshv = new List<HangVeDTO>();
        try
        {
            string query = "SELECT h.MaHV,h.TenHV,h.HanhLyKg,h.HeSoGia FROM HangVe h INNER JOIN Ghe g ON h.MaHV=g.MaHV WHERE g.MaGhe='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string tenHV = reader.GetString("TenHV");
                int hanhLyKg = reader.GetInt32("HanhLyKg");
                decimal heSoGia = reader.GetDecimal("HeSoGia");
                HangVeDTO hv = new HangVeDTO(ma, tenHV, hanhLyKg, heSoGia);
                dshv.Add(hv);//add to arraylist dshv
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc hạng vé theo ghế!");
        }
        return dshv;
    }

    public bool Them(HangVeDTO hv)
    {
        try
        {
            string heSoGia = hv.HeSoGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "INSERT INTO HangVe VALUES ('" + hv.MaHV + "','" + hv.TenHV + "'," + hv.HanhLyKg + "," + heSoGia + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin hạng vé!"); return false; }
    }

    public bool Sua(HangVeDTO hv)
    {
        try
        {
            string heSoGia = hv.HeSoGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "UPDATE HangVe SET TenHV='" + hv.TenHV + "',HanhLyKg=" + hv.HanhLyKg
                + ",HeSoGia=" + heSoGia + " WHERE MaHV='" + hv.MaHV + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật hạng vé!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM HangVe WHERE MaHV='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa hạng vé!"); return false; }
    }
}
