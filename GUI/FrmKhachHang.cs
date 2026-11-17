using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmKhachHang : Form
{
    private List<KhachHangDTO> ds = new List<KhachHangDTO>();

    public FrmKhachHang()
    {
        InitializeComponent();
        // Nhập Excel: mã chưa có thì thêm, mã đã có thì cập nhật; kiểm tra từng dòng như nhập tay
        thanhTim.ChoNhapExcel();
        thanhTim.NhapExcel += bang =>
        {
            KhachHangBUS bus = new KhachHangBUS();
            string kq = bus.NhapExcel(bang);
            MessageBox.Show(kq, "Nhập Excel");
            btnDocDSKH_Click(this, EventArgs.Empty);
        };
    }

    private void FrmKhachHang_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSKH_Click(sender, e);
    }

    private void NapDong(KhachHangDTO kh)
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

    private void HienThi(List<KhachHangDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSKH.Items.Clear();
        foreach (KhachHangDTO kh in ds)
            NapDong(kh);
        thanhTim.HienSoDong(ds.Count);
    }

    private KhachHangDTO LayTuForm()
    {
        return new KhachHangDTO(txtMaKH.Text, txtHoTen.Text, txtCCCD.Text.Trim(), txtSDT.Text, txtEmail.Text, txtTaiKhoan.Text, txtMatKhau.Text, txtDiaChi.Text);
    }

    private void btnDocDSKH_Click(object sender, EventArgs e)
    {
        HienThi(new KhachHangBUS().DocDSKH());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        KhachHangDTO kh = LayTuForm();
        KhachHangBUS bus = new KhachHangBUS();
        if (txtMaKH.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaKH.Text))
        {
            MessageBox.Show("Mã " + txtMaKH.Text + " đã tồn tại.");
            return;
        }
        string loi = bus.KiemTra(kh);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(kh))
            return;
        btnDocDSKH_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        KhachHangDTO kh = LayTuForm();
        KhachHangBUS bus = new KhachHangBUS();
        string loi = bus.KiemTra(kh);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(kh);
        HienThi(bus.DocDSKH());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        KhachHangBUS bus = new KhachHangBUS();
        bus.Xoa(txtMaKH.Text);
        HienThi(bus.DocDSKH());
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

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new KhachHangBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(KhachHangDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new KhachHangBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSKH_Click(sender, e);
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
