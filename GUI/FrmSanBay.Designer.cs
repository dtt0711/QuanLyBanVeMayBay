namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmSanBay
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
            lvDSSB = new ListView();
            colMa = new ColumnHeader();
            colTen = new ColumnHeader();
            colLoai = new ColumnHeader();
            pnl = new Panel();
            lblMaSanBay = new Label();
            txtMaSanBay = new TextBox();
            lblTen = new Label();
            txtTen = new TextBox();
            lblLoaiSanBay = new Label();
            txtLoaiSanBay = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnDocDSSB = new Button();
            pnl.SuspendLayout();
            SuspendLayout();
            lvDSSB.Columns.AddRange(new ColumnHeader[] { colMa, colTen, colLoai });
            lvDSSB.Dock = DockStyle.Fill;
            lvDSSB.FullRowSelect = true;
            lvDSSB.GridLines = true;
            lvDSSB.HideSelection = false;
            lvDSSB.MultiSelect = false;
            lvDSSB.Name = "lvDSSB";
            lvDSSB.View = View.Details;
            lvDSSB.SelectedIndexChanged += lvDSSB_SelectedIndexChanged;
            colMa.Text = "Mã sân bay";
            colMa.Width = 140;
            colTen.Text = "Tên";
            colTen.Width = 280;
            colLoai.Text = "Loại sân bay";
            colLoai.Width = 180;
            pnl.Controls.Add(lblMaSanBay);
            pnl.Controls.Add(txtMaSanBay);
            pnl.Controls.Add(lblTen);
            pnl.Controls.Add(txtTen);
            pnl.Controls.Add(lblLoaiSanBay);
            pnl.Controls.Add(txtLoaiSanBay);
            pnl.Controls.Add(btnThem);
            pnl.Controls.Add(btnSua);
            pnl.Controls.Add(btnXoa);
            pnl.Controls.Add(btnLamMoi);
            pnl.Controls.Add(btnDocDSSB);
            pnl.Dock = DockStyle.Top;
            pnl.Name = "pnl";
            pnl.Size = new Size(960, 142);
            lblMaSanBay.AutoSize = true;
            lblMaSanBay.Location = new Point(20, 18);
            lblMaSanBay.Name = "lblMaSanBay";
            lblMaSanBay.Text = "Mã sân bay:";
            txtMaSanBay.Location = new Point(130, 15);
            txtMaSanBay.Name = "txtMaSanBay";
            txtMaSanBay.Size = new Size(260, 23);
            lblTen.AutoSize = true;
            lblTen.Location = new Point(460, 18);
            lblTen.Name = "lblTen";
            lblTen.Text = "Tên:";
            txtTen.Location = new Point(560, 15);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(260, 23);
            lblLoaiSanBay.AutoSize = true;
            lblLoaiSanBay.Location = new Point(20, 54);
            lblLoaiSanBay.Name = "lblLoaiSanBay";
            lblLoaiSanBay.Text = "Loại sân bay:";
            txtLoaiSanBay.Location = new Point(130, 51);
            txtLoaiSanBay.Name = "txtLoaiSanBay";
            txtLoaiSanBay.Size = new Size(260, 23);
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
            btnDocDSSB.Location = new Point(420, 92);
            btnDocDSSB.Name = "btnDocDSSB";
            btnDocDSSB.Size = new Size(90, 30);
            btnDocDSSB.Text = "Đọc DS";
            btnDocDSSB.UseVisualStyleBackColor = true;
            btnDocDSSB.Click += btnDocDSSB_Click;
            thanhTim.Name = "thanhTim";
            thanhTim.Tim += thanhTim_Tim;
            thanhTim.TimNangCao += thanhTim_TimNangCao;
            thanhTim.TatCa += thanhTim_TatCa;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(960, 620);
            Controls.Add(lvDSSB);
            Controls.Add(thanhTim);
            Controls.Add(pnl);
            Name = "FrmSanBay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sân bay";
            Load += FrmSanBay_Load;
            pnl.ResumeLayout(false);
            pnl.PerformLayout();
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private ListView lvDSSB;
        private ColumnHeader colMa;
        private ColumnHeader colTen;
        private ColumnHeader colLoai;
        private Panel pnl;
        private Label lblMaSanBay;
        private TextBox txtMaSanBay;
        private Label lblTen;
        private TextBox txtTen;
        private Label lblLoaiSanBay;
        private TextBox txtLoaiSanBay;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnDocDSSB;
        private ThanhTimKiem thanhTim;
    }
}
