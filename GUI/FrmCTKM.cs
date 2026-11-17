using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

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

    private void NapDong(CTKMDTO ctkm)
    {
        int n = lvDSCTKM.Items.Count;
        lvDSCTKM.Items.Add(ctkm.MaCTKM);
        lvDSCTKM.Items[n].SubItems.Add(ctkm.TenCTKM);
        lvDSCTKM.Items[n].SubItems.Add(ctkm.NgayBatDau.ToString("dd/MM/yyyy"));
        lvDSCTKM.Items[n].SubItems.Add(ctkm.NgayKetThuc.ToString("dd/MM/yyyy"));
    }

    private void HienThi(List<CTKMDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSCTKM.Items.Clear();
        foreach (CTKMDTO ctkm in ds)
            NapDong(ctkm);
        thanhTim.HienSoDong(ds.Count);
    }

    private void btnDocDSCTKM_Click(object sender, EventArgs e)
    {
        HienThi(new CTKMBUS().DocDSCTKM());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        CTKMDTO ctkm = new CTKMDTO(txtMaCTKM.Text, txtTenCTKM.Text, dtpNgayBatDau.Value, dtpNgayKetThuc.Value);
        CTKMBUS bus = new CTKMBUS();
        if (txtMaCTKM.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaCTKM.Text))
        {
            MessageBox.Show("Mã " + txtMaCTKM.Text + " đã tồn tại.");
            return;
        }
        string loi = bus.KiemTra(ctkm);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(ctkm))
            return;
        btnDocDSCTKM_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        CTKMDTO ctkm = new CTKMDTO(txtMaCTKM.Text, txtTenCTKM.Text, dtpNgayBatDau.Value, dtpNgayKetThuc.Value);
        CTKMBUS bus = new CTKMBUS();
        string loi = bus.KiemTra(ctkm);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(ctkm);
        HienThi(bus.DocDSCTKM());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        CTKMBUS bus = new CTKMBUS();
        bus.Xoa(txtMaCTKM.Text);
        HienThi(bus.DocDSCTKM());
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

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new CTKMBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(CTKMDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new CTKMBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSCTKM_Click(sender, e);
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
