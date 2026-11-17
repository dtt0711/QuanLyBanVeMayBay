using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class CTHDDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public CTHDDAO()
    {
        MyConnection.Mo(connection);
    }

    private CTHDDTO DocDong(MySqlDataReader reader)
    {
        string maHD = reader.GetString(0);
        string maVe = reader.GetString("MaVe");
        decimal donGiaVe = reader.GetDecimal("DonGiaVe");
        int sl = reader.GetInt32("SL");
        decimal thanhTien = reader.GetDecimal("ThanhTien");
        return new CTHDDTO(maHD, maVe, donGiaVe, sl, thanhTien);
    }

    public List<CTHDDTO> DocDSCTHD()
    {
        List<CTHDDTO> dscthd = new List<CTHDDTO>();
        try
        {
            string query = "SELECT MaHD,MaVe,DonGiaVe,SL,ThanhTien FROM ChiTietHoaDon";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                dscthd.Add(DocDong(reader));//add to arraylist dscthd
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc chi tiết hóa đơn!");
        }
        return dscthd;
    }

    public List<CTHDDTO> DocTheoHD(string maCha)
    {
        List<CTHDDTO> dscthd = new List<CTHDDTO>();
        try
        {
            string query = "SELECT MaHD,MaVe,DonGiaVe,SL,ThanhTien FROM ChiTietHoaDon WHERE MaHD='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CTHDDTO cthd = DocDong(reader);
                dscthd.Add(cthd);//add to arraylist dscthd
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc chi tiết hóa đơn theo hóa đơn!");
        }
        return dscthd;
    }

    public List<CTHDDTO> DocTheoCB(string maCha)
    {
        List<CTHDDTO> dscthd = new List<CTHDDTO>();
        try
        {
            string query = "SELECT c.MaHD,c.MaVe,c.DonGiaVe,c.SL,c.ThanhTien FROM ChiTietHoaDon c INNER JOIN Ve v ON c.MaVe=v.MaVe WHERE v.MaChuyenBay='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CTHDDTO cthd = DocDong(reader);
                dscthd.Add(cthd);//add to arraylist dscthd
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc chi tiết hóa đơn theo chuyến bay!");
        }
        return dscthd;
    }

    // Vé đã được tính tiền ở hóa đơn nào chưa: trả về mã hóa đơn, "" nếu chưa
    public string DaLapHoaDon(string maVe)
    {
        string maHD = "";
        try
        {
            string query = "SELECT MaHD FROM ChiTietHoaDon WHERE MaVe='" + maVe + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                maHD = reader.GetString(0);
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc chi tiết hóa đơn theo vé!");
        }
        return maHD;
    }

    private void CapNhatTongTien(MySqlTransaction tran, string maHD)
    {
        TinhTienDAO.HoaDon(connection, tran, maHD);
    }

    public bool Them(CTHDDTO cthd)
    {
        MySqlTransaction tran = null;
        try
        {
            string donGiaVe = cthd.DonGiaVe.ToString(System.Globalization.CultureInfo.InvariantCulture);
            tran = connection.BeginTransaction();
            string query = "INSERT INTO ChiTietHoaDon(MaHD,MaVe,DonGiaVe,SL) VALUES ('" + cthd.MaHD
                + "','" + cthd.MaVe + "'," + donGiaVe + "," + cthd.SL + ")";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            command.ExecuteNonQuery();
            CapNhatTongTien(tran, cthd.MaHD);
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi ghi chi tiết hóa đơn!");
            return false;
        }
    }

    public bool Xoa(string maHD, string maVe)
    {
        MySqlTransaction tran = null;
        try
        {
            tran = connection.BeginTransaction();
            string query = "DELETE FROM ChiTietHoaDon WHERE MaHD='" + maHD + "' AND MaVe='" + maVe + "'";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            if (command.ExecuteNonQuery() == 0)
            {
                tran.Rollback();
                MessageBox.Show("Không tìm thấy chi tiết hóa đơn!");
                return false;
            }
            CapNhatTongTien(tran, maHD);
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi xóa chi tiết hóa đơn!");
            return false;
        }
    }
}
