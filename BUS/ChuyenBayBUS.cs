using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

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

    public void Them(ChuyenBayDTO cb)
    {
        DocDSCB();
        ChuyenBayDAO dao = new ChuyenBayDAO();
        dao.Them(cb);
        dscb.Add(cb);
    }

    public void Sua(ChuyenBayDTO cb)
    {
        DocDSCB();
        ChuyenBayDAO dao = new ChuyenBayDAO();
        dao.Sua(cb);
        foreach (ChuyenBayDTO c in dscb)
            if (c.MaCB.CompareTo(cb.MaCB) == 0)
            {
                c.MaMayBay = cb.MaMayBay;
                c.TuyenBay = cb.TuyenBay;
                c.NgayBay = cb.NgayBay;
                c.GiaVe = cb.GiaVe;
                c.SoGheTrong = cb.SoGheTrong;
            }
    }

    public void Xoa(string ma)
    {
        DocDSCB();
        ChuyenBayDAO dao = new ChuyenBayDAO();
        dao.Xoa(ma);
        dscb.Remove(timKiem(ma));
    }

    public ChuyenBayDTO timKiem(string ma)
    {
        foreach (ChuyenBayDTO c in dscb)
            if (c.MaCB.CompareTo(ma) == 0)
                return c;
        return null;
    }
}
