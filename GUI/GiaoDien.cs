using System.Collections;

namespace QuanLyBanVeMayBay.GUI;

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
    public static readonly Color ONhoa = Color.FromArgb(241, 245, 249);

    // Khóa một ô do chương trình tự tính (không cho người dùng nhập), tô nền xám cho dễ nhận ra
    public static void KhoaO(TextBox tb)
    {
        tb.ReadOnly = true;
        tb.TabStop = false;
        tb.BackColor = ONhoa;
    }

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
            Label tieuDe = new Label();
            tieuDe.Text = form.Text;
            tieuDe.Dock = DockStyle.Fill;
            tieuDe.TextAlign = ContentAlignment.MiddleLeft;
            tieuDe.Padding = new Padding(20, 0, 12, 0);
            tieuDe.ForeColor = Color.White;
            tieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            dau.Height = TextRenderer.MeasureText("Ág", tieuDe.Font).Height + 28;
            dau.BackColor = Chinh;
            dau.Controls.Add(tieuDe);
            form.Controls.Add(dau);
            dau.SendToBack();
        }

        foreach (Control c in form.Controls)
            Son(c);
        form.Shown += (_, _) =>
        {
            Control dau = form.Controls["pnlTieuDe"];
            if (dau != null && dau.Controls.Count > 0 && dau.Controls[0] is Label lb)
            {
                lb.Text = form.Text;
                dau.Height = TextRenderer.MeasureText("Ág", lb.Font).Height + 36;
            }
            CanCot(form);
        };
    }

    private static void CanCot(Control goc)
    {
        if (goc is ListView lv)
        {
            foreach (ColumnHeader col in lv.Columns)
            {
                int rong = TextRenderer.MeasureText(col.Text, lv.Font).Width + 28;
                if (col.Width < rong)
                    col.Width = rong;
            }
        }
        foreach (Control con in goc.Controls)
            CanCot(con);
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
            tb.BackColor = tb.ReadOnly ? ONhoa : The;
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

    public static void Nut(Button b)
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
        else if (b.Text.StartsWith("Xem") || b.Text.StartsWith("Tìm") || b.Text == "Thống kê" || b.Text == "Chọn" || b.Text == "Chi tiết hóa đơn")
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

    // Thêm nút "…" bên phải một ComboBox: mở FrmChon để tìm và chọn, rồi lấy mã chọn
    // từ biến static FrmChon.MaChon gán lại cho ComboBox (các field liên quan tự điền theo).
    public static void ThemNutChon(ComboBox cbo, string tieuDe)
    {
        Button nut = new Button();
        nut.Text = "…";
        nut.Name = "btnChon" + cbo.Name.Substring(3);
        cbo.Width -= 38;
        nut.Size = new Size(34, cbo.Height + 2);
        nut.Location = new Point(cbo.Right + 4, cbo.Top - 1);
        Nut(nut);
        cbo.Parent.Controls.Add(nut);
        cbo.EnabledChanged += (s, e) => nut.Enabled = cbo.Enabled;
        nut.Click += (s, e) =>
        {
            if (cbo.DataSource is not IList ds)
                return;
            List<object> dsChon = new List<object>();
            foreach (object x in ds)
            {
                object ma = x.GetType().GetProperty(cbo.ValueMember)?.GetValue(x);
                if (ma != null && !ma.ToString().StartsWith("Chọn "))
                    dsChon.Add(x);
            }
            if (dsChon.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để chọn.");
                return;
            }
            FrmChon f = new FrmChon(tieuDe, dsChon, cbo.ValueMember);
            if (f.ShowDialog(cbo.FindForm()) == DialogResult.OK)
                cbo.SelectedValue = FrmChon.MaChon;
        };
    }
}
