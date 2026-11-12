namespace BanVeMayBay.DTO;

public class CTHDDTO
{
    public string MaHD { get; set; } = "";
    public string MaCB { get; set; } = "";
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien { get; set; }

    public CTHDDTO()
    {
    }

    public CTHDDTO(string maHD, string maCB, int soLuong, decimal donGia, decimal thanhTien)
    {
        this.MaHD = maHD;
        this.MaCB = maCB;
        this.SoLuong = soLuong;
        this.DonGia = donGia;
        this.ThanhTien = thanhTien;
    }
}
