using MySql.Data.MySqlClient;

namespace QuanLyBanVeMayBay.DAO;

// Các câu lệnh tính lại những field tiền không cho người dùng nhập
public static class TinhTienDAO
{
    // Trạng thái phiếu đặt đi theo hóa đơn: có hóa đơn đã thanh toán -> "Đã thanh toán",
    // ngược lại "Chờ thanh toán" (phiếu đã hủy giữ nguyên)
    public static void TrangThaiDatVe(MySqlConnection connection, MySqlTransaction tran, string maDV)
    {
        string query = "UPDATE DatVe d SET TrangThai=CASE WHEN d.TrangThai='Đã hủy' THEN 'Đã hủy' "
            + "WHEN EXISTS(SELECT 1 FROM HoaDon h WHERE h.MaDV=d.MaDV AND h.TrangThai='Đã thanh toán') THEN 'Đã thanh toán' "
            + "ELSE 'Chờ thanh toán' END WHERE d.MaDV='" + maDV + "'";
        new MySqlCommand(query, connection, tran).ExecuteNonQuery();
    }

    // DatVe.TongTien = tổng đơn giá các vé của phiếu đặt - giảm giá (không âm)
    public static void DatVe(MySqlConnection connection, MySqlTransaction tran, string maDV)
    {
        string query = "UPDATE DatVe SET TongTien=GREATEST((SELECT COALESCE(SUM(DonGiaVe),0) FROM Ve WHERE MaDV='" + maDV
            + "' AND TrangThai<>'Đã hủy')-GiamGia,0) WHERE MaDV='" + maDV + "'";
        new MySqlCommand(query, connection, tran).ExecuteNonQuery();
    }

    // HoaDon.TongTien = tổng thành tiền chi tiết hóa đơn - giảm giá của phiếu đặt vé (không âm)
    public static void HoaDon(MySqlConnection connection, MySqlTransaction tran, string maHD)
    {
        string query = "UPDATE HoaDon h SET TongTien=GREATEST("
            + "(SELECT COALESCE(SUM(c.ThanhTien),0) FROM ChiTietHoaDon c WHERE c.MaHD=h.MaHD)"
            + "-(SELECT d.GiamGia FROM DatVe d WHERE d.MaDV=h.MaDV),0) WHERE h.MaHD='" + maHD + "'";
        new MySqlCommand(query, connection, tran).ExecuteNonQuery();
    }

    // Khi giảm giá của phiếu đặt vé đổi thì các hóa đơn của phiếu đó cũng đổi tổng tiền
    public static void HoaDonTheoDatVe(MySqlConnection connection, MySqlTransaction tran, string maDV)
    {
        string query = "UPDATE HoaDon h SET TongTien=GREATEST("
            + "(SELECT COALESCE(SUM(c.ThanhTien),0) FROM ChiTietHoaDon c WHERE c.MaHD=h.MaHD)"
            + "-(SELECT d.GiamGia FROM DatVe d WHERE d.MaDV=h.MaDV),0) WHERE h.MaDV='" + maDV + "'";
        new MySqlCommand(query, connection, tran).ExecuteNonQuery();
    }

    // Tổng số ghế của chuyến bay = số ghế của máy bay thực hiện chuyến (tính lại cho mọi chuyến)
    public static void TongSoGhe(MySqlConnection connection, MySqlTransaction tran)
    {
        string query = "UPDATE ChuyenBay c SET TongSoGhe=(SELECT COUNT(*) FROM Ghe g WHERE g.MaMayBay=c.MaMayBay)";
        new MySqlCommand(query, connection, tran).ExecuteNonQuery();
    }

    // Bán vé: tăng số vé đã bán của chuyến bay; trả vé: giảm (soVe = +1 hoặc -1)
    public static void SoVeDaBan(MySqlConnection connection, MySqlTransaction tran, string maChuyenBay, int soVe)
    {
        string query = "UPDATE ChuyenBay SET SoVeDaBan=SoVeDaBan+(" + soVe + ") WHERE MaChuyenBay='" + maChuyenBay + "'";
        new MySqlCommand(query, connection, tran).ExecuteNonQuery();
    }
}
