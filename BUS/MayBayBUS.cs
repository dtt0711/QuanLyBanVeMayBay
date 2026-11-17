using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class MayBayBUS
{
    private static List<MayBayDTO> dsmb;

    public List<MayBayDTO> DocDSMB()
    {
        MayBayDAO dao = new MayBayDAO();
        if (dsmb == null || dsmb.Count == 0)
        {
            dsmb = new List<MayBayDTO>();
            dsmb = dao.DocDSMB();
        }
        return dsmb;
    }

    public static void LamMoiDS()
    {
        dsmb = null;
    }

    public List<MayBayDTO> DocTheoHHK(string maCha)
    {
        MayBayDAO dao = new MayBayDAO();
        return dao.DocTheoHHK(maCha);
    }

    public bool Them(MayBayDTO mb)
    {
        DocDSMB();
        MayBayDAO dao = new MayBayDAO();
        if (!dao.Them(mb))
            return false;
        dsmb.Add(mb);
        return true;
    }

    public bool Sua(MayBayDTO mb)
    {
        DocDSMB();
        MayBayDAO dao = new MayBayDAO();
        if (!dao.Sua(mb))
            return false;
        foreach (MayBayDTO m in dsmb)
            if (m.MaMayBay.CompareTo(mb.MaMayBay) == 0)
            {
                m.MaHHK = mb.MaHHK;
                m.LoaiMayBay = mb.LoaiMayBay;
                m.SoGhe = mb.SoGhe;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSMB();
        MayBayDAO dao = new MayBayDAO();
        if (!dao.Xoa(ma))
            return false;
        dsmb.Remove(timKiem(ma));
        return true;
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<MayBayDTO> TimKiem(string tuKhoa)
    {
        List<MayBayDTO> kq = new List<MayBayDTO>();
        foreach (MayBayDTO x in DocDSMB())
            if (TimKiemBUS.Chua(x.MaMayBay, tuKhoa)
                || TimKiemBUS.Chua(x.MaHHK, tuKhoa)
                || TimKiemBUS.Chua(x.LoaiMayBay, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<MayBayDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSMB(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSMB();
        return timKiem(ma) != null;
    }

    public MayBayDTO timKiem(string ma)
    {
        foreach (MayBayDTO m in DocDSMB())
            if (m.MaMayBay.CompareTo(ma) == 0)
                return m;
        return null;
    }
}
