namespace BanVeMayBay.DTO;

public class PhieuChiDTO
{
    public string MaPC { get; set; } = "";
    public DateTime NgayChi { get; set; }
    public string MaCB { get; set; } = "";
    public string NoiDung { get; set; } = "";
    public decimal SoTien { get; set; }

    public PhieuChiDTO()
    {
    }

    public PhieuChiDTO(string maPC, DateTime ngayChi, string maCB, string noiDung, decimal soTien)
    {
        this.MaPC = maPC;
        this.NgayChi = ngayChi;
        this.MaCB = maCB;
        this.NoiDung = noiDung;
        this.SoTien = soTien;
    }
}
