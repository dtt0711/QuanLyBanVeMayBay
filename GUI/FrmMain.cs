namespace BanVeMayBay.GUI;

public partial class FrmMain : Form
{
    public FrmMain()
    {
        InitializeComponent();
        pnlTren.BringToFront();
        pnlMenu.BringToFront();
        foreach (Control c in Controls)
        {
            if (c is MdiClient nen)
                nen.BackColor = GiaoDien.Nen;
        }
    }

    private void Mo(Form f)
    {
        f.MdiParent = this;
        f.WindowState = FormWindowState.Maximized;
        f.Show();
        foreach (Control c in Controls)
        {
            if (c is MdiClient nen)
                nen.BackColor = GiaoDien.Nen;
        }
    }

    private void mnuHangHangKhong_Click(object sender, EventArgs e) => Mo(new FrmHangHangKhong());
    private void mnuMayBay_Click(object sender, EventArgs e) => Mo(new FrmMayBay());
    private void mnuChuyenBay_Click(object sender, EventArgs e) => Mo(new FrmChuyenBay());
    private void mnuKhachHang_Click(object sender, EventArgs e) => Mo(new FrmKhachHang());
    private void mnuHanhKhach_Click(object sender, EventArgs e) => Mo(new FrmHanhKhach());
    private void mnuNhanVien_Click(object sender, EventArgs e) => Mo(new FrmNhanVien());
    private void mnuCTKM_Click(object sender, EventArgs e) => Mo(new FrmCTKM());
    private void mnuVoucher_Click(object sender, EventArgs e) => Mo(new FrmVoucher());
    private void mnuHoaDon_Click(object sender, EventArgs e) => Mo(new FrmHoaDon());
    private void mnuPhieuChi_Click(object sender, EventArgs e) => Mo(new FrmPhieuChi());
}
