using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmHangVe : Form
{
    private List<HangVeDTO> ds = new List<HangVeDTO>();

    public FrmHangVe()
    {
        InitializeComponent();
    }

    private void FrmHangVe_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSHV_Click(sender, e);
    }

    private void NapDong(HangVeDTO hv)
    {
        int n = lvDSHV.Items.Count;
        lvDSHV.Items.Add(hv.MaHV);
        lvDSHV.Items[n].SubItems.Add(hv.TenHV);
        lvDSHV.Items[n].SubItems.Add(hv.HanhLyKg.ToString());
        lvDSHV.Items[n].SubItems.Add(hv.HeSoGia.ToString("0.00"));
    }

    private HangVeDTO LayTuForm()
    {
        int hanhLy = 0;
        int.TryParse(txtHanhLyKg.Text, out hanhLy);
        decimal heSo = 0;
        decimal.TryParse(txtHeSoGia.Text, out heSo);
        return new HangVeDTO(txtMaHV.Text, txtTenHV.Text, hanhLy, heSo);
    }

    private void btnDocDSHV_Click(object sender, EventArgs e)
    {
        HienThi(new HangVeBUS().DocDSHV());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        HangVeDTO hv = LayTuForm();
        HangVeBUS bus = new HangVeBUS();
        if (txtMaHV.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaHV.Text))
        {
            MessageBox.Show("Mã " + txtMaHV.Text + " đã tồn tại.");
            return;
        }
        string loi = bus.KiemTra(hv);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(hv))
            return;
        btnDocDSHV_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        HangVeDTO hv = LayTuForm();
        HangVeBUS bus = new HangVeBUS();
        string loi = bus.KiemTra(hv);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(hv);
        HienThi(bus.DocDSHV());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        HangVeBUS bus = new HangVeBUS();
        bus.Xoa(txtMaHV.Text);
        HienThi(bus.DocDSHV());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSHV.SelectedIndices.Clear();
        txtMaHV.Clear();
        txtTenHV.Clear();
        txtHanhLyKg.Clear();
        txtHeSoGia.Clear();
        txtMaHV.Enabled = true;
    }

    private void btnXemGhe_Click(object sender, EventArgs e)
    {
        GheBUS bus = new GheBUS();
        new FrmXemDS("Ghế hạng " + txtMaHV.Text, bus.DocTheoHV(txtMaHV.Text)).ShowDialog();
    }

    private void HienThi(List<HangVeDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSHV.Items.Clear();
        foreach (HangVeDTO x in ds)
            NapDong(x);
        thanhTim.HienSoDong(ds.Count);
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new HangVeBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(HangVeDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new HangVeBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        HienThi(new HangVeBUS().DocDSHV());
    }

    private void lvDSHV_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSHV.SelectedIndices.Count == 0) return;
        int i = lvDSHV.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaHV.Text = ds[i].MaHV;
        txtTenHV.Text = ds[i].TenHV;
        txtHanhLyKg.Text = ds[i].HanhLyKg.ToString();
        txtHeSoGia.Text = ds[i].HeSoGia.ToString("0.##");
        txtMaHV.Enabled = false;
    }
}
