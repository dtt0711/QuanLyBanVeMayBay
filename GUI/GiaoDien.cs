namespace BanVeMayBay.GUI;

public static class GiaoDien
{
    public static readonly Color Nen = Color.FromArgb(239, 246, 255);
    public static readonly Color The = Color.White;
    public static readonly Color Chu = Color.FromArgb(15, 23, 42);
    public static readonly Color ChuPhu = Color.FromArgb(71, 85, 105);
    public static readonly Color Chinh = Color.FromArgb(30, 58, 138);
    public static readonly Color XanhDam = Color.FromArgb(15, 35, 71);
    public static readonly Color Nhan = Color.FromArgb(234, 88, 12);
    public static readonly Color XoaMau = Color.FromArgb(185, 28, 28);

    public static void ApDung(Form form)
    {
        form.Font = new Font("Segoe UI", 9.75F);
        form.BackColor = Nen;
        form.ForeColor = Chu;

        if (form.Name != "FrmMain" && form.Controls["pnlTieuDe"] == null)
        {
            Panel dau = new Panel();
            dau.Name = "pnlTieuDe";
            dau.Dock = DockStyle.Top;
            dau.Height = 56;
            dau.BackColor = Chinh;
            Label tieuDe = new Label();
            tieuDe.Text = form.Text;
            tieuDe.AutoSize = true;
            tieuDe.ForeColor = Color.White;
            tieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            tieuDe.Location = new Point(20, 14);
            dau.Controls.Add(tieuDe);
            form.Controls.Add(dau);
        }

        foreach (Control c in form.Controls)
            Son(c);
    }

    private static void Son(Control c)
    {
        if (c is Panel p && p.Name != "pnlTieuDe")
            p.BackColor = The;
        if (c is ListView lv)
        {
            lv.BackColor = Color.FromArgb(248, 250, 252);
            lv.ForeColor = Chu;
            lv.Font = new Font("Segoe UI", 9.75F);
            lv.BorderStyle = BorderStyle.None;
        }
        if (c is Button b)
            Nut(b);
        if (c is TextBox tb)
        {
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.BackColor = The;
            tb.Font = new Font("Segoe UI", 9.75F);
        }
        if (c is ComboBox cb)
        {
            cb.FlatStyle = FlatStyle.Flat;
            cb.BackColor = The;
            cb.Font = new Font("Segoe UI", 9.75F);
        }
        if (c is DateTimePicker dtp)
            dtp.Font = new Font("Segoe UI", 9.75F);
        if (c is Label lb && (lb.Parent == null || lb.Parent.Name != "pnlTieuDe"))
        {
            lb.ForeColor = ChuPhu;
            lb.Font = new Font("Segoe UI", 9F);
        }
        foreach (Control con in c.Controls)
            Son(con);
    }

    private static void Nut(Button b)
    {
        b.FlatStyle = FlatStyle.Flat;
        b.FlatAppearance.BorderSize = 0;
        b.Cursor = Cursors.Hand;
        b.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        if (b.Text == "Thêm")
        {
            b.BackColor = Nhan;
            b.ForeColor = Color.FromArgb(28, 25, 23);
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(249, 115, 22);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(194, 65, 12);
        }
        else if (b.Text == "Xóa")
        {
            b.BackColor = XoaMau;
            b.ForeColor = Color.White;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 27, 27);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(127, 29, 29);
        }
        else if (b.Text == "Sửa")
        {
            b.BackColor = Color.FromArgb(29, 78, 216);
            b.ForeColor = Color.White;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            b.FlatAppearance.MouseDownBackColor = Chinh;
        }
        else if (b.Text.StartsWith("Xem") || b.Text == "Chi tiết hóa đơn")
        {
            b.BackColor = Chinh;
            b.ForeColor = Color.White;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            b.FlatAppearance.MouseDownBackColor = XanhDam;
        }
        else
        {
            b.BackColor = Color.FromArgb(226, 232, 240);
            b.ForeColor = Chu;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(203, 213, 225);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(148, 163, 184);
        }
    }
}
