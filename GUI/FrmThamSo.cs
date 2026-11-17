using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

// Bảng tham số: các giới hạn mà BUS dùng để kiểm tra dữ liệu (số vé tối đa, tuổi trẻ em, ...).
// Đổi giá trị ở đây thì quy định trong chương trình đổi theo, không phải sửa code.
public partial class FrmThamSo : Form
{
    private List<ThamSoDTO> ds = new List<ThamSoDTO>();

    public FrmThamSo()
    {
        InitializeComponent();
    }

    private void FrmThamSo_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        btnDocDSTS_Click(sender, e);
    }

    private void NapDong(ThamSoDTO ts)
    {
        int n = lvDSTS.Items.Count;
        lvDSTS.Items.Add(ts.MaThamSo);
        lvDSTS.Items[n].SubItems.Add(ts.TenThamSo);
        lvDSTS.Items[n].SubItems.Add(ts.GiaTri.ToString("N0"));
        lvDSTS.Items[n].SubItems.Add(ts.DonVi);
    }

    private void HienThi(List<ThamSoDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSTS.Items.Clear();
        foreach (ThamSoDTO ts in ds)
            NapDong(ts);
        thanhTim.HienSoDong(ds.Count);
    }

    private ThamSoDTO LayTuForm()
    {
        if (!decimal.TryParse(txtGiaTri.Text, out decimal giaTri))
        {
            MessageBox.Show("Giá trị phải là số.");
            return null;
        }
        ThamSoDTO ts = new ThamSoDTO(txtMaThamSo.Text.Trim().ToUpper(), txtTenThamSo.Text, giaTri, txtDonVi.Text);
        string loi = new ThamSoBUS().KiemTra(ts);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return null;
        }
        return ts;
    }

    private void btnDocDSTS_Click(object sender, EventArgs e)
    {
        HienThi(new ThamSoBUS().DocDSTS());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        ThamSoDTO ts = LayTuForm();
        if (ts == null) return;
        ThamSoBUS bus = new ThamSoBUS();
        if (ts.MaThamSo == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(ts.MaThamSo))
        {
            MessageBox.Show("Mã " + ts.MaThamSo + " đã tồn tại.");
            return;
        }
        if (!bus.Them(ts))
            return;
        btnDocDSTS_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        ThamSoDTO ts = LayTuForm();
        if (ts == null) return;
        ThamSoBUS bus = new ThamSoBUS();
        bus.Sua(ts);
        HienThi(bus.DocDSTS());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        ThamSoBUS bus = new ThamSoBUS();
        bus.Xoa(txtMaThamSo.Text);
        HienThi(bus.DocDSTS());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSTS.SelectedIndices.Clear();
        txtMaThamSo.Clear();
        txtTenThamSo.Clear();
        txtGiaTri.Clear();
        txtDonVi.Clear();
        txtMaThamSo.Enabled = true;
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new ThamSoBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(ThamSoDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new ThamSoBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSTS_Click(sender, e);
    }

    private void lvDSTS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSTS.SelectedIndices.Count == 0) return;
        int i = lvDSTS.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaThamSo.Text = ds[i].MaThamSo;
        txtTenThamSo.Text = ds[i].TenThamSo;
        txtGiaTri.Text = ds[i].GiaTri.ToString("0.##");
        txtDonVi.Text = ds[i].DonVi;
        txtMaThamSo.Enabled = false;
    }
}
