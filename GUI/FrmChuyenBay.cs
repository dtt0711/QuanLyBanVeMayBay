using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmChuyenBay : Form
{
    private List<ChuyenBayDTO> ds = new List<ChuyenBayDTO>();

    public FrmChuyenBay()
    {
        InitializeComponent();
        cboMaMayBay.Format += (s, e) =>
        {
            if (e.ListItem is MayBayDTO p)
                e.Value = p.MaHHK == "" ? "Chọn mã máy bay" : p.MaMayBay + " - " + p.MaHHK;
        };
        cboSanBayDi.Format += (s, e) => { if (e.ListItem is SanBayDTO p) e.Value = p.Ten == "" ? p.MaSanBay : p.MaSanBay + " - " + p.Ten; };
        cboSanBayDen.Format += (s, e) => { if (e.ListItem is SanBayDTO p) e.Value = p.Ten == "" ? p.MaSanBay : p.MaSanBay + " - " + p.Ten; };
        GiaoDien.ThemNutChon(cboMaMayBay, "Chọn máy bay");
        GiaoDien.ThemNutChon(cboSanBayDi, "Chọn sân bay đi");
        GiaoDien.ThemNutChon(cboSanBayDen, "Chọn sân bay đến");
        // Field không cho nhập: số vé đã bán do bán/trả vé cập nhật; thời gian bay và giờ đến tính từ 2 ngày
        GiaoDien.KhoaO(txtSoVeDaBan);
        // Tổng số ghế = số ghế của máy bay được chọn
        GiaoDien.KhoaO(txtTongSoGhe);
        cboMaMayBay.SelectedIndexChanged += (s, e) => txtTongSoGhe.Text = DemGhe().ToString();
        GiaoDien.KhoaO(txtThoiGianBay);
        dtpThoiGianDen.Enabled = false;
        cboTrangThai.Items.AddRange(TrangThaiBUS.ChuyenBay);
        cboTrangThai.SelectedIndex = 0;
        dtpNgayKhoiHanh.ValueChanged += (s, e) => TinhThoiGian();
        dtpNgayDen.ValueChanged += (s, e) => TinhThoiGian();
    }

    private void FrmChuyenBay_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSCB_Click(sender, e);
    }

    private void NapCombo()
    {
        List<MayBayDTO> mayBay = new List<MayBayDTO>();
        mayBay.Add(new MayBayDTO("Chọn mã máy bay", "", "", 0));
        mayBay.AddRange(new MayBayBUS().DocDSMB());
        cboMaMayBay.DataSource = mayBay;
        cboMaMayBay.DisplayMember = "MaMayBay";
        cboMaMayBay.ValueMember = "MaMayBay";
        cboMaMayBay.SelectedIndex = 0;

        List<SanBayDTO> sanBay = new SanBayBUS().DocDSSB();
        List<SanBayDTO> di = new List<SanBayDTO>();
        di.Add(new SanBayDTO { MaSanBay = "Chọn sân đi" });
        di.AddRange(sanBay);
        cboSanBayDi.DataSource = di;
        cboSanBayDi.DisplayMember = "MaSanBay";
        cboSanBayDi.ValueMember = "MaSanBay";
        cboSanBayDi.SelectedIndex = 0;
        List<SanBayDTO> den = new List<SanBayDTO>();
        den.Add(new SanBayDTO { MaSanBay = "Chọn sân đến" });
        den.AddRange(sanBay);
        cboSanBayDen.DataSource = den;
        cboSanBayDen.DisplayMember = "MaSanBay";
        cboSanBayDen.ValueMember = "MaSanBay";
        cboSanBayDen.SelectedIndex = 0;
    }

    // Thời gian bay (phút) = ngày đến - ngày khởi hành; giờ đến lấy theo ngày đến
    private void TinhThoiGian()
    {
        txtThoiGianBay.Text = ChuyenBayBUS.TinhThoiGianBay(dtpNgayKhoiHanh.Value, dtpNgayDen.Value).ToString();
        dtpThoiGianDen.Value = DateTime.Today.Add(dtpNgayDen.Value.TimeOfDay);
    }

    private int DemGhe()
    {
        MayBayDTO mb = cboMaMayBay.SelectedItem as MayBayDTO;
        return mb == null || mb.MaHHK == "" ? 0 : new GheBUS().DocTheoMayBay(mb.MaMayBay).Count;
    }

    private void NapDong(ChuyenBayDTO cb)
    {
        int n = lvDSCB.Items.Count;
        lvDSCB.Items.Add(cb.MaChuyenBay);
        lvDSCB.Items[n].SubItems.Add(cb.MaMayBay);
        lvDSCB.Items[n].SubItems.Add(cb.MaSanBayDi);
        lvDSCB.Items[n].SubItems.Add(cb.MaSanBayDen);
        lvDSCB.Items[n].SubItems.Add(cb.NgayKhoiHanh.ToString("dd/MM/yyyy HH:mm"));
        lvDSCB.Items[n].SubItems.Add(cb.NgayDen.ToString("dd/MM/yyyy HH:mm"));
        lvDSCB.Items[n].SubItems.Add(cb.GiaVe.ToString("N0"));
        lvDSCB.Items[n].SubItems.Add(cb.TrangThai);
    }

    private void HienThi(List<ChuyenBayDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSCB.Items.Clear();
        foreach (ChuyenBayDTO cb in ds)
            NapDong(cb);
        thanhTim.HienSoDong(ds.Count);
    }

    private bool ThieuChon()
    {
        MayBayDTO mayBayChon = cboMaMayBay.SelectedItem as MayBayDTO;
        string sanDi = cboSanBayDi.SelectedValue == null ? "" : cboSanBayDi.SelectedValue.ToString();
        string sanDen = cboSanBayDen.SelectedValue == null ? "" : cboSanBayDen.SelectedValue.ToString();
        if (mayBayChon == null || mayBayChon.MaHHK == "" || sanDi == "" || sanDen == "" || sanDi.StartsWith("Chọn ") || sanDen.StartsWith("Chọn "))
        {
            MessageBox.Show("Chọn máy bay, sân đi và sân đến.");
            return true;
        }
        return false;
    }

    // Đọc dữ liệu trên form và kiểm tra; trả về null nếu không hợp lệ
    private ChuyenBayDTO LayTuForm(bool laThem)
    {
        if (ThieuChon())
            return null;
        MayBayDTO mayBay = (MayBayDTO)cboMaMayBay.SelectedItem;
        int tongSoGhe = DemGhe();
        if (!decimal.TryParse(txtGiaVe.Text, out decimal giaVe))
        {
            MessageBox.Show("Nhập giá vé (giá hạng Phổ thông) bằng số.");
            return null;
        }
        // Thêm mới: chưa bán vé nào. Sửa: giữ số vé đã bán đang có
        ChuyenBayDTO cu = laThem ? null : new ChuyenBayBUS().timKiem(txtMaCB.Text);
        int soVeDaBan = cu == null ? 0 : cu.SoVeDaBan;
        string trangThai = cboTrangThai.Text == "" ? "Chưa khởi hành" : cboTrangThai.Text;
        ChuyenBayDTO cb = new ChuyenBayDTO(
            txtMaCB.Text,
            mayBay.MaHHK,
            cboSanBayDi.SelectedValue.ToString(),
            cboSanBayDen.SelectedValue.ToString(),
            mayBay.MaMayBay,
            dtpNgayKhoiHanh.Value,
            dtpNgayDen.Value,
            ChuyenBayBUS.TinhThoiGianBay(dtpNgayKhoiHanh.Value, dtpNgayDen.Value),
            dtpNgayDen.Value.ToString("HH:mm:ss"),
            tongSoGhe,
            soVeDaBan,
            giaVe,
            trangThai);
        string loi = new ChuyenBayBUS().KiemTra(cb);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return null;
        }
        return cb;
    }

    private void btnDocDSCB_Click(object sender, EventArgs e)
    {
        HienThi(new ChuyenBayBUS().DocDSCB());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        ChuyenBayDTO cb = LayTuForm(true);
        if (cb == null) return;
        ChuyenBayBUS bus = new ChuyenBayBUS();
        if (txtMaCB.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaCB.Text))
        {
            MessageBox.Show("Mã " + txtMaCB.Text + " đã tồn tại.");
            return;
        }
        if (!bus.Them(cb))
            return;
        btnDocDSCB_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        ChuyenBayDTO cb = LayTuForm(false);
        if (cb == null) return;
        ChuyenBayBUS bus = new ChuyenBayBUS();
        bus.Sua(cb);
        HienThi(bus.DocDSCB());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        ChuyenBayBUS bus = new ChuyenBayBUS();
        bus.Xoa(txtMaCB.Text);
        HienThi(bus.DocDSCB());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSCB.SelectedIndices.Clear();
        txtMaCB.Clear();
        cboMaMayBay.SelectedIndex = 0;
        cboSanBayDi.SelectedIndex = 0;
        cboSanBayDen.SelectedIndex = 0;
        dtpNgayKhoiHanh.Value = DateTime.Now;
        dtpNgayDen.Value = DateTime.Now;
        txtTongSoGhe.Text = "0";
        txtSoVeDaBan.Text = "0";
        txtGiaVe.Clear();
        cboTrangThai.SelectedIndex = 0;
        txtMaCB.Enabled = true;
        TinhThoiGian();
    }

    private void btnXemHD_Click(object sender, EventArgs e)
    {
        CTHDBUS bus = new CTHDBUS();
        new FrmXemDS("Chi tiết hóa đơn của chuyến bay " + txtMaCB.Text, bus.DocTheoCB(txtMaCB.Text)).ShowDialog();
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new ChuyenBayBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(ChuyenBayDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new ChuyenBayBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSCB_Click(sender, e);
    }

    private void lvDSCB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSCB.SelectedIndices.Count == 0) return;
        int i = lvDSCB.SelectedIndices[0];
        if (i >= ds.Count) return;
        ChuyenBayDTO cb = ds[i];
        txtMaCB.Text = cb.MaChuyenBay;
        cboMaMayBay.SelectedValue = cb.MaMayBay;
        cboSanBayDi.SelectedValue = cb.MaSanBayDi;
        cboSanBayDen.SelectedValue = cb.MaSanBayDen;
        dtpNgayKhoiHanh.Value = cb.NgayKhoiHanh;
        dtpNgayDen.Value = cb.NgayDen;
        TinhThoiGian();
        txtTongSoGhe.Text = cb.TongSoGhe.ToString();
        txtSoVeDaBan.Text = cb.SoVeDaBan.ToString();
        txtGiaVe.Text = cb.GiaVe.ToString();
        cboTrangThai.Text = cb.TrangThai;
        txtMaCB.Enabled = false;
    }
}
