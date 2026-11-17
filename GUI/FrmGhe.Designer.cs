namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmGhe
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
            lvDSGhe = new ListView();
            colMa = new ColumnHeader();
            colMayBay = new ColumnHeader();
            colHangVe = new ColumnHeader();
            pnl = new Panel();
            lblMaGhe = new Label();
            txtMaGhe = new TextBox();
            lblMayBay = new Label();
            cboMaMayBay = new ComboBox();
            lblHangVe = new Label();
            cboMaHV = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnDocDSGhe = new Button();
            pnl.SuspendLayout();
            SuspendLayout();
            lvDSGhe.Columns.AddRange(new ColumnHeader[] { colMa, colMayBay, colHangVe });
            lvDSGhe.Dock = DockStyle.Fill;
            lvDSGhe.FullRowSelect = true;
            lvDSGhe.GridLines = true;
            lvDSGhe.HideSelection = false;
            lvDSGhe.MultiSelect = false;
            lvDSGhe.Name = "lvDSGhe";
            lvDSGhe.View = View.Details;
            lvDSGhe.SelectedIndexChanged += lvDSGhe_SelectedIndexChanged;
            colMa.Text = "Mã ghế";
            colMa.Width = 140;
            colMayBay.Text = "Máy bay";
            colMayBay.Width = 160;
            colHangVe.Text = "Hạng vé";
            colHangVe.Width = 160;
            pnl.Controls.Add(lblMaGhe);
            pnl.Controls.Add(txtMaGhe);
            pnl.Controls.Add(lblMayBay);
            pnl.Controls.Add(cboMaMayBay);
            pnl.Controls.Add(lblHangVe);
            pnl.Controls.Add(cboMaHV);
            pnl.Controls.Add(btnThem);
            pnl.Controls.Add(btnSua);
            pnl.Controls.Add(btnXoa);
            pnl.Controls.Add(btnLamMoi);
            pnl.Controls.Add(btnDocDSGhe);
            pnl.Dock = DockStyle.Top;
            pnl.Name = "pnl";
            pnl.Size = new Size(960, 142);
            lblMaGhe.AutoSize = true;
            lblMaGhe.Location = new Point(20, 18);
            lblMaGhe.Name = "lblMaGhe";
            lblMaGhe.Text = "Mã ghế:";
            txtMaGhe.Location = new Point(130, 15);
            txtMaGhe.Name = "txtMaGhe";
            txtMaGhe.Size = new Size(260, 23);
            lblMayBay.AutoSize = true;
            lblMayBay.Location = new Point(460, 18);
            lblMayBay.Name = "lblMayBay";
            lblMayBay.Text = "Máy bay:";
            cboMaMayBay.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaMayBay.FormattingEnabled = true;
            cboMaMayBay.Location = new Point(560, 15);
            cboMaMayBay.Name = "cboMaMayBay";
            cboMaMayBay.Size = new Size(260, 23);
            lblHangVe.AutoSize = true;
            lblHangVe.Location = new Point(20, 54);
            lblHangVe.Name = "lblHangVe";
            lblHangVe.Text = "Hạng vé:";
            cboMaHV.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaHV.FormattingEnabled = true;
            cboMaHV.Location = new Point(130, 51);
            cboMaHV.Name = "cboMaHV";
            cboMaHV.Size = new Size(260, 23);
            btnThem.Location = new Point(20, 92);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 30);
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            btnSua.Location = new Point(120, 92);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 30);
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            btnXoa.Location = new Point(220, 92);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 30);
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Location = new Point(320, 92);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 30);
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDocDSGhe.Location = new Point(420, 92);
            btnDocDSGhe.Name = "btnDocDSGhe";
            btnDocDSGhe.Size = new Size(90, 30);
            btnDocDSGhe.Text = "Đọc DS";
            btnDocDSGhe.UseVisualStyleBackColor = true;
            btnDocDSGhe.Click += btnDocDSGhe_Click;
            thanhTim.Name = "thanhTim";
            thanhTim.Tim += thanhTim_Tim;
            thanhTim.TimNangCao += thanhTim_TimNangCao;
            thanhTim.TatCa += thanhTim_TatCa;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 620);
            Controls.Add(lvDSGhe);
            Controls.Add(thanhTim);
            Controls.Add(pnl);
            Name = "FrmGhe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý ghế";
            Load += FrmGhe_Load;
            pnl.ResumeLayout(false);
            pnl.PerformLayout();
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private ListView lvDSGhe;
        private ColumnHeader colMa;
        private ColumnHeader colMayBay;
        private ColumnHeader colHangVe;
        private Panel pnl;
        private Label lblMaGhe;
        private TextBox txtMaGhe;
        private Label lblMayBay;
        private ComboBox cboMaMayBay;
        private Label lblHangVe;
        private ComboBox cboMaHV;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnDocDSGhe;
        private ThanhTimKiem thanhTim;
    }
}
