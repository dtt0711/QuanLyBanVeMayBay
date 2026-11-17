namespace QuanLyBanVeMayBay.DTO;

public class SanBayDTO
{
    public string MaSanBay { get; set; } = "";
    public string Ten { get; set; } = "";
    public string LoaiSanBay { get; set; } = "";

    public SanBayDTO()
    {
    }

    public SanBayDTO(string maSanBay, string ten, string loaiSanBay)
    {
        this.MaSanBay = maSanBay;
        this.Ten = ten;
        this.LoaiSanBay = loaiSanBay;
    }
}
