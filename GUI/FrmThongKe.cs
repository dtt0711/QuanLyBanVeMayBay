using System.Data;
using QuanLyBanVeMayBay.BUS;

namespace QuanLyBanVeMayBay.GUI;

// Thống kê tiền thu từ hóa đơn đã thanh toán (đề tài không có phiếu nhập hàng nên không có tổng chi, lợi nhuận)
public partial class FrmThongKe : Form
{
    public FrmThongKe()
    {
        InitializeComponent();
        foreach (DataGridView g in new[] { dgvTongThu, dgvQuyNV, dgvQuyKH, dgvQuyCB, dgvDoiTuong, dgvNhieuKhoa })
            KieuBang(g);
        // Mỗi tab có nút xuất Excel và xuất PDF đúng bảng đang xem
        ThemNutXuat(pnlTongThu, 770, () => "Tổng thu", () => MoTaKhoang(dtpTu1, dtpDen1) + ", theo " + cboKy1.Text.ToLower(),
            () => new[] { ("Tổng thu theo " + cboKy1.Text.ToLower(), Bang(dgvTongThu)) });
        ThemNutXuat(pnlTheoQuy, 310, () => "Tổng tiền hóa đơn theo quý", () => "Năm " + cboNam.Text,
            () => new[] { ("Theo nhân viên và quý", Bang(dgvQuyNV)), ("Theo khách hàng và quý", Bang(dgvQuyKH)), ("Theo chuyến bay và quý", Bang(dgvQuyCB)) });
        ThemNutXuat(pnlDoiTuong, 880, () => "Tổng tiền theo " + cboDoiTuong.Text.ToLower(), () => MoTaKhoang(dtpTu3, dtpDen3) + ", theo " + cboKy3.Text.ToLower(),
            () => new[] { (cboDoiTuong.Text + " theo " + cboKy3.Text.ToLower(), Bang(dgvDoiTuong)) });
        ThemNutXuat(pnlNhieuKhoa, 880, () => "Tổng tiền theo " + cboLoai.Text.ToLower(), () => MoTaKhoang(dtpTu4, dtpDen4),
            () => new[] { (cboLoai.Text, Bang(dgvNhieuKhoa)) });
    }

    private static DataTable Bang(DataGridView g) => g.DataSource as DataTable ?? new DataTable();

    private static string MoTaKhoang(DateTimePicker tu, DateTimePicker den) =>
        "Từ " + tu.Value.ToString("dd/MM/yyyy") + " đến " + den.Value.ToString("dd/MM/yyyy");

    private void ThemNutXuat(Panel pnl, int x, Func<string> tieuDe, Func<string> moTa, Func<(string, DataTable)[]> layBang)
    {
        Button excel = new Button { Text = "Xuất Excel", Location = new Point(x, 14), Size = new Size(100, 30) };
        Button pdf = new Button { Text = "Xuất PDF", Location = new Point(x + 110, 14), Size = new Size(100, 30) };
        GiaoDien.Nut(excel);
        GiaoDien.Nut(pdf);
        pnl.Controls.Add(excel);
        pnl.Controls.Add(pdf);
        excel.Click += (s, e) => Xuat("xlsx", tieuDe(), moTa(), layBang());
        pdf.Click += (s, e) => Xuat("pdf", tieuDe(), moTa(), layBang());
    }

    private void Xuat(string loai, string tieuDe, string moTa, (string, DataTable)[] dsBang)
    {
        string duongDan = GiaoDien.HoiNoiLuu(GiaoDien.TenFile("ThongKe " + tieuDe), loai);
        if (duongDan == "")
            return;
        try
        {
            if (loai == "pdf")
                PdfBUS.XuatBang(duongDan, tieuDe.ToUpper(), moTa, dsBang);
            else
                ExcelBUS.Xuat(duongDan, dsBang);
            GiaoDien.BaoDaLuu(duongDan);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không xuất được file (file đang mở?): " + ex.Message);
        }
    }

    private void FrmThongKe_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        ThongKeBUS bus = new ThongKeBUS();
        List<int> dsNam = bus.DSNam();
        DateTime tu = dsNam.Count > 0 ? new DateTime(dsNam[0], 1, 1) : new DateTime(DateTime.Today.Year, 1, 1);
        foreach (DateTimePicker d in new[] { dtpTu1, dtpTu3, dtpTu4 })
            d.Value = tu;
        foreach (DateTimePicker d in new[] { dtpDen1, dtpDen3, dtpDen4 })
            d.Value = DateTime.Today;
        cboKy1.Items.AddRange(ThongKeBUS.DSKy);
        cboKy1.SelectedItem = "Quý";
        cboKy3.Items.AddRange(ThongKeBUS.DSKy);
        cboKy3.SelectedItem = "Tháng";
        cboDoiTuong.Items.AddRange(ThongKeBUS.DSDoiTuong);
        cboDoiTuong.SelectedIndex = 0;
        cboLoai.Items.AddRange(new object[] { "Khách hàng và chuyến bay", "Nhân viên, chuyến bay và năm" });
        cboLoai.SelectedIndex = 0;
        foreach (int n in dsNam)
            cboNam.Items.Add(n);
        if (cboNam.Items.Count > 0)
            cboNam.SelectedIndex = cboNam.Items.Count - 1;

