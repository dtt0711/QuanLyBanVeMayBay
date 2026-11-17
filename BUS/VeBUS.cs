using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class VeBUS
{
    private static List<VeDTO> dsve;

    public List<VeDTO> DocDSVe()
    {
        VeDAO dao = new VeDAO();
        if (dsve == null || dsve.Count == 0)
        {
            dsve = new List<VeDTO>();
            dsve = dao.DocDSVe();
        }
        return dsve;
    }

    public static void LamMoiDS()
    {
        dsve = null;
    }

    public List<VeDTO> DocTheoChuyenBay(string maCha)
    {
        VeDAO dao = new VeDAO();
        return dao.DocTheoChuyenBay(maCha);
    }

    public List<VeDTO> DocTheoDatVe(string maCha)
    {
        VeDAO dao = new VeDAO();
        return dao.DocTheoDatVe(maCha);
    }

    public bool Them(VeDTO ve)
    {
        DocDSVe();
        VeDAO dao = new VeDAO();
        if (!dao.Them(ve))
            return false;
        dsve.Add(ve);
        ChuyenBayBUS.LamMoiDS();
        DatVeBUS.LamMoiDS();
        return true;
    }

    public bool Sua(VeDTO ve)
    {
        DocDSVe();
        VeDAO dao = new VeDAO();
        if (!dao.Sua(ve))
            return false;
        ChuyenBayBUS.LamMoiDS();
        DatVeBUS.LamMoiDS();
        foreach (VeDTO v in dsve)
            if (v.MaVe.CompareTo(ve.MaVe) == 0)
            {
                v.MaChuyenBay = ve.MaChuyenBay;
                v.MaHV = ve.MaHV;
                v.MaGhe = ve.MaGhe;
                v.MaHK = ve.MaHK;
                v.DonGiaVe = ve.DonGiaVe;
                v.Ngay = ve.Ngay;
                v.MaDV = ve.MaDV;
            }
        return true;
    }

    // Hủy (trả) vé: đổi trạng thái, không xóa khỏi CSDL
    public bool Huy(string ma)
    {
        VeDAO dao = new VeDAO();
        if (!dao.Huy(ma))
            return false;
        LamMoiDS();
        ChuyenBayBUS.LamMoiDS();
        DatVeBUS.LamMoiDS();
        HoaDonBUS.LamMoiDS();
        return true;
    }

    public string KiemTraHuy(string ma)
    {
        VeDTO ve = timKiem(ma);
        if (ve == null)
            return "Chọn vé cần hủy.";
        if (ve.TrangThai == TrangThaiBUS.DaHuy)
            return "Vé " + ma + " đã hủy rồi.";
        DatVeDTO dv = new DatVeBUS().timKiem(ve.MaDV);
        if (dv != null && dv.TrangThai == TrangThaiBUS.DaThanhToan)
            return "Phiếu đặt " + ve.MaDV + " đã thanh toán, không hủy vé được (hoàn tiền nằm ngoài phạm vi đề tài).";
        ChuyenBayDTO cb = new ChuyenBayBUS().timKiem(ve.MaChuyenBay);
        if (cb != null && cb.NgayKhoiHanh <= DateTime.Now)
            return "Chuyến " + ve.MaChuyenBay + " đã khởi hành, không hủy vé được.";
        return "";
    }

    // Đơn giá vé không cho nhập: giá chuyến bay (giá hạng Phổ thông) x hệ số của hạng vé,
    // làm tròn đến nghìn đồng
    public static decimal LayDonGia(string maChuyenBay, string maHV)
    {
        ChuyenBayDTO cb = new ChuyenBayBUS().timKiem(maChuyenBay);
        HangVeDTO hv = new HangVeBUS().timKiem(maHV);
        if (cb == null || hv == null)
            return 0;
        return Math.Round(cb.GiaVe * hv.HeSoGia / 1000, MidpointRounding.AwayFromZero) * 1000;
    }

    // Chuyến bay còn bán vé được không: chưa khởi hành và không bị hủy
    public static string KiemTraChuyenBay(ChuyenBayDTO cb)
    {
        if (cb == null)
            return "Không tìm thấy chuyến bay.";
        if (cb.TrangThai.ToLower().Contains("hủy"))
            return "Chuyến " + cb.MaChuyenBay + " đã bị hủy, không bán vé.";
        if (cb.NgayKhoiHanh <= DateTime.Now)
            return "Chuyến " + cb.MaChuyenBay + " đã khởi hành lúc " + cb.NgayKhoiHanh.ToString("dd/MM/yyyy HH:mm") + ", không bán vé.";
        return "";
    }

    // Kiểm tra trước khi bán/sửa vé theo nghiệp vụ thực tế
    public string KiemTra(VeDTO x, bool laThem)
    {
        VeDTO cu = laThem ? null : timKiem(x.MaVe);
        if (cu != null && cu.TrangThai == TrangThaiBUS.DaHuy)
            return "Vé " + x.MaVe + " đã hủy, không sửa được.";
        // 1. Vé luôn được xuất từ một phiếu đặt, và phiếu đặt phải của đúng chuyến bay
        if (x.MaDV == "")
            return "Chọn phiếu đặt vé. Vé phải thuộc một phiếu đặt.";
        DatVeDTO dv = new DatVeBUS().timKiem(x.MaDV);
        if (dv == null || dv.MaChuyenBay != x.MaChuyenBay)
            return "Phiếu đặt " + x.MaDV + " không phải của chuyến " + x.MaChuyenBay + ".";
        if (dv.TrangThai == TrangThaiBUS.DaThanhToan)
            return "Phiếu đặt " + x.MaDV + " đã thanh toán, không thêm hay sửa vé được. Hãy lập phiếu đặt mới.";
        if (dv.TrangThai == TrangThaiBUS.DaHuy)
            return "Phiếu đặt " + x.MaDV + " đã hủy.";
        // 2. Hành khách phải thuộc khách hàng đứng tên phiếu đặt
        HanhKhachDTO hk = new HanhKhachBUS().timKiem(x.MaHK);
        if (hk == null || hk.MaKH != dv.MaKH)
            return "Hành khách " + x.MaHK + " không thuộc khách hàng " + dv.MaKH + " của phiếu đặt.";
        // 3. Không bán vé cho chuyến đã khởi hành hoặc đã hủy
        bool doiChuyen = cu == null || cu.MaChuyenBay != x.MaChuyenBay;
        ChuyenBayDTO cb = new ChuyenBayBUS().timKiem(x.MaChuyenBay);
        if (doiChuyen)
        {
            string loi = KiemTraChuyenBay(cb);
            if (loi != "")
                return loi;
            if (cb.SoVeDaBan >= cb.TongSoGhe)
                return "Chuyến " + x.MaChuyenBay + " đã bán hết " + cb.TongSoGhe + " ghế.";
        }
        // 4. Một ghế chỉ bán 1 lần, một hành khách chỉ có 1 vé trên một chuyến
        foreach (VeDTO v in DocDSVe())
        {
            if (v.MaVe == x.MaVe || v.MaChuyenBay != x.MaChuyenBay || v.TrangThai == TrangThaiBUS.DaHuy)
                continue;
            if (v.MaGhe == x.MaGhe)
                return "Ghế " + x.MaGhe + " của chuyến " + x.MaChuyenBay + " đã bán (vé " + v.MaVe + ").";
            if (v.MaHK == x.MaHK)
                return "Hành khách " + x.MaHK + " đã có vé " + v.MaVe + " trên chuyến " + x.MaChuyenBay + ".";
        }
        // 5. Số vé mỗi lần đặt không quá tham số SO_VE_TOI_DA
        if (cu == null || cu.MaDV != x.MaDV)
        {
            int soVeToiDa = (int)ThamSoBUS.LayGiaTri("SO_VE_TOI_DA", 9);
            if (DocTheoDatVe(x.MaDV).Count(v => v.TrangThai != TrangThaiBUS.DaHuy) >= soVeToiDa)
                return "Mỗi lần đặt chỉ được tối đa " + soVeToiDa + " vé.";
        }
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<VeDTO> TimKiem(string tuKhoa)
    {
        List<VeDTO> kq = new List<VeDTO>();
        foreach (VeDTO x in DocDSVe())
            if (TimKiemBUS.Chua(x.MaVe, tuKhoa)
                || TimKiemBUS.Chua(x.MaChuyenBay, tuKhoa)
                || TimKiemBUS.Chua(x.MaHV, tuKhoa)
                || TimKiemBUS.Chua(x.MaGhe, tuKhoa)
                || TimKiemBUS.Chua(x.MaHK, tuKhoa)
                || TimKiemBUS.Chua(x.MaDV, tuKhoa)
                || TimKiemBUS.Chua(x.TrangThai, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<VeDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSVe(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSVe();
        return timKiem(ma) != null;
    }

    public VeDTO timKiem(string ma)
    {
        foreach (VeDTO v in DocDSVe())
            if (v.MaVe.CompareTo(ma) == 0)
                return v;
        return null;
    }
}
