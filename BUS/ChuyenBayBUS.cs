using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class ChuyenBayBUS
{
    private static List<ChuyenBayDTO> dscb;

    public List<ChuyenBayDTO> DocDSCB()
    {
        ChuyenBayDAO dao = new ChuyenBayDAO();
        if (dscb == null || dscb.Count == 0)
        {
            dscb = new List<ChuyenBayDTO>();
            dscb = dao.DocDSCB();
        }
        return dscb;
    }

    public static void LamMoiDS()
    {
        dscb = null;
    }

    public List<ChuyenBayDTO> DocTheoMayBay(string maCha)
    {
        ChuyenBayDAO dao = new ChuyenBayDAO();
        return dao.DocTheoMayBay(maCha);
    }

    public bool Them(ChuyenBayDTO cb)
    {
        DocDSCB();
        ChuyenBayDAO dao = new ChuyenBayDAO();
        if (!dao.Them(cb))
            return false;
        dscb.Add(cb);
        return true;
    }

    public bool Sua(ChuyenBayDTO cb)
    {
        DocDSCB();
        ChuyenBayDAO dao = new ChuyenBayDAO();
        if (!dao.Sua(cb))
            return false;
        foreach (ChuyenBayDTO c in dscb)
            if (c.MaChuyenBay.CompareTo(cb.MaChuyenBay) == 0)
            {
                c.MaHHK = cb.MaHHK;
                c.MaSanBayDi = cb.MaSanBayDi;
                c.MaSanBayDen = cb.MaSanBayDen;
                c.MaMayBay = cb.MaMayBay;
                c.NgayKhoiHanh = cb.NgayKhoiHanh;
                c.NgayDen = cb.NgayDen;
                c.ThoiGianBay = cb.ThoiGianBay;
                c.ThoiGianDen = cb.ThoiGianDen;
                c.TongSoGhe = cb.TongSoGhe;
                c.SoVeDaBan = cb.SoVeDaBan;
                c.GiaVe = cb.GiaVe;
                c.TrangThai = cb.TrangThai;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSCB();
        ChuyenBayDAO dao = new ChuyenBayDAO();
        if (!dao.Xoa(ma))
            return false;
        dscb.Remove(timKiem(ma));
        return true;
    }

    // Thời gian bay (phút) tự tính từ ngày khởi hành và ngày đến, không cho nhập
    public static int TinhThoiGianBay(DateTime ngayKhoiHanh, DateTime ngayDen)
    {
        return (int)(ngayDen - ngayKhoiHanh).TotalMinutes;
    }

    // Kiểm tra dữ liệu trước khi ghi, giới hạn lấy từ bảng tham số
    public string KiemTra(ChuyenBayDTO x)
    {
        decimal tgMin = ThamSoBUS.LayGiaTri("TG_BAY_TOI_THIEU", 30);
        decimal tgMax = ThamSoBUS.LayGiaTri("TG_BAY_TOI_DA", 1200);
        decimal gheMax = ThamSoBUS.LayGiaTri("SO_GHE_TOI_DA", 500);
        decimal giaMin = ThamSoBUS.LayGiaTri("GIA_VE_TOI_THIEU", 100000);
        if (x.MaSanBayDi == x.MaSanBayDen)
            return "Sân bay đi và sân bay đến phải khác nhau.";
        if (x.ThoiGianBay < tgMin || x.ThoiGianBay > tgMax)
            return "Thời gian bay phải từ " + tgMin + " đến " + tgMax + " phút (ngày đến - ngày khởi hành).";
        if (x.TongSoGhe <= 0)
            return "Máy bay " + x.MaMayBay + " chưa có ghế nào. Thêm ghế ở mục Ghế trước.";
        if (x.TongSoGhe > gheMax)
            return "Máy bay có " + x.TongSoGhe + " ghế, vượt tham số tối đa " + gheMax + ".";
        ChuyenBayDTO cu = timKiem(x.MaChuyenBay);
        if (cu != null && cu.SoVeDaBan > 0 && cu.MaMayBay != x.MaMayBay)
            return "Chuyến đã bán " + cu.SoVeDaBan + " vé theo sơ đồ ghế của " + cu.MaMayBay + ", không đổi máy bay được.";
        if (x.TongSoGhe < x.SoVeDaBan)
            return "Tổng số ghế không được nhỏ hơn số vé đã bán (" + x.SoVeDaBan + ").";
        if (x.GiaVe < giaMin)
            return "Giá vé tối thiểu là " + giaMin.ToString("N0") + " đ.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<ChuyenBayDTO> TimKiem(string tuKhoa)
    {
        List<ChuyenBayDTO> kq = new List<ChuyenBayDTO>();
        foreach (ChuyenBayDTO x in DocDSCB())
            if (TimKiemBUS.Chua(x.MaChuyenBay, tuKhoa)
                || TimKiemBUS.Chua(x.MaHHK, tuKhoa)
                || TimKiemBUS.Chua(x.MaSanBayDi, tuKhoa)
                || TimKiemBUS.Chua(x.MaSanBayDen, tuKhoa)
                || TimKiemBUS.Chua(x.MaMayBay, tuKhoa)
                || TimKiemBUS.Chua(x.ThoiGianDen, tuKhoa)
                || TimKiemBUS.Chua(x.TrangThai, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<ChuyenBayDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSCB(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSCB();
        return timKiem(ma) != null;
    }

    public ChuyenBayDTO timKiem(string ma)
    {
        foreach (ChuyenBayDTO c in DocDSCB())
            if (c.MaChuyenBay.CompareTo(ma) == 0)
                return c;
        return null;
    }
}
