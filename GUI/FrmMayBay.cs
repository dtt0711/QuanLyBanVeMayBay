using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmMayBay : Form
{
    private List<MayBayDTO> ds = new List<MayBayDTO>();

    public FrmMayBay()
    {
        InitializeComponent();
        cboMaHHK.Format += (s, e) => { if (e.ListItem is HangHangKhongDTO p) e.Value = p.TenHang == "" ? p.MaHHK : p.MaHHK + " - " + p.TenHang; };
        GiaoDien.ThemNutChon(cboMaHHK, "Chọn hãng hàng không");
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
        List<HangHangKhongDTO> hang = new List<HangHangKhongDTO>();
        hang.Add(new HangHangKhongDTO { MaHHK = "Chọn hãng hàng không" });
        hang.AddRange(new HangHangKhongBUS().DocDSHHK());
        cboMaHHK.DataSource = hang;
        cboMaHHK.DisplayMember = "MaHHK";
        cboMaHHK.ValueMember = "MaHHK";
        cboMaHHK.SelectedIndex = 0;
    }

    private void NapDong(MayBayDTO mb)
    {
        int n = lvDSMB.Items.Count;
        lvDSMB.Items.Add(mb.MaMayBay);
        lvDSMB.Items[n].SubItems.Add(mb.MaHHK);
    }

    private void btnDocDSMB_Click(object sender, EventArgs e)
    {
        HienThi(new MayBayBUS().DocDSMB());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        if (cboMaHHK.SelectedValue == null || cboMaHHK.SelectedValue.ToString().StartsWith("Chọn "))
        {
            MessageBox.Show("Chọn hãng hàng không.");
            return;
        }
        MayBayDTO mb = new MayBayDTO(txtMaMayBay.Text, cboMaHHK.SelectedValue.ToString(), "", 0);
        MayBayBUS bus = new MayBayBUS();
        if (txtMaMayBay.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaMayBay.Text))
        {
            MessageBox.Show("Mã " + txtMaMayBay.Text + " đã tồn tại.");
            return;
        }
        if (!bus.Them(mb))
            return;
        btnDocDSMB_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        if (cboMaHHK.SelectedValue == null || cboMaHHK.SelectedValue.ToString().StartsWith("Chọn "))
        {
            MessageBox.Show("Chọn hãng hàng không.");
            return;
        }
        MayBayDTO mb = new MayBayDTO(txtMaMayBay.Text, cboMaHHK.SelectedValue.ToString(), "", 0);
        MayBayBUS bus = new MayBayBUS();
        bus.Sua(mb);
        HienThi(bus.DocDSMB());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        MayBayBUS bus = new MayBayBUS();
        bus.Xoa(txtMaMayBay.Text);
        HienThi(bus.DocDSMB());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSMB.SelectedIndices.Clear();
        txtMaMayBay.Clear();
        cboMaHHK.SelectedIndex = 0;
        txtMaMayBay.Enabled = true;
    }

    private void btnXemCB_Click(object sender, EventArgs e)
    {
        ChuyenBayBUS bus = new ChuyenBayBUS();
        new FrmXemDS("Chuyến bay của máy bay " + txtMaMayBay.Text, bus.DocTheoMayBay(txtMaMayBay.Text)).ShowDialog();
    }

    private void HienThi(List<MayBayDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSMB.Items.Clear();
        foreach (MayBayDTO x in ds)
            NapDong(x);
        thanhTim.HienSoDong(ds.Count);
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new MayBayBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(MayBayDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new MayBayBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        HienThi(new MayBayBUS().DocDSMB());
    }

    private void lvDSMB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSMB.SelectedIndices.Count == 0) return;
        int i = lvDSMB.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaMayBay.Text = ds[i].MaMayBay;
        cboMaHHK.SelectedValue = ds[i].MaHHK;
        txtMaMayBay.Enabled = false;
    }
}
