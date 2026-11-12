using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmCTKM : Form
{
    private List<CTKMDTO> ds = new List<CTKMDTO>();

    public FrmCTKM()
    {
        InitializeComponent();
    }

    private void FrmCTKM_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSCTKM_Click(sender, e);
    }

    private void btnDocDSCTKM_Click(object sender, EventArgs e)
    {
        CTKMBUS bus = new CTKMBUS();
        ds = bus.DocDSCTKM();
        lvDSCTKM.Items.Clear();
        foreach (CTKMDTO ctkm in ds)
        {
            int n = lvDSCTKM.Items.Count;
            lvDSCTKM.Items.Add(ctkm.MaCTKM);
            lvDSCTKM.Items[n].SubItems.Add(ctkm.TenCTKM);
            lvDSCTKM.Items[n].SubItems.Add(ctkm.NgayBatDau.ToString("dd/MM/yyyy"));
            lvDSCTKM.Items[n].SubItems.Add(ctkm.NgayKetThuc.ToString("dd/MM/yyyy"));
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        CTKMDTO ctkm = new CTKMDTO(txtMaCTKM.Text, txtTenCTKM.Text, dtpNgayBatDau.Value, dtpNgayKetThuc.Value);
        CTKMBUS bus = new CTKMBUS();
        bus.Them(ctkm);
        ds.Add(ctkm);
        int n = lvDSCTKM.Items.Count;
        lvDSCTKM.Items.Add(ctkm.MaCTKM);
        lvDSCTKM.Items[n].SubItems.Add(ctkm.TenCTKM);
        lvDSCTKM.Items[n].SubItems.Add(ctkm.NgayBatDau.ToString("dd/MM/yyyy"));
        lvDSCTKM.Items[n].SubItems.Add(ctkm.NgayKetThuc.ToString("dd/MM/yyyy"));
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        CTKMDTO ctkm = new CTKMDTO(txtMaCTKM.Text, txtTenCTKM.Text, dtpNgayBatDau.Value, dtpNgayKetThuc.Value);
        CTKMBUS bus = new CTKMBUS();
        bus.Sua(ctkm);
        ds = bus.DocDSCTKM();
        lvDSCTKM.Items.Clear();
        foreach (CTKMDTO c in ds)
        {
            int n = lvDSCTKM.Items.Count;
            lvDSCTKM.Items.Add(c.MaCTKM);
            lvDSCTKM.Items[n].SubItems.Add(c.TenCTKM);
            lvDSCTKM.Items[n].SubItems.Add(c.NgayBatDau.ToString("dd/MM/yyyy"));
            lvDSCTKM.Items[n].SubItems.Add(c.NgayKetThuc.ToString("dd/MM/yyyy"));
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        CTKMBUS bus = new CTKMBUS();
        bus.Xoa(txtMaCTKM.Text);
        ds = bus.DocDSCTKM();
        lvDSCTKM.Items.Clear();
        foreach (CTKMDTO c in ds)
        {
            int n = lvDSCTKM.Items.Count;
            lvDSCTKM.Items.Add(c.MaCTKM);
            lvDSCTKM.Items[n].SubItems.Add(c.TenCTKM);
            lvDSCTKM.Items[n].SubItems.Add(c.NgayBatDau.ToString("dd/MM/yyyy"));
            lvDSCTKM.Items[n].SubItems.Add(c.NgayKetThuc.ToString("dd/MM/yyyy"));
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSCTKM.SelectedIndices.Clear();
        txtMaCTKM.Clear();
        txtTenCTKM.Clear();
        dtpNgayBatDau.Value = DateTime.Now;
        dtpNgayKetThuc.Value = DateTime.Now;
        txtMaCTKM.Enabled = true;
    }

    private void btnXemCon_Click(object sender, EventArgs e)
    {
        VoucherBUS bus = new VoucherBUS();
        new FrmXemDS("Voucher của CTKM " + txtMaCTKM.Text, bus.DocTheoCTKM(txtMaCTKM.Text)).ShowDialog();
    }

    private void lvDSCTKM_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSCTKM.SelectedIndices.Count == 0) return;
        int i = lvDSCTKM.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaCTKM.Text = ds[i].MaCTKM;
        txtTenCTKM.Text = ds[i].TenCTKM;
        dtpNgayBatDau.Value = ds[i].NgayBatDau;
        dtpNgayKetThuc.Value = ds[i].NgayKetThuc;
        txtMaCTKM.Enabled = false;
    }
}
