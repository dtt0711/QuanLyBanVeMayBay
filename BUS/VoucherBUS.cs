using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class VoucherBUS
{
    private static List<VoucherDTO> dsvc;

    public List<VoucherDTO> DocDSVC()
    {
        VoucherDAO dao = new VoucherDAO();
        if (dsvc == null || dsvc.Count == 0)
        {
            dsvc = new List<VoucherDTO>();
            dsvc = dao.DocDSVC();
        }
        return dsvc;
    }

    public static void LamMoiDS()
    {
        dsvc = null;
    }

    public List<VoucherDTO> DocTheoCTKM(string maCha)
    {
        VoucherDAO dao = new VoucherDAO();
        return dao.DocTheoCTKM(maCha);
    }

    public bool Them(VoucherDTO vc)
    {
        DocDSVC();
        VoucherDAO dao = new VoucherDAO();
        if (!dao.Them(vc))
            return false;
        dsvc.Add(vc);
        return true;
    }

    public bool Sua(VoucherDTO vc)
    {
        DocDSVC();
        VoucherDAO dao = new VoucherDAO();
        if (!dao.Sua(vc))
            return false;
        foreach (VoucherDTO v in dsvc)
            if (v.MaVoucher.CompareTo(vc.MaVoucher) == 0)
            {
                v.MaCTKM = vc.MaCTKM;
                v.GiamGia = vc.GiamGia;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSVC();
        VoucherDAO dao = new VoucherDAO();
        if (!dao.Xoa(ma))
            return false;
        dsvc.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi, giới hạn lấy từ bảng tham số
    public string KiemTra(VoucherDTO x)
    {
        decimal giamToiDa = ThamSoBUS.LayGiaTri("GIAM_GIA_TOI_DA", 1000000);
        if (x.GiamGia <= 0 || (decimal)x.GiamGia > giamToiDa)
            return "Mức giảm phải lớn hơn 0 và không quá " + giamToiDa.ToString("N0") + " đ.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<VoucherDTO> TimKiem(string tuKhoa)
    {
        List<VoucherDTO> kq = new List<VoucherDTO>();
        foreach (VoucherDTO x in DocDSVC())
            if (TimKiemBUS.Chua(x.MaVoucher, tuKhoa)
                || TimKiemBUS.Chua(x.MaCTKM, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<VoucherDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSVC(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSVC();
        return timKiem(ma) != null;
    }

    public VoucherDTO timKiem(string ma)
    {
        foreach (VoucherDTO v in DocDSVC())
            if (v.MaVoucher.CompareTo(ma) == 0)
                return v;
        return null;
    }
}
