using BanVeMayBay.BUS;
using BanVeMayBay.DTO;

namespace BanVeMayBay.GUI;

public partial class FrmCTHD : Form
{
    private string maHD;

    public FrmCTHD()
    {
        InitializeComponent();
        cboMaCB.Format += (s, e) =>
        {
            if (e.ListItem is ChuyenBayDTO p)
                e.Value = p.MaCB + " - " + p.TuyenBay + " (" + p.SoGheTrong + " ghế trống)";
        };
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

    private void NapCombo()
    {
        cboMaCB.DataSource = new ChuyenBayBUS().DocDSCB();
        cboMaCB.DisplayMember = "MaCB";
        cboMaCB.ValueMember = "MaCB";
        cboMaCB.SelectedIndex = -1;
    }

    private void btnDocDSCTHD_Click(object sender, EventArgs e)
    {
        CTHDBUS bus = new CTHDBUS();
        List<CTHDDTO> ds = bus.DocTheoHD(maHD);
        decimal tong = 0;
        lvDSCTHD.Items.Clear();
        foreach (CTHDDTO cthd in ds)
        {
            int n = lvDSCTHD.Items.Count;
            lvDSCTHD.Items.Add(cthd.MaHD);
            lvDSCTHD.Items[n].SubItems.Add(cthd.MaCB);
            lvDSCTHD.Items[n].SubItems.Add(cthd.SoLuong.ToString());
            lvDSCTHD.Items[n].SubItems.Add(cthd.DonGia.ToString("N0"));
            lvDSCTHD.Items[n].SubItems.Add(cthd.ThanhTien.ToString("N0"));
            tong += cthd.ThanhTien;
        }
        lblTong.Text = "Tổng tiền: " + tong.ToString("N0");
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        ChuyenBayDTO cb = (ChuyenBayDTO)cboMaCB.SelectedItem;
        int soLuong = int.Parse(txtSoLuong.Text);
        CTHDDTO cthd = new CTHDDTO(maHD, cb.MaCB, soLuong, cb.GiaVe, soLuong * cb.GiaVe);
        CTHDBUS bus = new CTHDBUS();
        bus.Them(maHD, cb.MaCB, soLuong);
        int n = lvDSCTHD.Items.Count;
        lvDSCTHD.Items.Add(cthd.MaHD);
        lvDSCTHD.Items[n].SubItems.Add(cthd.MaCB);
        lvDSCTHD.Items[n].SubItems.Add(cthd.SoLuong.ToString());
        lvDSCTHD.Items[n].SubItems.Add(cthd.DonGia.ToString("N0"));
        lvDSCTHD.Items[n].SubItems.Add(cthd.ThanhTien.ToString("N0"));
        decimal tong = 0;
        foreach (CTHDDTO c in bus.DocTheoHD(maHD))
            tong += c.ThanhTien;
        lblTong.Text = "Tổng tiền: " + tong.ToString("N0");
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        string maCB = lvDSCTHD.SelectedItems[0].SubItems[1].Text;
        CTHDBUS bus = new CTHDBUS();
        bus.Xoa(maHD, maCB);
        List<CTHDDTO> ds = bus.DocTheoHD(maHD);
        decimal tong = 0;
        lvDSCTHD.Items.Clear();
        foreach (CTHDDTO cthd in ds)
        {
            int n = lvDSCTHD.Items.Count;
            lvDSCTHD.Items.Add(cthd.MaHD);
            lvDSCTHD.Items[n].SubItems.Add(cthd.MaCB);
            lvDSCTHD.Items[n].SubItems.Add(cthd.SoLuong.ToString());
            lvDSCTHD.Items[n].SubItems.Add(cthd.DonGia.ToString("N0"));
            lvDSCTHD.Items[n].SubItems.Add(cthd.ThanhTien.ToString("N0"));
            tong += cthd.ThanhTien;
        }
        lblTong.Text = "Tổng tiền: " + tong.ToString("N0");
    }
}
