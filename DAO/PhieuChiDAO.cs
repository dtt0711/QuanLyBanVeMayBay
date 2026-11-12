using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

public class PhieuChiDAO
{
    public List<PhieuChiDTO> DocDSPC()
    {
        List<PhieuChiDTO> dspc = new List<PhieuChiDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaPC,NgayChi,MaCB,NoiDung,SoTien FROM PhieuChi";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                DateTime ngayChi = reader.GetDateTime("NgayChi");
                string maCB = reader.GetString("MaCB");
                string noiDung = reader.IsDBNull(reader.GetOrdinal("NoiDung")) ? "" : reader.GetString("NoiDung");
                decimal soTien = reader.GetDecimal("SoTien");
                PhieuChiDTO pc = new PhieuChiDTO(ma, ngayChi, maCB, noiDung, soTien);
                dspc.Add(pc);//add to arraylist dspc
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc thông tin phiếu chi!");
        }
        finally
        {
            connection.Close();
        }
        return dspc;
    }

    public List<PhieuChiDTO> DocTheoCB(string maCha)
    {
        List<PhieuChiDTO> dspc = new List<PhieuChiDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaPC,NgayChi,MaCB,NoiDung,SoTien FROM PhieuChi WHERE MaCB='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ma = reader.GetString(0);
                DateTime ngayChi = reader.GetDateTime("NgayChi");
                string maCB = reader.GetString("MaCB");
                string noiDung = reader.IsDBNull(reader.GetOrdinal("NoiDung")) ? "" : reader.GetString("NoiDung");
                decimal soTien = reader.GetDecimal("SoTien");
                PhieuChiDTO pc = new PhieuChiDTO(ma, ngayChi, maCB, noiDung, soTien);
                dspc.Add(pc);//add to arraylist dspc
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc phiếu chi theo mã liên quan!");
        }
        finally
        {
            connection.Close();
        }
        return dspc;
    }

    public void Them(PhieuChiDTO pc)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = pc.MaPC;
            string ngayChi = pc.NgayChi.ToString("yyyy-MM-dd");
            string maCB = pc.MaCB;
            string noiDung = pc.NoiDung;
            string soTien = pc.SoTien.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "INSERT INTO PhieuChi(MaPC,NgayChi,MaCB,NoiDung,SoTien) VALUES ('" + ma + "','" + ngayChi + "','" + maCB + "','" + noiDung + "'," + soTien + ")";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi ghi thông tin phiếu chi!");
        }
        finally
        {
            connection.Close();
        }
    }

    public void Sua(PhieuChiDTO pc)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string ma = pc.MaPC;
            string ngayChi = pc.NgayChi.ToString("yyyy-MM-dd");
            string maCB = pc.MaCB;
            string noiDung = pc.NoiDung;
            string soTien = pc.SoTien.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string query = "UPDATE PhieuChi SET NgayChi='" + ngayChi + "', MaCB='" + maCB + "', NoiDung='" + noiDung + "', SoTien=" + soTien + " WHERE MaPC='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi sửa thông tin phiếu chi!");
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
            string query = "DELETE FROM PhieuChi WHERE MaPC='" + ma + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            int r = command.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Lỗi xóa thông tin phiếu chi!");
        }
        finally
        {
            connection.Close();
        }
    }
}
