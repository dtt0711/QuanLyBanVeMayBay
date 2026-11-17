using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class HangVeBUS
{
    private static List<HangVeDTO> dshv;

    public List<HangVeDTO> DocDSHV()
    {
        HangVeDAO dao = new HangVeDAO();
        if (dshv == null || dshv.Count == 0)
        {
            dshv = new List<HangVeDTO>();
            dshv = dao.DocDSHV();
        }
        return dshv;
    }

    public static void LamMoiDS()
    {
        dshv = null;
    }

    public List<HangVeDTO> DocTheoGhe(string maCha)
    {
        HangVeDAO dao = new HangVeDAO();
        return dao.DocTheoGhe(maCha);
    }

    public bool Them(HangVeDTO hv)
    {
        DocDSHV();
        HangVeDAO dao = new HangVeDAO();
        if (!dao.Them(hv))
            return false;
        dshv.Add(hv);
        return true;
    }

    public bool Sua(HangVeDTO hv)
    {
        DocDSHV();
        HangVeDAO dao = new HangVeDAO();
        if (!dao.Sua(hv))
            return false;
        foreach (HangVeDTO h in dshv)
            if (h.MaHV.CompareTo(hv.MaHV) == 0)
            {
                h.TenHV = hv.TenHV;
                h.HanhLyKg = hv.HanhLyKg;
                h.HeSoGia = hv.HeSoGia;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSHV();
        HangVeDAO dao = new HangVeDAO();
        if (!dao.Xoa(ma))
            return false;
        dshv.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi, giới hạn lấy từ bảng tham số
    public string KiemTra(HangVeDTO x)
    {
        decimal hanhLyToiDa = ThamSoBUS.LayGiaTri("HANH_LY_TOI_DA", 50);
        if (x.TenHV.Trim() == "")
            return "Nhập tên hạng vé.";
        if (x.HanhLyKg < 0 || x.HanhLyKg > hanhLyToiDa)
            return "Hành lý phải từ 0 đến " + hanhLyToiDa.ToString("N0") + " kg.";
        if (x.HeSoGia <= 0 || x.HeSoGia > 10)
            return "Hệ số giá phải lớn hơn 0 và không quá 10 (Phổ thông = 1).";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<HangVeDTO> TimKiem(string tuKhoa)
    {
        List<HangVeDTO> kq = new List<HangVeDTO>();
        foreach (HangVeDTO x in DocDSHV())
            if (TimKiemBUS.Chua(x.MaHV, tuKhoa)
                || TimKiemBUS.Chua(x.TenHV, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<HangVeDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSHV(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSHV();
        return timKiem(ma) != null;
    }

    public HangVeDTO timKiem(string ma)
    {
        foreach (HangVeDTO h in DocDSHV())
            if (h.MaHV.CompareTo(ma) == 0)
                return h;
        return null;
    }
}
