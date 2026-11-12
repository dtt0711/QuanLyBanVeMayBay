using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class VoucherBUS
{
    private static List<VoucherDTO>? dsvc;
    private CTKMDAO ctkmDao = new CTKMDAO();

    public List<VoucherDTO> DocDSVC()
    {
        VoucherDAO dao = new VoucherDAO();
        if (dsvc == null || dsvc.Count == 0)
        {
            dsvc = new List<VoucherDTO>();
            dsvc = dao.DocDSVC();
        }
        return dsvc;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dsvc = null;
    }

    public List<VoucherDTO> DocTheoCTKM(string maCha)
    {
        VoucherDAO dao = new VoucherDAO();
        return dao.DocTheoCTKM(maCha);
    }

    private void KiemTra(VoucherDTO vc)
    {
        if (string.IsNullOrWhiteSpace(vc.MaVoucher))
            throw new Exception("Mã voucher không được để trống!");
        if (string.IsNullOrWhiteSpace(vc.MaCTKM))
            throw new Exception("Mã CTKM không được để trống!");
        if (vc.GiamGia <= 0)
            throw new Exception("Giảm giá phải lớn hơn 0!");
        if (!ctkmDao.DocDSCTKM().Any(t => t.MaCTKM == vc.MaCTKM))
            throw new Exception("Mã CTKM không tồn tại!");
    }

    public void Them(VoucherDTO vc)
    {
        VoucherDAO dao = new VoucherDAO();
        KiemTra(vc);
        if (dao.DocDSVC().Any(t => t.MaVoucher == vc.MaVoucher))
            throw new Exception("Mã voucher đã tồn tại!");
        dao.Them(vc);
        LamMoiDS();
    }

    public void Sua(VoucherDTO vc)
    {
        VoucherDAO dao = new VoucherDAO();
        KiemTra(vc);
        if (!dao.DocDSVC().Any(t => t.MaVoucher == vc.MaVoucher))
            throw new Exception("Không tìm thấy Mã voucher cần sửa!");
        dao.Sua(vc);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        VoucherDAO dao = new VoucherDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã voucher cần xóa!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
