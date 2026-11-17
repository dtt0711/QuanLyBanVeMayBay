using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmHanhKhach : Form
{
    private List<HanhKhachDTO> ds = new List<HanhKhachDTO>();

    public FrmHanhKhach()
    {
        InitializeComponent();
        cboMaKH.Format += (s, e) => { if (e.ListItem is KhachHangDTO p) e.Value = p.HoTen == "" ? p.MaKH : p.MaKH + " - " + p.HoTen; };
        GiaoDien.ThemNutChon(cboMaKH, "Chọn khách hàng");
        // Loại hành khách tự tính theo ngày sinh và tham số TUOI_TRE_EM, không cho nhập
        GiaoDien.KhoaO(txtLoaiKH);
        dtpNgaySinh.ValueChanged += (s, e) => txtLoaiKH.Text = HanhKhachBUS.TinhLoaiHK(dtpNgaySinh.Value);
    }

    private void FrmHanhKhach_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSHK_Click(sender, e);
    }

    private void NapCombo()
    {
        List<KhachHangDTO> khach = new List<KhachHangDTO>();
        khach.Add(new KhachHangDTO { MaKH = "Chọn khách hàng" });
        khach.AddRange(new KhachHangBUS().DocDSKH());
        cboMaKH.DataSource = khach;
        cboMaKH.DisplayMember = "MaKH";
        cboMaKH.ValueMember = "MaKH";
        cboMaKH.SelectedIndex = 0;
    }

    private void NapDong(HanhKhachDTO hk)
    {
        int n = lvDSHK.Items.Count;
        lvDSHK.Items.Add(hk.MaHK);
        lvDSHK.Items[n].SubItems.Add(hk.HoTen);
        lvDSHK.Items[n].SubItems.Add(hk.NgaySinh.Year < 1753 ? "" : hk.NgaySinh.ToString("dd/MM/yyyy"));
        lvDSHK.Items[n].SubItems.Add(hk.CCCD);
        lvDSHK.Items[n].SubItems.Add(hk.GioiTinh);
        lvDSHK.Items[n].SubItems.Add(hk.MaKH);
        lvDSHK.Items[n].SubItems.Add(hk.LoaiKH);
    }

    private void HienThi(List<HanhKhachDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSHK.Items.Clear();
        foreach (HanhKhachDTO hk in ds)
            NapDong(hk);
        thanhTim.HienSoDong(ds.Count);
    }

    // Đọc dữ liệu trên form; trả về null nếu thiếu hoặc không hợp lệ
    private HanhKhachDTO LayTuForm()
    {
        if (cboMaKH.SelectedValue == null || cboMaKH.SelectedValue.ToString().StartsWith("Chọn "))
        {
            MessageBox.Show("Chọn khách hàng.");
            return null;
        }
        string loai = HanhKhachBUS.TinhLoaiHK(dtpNgaySinh.Value);
        HanhKhachDTO hk = new HanhKhachDTO(txtMaHK.Text, txtHoTen.Text, dtpNgaySinh.Value, txtCCCD.Text, txtGioiTinh.Text, cboMaKH.SelectedValue.ToString(), loai);
        string loi = new HanhKhachBUS().KiemTra(hk);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return null;
        }
        return hk;
    }

    private void btnDocDSHK_Click(object sender, EventArgs e)
    {
        HienThi(new HanhKhachBUS().DocDSHK());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        HanhKhachDTO hk = LayTuForm();
        if (hk == null) return;
        HanhKhachBUS bus = new HanhKhachBUS();
        if (txtMaHK.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaHK.Text))
        {
            MessageBox.Show("Mã " + txtMaHK.Text + " đã tồn tại.");
            return;
        }
        if (!bus.Them(hk))
            return;
        btnDocDSHK_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        HanhKhachDTO hk = LayTuForm();
        if (hk == null) return;
        HanhKhachBUS bus = new HanhKhachBUS();
        bus.Sua(hk);
        HienThi(bus.DocDSHK());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        HanhKhachBUS bus = new HanhKhachBUS();
        bus.Xoa(txtMaHK.Text);
        HienThi(bus.DocDSHK());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSHK.SelectedIndices.Clear();
        txtMaHK.Clear();
        txtHoTen.Clear();
        dtpNgaySinh.Value = DateTime.Now;
        txtCCCD.Clear();
        txtGioiTinh.Clear();
        cboMaKH.SelectedIndex = 0;
        txtLoaiKH.Text = HanhKhachBUS.TinhLoaiHK(dtpNgaySinh.Value);
        txtMaHK.Enabled = true;
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new HanhKhachBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(HanhKhachDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new HanhKhachBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSHK_Click(sender, e);
    }

    private void lvDSHK_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSHK.SelectedIndices.Count == 0) return;
        int i = lvDSHK.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaHK.Text = ds[i].MaHK;
        txtHoTen.Text = ds[i].HoTen;
        dtpNgaySinh.Value = ds[i].NgaySinh.Year < 1753 ? DateTime.Today : ds[i].NgaySinh;
        txtCCCD.Text = ds[i].CCCD;
        txtGioiTinh.Text = ds[i].GioiTinh;
        cboMaKH.SelectedValue = ds[i].MaKH;
        txtLoaiKH.Text = ds[i].LoaiKH;
        txtMaHK.Enabled = false;
    }
}
