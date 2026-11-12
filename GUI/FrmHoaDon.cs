using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmHoaDon : Form
{
    private List<HoaDonDTO> ds = new List<HoaDonDTO>();

    public FrmHoaDon()
    {
        InitializeComponent();
        cboMaNV.Format += (s, e) => { if (e.ListItem is NhanVienDTO p) e.Value = p.MaNV + " - " + p.Ho + " " + p.Ten; };
        cboMaKH.Format += (s, e) => { if (e.ListItem is KhachHangDTO p) e.Value = p.MaKH + " - " + p.HoTen; };
    }

    private void FrmHoaDon_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSHD_Click(sender, e);
    }

    private void NapCombo()
    {
        cboMaNV.DataSource = new NhanVienBUS().DocDSNV();
        cboMaNV.DisplayMember = "MaNV";
        cboMaNV.ValueMember = "MaNV";
        cboMaNV.SelectedIndex = -1;
        cboMaKH.DataSource = new KhachHangBUS().DocDSKH();
        cboMaKH.DisplayMember = "MaKH";
        cboMaKH.ValueMember = "MaKH";
        cboMaKH.SelectedIndex = -1;
    }

    private void btnDocDSHD_Click(object sender, EventArgs e)
    {
        HoaDonBUS bus = new HoaDonBUS();
        ds = bus.DocDSHD();
        lvDSHD.Items.Clear();
        foreach (HoaDonDTO hd in ds)
        {
            int n = lvDSHD.Items.Count;
            lvDSHD.Items.Add(hd.MaHD);
            lvDSHD.Items[n].SubItems.Add(hd.NgayLap.ToString("dd/MM/yyyy"));
            lvDSHD.Items[n].SubItems.Add(hd.MaNV);
            lvDSHD.Items[n].SubItems.Add(hd.MaKH);
            lvDSHD.Items[n].SubItems.Add(hd.TongTien.ToString("N0"));
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        HoaDonDTO hd = new HoaDonDTO(txtMaHD.Text, dtpNgayLap.Value, cboMaNV.SelectedValue.ToString(), cboMaKH.SelectedValue.ToString(), 0);
        HoaDonBUS bus = new HoaDonBUS();
        bus.Them(hd);
        ds.Add(hd);
        int n = lvDSHD.Items.Count;
        lvDSHD.Items.Add(hd.MaHD);
        lvDSHD.Items[n].SubItems.Add(hd.NgayLap.ToString("dd/MM/yyyy"));
        lvDSHD.Items[n].SubItems.Add(hd.MaNV);
        lvDSHD.Items[n].SubItems.Add(hd.MaKH);
        lvDSHD.Items[n].SubItems.Add(hd.TongTien.ToString("N0"));
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        HoaDonDTO hd = new HoaDonDTO(txtMaHD.Text, dtpNgayLap.Value, cboMaNV.SelectedValue.ToString(), cboMaKH.SelectedValue.ToString(), 0);
        HoaDonBUS bus = new HoaDonBUS();
        bus.Sua(hd);
        ds = bus.DocDSHD();
        lvDSHD.Items.Clear();
        foreach (HoaDonDTO h in ds)
        {
            int n = lvDSHD.Items.Count;
            lvDSHD.Items.Add(h.MaHD);
            lvDSHD.Items[n].SubItems.Add(h.NgayLap.ToString("dd/MM/yyyy"));
            lvDSHD.Items[n].SubItems.Add(h.MaNV);
            lvDSHD.Items[n].SubItems.Add(h.MaKH);
            lvDSHD.Items[n].SubItems.Add(h.TongTien.ToString("N0"));
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        HoaDonBUS bus = new HoaDonBUS();
        bus.Xoa(txtMaHD.Text);
        ds = bus.DocDSHD();
        lvDSHD.Items.Clear();
        foreach (HoaDonDTO h in ds)
        {
            int n = lvDSHD.Items.Count;
            lvDSHD.Items.Add(h.MaHD);
            lvDSHD.Items[n].SubItems.Add(h.NgayLap.ToString("dd/MM/yyyy"));
            lvDSHD.Items[n].SubItems.Add(h.MaNV);
            lvDSHD.Items[n].SubItems.Add(h.MaKH);
            lvDSHD.Items[n].SubItems.Add(h.TongTien.ToString("N0"));
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSHD.SelectedIndices.Clear();
        txtMaHD.Clear();
        dtpNgayLap.Value = DateTime.Now;
        cboMaNV.SelectedIndex = -1;
        cboMaKH.SelectedIndex = -1;
        txtMaHD.Enabled = true;
    }

    private void btnChiTiet_Click(object sender, EventArgs e)
    {
        new FrmCTHD(txtMaHD.Text).ShowDialog();
        btnDocDSHD_Click(sender, e);
    }

    private void lvDSHD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSHD.SelectedIndices.Count == 0) return;
        int i = lvDSHD.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaHD.Text = ds[i].MaHD;
        dtpNgayLap.Value = ds[i].NgayLap;
        cboMaNV.SelectedValue = ds[i].MaNV;
        cboMaKH.SelectedValue = ds[i].MaKH;
        txtMaHD.Enabled = false;
    }
}
