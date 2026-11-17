using System.Collections;
using System.Globalization;
using System.Reflection;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

// Các hàm tìm kiếm dùng chung cho mọi BUS, đều chạy trên danh sách (ArrayList) đã đọc lên
public static class TimKiemBUS
{
    public static readonly string[] PhepToan = { "=", "<>", ">", "≥", "<", "≤", "chứa" };

    // Tìm kiếm thường: giaTri có chứa tuKhoa không (không phân biệt hoa thường)
    public static bool Chua(string giaTri, string tuKhoa)
    {
        return (giaTri ?? "").ToLower().Contains(tuKhoa.Trim().ToLower());
    }

    // Tìm theo từ khóa trên mọi field của một danh sách bất kỳ (dùng ở form chọn "…")
    public static List<object> TheoTuKhoa(IList ds, string tuKhoa)
    {
        List<object> kq = new List<object>();
        foreach (object x in ds)
        {
            foreach (PropertyInfo p in x.GetType().GetProperties())
                if (Chua(ChuoiHienThi(p.GetValue(x)), tuKhoa))
                {
                    kq.Add(x);
                    break;
                }
        }
        return kq;
    }

    public static string ChuoiHienThi(object v)
    {
        if (v is DateTime d)
            return d.ToString("dd/MM/yyyy");
        if (v is decimal m)
            return m.ToString("N0");
        return v?.ToString() ?? "";
    }

    // Các field được phép lọc của một DTO (bỏ mật khẩu)
    public static List<string> DSCot(Type kieu)
    {
        List<string> cot = new List<string>();
        foreach (PropertyInfo p in kieu.GetProperties())
            if (p.Name != "MatKhau")
                cot.Add(p.Name);
        return cot;
    }

    // Kiểm tra giá trị nhập có đúng kiểu của field không. Trả về "" nếu hợp lệ.
    public static string KiemTraDieuKien(Type kieu, DieuKienDTO dk)
    {
        PropertyInfo p = kieu.GetProperty(dk.TenCot);
        if (p == null)
            return "Chưa chọn field.";
        if (dk.PhepToan == "chứa")
            return "";
        Type t = p.PropertyType;
        if (t == typeof(DateTime) && !DateTime.TryParseExact(dk.GiaTri, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            return "Field " + dk.TenCot + " là ngày, nhập theo dạng dd/MM/yyyy.";
        if ((t == typeof(int) || t == typeof(decimal) || t == typeof(double)) && !decimal.TryParse(dk.GiaTri, out _))
            return "Field " + dk.TenCot + " là số, hãy nhập số.";
        return "";
    }

    // Tìm kiếm nâng cao: giữ lại các phần tử thỏa danh sách điều kiện.
    // AND được ưu tiên trước OR, giống SQL: A AND B OR C AND D = (A AND B) OR (C AND D)
    public static List<T> Loc<T>(List<T> ds, List<DieuKienDTO> dsDK)
    {
        List<T> kq = new List<T>();
        foreach (T x in ds)
            if (ThoaMan(x, dsDK))
                kq.Add(x);
        return kq;
    }

    private static bool ThoaMan(object x, List<DieuKienDTO> dsDK)
    {
        if (dsDK.Count == 0)
            return true;
        bool ketQua = false;   // kết quả các nhóm đã xong, nối bằng OR
        bool nhom = true;      // kết quả nhóm đang xét, nối bằng AND
        for (int i = 0; i < dsDK.Count; i++)
        {
            if (i > 0 && dsDK[i].Noi == "OR")
            {
                ketQua = ketQua || nhom;
                nhom = true;
            }
            nhom = nhom && KiemTra(x, dsDK[i]);
        }
        return ketQua || nhom;
    }

    private static bool KiemTra(object x, DieuKienDTO dk)
    {
        object v = x.GetType().GetProperty(dk.TenCot).GetValue(x);
        bool dung;
        if (dk.PhepToan == "chứa")
            dung = Chua(ChuoiHienThi(v), dk.GiaTri);
        else
        {
            int ss = SoSanh(v, dk.GiaTri);
            switch (dk.PhepToan)
            {
                case "=": dung = ss == 0; break;
                case "<>": dung = ss != 0; break;
                case ">": dung = ss > 0; break;
                case "≥": dung = ss >= 0; break;
                case "<": dung = ss < 0; break;
                case "≤": dung = ss <= 0; break;
                default: dung = false; break;
            }
        }
        return dk.Khong ? !dung : dung;
    }

    // So sánh giá trị của field với giá trị người dùng nhập: âm (<), 0 (=), dương (>)
    private static int SoSanh(object v, string giaTri)
    {
        if (v is DateTime d)
            return d.Date.CompareTo(DateTime.ParseExact(giaTri, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        if (v is int || v is decimal || v is double)
            return Convert.ToDecimal(v).CompareTo(decimal.Parse(giaTri));
        return string.Compare(v?.ToString() ?? "", giaTri.Trim(), StringComparison.CurrentCultureIgnoreCase);
    }
}
