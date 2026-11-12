namespace BanVeMayBay.DTO;

public class HangHangKhongDTO
{
    public string MaHHK { get; set; } = "";
    public string TenHang { get; set; } = "";

    public HangHangKhongDTO()
    {
    }

    public HangHangKhongDTO(string maHHK, string tenHang)
    {
        this.MaHHK = maHHK;
        this.TenHang = tenHang;
    }
}
