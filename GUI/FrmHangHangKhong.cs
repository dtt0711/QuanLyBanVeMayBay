using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

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

    private void NapDong(HangHangKhongDTO hhk)
    {
        int n = lvDSHHK.Items.Count;
        lvDSHHK.Items.Add(hhk.MaHHK);
        lvDSHHK.Items[n].SubItems.Add(hhk.TenHang);
    }

    private void HienThi(List<HangHangKhongDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSHHK.Items.Clear();
        foreach (HangHangKhongDTO hhk in ds)
            NapDong(hhk);
        thanhTim.HienSoDong(ds.Count);
    }

    private void btnDocDSHHK_Click(object sender, EventArgs e)
    {
        HienThi(new HangHangKhongBUS().DocDSHHK());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        HangHangKhongDTO hhk = new HangHangKhongDTO(txtMaHHK.Text, txtTenHang.Text);
        HangHangKhongBUS bus = new HangHangKhongBUS();
        if (txtMaHHK.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaHHK.Text))
        {
            MessageBox.Show("Mã " + txtMaHHK.Text + " đã tồn tại.");
            return;
        }
        string loi = bus.KiemTra(hhk);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(hhk))
            return;
        btnDocDSHHK_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        HangHangKhongDTO hhk = new HangHangKhongDTO(txtMaHHK.Text, txtTenHang.Text);
        HangHangKhongBUS bus = new HangHangKhongBUS();
        string loi = bus.KiemTra(hhk);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(hhk);
        HienThi(bus.DocDSHHK());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        HangHangKhongBUS bus = new HangHangKhongBUS();
        bus.Xoa(txtMaHHK.Text);
        HienThi(bus.DocDSHHK());
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

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new HangHangKhongBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(HangHangKhongDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new HangHangKhongBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSHHK_Click(sender, e);
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
