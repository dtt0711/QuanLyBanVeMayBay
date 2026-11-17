using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmVe : Form
{
    private List<VeDTO> ds = new List<VeDTO>();
    private bool dangNap;

    public FrmVe()
    {
        InitializeComponent();
        cboMaChuyenBay.Format += (s, e) => { if (e.ListItem is ChuyenBayDTO p) e.Value = p.MaSanBayDi == "" ? p.MaChuyenBay : p.MaChuyenBay + " - " + p.MaSanBayDi + "→" + p.MaSanBayDen; };
        cboMaHV.Format += (s, e) => { if (e.ListItem is HangVeDTO p) e.Value = p.TenHV == "" ? p.MaHV : p.MaHV + " - " + p.TenHV; };
        cboMaGhe.Format += (s, e) => { if (e.ListItem is GheDTO p) e.Value = p.MaMayBay == "" ? p.MaGhe : p.MaGhe + " - " + p.MaHV; };
        cboMaHK.Format += (s, e) => { if (e.ListItem is HanhKhachDTO p) e.Value = p.HoTen == "" ? p.MaHK : p.MaHK + " - " + p.HoTen; };
        cboMaDV.Format += (s, e) => { if (e.ListItem is DatVeDTO p) e.Value = p.MaKH == "" ? p.MaDV : p.MaDV + " - " + p.MaKH; };
        GiaoDien.ThemNutChon(cboMaChuyenBay, "Chọn chuyến bay");
        GiaoDien.ThemNutChon(cboMaGhe, "Chọn ghế");
        GiaoDien.ThemNutChon(cboMaHK, "Chọn hành khách");
        GiaoDien.ThemNutChon(cboMaDV, "Chọn phiếu đặt vé");
        // Field không cho nhập: hạng vé lấy theo ghế, đơn giá lấy theo hạng vé
        cboMaHV.Enabled = false;
        GiaoDien.KhoaO(txtDonGiaVe);
        cboMaGhe.SelectedIndexChanged += cboMaGhe_SelectedIndexChanged;
        cboMaDV.SelectedIndexChanged += cboMaDV_SelectedIndexChanged;
    }

    private void FrmVe_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSVe_Click(sender, e);
    }

    private void NapCombo()
    {
        List<ChuyenBayDTO> chuyen = new List<ChuyenBayDTO>();
        chuyen.Add(new ChuyenBayDTO { MaChuyenBay = "Chọn chuyến bay" });
        chuyen.AddRange(new ChuyenBayBUS().DocDSCB());
        cboMaChuyenBay.DataSource = chuyen;
        cboMaChuyenBay.DisplayMember = "MaChuyenBay";
        cboMaChuyenBay.ValueMember = "MaChuyenBay";
        cboMaChuyenBay.SelectedIndex = 0;
        List<HangVeDTO> hangVe = new List<HangVeDTO>();
        hangVe.Add(new HangVeDTO { MaHV = "Chọn hạng vé" });
        hangVe.AddRange(new HangVeBUS().DocDSHV());
        cboMaHV.DataSource = hangVe;
        cboMaHV.DisplayMember = "MaHV";
        cboMaHV.ValueMember = "MaHV";
        cboMaHV.SelectedIndex = 0;
        GanGheVaDatVe("", "");
    }

    private static string GiaTri(ComboBox cbo)
    {
        string v = cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        return v.StartsWith("Chọn ") ? "" : v;
    }

    // Ghế chỉ lấy của máy bay thực hiện chuyến, phiếu đặt chỉ lấy của chuyến đó
    private void GanGheVaDatVe(string maChuyenBay, string maMayBay)
    {
        List<GheDTO> ghe = new List<GheDTO>();
        ghe.Add(new GheDTO { MaGhe = "Chọn ghế" });
        if (maMayBay != "") ghe.AddRange(new GheBUS().DocTheoMayBay(maMayBay));
        cboMaGhe.DataSource = ghe;
        cboMaGhe.DisplayMember = "MaGhe";
        cboMaGhe.ValueMember = "MaGhe";
        cboMaGhe.SelectedIndex = 0;
        List<DatVeDTO> datVe = new List<DatVeDTO>();
        datVe.Add(new DatVeDTO { MaDV = "Chọn đặt vé" });
        if (maChuyenBay != "") datVe.AddRange(new DatVeBUS().DocTheoChuyenBay(maChuyenBay));
        cboMaDV.DataSource = datVe;
        cboMaDV.DisplayMember = "MaDV";
        cboMaDV.ValueMember = "MaDV";
        cboMaDV.SelectedIndex = 0;
    }

    private void cboMaChuyenBay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dangNap) return;
        string maMayBay = cboMaChuyenBay.SelectedItem is ChuyenBayDTO cb ? cb.MaMayBay : "";
        GanGheVaDatVe(GiaTri(cboMaChuyenBay), maMayBay);
    }

    // Hành khách chỉ lấy những người thuộc khách hàng đứng tên phiếu đặt
    private void cboMaDV_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<HanhKhachDTO> hk = new List<HanhKhachDTO>();
        hk.Add(new HanhKhachDTO { MaHK = "Chọn hành khách" });
        if (cboMaDV.SelectedItem is DatVeDTO dv && dv.MaKH != "")
            hk.AddRange(new HanhKhachBUS().DocTheoKH(dv.MaKH));
        cboMaHK.DataSource = hk;
        cboMaHK.DisplayMember = "MaHK";
        cboMaHK.ValueMember = "MaHK";
        cboMaHK.SelectedIndex = 0;
    }

    // Chọn ghế -> tự điền hạng vé của ghế; đơn giá = giá chuyến bay x hệ số hạng vé
    private void cboMaGhe_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboMaGhe.SelectedItem is GheDTO g && g.MaHV != "")
        {
            cboMaHV.SelectedValue = g.MaHV;
            txtDonGiaVe.Text = VeBUS.LayDonGia(GiaTri(cboMaChuyenBay), g.MaHV).ToString("0");
        }
        else
        {
            cboMaHV.SelectedIndex = 0;
            txtDonGiaVe.Clear();
        }
    }

    private void NapDong(VeDTO ve)
    {
        int n = lvDSVe.Items.Count;
        lvDSVe.Items.Add(ve.MaVe);
        lvDSVe.Items[n].SubItems.Add(ve.MaChuyenBay);
        lvDSVe.Items[n].SubItems.Add(ve.MaHV);
        lvDSVe.Items[n].SubItems.Add(ve.MaGhe);
        lvDSVe.Items[n].SubItems.Add(ve.MaHK);
        lvDSVe.Items[n].SubItems.Add(ve.MaDV);
        lvDSVe.Items[n].SubItems.Add(ve.DonGiaVe.ToString("N0"));
        lvDSVe.Items[n].SubItems.Add(ve.Ngay.ToString("dd/MM/yyyy HH:mm"));
    }

    private void HienThi(List<VeDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSVe.Items.Clear();
        foreach (VeDTO ve in ds)
            NapDong(ve);
        thanhTim.HienSoDong(ds.Count);
    }

    // Đọc dữ liệu trên form và kiểm tra; trả về null nếu không hợp lệ
    private VeDTO LayTuForm(bool laThem)
    {
        if (GiaTri(cboMaChuyenBay) == "" || GiaTri(cboMaHV) == "" || GiaTri(cboMaGhe) == "" || GiaTri(cboMaHK) == "")
        {
            MessageBox.Show("Chọn chuyến bay, ghế, phiếu đặt và hành khách.");
            return null;
        }
        decimal donGia = 0;
        decimal.TryParse(txtDonGiaVe.Text, out donGia);
        VeDTO ve = new VeDTO(txtMaVe.Text, GiaTri(cboMaChuyenBay), GiaTri(cboMaHV), GiaTri(cboMaGhe), GiaTri(cboMaHK), donGia, dtpNgay.Value, GiaTri(cboMaDV));
        string loi = new VeBUS().KiemTra(ve, laThem);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return null;
        }
        return ve;
    }

    private void btnDocDSVe_Click(object sender, EventArgs e)
    {
        HienThi(new VeBUS().DocDSVe());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        VeBUS bus = new VeBUS();
        if (txtMaVe.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaVe.Text))
        {
            MessageBox.Show("Mã " + txtMaVe.Text + " đã tồn tại.");
            return;
        }
        VeDTO ve = LayTuForm(true);
        if (ve == null) return;
        if (!bus.Them(ve))
            return;
        btnDocDSVe_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        VeDTO ve = LayTuForm(false);
        if (ve == null) return;
        VeBUS bus = new VeBUS();
        bus.Sua(ve);
        HienThi(bus.DocDSVe());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        VeBUS bus = new VeBUS();
        bus.Xoa(txtMaVe.Text);
        HienThi(bus.DocDSVe());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSVe.SelectedIndices.Clear();
        txtMaVe.Clear();
        cboMaChuyenBay.SelectedIndex = 0;
        cboMaHV.SelectedIndex = 0;
        GanGheVaDatVe("", "");
        txtDonGiaVe.Clear();
        dtpNgay.Value = DateTime.Now;
        txtMaVe.Enabled = true;
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new VeBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(VeDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new VeBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSVe_Click(sender, e);
    }

    private void lvDSVe_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSVe.SelectedIndices.Count == 0) return;
        int i = lvDSVe.SelectedIndices[0];
        if (i >= ds.Count) return;
        VeDTO ve = ds[i];
        dangNap = true;
        txtMaVe.Text = ve.MaVe;
        cboMaChuyenBay.SelectedValue = ve.MaChuyenBay;
        ChuyenBayDTO cb = new ChuyenBayBUS().timKiem(ve.MaChuyenBay);
        GanGheVaDatVe(ve.MaChuyenBay, cb == null ? "" : cb.MaMayBay);
        cboMaGhe.SelectedValue = ve.MaGhe;
        cboMaHV.SelectedValue = ve.MaHV;
        if (ve.MaDV == "") cboMaDV.SelectedIndex = 0;
        else cboMaDV.SelectedValue = ve.MaDV;
        cboMaHK.SelectedValue = ve.MaHK;
        txtDonGiaVe.Text = ve.DonGiaVe.ToString("0");
        dtpNgay.Value = ve.Ngay;
        txtMaVe.Enabled = false;
        dangNap = false;
    }
}
