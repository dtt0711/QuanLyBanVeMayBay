using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmPhieuChi : Form
{
    private List<PhieuChiDTO> ds = new List<PhieuChiDTO>();

    public FrmPhieuChi()
    {
        InitializeComponent();
        cboMaCB.Format += (s, e) => { if (e.ListItem is ChuyenBayDTO p) e.Value = p.MaCB + " - " + p.TuyenBay; };
    }

    private void FrmPhieuChi_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSPC_Click(sender, e);
    }

    private void NapCombo()
    {
        cboMaCB.DataSource = new ChuyenBayBUS().DocDSCB();
        cboMaCB.DisplayMember = "MaCB";
        cboMaCB.ValueMember = "MaCB";
        cboMaCB.SelectedIndex = -1;
    }

    private void btnDocDSPC_Click(object sender, EventArgs e)
    {
        PhieuChiBUS bus = new PhieuChiBUS();
        ds = bus.DocDSPC();
        lvDSPC.Items.Clear();
        foreach (PhieuChiDTO pc in ds)
        {
            int n = lvDSPC.Items.Count;
            lvDSPC.Items.Add(pc.MaPC);
            lvDSPC.Items[n].SubItems.Add(pc.NgayChi.ToString("dd/MM/yyyy"));
            lvDSPC.Items[n].SubItems.Add(pc.MaCB);
            lvDSPC.Items[n].SubItems.Add(pc.NoiDung);
            lvDSPC.Items[n].SubItems.Add(pc.SoTien.ToString("N0"));
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        PhieuChiDTO pc = new PhieuChiDTO(txtMaPC.Text, dtpNgayChi.Value, cboMaCB.SelectedValue.ToString(), txtNoiDung.Text, decimal.Parse(txtSoTien.Text));
        PhieuChiBUS bus = new PhieuChiBUS();
        bus.Them(pc);
        ds.Add(pc);
        int n = lvDSPC.Items.Count;
        lvDSPC.Items.Add(pc.MaPC);
        lvDSPC.Items[n].SubItems.Add(pc.NgayChi.ToString("dd/MM/yyyy"));
        lvDSPC.Items[n].SubItems.Add(pc.MaCB);
        lvDSPC.Items[n].SubItems.Add(pc.NoiDung);
        lvDSPC.Items[n].SubItems.Add(pc.SoTien.ToString("N0"));
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        PhieuChiDTO pc = new PhieuChiDTO(txtMaPC.Text, dtpNgayChi.Value, cboMaCB.SelectedValue.ToString(), txtNoiDung.Text, decimal.Parse(txtSoTien.Text));
        PhieuChiBUS bus = new PhieuChiBUS();
        bus.Sua(pc);
        ds = bus.DocDSPC();
        lvDSPC.Items.Clear();
        foreach (PhieuChiDTO p in ds)
        {
            int n = lvDSPC.Items.Count;
            lvDSPC.Items.Add(p.MaPC);
            lvDSPC.Items[n].SubItems.Add(p.NgayChi.ToString("dd/MM/yyyy"));
            lvDSPC.Items[n].SubItems.Add(p.MaCB);
            lvDSPC.Items[n].SubItems.Add(p.NoiDung);
            lvDSPC.Items[n].SubItems.Add(p.SoTien.ToString("N0"));
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        PhieuChiBUS bus = new PhieuChiBUS();
        bus.Xoa(txtMaPC.Text);
        ds = bus.DocDSPC();
        lvDSPC.Items.Clear();
        foreach (PhieuChiDTO p in ds)
        {
            int n = lvDSPC.Items.Count;
            lvDSPC.Items.Add(p.MaPC);
            lvDSPC.Items[n].SubItems.Add(p.NgayChi.ToString("dd/MM/yyyy"));
            lvDSPC.Items[n].SubItems.Add(p.MaCB);
            lvDSPC.Items[n].SubItems.Add(p.NoiDung);
            lvDSPC.Items[n].SubItems.Add(p.SoTien.ToString("N0"));
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSPC.SelectedIndices.Clear();
        txtMaPC.Clear();
        dtpNgayChi.Value = DateTime.Now;
        cboMaCB.SelectedIndex = -1;
        txtNoiDung.Clear();
        txtSoTien.Clear();
        txtMaPC.Enabled = true;
    }

    private void lvDSPC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSPC.SelectedIndices.Count == 0) return;
        int i = lvDSPC.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaPC.Text = ds[i].MaPC;
        dtpNgayChi.Value = ds[i].NgayChi;
        cboMaCB.SelectedValue = ds[i].MaCB;
        txtNoiDung.Text = ds[i].NoiDung;
        txtSoTien.Text = ds[i].SoTien.ToString();
        txtMaPC.Enabled = false;
    }
}
