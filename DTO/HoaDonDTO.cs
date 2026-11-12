namespace BanVeMayBay.DTO;

public class HoaDonDTO
{
    public string MaHD { get; set; } = "";
    public DateTime NgayLap { get; set; }
    public string MaNV { get; set; } = "";
    public string MaKH { get; set; } = "";
    public decimal TongTien { get; set; }

    public HoaDonDTO()
    {
    }

    public HoaDonDTO(string maHD, DateTime ngayLap, string maNV, string maKH, decimal tongTien)
    {
        this.MaHD = maHD;
        this.NgayLap = ngayLap;
        this.MaNV = maNV;
        this.MaKH = maKH;
        this.TongTien = tongTien;
    }
}
