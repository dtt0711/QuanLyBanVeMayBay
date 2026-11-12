using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmChuyenBay : Form
{
    private List<ChuyenBayDTO> ds = new List<ChuyenBayDTO>();

    public FrmChuyenBay()
    {
        InitializeComponent();
        cboMaMayBay.Format += (s, e) => { if (e.ListItem is MayBayDTO p) e.Value = p.MaMayBay + " - " + p.LoaiMayBay; };
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
        cboMaMayBay.DataSource = new MayBayBUS().DocDSMB();
        cboMaMayBay.DisplayMember = "MaMayBay";
        cboMaMayBay.ValueMember = "MaMayBay";
        cboMaMayBay.SelectedIndex = -1;
    }

    private void btnDocDSCB_Click(object sender, EventArgs e)
    {
        ChuyenBayBUS bus = new ChuyenBayBUS();
        ds = bus.DocDSCB();
        lvDSCB.Items.Clear();
        foreach (ChuyenBayDTO cb in ds)
        {
            int n = lvDSCB.Items.Count;
            lvDSCB.Items.Add(cb.MaCB);
            lvDSCB.Items[n].SubItems.Add(cb.MaMayBay);
            lvDSCB.Items[n].SubItems.Add(cb.TuyenBay);
            lvDSCB.Items[n].SubItems.Add(cb.NgayBay.ToString("dd/MM/yyyy"));
            lvDSCB.Items[n].SubItems.Add(cb.GiaVe.ToString("N0"));
            lvDSCB.Items[n].SubItems.Add(cb.SoGheTrong.ToString());
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        MayBayDTO mayBay = (MayBayDTO)cboMaMayBay.SelectedItem;
        ChuyenBayDTO cb = new ChuyenBayDTO(txtMaCB.Text, mayBay.MaMayBay, txtTuyenBay.Text, dtpNgayBay.Value, decimal.Parse(txtGiaVe.Text), mayBay.SoGhe);
        ChuyenBayBUS bus = new ChuyenBayBUS();
        bus.Them(cb);
        ds.Add(cb);
        int n = lvDSCB.Items.Count;
        lvDSCB.Items.Add(cb.MaCB);
        lvDSCB.Items[n].SubItems.Add(cb.MaMayBay);
        lvDSCB.Items[n].SubItems.Add(cb.TuyenBay);
        lvDSCB.Items[n].SubItems.Add(cb.NgayBay.ToString("dd/MM/yyyy"));
        lvDSCB.Items[n].SubItems.Add(cb.GiaVe.ToString("N0"));
        lvDSCB.Items[n].SubItems.Add(cb.SoGheTrong.ToString());
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        MayBayDTO mayBay = (MayBayDTO)cboMaMayBay.SelectedItem;
        ChuyenBayDTO cb = new ChuyenBayDTO(txtMaCB.Text, mayBay.MaMayBay, txtTuyenBay.Text, dtpNgayBay.Value, decimal.Parse(txtGiaVe.Text), mayBay.SoGhe);
        ChuyenBayBUS bus = new ChuyenBayBUS();
        bus.Sua(cb);
        ds = bus.DocDSCB();
        lvDSCB.Items.Clear();
        foreach (ChuyenBayDTO c in ds)
        {
            int n = lvDSCB.Items.Count;
            lvDSCB.Items.Add(c.MaCB);
            lvDSCB.Items[n].SubItems.Add(c.MaMayBay);
            lvDSCB.Items[n].SubItems.Add(c.TuyenBay);
            lvDSCB.Items[n].SubItems.Add(c.NgayBay.ToString("dd/MM/yyyy"));
            lvDSCB.Items[n].SubItems.Add(c.GiaVe.ToString("N0"));
            lvDSCB.Items[n].SubItems.Add(c.SoGheTrong.ToString());
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        ChuyenBayBUS bus = new ChuyenBayBUS();
        bus.Xoa(txtMaCB.Text);
        ds = bus.DocDSCB();
        lvDSCB.Items.Clear();
        foreach (ChuyenBayDTO c in ds)
        {
            int n = lvDSCB.Items.Count;
            lvDSCB.Items.Add(c.MaCB);
            lvDSCB.Items[n].SubItems.Add(c.MaMayBay);
            lvDSCB.Items[n].SubItems.Add(c.TuyenBay);
            lvDSCB.Items[n].SubItems.Add(c.NgayBay.ToString("dd/MM/yyyy"));
            lvDSCB.Items[n].SubItems.Add(c.GiaVe.ToString("N0"));
            lvDSCB.Items[n].SubItems.Add(c.SoGheTrong.ToString());
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSCB.SelectedIndices.Clear();
        txtMaCB.Clear();
        cboMaMayBay.SelectedIndex = -1;
        txtTuyenBay.Clear();
        dtpNgayBay.Value = DateTime.Now;
        txtGiaVe.Clear();
        txtMaCB.Enabled = true;
        cboMaMayBay.Enabled = true;
    }

    private void btnXemPC_Click(object sender, EventArgs e)
    {
        PhieuChiBUS bus = new PhieuChiBUS();
        new FrmXemDS("Phiếu chi của chuyến bay " + txtMaCB.Text, bus.DocTheoCB(txtMaCB.Text)).ShowDialog();
    }

    private void btnXemHD_Click(object sender, EventArgs e)
    {
        CTHDBUS bus = new CTHDBUS();
        new FrmXemDS("Chi tiết hóa đơn của chuyến bay " + txtMaCB.Text, bus.DocTheoCB(txtMaCB.Text)).ShowDialog();
    }

    private void lvDSCB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSCB.SelectedIndices.Count == 0) return;
        int i = lvDSCB.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaCB.Text = ds[i].MaCB;
        cboMaMayBay.SelectedValue = ds[i].MaMayBay;
        txtTuyenBay.Text = ds[i].TuyenBay;
        dtpNgayBay.Value = ds[i].NgayBay;
        txtGiaVe.Text = ds[i].GiaVe.ToString();
        txtMaCB.Enabled = false;
        cboMaMayBay.Enabled = false;
    }
}
