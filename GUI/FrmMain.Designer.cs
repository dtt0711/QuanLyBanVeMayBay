namespace BanVeMayBay.GUI
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTren = new Panel();
            lblTenApp = new Label();
            lblPhu = new Label();
            pnlMenu = new Panel();
            flowMenu = new FlowLayoutPanel();
            lblDanhMuc = new Label();
            mnuHangHangKhong = new Button();
            mnuMayBay = new Button();
            mnuChuyenBay = new Button();
            mnuKhachHang = new Button();
            mnuHanhKhach = new Button();
            mnuNhanVien = new Button();
            lblKhuyenMai = new Label();
            mnuCTKM = new Button();
            mnuVoucher = new Button();
            lblGiaoDich = new Label();
            mnuHoaDon = new Button();
            mnuPhieuChi = new Button();
            pnlTren.SuspendLayout();
            pnlMenu.SuspendLayout();
            flowMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTren
            // 
            pnlTren.BackColor = Color.FromArgb(30, 58, 138);
            pnlTren.Controls.Add(lblPhu);
            pnlTren.Controls.Add(lblTenApp);
            pnlTren.Dock = DockStyle.Top;
            pnlTren.Name = "pnlTren";
            pnlTren.Size = new Size(1100, 64);
            // 
            // lblTenApp
            // 
            lblTenApp.AutoSize = true;
            lblTenApp.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTenApp.ForeColor = Color.White;
            lblTenApp.Location = new Point(236, 8);
            lblTenApp.Name = "lblTenApp";
            lblTenApp.Text = "Bán vé máy bay";
            // 
            // lblPhu
            // 
            lblPhu.AutoSize = true;
            lblPhu.Font = new Font("Segoe UI", 9F);
            lblPhu.ForeColor = Color.FromArgb(191, 219, 254);
            lblPhu.Location = new Point(238, 38);
            lblPhu.Name = "lblPhu";
            lblPhu.Text = "Quầy bán vé và danh mục chuyến bay";
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(15, 35, 71);
            pnlMenu.Controls.Add(flowMenu);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(220, 640);
            // 
            // flowMenu
            // 
            flowMenu.AutoScroll = true;
            flowMenu.BackColor = Color.FromArgb(15, 35, 71);
            flowMenu.Controls.Add(lblDanhMuc);
            flowMenu.Controls.Add(mnuHangHangKhong);
            flowMenu.Controls.Add(mnuMayBay);
            flowMenu.Controls.Add(mnuChuyenBay);
            flowMenu.Controls.Add(mnuKhachHang);
            flowMenu.Controls.Add(mnuHanhKhach);
            flowMenu.Controls.Add(mnuNhanVien);
            flowMenu.Controls.Add(lblKhuyenMai);
            flowMenu.Controls.Add(mnuCTKM);
            flowMenu.Controls.Add(mnuVoucher);
            flowMenu.Controls.Add(lblGiaoDich);
            flowMenu.Controls.Add(mnuHoaDon);
            flowMenu.Controls.Add(mnuPhieuChi);
            flowMenu.Dock = DockStyle.Fill;
            flowMenu.FlowDirection = FlowDirection.TopDown;
            flowMenu.Name = "flowMenu";
            flowMenu.Padding = new Padding(10, 12, 10, 12);
            flowMenu.WrapContents = false;
            // 
            // lblDanhMuc
            // 
            lblDanhMuc.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDanhMuc.ForeColor = Color.FromArgb(148, 163, 184);
            lblDanhMuc.Margin = new Padding(8, 4, 3, 6);
            lblDanhMuc.Name = "lblDanhMuc";
            lblDanhMuc.Size = new Size(180, 18);
            lblDanhMuc.Text = "DANH MỤC";
            // 
            // mnuHangHangKhong
            // 
            DatNut(mnuHangHangKhong, "mnuHangHangKhong", "Hãng hàng không");
            mnuHangHangKhong.Click += mnuHangHangKhong_Click;
            // 
            // mnuMayBay
            // 
            DatNut(mnuMayBay, "mnuMayBay", "Máy bay");
            mnuMayBay.Click += mnuMayBay_Click;
            // 
            // mnuChuyenBay
            // 
            DatNut(mnuChuyenBay, "mnuChuyenBay", "Chuyến bay");
            mnuChuyenBay.Click += mnuChuyenBay_Click;
            // 
            // mnuKhachHang
            // 
            DatNut(mnuKhachHang, "mnuKhachHang", "Khách hàng");
            mnuKhachHang.Click += mnuKhachHang_Click;
            // 
            // mnuHanhKhach
            // 
            DatNut(mnuHanhKhach, "mnuHanhKhach", "Hành khách");
            mnuHanhKhach.Click += mnuHanhKhach_Click;
            // 
            // mnuNhanVien
            // 
            DatNut(mnuNhanVien, "mnuNhanVien", "Nhân viên");
            mnuNhanVien.Click += mnuNhanVien_Click;
            // 
            // lblKhuyenMai
            // 
            lblKhuyenMai.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblKhuyenMai.ForeColor = Color.FromArgb(148, 163, 184);
            lblKhuyenMai.Margin = new Padding(8, 16, 3, 6);
            lblKhuyenMai.Name = "lblKhuyenMai";
            lblKhuyenMai.Size = new Size(180, 18);
            lblKhuyenMai.Text = "KHUYẾN MÃI";
            // 
            // mnuCTKM
            // 
            DatNut(mnuCTKM, "mnuCTKM", "Chương trình");
            mnuCTKM.Click += mnuCTKM_Click;
            // 
            // mnuVoucher
            // 
            DatNut(mnuVoucher, "mnuVoucher", "Voucher");
            mnuVoucher.Click += mnuVoucher_Click;
            // 
            // lblGiaoDich
            // 
            lblGiaoDich.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblGiaoDich.ForeColor = Color.FromArgb(148, 163, 184);
            lblGiaoDich.Margin = new Padding(8, 16, 3, 6);
            lblGiaoDich.Name = "lblGiaoDich";
            lblGiaoDich.Size = new Size(180, 18);
            lblGiaoDich.Text = "GIAO DỊCH";
            // 
            // mnuHoaDon
            // 
            DatNut(mnuHoaDon, "mnuHoaDon", "Hóa đơn");
            mnuHoaDon.Click += mnuHoaDon_Click;
            // 
            // mnuPhieuChi
            // 
            DatNut(mnuPhieuChi, "mnuPhieuChi", "Phiếu chi");
            mnuPhieuChi.Click += mnuPhieuChi_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(239, 246, 255);
            ClientSize = new Size(1100, 700);
            Controls.Add(pnlMenu);
            Controls.Add(pnlTren);
            Font = new Font("Segoe UI", 9.75F);
            IsMdiContainer = true;
            Name = "FrmMain";
            Text = "Bán vé máy bay";
            WindowState = FormWindowState.Maximized;
            pnlTren.ResumeLayout(false);
            pnlTren.PerformLayout();
            flowMenu.ResumeLayout(false);
            pnlMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void DatNut(Button nut, string ten, string chu)
        {
            nut.BackColor = Color.FromArgb(15, 35, 71);
            nut.Cursor = Cursors.Hand;
            nut.FlatAppearance.BorderSize = 0;
            nut.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 58, 138);
            nut.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            nut.FlatStyle = FlatStyle.Flat;
            nut.Font = new Font("Segoe UI", 10F);
            nut.ForeColor = Color.White;
            nut.Margin = new Padding(0, 1, 0, 1);
            nut.Name = ten;
            nut.Size = new Size(196, 36);
            nut.Text = chu;
            nut.TextAlign = ContentAlignment.MiddleLeft;
            nut.Padding = new Padding(12, 0, 0, 0);
        }

        private Panel pnlTren;
        private Label lblTenApp;
        private Label lblPhu;
        private Panel pnlMenu;
        private FlowLayoutPanel flowMenu;
        private Label lblDanhMuc;
        private Button mnuHangHangKhong;
        private Button mnuMayBay;
        private Button mnuChuyenBay;
        private Button mnuKhachHang;
        private Button mnuHanhKhach;
        private Button mnuNhanVien;
        private Label lblKhuyenMai;
        private Button mnuCTKM;
        private Button mnuVoucher;
        private Label lblGiaoDich;
        private Button mnuHoaDon;
        private Button mnuPhieuChi;
    }
}
