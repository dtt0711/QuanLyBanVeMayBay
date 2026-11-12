using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class CTKMBUS
{
    private static List<CTKMDTO>? dsctkm;
    private VoucherDAO voucherDao = new VoucherDAO();

    public List<CTKMDTO> DocDSCTKM()
    {
        CTKMDAO dao = new CTKMDAO();
        if (dsctkm == null || dsctkm.Count == 0)
        {
            dsctkm = new List<CTKMDTO>();
            dsctkm = dao.DocDSCTKM();
        }
        return dsctkm;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dsctkm = null;
    }

    private void KiemTra(CTKMDTO ctkm)
    {
        if (string.IsNullOrWhiteSpace(ctkm.MaCTKM))
            throw new Exception("Mã CTKM không được để trống!");
        if (string.IsNullOrWhiteSpace(ctkm.TenCTKM))
            throw new Exception("Tên CTKM không được để trống!");
        if (ctkm.NgayKetThuc < ctkm.NgayBatDau)
            throw new Exception("Ngày kết thúc phải sau ngày bắt đầu!");
    }

    public void Them(CTKMDTO ctkm)
    {
        CTKMDAO dao = new CTKMDAO();
        KiemTra(ctkm);
        if (dao.DocDSCTKM().Any(t => t.MaCTKM == ctkm.MaCTKM))
            throw new Exception("Mã CTKM đã tồn tại!");
        dao.Them(ctkm);
        LamMoiDS();
    }

    public void Sua(CTKMDTO ctkm)
    {
        CTKMDAO dao = new CTKMDAO();
        KiemTra(ctkm);
        if (!dao.DocDSCTKM().Any(t => t.MaCTKM == ctkm.MaCTKM))
            throw new Exception("Không tìm thấy Mã CTKM cần sửa!");
        dao.Sua(ctkm);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        CTKMDAO dao = new CTKMDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã CTKM cần xóa!");
        if (voucherDao.DocTheoCTKM(ma).Count > 0)
            throw new Exception("CTKM đang có voucher, hãy xóa voucher trước!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
