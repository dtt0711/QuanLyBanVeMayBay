using System.Data;
using System.Globalization;
using ClosedXML.Excel;

namespace QuanLyBanVeMayBay.BUS;

// Xuất / nhập file Excel (.xlsx) bằng thư viện ClosedXML
public static class ExcelBUS
{
    // Xuất một hoặc nhiều bảng, mỗi bảng một sheet. Dòng 1 là tiêu đề cột.
    public static void Xuat(string duongDan, params (string tenSheet, DataTable bang)[] dsBang)
    {
        using XLWorkbook wb = new XLWorkbook();
        foreach (var (tenSheet, bang) in dsBang)
        {
            IXLWorksheet ws = wb.Worksheets.Add(TenSheet(tenSheet, wb));
            for (int c = 0; c < bang.Columns.Count; c++)
                ws.Cell(1, c + 1).Value = bang.Columns[c].ColumnName;
            for (int r = 0; r < bang.Rows.Count; r++)
                for (int c = 0; c < bang.Columns.Count; c++)
                    GhiO(ws.Cell(r + 2, c + 1), bang.Rows[r][c]);

            IXLRange tieuDe = ws.Range(1, 1, 1, Math.Max(1, bang.Columns.Count));
            tieuDe.Style.Font.Bold = true;
            tieuDe.Style.Fill.BackgroundColor = XLColor.FromArgb(226, 232, 240);
            // Dòng tổng cộng (nếu có) in đậm
            if (bang.Rows.Count > 0 && bang.Rows[bang.Rows.Count - 1][0]?.ToString() == "TC")
                ws.Row(bang.Rows.Count + 1).Style.Font.Bold = true;
            ws.SheetView.FreezeRows(1);
            ws.Columns().AdjustToContents();
        }
        wb.SaveAs(duongDan);
    }

    private static void GhiO(IXLCell o, object v)
    {
        switch (v)
        {
            case decimal d:
                o.Value = (double)d;
                o.Style.NumberFormat.Format = "#,##0";
                break;
            case int i:
                o.Value = i;
                break;
            case DateTime t:
                o.Value = t;
                o.Style.DateFormat.Format = t.TimeOfDay == TimeSpan.Zero ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm";
                break;
            default:
                o.Value = v == null || v == DBNull.Value ? "" : v.ToString();   // mã, CCCD, SĐT giữ dạng chữ
                break;
        }
    }

    private static string TenSheet(string ten, XLWorkbook wb)
    {
        foreach (char c in new[] { ':', '\\', '/', '?', '*', '[', ']' })
            ten = ten.Replace(c, ' ');
        ten = ten.Length > 31 ? ten.Substring(0, 31) : ten;
        string kq = ten;
        for (int i = 2; wb.Worksheets.Contains(kq); i++)
            kq = (ten.Length > 28 ? ten.Substring(0, 28) : ten) + " " + i;
        return kq;
    }

    // Đọc sheet đầu tiên: dòng 1 là tên cột, các dòng sau là dữ liệu (mọi ô đọc dạng chữ)
    public static DataTable Doc(string duongDan)
    {
        DataTable bang = new DataTable();
        using XLWorkbook wb = new XLWorkbook(duongDan);
        IXLWorksheet ws = wb.Worksheets.First();
        IXLRow dau = ws.FirstRowUsed();
        if (dau == null)
            return bang;
        int soCot = dau.LastCellUsed().Address.ColumnNumber;
        for (int c = 1; c <= soCot; c++)
            bang.Columns.Add(ws.Cell(dau.RowNumber(), c).GetFormattedString().Trim());
        int dongCuoi = ws.LastRowUsed().RowNumber();
        for (int r = dau.RowNumber() + 1; r <= dongCuoi; r++)
        {
            DataRow dong = bang.NewRow();
            bool rong = true;
            for (int c = 1; c <= soCot; c++)
            {
                IXLCell o = ws.Cell(r, c);
                string s = o.DataType == XLDataType.Number
                    ? o.GetDouble().ToString("0.##", CultureInfo.InvariantCulture)
                    : o.GetFormattedString().Trim();
                dong[c - 1] = s;
                if (s != "")
                    rong = false;
            }
            if (!rong)
                bang.Rows.Add(dong);
        }
        return bang;
    }

    // Lấy giá trị ô theo tên cột (không phân biệt hoa thường); "" nếu không có cột đó
    public static string Lay(DataRow dong, params string[] tenCot)
    {
        foreach (string ten in tenCot)
            foreach (DataColumn c in dong.Table.Columns)
                if (string.Equals(c.ColumnName, ten, StringComparison.OrdinalIgnoreCase))
                    return dong[c]?.ToString().Trim() ?? "";
        return "";
    }

    public static bool CoCot(DataTable bang, params string[] tenCot)
    {
        foreach (string ten in tenCot)
            foreach (DataColumn c in bang.Columns)
                if (string.Equals(c.ColumnName, ten, StringComparison.OrdinalIgnoreCase))
                    return true;
        return false;
    }
}
