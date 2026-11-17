using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class ThamSoDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public ThamSoDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<ThamSoDTO> DocDSTS()
    {
        List<ThamSoDTO> dsts = new List<ThamSoDTO>();
        try
        {
            string query = "SELECT MaThamSo,TenThamSo,GiaTri,DonVi FROM ThamSo";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string ten = reader.GetString("TenThamSo");
                decimal giaTri = reader.GetDecimal("GiaTri");
                string donVi = reader.IsDBNull(reader.GetOrdinal("DonVi")) ? "" : reader.GetString("DonVi");
                ThamSoDTO ts = new ThamSoDTO(ma, ten, giaTri, donVi);
                dsts.Add(ts);//add to arraylist dsts
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc bảng tham số!");
        }
        return dsts;
    }

    public bool Them(ThamSoDTO ts)
    {
        try
        {
            string giaTri = ts.GiaTri.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "INSERT INTO ThamSo VALUES ('" + ts.MaThamSo + "','" + ts.TenThamSo
                + "'," + giaTri + ",'" + ts.DonVi + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi tham số!"); return false; }
    }

    public bool Sua(ThamSoDTO ts)
    {
        try
        {
            string giaTri = ts.GiaTri.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "UPDATE ThamSo SET TenThamSo='" + ts.TenThamSo + "',GiaTri=" + giaTri
                + ",DonVi='" + ts.DonVi + "' WHERE MaThamSo='" + ts.MaThamSo + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật tham số!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM ThamSo WHERE MaThamSo='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa tham số!"); return false; }
    }
}
