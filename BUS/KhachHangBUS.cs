using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class KhachHangBUS
{
    private static List<KhachHangDTO> dskh;

    public List<KhachHangDTO> DocDSKH()
    {
        KhachHangDAO dao = new KhachHangDAO();
        if (dskh == null || dskh.Count == 0)
        {
            dskh = new List<KhachHangDTO>();
            dskh = dao.DocDSKH();
        }
        return dskh;
    }

    public static void LamMoiDS()
    {
        dskh = null;
    }

    public bool Them(KhachHangDTO kh)
    {
        DocDSKH();
        KhachHangDAO dao = new KhachHangDAO();
        if (!dao.Them(kh))
            return false;
        dskh.Add(kh);
        return true;
    }

    public bool Sua(KhachHangDTO kh)
    {
        DocDSKH();
        KhachHangDAO dao = new KhachHangDAO();
        if (!dao.Sua(kh))
            return false;
        foreach (KhachHangDTO k in dskh)
            if (k.MaKH.CompareTo(kh.MaKH) == 0)
            {
                k.HoTen = kh.HoTen;
                k.CCCD = kh.CCCD;
                k.SDT = kh.SDT;
                k.Email = kh.Email;
                k.TaiKhoan = kh.TaiKhoan;
                k.MatKhau = kh.MatKhau;
                k.DiaChi = kh.DiaChi;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSKH();
        KhachHangDAO dao = new KhachHangDAO();
        if (!dao.Xoa(ma))
            return false;
        dskh.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi: field bắt buộc, CCCD và tài khoản không trùng với khách khác
    public string KiemTra(KhachHangDTO x)
    {
        int doDaiMK = (int)ThamSoBUS.LayGiaTri("MAT_KHAU_TOI_THIEU", 6);
        if (x.HoTen.Trim() == "")
            return "Nhập họ tên khách hàng.";
        if (x.CCCD.Length != 12 || !x.CCCD.All(char.IsDigit))
            return "CCCD phải gồm đúng 12 chữ số.";
        if (x.TaiKhoan.Trim() == "")
            return "Nhập tài khoản.";
        if (x.MatKhau.Length < doDaiMK)
            return "Mật khẩu phải có ít nhất " + doDaiMK + " ký tự.";
        foreach (KhachHangDTO k in DocDSKH())
        {
            if (k.MaKH == x.MaKH)
                continue;
            if (k.CCCD == x.CCCD)
                return "CCCD " + x.CCCD + " đã thuộc khách hàng " + k.MaKH + ".";
            if (k.TaiKhoan.ToLower() == x.TaiKhoan.Trim().ToLower())
                return "Tài khoản " + x.TaiKhoan + " đã có người dùng.";
        }
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<KhachHangDTO> TimKiem(string tuKhoa)
    {
        List<KhachHangDTO> kq = new List<KhachHangDTO>();
        foreach (KhachHangDTO x in DocDSKH())
            if (TimKiemBUS.Chua(x.MaKH, tuKhoa)
                || TimKiemBUS.Chua(x.HoTen, tuKhoa)
                || TimKiemBUS.Chua(x.CCCD, tuKhoa)
                || TimKiemBUS.Chua(x.SDT, tuKhoa)
                || TimKiemBUS.Chua(x.Email, tuKhoa)
                || TimKiemBUS.Chua(x.TaiKhoan, tuKhoa)
                || TimKiemBUS.Chua(x.DiaChi, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<KhachHangDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSKH(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSKH();
        return timKiem(ma) != null;
    }

    public KhachHangDTO timKiem(string ma)
    {
        foreach (KhachHangDTO k in DocDSKH())
            if (k.MaKH.CompareTo(ma) == 0)
                return k;
        return null;
    }
}
