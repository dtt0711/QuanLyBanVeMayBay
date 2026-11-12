using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class HanhKhachBUS
{
    private static List<HanhKhachDTO>? dshk;
    private KhachHangDAO khachHangDao = new KhachHangDAO();

    public List<HanhKhachDTO> DocDSHK()
    {
        HanhKhachDAO dao = new HanhKhachDAO();
        if (dshk == null || dshk.Count == 0)
        {
            dshk = new List<HanhKhachDTO>();
            dshk = dao.DocDSHK();
        }
        return dshk;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dshk = null;
    }

    public List<HanhKhachDTO> DocTheoKH(string maCha)
    {
        HanhKhachDAO dao = new HanhKhachDAO();
        return dao.DocTheoKH(maCha);
    }

    private void KiemTra(HanhKhachDTO hk)
    {
        if (string.IsNullOrWhiteSpace(hk.MaHK))
            throw new Exception("Mã HK không được để trống!");
        if (string.IsNullOrWhiteSpace(hk.HoTen))
            throw new Exception("Họ tên không được để trống!");
        if (string.IsNullOrWhiteSpace(hk.CCCD))
            throw new Exception("CCCD không được để trống!");
        if (string.IsNullOrWhiteSpace(hk.GioiTinh))
            throw new Exception("Giới tính không được để trống!");
        if (string.IsNullOrWhiteSpace(hk.MaKH))
            throw new Exception("Mã KH không được để trống!");
        if (string.IsNullOrWhiteSpace(hk.LoaiKH))
            throw new Exception("Loại KH không được để trống!");
        if (hk.NgaySinh > DateTime.Now)
            throw new Exception("Ngày sinh không hợp lệ!");
        if (!khachHangDao.DocDSKH().Any(t => t.MaKH == hk.MaKH))
            throw new Exception("Mã KH không tồn tại!");
    }

    public void Them(HanhKhachDTO hk)
    {
        HanhKhachDAO dao = new HanhKhachDAO();
        KiemTra(hk);
        if (dao.DocDSHK().Any(t => t.MaHK == hk.MaHK))
            throw new Exception("Mã HK đã tồn tại!");
        dao.Them(hk);
        LamMoiDS();
    }

    public void Sua(HanhKhachDTO hk)
    {
        HanhKhachDAO dao = new HanhKhachDAO();
        KiemTra(hk);
        if (!dao.DocDSHK().Any(t => t.MaHK == hk.MaHK))
            throw new Exception("Không tìm thấy Mã HK cần sửa!");
        dao.Sua(hk);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        HanhKhachDAO dao = new HanhKhachDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã HK cần xóa!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
