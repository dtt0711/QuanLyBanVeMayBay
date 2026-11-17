using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class HanhKhachBUS
{
    private static List<HanhKhachDTO> dshk;

    public List<HanhKhachDTO> DocDSHK()
    {
        HanhKhachDAO dao = new HanhKhachDAO();
        if (dshk == null || dshk.Count == 0)
        {
            dshk = new List<HanhKhachDTO>();
            dshk = dao.DocDSHK();
        }
        return dshk;
    }

    public static void LamMoiDS()
    {
        dshk = null;
    }

    public List<HanhKhachDTO> DocTheoKH(string maCha)
    {
        HanhKhachDAO dao = new HanhKhachDAO();
        return dao.DocTheoKH(maCha);
    }

    public bool Them(HanhKhachDTO hk)
    {
        DocDSHK();
        HanhKhachDAO dao = new HanhKhachDAO();
        if (!dao.Them(hk))
            return false;
        dshk.Add(hk);
        return true;
    }

    public bool Sua(HanhKhachDTO hk)
    {
        DocDSHK();
        HanhKhachDAO dao = new HanhKhachDAO();
        if (!dao.Sua(hk))
            return false;
        foreach (HanhKhachDTO h in dshk)
            if (h.MaHK.CompareTo(hk.MaHK) == 0)
            {
                h.HoTen = hk.HoTen;
                h.NgaySinh = hk.NgaySinh;
                h.CCCD = hk.CCCD;
                h.GioiTinh = hk.GioiTinh;
                h.MaKH = hk.MaKH;
                h.LoaiKH = hk.LoaiKH;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSHK();
        HanhKhachDAO dao = new HanhKhachDAO();
        if (!dao.Xoa(ma))
            return false;
        dshk.Remove(timKiem(ma));
        return true;
    }

    // Loại hành khách tự tính theo tuổi, không cho nhập: tuổi <= TUOI_TRE_EM là trẻ em
    public static string TinhLoaiHK(DateTime ngaySinh)
    {
        int tuoiTreEm = (int)ThamSoBUS.LayGiaTri("TUOI_TRE_EM", 12);
        DateTime homNay = DateTime.Today;
        int tuoi = homNay.Year - ngaySinh.Year;
        if (ngaySinh.Date > homNay.AddYears(-tuoi))
            tuoi--;
        return tuoi <= tuoiTreEm ? "Trẻ em" : "Người lớn";
    }

    public string KiemTra(HanhKhachDTO x)
    {
        if (x.HoTen.Trim() == "")
            return "Nhập họ tên hành khách.";
        if (x.NgaySinh.Date > DateTime.Today)
            return "Ngày sinh không được sau hôm nay.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<HanhKhachDTO> TimKiem(string tuKhoa)
    {
        List<HanhKhachDTO> kq = new List<HanhKhachDTO>();
        foreach (HanhKhachDTO x in DocDSHK())
            if (TimKiemBUS.Chua(x.MaHK, tuKhoa)
                || TimKiemBUS.Chua(x.HoTen, tuKhoa)
                || TimKiemBUS.Chua(x.CCCD, tuKhoa)
                || TimKiemBUS.Chua(x.GioiTinh, tuKhoa)
                || TimKiemBUS.Chua(x.MaKH, tuKhoa)
                || TimKiemBUS.Chua(x.LoaiKH, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<HanhKhachDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSHK(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSHK();
        return timKiem(ma) != null;
    }

    public HanhKhachDTO timKiem(string ma)
    {
        foreach (HanhKhachDTO h in DocDSHK())
            if (h.MaHK.CompareTo(ma) == 0)
                return h;
        return null;
    }
}
