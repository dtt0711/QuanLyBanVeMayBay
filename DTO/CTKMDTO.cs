namespace BanVeMayBay.DTO;

public class CTKMDTO
{
    public string MaCTKM { get; set; } = "";
    public string TenCTKM { get; set; } = "";
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }

    public CTKMDTO()
    {
    }

    public CTKMDTO(string maCTKM, string tenCTKM, DateTime ngayBatDau, DateTime ngayKetThuc)
    {
        this.MaCTKM = maCTKM;
        this.TenCTKM = tenCTKM;
        this.NgayBatDau = ngayBatDau;
        this.NgayKetThuc = ngayKetThuc;
    }
}
