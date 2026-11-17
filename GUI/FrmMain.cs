namespace QuanLyBanVeMayBay.GUI;

public partial class FrmMain : Form
{
    public FrmMain()
    {
        InitializeComponent();
        Resize += (_, _) => SapXepVung();
        Shown += (_, _) => SapXepVung();
        SapXepVung();
    }

    private void SapXepVung()
    {
        int trai = pnlMenu.Width;
        int tren = pnlTren.Height;
        foreach (Control c in Controls)
        {
            if (c is MdiClient nen)
            {
                nen.Dock = DockStyle.None;
                nen.BackColor = GiaoDien.Nen;
                nen.SetBounds(trai, tren, Math.Max(0, ClientSize.Width - trai), Math.Max(0, ClientSize.Height - tren));
                nen.BringToFront();
                if (ActiveMdiChild != null && ActiveMdiChild.WindowState != FormWindowState.Minimized)
                {
                    ActiveMdiChild.WindowState = FormWindowState.Normal;
                    ActiveMdiChild.Bounds = nen.ClientRectangle;
                }
            }
        }
    }

    private void Mo(Form f)
    {
        f.FormBorderStyle = FormBorderStyle.None;
        f.MdiParent = this;
        f.WindowState = FormWindowState.Normal;
        f.Show();
        BeginInvoke(SapXepVung);
    }

    private void mnuHangHangKhong_Click(object sender, EventArgs e) => Mo(new FrmHangHangKhong());
    private void mnuSanBay_Click(object sender, EventArgs e) => Mo(new FrmSanBay());
    private void mnuMayBay_Click(object sender, EventArgs e) => Mo(new FrmMayBay());
    private void mnuHangVe_Click(object sender, EventArgs e) => Mo(new FrmHangVe());
    private void mnuGhe_Click(object sender, EventArgs e) => Mo(new FrmGhe());
    private void mnuChuyenBay_Click(object sender, EventArgs e) => Mo(new FrmChuyenBay());
    private void mnuKhachHang_Click(object sender, EventArgs e) => Mo(new FrmKhachHang());
    private void mnuHanhKhach_Click(object sender, EventArgs e) => Mo(new FrmHanhKhach());
    private void mnuNhanVien_Click(object sender, EventArgs e) => Mo(new FrmNhanVien());
    private void mnuCTKM_Click(object sender, EventArgs e) => Mo(new FrmCTKM());
    private void mnuVoucher_Click(object sender, EventArgs e) => Mo(new FrmVoucher());
    private void mnuDatVe_Click(object sender, EventArgs e) => Mo(new FrmDatVe());
    private void mnuVe_Click(object sender, EventArgs e) => Mo(new FrmVe());
    private void mnuHoaDon_Click(object sender, EventArgs e) => Mo(new FrmHoaDon());
    private void mnuThamSo_Click(object sender, EventArgs e) => Mo(new FrmThamSo());
    private void mnuThongKe_Click(object sender, EventArgs e) => Mo(new FrmThongKe());
}
