namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmThongKe
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
            tab = new TabControl();
            tabTongThu = new TabPage();
            tabTheoQuy = new TabPage();
            tabDoiTuong = new TabPage();
            tabNhieuKhoa = new TabPage();
            // tab 1: tong thu
            pnlTongThu = new Panel();
            lblTu1 = new Label();
            dtpTu1 = new DateTimePicker();
            lblDen1 = new Label();
            dtpDen1 = new DateTimePicker();
            lblKy1 = new Label();
            cboKy1 = new ComboBox();
            btnTongThu = new Button();
            lblTongThu = new Label();
            dgvTongThu = new DataGridView();
            // tab 2: theo quy
            pnlTheoQuy = new Panel();
            lblNam = new Label();
            cboNam = new ComboBox();
            btnTheoQuy = new Button();
            tlpQuy = new TableLayoutPanel();
            lblQuyNV = new Label();
            dgvQuyNV = new DataGridView();
            lblQuyKH = new Label();
            dgvQuyKH = new DataGridView();
            lblQuyCB = new Label();
            dgvQuyCB = new DataGridView();
            // tab 3: theo doi tuong
            pnlDoiTuong = new Panel();
            lblDoiTuong = new Label();
            cboDoiTuong = new ComboBox();
            lblKy3 = new Label();
            cboKy3 = new ComboBox();
            lblTu3 = new Label();
            dtpTu3 = new DateTimePicker();
            lblDen3 = new Label();
            dtpDen3 = new DateTimePicker();
            btnDoiTuong = new Button();
            dgvDoiTuong = new DataGridView();
            // tab 4: nhieu khoa
            pnlNhieuKhoa = new Panel();
            lblLoai = new Label();
            cboLoai = new ComboBox();
            lblTu4 = new Label();
            dtpTu4 = new DateTimePicker();
            lblDen4 = new Label();
            dtpDen4 = new DateTimePicker();
            btnNhieuKhoa = new Button();
            dgvNhieuKhoa = new DataGridView();
            SuspendLayout();

            tab.Controls.Add(tabTongThu);
            tab.Controls.Add(tabTheoQuy);
            tab.Controls.Add(tabDoiTuong);
            tab.Controls.Add(tabNhieuKhoa);
            tab.Dock = DockStyle.Fill;
            tab.Name = "tab";
            tab.Padding = new Point(14, 6);

            // ===== Tab 1 =====
            tabTongThu.Text = "Tổng thu";
            tabTongThu.Controls.Add(dgvTongThu);
            tabTongThu.Controls.Add(lblTongThu);
            tabTongThu.Controls.Add(pnlTongThu);
            pnlTongThu.Dock = DockStyle.Top;
            pnlTongThu.Size = new Size(1100, 60);
            pnlTongThu.Controls.AddRange(new Control[] { lblTu1, dtpTu1, lblDen1, dtpDen1, lblKy1, cboKy1, btnTongThu });
            lblTu1.AutoSize = true; lblTu1.Location = new Point(20, 21); lblTu1.Text = "Từ ngày:";
            dtpTu1.Format = DateTimePickerFormat.Custom; dtpTu1.CustomFormat = "dd/MM/yyyy";
            dtpTu1.Location = new Point(90, 17); dtpTu1.Size = new Size(130, 23); dtpTu1.Name = "dtpTu1";
            lblDen1.AutoSize = true; lblDen1.Location = new Point(240, 21); lblDen1.Text = "Đến ngày:";
            dtpDen1.Format = DateTimePickerFormat.Custom; dtpDen1.CustomFormat = "dd/MM/yyyy";
            dtpDen1.Location = new Point(320, 17); dtpDen1.Size = new Size(130, 23); dtpDen1.Name = "dtpDen1";
            lblKy1.AutoSize = true; lblKy1.Location = new Point(470, 21); lblKy1.Text = "Theo:";
            cboKy1.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKy1.Location = new Point(520, 17); cboKy1.Size = new Size(110, 23); cboKy1.Name = "cboKy1";
            btnTongThu.Location = new Point(650, 14); btnTongThu.Size = new Size(110, 30); btnTongThu.Text = "Thống kê";
            btnTongThu.Name = "btnTongThu";
            btnTongThu.Click += btnTongThu_Click;
            lblTongThu.Dock = DockStyle.Top;
            lblTongThu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongThu.Name = "lblTongThu";
            lblTongThu.Padding = new Padding(20, 0, 0, 0);
            lblTongThu.Size = new Size(1100, 40);
            lblTongThu.TextAlign = ContentAlignment.MiddleLeft;
            dgvTongThu.Dock = DockStyle.Fill; dgvTongThu.Name = "dgvTongThu";

            // ===== Tab 2 =====
            tabTheoQuy.Text = "Theo quý";
            tabTheoQuy.Controls.Add(tlpQuy);
            tabTheoQuy.Controls.Add(pnlTheoQuy);
            pnlTheoQuy.Dock = DockStyle.Top;
            pnlTheoQuy.Size = new Size(1100, 60);
            pnlTheoQuy.Controls.AddRange(new Control[] { lblNam, cboNam, btnTheoQuy });
            lblNam.AutoSize = true; lblNam.Location = new Point(20, 21); lblNam.Text = "Năm:";
            cboNam.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNam.Location = new Point(70, 17); cboNam.Size = new Size(100, 23); cboNam.Name = "cboNam";
            btnTheoQuy.Location = new Point(190, 14); btnTheoQuy.Size = new Size(110, 30); btnTheoQuy.Text = "Thống kê";
            btnTheoQuy.Name = "btnTheoQuy";
            btnTheoQuy.Click += btnTheoQuy_Click;
            tlpQuy.Dock = DockStyle.Fill;
            tlpQuy.ColumnCount = 1;
            tlpQuy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpQuy.RowCount = 6;
            for (int i = 0; i < 3; i++)
            {
                tlpQuy.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                tlpQuy.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3F));
            }
            tlpQuy.Controls.Add(lblQuyNV, 0, 0);
            tlpQuy.Controls.Add(dgvQuyNV, 0, 1);
            tlpQuy.Controls.Add(lblQuyKH, 0, 2);
            tlpQuy.Controls.Add(dgvQuyKH, 0, 3);
            tlpQuy.Controls.Add(lblQuyCB, 0, 4);
            tlpQuy.Controls.Add(dgvQuyCB, 0, 5);
            lblQuyNV.Dock = DockStyle.Fill; lblQuyNV.TextAlign = ContentAlignment.BottomLeft;
            lblQuyNV.Text = "Tổng tiền hóa đơn theo nhân viên và quý";
            lblQuyKH.Dock = DockStyle.Fill; lblQuyKH.TextAlign = ContentAlignment.BottomLeft;
            lblQuyKH.Text = "Tổng tiền hóa đơn theo khách hàng và quý";
            lblQuyCB.Dock = DockStyle.Fill; lblQuyCB.TextAlign = ContentAlignment.BottomLeft;
            lblQuyCB.Text = "Tổng tiền hóa đơn theo chuyến bay (sản phẩm) và quý - từ hóa đơn và chi tiết hóa đơn";
            dgvQuyNV.Dock = DockStyle.Fill; dgvQuyNV.Name = "dgvQuyNV";
            dgvQuyKH.Dock = DockStyle.Fill; dgvQuyKH.Name = "dgvQuyKH";
            dgvQuyCB.Dock = DockStyle.Fill; dgvQuyCB.Name = "dgvQuyCB";

            // ===== Tab 3 =====
            tabDoiTuong.Text = "Theo đối tượng";
            tabDoiTuong.Controls.Add(dgvDoiTuong);
            tabDoiTuong.Controls.Add(pnlDoiTuong);
            pnlDoiTuong.Dock = DockStyle.Top;
            pnlDoiTuong.Size = new Size(1100, 60);
            pnlDoiTuong.Controls.AddRange(new Control[] { lblDoiTuong, cboDoiTuong, lblKy3, cboKy3, lblTu3, dtpTu3, lblDen3, dtpDen3, btnDoiTuong });
            lblDoiTuong.AutoSize = true; lblDoiTuong.Location = new Point(20, 21); lblDoiTuong.Text = "Theo:";
            cboDoiTuong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoiTuong.Location = new Point(70, 17); cboDoiTuong.Size = new Size(120, 23); cboDoiTuong.Name = "cboDoiTuong";
            lblKy3.AutoSize = true; lblKy3.Location = new Point(205, 21); lblKy3.Text = "Kỳ:";
            cboKy3.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKy3.Location = new Point(240, 17); cboKy3.Size = new Size(90, 23); cboKy3.Name = "cboKy3";
            lblTu3.AutoSize = true; lblTu3.Location = new Point(345, 21); lblTu3.Text = "Từ ngày:";
            dtpTu3.Format = DateTimePickerFormat.Custom; dtpTu3.CustomFormat = "dd/MM/yyyy";
            dtpTu3.Location = new Point(410, 17); dtpTu3.Size = new Size(120, 23); dtpTu3.Name = "dtpTu3";
            lblDen3.AutoSize = true; lblDen3.Location = new Point(545, 21); lblDen3.Text = "Đến ngày:";
            dtpDen3.Format = DateTimePickerFormat.Custom; dtpDen3.CustomFormat = "dd/MM/yyyy";
            dtpDen3.Location = new Point(620, 17); dtpDen3.Size = new Size(120, 23); dtpDen3.Name = "dtpDen3";
            btnDoiTuong.Location = new Point(760, 14); btnDoiTuong.Size = new Size(110, 30); btnDoiTuong.Text = "Thống kê";
            btnDoiTuong.Name = "btnDoiTuong";
            btnDoiTuong.Click += btnDoiTuong_Click;
            dgvDoiTuong.Dock = DockStyle.Fill; dgvDoiTuong.Name = "dgvDoiTuong";

            // ===== Tab 4 =====
            tabNhieuKhoa.Text = "Nhiều khóa";
            tabNhieuKhoa.Controls.Add(dgvNhieuKhoa);
            tabNhieuKhoa.Controls.Add(pnlNhieuKhoa);
            pnlNhieuKhoa.Dock = DockStyle.Top;
            pnlNhieuKhoa.Size = new Size(1100, 60);
            pnlNhieuKhoa.Controls.AddRange(new Control[] { lblLoai, cboLoai, lblTu4, dtpTu4, lblDen4, dtpDen4, btnNhieuKhoa });
            lblLoai.AutoSize = true; lblLoai.Location = new Point(20, 21); lblLoai.Text = "Theo:";
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Location = new Point(70, 17); cboLoai.Size = new Size(260, 23); cboLoai.Name = "cboLoai";
            lblTu4.AutoSize = true; lblTu4.Location = new Point(345, 21); lblTu4.Text = "Từ ngày:";
            dtpTu4.Format = DateTimePickerFormat.Custom; dtpTu4.CustomFormat = "dd/MM/yyyy";
            dtpTu4.Location = new Point(410, 17); dtpTu4.Size = new Size(120, 23); dtpTu4.Name = "dtpTu4";
            lblDen4.AutoSize = true; lblDen4.Location = new Point(545, 21); lblDen4.Text = "Đến ngày:";
            dtpDen4.Format = DateTimePickerFormat.Custom; dtpDen4.CustomFormat = "dd/MM/yyyy";
            dtpDen4.Location = new Point(620, 17); dtpDen4.Size = new Size(120, 23); dtpDen4.Name = "dtpDen4";
            btnNhieuKhoa.Location = new Point(760, 14); btnNhieuKhoa.Size = new Size(110, 30); btnNhieuKhoa.Text = "Thống kê";
            btnNhieuKhoa.Name = "btnNhieuKhoa";
            btnNhieuKhoa.Click += btnNhieuKhoa_Click;
            dgvNhieuKhoa.Dock = DockStyle.Fill; dgvNhieuKhoa.Name = "dgvNhieuKhoa";

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 700);
            Controls.Add(tab);
            Name = "FrmThongKe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thống kê doanh thu";
            Load += FrmThongKe_Load;
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private TabControl tab;
        private TabPage tabTongThu;
        private TabPage tabTheoQuy;
        private TabPage tabDoiTuong;
        private TabPage tabNhieuKhoa;
        private Panel pnlTongThu;
        private Label lblTu1;
        private DateTimePicker dtpTu1;
        private Label lblDen1;
        private DateTimePicker dtpDen1;
        private Label lblKy1;
        private ComboBox cboKy1;
        private Button btnTongThu;
        private Label lblTongThu;
        private DataGridView dgvTongThu;
        private Panel pnlTheoQuy;
        private Label lblNam;
        private ComboBox cboNam;
        private Button btnTheoQuy;
        private TableLayoutPanel tlpQuy;
        private Label lblQuyNV;
        private DataGridView dgvQuyNV;
        private Label lblQuyKH;
        private DataGridView dgvQuyKH;
        private Label lblQuyCB;
        private DataGridView dgvQuyCB;
        private Panel pnlDoiTuong;
        private Label lblDoiTuong;
        private ComboBox cboDoiTuong;
        private Label lblKy3;
        private ComboBox cboKy3;
        private Label lblTu3;
        private DateTimePicker dtpTu3;
        private Label lblDen3;
        private DateTimePicker dtpDen3;
        private Button btnDoiTuong;
        private DataGridView dgvDoiTuong;
        private Panel pnlNhieuKhoa;
        private Label lblLoai;
        private ComboBox cboLoai;
        private Label lblTu4;
        private DateTimePicker dtpTu4;
        private Label lblDen4;
        private DateTimePicker dtpDen4;
        private Button btnNhieuKhoa;
        private DataGridView dgvNhieuKhoa;
    }
}
