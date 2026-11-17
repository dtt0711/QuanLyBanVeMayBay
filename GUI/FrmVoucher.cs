using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmVoucher : Form
{
    private List<VoucherDTO> ds = new List<VoucherDTO>();

    public FrmVoucher()
    {
        InitializeComponent();
        cboMaCTKM.Format += (s, e) => { if (e.ListItem is CTKMDTO p) e.Value = p.TenCTKM == "" ? p.MaCTKM : p.MaCTKM + " - " + p.TenCTKM; };
        GiaoDien.ThemNutChon(cboMaCTKM, "Chọn chương trình khuyến mãi");
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
        List<CTKMDTO> ctkm = new List<CTKMDTO>();
        ctkm.Add(new CTKMDTO { MaCTKM = "Chọn chương trình" });
        ctkm.AddRange(new CTKMBUS().DocDSCTKM());
        cboMaCTKM.DataSource = ctkm;
        cboMaCTKM.DisplayMember = "MaCTKM";
        cboMaCTKM.ValueMember = "MaCTKM";
        cboMaCTKM.SelectedIndex = 0;
    }

    private void NapDong(VoucherDTO vc)
    {
        int n = lvDSVC.Items.Count;
        lvDSVC.Items.Add(vc.MaVoucher);
        lvDSVC.Items[n].SubItems.Add(vc.MaCTKM);
        lvDSVC.Items[n].SubItems.Add(vc.GiamGia.ToString("N0"));
    }

    private void HienThi(List<VoucherDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSVC.Items.Clear();
        foreach (VoucherDTO vc in ds)
            NapDong(vc);
        thanhTim.HienSoDong(ds.Count);
    }

    // Đọc dữ liệu trên form; trả về null nếu thiếu hoặc sai kiểu
    private VoucherDTO LayTuForm()
    {
        if (cboMaCTKM.SelectedValue == null || cboMaCTKM.SelectedValue.ToString().StartsWith("Chọn "))
        {
            MessageBox.Show("Chọn chương trình khuyến mãi.");
            return null;
        }
        if (!double.TryParse(txtGiamGia.Text, out double giamGia))
        {
            MessageBox.Show("Mức giảm phải là số.");
            return null;
        }
        VoucherDTO vc = new VoucherDTO(txtMaVoucher.Text, cboMaCTKM.SelectedValue.ToString(), giamGia);
        string loi = new VoucherBUS().KiemTra(vc);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return null;
        }
        return vc;
    }

    private void btnDocDSVC_Click(object sender, EventArgs e)
    {
        HienThi(new VoucherBUS().DocDSVC());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        VoucherDTO vc = LayTuForm();
        if (vc == null) return;
        VoucherBUS bus = new VoucherBUS();
        if (txtMaVoucher.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaVoucher.Text))
        {
            MessageBox.Show("Mã " + txtMaVoucher.Text + " đã tồn tại.");
            return;
        }
        if (!bus.Them(vc))
            return;
        btnDocDSVC_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        VoucherDTO vc = LayTuForm();
        if (vc == null) return;
        VoucherBUS bus = new VoucherBUS();
        bus.Sua(vc);
        HienThi(bus.DocDSVC());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        VoucherBUS bus = new VoucherBUS();
        bus.Xoa(txtMaVoucher.Text);
        HienThi(bus.DocDSVC());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSVC.SelectedIndices.Clear();
        txtMaVoucher.Clear();
        cboMaCTKM.SelectedIndex = 0;
        txtGiamGia.Clear();
        txtMaVoucher.Enabled = true;
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new VoucherBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(VoucherDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new VoucherBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSVC_Click(sender, e);
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
