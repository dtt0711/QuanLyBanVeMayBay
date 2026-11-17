using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class ThamSoBUS
{
    private static List<ThamSoDTO> dsts;

    public List<ThamSoDTO> DocDSTS()
    {
        if (dsts == null || dsts.Count == 0)
        {
            ThamSoDAO dao = new ThamSoDAO();
            dsts = new List<ThamSoDTO>();
            dsts = dao.DocDSTS();
        }
        return dsts;
    }

    public static void LamMoiDS()
    {
        dsts = null;
    }

    // Lấy giá trị một tham số; nếu bảng tham số chưa có dòng đó thì dùng giá trị mặc định
    public static decimal LayGiaTri(string ma, decimal macDinh)
    {
        ThamSoDTO ts = new ThamSoBUS().timKiem(ma);
        return ts == null ? macDinh : ts.GiaTri;
    }

    public string KiemTra(ThamSoDTO ts)
    {
        if (ts.TenThamSo.Trim() == "")
            return "Nhập tên tham số.";
        if (ts.GiaTri < 0)
            return "Giá trị tham số không được âm.";
        return "";
    }

    public bool Them(ThamSoDTO ts)
    {
        DocDSTS();
        ThamSoDAO dao = new ThamSoDAO();
        if (!dao.Them(ts))
            return false;
        dsts.Add(ts);
        return true;
    }

    public bool Sua(ThamSoDTO ts)
    {
        DocDSTS();
        ThamSoDAO dao = new ThamSoDAO();
        if (!dao.Sua(ts))
            return false;
        foreach (ThamSoDTO t in dsts)
            if (t.MaThamSo.CompareTo(ts.MaThamSo) == 0)
            {
                t.TenThamSo = ts.TenThamSo;
                t.GiaTri = ts.GiaTri;
                t.DonVi = ts.DonVi;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSTS();
        ThamSoDAO dao = new ThamSoDAO();
        if (!dao.Xoa(ma))
            return false;
        dsts.Remove(timKiem(ma));
        return true;
    }

    public List<ThamSoDTO> TimKiem(string tuKhoa)
    {
        List<ThamSoDTO> kq = new List<ThamSoDTO>();
        foreach (ThamSoDTO x in DocDSTS())
            if (TimKiemBUS.Chua(x.MaThamSo, tuKhoa) || TimKiemBUS.Chua(x.TenThamSo, tuKhoa) || TimKiemBUS.Chua(x.DonVi, tuKhoa))
                kq.Add(x);
        return kq;
    }

    public List<ThamSoDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSTS(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSTS();
        return timKiem(ma) != null;
    }

    public ThamSoDTO timKiem(string ma)
    {
        foreach (ThamSoDTO t in DocDSTS())
            if (t.MaThamSo.CompareTo(ma) == 0)
                return t;
        return null;
    }
}
