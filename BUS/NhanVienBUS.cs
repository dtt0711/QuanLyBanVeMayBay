using System.Data;
using QuanLyBanVeMayBay.DAO;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

public class NhanVienBUS
{
    private static List<NhanVienDTO> dsnv;

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

    public static void LamMoiDS()
    {
        dsnv = null;
    }

    public bool Them(NhanVienDTO nv)
    {
        DocDSNV();
        NhanVienDAO dao = new NhanVienDAO();
        if (!dao.Them(nv))
            return false;
        dsnv.Add(nv);
        return true;
    }

    public bool Sua(NhanVienDTO nv)
    {
        DocDSNV();
        NhanVienDAO dao = new NhanVienDAO();
        if (!dao.Sua(nv))
            return false;
        foreach (NhanVienDTO n in dsnv)
            if (n.MaNV.CompareTo(nv.MaNV) == 0)
            {
                n.Ho = nv.Ho;
                n.Ten = nv.Ten;
                n.ChucVu = nv.ChucVu;
            }
        return true;
    }

    public bool Xoa(string ma)
    {
        DocDSNV();
        NhanVienDAO dao = new NhanVienDAO();
        if (!dao.Xoa(ma))
            return false;
        dsnv.Remove(timKiem(ma));
        return true;
    }

    // Kiểm tra dữ liệu trước khi ghi. Trả về "" nếu hợp lệ, ngược lại trả về lỗi.
    public string KiemTra(NhanVienDTO x)
    {
        if (x.Ho.Trim() == "" || x.Ten.Trim() == "")
            return "Nhập họ và tên nhân viên.";
        return "";
    }

    // Nhập nhân viên từ Excel: mã chưa có thì thêm, mã đã có thì cập nhật; dòng lỗi bỏ qua và báo lại
    public string NhapExcel(DataTable bang)
    {
        if (!ExcelBUS.CoCot(bang, "Mã NV") || !ExcelBUS.CoCot(bang, "Họ") || !ExcelBUS.CoCot(bang, "Tên"))
            return "File cần có các cột: Mã NV, Họ, Tên, Chức vụ.";
        int them = 0, sua = 0;
        List<string> loi = new List<string>();
        for (int i = 0; i < bang.Rows.Count; i++)
        {
            DataRow r = bang.Rows[i];
            string ma = ExcelBUS.Lay(r, "Mã NV");
            if (ma == "")
            {
                loi.Add("Dòng " + (i + 2) + ": thiếu mã nhân viên.");
                continue;
            }
            NhanVienDTO nv = new NhanVienDTO(ma, ExcelBUS.Lay(r, "Họ"), ExcelBUS.Lay(r, "Tên"), ExcelBUS.Lay(r, "Chức vụ"));
            string l = KiemTra(nv);
            if (l != "")
            {
                loi.Add("Dòng " + (i + 2) + " (" + ma + "): " + l);
                continue;
            }
            if (!TrungMa(ma))
            {
                if (Them(nv)) them++;
                else loi.Add("Dòng " + (i + 2) + " (" + ma + "): không ghi được vào CSDL.");
            }
            else
            {
                if (Sua(nv)) sua++;
                else loi.Add("Dòng " + (i + 2) + " (" + ma + "): không cập nhật được.");
            }
        }
        return KhachHangBUS.KetQuaNhap(them, sua, loi);
    }

    // Tìm kiếm thường: giữ lại các dòng có ít nhất một field chứa từ khóa
    public List<NhanVienDTO> TimKiem(string tuKhoa)
    {
        List<NhanVienDTO> kq = new List<NhanVienDTO>();
        foreach (NhanVienDTO x in DocDSNV())
            if (TimKiemBUS.Chua(x.MaNV, tuKhoa)
                || TimKiemBUS.Chua(x.Ho + " " + x.Ten, tuKhoa)
                || TimKiemBUS.Chua(x.Ho, tuKhoa)
                || TimKiemBUS.Chua(x.Ten, tuKhoa)
                || TimKiemBUS.Chua(x.ChucVu, tuKhoa))
                kq.Add(x);
        return kq;
    }

    // Tìm kiếm nâng cao: lọc theo các điều kiện and, or, not, >, ≥, <, ≤, <>
    public List<NhanVienDTO> TimNangCao(List<DieuKienDTO> dsDK)
    {
        return TimKiemBUS.Loc(DocDSNV(), dsDK);
    }

    public bool TrungMa(string ma)
    {
        DocDSNV();
        return timKiem(ma) != null;
    }

    public NhanVienDTO timKiem(string ma)
    {
        foreach (NhanVienDTO n in DocDSNV())
            if (n.MaNV.CompareTo(ma) == 0)
                return n;
        return null;
    }
}
