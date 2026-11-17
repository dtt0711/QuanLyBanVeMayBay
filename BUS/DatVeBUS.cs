using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class DatVeBUS
{
    private static List<DatVeDTO> dsdv;

    public List<DatVeDTO> DocDSDV()
    {
        DatVeDAO dao = new DatVeDAO();
        if (dsdv == null || dsdv.Count == 0)
        {
            dsdv = new List<DatVeDTO>();
            dsdv = dao.DocDSDV();
        }
        return dsdv;
    }

    public static void LamMoiDS()
    {
        dsdv = null;
    }

    public List<DatVeDTO> DocTheoKH(string maCha)
    {
        DatVeDAO dao = new DatVeDAO();
        return dao.DocTheoKH(maCha);
    }

    public List<DatVeDTO> DocTheoChuyenBay(string maCha)
    {
        DatVeDAO dao = new DatVeDAO();
        return dao.DocTheoChuyenBay(maCha);
    }

    // Tổng tiền do CSDL tự tính lại khi ghi, nên đọc lại danh sách sau khi ghi
    public bool Them(DatVeDTO dv)
    {
        DatVeDAO dao = new DatVeDAO();
        if (!dao.Them(dv))
            return false;
        LamMoiDS();
        HoaDonBUS.LamMoiDS();
        return true;
    }

    public bool Sua(DatVeDTO dv)
    {
        DatVeDAO dao = new DatVeDAO();
        if (!dao.Sua(dv))
            return false;
        LamMoiDS();
        HoaDonBUS.LamMoiDS();
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSDV();
        DatVeDAO dao = new DatVeDAO();
        if (!dao.Xoa(ma))
            return false;
        dsdv.Remove(timKiem(ma));
        return true;
    }

    // Giảm giá không cho nhập: lấy theo voucher được chọn
    public static decimal LayGiamGia(string maVoucher)
    {
        VoucherDTO vc = maVoucher == "" ? null : new VoucherBUS().timKiem(maVoucher);
        return vc == null ? 0 : (decimal)vc.GiamGia;
    }

    // Kiểm tra trước khi ghi: thời hạn đặt vé (tham số GIO_DAT_TRUOC) và thời gian của voucher
    public string KiemTra(DatVeDTO x, bool laThem)
    {
        DatVeDTO cu = laThem ? null : timKiem(x.MaDV);
        bool doiChuyen = cu == null || cu.MaChuyenBay != x.MaChuyenBay;
        bool doiVoucher = cu == null || cu.MaVoucher != x.MaVoucher;
        ChuyenBayDTO cb = new ChuyenBayBUS().timKiem(x.MaChuyenBay);
        decimal gioDatTruoc = ThamSoBUS.LayGiaTri("GIO_DAT_TRUOC", 2);
        if (doiChuyen && cb != null && cb.TrangThai.ToLower().Contains("hủy"))
            return "Chuyến " + cb.MaChuyenBay + " đã bị hủy.";
        if (doiChuyen && cb != null && (cb.NgayKhoiHanh - x.NgayDat).TotalHours < (double)gioDatTruoc)
            return "Phải đặt vé trước giờ khởi hành ít nhất " + gioDatTruoc + " giờ.";
        if (cu != null && doiChuyen && new VeBUS().DocTheoDatVe(x.MaDV).Count > 0)
            return "Phiếu đặt đã có vé, không đổi chuyến bay được. Hãy trả vé trước.";
        if (doiVoucher && x.MaVoucher != "")
        {
            VoucherDTO vc = new VoucherBUS().timKiem(x.MaVoucher);
            CTKMDTO km = vc == null ? null : new CTKMBUS().timKiem(vc.MaCTKM);
            if (km != null && (x.NgayDat.Date < km.NgayBatDau.Date || x.NgayDat.Date > km.NgayKetThuc.Date))
                return "Voucher " + x.MaVoucher + " chỉ dùng từ " + km.NgayBatDau.ToString("dd/MM/yyyy")
                    + " đến " + km.NgayKetThuc.ToString("dd/MM/yyyy") + ".";
        }
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<DatVeDTO> TimKiem(string tuKhoa)
    {
        List<DatVeDTO> kq = new List<DatVeDTO>();
        foreach (DatVeDTO x in DocDSDV())
            if (TimKiemBUS.Chua(x.MaDV, tuKhoa)
                || TimKiemBUS.Chua(x.MaKH, tuKhoa)
                || TimKiemBUS.Chua(x.MaNV, tuKhoa)
                || TimKiemBUS.Chua(x.MaChuyenBay, tuKhoa)
                || TimKiemBUS.Chua(x.TrangThai, tuKhoa)
                || TimKiemBUS.Chua(x.MaVoucher, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<DatVeDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSDV(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSDV();
        return timKiem(ma) != null;
    }

    public DatVeDTO timKiem(string ma)
    {
        foreach (DatVeDTO d in DocDSDV())
            if (d.MaDV.CompareTo(ma) == 0)
                return d;
        return null;
    }
}
