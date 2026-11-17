namespace QuanLyBanVeMayBay.GUI
{
    partial class FrmChon
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
            pnl = new Panel();
            lblTim = new Label();
            txtTuKhoa = new TextBox();
            lblSoDong = new Label();
            lvDS = new ListView();
            pnlNut = new Panel();
            btnChon = new Button();
            btnHuy = new Button();
            pnl.SuspendLayout();
            pnlNut.SuspendLayout();
            SuspendLayout();
            pnl.Controls.Add(lblTim);
            pnl.Controls.Add(txtTuKhoa);
            pnl.Controls.Add(lblSoDong);
            pnl.Dock = DockStyle.Top;
            pnl.Name = "pnl";
            pnl.Size = new Size(760, 50);
            lblTim.AutoSize = true;
            lblTim.Location = new Point(20, 16);
            lblTim.Name = "lblTim";
            lblTim.Text = "Tìm kiếm:";
            txtTuKhoa.Location = new Point(110, 13);
            txtTuKhoa.Name = "txtTuKhoa";
            txtTuKhoa.PlaceholderText = "Gõ để lọc danh sách";
            txtTuKhoa.Size = new Size(320, 23);
            txtTuKhoa.TabIndex = 0;
            txtTuKhoa.TextChanged += txtTuKhoa_TextChanged;
            lblSoDong.AutoSize = true;
            lblSoDong.Location = new Point(445, 16);
            lblSoDong.Name = "lblSoDong";
            lvDS.Dock = DockStyle.Fill;
            lvDS.FullRowSelect = true;
            lvDS.GridLines = true;
            lvDS.HideSelection = false;
            lvDS.MultiSelect = false;
            lvDS.Name = "lvDS";
            lvDS.View = View.Details;
            lvDS.DoubleClick += lvDS_DoubleClick;
            pnlNut.Controls.Add(btnChon);
            pnlNut.Controls.Add(btnHuy);
            pnlNut.Dock = DockStyle.Bottom;
            pnlNut.Name = "pnlNut";
            pnlNut.Size = new Size(760, 52);
            btnChon.Location = new Point(20, 11);
            btnChon.Name = "btnChon";
            btnChon.Size = new Size(90, 30);
            btnChon.Text = "Chọn";
            btnChon.Click += btnChon_Click;
            btnHuy.Location = new Point(120, 11);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(90, 30);
            btnHuy.Text = "Hủy";
            btnHuy.Click += btnHuy_Click;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnHuy;
            ClientSize = new Size(760, 440);
            Controls.Add(lvDS);
            Controls.Add(pnl);
            Controls.Add(pnlNut);
            Name = "FrmChon";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chọn";
            pnl.ResumeLayout(false);
            pnl.PerformLayout();
            pnlNut.ResumeLayout(false);
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private Panel pnl;
        private Label lblTim;
        private TextBox txtTuKhoa;
        private Label lblSoDong;
        private ListView lvDS;
        private Panel pnlNut;
        private Button btnChon;
        private Button btnHuy;
    }
}
