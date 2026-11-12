using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmHangHangKhong : Form
{
    private List<HangHangKhongDTO> ds = new List<HangHangKhongDTO>();

    public FrmHangHangKhong()
    {
        InitializeComponent();
    }

    private void FrmHangHangKhong_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSHHK_Click(sender, e);
    }

    private void btnDocDSHHK_Click(object sender, EventArgs e)
    {
        HangHangKhongBUS bus = new HangHangKhongBUS();
        ds = bus.DocDSHHK();
        lvDSHHK.Items.Clear();
        foreach (HangHangKhongDTO hhk in ds)
        {
            int n = lvDSHHK.Items.Count;
            lvDSHHK.Items.Add(hhk.MaHHK);
            lvDSHHK.Items[n].SubItems.Add(hhk.TenHang);
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        HangHangKhongDTO hhk = new HangHangKhongDTO(txtMaHHK.Text, txtTenHang.Text);
        HangHangKhongBUS bus = new HangHangKhongBUS();
        bus.Them(hhk);
        ds.Add(hhk);
        int n = lvDSHHK.Items.Count;
        lvDSHHK.Items.Add(hhk.MaHHK);
        lvDSHHK.Items[n].SubItems.Add(hhk.TenHang);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        HangHangKhongDTO hhk = new HangHangKhongDTO(txtMaHHK.Text, txtTenHang.Text);
        HangHangKhongBUS bus = new HangHangKhongBUS();
        bus.Sua(hhk);
        ds = bus.DocDSHHK();
        lvDSHHK.Items.Clear();
        foreach (HangHangKhongDTO h in ds)
        {
            int n = lvDSHHK.Items.Count;
            lvDSHHK.Items.Add(h.MaHHK);
            lvDSHHK.Items[n].SubItems.Add(h.TenHang);
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        HangHangKhongBUS bus = new HangHangKhongBUS();
        bus.Xoa(txtMaHHK.Text);
        ds = bus.DocDSHHK();
        lvDSHHK.Items.Clear();
        foreach (HangHangKhongDTO h in ds)
        {
            int n = lvDSHHK.Items.Count;
            lvDSHHK.Items.Add(h.MaHHK);
            lvDSHHK.Items[n].SubItems.Add(h.TenHang);
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSHHK.SelectedIndices.Clear();
        txtMaHHK.Clear();
        txtTenHang.Clear();
        txtMaHHK.Enabled = true;
    }

    private void btnXemMB_Click(object sender, EventArgs e)
    {
        MayBayBUS bus = new MayBayBUS();
        new FrmXemDS("Máy bay của hãng " + txtMaHHK.Text, bus.DocTheoHHK(txtMaHHK.Text)).ShowDialog();
    }

    private void lvDSHHK_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSHHK.SelectedIndices.Count == 0) return;
        int i = lvDSHHK.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaHHK.Text = ds[i].MaHHK;
        txtTenHang.Text = ds[i].TenHang;
        txtMaHHK.Enabled = false;
    }
}
