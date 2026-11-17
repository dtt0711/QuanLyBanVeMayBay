namespace QuanLyBanVeMayBay.DTO;

public class DatVeDTO
{
    public string MaDV { get; set; } = "";
    public string MaKH { get; set; } = "";
    public string MaNV { get; set; } = "";
    public string MaChuyenBay { get; set; } = "";
    public DateTime NgayDat { get; set; }
    public decimal TongTien { get; set; }
    public decimal GiamGia { get; set; }
    public string TrangThai { get; set; } = "";
    public string MaVoucher { get; set; } = "";

    public DatVeDTO()
    {
    }

    public DatVeDTO(string maDV, string maKH, string maNV, string maChuyenBay, DateTime ngayDat, decimal tongTien, decimal giamGia, string trangThai, string maVoucher)
    {
        this.MaDV = maDV;
        this.MaKH = maKH;
        this.MaNV = maNV;
        this.MaChuyenBay = maChuyenBay;
        this.NgayDat = ngayDat;
        this.TongTien = tongTien;
        this.GiamGia = giamGia;
        this.TrangThai = trangThai;
        this.MaVoucher = maVoucher;
    }
}
