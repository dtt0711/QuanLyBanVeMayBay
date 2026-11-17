using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmHoaDon : Form
{
    private List<HoaDonDTO> ds = new List<HoaDonDTO>();
    private bool dangNap;

    public FrmHoaDon()
    {
        InitializeComponent();
        cboMaNV.Format += (s, e) => { if (e.ListItem is NhanVienDTO p) e.Value = p.Ho == "" ? p.MaNV : p.MaNV + " - " + p.Ho + " " + p.Ten; };
        cboMaKH.Format += (s, e) => { if (e.ListItem is KhachHangDTO p) e.Value = p.HoTen == "" ? p.MaKH : p.MaKH + " - " + p.HoTen; };
        cboMaDV.Format += (s, e) => { if (e.ListItem is DatVeDTO p) e.Value = p.MaKH == "" ? p.MaDV : p.MaDV + " - " + p.MaChuyenBay; };
        GiaoDien.ThemNutChon(cboMaDV, "Chọn phiếu đặt vé");
        GiaoDien.ThemNutChon(cboMaNV, "Chọn nhân viên");
        // Khách hàng không cho nhập: lấy theo phiếu đặt vé
        cboMaKH.Enabled = false;
        cboTrangThai.Items.AddRange(TrangThaiBUS.HoaDon);
        cboTrangThai.SelectedIndex = 0;
        cboPhuongThucTT.Items.AddRange(TrangThaiBUS.PhuongThucTT);
        cboPhuongThucTT.SelectedIndex = 0;
        cboMaDV.SelectedIndexChanged += cboMaDV_SelectedIndexChanged;
    }

    private void FrmHoaDon_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSHD_Click(sender, e);
    }

    private void NapCombo()
    {
        List<NhanVienDTO> nv = new List<NhanVienDTO>();
        nv.Add(new NhanVienDTO { MaNV = "Chọn nhân viên" });
        nv.AddRange(new NhanVienBUS().DocDSNV());
        cboMaNV.DataSource = nv;
        cboMaNV.DisplayMember = "MaNV";
        cboMaNV.ValueMember = "MaNV";
        cboMaNV.SelectedIndex = 0;
        List<KhachHangDTO> kh = new List<KhachHangDTO>();
        kh.Add(new KhachHangDTO { MaKH = "Chọn khách hàng" });
        kh.AddRange(new KhachHangBUS().DocDSKH());
        cboMaKH.DataSource = kh;
        cboMaKH.DisplayMember = "MaKH";
        cboMaKH.ValueMember = "MaKH";
        cboMaKH.SelectedIndex = 0;
        List<DatVeDTO> dv = new List<DatVeDTO>();
        dv.Add(new DatVeDTO { MaDV = "Chọn đặt vé" });
        dv.AddRange(new DatVeBUS().DocDSDV());
        cboMaDV.DataSource = dv;
        cboMaDV.DisplayMember = "MaDV";
        cboMaDV.ValueMember = "MaDV";
        cboMaDV.SelectedIndex = 0;
    }

    private static string GiaTriCombo(ComboBox cbo)
    {
        string v = cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        return v.StartsWith("Chọn ") ? "" : v;
    }

    // Chọn phiếu đặt vé -> lấy thông tin khách hàng, nhân viên của phiếu điền vào form
    private void cboMaDV_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dangNap) return;
        if (cboMaDV.SelectedItem is DatVeDTO dv && dv.MaKH != "")
        {
            cboMaKH.SelectedValue = dv.MaKH;
            if (dv.MaNV == "") cboMaNV.SelectedIndex = 0;
            else cboMaNV.SelectedValue = dv.MaNV;
        }
        else
            cboMaKH.SelectedIndex = 0;
    }

    private void NapDong(HoaDonDTO hd)
    {
        int n = lvDSHD.Items.Count;
        lvDSHD.Items.Add(hd.MaHD);
        lvDSHD.Items[n].SubItems.Add(hd.NgayLap.ToString("dd/MM/yyyy HH:mm"));
        lvDSHD.Items[n].SubItems.Add(hd.MaKH);
        lvDSHD.Items[n].SubItems.Add(hd.MaNV);
        lvDSHD.Items[n].SubItems.Add(hd.MaDV);
        lvDSHD.Items[n].SubItems.Add(hd.PhuongThucTT);
        lvDSHD.Items[n].SubItems.Add(hd.TrangThai);
        lvDSHD.Items[n].SubItems.Add(hd.TongTien.ToString("N0"));
    }

    private void HienThi(List<HoaDonDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSHD.Items.Clear();
        foreach (HoaDonDTO hd in ds)
            NapDong(hd);
        thanhTim.HienSoDong(ds.Count);
        HienThiCTHD("");
    }

    // ListView con: click 1 dòng hóa đơn -> hiện n dòng chi tiết hóa đơn
    private void HienThiCTHD(string maHD)
    {
        lvDSCTHD.Items.Clear();
        if (maHD == "")
        {
            lblCTHD.Text = "Chi tiết hóa đơn: chọn một hóa đơn ở danh sách trên";
            return;
        }
        decimal tong = 0;
        List<CTHDDTO> dsCT = new CTHDBUS().DocTheoHD(maHD);
        VeBUS veBUS = new VeBUS();
        foreach (CTHDDTO ct in dsCT)
        {
            VeDTO ve = veBUS.timKiem(ct.MaVe);
            ListViewItem dong = new ListViewItem(ct.MaVe);
            dong.SubItems.Add(ve == null ? "" : ve.MaChuyenBay);
            dong.SubItems.Add(ve == null ? "" : ve.MaHV);
            dong.SubItems.Add(ct.SL.ToString());
            dong.SubItems.Add(ct.DonGiaVe.ToString("N0"));
            dong.SubItems.Add(ct.ThanhTien.ToString("N0"));
            lvDSCTHD.Items.Add(dong);
            tong += ct.ThanhTien;
        }
        HoaDonDTO hd = new HoaDonBUS().timKiem(maHD);
        DatVeDTO dv = hd == null ? null : new DatVeBUS().timKiem(hd.MaDV);
        decimal giam = dv == null ? 0 : dv.GiamGia;
        lblCTHD.Text = "Chi tiết hóa đơn " + maHD + ": " + dsCT.Count + " dòng, cộng thành tiền " + tong.ToString("N0")
            + " - giảm giá phiếu đặt " + giam.ToString("N0") + " = tổng tiền " + Math.Max(tong - giam, 0).ToString("N0") + " đ";
    }

    private HoaDonDTO LayTuForm()
    {
        if (GiaTriCombo(cboMaDV) == "" || GiaTriCombo(cboMaKH) == "")
        {
            MessageBox.Show("Chọn phiếu đặt vé.");
            return null;
        }
        decimal tongTien = 0;
        decimal.TryParse(txtTongTien.Text, out tongTien);
        string trangThai = cboTrangThai.Text == "" ? "Chưa thanh toán" : cboTrangThai.Text;
        return new HoaDonDTO(txtMaHD.Text, GiaTriCombo(cboMaKH), GiaTriCombo(cboMaNV), GiaTriCombo(cboMaDV), dtpNgayLap.Value, tongTien, cboPhuongThucTT.Text, trangThai);
    }

    private void btnDocDSHD_Click(object sender, EventArgs e)
    {
        HienThi(new HoaDonBUS().DocDSHD());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        HoaDonDTO hd = LayTuForm();
        if (hd == null) return;
        HoaDonBUS bus = new HoaDonBUS();
        if (txtMaHD.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaHD.Text))
        {
            MessageBox.Show("Mã " + txtMaHD.Text + " đã tồn tại.");
            return;
        }
        string loi = bus.KiemTra(hd);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        if (!bus.Them(hd))
            return;
        btnDocDSHD_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        HoaDonDTO hd = LayTuForm();
        if (hd == null) return;
        HoaDonBUS bus = new HoaDonBUS();
        string loi = bus.KiemTra(hd);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Sua(hd);
        HienThi(bus.DocDSHD());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        HoaDonBUS bus = new HoaDonBUS();
        string loi = bus.KiemTraXoa(txtMaHD.Text);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Xoa(txtMaHD.Text);
        HienThi(bus.DocDSHD());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSHD.SelectedIndices.Clear();
        txtMaHD.Clear();
        dtpNgayLap.Value = DateTime.Now;
        cboMaNV.SelectedIndex = 0;
        cboMaKH.SelectedIndex = 0;
        cboMaDV.SelectedIndex = 0;
        cboPhuongThucTT.SelectedIndex = 0;
        cboTrangThai.SelectedIndex = 0;
        txtTongTien.Text = "0";
        txtMaHD.Enabled = true;
        HienThiCTHD("");
    }

    private void btnChiTiet_Click(object sender, EventArgs e)
    {
        if (txtMaHD.Text == "" || txtMaHD.Enabled)
        {
            MessageBox.Show("Chọn một hóa đơn trong danh sách.");
            return;
        }
        string ma = txtMaHD.Text;
        new FrmCTHD(ma).ShowDialog();
        btnDocDSHD_Click(sender, e);
        int i = ds.FindIndex(h => h.MaHD == ma);
        if (i >= 0)
            lvDSHD.Items[i].Selected = true;
    }

    // Xuất hóa đơn đang chọn ra file PDF (thư viện QuestPDF)
    private void btnInHD_Click(object sender, EventArgs e)
    {
        if (txtMaHD.Text == "" || txtMaHD.Enabled)
        {
            MessageBox.Show("Chọn một hóa đơn trong danh sách.");
            return;
        }
        string duongDan = GiaoDien.HoiNoiLuu("HoaDon_" + txtMaHD.Text, "pdf");
        if (duongDan == "")
            return;
        try
        {
            PdfBUS.XuatHoaDon(txtMaHD.Text, duongDan);
            GiaoDien.BaoDaLuu(duongDan);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Không xuất được PDF (file đang mở?): " + ex.Message);
        }
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new HoaDonBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(HoaDonDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new HoaDonBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSHD_Click(sender, e);
    }

    private void lvDSHD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSHD.SelectedIndices.Count == 0) return;
        int i = lvDSHD.SelectedIndices[0];
        if (i >= ds.Count) return;
        HoaDonDTO hd = ds[i];
        dangNap = true;
        txtMaHD.Text = hd.MaHD;
        dtpNgayLap.Value = hd.NgayLap;
        cboMaDV.SelectedValue = hd.MaDV;
        cboMaKH.SelectedValue = hd.MaKH;
        if (hd.MaNV == "")
            cboMaNV.SelectedIndex = 0;
        else
            cboMaNV.SelectedValue = hd.MaNV;
        cboPhuongThucTT.Text = hd.PhuongThucTT;
        cboTrangThai.Text = hd.TrangThai;
        txtTongTien.Text = hd.TongTien.ToString("N0");
        txtMaHD.Enabled = false;
        dangNap = false;
        HienThiCTHD(hd.MaHD);
    }
}
