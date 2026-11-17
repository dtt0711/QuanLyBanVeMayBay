using QuanLyBanVeMayBay.BUS;
using QuanLyBanVeMayBay.DTO;

namespace QuanLyBanVeMayBay.GUI;

public partial class FrmDatVe : Form
{
    private List<DatVeDTO> ds = new List<DatVeDTO>();

    public FrmDatVe()
    {
        InitializeComponent();
        cboMaKH.Format += (s, e) => { if (e.ListItem is KhachHangDTO p) e.Value = p.HoTen == "" ? p.MaKH : p.MaKH + " - " + p.HoTen; };
        cboMaNV.Format += (s, e) => { if (e.ListItem is NhanVienDTO p) e.Value = p.Ho == "" ? p.MaNV : p.MaNV + " - " + p.Ho + " " + p.Ten; };
        cboMaChuyenBay.Format += (s, e) => { if (e.ListItem is ChuyenBayDTO p) e.Value = p.MaSanBayDi == "" ? p.MaChuyenBay : p.MaChuyenBay + " - " + p.MaSanBayDi + "→" + p.MaSanBayDen; };
        cboMaVoucher.Format += (s, e) => { if (e.ListItem is VoucherDTO p) e.Value = p.MaCTKM == "" ? p.MaVoucher : p.MaVoucher + " - " + p.GiamGia.ToString("N0"); };
        GiaoDien.ThemNutChon(cboMaKH, "Chọn khách hàng");
        GiaoDien.ThemNutChon(cboMaNV, "Chọn nhân viên");
        GiaoDien.ThemNutChon(cboMaChuyenBay, "Chọn chuyến bay");
        GiaoDien.ThemNutChon(cboMaVoucher, "Chọn voucher");
        // Field không cho nhập: giảm giá lấy theo voucher, tổng tiền = tổng giá vé - giảm giá
        GiaoDien.KhoaO(txtTongTien);
        // "Đã thanh toán" chỉ có khi lập hóa đơn, nên danh sách chọn chỉ có Chờ thanh toán / Đã hủy
        cboTrangThai.Items.AddRange(TrangThaiBUS.DatVe);
        cboTrangThai.SelectedIndex = 0;
        GiaoDien.KhoaO(txtGiamGia);
        cboMaVoucher.SelectedIndexChanged += (s, e) => txtGiamGia.Text = DatVeBUS.LayGiamGia(GiaTri(cboMaVoucher)).ToString("0");
    }

    private void FrmDatVe_Load(object sender, EventArgs e)
    {
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            return;
        NapCombo();
        btnDocDSDV_Click(sender, e);
    }

    private void NapCombo()
    {
        List<KhachHangDTO> kh = new List<KhachHangDTO>();
        kh.Add(new KhachHangDTO { MaKH = "Chọn khách hàng" });
        kh.AddRange(new KhachHangBUS().DocDSKH());
        cboMaKH.DataSource = kh;
        cboMaKH.DisplayMember = "MaKH";
        cboMaKH.ValueMember = "MaKH";
        cboMaKH.SelectedIndex = 0;
        List<NhanVienDTO> nv = new List<NhanVienDTO>();
        nv.Add(new NhanVienDTO { MaNV = "Chọn nhân viên" });
        nv.AddRange(new NhanVienBUS().DocDSNV());
        cboMaNV.DataSource = nv;
        cboMaNV.DisplayMember = "MaNV";
        cboMaNV.ValueMember = "MaNV";
        cboMaNV.SelectedIndex = 0;
        List<ChuyenBayDTO> cb = new List<ChuyenBayDTO>();
        cb.Add(new ChuyenBayDTO { MaChuyenBay = "Chọn chuyến bay" });
        cb.AddRange(new ChuyenBayBUS().DocDSCB());
        cboMaChuyenBay.DataSource = cb;
        cboMaChuyenBay.DisplayMember = "MaChuyenBay";
        cboMaChuyenBay.ValueMember = "MaChuyenBay";
        cboMaChuyenBay.SelectedIndex = 0;
        List<VoucherDTO> vc = new List<VoucherDTO>();
        vc.Add(new VoucherDTO { MaVoucher = "Chọn voucher" });
        vc.AddRange(new VoucherBUS().DocDSVC());
        cboMaVoucher.DataSource = vc;
        cboMaVoucher.DisplayMember = "MaVoucher";
        cboMaVoucher.ValueMember = "MaVoucher";
        cboMaVoucher.SelectedIndex = 0;
    }

