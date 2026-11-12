using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmNhanVien : Form
{
    private List<NhanVienDTO> ds = new List<NhanVienDTO>();

    public FrmNhanVien()
    {
        InitializeComponent();
    }

    private void FrmNhanVien_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSNV_Click(sender, e);
    }

    private void btnDocDSNV_Click(object sender, EventArgs e)
    {
        NhanVienBUS bus = new NhanVienBUS();
        ds = bus.DocDSNV();
        lvDSNV.Items.Clear();
        foreach (NhanVienDTO nv in ds)
        {
            int n = lvDSNV.Items.Count;
            lvDSNV.Items.Add(nv.MaNV);
            lvDSNV.Items[n].SubItems.Add(nv.Ho);
            lvDSNV.Items[n].SubItems.Add(nv.Ten);
            lvDSNV.Items[n].SubItems.Add(nv.ChucVu);
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        NhanVienDTO nv = new NhanVienDTO(txtMaNV.Text, txtHo.Text, txtTen.Text, txtChucVu.Text);
        NhanVienBUS bus = new NhanVienBUS();
        bus.Them(nv);
        ds.Add(nv);
        int n = lvDSNV.Items.Count;
        lvDSNV.Items.Add(nv.MaNV);
        lvDSNV.Items[n].SubItems.Add(nv.Ho);
        lvDSNV.Items[n].SubItems.Add(nv.Ten);
        lvDSNV.Items[n].SubItems.Add(nv.ChucVu);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        NhanVienDTO nv = new NhanVienDTO(txtMaNV.Text, txtHo.Text, txtTen.Text, txtChucVu.Text);
        NhanVienBUS bus = new NhanVienBUS();
        bus.Sua(nv);
        ds = bus.DocDSNV();
        lvDSNV.Items.Clear();
        foreach (NhanVienDTO nvi in ds)
        {
            int n = lvDSNV.Items.Count;
            lvDSNV.Items.Add(nvi.MaNV);
            lvDSNV.Items[n].SubItems.Add(nvi.Ho);
            lvDSNV.Items[n].SubItems.Add(nvi.Ten);
            lvDSNV.Items[n].SubItems.Add(nvi.ChucVu);
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        NhanVienBUS bus = new NhanVienBUS();
        bus.Xoa(txtMaNV.Text);
        ds = bus.DocDSNV();
        lvDSNV.Items.Clear();
        foreach (NhanVienDTO nv in ds)
        {
            int n = lvDSNV.Items.Count;
            lvDSNV.Items.Add(nv.MaNV);
            lvDSNV.Items[n].SubItems.Add(nv.Ho);
            lvDSNV.Items[n].SubItems.Add(nv.Ten);
            lvDSNV.Items[n].SubItems.Add(nv.ChucVu);
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSNV.SelectedIndices.Clear();
        txtMaNV.Clear();
        txtHo.Clear();
        txtTen.Clear();
        txtChucVu.Clear();
        txtMaNV.Enabled = true;
    }

    private void btnXemHD_Click(object sender, EventArgs e)
    {
        HoaDonBUS bus = new HoaDonBUS();
        new FrmXemDS("Hóa đơn của NV " + txtMaNV.Text, bus.DocTheoNV(txtMaNV.Text)).ShowDialog();
    }

    private void lvDSNV_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSNV.SelectedIndices.Count == 0) return;
        int i = lvDSNV.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaNV.Text = ds[i].MaNV;
        txtHo.Text = ds[i].Ho;
        txtTen.Text = ds[i].Ten;
        txtChucVu.Text = ds[i].ChucVu;
        txtMaNV.Enabled = false;
    }
}
