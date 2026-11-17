using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class CTKMDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public CTKMDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<CTKMDTO> DocDSCTKM()
    {
        List<CTKMDTO> dsctkm = new List<CTKMDTO>();
        try
        {
            string query = "SELECT MaCTKM,TenCTKM,NgayBatDau,NgayKetThuc FROM CTKM";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
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
            MessageBox.Show("Lỗi đọc thông tin chương trình khuyến mãi!");
        }
        return dsctkm;
    }

    public bool Them(CTKMDTO ctkm)
    {
        try
        {
            string ngayBatDau = ctkm.NgayBatDau.ToString("yyyy-MM-dd HH:mm:ss");
            string ngayKetThuc = ctkm.NgayKetThuc.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "INSERT INTO CTKM VALUES ('" + ctkm.MaCTKM
                + "','" + ctkm.TenCTKM + "','" + ngayBatDau + "','" + ngayKetThuc + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin chương trình khuyến mãi!"); return false; }
    }

    public bool Sua(CTKMDTO ctkm)
    {
        try
        {
            string ngayBatDau = ctkm.NgayBatDau.ToString("yyyy-MM-dd HH:mm:ss");
            string ngayKetThuc = ctkm.NgayKetThuc.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "UPDATE CTKM SET TenCTKM='" + ctkm.TenCTKM + "',NgayBatDau='" + ngayBatDau
                + "',NgayKetThuc='" + ngayKetThuc + "' WHERE MaCTKM='" + ctkm.MaCTKM + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật chương trình khuyến mãi!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM CTKM WHERE MaCTKM='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa chương trình khuyến mãi!"); return false; }
    }
}
