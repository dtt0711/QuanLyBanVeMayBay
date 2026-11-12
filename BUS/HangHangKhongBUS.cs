using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class HangHangKhongBUS
{
    private static List<HangHangKhongDTO>? dshhk;
    private MayBayDAO mayBayDao = new MayBayDAO();

    public List<HangHangKhongDTO> DocDSHHK()
    {
        HangHangKhongDAO dao = new HangHangKhongDAO();
        if (dshhk == null || dshhk.Count == 0)
        {
            dshhk = new List<HangHangKhongDTO>();
            dshhk = dao.DocDS();
        }
        return dshhk;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dshhk = null;
    }

    private void KiemTra(HangHangKhongDTO hhk)
    {
        if (string.IsNullOrWhiteSpace(hhk.MaHHK))
            throw new Exception("Mã hãng không được để trống!");
        if (string.IsNullOrWhiteSpace(hhk.TenHang))
            throw new Exception("Tên hãng không được để trống!");
    }

    public void Them(HangHangKhongDTO hhk)
    {
        HangHangKhongDAO dao = new HangHangKhongDAO();
        KiemTra(hhk);
        if (dao.DocDS().Any(t => t.MaHHK == hhk.MaHHK))
            throw new Exception("Mã hãng đã tồn tại!");
        dao.Them(hhk);
        LamMoiDS();
    }

    public void Sua(HangHangKhongDTO hhk)
    {
        HangHangKhongDAO dao = new HangHangKhongDAO();
        KiemTra(hhk);
        if (!dao.DocDS().Any(t => t.MaHHK == hhk.MaHHK))
            throw new Exception("Không tìm thấy Mã hãng cần sửa!");
        dao.Sua(hhk);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        HangHangKhongDAO dao = new HangHangKhongDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã hãng cần xóa!");
        if (mayBayDao.DocTheoHHK(ma).Count > 0)
            throw new Exception("Hãng đã có máy bay, không thể xóa!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
