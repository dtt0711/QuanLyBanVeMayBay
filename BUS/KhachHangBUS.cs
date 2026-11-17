using System.Data;
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

    // Nhập khách hàng từ Excel: mã chưa có thì thêm, mã đã có thì cập nhật.
    // Mỗi dòng đều qua KiemTra như khi nhập tay; dòng lỗi bị bỏ qua và báo lại.
    // Không có cột Mật khẩu: khách cũ giữ mật khẩu cũ, khách mới lấy mật khẩu = số CCCD.
    public string NhapExcel(DataTable bang)
    {
        if (!ExcelBUS.CoCot(bang, "Mã KH") || !ExcelBUS.CoCot(bang, "Họ tên") || !ExcelBUS.CoCot(bang, "CCCD"))
            return "File cần có các cột: Mã KH, Họ tên, CCCD, SĐT, Email, Tài khoản, Mật khẩu (có thể bỏ), Địa chỉ.";
        int them = 0, sua = 0;
        List<string> loi = new List<string>();
        for (int i = 0; i < bang.Rows.Count; i++)
        {
            DataRow r = bang.Rows[i];
            string ma = ExcelBUS.Lay(r, "Mã KH");
            if (ma == "")
            {
                loi.Add("Dòng " + (i + 2) + ": thiếu mã khách hàng.");
                continue;
            }
            string cccd = ExcelBUS.Lay(r, "CCCD");
            if (cccd.All(char.IsDigit) && cccd.Length < 12)
                cccd = cccd.PadLeft(12, '0');      // Excel làm mất số 0 đầu khi ô là số
            string sdt = ExcelBUS.Lay(r, "SĐT", "SDT");
            if (sdt.All(char.IsDigit) && sdt.Length == 9)
                sdt = "0" + sdt;
            KhachHangDTO cu = timKiem(ma);
            string matKhau = ExcelBUS.Lay(r, "Mật khẩu", "MatKhau");
            if (matKhau == "")
                matKhau = cu != null ? cu.MatKhau : cccd;
            KhachHangDTO kh = new KhachHangDTO(ma, ExcelBUS.Lay(r, "Họ tên"), cccd, sdt, ExcelBUS.Lay(r, "Email"),
                ExcelBUS.Lay(r, "Tài khoản", "TaiKhoan"), matKhau, ExcelBUS.Lay(r, "Địa chỉ", "DiaChi"));
            string l = KiemTra(kh);
            if (l != "")
            {
                loi.Add("Dòng " + (i + 2) + " (" + ma + "): " + l);
                continue;
            }
            if (cu == null)
            {
                if (Them(kh)) them++;
                else loi.Add("Dòng " + (i + 2) + " (" + ma + "): không ghi được vào CSDL.");
            }
            else
            {
                if (Sua(kh)) sua++;
                else loi.Add("Dòng " + (i + 2) + " (" + ma + "): không cập nhật được.");
            }
        }
        return KetQuaNhap(them, sua, loi);
    }

    public static string KetQuaNhap(int them, int sua, List<string> loi)
    {
        string kq = "Đã thêm " + them + " dòng, cập nhật " + sua + " dòng.";
        if (loi.Count > 0)
            kq += Environment.NewLine + loi.Count + " dòng bị bỏ qua:" + Environment.NewLine
                + string.Join(Environment.NewLine, loi.Take(15)) + (loi.Count > 15 ? Environment.NewLine + "..." : "");
        return kq;
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
