namespace QuanLyBanVeMayBay.DTO;

public class ChuyenBayDTO
{
    public string MaChuyenBay { get; set; } = "";
    public string MaHHK { get; set; } = "";
    public string MaSanBayDi { get; set; } = "";
    public string MaSanBayDen { get; set; } = "";
    public string MaMayBay { get; set; } = "";
    public DateTime NgayKhoiHanh { get; set; }
    public DateTime NgayDen { get; set; }
    public int ThoiGianBay { get; set; }
    public string ThoiGianDen { get; set; } = "";
    public int TongSoGhe { get; set; }
    public int SoVeDaBan { get; set; }
    public decimal GiaVe { get; set; }
    public string TrangThai { get; set; } = "";

    public ChuyenBayDTO()
    {
    }

    public ChuyenBayDTO(string maChuyenBay, string maHHK, string maSanBayDi, string maSanBayDen, string maMayBay, DateTime ngayKhoiHanh, DateTime ngayDen, int thoiGianBay, string thoiGianDen, int tongSoGhe, int soVeDaBan, decimal giaVe, string trangThai)
    {
        this.MaChuyenBay = maChuyenBay;
        this.MaHHK = maHHK;
        this.MaSanBayDi = maSanBayDi;
        this.MaSanBayDen = maSanBayDen;
        this.MaMayBay = maMayBay;
        this.NgayKhoiHanh = ngayKhoiHanh;
        this.NgayDen = ngayDen;
        this.ThoiGianBay = thoiGianBay;
        this.ThoiGianDen = thoiGianDen;
        this.TongSoGhe = tongSoGhe;
        this.SoVeDaBan = soVeDaBan;
        this.GiaVe = giaVe;
        this.TrangThai = trangThai;
    }
}
