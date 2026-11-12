using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class MayBayBUS
{
    private static List<MayBayDTO>? dsmb;
    private HangHangKhongDAO hangHangKhongDao = new HangHangKhongDAO();
    private ChuyenBayDAO chuyenBayDao = new ChuyenBayDAO();

    public List<MayBayDTO> DocDSMB()
    {
        MayBayDAO dao = new MayBayDAO();
        if (dsmb == null || dsmb.Count == 0)
        {
            dsmb = new List<MayBayDTO>();
            dsmb = dao.DocDSMB();
        }
        return dsmb;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dsmb = null;
    }

    public List<MayBayDTO> DocTheoHHK(string maCha)
    {
        MayBayDAO dao = new MayBayDAO();
        return dao.DocTheoHHK(maCha);
    }

    private void KiemTra(MayBayDTO mb)
    {
        if (string.IsNullOrWhiteSpace(mb.MaMayBay))
            throw new Exception("Mã máy bay không được để trống!");
        if (string.IsNullOrWhiteSpace(mb.MaHHK))
            throw new Exception("Hãng HK không được để trống!");
        if (mb.SoGhe <= 0)
            throw new Exception("Số ghế phải lớn hơn 0!");
        if (!hangHangKhongDao.DocDS().Any(t => t.MaHHK == mb.MaHHK))
            throw new Exception("Hãng HK không tồn tại!");
    }

    public void Them(MayBayDTO mb)
    {
        MayBayDAO dao = new MayBayDAO();
        KiemTra(mb);
        if (dao.DocDSMB().Any(t => t.MaMayBay == mb.MaMayBay))
            throw new Exception("Mã máy bay đã tồn tại!");
        dao.Them(mb);
        LamMoiDS();
    }

    public void Sua(MayBayDTO mb)
    {
        MayBayDAO dao = new MayBayDAO();
        KiemTra(mb);
        if (!dao.DocDSMB().Any(t => t.MaMayBay == mb.MaMayBay))
            throw new Exception("Không tìm thấy Mã máy bay cần sửa!");
        dao.Sua(mb);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        MayBayDAO dao = new MayBayDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã máy bay cần xóa!");
        if (chuyenBayDao.DocTheoMayBay(ma).Count > 0)
            throw new Exception("Máy bay đã có chuyến bay, không thể xóa!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
