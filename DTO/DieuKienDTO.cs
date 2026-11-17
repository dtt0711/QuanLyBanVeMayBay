namespace QuanLyBanVeMayBay.DTO;

// Một điều kiện của tìm kiếm nâng cao, ví dụ: AND  NOT  TongTien  >=  1000000
public class DieuKienDTO
{
    public string Noi { get; set; } = "AND";      // AND / OR: cách nối với điều kiện đứng trước
    public bool Khong { get; set; }                 // NOT: phủ định kết quả của điều kiện
    public string TenCot { get; set; } = "";        // tên field trong DTO
    public string PhepToan { get; set; } = "=";     // =, <>, >, ≥, <, ≤, chứa
    public string GiaTri { get; set; } = "";

    public DieuKienDTO()
    {
    }

    public DieuKienDTO(string noi, bool khong, string tenCot, string phepToan, string giaTri)
    {
        this.Noi = noi;
        this.Khong = khong;
        this.TenCot = tenCot;
        this.PhepToan = phepToan;
        this.GiaTri = giaTri;
    }
}
