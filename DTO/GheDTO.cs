namespace QuanLyBanVeMayBay.DTO;

public class GheDTO
{
    public string MaGhe { get; set; } = "";
    public string MaMayBay { get; set; } = "";
    public string MaHV { get; set; } = "";

    public GheDTO()
    {
    }

    public GheDTO(string maGhe, string maMayBay, string maHV)
    {
        this.MaGhe = maGhe;
        this.MaMayBay = maMayBay;
        this.MaHV = maHV;
    }
}
