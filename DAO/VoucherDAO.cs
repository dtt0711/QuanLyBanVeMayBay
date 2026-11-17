using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class VoucherDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public VoucherDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<VoucherDTO> DocDSVC()
    {
        List<VoucherDTO> dsvc = new List<VoucherDTO>();
        try
        {
            string query = "SELECT MaVoucher,MaCTKM,GiamGia FROM Voucher";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maCTKM = reader.GetString("MaCTKM");
                double giamGia = reader.GetDouble("GiamGia");
                VoucherDTO vc = new VoucherDTO(ma, maCTKM, giamGia);
                dsvc.Add(vc);//add to arraylist dsvc
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin voucher!");
        }
        return dsvc;
    }

    public List<VoucherDTO> DocTheoCTKM(string maCha)
    {
        List<VoucherDTO> dsvc = new List<VoucherDTO>();
        try
        {
            string query = "SELECT MaVoucher,MaCTKM,GiamGia FROM Voucher WHERE MaCTKM='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maCTKM = reader.GetString("MaCTKM");
                double giamGia = reader.GetDouble("GiamGia");
                VoucherDTO vc = new VoucherDTO(ma, maCTKM, giamGia);
                dsvc.Add(vc);//add to arraylist dsvc
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc voucher theo chương trình khuyến mãi!");
        }
        return dsvc;
    }

    public bool Them(VoucherDTO vc)
    {
        try
        {
            string giamGia = vc.GiamGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "INSERT INTO Voucher VALUES ('" + vc.MaVoucher
                + "','" + vc.MaCTKM + "'," + giamGia + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin voucher!"); return false; }
    }

    public bool Sua(VoucherDTO vc)
    {
        try
        {
            string giamGia = vc.GiamGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "UPDATE Voucher SET MaCTKM='" + vc.MaCTKM + "',GiamGia=" + giamGia
                + " WHERE MaVoucher='" + vc.MaVoucher + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật voucher!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM Voucher WHERE MaVoucher='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa voucher!"); return false; }
    }
}
