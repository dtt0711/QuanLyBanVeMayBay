namespace BanVeMayBay.DTO;

public class ChuyenBayDTO
{
    public string MaCB { get; set; } = "";
    public string MaMayBay { get; set; } = "";
    public string TuyenBay { get; set; } = "";
    public DateTime NgayBay { get; set; }
    public decimal GiaVe { get; set; }
    public int SoGheTrong { get; set; }

    public ChuyenBayDTO()
    {
    }

    public ChuyenBayDTO(string maCB, string maMayBay, string tuyenBay, DateTime ngayBay, decimal giaVe, int soGheTrong)
    {
        this.MaCB = maCB;
        this.MaMayBay = maMayBay;
        this.TuyenBay = tuyenBay;
        this.NgayBay = ngayBay;
        this.GiaVe = giaVe;
        this.SoGheTrong = soGheTrong;
    }
}
