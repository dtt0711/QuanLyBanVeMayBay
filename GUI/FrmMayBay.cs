using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmMayBay : Form
{
    private List<MayBayDTO> ds = new List<MayBayDTO>();

    public FrmMayBay()
    {
        InitializeComponent();
        cboMaHHK.Format += (s, e) => { if (e.ListItem is HangHangKhongDTO p) e.Value = p.MaHHK + " - " + p.TenHang; };
    }

    private void FrmMayBay_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSMB_Click(sender, e);
    }

    private void NapCombo()
    {
        cboMaHHK.DataSource = new HangHangKhongBUS().DocDSHHK();
        cboMaHHK.DisplayMember = "MaHHK";
        cboMaHHK.ValueMember = "MaHHK";
        cboMaHHK.SelectedIndex = -1;
    }

    private void btnDocDSMB_Click(object sender, EventArgs e)
    {
        MayBayBUS bus = new MayBayBUS();
        ds = bus.DocDSMB();
        lvDSMB.Items.Clear();
        foreach (MayBayDTO mb in ds)
        {
            int n = lvDSMB.Items.Count;
            lvDSMB.Items.Add(mb.MaMayBay);
            lvDSMB.Items[n].SubItems.Add(mb.MaHHK);
            lvDSMB.Items[n].SubItems.Add(mb.LoaiMayBay);
            lvDSMB.Items[n].SubItems.Add(mb.SoGhe.ToString());
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        MayBayDTO mb = new MayBayDTO(txtMaMayBay.Text, cboMaHHK.SelectedValue.ToString(), txtLoaiMayBay.Text, int.Parse(txtSoGhe.Text));
        MayBayBUS bus = new MayBayBUS();
        bus.Them(mb);
        ds.Add(mb);
        int n = lvDSMB.Items.Count;
        lvDSMB.Items.Add(mb.MaMayBay);
        lvDSMB.Items[n].SubItems.Add(mb.MaHHK);
        lvDSMB.Items[n].SubItems.Add(mb.LoaiMayBay);
        lvDSMB.Items[n].SubItems.Add(mb.SoGhe.ToString());
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        MayBayDTO mb = new MayBayDTO(txtMaMayBay.Text, cboMaHHK.SelectedValue.ToString(), txtLoaiMayBay.Text, int.Parse(txtSoGhe.Text));
        MayBayBUS bus = new MayBayBUS();
        bus.Sua(mb);
        ds = bus.DocDSMB();
        lvDSMB.Items.Clear();
        foreach (MayBayDTO m in ds)
        {
            int n = lvDSMB.Items.Count;
            lvDSMB.Items.Add(m.MaMayBay);
            lvDSMB.Items[n].SubItems.Add(m.MaHHK);
            lvDSMB.Items[n].SubItems.Add(m.LoaiMayBay);
            lvDSMB.Items[n].SubItems.Add(m.SoGhe.ToString());
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        MayBayBUS bus = new MayBayBUS();
        bus.Xoa(txtMaMayBay.Text);
        ds = bus.DocDSMB();
        lvDSMB.Items.Clear();
        foreach (MayBayDTO m in ds)
        {
            int n = lvDSMB.Items.Count;
            lvDSMB.Items.Add(m.MaMayBay);
            lvDSMB.Items[n].SubItems.Add(m.MaHHK);
            lvDSMB.Items[n].SubItems.Add(m.LoaiMayBay);
            lvDSMB.Items[n].SubItems.Add(m.SoGhe.ToString());
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSMB.SelectedIndices.Clear();
        txtMaMayBay.Clear();
        cboMaHHK.SelectedIndex = -1;
        txtLoaiMayBay.Clear();
        txtSoGhe.Clear();
        txtMaMayBay.Enabled = true;
    }

    private void btnXemCB_Click(object sender, EventArgs e)
    {
        ChuyenBayBUS bus = new ChuyenBayBUS();
        new FrmXemDS("Chuyến bay của máy bay " + txtMaMayBay.Text, bus.DocTheoMayBay(txtMaMayBay.Text)).ShowDialog();
    }

    private void lvDSMB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSMB.SelectedIndices.Count == 0) return;
        int i = lvDSMB.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaMayBay.Text = ds[i].MaMayBay;
        cboMaHHK.SelectedValue = ds[i].MaHHK;
        txtLoaiMayBay.Text = ds[i].LoaiMayBay;
        txtSoGhe.Text = ds[i].SoGhe.ToString();
        txtMaMayBay.Enabled = false;
    }
}
