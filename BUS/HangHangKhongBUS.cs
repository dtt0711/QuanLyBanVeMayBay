using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class HangHangKhongBUS
{
    private static List<HangHangKhongDTO> dshhk;

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

    public static void LamMoiDS()
    {
        dshhk = null;
    }

    public bool Them(HangHangKhongDTO hhk)
    {
        DocDSHHK();
        HangHangKhongDAO dao = new HangHangKhongDAO();
        if (!dao.Them(hhk))
            return false;
        dshhk.Add(hhk);
        return true;
    }

    public bool Sua(HangHangKhongDTO hhk)
    {
        DocDSHHK();
        HangHangKhongDAO dao = new HangHangKhongDAO();
        if (!dao.Sua(hhk))
            return false;
        foreach (HangHangKhongDTO h in dshhk)
            if (h.MaHHK.CompareTo(hhk.MaHHK) == 0)
            {
                h.TenHang = hhk.TenHang;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSHHK();
        HangHangKhongDAO dao = new HangHangKhongDAO();
        if (!dao.Xoa(ma))
            return false;
        dshhk.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi. Trả về "" nếu hợp lệ, ngược lại trả về lỗi.
    public string KiemTra(HangHangKhongDTO x)
    {
        if (x.TenHang.Trim() == "")
            return "Nhập tên hãng.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<HangHangKhongDTO> TimKiem(string tuKhoa)
    {
        List<HangHangKhongDTO> kq = new List<HangHangKhongDTO>();
        foreach (HangHangKhongDTO x in DocDSHHK())
            if (TimKiemBUS.Chua(x.MaHHK, tuKhoa)
                || TimKiemBUS.Chua(x.TenHang, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<HangHangKhongDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSHHK(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSHHK();
        return timKiem(ma) != null;
    }

    public HangHangKhongDTO timKiem(string ma)
    {
        foreach (HangHangKhongDTO h in DocDSHHK())
            if (h.MaHHK.CompareTo(ma) == 0)
                return h;
        return null;
    }
}
