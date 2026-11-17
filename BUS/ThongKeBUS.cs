using System.Data;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.BUS;

// Thống kê tiền thu (hóa đơn đã thanh toán). Mọi phép tính chạy trên các danh sách
// (ArrayList) đã đọc lên từ BUS, không viết câu SQL thống kê.
//
// Tiền theo chuyến bay (sản phẩm) lấy từ chi tiết hóa đơn. Vì tổng tiền hóa đơn đã trừ giảm giá,
// phần giảm giá được chia cho các vé theo tỷ lệ giá vé, để cộng các vé lại đúng bằng tổng tiền
// hóa đơn. Nhờ vậy mọi bảng thống kê đều có cùng tổng cộng.
public class ThongKeBUS
{
    public const string DaThanhToan = "Đã thanh toán";
    public static readonly string[] DSDoiTuong = { "Nhân viên", "Khách hàng", "Chuyến bay" };
    public static readonly string[] DSKy = { "Ngày", "Tháng", "Quý", "Năm" };

    // Một dòng tiền: một vé trong một hóa đơn, kèm tiền sau khi đã chia giảm giá
    private class DongTien
    {
        public HoaDonDTO HoaDon;
        public string MaChuyenBay;
        public decimal Tien;
    }

    // ---------- Dữ liệu nguồn ----------

    // Hóa đơn đã thanh toán, lập trong khoảng [tu, den] (tính theo ngày)
    public List<HoaDonDTO> HoaDonDaThu(DateTime tu, DateTime den)
    {
        List<HoaDonDTO> kq = new List<HoaDonDTO>();
        foreach (HoaDonDTO hd in new HoaDonBUS().DocDSHD())
            if (hd.TrangThai == DaThanhToan && hd.NgayLap.Date >= tu.Date && hd.NgayLap.Date <= den.Date)
                kq.Add(hd);
        return kq;
    }

    // Chia tiền mỗi hóa đơn cho các vé: tiền vé = thành tiền x (tổng tiền HĐ / tổng thành tiền HĐ);
    // vé cuối của hóa đơn nhận phần lẻ để tổng khớp đúng tổng tiền hóa đơn
    private List<DongTien> DSDongTien(DateTime tu, DateTime den)
    {
        List<CTHDDTO> dsCT = new CTHDBUS().DocDSCTHD();
        Dictionary<string, string> chuyenCuaVe = new Dictionary<string, string>();
        foreach (VeDTO v in new VeBUS().DocDSVe())
            chuyenCuaVe[v.MaVe] = v.MaChuyenBay;

        List<DongTien> kq = new List<DongTien>();
        foreach (HoaDonDTO hd in HoaDonDaThu(tu, den))
        {
            List<CTHDDTO> ct = dsCT.FindAll(c => c.MaHD == hd.MaHD);
            decimal tongThanhTien = 0;
            foreach (CTHDDTO c in ct)
                tongThanhTien += c.ThanhTien;
            if (tongThanhTien == 0)
                continue;
            decimal daChia = 0;
            for (int i = 0; i < ct.Count; i++)
            {
                decimal tien = i == ct.Count - 1
                    ? hd.TongTien - daChia
                    : Math.Round(ct[i].ThanhTien * hd.TongTien / tongThanhTien, 0);
                daChia += tien;
                string maCB = chuyenCuaVe.ContainsKey(ct[i].MaVe) ? chuyenCuaVe[ct[i].MaVe] : "";
                kq.Add(new DongTien { HoaDon = hd, MaChuyenBay = maCB, Tien = tien });
            }
        }
        return kq;
    }

    public List<int> DSNam()
    {
        List<int> nam = new List<int>();
        foreach (HoaDonDTO hd in new HoaDonBUS().DocDSHD())
            if (!nam.Contains(hd.NgayLap.Year))
                nam.Add(hd.NgayLap.Year);
        nam.Sort();
        return nam;
    }

    // ---------- Tên hiển thị ----------

    private Dictionary<string, string> TenDoiTuong(string doiTuong)
    {
        Dictionary<string, string> ten = new Dictionary<string, string>();
        if (doiTuong == "Nhân viên")
            foreach (NhanVienDTO nv in new NhanVienBUS().DocDSNV())
                ten[nv.MaNV] = nv.MaNV + " - " + nv.Ho + " " + nv.Ten;
        else if (doiTuong == "Khách hàng")
            foreach (KhachHangDTO kh in new KhachHangBUS().DocDSKH())
                ten[kh.MaKH] = kh.MaKH + " - " + kh.HoTen;
        else
            foreach (ChuyenBayDTO cb in new ChuyenBayBUS().DocDSCB())
                ten[cb.MaChuyenBay] = cb.MaChuyenBay + " - " + cb.MaSanBayDi + "→" + cb.MaSanBayDen + " " + cb.NgayKhoiHanh.ToString("dd/MM/yyyy");
        return ten;
    }

