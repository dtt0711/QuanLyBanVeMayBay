namespace QuanLyBanVeMayBay.DTO;

public class HoaDonDTO
{
    public string MaHD { get; set; } = "";
    public string MaKH { get; set; } = "";
    public string MaNV { get; set; } = "";
    public string MaDV { get; set; } = "";
    public DateTime NgayLap { get; set; }
    public decimal TongTien { get; set; }
    public string PhuongThucTT { get; set; } = "";
    public string TrangThai { get; set; } = "";

    public HoaDonDTO()
    {
    }

    public HoaDonDTO(string maHD, string maKH, string maNV, string maDV, DateTime ngayLap, decimal tongTien, string phuongThucTT, string trangThai)
    {
        this.MaHD = maHD;
        this.MaKH = maKH;
        this.MaNV = maNV;
        this.MaDV = maDV;
        this.NgayLap = ngayLap;
        this.TongTien = tongTien;
        this.PhuongThucTT = phuongThucTT;
        this.TrangThai = trangThai;
    }
}
