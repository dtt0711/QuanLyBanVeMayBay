using System.Data;
using QuanLyBanVeMayBay.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuanLyBanVeMayBay.BUS;

// Xuất báo cáo PDF bằng thư viện QuestPDF (giấy phép Community, miễn phí cho sinh viên)
public static class PdfBUS
{
    private const string Font = "Arial";   // font có sẵn trên Windows, hiển thị đủ tiếng Việt

    static PdfBUS()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        // Từ QuestPDF 2026.9 font hệ thống không còn tự dùng: nạp font Arial (thường, đậm, nghiêng) của Windows
        string thuMucFont = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        bool coFont = false;
        foreach (string tep in new[] { "arial.ttf", "arialbd.ttf", "ariali.ttf", "arialbi.ttf" })
        {
            string duongDan = Path.Combine(thuMucFont, tep);
            if (File.Exists(duongDan))
            {
                QuestPDF.Drawing.FontManager.RegisterFontFromFile(duongDan);
                coFont = true;
            }
        }
        if (!coFont)
            QuestPDF.Settings.UseSystemFonts = true;
    }

    private static IContainer OTieuDe(IContainer o) =>
        o.Background(Colors.Grey.Lighten3).Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(4);

    private static IContainer ODuLieu(IContainer o) =>
        o.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten1).Padding(4);

    private static string ChuoiO(object v)
    {
        if (v is decimal d) return d.ToString("N0");
        if (v is int i) return i.ToString("N0");
        if (v is DateTime t) return t.ToString(t.TimeOfDay == TimeSpan.Zero ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm");
        return v == null || v == DBNull.Value ? "" : v.ToString();
    }

    // Báo cáo dạng bảng (dùng cho thống kê): mỗi DataTable một bảng, dòng TC in đậm
    public static void XuatBang(string duongDan, string tieuDe, string moTa, params (string ten, DataTable bang)[] dsBang)
    {
        Document.Create(tl =>
        {
            tl.Page(trang =>
            {
                trang.Size(PageSizes.A4.Landscape());
                trang.Margin(28);
                trang.DefaultTextStyle(x => x.FontFamily(Font).FontSize(9));
                trang.Header().Column(c =>
                {
                    c.Item().Text("HỆ THỐNG BÁN VÉ MÁY BAY").FontSize(9).FontColor(Colors.Grey.Darken1);
                    c.Item().Text(tieuDe).FontSize(16).Bold();
                    c.Item().Text(moTa).FontSize(9);
                    c.Item().PaddingBottom(6);
                });
                trang.Content().Column(c =>
                {
                    c.Spacing(14);
                    foreach (var (ten, bang) in dsBang)
                        c.Item().Column(k =>
                        {
                            k.Item().PaddingBottom(4).Text(ten).Bold().FontSize(11);
                            k.Item().Table(t =>
                            {
                                t.ColumnsDefinition(cd =>
                                {
                                    foreach (DataColumn col in bang.Columns)
                                    {
                                        if (col.DataType == typeof(string)) cd.RelativeColumn(2.2f);
                                        else cd.RelativeColumn(1);
                                    }
                                });
                                t.Header(h =>
                                {
                                    foreach (DataColumn col in bang.Columns)
                                        h.Cell().Element(OTieuDe).Text(col.ColumnName).Bold();
                                });
                                for (int r = 0; r < bang.Rows.Count; r++)
                                {
                                    bool tc = r == bang.Rows.Count - 1 && bang.Rows[r][0]?.ToString() == "TC";
                                    foreach (DataColumn col in bang.Columns)
                                    {
                                        IContainer o = t.Cell();
                                        if (tc) o = o.Background(Colors.Grey.Lighten3);
                                        o = o.Element(ODuLieu);
                                        if (col.DataType != typeof(string)) o = o.AlignRight();
                                        var chu = o.Text(ChuoiO(bang.Rows[r][col]));
                                        if (tc || col.ColumnName == "TC") chu.Bold();
                                    }
                                }
                            });
                        });
                });
                trang.Footer().Row(r =>
                {
                    r.RelativeItem().Text("Xuất lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(8);
                    r.RelativeItem().AlignRight().Text(x =>
                    {
                        x.Span("Trang ").FontSize(8);
                        x.CurrentPageNumber().FontSize(8);
                        x.Span(" / ").FontSize(8);
                        x.TotalPages().FontSize(8);
                    });
                });
            });
        }).GeneratePdf(duongDan);
    }

    // Hóa đơn bán vé: thông tin hóa đơn, khách hàng, chuyến bay, danh sách vé và tổng tiền
    public static void XuatHoaDon(string maHD, string duongDan)
    {
        HoaDonDTO hd = new HoaDonBUS().timKiem(maHD) ?? throw new ArgumentException("Không tìm thấy hóa đơn " + maHD);
        DatVeDTO dv = new DatVeBUS().timKiem(hd.MaDV);
        KhachHangDTO kh = new KhachHangBUS().timKiem(hd.MaKH);
        NhanVienDTO nv = hd.MaNV == "" ? null : new NhanVienBUS().timKiem(hd.MaNV);
        ChuyenBayDTO cb = dv == null ? null : new ChuyenBayBUS().timKiem(dv.MaChuyenBay);
        string hang = cb == null ? "" : new HangHangKhongBUS().timKiem(cb.MaHHK)?.TenHang ?? cb.MaHHK;
        SanBayBUS sbBus = new SanBayBUS();
        string sanDi = cb == null ? "" : cb.MaSanBayDi + " - " + (sbBus.timKiem(cb.MaSanBayDi)?.Ten ?? "");
        string sanDen = cb == null ? "" : cb.MaSanBayDen + " - " + (sbBus.timKiem(cb.MaSanBayDen)?.Ten ?? "");
        List<CTHDDTO> dsCT = new CTHDBUS().DocTheoHD(maHD);
        VeBUS veBus = new VeBUS();
        HanhKhachBUS hkBus = new HanhKhachBUS();
        HangVeBUS hvBus = new HangVeBUS();
        decimal cong = dsCT.Sum(c => c.ThanhTien);
        decimal giam = dv == null ? 0 : dv.GiamGia;

        Document.Create(tl =>
        {
            tl.Page(trang =>
            {
                trang.Size(PageSizes.A4);
                trang.Margin(36);
                trang.DefaultTextStyle(x => x.FontFamily(Font).FontSize(10));
                trang.Header().Row(r =>
                {
                    r.RelativeItem().Column(c =>
                    {
                        c.Item().Text("HỆ THỐNG BÁN VÉ MÁY BAY").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                        c.Item().Text("Quầy bán vé và danh mục chuyến bay").FontSize(9).FontColor(Colors.Grey.Darken1);
                    });
                    r.RelativeItem().AlignRight().Column(c =>
                    {
                        c.Item().AlignRight().Text("Số: " + hd.MaHD).Bold();
                        c.Item().AlignRight().Text("Ngày lập: " + hd.NgayLap.ToString("dd/MM/yyyy HH:mm"));
                    });
                });
                trang.Content().PaddingTop(16).Column(c =>
                {
                    c.Spacing(10);
                    c.Item().AlignCenter().Text("HÓA ĐƠN BÁN VÉ MÁY BAY").FontSize(18).Bold();

                    c.Item().Row(r =>
                    {
                        r.RelativeItem().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Column(k =>
                        {
                            k.Item().Text("Khách hàng").Bold();
                            k.Item().Text("Họ tên: " + (kh?.HoTen ?? hd.MaKH) + " (" + hd.MaKH + ")");
                            k.Item().Text("CCCD: " + (kh?.CCCD ?? ""));
                            k.Item().Text("Điện thoại: " + (kh?.SDT ?? ""));
                            k.Item().Text("Email: " + (kh?.Email ?? ""));
                        });
                        r.ConstantItem(12);
                        r.RelativeItem().Border(0.5f).BorderColor(Colors.Grey.Medium).Padding(8).Column(k =>
                        {
                            k.Item().Text("Chuyến bay").Bold();
                            k.Item().Text((cb?.MaChuyenBay ?? "") + " - " + hang);
                            k.Item().Text("Từ: " + sanDi);
                            k.Item().Text("Đến: " + sanDen);
                            k.Item().Text("Khởi hành: " + (cb == null ? "" : cb.NgayKhoiHanh.ToString("dd/MM/yyyy HH:mm")));
                        });
                    });

                    c.Item().Text("Phiếu đặt: " + hd.MaDV + "     Nhân viên lập: " + (nv == null ? "" : nv.Ho + " " + nv.Ten + " (" + nv.MaNV + ")")
                        + "     Thanh toán: " + hd.PhuongThucTT);

                    c.Item().Table(t =>
                    {
                        t.ColumnsDefinition(cd =>
                        {
                            cd.ConstantColumn(28);
                            cd.ConstantColumn(50);
                            cd.RelativeColumn(3);
                            cd.RelativeColumn(2);
                            cd.ConstantColumn(62);
                            cd.ConstantColumn(28);
                            cd.RelativeColumn(1.6f);
                            cd.RelativeColumn(1.6f);
                        });
                        t.Header(h =>
                        {
                            foreach (string ten in new[] { "STT", "Mã vé", "Hành khách", "Hạng vé", "Ghế", "SL", "Đơn giá", "Thành tiền" })
                                h.Cell().Element(OTieuDe).Text(ten).Bold();
                        });
                        int stt = 0;
                        foreach (CTHDDTO ct in dsCT)
                        {
                            VeDTO ve = veBus.timKiem(ct.MaVe);
                            HanhKhachDTO hk = ve == null ? null : hkBus.timKiem(ve.MaHK);
                            string tenHang = ve == null ? "" : hvBus.timKiem(ve.MaHV)?.TenHV ?? ve.MaHV;
                            t.Cell().Element(ODuLieu).Text((++stt).ToString());
                            t.Cell().Element(ODuLieu).Text(ct.MaVe);
                            t.Cell().Element(ODuLieu).Text(hk == null ? "" : hk.HoTen + " (" + hk.LoaiKH + ")");
                            t.Cell().Element(ODuLieu).Text(tenHang);
                            t.Cell().Element(ODuLieu).Text(ve?.MaGhe ?? "");
                            t.Cell().Element(ODuLieu).AlignRight().Text(ct.SL.ToString());
                            t.Cell().Element(ODuLieu).AlignRight().Text(ct.DonGiaVe.ToString("N0"));
                            t.Cell().Element(ODuLieu).AlignRight().Text(ct.ThanhTien.ToString("N0"));
                        }
                    });

                    c.Item().AlignRight().Width(260).Column(k =>
                    {
                        void Dong(string nhan, decimal tien, bool dam)
                        {
                            k.Item().Row(r =>
                            {
                                var a = r.RelativeItem().Text(nhan);
                                var b = r.RelativeItem().AlignRight().Text(tien.ToString("N0") + " đ");
                                if (dam) { a.Bold(); b.Bold(); }
                            });
                        }
                        Dong("Cộng thành tiền:", cong, false);
                        Dong("Giảm giá" + (dv != null && dv.MaVoucher != "" ? " (voucher " + dv.MaVoucher + ")" : "") + ":", giam, false);
                        k.Item().PaddingVertical(2).LineHorizontal(0.5f);
                        Dong("Tổng thanh toán:", hd.TongTien, true);
                    });

                    c.Item().Text("Trạng thái: " + hd.TrangThai).Italic();

                    c.Item().PaddingTop(24).Row(r =>
                    {
                        r.RelativeItem().AlignCenter().Column(k =>
                        {
                            k.Item().AlignCenter().Text("Khách hàng").Bold();
                            k.Item().AlignCenter().Text("(Ký, ghi rõ họ tên)").FontSize(8).Italic();
                        });
                        r.RelativeItem().AlignCenter().Column(k =>
                        {
                            k.Item().AlignCenter().Text("Nhân viên lập hóa đơn").Bold();
                            k.Item().AlignCenter().Text("(Ký, ghi rõ họ tên)").FontSize(8).Italic();
                        });
                    });
                });
                trang.Footer().AlignCenter().Text("Cảm ơn quý khách đã sử dụng dịch vụ").FontSize(8).FontColor(Colors.Grey.Darken1);
            });
        }).GeneratePdf(duongDan);
    }
}
