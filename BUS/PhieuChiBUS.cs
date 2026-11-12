using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class PhieuChiBUS
{
    private static List<PhieuChiDTO>? dspc;
    private ChuyenBayDAO chuyenBayDao = new ChuyenBayDAO();

    public List<PhieuChiDTO> DocDSPC()
    {
        PhieuChiDAO dao = new PhieuChiDAO();
        if (dspc == null || dspc.Count == 0)
        {
            dspc = new List<PhieuChiDTO>();
            dspc = dao.DocDSPC();
        }
        return dspc;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dspc = null;
    }

    public List<PhieuChiDTO> DocTheoCB(string maCha)
    {
        PhieuChiDAO dao = new PhieuChiDAO();
        return dao.DocTheoCB(maCha);
    }

    private void KiemTra(PhieuChiDTO pc)
    {
        if (string.IsNullOrWhiteSpace(pc.MaPC))
            throw new Exception("Mã PC không được để trống!");
        if (string.IsNullOrWhiteSpace(pc.MaCB))
            throw new Exception("Chuyến bay không được để trống!");
        if (pc.SoTien <= 0)
            throw new Exception("Số tiền phải lớn hơn 0!");
        if (!chuyenBayDao.DocDSCB().Any(t => t.MaCB == pc.MaCB))
            throw new Exception("Chuyến bay không tồn tại!");
    }

    public void Them(PhieuChiDTO pc)
    {
        PhieuChiDAO dao = new PhieuChiDAO();
        KiemTra(pc);
        if (dao.DocDSPC().Any(t => t.MaPC == pc.MaPC))
            throw new Exception("Mã PC đã tồn tại!");
        dao.Them(pc);
        LamMoiDS();
    }

    public void Sua(PhieuChiDTO pc)
    {
        PhieuChiDAO dao = new PhieuChiDAO();
        KiemTra(pc);
        if (!dao.DocDSPC().Any(t => t.MaPC == pc.MaPC))
            throw new Exception("Không tìm thấy Mã PC cần sửa!");
        dao.Sua(pc);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        PhieuChiDAO dao = new PhieuChiDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã PC cần xóa!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
