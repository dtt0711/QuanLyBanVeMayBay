using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class CTKMDAO
{
    public List<CTKMDTO> DocDSCTKM()
    {
        List<CTKMDTO> dsctkm = new List<CTKMDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaCTKM,TenCTKM,NgayBatDau,NgayKetThuc FROM CTKM";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string tenCTKM = reader.GetString("TenCTKM");
                DateTime ngayBatDau = reader.GetDateTime("NgayBatDau");
                DateTime ngayKetThuc = reader.GetDateTime("NgayKetThuc");
                CTKMDTO ctkm = new CTKMDTO(ma, tenCTKM, ngayBatDau, ngayKetThuc);
                dsctkm.Add(ctkm);//add to arraylist dsctkm
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc thông tin chương trình khuyến mãi!");
        }
        finally
        {
            connection.Close();
        }
        return dsctkm;
    }

    public void Them(CTKMDTO ctkm)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = ctkm.MaCTKM;
            string tenCTKM = ctkm.TenCTKM;
            string ngayBatDau = ctkm.NgayBatDau.ToString("yyyy-MM-dd");
            string ngayKetThuc = ctkm.NgayKetThuc.ToString("yyyy-MM-dd");
            string query = "INSERT INTO CTKM(MaCTKM,TenCTKM,NgayBatDau,NgayKetThuc) VALUES ('" + ma + "','" + tenCTKM + "','" + ngayBatDau + "','" + ngayKetThuc + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin chương trình khuyến mãi!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(CTKMDTO ctkm)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = ctkm.MaCTKM;
            string tenCTKM = ctkm.TenCTKM;
            string ngayBatDau = ctkm.NgayBatDau.ToString("yyyy-MM-dd");
            string ngayKetThuc = ctkm.NgayKetThuc.ToString("yyyy-MM-dd");
            string query = "UPDATE CTKM SET TenCTKM='" + tenCTKM + "', NgayBatDau='" + ngayBatDau + "', NgayKetThuc='" + ngayKetThuc + "' WHERE MaCTKM='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin chương trình khuyến mãi!");
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
            string query = "DELETE FROM CTKM WHERE MaCTKM='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin chương trình khuyến mãi!");
        }
        finally
        {
            connection.Close();
        }
    }
}
