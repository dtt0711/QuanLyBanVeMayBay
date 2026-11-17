namespace QuanLyBanVeMayBay.DTO;

public class ThamSoDTO
{
    public string MaThamSo { get; set; } = "";
    public string TenThamSo { get; set; } = "";
    public decimal GiaTri { get; set; }
    public string DonVi { get; set; } = "";

    public ThamSoDTO()
    {
    }

    public ThamSoDTO(string maThamSo, string tenThamSo, decimal giaTri, string donVi)
    {
        this.MaThamSo = maThamSo;
        this.TenThamSo = tenThamSo;
        this.GiaTri = giaTri;
        this.DonVi = donVi;
    }
}
