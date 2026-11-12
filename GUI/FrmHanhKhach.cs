using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmHanhKhach : Form
{
    private List<HanhKhachDTO> ds = new List<HanhKhachDTO>();

    public FrmHanhKhach()
    {
        InitializeComponent();
        cboMaKH.Format += (s, e) => { if (e.ListItem is KhachHangDTO p) e.Value = p.MaKH + " - " + p.HoTen; };
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
        cboMaKH.DataSource = new KhachHangBUS().DocDSKH();
        cboMaKH.DisplayMember = "MaKH";
        cboMaKH.ValueMember = "MaKH";
        cboMaKH.SelectedIndex = -1;
    }

    private void btnDocDSHK_Click(object sender, EventArgs e)
    {
        HanhKhachBUS bus = new HanhKhachBUS();
        ds = bus.DocDSHK();
        lvDSHK.Items.Clear();
        foreach (HanhKhachDTO hk in ds)
        {
            int n = lvDSHK.Items.Count;
            lvDSHK.Items.Add(hk.MaHK);
            lvDSHK.Items[n].SubItems.Add(hk.HoTen);
            lvDSHK.Items[n].SubItems.Add(hk.NgaySinh.ToString("dd/MM/yyyy"));
            lvDSHK.Items[n].SubItems.Add(hk.CCCD);
            lvDSHK.Items[n].SubItems.Add(hk.GioiTinh);
            lvDSHK.Items[n].SubItems.Add(hk.MaKH);
            lvDSHK.Items[n].SubItems.Add(hk.LoaiKH);
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        HanhKhachDTO hk = new HanhKhachDTO(txtMaHK.Text, txtHoTen.Text, dtpNgaySinh.Value, txtCCCD.Text, txtGioiTinh.Text, cboMaKH.SelectedValue.ToString(), txtLoaiKH.Text);
        HanhKhachBUS bus = new HanhKhachBUS();
        bus.Them(hk);
        ds.Add(hk);
        int n = lvDSHK.Items.Count;
        lvDSHK.Items.Add(hk.MaHK);
        lvDSHK.Items[n].SubItems.Add(hk.HoTen);
        lvDSHK.Items[n].SubItems.Add(hk.NgaySinh.ToString("dd/MM/yyyy"));
        lvDSHK.Items[n].SubItems.Add(hk.CCCD);
        lvDSHK.Items[n].SubItems.Add(hk.GioiTinh);
        lvDSHK.Items[n].SubItems.Add(hk.MaKH);
        lvDSHK.Items[n].SubItems.Add(hk.LoaiKH);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        HanhKhachDTO hk = new HanhKhachDTO(txtMaHK.Text, txtHoTen.Text, dtpNgaySinh.Value, txtCCCD.Text, txtGioiTinh.Text, cboMaKH.SelectedValue.ToString(), txtLoaiKH.Text);
        HanhKhachBUS bus = new HanhKhachBUS();
        bus.Sua(hk);
        ds = bus.DocDSHK();
        lvDSHK.Items.Clear();
        foreach (HanhKhachDTO h in ds)
        {
            int n = lvDSHK.Items.Count;
            lvDSHK.Items.Add(h.MaHK);
            lvDSHK.Items[n].SubItems.Add(h.HoTen);
            lvDSHK.Items[n].SubItems.Add(h.NgaySinh.ToString("dd/MM/yyyy"));
            lvDSHK.Items[n].SubItems.Add(h.CCCD);
            lvDSHK.Items[n].SubItems.Add(h.GioiTinh);
            lvDSHK.Items[n].SubItems.Add(h.MaKH);
            lvDSHK.Items[n].SubItems.Add(h.LoaiKH);
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        HanhKhachBUS bus = new HanhKhachBUS();
        bus.Xoa(txtMaHK.Text);
        ds = bus.DocDSHK();
        lvDSHK.Items.Clear();
        foreach (HanhKhachDTO h in ds)
        {
            int n = lvDSHK.Items.Count;
            lvDSHK.Items.Add(h.MaHK);
            lvDSHK.Items[n].SubItems.Add(h.HoTen);
            lvDSHK.Items[n].SubItems.Add(h.NgaySinh.ToString("dd/MM/yyyy"));
            lvDSHK.Items[n].SubItems.Add(h.CCCD);
            lvDSHK.Items[n].SubItems.Add(h.GioiTinh);
            lvDSHK.Items[n].SubItems.Add(h.MaKH);
            lvDSHK.Items[n].SubItems.Add(h.LoaiKH);
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSHK.SelectedIndices.Clear();
        txtMaHK.Clear();
        txtHoTen.Clear();
        dtpNgaySinh.Value = DateTime.Now;
        txtCCCD.Clear();
        txtGioiTinh.Clear();
        cboMaKH.SelectedIndex = -1;
        txtLoaiKH.Clear();
        txtMaHK.Enabled = true;
    }

    private void lvDSHK_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSHK.SelectedIndices.Count == 0) return;
        int i = lvDSHK.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaHK.Text = ds[i].MaHK;
        txtHoTen.Text = ds[i].HoTen;
        dtpNgaySinh.Value = ds[i].NgaySinh;
        txtCCCD.Text = ds[i].CCCD;
        txtGioiTinh.Text = ds[i].GioiTinh;
        cboMaKH.SelectedValue = ds[i].MaKH;
        txtLoaiKH.Text = ds[i].LoaiKH;
        txtMaHK.Enabled = false;
    }
}
