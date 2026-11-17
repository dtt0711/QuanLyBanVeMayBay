namespace QuanLyBanVeMayBay.DTO;

public class HangVeDTO
{
    public string MaHV { get; set; } = "";
    public string TenHV { get; set; } = "";
    public int HanhLyKg { get; set; }
    public decimal HeSoGia { get; set; }   // giá vé = giá chuyến bay x hệ số

    public HangVeDTO()
    {
    }

    public HangVeDTO(string maHV, string tenHV, int hanhLyKg, decimal heSoGia)
    {
        this.MaHV = maHV;
        this.TenHV = tenHV;
        this.HanhLyKg = hanhLyKg;
        this.HeSoGia = heSoGia;
    }
}
