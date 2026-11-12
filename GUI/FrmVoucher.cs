using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmVoucher : Form
{
    private List<VoucherDTO> ds = new List<VoucherDTO>();

    public FrmVoucher()
    {
        InitializeComponent();
        cboMaCTKM.Format += (s, e) => { if (e.ListItem is CTKMDTO p) e.Value = p.MaCTKM + " - " + p.TenCTKM; };
    }

    private void FrmVoucher_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSVC_Click(sender, e);
    }

    private void NapCombo()
    {
        cboMaCTKM.DataSource = new CTKMBUS().DocDSCTKM();
        cboMaCTKM.DisplayMember = "MaCTKM";
        cboMaCTKM.ValueMember = "MaCTKM";
        cboMaCTKM.SelectedIndex = -1;
    }

    private void btnDocDSVC_Click(object sender, EventArgs e)
    {
        VoucherBUS bus = new VoucherBUS();
        ds = bus.DocDSVC();
        lvDSVC.Items.Clear();
        foreach (VoucherDTO vc in ds)
        {
            int n = lvDSVC.Items.Count;
            lvDSVC.Items.Add(vc.MaVoucher);
            lvDSVC.Items[n].SubItems.Add(vc.MaCTKM);
            lvDSVC.Items[n].SubItems.Add(vc.GiamGia.ToString());
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        VoucherDTO vc = new VoucherDTO(txtMaVoucher.Text, cboMaCTKM.SelectedValue.ToString(), double.Parse(txtGiamGia.Text));
        VoucherBUS bus = new VoucherBUS();
        bus.Them(vc);
        ds.Add(vc);
        int n = lvDSVC.Items.Count;
        lvDSVC.Items.Add(vc.MaVoucher);
        lvDSVC.Items[n].SubItems.Add(vc.MaCTKM);
        lvDSVC.Items[n].SubItems.Add(vc.GiamGia.ToString());
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        VoucherDTO vc = new VoucherDTO(txtMaVoucher.Text, cboMaCTKM.SelectedValue.ToString(), double.Parse(txtGiamGia.Text));
        VoucherBUS bus = new VoucherBUS();
        bus.Sua(vc);
        ds = bus.DocDSVC();
        lvDSVC.Items.Clear();
        foreach (VoucherDTO v in ds)
        {
            int n = lvDSVC.Items.Count;
            lvDSVC.Items.Add(v.MaVoucher);
            lvDSVC.Items[n].SubItems.Add(v.MaCTKM);
            lvDSVC.Items[n].SubItems.Add(v.GiamGia.ToString());
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        VoucherBUS bus = new VoucherBUS();
        bus.Xoa(txtMaVoucher.Text);
        ds = bus.DocDSVC();
        lvDSVC.Items.Clear();
        foreach (VoucherDTO v in ds)
        {
            int n = lvDSVC.Items.Count;
            lvDSVC.Items.Add(v.MaVoucher);
            lvDSVC.Items[n].SubItems.Add(v.MaCTKM);
            lvDSVC.Items[n].SubItems.Add(v.GiamGia.ToString());
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSVC.SelectedIndices.Clear();
        txtMaVoucher.Clear();
        cboMaCTKM.SelectedIndex = -1;
        txtGiamGia.Clear();
        txtMaVoucher.Enabled = true;
    }

    private void lvDSVC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSVC.SelectedIndices.Count == 0) return;
        int i = lvDSVC.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaVoucher.Text = ds[i].MaVoucher;
        cboMaCTKM.SelectedValue = ds[i].MaCTKM;
        txtGiamGia.Text = ds[i].GiamGia.ToString();
        txtMaVoucher.Enabled = false;
    }
}
