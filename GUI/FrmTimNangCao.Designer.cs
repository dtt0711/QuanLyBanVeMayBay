namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmTimNangCao
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
            dgvDieuKien = new DataGridView();
            colNoi = new DataGridViewComboBoxColumn();
            colKhong = new DataGridViewCheckBoxColumn();
            colCot = new DataGridViewComboBoxColumn();
            colPhepToan = new DataGridViewComboBoxColumn();
            colGiaTri = new DataGridViewTextBoxColumn();
            pnl = new Panel();
            lblHuongDan = new Label();
            btnTim = new Button();
            btnXoaHet = new Button();
            btnHuy = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDieuKien).BeginInit();
            pnl.SuspendLayout();
            SuspendLayout();
            dgvDieuKien.AllowUserToResizeRows = false;
            dgvDieuKien.BackgroundColor = Color.White;
            dgvDieuKien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDieuKien.Columns.AddRange(new DataGridViewColumn[] { colNoi, colKhong, colCot, colPhepToan, colGiaTri });
            dgvDieuKien.Dock = DockStyle.Fill;
            dgvDieuKien.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvDieuKien.Name = "dgvDieuKien";
            dgvDieuKien.RowHeadersWidth = 30;
            colNoi.HeaderText = "Nối (AND/OR)";
            colNoi.Name = "colNoi";
            colNoi.Width = 110;
            colKhong.HeaderText = "NOT";
            colKhong.Name = "colKhong";
            colKhong.Width = 50;
            colCot.HeaderText = "Field";
            colCot.Name = "colCot";
            colCot.Width = 170;
            colPhepToan.HeaderText = "Phép toán";
            colPhepToan.Name = "colPhepToan";
            colPhepToan.Width = 100;
            colGiaTri.HeaderText = "Giá trị";
            colGiaTri.Name = "colGiaTri";
            colGiaTri.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            pnl.Controls.Add(lblHuongDan);
            pnl.Controls.Add(btnTim);
            pnl.Controls.Add(btnXoaHet);
            pnl.Controls.Add(btnHuy);
            pnl.Dock = DockStyle.Bottom;
            pnl.Name = "pnl";
            pnl.Size = new Size(760, 96);
            lblHuongDan.Location = new Point(20, 8);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(720, 36);
            lblHuongDan.Text = "Mỗi dòng là một điều kiện. AND được tính trước OR. Tích NOT để lấy phần ngược lại. Ngày nhập dạng dd/MM/yyyy, số nhập không có dấu chấm.";
            btnTim.Location = new Point(20, 52);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(90, 30);
            btnTim.Text = "Tìm";
            btnTim.Click += btnTim_Click;
            btnXoaHet.Location = new Point(120, 52);
            btnXoaHet.Name = "btnXoaHet";
            btnXoaHet.Size = new Size(120, 30);
            btnXoaHet.Text = "Xóa điều kiện";
            btnXoaHet.Click += btnXoaHet_Click;
            btnHuy.Location = new Point(250, 52);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 30);
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;
            AcceptButton = btnTim;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnHuy;
            ClientSize = new Size(760, 380);
            Controls.Add(dgvDieuKien);
            Controls.Add(pnl);
            Name = "FrmTimNangCao";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tìm nâng cao";
            ((System.ComponentModel.ISupportInitialize)dgvDieuKien).EndInit();
            pnl.ResumeLayout(false);
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private DataGridView dgvDieuKien;
        private DataGridViewComboBoxColumn colNoi;
        private DataGridViewCheckBoxColumn colKhong;
        private DataGridViewComboBoxColumn colCot;
        private DataGridViewComboBoxColumn colPhepToan;
        private DataGridViewTextBoxColumn colGiaTri;
        private Panel pnl;
        private Label lblHuongDan;
        private Button btnTim;
        private Button btnXoaHet;
        private Button btnHuy;
    }
}
