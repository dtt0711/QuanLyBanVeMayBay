using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class HoaDonDAO
{
    public List<HoaDonDTO> DocDSHD()
    {
        List<HoaDonDTO> dshd = new List<HoaDonDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaHD,NgayLap,MaNV,MaKH,TongTien FROM HoaDon";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                DateTime ngayLap = reader.GetDateTime("NgayLap");
                string maNV = reader.GetString("MaNV");
                string maKH = reader.GetString("MaKH");
                decimal tongTien = reader.GetDecimal("TongTien");
                HoaDonDTO hd = new HoaDonDTO(ma, ngayLap, maNV, maKH, tongTien);
                dshd.Add(hd);//add to arraylist dshd
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc thông tin hóa đơn!");
        }
        finally
        {
            connection.Close();
        }
        return dshd;
    }

    public List<HoaDonDTO> DocTheoNV(string maCha)
    {
        List<HoaDonDTO> dshd = new List<HoaDonDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaHD,NgayLap,MaNV,MaKH,TongTien FROM HoaDon WHERE MaNV='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                DateTime ngayLap = reader.GetDateTime("NgayLap");
                string maNV = reader.GetString("MaNV");
                string maKH = reader.GetString("MaKH");
                decimal tongTien = reader.GetDecimal("TongTien");
                HoaDonDTO hd = new HoaDonDTO(ma, ngayLap, maNV, maKH, tongTien);
                dshd.Add(hd);//add to arraylist dshd
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc hóa đơn theo mã liên quan!");
        }
        finally
        {
            connection.Close();
        }
        return dshd;
    }

    public List<HoaDonDTO> DocTheoKH(string maCha)
    {
        List<HoaDonDTO> dshd = new List<HoaDonDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaHD,NgayLap,MaNV,MaKH,TongTien FROM HoaDon WHERE MaKH='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                DateTime ngayLap = reader.GetDateTime("NgayLap");
                string maNV = reader.GetString("MaNV");
                string maKH = reader.GetString("MaKH");
                decimal tongTien = reader.GetDecimal("TongTien");
                HoaDonDTO hd = new HoaDonDTO(ma, ngayLap, maNV, maKH, tongTien);
                dshd.Add(hd);//add to arraylist dshd
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc hóa đơn theo mã liên quan!");
        }
        finally
        {
            connection.Close();
        }
        return dshd;
    }

    public void Them(HoaDonDTO hd)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = hd.MaHD;
            string ngayLap = hd.NgayLap.ToString("yyyy-MM-dd");
            string maNV = hd.MaNV;
            string maKH = hd.MaKH;
            string query = "INSERT INTO HoaDon(MaHD,NgayLap,MaNV,MaKH,TongTien) VALUES ('" + ma + "','" + ngayLap + "','" + maNV + "','" + maKH + "',0)";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin hóa đơn!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(HoaDonDTO hd)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = hd.MaHD;
            string ngayLap = hd.NgayLap.ToString("yyyy-MM-dd");
            string maNV = hd.MaNV;
            string maKH = hd.MaKH;
            string query = "UPDATE HoaDon SET NgayLap='" + ngayLap + "', MaNV='" + maNV + "', MaKH='" + maKH + "' WHERE MaHD='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin hóa đơn!");
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
            string query = "DELETE FROM HoaDon WHERE MaHD='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin hóa đơn!");
        }
        finally
        {
            connection.Close();
        }
    }
}
