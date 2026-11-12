namespace BanVeMayBay.DTO;

public class VoucherDTO
{
    public string MaVoucher { get; set; } = "";
    public string MaCTKM { get; set; } = "";
    public double GiamGia { get; set; }

    public VoucherDTO()
    {
    }

    public VoucherDTO(string maVoucher, string maCTKM, double giamGia)
    {
        this.MaVoucher = maVoucher;
        this.MaCTKM = maCTKM;
        this.GiamGia = giamGia;
    }
}
