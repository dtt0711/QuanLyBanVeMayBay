namespace BanVeMayBay.DTO;

public class NhanVienDTO
{
    public string MaNV { get; set; } = "";
    public string Ho { get; set; } = "";
    public string Ten { get; set; } = "";
    public string ChucVu { get; set; } = "";

    public NhanVienDTO()
    {
    }

    public NhanVienDTO(string maNV, string ho, string ten, string chucVu)
    {
        this.MaNV = maNV;
        this.Ho = ho;
        this.Ten = ten;
        this.ChucVu = chucVu;
    }
}