    private string MaDoiTuong(DongTien d, string doiTuong)
    {
        if (doiTuong == "Nhân viên")
            return d.HoaDon.MaNV == "" ? "(không có NV)" : d.HoaDon.MaNV;
        if (doiTuong == "Khách hàng")
            return d.HoaDon.MaKH;
        return d.MaChuyenBay;
    }

    // Nhãn của kỳ và khóa để sắp xếp theo thời gian
    private static string NhanKy(DateTime ngay, string ky)
    {
        switch (ky)
        {
            case "Ngày": return ngay.ToString("dd/MM/yyyy");
            case "Tháng": return ngay.ToString("MM/yyyy");
            case "Quý": return "Q" + ((ngay.Month - 1) / 3 + 1) + "/" + ngay.Year;
            default: return ngay.Year.ToString();
        }
    }

    private static int KhoaKy(DateTime ngay, string ky)
    {
        switch (ky)
        {
            case "Ngày": return ngay.Year * 10000 + ngay.Month * 100 + ngay.Day;
            case "Tháng": return ngay.Year * 100 + ngay.Month;
            case "Quý": return ngay.Year * 10 + (ngay.Month - 1) / 3 + 1;
            default: return ngay.Year;
        }
    }

    // ---------- Bảng 2 chiều có dòng và cột tổng cộng ----------

    // dsDong: (các khóa của dòng, tên cột, số tiền). Kết quả: các cột khóa + các cột giá trị + cột TC,
    // dòng cuối là TC. dongCoDinh: các dòng luôn hiện kể cả khi bằng 0 (có thể null).
    private DataTable BangHaiChieu(string[] tenCotKhoa, List<(string[] khoa, string cot, decimal tien)> dsDong,
        List<string> dsCot, List<string[]> dongCoDinh)
    {
        DataTable bang = new DataTable();
        foreach (string c in tenCotKhoa)
            bang.Columns.Add(c, typeof(string));
        foreach (string c in dsCot)
            bang.Columns.Add(c, typeof(decimal));
        bang.Columns.Add("TC", typeof(decimal));

        Dictionary<string, DataRow> dong = new Dictionary<string, DataRow>();
        List<string> thuTu = new List<string>();
        DataRow TaoDong(string[] khoa)
        {
            string k = string.Join("|", khoa);
            if (!dong.ContainsKey(k))
            {
                DataRow r = bang.NewRow();
                for (int i = 0; i < khoa.Length; i++)
                    r[i] = khoa[i];
                foreach (string c in dsCot)
                    r[c] = 0m;
                r["TC"] = 0m;
                dong[k] = r;
                thuTu.Add(k);
            }
            return dong[k];
        }
        if (dongCoDinh != null)
            foreach (string[] k in dongCoDinh)
                TaoDong(k);
        foreach (var d in dsDong)
        {
            DataRow r = TaoDong(d.khoa);
            r[d.cot] = (decimal)r[d.cot] + d.tien;
            r["TC"] = (decimal)r["TC"] + d.tien;
        }
        thuTu.Sort(StringComparer.Ordinal);
        foreach (string k in thuTu)
            bang.Rows.Add(dong[k]);

        DataRow tc = bang.NewRow();
        tc[0] = "TC";
        foreach (string c in dsCot)
            tc[c] = bang.AsEnumerable().Sum(r => r.Field<decimal>(c));
        tc["TC"] = bang.AsEnumerable().Sum(r => r.Field<decimal>("TC"));
        bang.Rows.Add(tc);
        return bang;
    }

    // ---------- a. Tổng thu ----------

    // a.i Tổng thu từ ngày đến ngày
    public decimal TongThu(DateTime tu, DateTime den)
    {
        decimal tong = 0;
        foreach (HoaDonDTO hd in HoaDonDaThu(tu, den))
            tong += hd.TongTien;
        return tong;
    }

    // a.ii Tổng thu theo ngày / tháng / quý / năm trong khoảng [tu, den]
    public DataTable TongThuTheoKy(DateTime tu, DateTime den, string ky)
    {
        DataTable bang = new DataTable();
        bang.Columns.Add("Kỳ", typeof(string));
        bang.Columns.Add("Số hóa đơn", typeof(int));
        bang.Columns.Add("Tổng tiền", typeof(decimal));
        SortedDictionary<int, DataRow> theoKy = new SortedDictionary<int, DataRow>();
        foreach (HoaDonDTO hd in HoaDonDaThu(tu, den))
        {
            int k = KhoaKy(hd.NgayLap, ky);
            if (!theoKy.ContainsKey(k))
            {
                DataRow r = bang.NewRow();
                r[0] = NhanKy(hd.NgayLap, ky);
                r[1] = 0;
                r[2] = 0m;
                theoKy[k] = r;
            }
            theoKy[k][1] = (int)theoKy[k][1] + 1;
            theoKy[k][2] = (decimal)theoKy[k][2] + hd.TongTien;
        }
        int soHD = 0;
        decimal tong = 0;
        foreach (DataRow r in theoKy.Values)
        {
            bang.Rows.Add(r);
            soHD += (int)r[1];
            tong += (decimal)r[2];
        }
        bang.Rows.Add("TC", soHD, tong);
        return bang;
    }

