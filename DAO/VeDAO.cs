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

    public List<VeDTO> DocDSVe()
    {
        List<VeDTO> dsve = new List<VeDTO>();
        try
        {
            string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV FROM Ve";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maChuyenBay = reader.GetString("MaChuyenBay");
                string maHV = reader.GetString("MaHV");
                string maGhe = reader.GetString("MaGhe");
                string maHK = reader.GetString("MaHK");
                decimal donGiaVe = reader.GetDecimal("DonGiaVe");
                DateTime ngay = reader.GetDateTime("Ngay");
                string maDV = reader.IsDBNull(reader.GetOrdinal("MaDV")) ? "" : reader.GetString("MaDV");
                VeDTO ve = new VeDTO(ma, maChuyenBay, maHV, maGhe, maHK, donGiaVe, ngay, maDV);
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
            string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV FROM Ve WHERE MaChuyenBay='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maChuyenBay = reader.GetString("MaChuyenBay");
                string maHV = reader.GetString("MaHV");
                string maGhe = reader.GetString("MaGhe");
                string maHK = reader.GetString("MaHK");
                decimal donGiaVe = reader.GetDecimal("DonGiaVe");
                DateTime ngay = reader.GetDateTime("Ngay");
                string maDV = reader.IsDBNull(reader.GetOrdinal("MaDV")) ? "" : reader.GetString("MaDV");
                VeDTO ve = new VeDTO(ma, maChuyenBay, maHV, maGhe, maHK, donGiaVe, ngay, maDV);
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
            string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV FROM Ve WHERE MaDV='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                string maChuyenBay = reader.GetString("MaChuyenBay");
                string maHV = reader.GetString("MaHV");
                string maGhe = reader.GetString("MaGhe");
                string maHK = reader.GetString("MaHK");
                decimal donGiaVe = reader.GetDecimal("DonGiaVe");
                DateTime ngay = reader.GetDateTime("Ngay");
                string maDV = reader.IsDBNull(reader.GetOrdinal("MaDV")) ? "" : reader.GetString("MaDV");
                VeDTO ve = new VeDTO(ma, maChuyenBay, maHV, maGhe, maHK, donGiaVe, ngay, maDV);
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
        string query = "SELECT MaVe,MaChuyenBay,MaHV,MaGhe,MaHK,DonGiaVe,Ngay,MaDV FROM Ve WHERE MaVe='" + ma + "'";
        MySqlCommand command = new MySqlCommand(query, connection, tran);
        using MySqlDataReader reader = command.ExecuteReader();
        VeDTO ve = null;
        if (reader.Read())
        {
            string maDV = reader.IsDBNull(reader.GetOrdinal("MaDV")) ? "" : reader.GetString("MaDV");
            ve = new VeDTO(reader.GetString(0), reader.GetString("MaChuyenBay"), reader.GetString("MaHV"),
                reader.GetString("MaGhe"), reader.GetString("MaHK"), reader.GetDecimal("DonGiaVe"), reader.GetDateTime("Ngay"), maDV);
        }
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
            string query = "INSERT INTO Ve VALUES ('" + ve.MaVe
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

    // Trả vé: xóa vé, giảm SoVeDaBan của chuyến bay, tính lại TongTien của phiếu đặt
    public bool Xoa(string ma)
    {
        MySqlTransaction tran = null;
        try
        {
            tran = connection.BeginTransaction();
            VeDTO cu = DocVe(tran, ma);
            string query = "DELETE FROM Ve WHERE MaVe='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection, tran);
            command.ExecuteNonQuery();
            if (cu != null)
            {
                TinhTienDAO.SoVeDaBan(connection, tran, cu.MaChuyenBay, -1);
                if (cu.MaDV != "")
                    TinhTienDAO.DatVe(connection, tran, cu.MaDV);
            }
            tran.Commit();
            return true;
        }
        catch
        {
            if (tran != null)
                tran.Rollback();
            MessageBox.Show("Lỗi xóa vé!");
            return false;
        }
    }
}
