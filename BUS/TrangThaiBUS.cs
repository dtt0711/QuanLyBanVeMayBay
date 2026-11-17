namespace QuanLyBanVeMayBay.BUS;

// Các trạng thái cố định (khớp với ràng buộc CHECK trong CSDL), form chỉ cho chọn trong danh sách này
public static class TrangThaiBUS
{
    public const string ChuaKhoiHanh = "Chưa khởi hành";
    public const string DaKhoiHanh = "Đã khởi hành";
    public const string DaHaCanh = "Đã hạ cánh";
    public const string DaHuy = "Đã hủy";
    public const string ChoThanhToan = "Chờ thanh toán";
    public const string ChuaThanhToan = "Chưa thanh toán";
    public const string DaThanhToan = "Đã thanh toán";
    public const string HieuLuc = "Hiệu lực";

    public static readonly string[] ChuyenBay = { ChuaKhoiHanh, DaKhoiHanh, DaHaCanh, DaHuy };
    // Phiếu đặt chỉ chuyển sang "Đã thanh toán" khi lập hóa đơn đã thanh toán, không chọn tay
    public static readonly string[] DatVe = { ChoThanhToan, DaHuy };
    public static readonly string[] HoaDon = { ChuaThanhToan, DaThanhToan };
    public static readonly string[] PhuongThucTT = { "Tiền mặt", "Chuyển khoản", "Thẻ" };
}
