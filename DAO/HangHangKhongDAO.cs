using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class HangHangKhongDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public HangHangKhongDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<HangHangKhongDTO> DocDS()
    {
        List<HangHangKhongDTO> dshhk = new List<HangHangKhongDTO>();
        try
        {
            string query = "SELECT MaHHK,TenHang FROM HangHangKhong";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string tenHang = reader.GetString("TenHang");
                HangHangKhongDTO hhk = new HangHangKhongDTO(ma, tenHang);
                dshhk.Add(hhk);//add to arraylist dshhk
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin hãng hàng không!");
        }
        return dshhk;
    }

    public bool Them(HangHangKhongDTO hhk)
    {
        try
        {
            string query = "INSERT INTO HangHangKhong VALUES ('" + hhk.MaHHK + "','" + hhk.TenHang + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin hãng hàng không!"); return false; }
    }

    public bool Sua(HangHangKhongDTO hhk)
    {
        try
        {
            string query = "UPDATE HangHangKhong SET TenHang='" + hhk.TenHang + "' WHERE MaHHK='" + hhk.MaHHK + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật hãng hàng không!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM HangHangKhong WHERE MaHHK='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa hãng hàng không!"); return false; }
    }
}
