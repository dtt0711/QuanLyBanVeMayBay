using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class NhanVienBUS
{
    private static List<NhanVienDTO> dsnv;

    public List<NhanVienDTO> DocDSNV()
    {
        NhanVienDAO dao = new NhanVienDAO();
        if (dsnv == null || dsnv.Count == 0)
        {
            dsnv = new List<NhanVienDTO>();
            dsnv = dao.DocDSNV();
        }
        return dsnv;
    }

    public static void LamMoiDS()
    {
        dsnv = null;
    }

    public bool Them(NhanVienDTO nv)
    {
        DocDSNV();
        NhanVienDAO dao = new NhanVienDAO();
        if (!dao.Them(nv))
            return false;
        dsnv.Add(nv);
        return true;
    }

    public bool Sua(NhanVienDTO nv)
    {
        DocDSNV();
        NhanVienDAO dao = new NhanVienDAO();
        if (!dao.Sua(nv))
            return false;
        foreach (NhanVienDTO n in dsnv)
            if (n.MaNV.CompareTo(nv.MaNV) == 0)
            {
                n.Ho = nv.Ho;
                n.Ten = nv.Ten;
                n.ChucVu = nv.ChucVu;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSNV();
        NhanVienDAO dao = new NhanVienDAO();
        if (!dao.Xoa(ma))
            return false;
        dsnv.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi. Trả về "" nếu hợp lệ, ngược lại trả về lỗi.
    public string KiemTra(NhanVienDTO x)
    {
        if (x.Ho.Trim() == "" || x.Ten.Trim() == "")
            return "Nhập họ và tên nhân viên.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<NhanVienDTO> TimKiem(string tuKhoa)
    {
        List<NhanVienDTO> kq = new List<NhanVienDTO>();
        foreach (NhanVienDTO x in DocDSNV())
            if (TimKiemBUS.Chua(x.MaNV, tuKhoa)
                || TimKiemBUS.Chua(x.Ho + " " + x.Ten, tuKhoa)
                || TimKiemBUS.Chua(x.Ho, tuKhoa)
                || TimKiemBUS.Chua(x.Ten, tuKhoa)
                || TimKiemBUS.Chua(x.ChucVu, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<NhanVienDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSNV(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSNV();
        return timKiem(ma) != null;
    }

    public NhanVienDTO timKiem(string ma)
    {
        foreach (NhanVienDTO n in DocDSNV())
            if (n.MaNV.CompareTo(ma) == 0)
                return n;
        return null;
    }
}
