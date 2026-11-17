using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class SanBayBUS
{
    private static List<SanBayDTO> dssb;

    public List<SanBayDTO> DocDSSB()
    {
        SanBayDAO dao = new SanBayDAO();
        if (dssb == null || dssb.Count == 0)
        {
            dssb = new List<SanBayDTO>();
            dssb = dao.DocDSSB();
        }
        return dssb;
    }

    public static void LamMoiDS()
    {
        dssb = null;
    }

    public bool Them(SanBayDTO sb)
    {
        DocDSSB();
        SanBayDAO dao = new SanBayDAO();
        if (!dao.Them(sb))
            return false;
        dssb.Add(sb);
        return true;
    }

    public bool Sua(SanBayDTO sb)
    {
        DocDSSB();
        SanBayDAO dao = new SanBayDAO();
        if (!dao.Sua(sb))
            return false;
        foreach (SanBayDTO s in dssb)
            if (s.MaSanBay.CompareTo(sb.MaSanBay) == 0)
            {
                s.Ten = sb.Ten;
                s.LoaiSanBay = sb.LoaiSanBay;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSSB();
        SanBayDAO dao = new SanBayDAO();
        if (!dao.Xoa(ma))
            return false;
        dssb.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi. Trả về "" nếu hợp lệ, ngược lại trả về lỗi.
    public string KiemTra(SanBayDTO x)
    {
        if (x.Ten.Trim() == "")
            return "Nhập tên sân bay.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<SanBayDTO> TimKiem(string tuKhoa)
    {
        List<SanBayDTO> kq = new List<SanBayDTO>();
        foreach (SanBayDTO x in DocDSSB())
            if (TimKiemBUS.Chua(x.MaSanBay, tuKhoa)
                || TimKiemBUS.Chua(x.Ten, tuKhoa)
                || TimKiemBUS.Chua(x.LoaiSanBay, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<SanBayDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSSB(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSSB();
        return timKiem(ma) != null;
    }

    public SanBayDTO timKiem(string ma)
    {
        foreach (SanBayDTO s in DocDSSB())
            if (s.MaSanBay.CompareTo(ma) == 0)
                return s;
        return null;
    }
}
