using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class GheBUS
{
    private static List<GheDTO> dsghe;

    public List<GheDTO> DocDSGhe()
    {
        GheDAO dao = new GheDAO();
        if (dsghe == null || dsghe.Count == 0)
        {
            dsghe = new List<GheDTO>();
            dsghe = dao.DocDSGhe();
        }
        return dsghe;
    }

    public static void LamMoiDS()
    {
        dsghe = null;
    }

    public List<GheDTO> DocTheoMayBay(string maCha)
    {
        GheDAO dao = new GheDAO();
        return dao.DocTheoMayBay(maCha);
    }

    public List<GheDTO> DocTheoHV(string maCha)
    {
        GheDAO dao = new GheDAO();
        return dao.DocTheoHV(maCha);
    }

    public bool Them(GheDTO g)
    {
        DocDSGhe();
        GheDAO dao = new GheDAO();
        if (!dao.Them(g))
            return false;
        dsghe.Add(g);
        ChuyenBayBUS.LamMoiDS();
        return true;
    }

    public bool Sua(GheDTO g)
    {
        DocDSGhe();
        GheDAO dao = new GheDAO();
        if (!dao.Sua(g))
            return false;
        ChuyenBayBUS.LamMoiDS();
        foreach (GheDTO x in dsghe)
            if (x.MaGhe.CompareTo(g.MaGhe) == 0)
            {
                x.MaMayBay = g.MaMayBay;
                x.MaHV = g.MaHV;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSGhe();
        GheDAO dao = new GheDAO();
        if (!dao.Xoa(ma))
            return false;
        ChuyenBayBUS.LamMoiDS();
        dsghe.Remove(timKiem(ma));
        return true;
    }

    // Ghế đã bán vé thì không được đổi sang máy bay hoặc hạng vé khác (vé sẽ sai hạng, sai giá)
    public string KiemTra(GheDTO g, bool laThem)
    {
        if (laThem)
            return "";
        GheDTO cu = timKiem(g.MaGhe);
        if (cu != null && (cu.MaMayBay != g.MaMayBay || cu.MaHV != g.MaHV))
            foreach (VeDTO v in new VeBUS().DocDSVe())
                if (v.MaGhe == g.MaGhe)
                    return "Ghế " + g.MaGhe + " đã bán (vé " + v.MaVe + "), không đổi máy bay hoặc hạng vé được.";
        return "";
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<GheDTO> TimKiem(string tuKhoa)
    {
        List<GheDTO> kq = new List<GheDTO>();
        foreach (GheDTO x in DocDSGhe())
            if (TimKiemBUS.Chua(x.MaGhe, tuKhoa)
                || TimKiemBUS.Chua(x.MaMayBay, tuKhoa)
                || TimKiemBUS.Chua(x.MaHV, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<GheDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSGhe(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSGhe();
        return timKiem(ma) != null;
    }

    public GheDTO timKiem(string ma)
    {
        foreach (GheDTO x in DocDSGhe())
            if (x.MaGhe.CompareTo(ma) == 0)
                return x;
        return null;
    }
}
