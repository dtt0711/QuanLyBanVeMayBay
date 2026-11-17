using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class CTHDBUS
{
    public List<CTHDDTO> DocDSCTHD()
    {
        CTHDDAO dao = new CTHDDAO();
        return dao.DocDSCTHD();
    }

    public List<CTHDDTO> DocTheoHD(string maCha)
    {
        CTHDDAO dao = new CTHDDAO();
        return dao.DocTheoHD(maCha);
    }

    public List<CTHDDTO> DocTheoCB(string maCha)
    {
        CTHDDAO dao = new CTHDDAO();
        return dao.DocTheoCB(maCha);
    }

    public bool TrungMa(string maHD, string maVe)
    {
        foreach (CTHDDTO c in DocTheoHD(maHD))
            if (c.MaVe.CompareTo(maVe) == 0)
                return true;
        return false;
    }

    // Các vé của phiếu đặt (của hóa đơn) chưa được tính tiền ở hóa đơn nào
    public List<VeDTO> VeChuaLapHD(string maHD)
    {
        List<VeDTO> kq = new List<VeDTO>();
        HoaDonDTO hd = new HoaDonBUS().timKiem(maHD);
        if (hd == null)
            return kq;
        CTHDDAO dao = new CTHDDAO();
        foreach (VeDTO v in new VeBUS().DocTheoDatVe(hd.MaDV))
            if (dao.DaLapHoaDon(v.MaVe) == "")
                kq.Add(v);
        return kq;
    }

    // Một dòng chi tiết = một vé: vé phải thuộc phiếu đặt của hóa đơn và chỉ được tính tiền 1 lần
    public string KiemTra(string maHD, string maVe)
    {
        HoaDonDTO hd = new HoaDonBUS().timKiem(maHD);
        VeDTO ve = new VeBUS().timKiem(maVe);
        if (hd == null || ve == null)
            return "Không tìm thấy hóa đơn hoặc vé.";
        if (ve.MaDV != hd.MaDV)
            return "Vé " + maVe + " thuộc phiếu đặt " + ve.MaDV + ", không thuộc phiếu đặt " + hd.MaDV + " của hóa đơn này.";
        string daLap = new CTHDDAO().DaLapHoaDon(maVe);
        if (daLap != "")
            return "Vé " + maVe + " đã được tính tiền ở hóa đơn " + daLap + ".";
        return "";
    }

    // Số lượng luôn là 1 vì một vé là một ghế của một hành khách
    public bool Them(string maHD, string maVe)
    {
        VeDTO ve = new VeBUS().timKiem(maVe);
        if (ve == null)
            return false;
        CTHDDTO cthd = new CTHDDTO(maHD, maVe, ve.DonGiaVe, 1, ve.DonGiaVe);
        CTHDDAO dao = new CTHDDAO();
        if (!dao.Them(cthd))
            return false;
        HoaDonBUS.LamMoiDS();
        return true;
    }

    public bool Xoa(string maHD, string maVe)
    {
        CTHDDAO dao = new CTHDDAO();
        if (!dao.Xoa(maHD, maVe))
            return false;
        HoaDonBUS.LamMoiDS();
        return true;
    }
}
