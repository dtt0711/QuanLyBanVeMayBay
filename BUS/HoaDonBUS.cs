using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class HoaDonBUS
{
    private static List<HoaDonDTO> dshd;

    public List<HoaDonDTO> DocDSHD()
    {
        HoaDonDAO dao = new HoaDonDAO();
        if (dshd == null || dshd.Count == 0)
        {
            dshd = new List<HoaDonDTO>();
            dshd = dao.DocDSHD();
        }
        return dshd;
    }

    public static void LamMoiDS()
    {
        dshd = null;
    }

    public List<HoaDonDTO> DocTheoNV(string maCha)
    {
        HoaDonDAO dao = new HoaDonDAO();
        return dao.DocTheoNV(maCha);
    }

    public List<HoaDonDTO> DocTheoKH(string maCha)
    {
        HoaDonDAO dao = new HoaDonDAO();
        return dao.DocTheoKH(maCha);
    }

    // Tổng tiền do CSDL tự tính lại khi ghi, nên đọc lại danh sách sau khi ghi
    // Kiểm tra theo nghiệp vụ: mỗi phiếu đặt chỉ lập 1 hóa đơn, khách hàng là người đặt,
    // ngày lập không trước ngày đặt
    public string KiemTra(HoaDonDTO hd)
    {
        HoaDonDTO cu = timKiem(hd.MaHD);
        if (cu != null && cu.TrangThai == TrangThaiBUS.DaThanhToan)
            return "Hóa đơn " + hd.MaHD + " đã thanh toán, không sửa được.";
        DatVeDTO dv = new DatVeBUS().timKiem(hd.MaDV);
        if (dv == null)
            return "Chọn phiếu đặt vé.";
        if (dv.TrangThai == TrangThaiBUS.DaHuy)
            return "Phiếu đặt " + hd.MaDV + " đã hủy, không lập hóa đơn được.";
        if (!new VeBUS().DocTheoDatVe(hd.MaDV).Exists(v => v.TrangThai != TrangThaiBUS.DaHuy))
            return "Phiếu đặt " + hd.MaDV + " chưa có vé nào, bán vé trước rồi mới lập hóa đơn.";
        foreach (HoaDonDTO h in DocDSHD())
            if (h.MaDV == hd.MaDV && h.MaHD != hd.MaHD)
                return "Phiếu đặt " + hd.MaDV + " đã có hóa đơn " + h.MaHD + ".";
        if (hd.MaKH != dv.MaKH)
            return "Khách hàng của hóa đơn phải là người đặt " + dv.MaKH + ".";
        if (hd.NgayLap < dv.NgayDat)
            return "Ngày lập hóa đơn không được trước ngày đặt " + dv.NgayDat.ToString("dd/MM/yyyy HH:mm") + ".";
        return "";
    }

    // Lập hóa đơn cho phiếu đặt: tự đưa tất cả vé của phiếu đặt vào chi tiết hóa đơn
    public bool Them(HoaDonDTO hd)
    {
        HoaDonDAO dao = new HoaDonDAO();
        if (!dao.Them(hd))
            return false;
        LamMoiDS();
        CTHDBUS ct = new CTHDBUS();
        foreach (VeDTO v in ct.VeChuaLapHD(hd.MaHD))
            ct.Them(hd.MaHD, v.MaVe);
        LamMoiDS();
        DatVeBUS.LamMoiDS();
        return true;
    }

    public bool Sua(HoaDonDTO hd)
    {
        HoaDonDAO dao = new HoaDonDAO();
        if (!dao.Sua(hd))
            return false;
        LamMoiDS();
        DatVeBUS.LamMoiDS();
        return true;
    }

    // Hóa đơn đã thanh toán là chứng từ, không được xóa
    public string KiemTraXoa(string ma)
    {
        HoaDonDTO hd = timKiem(ma);
        if (hd == null)
            return "Chọn hóa đơn cần xóa.";
        if (hd.TrangThai == TrangThaiBUS.DaThanhToan)
            return "Hóa đơn " + ma + " đã thanh toán, không xóa được.";
        return "";
    }

    public bool Xoa(string ma)
    {
        HoaDonDAO dao = new HoaDonDAO();
        if (!dao.Xoa(ma))
            return false;
        LamMoiDS();
        DatVeBUS.LamMoiDS();
        return true;
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<HoaDonDTO> TimKiem(string tuKhoa)
    {
        List<HoaDonDTO> kq = new List<HoaDonDTO>();
        foreach (HoaDonDTO x in DocDSHD())
            if (TimKiemBUS.Chua(x.MaHD, tuKhoa)
                || TimKiemBUS.Chua(x.MaKH, tuKhoa)
                || TimKiemBUS.Chua(x.MaNV, tuKhoa)
                || TimKiemBUS.Chua(x.MaDV, tuKhoa)
                || TimKiemBUS.Chua(x.PhuongThucTT, tuKhoa)
                || TimKiemBUS.Chua(x.TrangThai, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<HoaDonDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSHD(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSHD();
        return timKiem(ma) != null;
    }

    public HoaDonDTO timKiem(string ma)
    {
        foreach (HoaDonDTO h in DocDSHD())
            if (h.MaHD.CompareTo(ma) == 0)
                return h;
        return null;
    }
}
