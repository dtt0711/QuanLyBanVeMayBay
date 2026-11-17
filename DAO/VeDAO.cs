using MySql.Data.MySqlClient;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.DAO;

public class VeDAO
{
    MySqlConnection connection = MyConnection.GetConnection();

    public VeDAO()
    {
        MyConnection.Mo(connection);
    }

    private VeDTO DocDong(MySqlDataReader reader)
    {
        string maDV = reader.IsDBNull(reader.GetOrdinal("MaDV")) ? "" : reader.GetString("MaDV");
        VeDTO ve = new VeDTO(reader.GetString(0), reader.GetString("MaChuyenBay"), reader.GetString("MaHV"), reader.GetString("MaGhe"),
            reader.GetString("MaHK"), reader.GetDecimal("DonGiaVe"), reader.GetDateTime("Ngay"), maDV);
        ve.TrangThai = reader.GetString("TrangThai");
        return ve;
    }

    public List<VeDTO> DocDSVe()
    {
        List<VeDTO> dsve = new List<VeDTO>();
        try
        {
            string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV,TrangThai FROM Ve";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VeDTO ve = DocDong(reader);
                dsve.Add(ve);//add to arraylist dsve
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc thông tin vé!");
        }
        return dsve;
    }

    public List<VeDTO> DocTheoChuyenBay(string maCha)
    {
        List<VeDTO> dsve = new List<VeDTO>();
        try
        {
            string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV,TrangThai FROM Ve WHERE MaChuyenBay='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VeDTO ve = DocDong(reader);
                dsve.Add(ve);//add to arraylist dsve
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc vé theo chuyến bay!");
        }
        return dsve;
    }

    public List<VeDTO> DocTheoDatVe(string maCha)
    {
        List<VeDTO> dsve = new List<VeDTO>();
        try
        {
            string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV,TrangThai FROM Ve WHERE MaDV='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                VeDTO ve = DocDong(reader);
                dsve.Add(ve);//add to arraylist dsve
            }
            reader.Close();
        }
        catch
        {
            MessageBox.Show("Lỗi đọc vé theo đặt vé!");
        }
        return dsve;
    }

    // Đọc chuyến bay và phiếu đặt hiện tại của vé (dùng khi sửa, xóa vé)
    private VeDTO DocVe(MySqlTransaction tran, string ma)
    {
        string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV,TrangThai FROM Ve WHERE MaVe='" + ma + "'";
        MySqlCommand command = new MySqlCommand(query, connection, tran);
        using MySqlDataReader reader = command.ExecuteReader();
        VeDTO ve = null;
        if (reader.Read())
            ve = DocDong(reader);
        reader.Close();
        return ve;
    }

    // Bán vé: thêm vé, tăng SoVeDaBan của chuyến bay, tính lại TongTien của phiếu đặt
    public bool Them(VeDTO ve)
    {
        MySqlTransaction tran = null;
        try
        {
            string ngay = ve.Ngay.ToString("yyyy-MM-dd HH:mm:ss");
            string donGiaVe = ve.DonGiaVe.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string maDV = string.IsNullOrEmpty(ve.MaDV) ? "NULL" : "'" + ve.MaDV + "'";
            tran = connection.BeginTransaction();
            string query = "INSERT INTO Ve(MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV) VALUES ('" + ve.MaVe
                + "','" + ve.MaChuyenBay + "','" + ve.MaHV + "','" + ve.MaGhe + "','" + ve.MaHK
                + "'," + donGiaVe + ",'" + ngay + "'," + maDV + ")";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            command.ExecuteNonQuery();
            TinhTienDAO.SoVeDaBan(connection, tran, ve.MaChuyenBay, 1);
            if (ve.MaDV != "")
                TinhTienDAO.DatVe(connection, tran, ve.MaDV);
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi ghi thông tin vé!");
            return false;
        }
    }

    // Sửa vé: nếu đổi chuyến bay thì trả 1 vé cho chuyến cũ, bán 1 vé của chuyến mới
    public bool Sua(VeDTO ve)
    {
        MySqlTransaction tran = null;
        try
        {
            string ngay = ve.Ngay.ToString("yyyy-MM-dd HH:mm:ss");
            string donGiaVe = ve.DonGiaVe.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string maDV = string.IsNullOrEmpty(ve.MaDV) ? "NULL" : "'" + ve.MaDV + "'";
            tran = connection.BeginTransaction();
            VeDTO cu = DocVe(tran, ve.MaVe);
            string query = "UPDATE Ve SET MaChuyenBay='" + ve.MaChuyenBay + "',MaHV='" + ve.MaHV
                + "',MaGhe='" + ve.MaGhe + "',MaHK='" + ve.MaHK + "',DonGiaVe=" + donGiaVe
                + ",Ngay='" + ngay + "',MaDV=" + maDV
                + " WHERE MaVe='" + ve.MaVe + "'";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            command.ExecuteNonQuery();
            if (cu != null && cu.MaChuyenBay != ve.MaChuyenBay)
            {
                TinhTienDAO.SoVeDaBan(connection, tran, cu.MaChuyenBay, -1);
                TinhTienDAO.SoVeDaBan(connection, tran, ve.MaChuyenBay, 1);
            }
            if (cu != null && cu.MaDV != "" && cu.MaDV != ve.MaDV)
                TinhTienDAO.DatVe(connection, tran, cu.MaDV);
            if (ve.MaDV != "")
                TinhTienDAO.DatVe(connection, tran, ve.MaDV);
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi cập nhật vé!");
            return false;
        }
    }

    // Hủy (trả) vé: không xóa mà đổi trạng thái sang "Đã hủy" để còn lịch sử. Ghế và hành khách được nhả ra
    // (ràng buộc không trùng chỉ tính vé còn hiệu lực), giảm SoVeDaBan, tính lại tiền phiếu đặt,
    // và bỏ vé khỏi hóa đơn chưa thanh toán nếu có.
    public bool Huy(string ma)
    {
        MySqlTransaction tran = null;
        try
        {
            tran = connection.BeginTransaction();
            VeDTO cu = DocVe(tran, ma);
            string query = "UPDATE Ve SET TrangThai='Đã hủy' WHERE MaVe='" + ma + "' AND TrangThai<>'Đã hủy'";
            if (new MySqlCommand(query, connection, tran).ExecuteNonQuery() == 1 && cu != null)
            {
                TinhTienDAO.SoVeDaBan(connection, tran, cu.MaChuyenBay, -1);
                TinhTienDAO.DatVe(connection, tran, cu.MaDV);
                query = "SELECT c.MaHD FROM ChiTietHoaDon c JOIN HoaDon h ON h.MaHD=c.MaHD WHERE c.MaVe='" + ma + "'";
                object maHD = new MySqlCommand(query, connection, tran).ExecuteScalar();
                if (maHD != null)
                {
                    new MySqlCommand("DELETE FROM ChiTietHoaDon WHERE MaVe='" + ma + "'", connection, tran).ExecuteNonQuery();
                    TinhTienDAO.HoaDon(connection, tran, maHD.ToString());
                }
            }
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi hủy vé!");
            return false;
        }
    }
}
