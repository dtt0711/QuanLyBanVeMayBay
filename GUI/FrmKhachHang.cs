using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmKhachHang : Form
{
    private List<KhachHangDTO> ds = new List<KhachHangDTO>();

    public FrmKhachHang()
    {
        InitializeComponent();
    }

    private void FrmKhachHang_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSKH_Click(sender, e);
    }

    private void btnDocDSKH_Click(object sender, EventArgs e)
    {
        KhachHangBUS bus = new KhachHangBUS();
        ds = bus.DocDSKH();
        lvDSKH.Items.Clear();
        foreach (KhachHangDTO kh in ds)
        {
            int n = lvDSKH.Items.Count;
            lvDSKH.Items.Add(kh.MaKH);
            lvDSKH.Items[n].SubItems.Add(kh.HoTen);
            lvDSKH.Items[n].SubItems.Add(kh.CCCD);
            lvDSKH.Items[n].SubItems.Add(kh.SDT);
            lvDSKH.Items[n].SubItems.Add(kh.Email);
            lvDSKH.Items[n].SubItems.Add(kh.TaiKhoan);
            lvDSKH.Items[n].SubItems.Add(kh.DiaChi);
        }
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        KhachHangDTO kh = new KhachHangDTO(txtMaKH.Text, txtHoTen.Text, txtCCCD.Text, txtSDT.Text, txtEmail.Text, txtTaiKhoan.Text, txtMatKhau.Text, txtDiaChi.Text);
        KhachHangBUS bus = new KhachHangBUS();
        bus.Them(kh);
        ds.Add(kh);
        int n = lvDSKH.Items.Count;
        lvDSKH.Items.Add(kh.MaKH);
        lvDSKH.Items[n].SubItems.Add(kh.HoTen);
        lvDSKH.Items[n].SubItems.Add(kh.CCCD);
        lvDSKH.Items[n].SubItems.Add(kh.SDT);
        lvDSKH.Items[n].SubItems.Add(kh.Email);
        lvDSKH.Items[n].SubItems.Add(kh.TaiKhoan);
        lvDSKH.Items[n].SubItems.Add(kh.DiaChi);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        KhachHangDTO kh = new KhachHangDTO(txtMaKH.Text, txtHoTen.Text, txtCCCD.Text, txtSDT.Text, txtEmail.Text, txtTaiKhoan.Text, txtMatKhau.Text, txtDiaChi.Text);
        KhachHangBUS bus = new KhachHangBUS();
        bus.Sua(kh);
        ds = bus.DocDSKH();
        lvDSKH.Items.Clear();
        foreach (KhachHangDTO k in ds)
        {
            int n = lvDSKH.Items.Count;
            lvDSKH.Items.Add(k.MaKH);
            lvDSKH.Items[n].SubItems.Add(k.HoTen);
            lvDSKH.Items[n].SubItems.Add(k.CCCD);
            lvDSKH.Items[n].SubItems.Add(k.SDT);
            lvDSKH.Items[n].SubItems.Add(k.Email);
            lvDSKH.Items[n].SubItems.Add(k.TaiKhoan);
            lvDSKH.Items[n].SubItems.Add(k.DiaChi);
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        KhachHangBUS bus = new KhachHangBUS();
        bus.Xoa(txtMaKH.Text);
        ds = bus.DocDSKH();
        lvDSKH.Items.Clear();
        foreach (KhachHangDTO k in ds)
        {
            int n = lvDSKH.Items.Count;
            lvDSKH.Items.Add(k.MaKH);
            lvDSKH.Items[n].SubItems.Add(k.HoTen);
            lvDSKH.Items[n].SubItems.Add(k.CCCD);
            lvDSKH.Items[n].SubItems.Add(k.SDT);
            lvDSKH.Items[n].SubItems.Add(k.Email);
            lvDSKH.Items[n].SubItems.Add(k.TaiKhoan);
            lvDSKH.Items[n].SubItems.Add(k.DiaChi);
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSKH.SelectedIndices.Clear();
        txtMaKH.Clear();
        txtHoTen.Clear();
        txtCCCD.Clear();
        txtSDT.Clear();
        txtEmail.Clear();
        txtTaiKhoan.Clear();
        txtMatKhau.Clear();
        txtDiaChi.Clear();
        txtMaKH.Enabled = true;
    }

    private void btnXemCon_Click(object sender, EventArgs e)
    {
        HanhKhachBUS bus = new HanhKhachBUS();
        new FrmXemDS("Hành khách của KH " + txtMaKH.Text, bus.DocTheoKH(txtMaKH.Text)).ShowDialog();
    }

    private void btnXemHD_Click(object sender, EventArgs e)
    {
        HoaDonBUS bus = new HoaDonBUS();
        new FrmXemDS("Hóa đơn của KH " + txtMaKH.Text, bus.DocTheoKH(txtMaKH.Text)).ShowDialog();
    }

    private void lvDSKH_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSKH.SelectedIndices.Count == 0) return;
        int i = lvDSKH.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaKH.Text = ds[i].MaKH;
        txtHoTen.Text = ds[i].HoTen;
        txtCCCD.Text = ds[i].CCCD;
        txtSDT.Text = ds[i].SDT;
        txtEmail.Text = ds[i].Email;
        txtTaiKhoan.Text = ds[i].TaiKhoan;
        txtMatKhau.Text = ds[i].MatKhau;
        txtDiaChi.Text = ds[i].DiaChi;
        txtMaKH.Enabled = false;
    }
}
