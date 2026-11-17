using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmGhe : Form
{
    private List<GheDTO> ds = new List<GheDTO>();

    public FrmGhe()
    {
        InitializeComponent();
        cboMaMayBay.Format += (s, e) => { if (e.ListItem is MayBayDTO p) e.Value = p.MaHHK == "" ? p.MaMayBay : p.MaMayBay + " - " + p.MaHHK; };
        cboMaHV.Format += (s, e) => { if (e.ListItem is HangVeDTO p) e.Value = p.TenHV == "" ? p.MaHV : p.MaHV + " - " + p.TenHV; };
        GiaoDien.ThemNutChon(cboMaMayBay, "Chọn máy bay");
        GiaoDien.ThemNutChon(cboMaHV, "Chọn hạng vé");
    }

    private void FrmGhe_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSGhe_Click(sender, e);
    }

    private void NapCombo()
    {
        List<MayBayDTO> mayBay = new List<MayBayDTO>();
        mayBay.Add(new MayBayDTO { MaMayBay = "Chọn mã máy bay" });
        mayBay.AddRange(new MayBayBUS().DocDSMB());
        cboMaMayBay.DataSource = mayBay;
        cboMaMayBay.DisplayMember = "MaMayBay";
        cboMaMayBay.ValueMember = "MaMayBay";
        cboMaMayBay.SelectedIndex = 0;
        List<HangVeDTO> hangVe = new List<HangVeDTO>();
        hangVe.Add(new HangVeDTO { MaHV = "Chọn hạng vé" });
        hangVe.AddRange(new HangVeBUS().DocDSHV());
        cboMaHV.DataSource = hangVe;
        cboMaHV.DisplayMember = "MaHV";
        cboMaHV.ValueMember = "MaHV";
        cboMaHV.SelectedIndex = 0;
    }

    private static string GiaTri(ComboBox cbo)
    {
        string v = cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        return v.StartsWith("Chọn ") ? "" : v;
    }

    private void NapDong(GheDTO g)
    {
        int n = lvDSGhe.Items.Count;
        lvDSGhe.Items.Add(g.MaGhe);
        lvDSGhe.Items[n].SubItems.Add(g.MaMayBay);
        lvDSGhe.Items[n].SubItems.Add(g.MaHV);
    }

    private void btnDocDSGhe_Click(object sender, EventArgs e)
    {
        HienThi(new GheBUS().DocDSGhe());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        if (GiaTri(cboMaMayBay) == "" || GiaTri(cboMaHV) == "")
        {
            MessageBox.Show("Chọn máy bay và hạng vé.");
            return;
        }
        GheDTO g = new GheDTO(txtMaGhe.Text, GiaTri(cboMaMayBay), GiaTri(cboMaHV));
        GheBUS bus = new GheBUS();
        if (txtMaGhe.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaGhe.Text))
        {
            MessageBox.Show("Mã " + txtMaGhe.Text + " đã tồn tại.");
            return;
        }
        if (!bus.Them(g))
            return;
        btnDocDSGhe_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        if (GiaTri(cboMaMayBay) == "" || GiaTri(cboMaHV) == "")
        {
            MessageBox.Show("Chọn máy bay và hạng vé.");
            return;
        }
        GheDTO g = new GheDTO(txtMaGhe.Text, GiaTri(cboMaMayBay), GiaTri(cboMaHV));
        GheBUS bus = new GheBUS();
        string loi = bus.KiemTra(g, false);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(g);
        HienThi(bus.DocDSGhe());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        GheBUS bus = new GheBUS();
        bus.Xoa(txtMaGhe.Text);
        HienThi(bus.DocDSGhe());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSGhe.SelectedIndices.Clear();
        txtMaGhe.Clear();
        cboMaMayBay.SelectedIndex = 0;
        cboMaHV.SelectedIndex = 0;
        txtMaGhe.Enabled = true;
    }

    private void HienThi(List<GheDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSGhe.Items.Clear();
        foreach (GheDTO x in ds)
            NapDong(x);
        thanhTim.HienSoDong(ds.Count);
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new GheBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(GheDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new GheBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        HienThi(new GheBUS().DocDSGhe());
    }

    private void lvDSGhe_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSGhe.SelectedIndices.Count == 0) return;
        int i = lvDSGhe.SelectedIndices[0];
        if (i >= ds.Count) return;
        txtMaGhe.Text = ds[i].MaGhe;
        cboMaMayBay.SelectedValue = ds[i].MaMayBay;
        cboMaHV.SelectedValue = ds[i].MaHV;
        txtMaGhe.Enabled = false;
    }
}
