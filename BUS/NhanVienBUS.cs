using BanVeMayBay.DAO;
using BanVeMayBay.DTO;

namespace BanVeMayBay.BUS;

public class NhanVienBUS
{
    private static List<NhanVienDTO>? dsnv;
    private HoaDonDAO hoaDonDao = new HoaDonDAO();

    public List<NhanVienDTO> DocDSNV()
    {
        NhanVienDAO dao = new NhanVienDAO();
        if (dsnv == null || dsnv.Count == 0)
        {
            dsnv = new List<NhanVienDTO>();
            dsnv = dao.DocDSNV();
        }
        return dsnv;
    }

    // xóa danh sách đã lưu để lần đọc sau lấy lại dữ liệu mới từ CSDL
    public static void LamMoiDS()
    {
        dsnv = null;
    }

    private void KiemTra(NhanVienDTO nv)
    {
        if (string.IsNullOrWhiteSpace(nv.MaNV))
            throw new Exception("Mã NV không được để trống!");
        if (string.IsNullOrWhiteSpace(nv.Ho))
            throw new Exception("Họ không được để trống!");
        if (string.IsNullOrWhiteSpace(nv.Ten))
            throw new Exception("Tên không được để trống!");
        if (string.IsNullOrWhiteSpace(nv.ChucVu))
            throw new Exception("Chức vụ không được để trống!");
    }

    public void Them(NhanVienDTO nv)
    {
        NhanVienDAO dao = new NhanVienDAO();
        KiemTra(nv);
        if (dao.DocDSNV().Any(t => t.MaNV == nv.MaNV))
            throw new Exception("Mã NV đã tồn tại!");
        dao.Them(nv);
        LamMoiDS();
    }

    public void Sua(NhanVienDTO nv)
    {
        NhanVienDAO dao = new NhanVienDAO();
        KiemTra(nv);
        if (!dao.DocDSNV().Any(t => t.MaNV == nv.MaNV))
            throw new Exception("Không tìm thấy Mã NV cần sửa!");
        dao.Sua(nv);
        LamMoiDS();
    }

    public void Xoa(string ma)
    {
        NhanVienDAO dao = new NhanVienDAO();
        if (string.IsNullOrWhiteSpace(ma))
            throw new Exception("Chưa chọn Mã NV cần xóa!");
        if (hoaDonDao.DocTheoNV(ma).Count > 0)
            throw new Exception("Nhân viên đã lập hóa đơn, không thể xóa!");
        dao.Xoa(ma);
        LamMoiDS();
    }
}
