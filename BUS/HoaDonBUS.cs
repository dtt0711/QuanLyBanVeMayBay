using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class HoaDonBUS
{
    private static List<HoaDonDTO>? dshd;
    private NhanVienDAO nhanVienDao = new NhanVienDAO();
    private KhachHangDAO khachHangDao = new KhachHangDAO();
    private CTHDDAO cthdDao = new CTHDDAO();

    public List<HoaDonDTO> DocDSHD()
    {
        HoaDonDAO dao = new HoaDonDAO();
        if (dshd == null || dshd.Count == 0)
        {
            dshd = new List<HoaDonDTO>();
            dshd = dao.DocDSHD();
        }
        return dshd;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dshd = null;
    }

    public List<HoaDonDTO> DocTheoNV(string maCha)
    {
        HoaDonDAO dao = new HoaDonDAO();
        return dao.DocTheoNV(maCha);
    }

    public List<HoaDonDTO> DocTheoKH(string maCha)
    {
        HoaDonDAO dao = new HoaDonDAO();
        return dao.DocTheoKH(maCha);
    }

    private void KiemTra(HoaDonDTO hd)
    {
        if (string.IsNullOrWhiteSpace(hd.MaHD))
            throw new Exception("Mã HĐ không được để trống!");
        if (string.IsNullOrWhiteSpace(hd.MaNV))
            throw new Exception("Nhân viên không được để trống!");
        if (string.IsNullOrWhiteSpace(hd.MaKH))
            throw new Exception("Khách hàng không được để trống!");
        if (!nhanVienDao.DocDSNV().Any(t => t.MaNV == hd.MaNV))
            throw new Exception("Nhân viên không tồn tại!");
        if (!khachHangDao.DocDSKH().Any(t => t.MaKH == hd.MaKH))
            throw new Exception("Khách hàng không tồn tại!");
    }

    public void Them(HoaDonDTO hd)
    {
        HoaDonDAO dao = new HoaDonDAO();
        KiemTra(hd);
        if (dao.DocDSHD().Any(t => t.MaHD == hd.MaHD))
            throw new Exception("Mã HĐ đã tồn tại!");
        dao.Them(hd);
        LamMoiDS();
    }

    public void Sua(HoaDonDTO hd)
    {
        HoaDonDAO dao = new HoaDonDAO();
        KiemTra(hd);
        if (!dao.DocDSHD().Any(t => t.MaHD == hd.MaHD))
            throw new Exception("Không tìm thấy Mã HĐ cần sửa!");
        dao.Sua(hd);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        HoaDonDAO dao = new HoaDonDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã HĐ cần xóa!");
        if (cthdDao.DocTheoHD(ma).Count > 0)
            throw new Exception("Hóa đơn còn chi tiết, hãy xóa chi tiết trước!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
