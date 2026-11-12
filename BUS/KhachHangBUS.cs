using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class KhachHangBUS
{
    private static List<KhachHangDTO>? dskh;
    private HanhKhachDAO hanhKhachDao = new HanhKhachDAO();
    private HoaDonDAO hoaDonDao = new HoaDonDAO();

    public List<KhachHangDTO> DocDSKH()
    {
        KhachHangDAO dao = new KhachHangDAO();
        if (dskh == null || dskh.Count == 0)
        {
            dskh = new List<KhachHangDTO>();
            dskh = dao.DocDSKH();
        }
        return dskh;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dskh = null;
    }

    private void KiemTra(KhachHangDTO kh)
    {
        if (string.IsNullOrWhiteSpace(kh.MaKH))
            throw new Exception("Mã KH không được để trống!");
        if (string.IsNullOrWhiteSpace(kh.HoTen))
            throw new Exception("Họ tên không được để trống!");
        if (string.IsNullOrWhiteSpace(kh.CCCD))
            throw new Exception("CCCD không được để trống!");
        if (string.IsNullOrWhiteSpace(kh.SDT))
            throw new Exception("SĐT không được để trống!");
        if (string.IsNullOrWhiteSpace(kh.Email))
            throw new Exception("Email không được để trống!");
        if (string.IsNullOrWhiteSpace(kh.TaiKhoan))
            throw new Exception("Tài khoản không được để trống!");
        if (string.IsNullOrWhiteSpace(kh.MatKhau))
            throw new Exception("Mật khẩu không được để trống!");
        if (string.IsNullOrWhiteSpace(kh.DiaChi))
            throw new Exception("Địa chỉ không được để trống!");
    }

    public void Them(KhachHangDTO kh)
    {
        KhachHangDAO dao = new KhachHangDAO();
        KiemTra(kh);
        if (dao.DocDSKH().Any(t => t.MaKH == kh.MaKH))
            throw new Exception("Mã KH đã tồn tại!");
        dao.Them(kh);
        LamMoiDS();
    }

    public void Sua(KhachHangDTO kh)
    {
        KhachHangDAO dao = new KhachHangDAO();
        KiemTra(kh);
        if (!dao.DocDSKH().Any(t => t.MaKH == kh.MaKH))
            throw new Exception("Không tìm thấy Mã KH cần sửa!");
        dao.Sua(kh);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        KhachHangDAO dao = new KhachHangDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã KH cần xóa!");
        if (hanhKhachDao.DocTheoKH(ma).Count > 0)
            throw new Exception("Khách hàng đang có hành khách, hãy xóa hành khách trước!");
        if (hoaDonDao.DocTheoKH(ma).Count > 0)
            throw new Exception("Khách hàng đã có hóa đơn, không thể xóa!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
