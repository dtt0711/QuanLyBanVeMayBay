using System.Collections;
using System.Reflection;
using QuanLyBanVeMayBay.BUS;

namespace QuanLyBanVeMayBay.GUI;

// Form mở từ nút "…": tìm kiếm -> chọn 1 dòng -> trả kết quả qua biến static
// MaChon (mã của dòng) và DongChon (cả đối tượng), form gọi đọc 2 biến này để điền form.
public partial class FrmChon : Form
{
    public static string MaChon = "";
    public static object DongChon;

    private IList dsGoc;
    private List<object> dsHienThi = new List<object>();
    private string tenMa;
    private PropertyInfo[] cot;

    public FrmChon()
    {
        InitializeComponent();
    }

    public FrmChon(string tieuDe, IList ds, string tenMa) : this()
    {
        Text = tieuDe;
        this.tenMa = tenMa;
        dsGoc = ds;
        if (ds.Count > 0)
        {
            cot = ds[0].GetType().GetProperties().Where(p => p.Name != "MatKhau").ToArray();
            foreach (PropertyInfo p in cot)
                lvDS.Columns.Add(p.Name, 120);
        }
        HienThi(TimKiemBUS.TheoTuKhoa(dsGoc, ""));
    }

    private void HienThi(List<object> ds)
    {
        dsHienThi = ds;
        lvDS.Items.Clear();
        foreach (object x in ds)
        {
            ListViewItem dong = new ListViewItem(TimKiemBUS.ChuoiHienThi(cot[0].GetValue(x)));
            for (int i = 1; i < cot.Length; i++)
                dong.SubItems.Add(TimKiemBUS.ChuoiHienThi(cot[i].GetValue(x)));
            lvDS.Items.Add(dong);
        }
        lblSoDong.Text = ds.Count + " dòng";
    }

    private void txtTuKhoa_TextChanged(object sender, EventArgs e)
    {
        if (cot != null)
            HienThi(TimKiemBUS.TheoTuKhoa(dsGoc, txtTuKhoa.Text));
    }

    private void btnChon_Click(object sender, EventArgs e)
    {
        if (lvDS.SelectedIndices.Count == 0)
        {
            MessageBox.Show("Chọn một dòng.");
            return;
        }
        DongChon = dsHienThi[lvDS.SelectedIndices[0]];
        MaChon = DongChon.GetType().GetProperty(tenMa).GetValue(DongChon)?.ToString() ?? "";
        DialogResult = DialogResult.OK;
    }

    private void lvDS_DoubleClick(object sender, EventArgs e)
    {
        btnChon_Click(sender, e);
    }

    private void btnHuy_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}
