namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmMayBay
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
            thanhTim = new ThanhTimKiem();
            lvDSMB = new ListView();
            colMaMayBay = new ColumnHeader();
            colHang = new ColumnHeader();
            pnl = new Panel();
            lblMaMayBay = new Label();
            txtMaMayBay = new TextBox();
            lblHang = new Label();
            cboMaHHK = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnDocDSMB = new Button();
            btnXemCB = new Button();
            pnl.SuspendLayout();
            SuspendLayout();
            lvDSMB.Columns.AddRange(new ColumnHeader[] { colMaMayBay, colHang });
            lvDSMB.Dock = DockStyle.Fill;
            lvDSMB.FullRowSelect = true;
            lvDSMB.GridLines = true;
            lvDSMB.HideSelection = false;
            lvDSMB.Location = new Point(0, 106);
            lvDSMB.MultiSelect = false;
            lvDSMB.Name = "lvDSMB";
            lvDSMB.Size = new Size(960, 514);
            lvDSMB.TabIndex = 0;
            lvDSMB.UseCompatibleStateImageBehavior = false;
            lvDSMB.View = View.Details;
            lvDSMB.SelectedIndexChanged += lvDSMB_SelectedIndexChanged;
            colMaMayBay.Text = "Mã máy bay";
            colMaMayBay.Width = 180;
            colHang.Text = "Hãng hàng không";
            colHang.Width = 220;
            pnl.Controls.Add(lblMaMayBay);
            pnl.Controls.Add(txtMaMayBay);
            pnl.Controls.Add(lblHang);
            pnl.Controls.Add(cboMaHHK);
            pnl.Controls.Add(btnThem);
            pnl.Controls.Add(btnSua);
            pnl.Controls.Add(btnXoa);
            pnl.Controls.Add(btnLamMoi);
            pnl.Controls.Add(btnDocDSMB);
            pnl.Controls.Add(btnXemCB);
            pnl.Dock = DockStyle.Top;
            pnl.Location = new Point(0, 0);
            pnl.Name = "pnl";
            pnl.Size = new Size(960, 106);
            pnl.TabIndex = 1;
            lblMaMayBay.AutoSize = true;
            lblMaMayBay.Location = new Point(20, 18);
            lblMaMayBay.Name = "lblMaMayBay";
            lblMaMayBay.Text = "Mã máy bay:";
            txtMaMayBay.Location = new Point(130, 15);
            txtMaMayBay.Name = "txtMaMayBay";
            txtMaMayBay.Size = new Size(260, 23);
            txtMaMayBay.TabIndex = 0;
            lblHang.AutoSize = true;
            lblHang.Location = new Point(460, 18);
            lblHang.Name = "lblHang";
            lblHang.Text = "Hãng HK:";
            cboMaHHK.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaHHK.FormattingEnabled = true;
            cboMaHHK.Location = new Point(560, 15);
            cboMaHHK.Name = "cboMaHHK";
            cboMaHHK.Size = new Size(260, 23);
            cboMaHHK.TabIndex = 1;
            btnThem.Location = new Point(20, 56);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 30);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            btnSua.Location = new Point(120, 56);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 30);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            btnXoa.Location = new Point(220, 56);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 30);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Location = new Point(320, 56);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 30);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDocDSMB.Location = new Point(420, 56);
            btnDocDSMB.Name = "btnDocDSMB";
            btnDocDSMB.Size = new Size(90, 30);
            btnDocDSMB.TabIndex = 6;
            btnDocDSMB.Text = "Đọc DS";
            btnDocDSMB.UseVisualStyleBackColor = true;
            btnDocDSMB.Click += btnDocDSMB_Click;
            btnXemCB.Location = new Point(520, 56);
            btnXemCB.Name = "btnXemCB";
            btnXemCB.Size = new Size(130, 30);
            btnXemCB.TabIndex = 7;
            btnXemCB.Text = "Xem chuyến bay";
            btnXemCB.UseVisualStyleBackColor = true;
            btnXemCB.Click += btnXemCB_Click;
            thanhTim.Name = "thanhTim";
            thanhTim.Tim += thanhTim_Tim;
            thanhTim.TimNangCao += thanhTim_TimNangCao;
            thanhTim.TatCa += thanhTim_TatCa;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 620);
            Controls.Add(lvDSMB);
            Controls.Add(thanhTim);
            Controls.Add(pnl);
            Name = "FrmMayBay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý máy bay";
            Load += FrmMayBay_Load;
            pnl.ResumeLayout(false);
            pnl.PerformLayout();
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private ListView lvDSMB;
        private ColumnHeader colMaMayBay;
        private ColumnHeader colHang;
        private Panel pnl;
        private Label lblMaMayBay;
        private TextBox txtMaMayBay;
        private Label lblHang;
        private ComboBox cboMaHHK;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnDocDSMB;
        private Button btnXemCB;
        private ThanhTimKiem thanhTim;
    }
}