    // ---------- Bảng theo quý: nhân viên / khách hàng / chuyến bay ----------

    public DataTable TheoQuy(string doiTuong, int nam)
    {
        Dictionary<string, string> ten = TenDoiTuong(doiTuong);
        List<(string[], string, decimal)> ds = new List<(string[], string, decimal)>();
        foreach (DongTien d in DSDongTien(new DateTime(nam, 1, 1), new DateTime(nam, 12, 31)))
        {
            string ma = MaDoiTuong(d, doiTuong);
            string quy = "Q" + ((d.HoaDon.NgayLap.Month - 1) / 3 + 1);
            ds.Add((new[] { ten.ContainsKey(ma) ? ten[ma] : ma }, quy, d.Tien));
        }
        List<string[]> coDinh = ten.Values.Select(t => new[] { t }).ToList();
        return BangHaiChieu(new[] { doiTuong }, ds, new List<string> { "Q1", "Q2", "Q3", "Q4" }, coDinh);
    }

    // ---------- b.i Theo đối tượng và theo kỳ, trong khoảng từ ngày đến ngày ----------

    public DataTable TheoDoiTuongVaKy(string doiTuong, string ky, DateTime tu, DateTime den)
    {
        Dictionary<string, string> ten = TenDoiTuong(doiTuong);
        List<DongTien> dsTien = DSDongTien(tu, den);
        SortedDictionary<int, string> cot = new SortedDictionary<int, string>();
        List<(string[], string, decimal)> ds = new List<(string[], string, decimal)>();
        foreach (DongTien d in dsTien)
        {
            string ma = MaDoiTuong(d, doiTuong);
            string nhan = NhanKy(d.HoaDon.NgayLap, ky);
            cot[KhoaKy(d.HoaDon.NgayLap, ky)] = nhan;
            ds.Add((new[] { ten.ContainsKey(ma) ? ten[ma] : ma }, nhan, d.Tien));
        }
        return BangHaiChieu(new[] { doiTuong }, ds, cot.Values.ToList(), null);
    }

    // ---------- b.ii Khách hàng và chuyến bay (2 khóa) ----------

    public DataTable KhachHangVaChuyenBay(DateTime tu, DateTime den)
    {
        Dictionary<string, string> tenKH = TenDoiTuong("Khách hàng");
        List<(string[], string, decimal)> ds = new List<(string[], string, decimal)>();
        SortedSet<string> cot = new SortedSet<string>(StringComparer.Ordinal);
        foreach (DongTien d in DSDongTien(tu, den))
        {
            cot.Add(d.MaChuyenBay);
            string kh = d.HoaDon.MaKH;
            ds.Add((new[] { tenKH.ContainsKey(kh) ? tenKH[kh] : kh }, d.MaChuyenBay, d.Tien));
        }
        return BangHaiChieu(new[] { "Khách hàng" }, ds, cot.ToList(), null);
    }

    // ---------- b.iii Nhân viên và chuyến bay và năm (3 khóa) ----------

    public DataTable NhanVienChuyenBayNam(DateTime tu, DateTime den)
    {
        Dictionary<string, string> tenNV = TenDoiTuong("Nhân viên");
        Dictionary<string, string> tenCB = TenDoiTuong("Chuyến bay");
        List<(string[], string, decimal)> ds = new List<(string[], string, decimal)>();
        SortedSet<string> cot = new SortedSet<string>(StringComparer.Ordinal);
        foreach (DongTien d in DSDongTien(tu, den))
        {
            string nam = d.HoaDon.NgayLap.Year.ToString();
            cot.Add(nam);
            string nv = MaDoiTuong(d, "Nhân viên");
            ds.Add((new[] { tenNV.ContainsKey(nv) ? tenNV[nv] : nv, tenCB.ContainsKey(d.MaChuyenBay) ? tenCB[d.MaChuyenBay] : d.MaChuyenBay }, nam, d.Tien));
        }
        return BangHaiChieu(new[] { "Nhân viên", "Chuyến bay" }, ds, cot.ToList(), null);
    }
}
