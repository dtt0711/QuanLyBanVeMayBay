using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class CTKMBUS
{
    private static List<CTKMDTO> dsctkm;

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

    public static void LamMoiDS()
    {
        dsctkm = null;
    }

    public bool Them(CTKMDTO ctkm)
    {
        DocDSCTKM();
        CTKMDAO dao = new CTKMDAO();
        if (!dao.Them(ctkm))
            return false;
        dsctkm.Add(ctkm);
        return true;
    }

    public bool Sua(CTKMDTO ctkm)
    {
        DocDSCTKM();
        CTKMDAO dao = new CTKMDAO();
        if (!dao.Sua(ctkm))
            return false;
        foreach (CTKMDTO c in dsctkm)
            if (c.MaCTKM.CompareTo(ctkm.MaCTKM) == 0)
            {
                c.TenCTKM = ctkm.TenCTKM;
                c.NgayBatDau = ctkm.NgayBatDau;
                c.NgayKetThuc = ctkm.NgayKetThuc;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSCTKM();
        CTKMDAO dao = new CTKMDAO();
        if (!dao.Xoa(ma))
            return false;
        dsctkm.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi. Trả về "" nếu hợp lệ, ngược lại trả về lỗi.
    public string KiemTra(CTKMDTO x)
    {
        if (x.TenCTKM.Trim() == "")
            return "Nhập tên chương trình.";
        if (x.NgayKetThuc.Date < x.NgayBatDau.Date)
            return "Ngày kết thúc phải sau ngày bắt đầu.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<CTKMDTO> TimKiem(string tuKhoa)
    {
        List<CTKMDTO> kq = new List<CTKMDTO>();
        foreach (CTKMDTO x in DocDSCTKM())
            if (TimKiemBUS.Chua(x.MaCTKM, tuKhoa)
                || TimKiemBUS.Chua(x.TenCTKM, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<CTKMDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSCTKM(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSCTKM();
        return timKiem(ma) != null;
    }

    public CTKMDTO timKiem(string ma)
    {
        foreach (CTKMDTO c in DocDSCTKM())
            if (c.MaCTKM.CompareTo(ma) == 0)
                return c;
        return null;
    }
}
