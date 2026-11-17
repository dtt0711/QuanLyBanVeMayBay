using System.Data;
using QuanLyBanVeMayBay.BUS;

namespace QuanLyBanVeMayBay.GUI;

// Thanh tìm kiếm dùng chung, đặt giữa phần nhập liệu và danh sách của mỗi form.
// Form chỉ việc bắt 3 sự kiện Tim, TimNangCao, TatCa rồi gọi hàm tìm kiếm trong BUS.
public class ThanhTimKiem : UserControl
{
    private Label lblTim;
    private TextBox txtTuKhoa;
    private Button btnTim;
    private Button btnTimNangCao;
    private Button btnTatCa;
    private Button btnXuatExcel;
    private Button btnNhapExcel;
    private Label lblKetQua;

    public event EventHandler Tim;
    public event EventHandler TimNangCao;
    public event EventHandler TatCa;
    // Form nào cho nhập Excel thì gọi ChoNhapExcel() và bắt sự kiện này (nhận bảng đọc từ file)
    public event Action<DataTable> NhapExcel;

    public void ChoNhapExcel()
    {
        btnNhapExcel.Visible = true;
    }

    public string TuKhoa => txtTuKhoa.Text;

    public ThanhTimKiem()
    {
        lblTim = new Label();
        txtTuKhoa = new TextBox();
        btnTim = new Button();
        btnTimNangCao = new Button();
        btnTatCa = new Button();
        btnXuatExcel = new Button();
        btnNhapExcel = new Button();
        lblKetQua = new Label();

        lblTim.AutoSize = true;
        lblTim.Location = new Point(20, 15);
        lblTim.Text = "Tìm kiếm:";
        txtTuKhoa.Location = new Point(130, 11);
        txtTuKhoa.Size = new Size(280, 23);
        txtTuKhoa.PlaceholderText = "Nhập từ khóa rồi nhấn Enter";
        txtTuKhoa.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Tim?.Invoke(this, EventArgs.Empty);
            }
        };
        btnTim.Location = new Point(420, 8);
        btnTim.Size = new Size(90, 30);
        btnTim.Text = "Tìm";
        btnTim.Click += (s, e) => Tim?.Invoke(this, EventArgs.Empty);
        btnTimNangCao.Location = new Point(520, 8);
        btnTimNangCao.Size = new Size(130, 30);
        btnTimNangCao.Text = "Tìm nâng cao";
        btnTimNangCao.Click += (s, e) => TimNangCao?.Invoke(this, EventArgs.Empty);
        btnTatCa.Location = new Point(660, 8);
        btnTatCa.Size = new Size(90, 30);
        btnTatCa.Text = "Tất cả";
        btnTatCa.Click += (s, e) =>
        {
            txtTuKhoa.Clear();
            TatCa?.Invoke(this, EventArgs.Empty);
        };
        btnXuatExcel.Location = new Point(760, 8);
        btnXuatExcel.Size = new Size(110, 30);
        btnXuatExcel.Text = "Xuất Excel";
        btnXuatExcel.Click += (s, e) => XuatExcel();
        btnNhapExcel.Location = new Point(880, 8);
        btnNhapExcel.Size = new Size(110, 30);
        btnNhapExcel.Text = "Nhập Excel";
        btnNhapExcel.Visible = false;
        btnNhapExcel.Click += (s, e) => DocExcel();
        lblKetQua.AutoSize = true;
        lblKetQua.Location = new Point(1004, 15);

        Controls.Add(lblTim);
        Controls.Add(txtTuKhoa);
        Controls.Add(btnTim);
        Controls.Add(btnTimNangCao);
        Controls.Add(btnTatCa);
        Controls.Add(btnXuatExcel);
        Controls.Add(btnNhapExcel);
        Controls.Add(lblKetQua);
        Dock = DockStyle.Top;
        Height = 46;
        BackColor = Color.White;
        Name = "thanhTim";
    }

    // Xuất đúng những dòng đang hiện trong danh sách của form (kể cả kết quả tìm kiếm)
    private void XuatExcel()
    {
        ListView lv = Parent?.Controls.OfType<ListView>().FirstOrDefault(l => l.Dock == DockStyle.Fill);
        Form f = FindForm();
        if (lv == null || f == null)
            return;
        string tieuDe = f.Text.Replace("Quản lý ", "");
        string duongDan = GiaoDien.HoiNoiLuu(GiaoDien.TenFile(tieuDe), "xlsx");
        if (duongDan == "")
            return;
        try
        {
            ExcelBUS.Xuat(duongDan, (tieuDe, GiaoDien.BangTuListView(lv)));
            GiaoDien.BaoDaLuu(duongDan);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không xuất được file Excel: " + ex.Message);
        }
    }

    private void DocExcel()
    {
        string duongDan = GiaoDien.HoiMoFileExcel();
        if (duongDan == "")
            return;
        DataTable bang;
        try
        {
            bang = ExcelBUS.Doc(duongDan);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không đọc được file Excel (file đang mở trong Excel?): " + ex.Message);
            return;
        }
        NhapExcel?.Invoke(bang);
    }

    public void HienSoDong(int soDong)
    {
        lblKetQua.Text = soDong + " dòng";
    }
}
