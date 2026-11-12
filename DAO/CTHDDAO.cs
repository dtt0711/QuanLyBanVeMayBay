using MySql.Data.MySqlClient;
using BanVeMayBay.DTO;

namespace BanVeMayBay.DAO;

// Thêm / xóa chi tiết hóa đơn đồng thời cập nhật ghế trống của chuyến bay và tổng tiền hóa đơn (trong 1 transaction)
public class CTHDDAO
{
    public List<CTHDDTO> DocTheoHD(string maCha)
    {
        List<CTHDDTO> dscthd = new List<CTHDDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaHD,MaCB,SoLuong,DonGia,ThanhTien FROM CTHD WHERE MaHD='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string maHD = reader.GetString(0);
                string maCB = reader.GetString("MaCB");
                int soLuong = reader.GetInt32("SoLuong");
                decimal donGia = reader.GetDecimal("DonGia");
                decimal thanhTien = reader.GetDecimal("ThanhTien");
                CTHDDTO cthd = new CTHDDTO(maHD, maCB, soLuong, donGia, thanhTien);
                dscthd.Add(cthd);//add to arraylist dscthd
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc chi tiết hóa đơn!");
        }
        finally
        {
            connection.Close();
        }
        return dscthd;
    }

    public List<CTHDDTO> DocTheoCB(string maCha)
    {
        List<CTHDDTO> dscthd = new List<CTHDDTO>();
        MySqlConnection connection = MyConnection.GetConnection();
        try
        {
            connection.Open();
            string query = "SELECT MaHD,MaCB,SoLuong,DonGia,ThanhTien FROM CTHD WHERE MaCB='" + maCha + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string maHD = reader.GetString(0);
                string maCB = reader.GetString("MaCB");
                int soLuong = reader.GetInt32("SoLuong");
                decimal donGia = reader.GetDecimal("DonGia");
                decimal thanhTien = reader.GetDecimal("ThanhTien");
                CTHDDTO cthd = new CTHDDTO(maHD, maCB, soLuong, donGia, thanhTien);
                dscthd.Add(cthd);//add to arraylist dscthd
            }
            reader.Close();
        }
        catch
        {
            throw new Exception("Lỗi đọc chi tiết hóa đơn!");
        }
        finally
        {
            connection.Close();
        }
        return dscthd;
    }

    private void CapNhatTongTien(MySqlConnection connection, MySqlTransaction tran, string maHD)
    {
        string query = "UPDATE HoaDon SET TongTien = (SELECT COALESCE(SUM(ThanhTien),0) FROM CTHD WHERE MaHD='" + maHD + "') WHERE MaHD='" + maHD + "'";
        MySqlCommand command = new MySqlCommand(query, connection, tran);
        command.ExecuteNonQuery();
    }

    public void Them(CTHDDTO cthd)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        MySqlTransaction? tran = null;
        try
        {
            connection.Open();
            string maHD = cthd.MaHD;
            string maCB = cthd.MaCB;
            int soLuong = cthd.SoLuong;
            string donGia = cthd.DonGia.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string thanhTien = cthd.ThanhTien.ToString(System.Globalization.CultureInfo.InvariantCulture);
            tran = connection.BeginTransaction();

            string q1 = "UPDATE ChuyenBay SET SoGheTrong = SoGheTrong - " + soLuong + " WHERE MaCB='" + maCB + "' AND SoGheTrong >= " + soLuong;
            MySqlCommand c1 = new MySqlCommand(q1, connection, tran);
            if (c1.ExecuteNonQuery() == 0)
                throw new Exception("Không đủ ghế trống!");

            string q2 = "INSERT INTO CTHD(MaHD,MaCB,SoLuong,DonGia,ThanhTien) VALUES ('" + maHD + "','" + maCB + "'," + soLuong + "," + donGia + "," + thanhTien + ")";
            MySqlCommand c2 = new MySqlCommand(q2, connection, tran);
            c2.ExecuteNonQuery();

            CapNhatTongTien(connection, tran, maHD);
            tran.Commit();
        }
        catch (MySqlException)
        {
            tran?.Rollback();
            throw new Exception("Lỗi ghi chi tiết hóa đơn!");
        }
        catch
        {
            tran?.Rollback();
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public void Xoa(string maHD, string maCB)
    {
        MySqlConnection connection = MyConnection.GetConnection();
        MySqlTransaction? tran = null;
        try
        {
            connection.Open();
            tran = connection.BeginTransaction();

            string q0 = "SELECT SoLuong FROM CTHD WHERE MaHD='" + maHD + "' AND MaCB='" + maCB + "'";
            MySqlCommand c0 = new MySqlCommand(q0, connection, tran);
            object? sl = c0.ExecuteScalar();
            if (sl == null)
                throw new Exception("Không tìm thấy chi tiết hóa đơn!");

            string q1 = "DELETE FROM CTHD WHERE MaHD='" + maHD + "' AND MaCB='" + maCB + "'";
            MySqlCommand c1 = new MySqlCommand(q1, connection, tran);
            c1.ExecuteNonQuery();

            // hủy vé: trả ghế về chuyến bay
            string q2 = "UPDATE ChuyenBay SET SoGheTrong = SoGheTrong + " + Convert.ToInt32(sl) + " WHERE MaCB='" + maCB + "'";
            MySqlCommand c2 = new MySqlCommand(q2, connection, tran);
            c2.ExecuteNonQuery();

            CapNhatTongTien(connection, tran, maHD);
            tran.Commit();
        }
        catch (MySqlException)
        {
            tran?.Rollback();
            throw new Exception("Lỗi xóa chi tiết hóa đơn!");
        }
        catch
        {
            tran?.Rollback();
            throw;
        }
        finally
        {
            connection.Close();
        }
    }
}
