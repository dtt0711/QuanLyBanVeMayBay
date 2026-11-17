namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmCTHD
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
            lvDSCTHD = new ListView();
            colMaHD = new ColumnHeader();
            colMaCB = new ColumnHeader();
            colSoLuong = new ColumnHeader();
            colDonGia = new ColumnHeader();
            colThanhTien = new ColumnHeader();
            pnl = new Panel();
            lblChuyenBay = new Label();
            cboMaCB = new ComboBox();
            lblSoLuong = new Label();
            txtSoLuong = new TextBox();
            btnThem = new Button();
            btnXoa = new Button();
            btnThemTatCa = new Button();
            lblTong = new Label();
            pnl.SuspendLayout();
            SuspendLayout();
            lvDSCTHD.Columns.AddRange(new ColumnHeader[] { colMaHD, colMaCB, colSoLuong, colDonGia, colThanhTien });
            lvDSCTHD.Dock = DockStyle.Fill;
            lvDSCTHD.FullRowSelect = true;
            lvDSCTHD.GridLines = true;
            lvDSCTHD.HideSelection = false;
            lvDSCTHD.Location = new Point(0, 100);
            lvDSCTHD.MultiSelect = false;
            lvDSCTHD.Name = "lvDSCTHD";
            lvDSCTHD.Size = new Size(760, 380);
            lvDSCTHD.TabIndex = 0;
            lvDSCTHD.UseCompatibleStateImageBehavior = false;
            lvDSCTHD.View = View.Details;
            colMaHD.Text = "Mã HĐ";
            colMaHD.Width = 110;
            colMaCB.Text = "Mã vé";
            colMaCB.Width = 110;
            colSoLuong.Text = "SL";
            colSoLuong.Width = 90;
            colDonGia.Text = "Đơn giá vé";
            colDonGia.Width = 140;
            colThanhTien.Text = "Thành tiền";
            colThanhTien.Width = 160;
            pnl.Controls.Add(lblChuyenBay);
            pnl.Controls.Add(cboMaCB);
            pnl.Controls.Add(lblSoLuong);
            pnl.Controls.Add(txtSoLuong);
            pnl.Controls.Add(btnThem);
            pnl.Controls.Add(btnXoa);
            pnl.Controls.Add(lblTong);
            pnl.Dock = DockStyle.Top;
            pnl.Location = new Point(0, 0);
            pnl.Name = "pnl";
            pnl.Size = new Size(760, 100);
            pnl.TabIndex = 1;
            lblChuyenBay.AutoSize = true;
            lblChuyenBay.Location = new Point(20, 18);
            lblChuyenBay.Name = "lblChuyenBay";
            lblChuyenBay.Text = "Vé:";
            cboMaCB.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaCB.FormattingEnabled = true;
            cboMaCB.Location = new Point(110, 15);
            cboMaCB.Name = "cboMaCB";
            cboMaCB.Size = new Size(380, 23);
            cboMaCB.TabIndex = 0;
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(510, 18);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Text = "SL:";
            txtSoLuong.Location = new Point(580, 15);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new Size(80, 23);
            txtSoLuong.TabIndex = 1;
            btnThem.Location = new Point(20, 55);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 30);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            btnXoa.Location = new Point(120, 55);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 30);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            lblTong.AutoSize = true;
            lblTong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTong.Location = new Point(390, 62);
            lblTong.Name = "lblTong";
            lblTong.Text = "Tổng tiền:";
            btnThemTatCa.Location = new Point(220, 55);
            btnThemTatCa.Name = "btnThemTatCa";
            btnThemTatCa.Size = new Size(150, 30);
            btnThemTatCa.Text = "Thêm tất cả vé";
            btnThemTatCa.Click += btnThemTatCa_Click;
            pnl.Controls.Add(btnThemTatCa);
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 480);
            Controls.Add(lvDSCTHD);
            Controls.Add(pnl);
            Name = "FrmCTHD";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết hóa đơn";
            pnl.ResumeLayout(false);
            pnl.PerformLayout();
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private ListView lvDSCTHD;
        private ColumnHeader colMaHD;
        private ColumnHeader colMaCB;
        private ColumnHeader colSoLuong;
        private ColumnHeader colDonGia;
        private ColumnHeader colThanhTien;
        private Panel pnl;
        private Label lblChuyenBay;
        private ComboBox cboMaCB;
        private Label lblSoLuong;
        private TextBox txtSoLuong;
        private Button btnThem;
        private Button btnXoa;
        private Button btnThemTatCa;
        private Label lblTong;
    }
}