    private static string GiaTri(ComboBox cbo)
    {
        string v = cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        return v.StartsWith("Chọn ") ? "" : v;
    }

    private void NapDong(DatVeDTO dv)
    {
        int n = lvDSDV.Items.Count;
        lvDSDV.Items.Add(dv.MaDV);
        lvDSDV.Items[n].SubItems.Add(dv.NgayDat.ToString("dd/MM/yyyy HH:mm"));
        lvDSDV.Items[n].SubItems.Add(dv.MaKH);
        lvDSDV.Items[n].SubItems.Add(dv.MaNV);
        lvDSDV.Items[n].SubItems.Add(dv.MaChuyenBay);
        lvDSDV.Items[n].SubItems.Add(dv.MaVoucher);
        lvDSDV.Items[n].SubItems.Add(dv.GiamGia.ToString("N0"));
        lvDSDV.Items[n].SubItems.Add(dv.TongTien.ToString("N0"));
        lvDSDV.Items[n].SubItems.Add(dv.TrangThai);
    }

    private void HienThi(List<DatVeDTO> dsMoi)
    {
        ds = dsMoi;
        lvDSDV.Items.Clear();
        foreach (DatVeDTO dv in ds)
            NapDong(dv);
        thanhTim.HienSoDong(ds.Count);
        HienThiVe("");
    }

    // ListView con: các vé (n dòng) của phiếu đặt vé đang chọn (1 dòng)
    private void HienThiVe(string maDV)
    {
        lvDSVeCon.Items.Clear();
        if (maDV == "")
        {
            lblVeCon.Text = "Vé của phiếu đặt: chọn một phiếu đặt ở danh sách trên";
            return;
        }
        decimal tong = 0;
        List<VeDTO> dsVe = new VeBUS().DocTheoDatVe(maDV);
        foreach (VeDTO ve in dsVe)
        {
            ListViewItem dong = new ListViewItem(ve.MaVe);
            dong.SubItems.Add(ve.MaChuyenBay);
            dong.SubItems.Add(ve.MaHV);
            dong.SubItems.Add(ve.MaGhe);
            dong.SubItems.Add(ve.MaHK);
            dong.SubItems.Add(ve.DonGiaVe.ToString("N0"));
            dong.SubItems.Add(ve.TrangThai);
            if (ve.TrangThai == TrangThaiBUS.DaHuy)
                dong.ForeColor = Color.Gray;
            else
                tong += ve.DonGiaVe;
            lvDSVeCon.Items.Add(dong);
        }
        int soHieuLuc = dsVe.Count(v => v.TrangThai != TrangThaiBUS.DaHuy);
        lblVeCon.Text = "Vé của phiếu đặt " + maDV + ": " + soHieuLuc + " vé hiệu lực"
            + (dsVe.Count > soHieuLuc ? ", " + (dsVe.Count - soHieuLuc) + " vé đã hủy" : "") + ", tổng giá vé " + tong.ToString("N0") + " đ";
    }

