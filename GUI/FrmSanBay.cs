using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmSanBay : Form
{
    private List<SanBayDTO> ds = new List<SanBayDTO>();

    public FrmSanBay()
    {
        InitializeComponent();
    }

    private void FrmSanBay_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSSB_Click(sender, e);
    }

    private void NapDong(SanBayDTO sb)
    {
        int n = lvDSSB.Items.Count;
        lvDSSB.Items.Add(sb.MaSanBay);
        lvDSSB.Items[n].SubItems.Add(sb.Ten);
        lvDSSB.Items[n].SubItems.Add(sb.LoaiSanBay);
    }

    private void btnDocDSSB_Click(object sender, EventArgs e)
    {
        HienThi(new SanBayBUS().DocDSSB());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        SanBayDTO sb = new SanBayDTO(txtMaSanBay.Text, txtTen.Text, txtLoaiSanBay.Text);
        SanBayBUS bus = new SanBayBUS();
        if (txtMaSanBay.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaSanBay.Text))
        {
            MessageBox.Show("Mã " + txtMaSanBay.Text + " đã tồn tại.");
            return;
        }
        string loi = bus.KiemTra(sb);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(sb))
            return;
        btnDocDSSB_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        SanBayDTO sb = new SanBayDTO(txtMaSanBay.Text, txtTen.Text, txtLoaiSanBay.Text);
        SanBayBUS bus = new SanBayBUS();
        string loi = bus.KiemTra(sb);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(sb);
        HienThi(bus.DocDSSB());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        SanBayBUS bus = new SanBayBUS();
        bus.Xoa(txtMaSanBay.Text);
        HienThi(bus.DocDSSB());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSSB.SelectedIndices.Clear();
        txtMaSanBay.Clear();
        txtTen.Clear();
        txtLoaiSanBay.Clear();
        txtMaSanBay.Enabled = true;
    }

    private void HienThi(List<SanBayDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSSB.Items.Clear();
        foreach (SanBayDTO x in ds)
            NapDong(x);
        thanhTim.HienSoDong(ds.Count);
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new SanBayBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(SanBayDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new SanBayBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        HienThi(new SanBayBUS().DocDSSB());
    }

    private void lvDSSB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSSB.SelectedIndices.Count == 0) return;
        int i = lvDSSB.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaSanBay.Text = ds[i].MaSanBay;
        txtTen.Text = ds[i].Ten;
        txtLoaiSanBay.Text = ds[i].LoaiSanBay;
        txtMaSanBay.Enabled = false;
    }
}
