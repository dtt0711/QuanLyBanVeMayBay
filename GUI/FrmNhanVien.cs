using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmNhanVien : Form
{
    private List<NhanVienDTO> ds = new List<NhanVienDTO>();

    public FrmNhanVien()
    {
        InitializeComponent();
    }

    private void FrmNhanVien_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSNV_Click(sender, e);
    }

    private void NapDong(NhanVienDTO nv)
    {
        int n = lvDSNV.Items.Count;
        lvDSNV.Items.Add(nv.MaNV);
        lvDSNV.Items[n].SubItems.Add(nv.Ho);
        lvDSNV.Items[n].SubItems.Add(nv.Ten);
        lvDSNV.Items[n].SubItems.Add(nv.ChucVu);
    }

    private void HienThi(List<NhanVienDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSNV.Items.Clear();
        foreach (NhanVienDTO nv in ds)
            NapDong(nv);
        thanhTim.HienSoDong(ds.Count);
    }

    private void btnDocDSNV_Click(object sender, EventArgs e)
    {
        HienThi(new NhanVienBUS().DocDSNV());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        NhanVienDTO nv = new NhanVienDTO(txtMaNV.Text, txtHo.Text, txtTen.Text, txtChucVu.Text);
        NhanVienBUS bus = new NhanVienBUS();
        if (txtMaNV.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaNV.Text))
        {
            MessageBox.Show("Mã " + txtMaNV.Text + " đã tồn tại.");
            return;
        }
        string loi = bus.KiemTra(nv);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(nv))
            return;
        btnDocDSNV_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        NhanVienDTO nv = new NhanVienDTO(txtMaNV.Text, txtHo.Text, txtTen.Text, txtChucVu.Text);
        NhanVienBUS bus = new NhanVienBUS();
        string loi = bus.KiemTra(nv);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(nv);
        HienThi(bus.DocDSNV());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        NhanVienBUS bus = new NhanVienBUS();
        bus.Xoa(txtMaNV.Text);
        HienThi(bus.DocDSNV());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSNV.SelectedIndices.Clear();
        txtMaNV.Clear();
        txtHo.Clear();
        txtTen.Clear();
        txtChucVu.Clear();
        txtMaNV.Enabled = true;
    }

    private void btnXemHD_Click(object sender, EventArgs e)
    {
        HoaDonBUS bus = new HoaDonBUS();
        new FrmXemDS("Hóa đơn của NV " + txtMaNV.Text, bus.DocTheoNV(txtMaNV.Text)).ShowDialog();
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new NhanVienBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(NhanVienDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new NhanVienBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSNV_Click(sender, e);
    }

    private void lvDSNV_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSNV.SelectedIndices.Count == 0) return;
        int i = lvDSNV.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaNV.Text = ds[i].MaNV;
        txtHo.Text = ds[i].Ho;
        txtTen.Text = ds[i].Ten;
        txtChucVu.Text = ds[i].ChucVu;
        txtMaNV.Enabled = false;
    }
}
