namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmVoucher
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
            lvDSVC = new ListView();
            colMa = new ColumnHeader();
            colCTKM = new ColumnHeader();
            colGiamGia = new ColumnHeader();
            pnl = new Panel();
            lblMaVoucher = new Label();
            txtMaVoucher = new TextBox();
            lblMaCTKM = new Label();
            cboMaCTKM = new ComboBox();
            lblGiamGia = new Label();
            txtGiamGia = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnDocDSVC = new Button();
            pnl.SuspendLayout();
            SuspendLayout();
            lvDSVC.Columns.AddRange(new ColumnHeader[] { colMa, colCTKM, colGiamGia });
            lvDSVC.Dock = DockStyle.Fill;
            lvDSVC.FullRowSelect = true;
            lvDSVC.GridLines = true;
            lvDSVC.HideSelection = false;
            lvDSVC.Location = new Point(0, 142);
            lvDSVC.MultiSelect = false;
            lvDSVC.Name = "lvDSVC";
            lvDSVC.Size = new Size(960, 478);
            lvDSVC.TabIndex = 0;
            lvDSVC.UseCompatibleStateImageBehavior = false;
            lvDSVC.View = View.Details;
            lvDSVC.SelectedIndexChanged += lvDSVC_SelectedIndexChanged;
            colMa.Text = "Mã voucher";
            colMa.Width = 120;
            colCTKM.Text = "Mã CTKM";
            colCTKM.Width = 120;
            colGiamGia.Text = "Giảm giá";
            colGiamGia.Width = 120;
            pnl.Controls.Add(lblMaVoucher);
            pnl.Controls.Add(txtMaVoucher);
            pnl.Controls.Add(lblMaCTKM);
            pnl.Controls.Add(cboMaCTKM);
            pnl.Controls.Add(lblGiamGia);
            pnl.Controls.Add(txtGiamGia);
            pnl.Controls.Add(btnThem);
            pnl.Controls.Add(btnSua);
            pnl.Controls.Add(btnXoa);
            pnl.Controls.Add(btnLamMoi);
            pnl.Controls.Add(btnDocDSVC);
            pnl.Dock = DockStyle.Top;
            pnl.Location = new Point(0, 0);
            pnl.Name = "pnl";
            pnl.Size = new Size(960, 142);
            pnl.TabIndex = 1;
            lblMaVoucher.AutoSize = true;
            lblMaVoucher.Location = new Point(20, 18);
            lblMaVoucher.Name = "lblMaVoucher";
            lblMaVoucher.Text = "Mã voucher:";
            txtMaVoucher.Location = new Point(120, 15);
            txtMaVoucher.Name = "txtMaVoucher";
            txtMaVoucher.Size = new Size(260, 23);
            txtMaVoucher.TabIndex = 0;
            lblMaCTKM.AutoSize = true;
            lblMaCTKM.Location = new Point(460, 18);
            lblMaCTKM.Name = "lblMaCTKM";
            lblMaCTKM.Text = "Mã CTKM:";
            cboMaCTKM.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaCTKM.FormattingEnabled = true;
            cboMaCTKM.Location = new Point(560, 15);
            cboMaCTKM.Name = "cboMaCTKM";
            cboMaCTKM.Size = new Size(260, 23);
            cboMaCTKM.TabIndex = 1;
            lblGiamGia.AutoSize = true;
            lblGiamGia.Location = new Point(20, 54);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Text = "Giảm giá:";
            txtGiamGia.Location = new Point(120, 51);
            txtGiamGia.Name = "txtGiamGia";
            txtGiamGia.Size = new Size(260, 23);
            txtGiamGia.TabIndex = 2;
            btnThem.Location = new Point(20, 92);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 30);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            btnSua.Location = new Point(120, 92);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 30);
            btnSua.TabIndex = 4;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            btnXoa.Location = new Point(220, 92);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 30);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Location = new Point(320, 92);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 30);
            btnLamMoi.TabIndex = 6;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDocDSVC.Location = new Point(420, 92);
            btnDocDSVC.Name = "btnDocDSVC";
            btnDocDSVC.Size = new Size(90, 30);
            btnDocDSVC.TabIndex = 7;
            btnDocDSVC.Text = "Đọc DS";
            btnDocDSVC.UseVisualStyleBackColor = true;
            btnDocDSVC.Click += btnDocDSVC_Click;
            thanhTim.Name = "thanhTim";
            thanhTim.Tim += thanhTim_Tim;
            thanhTim.TimNangCao += thanhTim_TimNangCao;
            thanhTim.TatCa += thanhTim_TatCa;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 620);
            Controls.Add(lvDSVC);
            Controls.Add(thanhTim);
            Controls.Add(pnl);
            Name = "FrmVoucher";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý voucher";
            Load += FrmVoucher_Load;
            pnl.ResumeLayout(false);
            pnl.PerformLayout();
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private ListView lvDSVC;
        private ColumnHeader colMa;
        private ColumnHeader colCTKM;
        private ColumnHeader colGiamGia;
        private Panel pnl;
        private Label lblMaVoucher;
        private TextBox txtMaVoucher;
        private Label lblMaCTKM;
        private ComboBox cboMaCTKM;
        private Label lblGiamGia;
        private TextBox txtGiamGia;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnDocDSVC;
        private ThanhTimKiem thanhTim;
    }
}
