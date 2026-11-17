namespace QuanLyBanVeMayBay.GUI
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
            mnuSanBay = new Button();
            mnuMayBay = new Button();
            mnuHangVe = new Button();
            mnuGhe = new Button();
            mnuChuyenBay = new Button();
            mnuKhachHang = new Button();
            mnuHanhKhach = new Button();
            mnuNhanVien = new Button();
            lblKhuyenMai = new Label();
            mnuCTKM = new Button();
            mnuVoucher = new Button();
            lblGiaoDich = new Label();
            mnuDatVe = new Button();
            mnuVe = new Button();
            mnuHoaDon = new Button();
            lblHeThong = new Label();
            mnuThamSo = new Button();
            lblBaoCao = new Label();
            mnuThongKe = new Button();
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
            flowMenu.Controls.Add(mnuSanBay);
            flowMenu.Controls.Add(mnuMayBay);
            flowMenu.Controls.Add(mnuHangVe);
            flowMenu.Controls.Add(mnuGhe);
            flowMenu.Controls.Add(mnuChuyenBay);
            flowMenu.Controls.Add(mnuKhachHang);
            flowMenu.Controls.Add(mnuHanhKhach);
            flowMenu.Controls.Add(mnuNhanVien);
            flowMenu.Controls.Add(lblKhuyenMai);
            flowMenu.Controls.Add(mnuCTKM);
            flowMenu.Controls.Add(mnuVoucher);
            flowMenu.Controls.Add(lblGiaoDich);
            flowMenu.Controls.Add(mnuDatVe);
            flowMenu.Controls.Add(mnuVe);
            flowMenu.Controls.Add(mnuHoaDon);
            flowMenu.Controls.Add(lblBaoCao);
            flowMenu.Controls.Add(mnuThongKe);
            flowMenu.Controls.Add(lblHeThong);
            flowMenu.Controls.Add(mnuThamSo);
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
            // mnuSanBay
            // 
            DatNut(mnuSanBay, "mnuSanBay", "Sân bay");
            mnuSanBay.Click += mnuSanBay_Click;
            // 
            // mnuMayBay
            // 
            DatNut(mnuMayBay, "mnuMayBay", "Máy bay");
            mnuMayBay.Click += mnuMayBay_Click;
            // 
            // mnuHangVe
            // 
            DatNut(mnuHangVe, "mnuHangVe", "Hạng vé");
            mnuHangVe.Click += mnuHangVe_Click;
            // 
            // mnuGhe
            // 
            DatNut(mnuGhe, "mnuGhe", "Ghế");
            mnuGhe.Click += mnuGhe_Click;
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
            // mnuDatVe
            // 
            DatNut(mnuDatVe, "mnuDatVe", "Đặt vé");
            mnuDatVe.Click += mnuDatVe_Click;
            // 
            // mnuVe
            // 
            DatNut(mnuVe, "mnuVe", "Vé");
            mnuVe.Click += mnuVe_Click;
            // 
            // mnuHoaDon
            // 
            DatNut(mnuHoaDon, "mnuHoaDon", "Hóa đơn");
            mnuHoaDon.Click += mnuHoaDon_Click;
            // 
            // lblHeThong
            // 
            lblHeThong.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblHeThong.ForeColor = Color.FromArgb(148, 163, 184);
            lblHeThong.Margin = new Padding(8, 16, 3, 6);
            lblHeThong.Name = "lblHeThong";
            lblHeThong.Size = new Size(180, 18);
            lblHeThong.Text = "HỆ THỐNG";
            // 
            // mnuThamSo
            // 
            DatNut(mnuThamSo, "mnuThamSo", "Tham số");
            mnuThamSo.Click += mnuThamSo_Click;
            // 
            // lblBaoCao
            // 
            lblBaoCao.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblBaoCao.ForeColor = Color.FromArgb(148, 163, 184);
            lblBaoCao.Margin = new Padding(8, 16, 3, 6);
            lblBaoCao.Name = "lblBaoCao";
            lblBaoCao.Size = new Size(180, 18);
            lblBaoCao.Text = "BÁO CÁO";
            // 
            // mnuThongKe
            // 
            DatNut(mnuThongKe, "mnuThongKe", "Thống kê doanh thu");
            mnuThongKe.Click += mnuThongKe_Click;
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
            nut.Margin = new Padding(0, 0, 0, 0);
            nut.Name = ten;
            nut.Size = new Size(196, 32);
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
        private Button mnuSanBay;
        private Button mnuMayBay;
        private Button mnuHangVe;
        private Button mnuGhe;
        private Button mnuChuyenBay;
        private Button mnuKhachHang;
        private Button mnuHanhKhach;
        private Button mnuNhanVien;
        private Label lblKhuyenMai;
        private Button mnuCTKM;
        private Button mnuVoucher;
        private Label lblGiaoDich;
        private Button mnuDatVe;
        private Button mnuVe;
        private Button mnuHoaDon;
        private Label lblHeThong;
        private Button mnuThamSo;
        private Label lblBaoCao;
        private Button mnuThongKe;
    }
}
