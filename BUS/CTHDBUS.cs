using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class CTHDBUS
{
    private HoaDonDAO hdDao = new HoaDonDAO();
    private ChuyenBayDAO cbDao = new ChuyenBayDAO();

    public List<CTHDDTO> DocTheoHD(string maHD)
    {
        CTHDDAO dao = new CTHDDAO();
        return dao.DocTheoHD(maHD);
    }

    public List<CTHDDTO> DocTheoCB(string maCB)
    {
        CTHDDAO dao = new CTHDDAO();
        return dao.DocTheoCB(maCB);
    }

    // Đơn giá lấy từ giá vé của chuyến bay, thành tiền = số lượng * đơn giá
    public void Them(string maHD, string maCB, int soLuong)
    {
        CTHDDAO dao = new CTHDDAO();
        if (string.IsNullOrWhiteSpace(maCB))
            throw new Exception("Chưa chọn chuyến bay!");
        if (soLuong <= 0)
            throw new Exception("Số lượng phải lớn hơn 0!");
        if (!hdDao.DocDSHD().Any(t => t.MaHD == maHD))
            throw new Exception("Hóa đơn không tồn tại!");
        ChuyenBayDTO? chuyenBay = cbDao.DocDSCB().FirstOrDefault(t => t.MaCB == maCB);
        if (chuyenBay == null)
            throw new Exception("Chuyến bay không tồn tại!");
        if (dao.DocTheoHD(maHD).Any(t => t.MaCB == maCB))
            throw new Exception("Chuyến bay này đã có trong hóa đơn!");
        if (soLuong > chuyenBay.SoGheTrong)
            throw new Exception("Chuyến bay chỉ còn " + chuyenBay.SoGheTrong + " ghế trống!");

        CTHDDTO cthd = new CTHDDTO(maHD, maCB, soLuong, chuyenBay.GiaVe, soLuong * chuyenBay.GiaVe);
        dao.Them(cthd);
        // ghế trống và tổng tiền đã đổi trong CSDL nên phải đọc lại
        ChuyenBayBUS.LamMoiDS();
        HoaDonBUS.LamMoiDS();
    }

    public void Xoa(string maHD, string maCB)
    {
        CTHDDAO dao = new CTHDDAO();
        if (string.IsNullOrWhiteSpace(maHD) || string.IsNullOrWhiteSpace(maCB))
            throw new Exception("Chưa chọn chi tiết cần xóa!");
        dao.Xoa(maHD, maCB);
        ChuyenBayBUS.LamMoiDS();
        HoaDonBUS.LamMoiDS();
    }
}
