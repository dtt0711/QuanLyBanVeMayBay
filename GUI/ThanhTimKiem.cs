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
    private Label lblKetQua;

    public event EventHandler Tim;
    public event EventHandler TimNangCao;
    public event EventHandler TatCa;

    public string TuKhoa => txtTuKhoa.Text;

    public ThanhTimKiem()
    {
        lblTim = new Label();
        txtTuKhoa = new TextBox();
        btnTim = new Button();
        btnTimNangCao = new Button();
        btnTatCa = new Button();
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
        lblKetQua.AutoSize = true;
        lblKetQua.Location = new Point(764, 15);

        Controls.Add(lblTim);
        Controls.Add(txtTuKhoa);
        Controls.Add(btnTim);
        Controls.Add(btnTimNangCao);
        Controls.Add(btnTatCa);
        Controls.Add(lblKetQua);
        Dock = DockStyle.Top;
        Height = 46;
        BackColor = Color.White;
        Name = "thanhTim";
    }

    public void HienSoDong(int soDong)
    {
        lblKetQua.Text = soDong + " dòng";
    }
}
