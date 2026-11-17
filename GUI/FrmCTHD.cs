using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmCTHD : Form
{
    private string maHD;

    public FrmCTHD()
    {
        InitializeComponent();
        cboMaCB.Format += (s, e) =>
        {
            if (e.ListItem is VeDTO p)
                e.Value = p.MaChuyenBay == "" ? p.MaVe : p.MaVe + " - " + p.MaChuyenBay;
        };
        GiaoDien.ThemNutChon(cboMaCB, "Chọn vé");
        // Một vé là một ghế của một hành khách nên số lượng luôn là 1
        txtSoLuong.Text = "1";
        GiaoDien.KhoaO(txtSoLuong);
    }

    public FrmCTHD(string maHD) : this()
    {
        this.maHD = maHD;
        Text = "Chi tiết hóa đơn " + maHD;
        Load += FrmCTHD_Load;
    }

    private void FrmCTHD_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSCTHD_Click(sender, e);
    }

    // Chỉ cho chọn vé của phiếu đặt này mà chưa được tính tiền
    private void NapCombo()
    {
        List<VeDTO> ve = new List<VeDTO>();
        ve.Add(new VeDTO { MaVe = "Chọn vé" });
        ve.AddRange(new CTHDBUS().VeChuaLapHD(maHD));
        cboMaCB.DataSource = ve;
        cboMaCB.DisplayMember = "MaVe";
        cboMaCB.ValueMember = "MaVe";
        cboMaCB.SelectedIndex = 0;
    }

    private void btnDocDSCTHD_Click(object sender, EventArgs e)
    {
        List<CTHDDTO> ds = new CTHDBUS().DocTheoHD(maHD);
        decimal tong = 0;
        lvDSCTHD.Items.Clear();
        foreach (CTHDDTO cthd in ds)
        {
            int n = lvDSCTHD.Items.Count;
            lvDSCTHD.Items.Add(cthd.MaHD);
            lvDSCTHD.Items[n].SubItems.Add(cthd.MaVe);
            lvDSCTHD.Items[n].SubItems.Add(cthd.SL.ToString());
            lvDSCTHD.Items[n].SubItems.Add(cthd.DonGiaVe.ToString("N0"));
            lvDSCTHD.Items[n].SubItems.Add(cthd.ThanhTien.ToString("N0"));
            tong += cthd.ThanhTien;
        }
        lblTong.Text = "Cộng thành tiền: " + tong.ToString("N0");
        NapCombo();
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        VeDTO ve = (VeDTO)cboMaCB.SelectedItem;
        if (ve == null || ve.MaChuyenBay == "")
        {
            MessageBox.Show("Chọn vé.");
            return;
        }
        CTHDBUS bus = new CTHDBUS();
        string loi = bus.KiemTra(maHD, ve.MaVe);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(maHD, ve.MaVe))
            return;
        btnDocDSCTHD_Click(sender, e);
    }

    private void btnThemTatCa_Click(object sender, EventArgs e)
    {
        CTHDBUS bus = new CTHDBUS();
        List<VeDTO> ds = bus.VeChuaLapHD(maHD);
        if (ds.Count == 0)
        {
            MessageBox.Show("Các vé của phiếu đặt đã có trong hóa đơn.");
            return;
        }
        foreach (VeDTO v in ds)
            bus.Them(maHD, v.MaVe);
        btnDocDSCTHD_Click(sender, e);
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (lvDSCTHD.SelectedItems.Count == 0) return;
        string maVe = lvDSCTHD.SelectedItems[0].SubItems[1].Text;
        new CTHDBUS().Xoa(maHD, maVe);
        btnDocDSCTHD_Click(sender, e);
    }
}
