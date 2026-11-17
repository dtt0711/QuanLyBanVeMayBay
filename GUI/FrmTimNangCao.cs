using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

// Form nhập điều kiện tìm kiếm nâng cao. Kết quả trả về qua biến static DieuKien,
// form gọi lấy DieuKien rồi chuyển cho hàm TimNangCao của BUS tương ứng.
public partial class FrmTimNangCao : Form
{
    public static List<DieuKienDTO> DieuKien = new List<DieuKienDTO>();

    private Type kieu;

    public FrmTimNangCao()
    {
        InitializeComponent();
    }

    public FrmTimNangCao(Type kieu, string tieuDe) : this()
    {
        this.kieu = kieu;
        Text = "Tìm nâng cao - " + tieuDe;
        colNoi.Items.AddRange("AND", "OR");
        colCot.Items.AddRange(TimKiemBUS.DSCot(kieu).ToArray());
        colPhepToan.Items.AddRange(TimKiemBUS.PhepToan);
        dgvDieuKien.DefaultValuesNeeded += (s, e) =>
        {
            e.Row.Cells["colNoi"].Value = "AND";
            e.Row.Cells["colKhong"].Value = false;
            e.Row.Cells["colPhepToan"].Value = "=";
        };
        dgvDieuKien.DataError += (s, e) => e.ThrowException = false;
    }

    private void btnTim_Click(object sender, EventArgs e)
    {
        dgvDieuKien.EndEdit();
        List<DieuKienDTO> ds = new List<DieuKienDTO>();
        foreach (DataGridViewRow r in dgvDieuKien.Rows)
        {
            if (r.IsNewRow)
                continue;
            string cot = r.Cells["colCot"].Value?.ToString() ?? "";
            if (cot == "")
                continue;
            DieuKienDTO dk = new DieuKienDTO(
                r.Cells["colNoi"].Value?.ToString() ?? "AND",
                r.Cells["colKhong"].Value is bool b && b,
                cot,
                r.Cells["colPhepToan"].Value?.ToString() ?? "=",
                r.Cells["colGiaTri"].Value?.ToString() ?? "");
            string loi = TimKiemBUS.KiemTraDieuKien(kieu, dk);
            if (loi != "")
            {
                MessageBox.Show("Dòng " + (r.Index + 1) + ": " + loi);
                return;
            }
            ds.Add(dk);
        }
        if (ds.Count == 0)
        {
            MessageBox.Show("Nhập ít nhất một điều kiện (chọn field, phép toán, giá trị).");
            return;
        }
        DieuKien = ds;
        DialogResult = DialogResult.OK;
    }

    private void btnXoaHet_Click(object sender, EventArgs e)
    {
        dgvDieuKien.Rows.Clear();
    }

    private void btnHuy_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}
