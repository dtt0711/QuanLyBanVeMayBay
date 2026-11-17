namespace QuanLyBanVeMayBay.DTO;

public class VeDTO
{
    public string MaVe { get; set; } = "";
    public string MaChuyenBay { get; set; } = "";
    public string MaHV { get; set; } = "";
    public string MaGhe { get; set; } = "";
    public string MaHK { get; set; } = "";
    public decimal DonGiaVe { get; set; }
    public DateTime Ngay { get; set; }
    public string MaDV { get; set; } = "";
    public string TrangThai { get; set; } = "Hiệu lực";   // Hiệu lực / Đã hủy

    public VeDTO()
    {
    }

    public VeDTO(string maVe, string maChuyenBay, string maHV, string maGhe, string maHK, decimal donGiaVe, DateTime ngay, string maDV)
    {
        this.MaVe = maVe;
        this.MaChuyenBay = maChuyenBay;
        this.MaHV = maHV;
        this.MaGhe = maGhe;
        this.MaHK = maHK;
        this.DonGiaVe = donGiaVe;
        this.Ngay = ngay;
        this.MaDV = maDV;
    }
}
