namespace BanVeMayBay.GUI
{
    partial class FrmHangHangKhong
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
            lvDSHHK = new ListView();
            colMaHang = new ColumnHeader();
            colTenHang = new ColumnHeader();
            pnl = new Panel();
            lblMaHang = new Label();
            txtMaHHK = new TextBox();
            lblTenHang = new Label();
            txtTenHang = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnDocDSHHK = new Button();
            btnXemMB = new Button();
            pnl.SuspendLayout();
            SuspendLayout();
            // 
            // lvDSHHK
            // 
            lvDSHHK.Columns.AddRange(new ColumnHeader[] { colMaHang, colTenHang });
            lvDSHHK.Dock = DockStyle.Fill;
            lvDSHHK.FullRowSelect = true;
            lvDSHHK.GridLines = true;
            lvDSHHK.HideSelection = false;
            lvDSHHK.Location = new Point(0, 106);
            lvDSHHK.MultiSelect = false;
            lvDSHHK.Name = "lvDSHHK";
            lvDSHHK.Size = new Size(960, 514);
            lvDSHHK.TabIndex = 0;
            lvDSHHK.UseCompatibleStateImageBehavior = false;
            lvDSHHK.View = View.Details;
            lvDSHHK.SelectedIndexChanged += lvDSHHK_SelectedIndexChanged;
            // 
            // colMaHang
            // 
            colMaHang.Text = "Mã hãng";
            colMaHang.Width = 120;
            // 
            // colTenHang
            // 
            colTenHang.Text = "Tên hãng";
            colTenHang.Width = 120;
            // 
            // pnl
            // 
            pnl.Controls.Add(lblMaHang);
            pnl.Controls.Add(txtMaHHK);
            pnl.Controls.Add(lblTenHang);
            pnl.Controls.Add(txtTenHang);
            pnl.Controls.Add(btnThem);
            pnl.Controls.Add(btnSua);
            pnl.Controls.Add(btnXoa);
            pnl.Controls.Add(btnLamMoi);
            pnl.Controls.Add(btnDocDSHHK);
            pnl.Controls.Add(btnXemMB);
            pnl.Dock = DockStyle.Top;
            pnl.Location = new Point(0, 0);
            pnl.Name = "pnl";
            pnl.Size = new Size(960, 106);
            pnl.TabIndex = 1;
            // 
            // lblMaHang
            // 
            lblMaHang.AutoSize = true;
            lblMaHang.Location = new Point(20, 18);
            lblMaHang.Name = "lblMaHang";
            lblMaHang.Text = "Mã hãng:";
            // 
            // txtMaHHK
            // 
            txtMaHHK.Location = new Point(120, 15);
            txtMaHHK.Name = "txtMaHHK";
            txtMaHHK.Size = new Size(260, 23);
            txtMaHHK.TabIndex = 0;
            // 
            // lblTenHang
            // 
            lblTenHang.AutoSize = true;
            lblTenHang.Location = new Point(460, 18);
            lblTenHang.Name = "lblTenHang";
            lblTenHang.Text = "Tên hãng:";
            // 
            // txtTenHang
            // 
            txtTenHang.Location = new Point(560, 15);
            txtTenHang.Name = "txtTenHang";
            txtTenHang.Size = new Size(260, 23);
            txtTenHang.TabIndex = 1;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(20, 56);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 30);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(120, 56);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 30);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(220, 56);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 30);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(320, 56);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(90, 30);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnDocDSHHK
            // 
            btnDocDSHHK.Location = new Point(420, 56);
            btnDocDSHHK.Name = "btnDocDSHHK";
            btnDocDSHHK.Size = new Size(90, 30);
            btnDocDSHHK.TabIndex = 6;
            btnDocDSHHK.Text = "Đọc DS";
            btnDocDSHHK.UseVisualStyleBackColor = true;
            btnDocDSHHK.Click += btnDocDSHHK_Click;
            // 
            // btnXemMB
            // 
            btnXemMB.Location = new Point(520, 56);
            btnXemMB.Name = "btnXemMB";
            btnXemMB.Size = new Size(130, 30);
            btnXemMB.TabIndex = 7;
            btnXemMB.Text = "Xem máy bay";
            btnXemMB.UseVisualStyleBackColor = true;
            btnXemMB.Click += btnXemMB_Click;
            // 
            // FrmHangHangKhong
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 620);
            Controls.Add(lvDSHHK);
            Controls.Add(pnl);
            Name = "FrmHangHangKhong";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý hãng hàng không";
            Load += FrmHangHangKhong_Load;
            pnl.ResumeLayout(false);
            pnl.PerformLayout();
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private ListView lvDSHHK;
        private ColumnHeader colMaHang;
        private ColumnHeader colTenHang;
        private Panel pnl;
        private Label lblMaHang;
        private TextBox txtMaHHK;
        private Label lblTenHang;
        private TextBox txtTenHang;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnDocDSHHK;
        private Button btnXemMB;
    }
}