        btnTongThu_Click(sender, e);
        btnTheoQuy_Click(sender, e);
        btnDoiTuong_Click(sender, e);
        btnNhieuKhoa_Click(sender, e);
    }

    // Định dạng bảng: chỉ xem, số có dấu phân cách nghìn, dòng/cột TC in đậm
    private static void KieuBang(DataGridView g)
    {
        g.ReadOnly = true;
        g.AllowUserToAddRows = false;
        g.AllowUserToDeleteRows = false;
        g.RowHeadersVisible = false;
        g.BackgroundColor = Color.White;
        g.BorderStyle = BorderStyle.None;
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        g.EnableHeadersVisualStyles = false;
        g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        g.DataBindingComplete += (s, e) =>
        {
            foreach (DataGridViewColumn c in g.Columns)
            {
                if (c.ValueType == typeof(decimal) || c.ValueType == typeof(int))
                {
                    c.DefaultCellStyle.Format = "N0";
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    c.MinimumWidth = 90;
                }
                if (c.Name == "TC")
                    c.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
            if (g.Rows.Count > 0)
            {
                DataGridViewRow tc = g.Rows[g.Rows.Count - 1];
                tc.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                tc.DefaultCellStyle.BackColor = Color.FromArgb(226, 232, 240);
            }
            g.ClearSelection();
        };
    }

    private bool KhoangNgayHopLe(DateTimePicker tu, DateTimePicker den)
    {
        if (tu.Value.Date > den.Value.Date)
        {
            MessageBox.Show("Từ ngày phải trước hoặc bằng đến ngày.");
            return false;
        }
        return true;
    }

    // a. Tổng thu từ ngày đến ngày, chia theo ngày/tháng/quý/năm
    private void btnTongThu_Click(object sender, EventArgs e)
    {
        if (!KhoangNgayHopLe(dtpTu1, dtpDen1)) return;
        ThongKeBUS bus = new ThongKeBUS();
        int soHD = bus.HoaDonDaThu(dtpTu1.Value, dtpDen1.Value).Count;
        decimal tong = bus.TongThu(dtpTu1.Value, dtpDen1.Value);
        lblTongThu.Text = "Tổng thu từ " + dtpTu1.Value.ToString("dd/MM/yyyy") + " đến " + dtpDen1.Value.ToString("dd/MM/yyyy")
            + ": " + tong.ToString("N0") + " đ (" + soHD + " hóa đơn đã thanh toán)";
        dgvTongThu.DataSource = bus.TongThuTheoKy(dtpTu1.Value, dtpDen1.Value, cboKy1.Text);
    }

    // 3 bảng theo quý của một năm: nhân viên, khách hàng, chuyến bay
    private void btnTheoQuy_Click(object sender, EventArgs e)
    {
        if (cboNam.SelectedItem == null) return;
        int nam = (int)cboNam.SelectedItem;
        ThongKeBUS bus = new ThongKeBUS();
        dgvQuyNV.DataSource = bus.TheoQuy("Nhân viên", nam);
        dgvQuyKH.DataSource = bus.TheoQuy("Khách hàng", nam);
        dgvQuyCB.DataSource = bus.TheoQuy("Chuyến bay", nam);
    }

    // b.i Theo khách hàng / nhân viên / chuyến bay, chia theo ngày/tháng/quý/năm, trong khoảng ngày
    private void btnDoiTuong_Click(object sender, EventArgs e)
    {
        if (!KhoangNgayHopLe(dtpTu3, dtpDen3)) return;
        dgvDoiTuong.DataSource = new ThongKeBUS().TheoDoiTuongVaKy(cboDoiTuong.Text, cboKy3.Text, dtpTu3.Value, dtpDen3.Value);
    }

    // b.ii Khách hàng và chuyến bay (2 khóa); b.iii Nhân viên, chuyến bay và năm (3 khóa)
    private void btnNhieuKhoa_Click(object sender, EventArgs e)
    {
        if (!KhoangNgayHopLe(dtpTu4, dtpDen4)) return;
        ThongKeBUS bus = new ThongKeBUS();
        dgvNhieuKhoa.DataSource = cboLoai.SelectedIndex == 0
            ? bus.KhachHangVaChuyenBay(dtpTu4.Value, dtpDen4.Value)
            : bus.NhanVienChuyenBayNam(dtpTu4.Value, dtpDen4.Value);
    }
}
