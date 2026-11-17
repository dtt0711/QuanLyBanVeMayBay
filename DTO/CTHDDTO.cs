namespace QuanLyBanVeMayBay.DTO;

public class CTHDDTO
{
    public string MaHD { get; set; } = "";
    public string MaVe { get; set; } = "";
    public decimal DonGiaVe { get; set; }
    public int SL { get; set; }
    public decimal ThanhTien { get; set; }

    public CTHDDTO()
    {
    }

    public CTHDDTO(string maHD, string maVe, decimal donGiaVe, int sl, decimal thanhTien)
    {
        this.MaHD = maHD;
        this.MaVe = maVe;
        this.DonGiaVe = donGiaVe;
        this.SL = sl;
        this.ThanhTien = thanhTien;
    }
}
