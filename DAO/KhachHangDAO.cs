using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class KhachHangDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public KhachHangDAO()
    {
        MyConnection.Mo(connection);
    }

    public List<KhachHangDTO> DocDSKH()
    {
        List<KhachHangDTO> dskh = new List<KhachHangDTO>();
        try
        {
            string query = "SELECT MaKH,HoTen,CCCD,SDT,Email,TaiKhoan,MatKhau,DiaChi FROM KhachHang";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string hoTen = reader.GetString("HoTen");
                string cccd = reader.GetString("CCCD");
                string sdt = reader.IsDBNull(reader.GetOrdinal("SDT")) ? "" : reader.GetString("SDT");
                string email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email");
                string taiKhoan = reader.GetString("TaiKhoan");
                string matKhau = reader.GetString("MatKhau");
                string diaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString("DiaChi");
                KhachHangDTO kh = new KhachHangDTO(ma, hoTen, cccd, sdt, email, taiKhoan, matKhau, diaChi);
                dskh.Add(kh);//add to arraylist dskh
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin khách hàng!");
        }
        return dskh;
    }

    public bool Them(KhachHangDTO kh)
    {
        try
        {
            string query = "INSERT INTO KhachHang VALUES ('" + kh.MaKH
                + "','" + kh.HoTen + "','" + kh.CCCD + "','" + kh.SDT
                + "','" + kh.Email + "','" + kh.TaiKhoan + "','" + kh.MatKhau
                + "','" + kh.DiaChi + "')";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi ghi thông tin khách hàng!"); return false; }
    }

    public bool Sua(KhachHangDTO kh)
    {
        try
        {
            string query = "UPDATE KhachHang SET HoTen='" + kh.HoTen + "',CCCD='" + kh.CCCD
                + "',SDT='" + kh.SDT + "',Email='" + kh.Email + "',TaiKhoan='" + kh.TaiKhoan
                + "',MatKhau='" + kh.MatKhau + "',DiaChi='" + kh.DiaChi
                + "' WHERE MaKH='" + kh.MaKH + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi cập nhật khách hàng!"); return false; }
    }

    public bool Xoa(string ma)
    {
        try
        {
            string query = "DELETE FROM KhachHang WHERE MaKH='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }
        catch { MessageBox.Show("Lỗi xóa khách hàng!"); return false; }
    }
}
