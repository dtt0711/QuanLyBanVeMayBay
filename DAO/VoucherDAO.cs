using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class VoucherDAO
{
    public List<VoucherDTO> DocDSVC()
    {
        List<VoucherDTO> dsvc = new List<VoucherDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaVoucher,MaCTKM,GiamGia FROM Voucher";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
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
            throw new Exception("Lỗi đọc thông tin voucher!");
        }
        finally
        {
            connection.Close();
        }
        return dsvc;
    }

    public List<VoucherDTO> DocTheoCTKM(string maCha)
    {
        List<VoucherDTO> dsvc = new List<VoucherDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaVoucher,MaCTKM,GiamGia FROM Voucher WHERE MaCTKM='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
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
            throw new Exception("Lỗi đọc voucher theo mã liên quan!");
        }
        finally
        {
            connection.Close();
        }
        return dsvc;
    }

    public void Them(VoucherDTO vc)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = vc.MaVoucher;
            string maCTKM = vc.MaCTKM;
            string giamGia = vc.GiamGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "INSERT INTO Voucher(MaVoucher,MaCTKM,GiamGia) VALUES ('" + ma + "','" + maCTKM + "'," + giamGia + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin voucher!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(VoucherDTO vc)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = vc.MaVoucher;
            string maCTKM = vc.MaCTKM;
            string giamGia = vc.GiamGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "UPDATE Voucher SET MaCTKM='" + maCTKM + "', GiamGia=" + giamGia + " WHERE MaVoucher='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin voucher!");
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
            string query = "DELETE FROM Voucher WHERE MaVoucher='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin voucher!");
        }
        finally
        {
            connection.Close();
        }
    }
}