    // Đọc dữ liệu trên form và kiểm tra; trả về null nếu không hợp lệ
    private DatVeDTO LayTuForm(bool laThem)
    {
        if (GiaTri(cboMaKH) == "" || GiaTri(cboMaChuyenBay) == "")
        {
            MessageBox.Show("Chọn khách hàng và chuyến bay.");
            return null;
        }
        decimal tongTien = 0;
        decimal.TryParse(txtTongTien.Text, out tongTien);
        decimal giamGia = DatVeBUS.LayGiamGia(GiaTri(cboMaVoucher));
        string trangThai = cboTrangThai.Text == "" ? "Chờ thanh toán" : cboTrangThai.Text;
        DatVeDTO dv = new DatVeDTO(txtMaDV.Text, GiaTri(cboMaKH), GiaTri(cboMaNV), GiaTri(cboMaChuyenBay), dtpNgayDat.Value, tongTien, giamGia, trangThai, GiaTri(cboMaVoucher));
        string loi = new DatVeBUS().KiemTra(dv, laThem);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return null;
        }
        return dv;
    }

    private void btnDocDSDV_Click(object sender, EventArgs e)
    {
        HienThi(new DatVeBUS().DocDSDV());
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        DatVeBUS bus = new DatVeBUS();
        if (txtMaDV.Text.Trim() == "")
        {
            MessageBox.Show("Nhập mã trước khi thêm.");
            return;
        }
        if (bus.TrungMa(txtMaDV.Text))
        {
            MessageBox.Show("Mã " + txtMaDV.Text + " đã tồn tại.");
            return;
        }
        DatVeDTO dv = LayTuForm(true);
        if (dv == null) return;
        if (!bus.Them(dv))
            return;
        btnDocDSDV_Click(sender, e);
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        DatVeDTO dv = LayTuForm(false);
        if (dv == null) return;
        DatVeBUS bus = new DatVeBUS();
        DatVeDTO cu = bus.timKiem(dv.MaDV);
        if (dv.TrangThai == TrangThaiBUS.DaHuy && cu != null && cu.TrangThai != TrangThaiBUS.DaHuy
            && MessageBox.Show("Hủy phiếu đặt " + dv.MaDV + " sẽ hủy luôn các vé của phiếu. Tiếp tục?", "Hủy phiếu đặt", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;
        bus.Sua(dv);
        HienThi(bus.DocDSDV());
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        DatVeBUS bus = new DatVeBUS();
        string loi = bus.KiemTraXoa(txtMaDV.Text);
        if (loi != "")
        {
            MessageBox.Show(loi);
            return;
        }
        bus.Xoa(txtMaDV.Text);
        HienThi(bus.DocDSDV());
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        lvDSDV.SelectedIndices.Clear();
        txtMaDV.Clear();
        dtpNgayDat.Value = DateTime.Now;
        cboMaKH.SelectedIndex = 0;
        cboMaNV.SelectedIndex = 0;
        cboMaChuyenBay.SelectedIndex = 0;
        cboMaVoucher.SelectedIndex = 0;
        txtTongTien.Text = "0";
        txtGiamGia.Text = "0";
        cboTrangThai.Items.Remove(TrangThaiBUS.DaThanhToan);
        cboTrangThai.SelectedIndex = 0;
        txtMaDV.Enabled = true;
        HienThiVe("");
    }

    private void btnXemVe_Click(object sender, EventArgs e)
    {
        new FrmVe().ShowDialog();
        DatVeBUS.LamMoiDS();
        btnDocDSDV_Click(sender, e);
    }

    private void thanhTim_Tim(object sender, EventArgs e)
    {
        HienThi(new DatVeBUS().TimKiem(thanhTim.TuKhoa));
    }

    private void thanhTim_TimNangCao(object sender, EventArgs e)
    {
        if (new FrmTimNangCao(typeof(DatVeDTO), Text).ShowDialog(this) == DialogResult.OK)
            HienThi(new DatVeBUS().TimNangCao(FrmTimNangCao.DieuKien));
    }

    private void thanhTim_TatCa(object sender, EventArgs e)
    {
        btnDocDSDV_Click(sender, e);
    }

    private void lvDSDV_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvDSDV.SelectedIndices.Count == 0) return;
        int i = lvDSDV.SelectedIndices[0];
        if (i >= ds.Count) return;
        DatVeDTO dv = ds[i];
        txtMaDV.Text = dv.MaDV;
        dtpNgayDat.Value = dv.NgayDat;
        cboMaKH.SelectedValue = dv.MaKH;
        if (dv.MaNV == "") cboMaNV.SelectedIndex = 0;
        else cboMaNV.SelectedValue = dv.MaNV;
        cboMaChuyenBay.SelectedValue = dv.MaChuyenBay;
        if (dv.MaVoucher == "") cboMaVoucher.SelectedIndex = 0;
        else cboMaVoucher.SelectedValue = dv.MaVoucher;
        txtTongTien.Text = dv.TongTien.ToString("0");
        txtGiamGia.Text = dv.GiamGia.ToString("0");
        // phiếu đã thanh toán: hiện trạng thái đó (chỉ để xem, không chọn được khi tạo mới)
        if (dv.TrangThai == TrangThaiBUS.DaThanhToan && !cboTrangThai.Items.Contains(TrangThaiBUS.DaThanhToan))
            cboTrangThai.Items.Add(TrangThaiBUS.DaThanhToan);
        cboTrangThai.Text = dv.TrangThai;
        txtMaDV.Enabled = false;
        HienThiVe(dv.MaDV);
    }
}
