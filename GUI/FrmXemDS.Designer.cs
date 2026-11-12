namespace BanVeMayBay.GUI
{
    partial class FrmXemDS
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
            lvDS = new ListView();
            SuspendLayout();
            // 
            // lvDS
            // 
            lvDS.Dock = DockStyle.Fill;
            lvDS.FullRowSelect = true;
            lvDS.GridLines = true;
            lvDS.Location = new Point(0, 0);
            lvDS.Name = "lvDS";
            lvDS.Size = new Size(760, 400);
            lvDS.TabIndex = 0;
            lvDS.UseCompatibleStateImageBehavior = false;
            lvDS.View = View.Details;
            // 
            // FrmXemDS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 400);
            Controls.Add(lvDS);
            Name = "FrmXemDS";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Danh sách";
            ResumeLayout(false);
            GiaoDien.ApDung(this);
        }

        private ListView lvDS;
    }
}
