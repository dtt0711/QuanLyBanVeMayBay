namespace BanVeMayBay.DTO;

public class MayBayDTO
{
    public string MaMayBay { get; set; } = "";
    public string MaHHK { get; set; } = "";
    public string LoaiMayBay { get; set; } = "";
    public int SoGhe { get; set; }

    public MayBayDTO()
    {
    }

    public MayBayDTO(string maMayBay, string maHHK, string loaiMayBay, int soGhe)
    {
        this.MaMayBay = maMayBay;
        this.MaHHK = maHHK;
        this.LoaiMayBay = loaiMayBay;
        this.SoGhe = soGhe;
    }
}
