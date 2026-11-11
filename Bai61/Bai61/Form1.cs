using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Bai61
{
    public class NhanVien
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string SoDienThoai { get; set; }
        public string ChucVu { get; set; }
    }

    public class KhachHang
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string DienThoai { get; set; }
        public int DiemTichLuy { get; set; }
    }

    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
    }

    public class ChiTietHoaDon
    {
        public int MaHD { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }

    public class CartItem
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get { return SoLuong * DonGia; } }
    }

    public class DBConnection
    {
        public static string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SalesDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=False";
    }

    public class SanPhamDAL
    {
        public List<SanPham> GetAll()
        {
            List<SanPham> list = new List<SanPham>();
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SanPham
                    {
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                        SoLuongTon = Convert.ToInt32(reader["SoLuongTon"])
                    });
                }
            }
            return list;
        }

        public List<SanPham> Search(string keyword)
        {
            List<SanPham> list = new List<SanPham>();
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham WHERE TenSP LIKE @kw OR MaSP LIKE @kw", conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SanPham
                    {
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                        SoLuongTon = Convert.ToInt32(reader["SoLuongTon"])
                    });
                }
            }
            return list;
        }

        public SanPham GetByMa(string maSP)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham WHERE MaSP=@MaSP", conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new SanPham
                    {
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                        SoLuongTon = Convert.ToInt32(reader["SoLuongTon"])
                    };
                }
            }
            return null;
        }

        public bool Insert(SanPham sp)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO SanPham(MaSP, TenSP, GiaBan, SoLuongTon) VALUES(@MaSP, @TenSP, @GiaBan, @SoLuongTon)", conn);
                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(SanPham sp)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE SanPham SET TenSP=@TenSP, GiaBan=@GiaBan, SoLuongTon=@SoLuongTon WHERE MaSP=@MaSP", conn);
                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@GiaBan", sp.GiaBan);
                cmd.Parameters.AddWithValue("@SoLuongTon", sp.SoLuongTon);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string maSP)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM SanPham WHERE MaSP=@MaSP", conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public class KhachHangDAL
    {
        public List<KhachHang> GetAll()
        {
            List<KhachHang> list = new List<KhachHang>();
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaKH, TenKH, DienThoai, DiemTichLuy FROM KhachHang", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new KhachHang
                    {
                        MaKH = Convert.ToInt32(reader["MaKH"]),
                        TenKH = reader["TenKH"].ToString(),
                        DienThoai = reader["DienThoai"].ToString(),
                        DiemTichLuy = Convert.ToInt32(reader["DiemTichLuy"])
                    });
                }
            }
            return list;
        }

        public bool Insert(KhachHang kh)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO KhachHang(TenKH, DienThoai, DiemTichLuy) VALUES(@TenKH, @DienThoai, @DiemTichLuy)", conn);
                cmd.Parameters.AddWithValue("@TenKH", kh.TenKH);
                cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(kh.DienThoai) ? (object)DBNull.Value : kh.DienThoai);
                cmd.Parameters.AddWithValue("@DiemTichLuy", kh.DiemTichLuy);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(KhachHang kh)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE KhachHang SET TenKH=@TenKH, DienThoai=@DienThoai, DiemTichLuy=@DiemTichLuy WHERE MaKH=@MaKH", conn);
                cmd.Parameters.AddWithValue("@MaKH", kh.MaKH);
                cmd.Parameters.AddWithValue("@TenKH", kh.TenKH);
                cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(kh.DienThoai) ? (object)DBNull.Value : kh.DienThoai);
                cmd.Parameters.AddWithValue("@DiemTichLuy", kh.DiemTichLuy);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maKH)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKH=@MaKH", conn);
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public class NhanVienDAL
    {
        public List<NhanVien> GetAll()
        {
            List<NhanVien> list = new List<NhanVien>();
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaNV, TenNV, SoDienThoai, ChucVu FROM NhanVien", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new NhanVien
                    {
                        MaNV = Convert.ToInt32(reader["MaNV"]),
                        TenNV = reader["TenNV"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        ChucVu = reader["ChucVu"].ToString()
                    });
                }
            }
            return list;
        }

        public bool Insert(NhanVien nv)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO NhanVien(TenNV, SoDienThoai, ChucVu) VALUES(@TenNV, @SoDienThoai, @ChucVu)", conn);
                cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrEmpty(nv.SoDienThoai) ? (object)DBNull.Value : nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@ChucVu", string.IsNullOrEmpty(nv.ChucVu) ? (object)DBNull.Value : nv.ChucVu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(NhanVien nv)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET TenNV=@TenNV, SoDienThoai=@SoDienThoai, ChucVu=@ChucVu WHERE MaNV=@MaNV", conn);
                cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrEmpty(nv.SoDienThoai) ? (object)DBNull.Value : nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@ChucVu", string.IsNullOrEmpty(nv.ChucVu) ? (object)DBNull.Value : nv.ChucVu);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maNV)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNV=@MaNV", conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public class HoaDonDAL
    {
        public bool ThanhToan(int maNV, int? maKH, decimal tongTien, List<ChiTietHoaDon> chiTiet, out string error, out int maHD)
        {
            error = "";
            maHD = 0;
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlCommand cmdHD = new SqlCommand("INSERT INTO HoaDon(NgayLap, MaNV, MaKH, TongTien) OUTPUT INSERTED.MaHD VALUES(GETDATE(), @MaNV, @MaKH, @TongTien)", conn, trans);
                    cmdHD.Parameters.AddWithValue("@MaNV", maNV);
                    cmdHD.Parameters.AddWithValue("@MaKH", maKH.HasValue ? (object)maKH.Value : DBNull.Value);
                    cmdHD.Parameters.AddWithValue("@TongTien", tongTien);
                    maHD = Convert.ToInt32(cmdHD.ExecuteScalar());

                    foreach (ChiTietHoaDon ct in chiTiet)
                    {
                        SqlCommand cmdCheck = new SqlCommand("SELECT SoLuongTon FROM SanPham WHERE MaSP=@MaSP", conn, trans);
                        cmdCheck.Parameters.AddWithValue("@MaSP", ct.MaSP);
                        object obj = cmdCheck.ExecuteScalar();
                        int ton = obj != null ? Convert.ToInt32(obj) : 0;
                        if (ton < ct.SoLuong)
                        {
                            trans.Rollback();
                            error = "Sản phẩm " + ct.MaSP + " không đủ tồn kho (còn " + ton + ")!";
                            maHD = 0;
                            return false;
                        }

                        SqlCommand cmdCT = new SqlCommand("INSERT INTO ChiTietHoaDon(MaHD, MaSP, SoLuong, DonGia) VALUES(@MaHD, @MaSP, @SoLuong, @DonGia)", conn, trans);
                        cmdCT.Parameters.AddWithValue("@MaHD", maHD);
                        cmdCT.Parameters.AddWithValue("@MaSP", ct.MaSP);
                        cmdCT.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                        cmdCT.Parameters.AddWithValue("@DonGia", ct.DonGia);
                        cmdCT.ExecuteNonQuery();

                        SqlCommand cmdUpd = new SqlCommand("UPDATE SanPham SET SoLuongTon = SoLuongTon - @SoLuong WHERE MaSP=@MaSP", conn, trans);
                        cmdUpd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                        cmdUpd.Parameters.AddWithValue("@MaSP", ct.MaSP);
                        cmdUpd.ExecuteNonQuery();
                    }

                    if (maKH.HasValue && maKH.Value > 0)
                    {
                        int diem = (int)(tongTien / 100000);
                        if (diem > 0)
                        {
                            SqlCommand cmdDiem = new SqlCommand("UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @Diem WHERE MaKH=@MaKH", conn, trans);
                            cmdDiem.Parameters.AddWithValue("@Diem", diem);
                            cmdDiem.Parameters.AddWithValue("@MaKH", maKH.Value);
                            cmdDiem.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    try { trans.Rollback(); } catch { }
                    error = ex.Message;
                    maHD = 0;
                    return false;
                }
            }
        }

        public decimal DoanhThuNgay(DateTime ngay)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE CAST(NgayLap AS DATE) = @Ngay", conn);
                cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public decimal DoanhThuThang(int thang, int nam)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE MONTH(NgayLap)=@Thang AND YEAR(NgayLap)=@Nam", conn);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public DataTable Top5SanPham()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSP", typeof(string));
            dt.Columns.Add("TenSP", typeof(string));
            dt.Columns.Add("TongSoLuong", typeof(int));
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 5 sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSoLuong FROM ChiTietHoaDon ct INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP GROUP BY sp.MaSP, sp.TenSP ORDER BY TongSoLuong DESC", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add(reader["MaSP"].ToString(), reader["TenSP"].ToString(), Convert.ToInt32(reader["TongSoLuong"]));
                }
            }
            return dt;
        }
    }

    public class SanPhamBUS
    {
        private SanPhamDAL dal = new SanPhamDAL();
        public List<SanPham> GetAll() { return dal.GetAll(); }
        public List<SanPham> Search(string kw) { return dal.Search(kw); }
        public SanPham GetByMa(string maSP) { return dal.GetByMa(maSP); }
        public bool Insert(SanPham sp) { return dal.Insert(sp); }
        public bool Update(SanPham sp) { return dal.Update(sp); }
        public bool Delete(string maSP) { return dal.Delete(maSP); }
    }

    public class KhachHangBUS
    {
        private KhachHangDAL dal = new KhachHangDAL();
        public List<KhachHang> GetAll() { return dal.GetAll(); }
        public bool Insert(KhachHang kh) { return dal.Insert(kh); }
        public bool Update(KhachHang kh) { return dal.Update(kh); }
        public bool Delete(int maKH) { return dal.Delete(maKH); }
    }

    public class NhanVienBUS
    {
        private NhanVienDAL dal = new NhanVienDAL();
        public List<NhanVien> GetAll() { return dal.GetAll(); }
        public bool Insert(NhanVien nv) { return dal.Insert(nv); }
        public bool Update(NhanVien nv) { return dal.Update(nv); }
        public bool Delete(int maNV) { return dal.Delete(maNV); }
    }

    public class HoaDonBUS
    {
        private HoaDonDAL dal = new HoaDonDAL();
        public bool ThanhToan(int maNV, int? maKH, decimal tongTien, List<ChiTietHoaDon> chiTiet, out string error, out int maHD)
        {
            return dal.ThanhToan(maNV, maKH, tongTien, chiTiet, out error, out maHD);
        }
        public decimal DoanhThuNgay(DateTime ngay) { return dal.DoanhThuNgay(ngay); }
        public decimal DoanhThuThang(int thang, int nam) { return dal.DoanhThuThang(thang, nam); }
        public DataTable Top5SanPham() { return dal.Top5SanPham(); }
    }

    public partial class Form1 : Form
    {
        private SanPhamBUS spBus = new SanPhamBUS();
        private KhachHangBUS khBus = new KhachHangBUS();
        private NhanVienBUS nvBus = new NhanVienBUS();
        private HoaDonBUS hdBus = new HoaDonBUS();
        private BindingList<CartItem> cart = new BindingList<CartItem>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            LoadKhachHang();
            LoadNhanVien();
            LoadComboNhanVien();
            LoadComboKhachHang();
            dgvGioHang.DataSource = cart;
            CapNhatTongTien();
        }

        private void LoadSanPham()
        {
            try { dgvSanPham.DataSource = spBus.GetAll(); }
            catch (Exception ex) { MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message); }
        }

        private void LoadKhachHang()
        {
            try { dgvKhachHang.DataSource = khBus.GetAll(); }
            catch (Exception ex) { MessageBox.Show("Lỗi tải khách hàng: " + ex.Message); }
        }

        private void LoadNhanVien()
        {
            try { dgvNhanVien.DataSource = nvBus.GetAll(); }
            catch (Exception ex) { MessageBox.Show("Lỗi tải nhân viên: " + ex.Message); }
        }

        private void LoadComboNhanVien()
        {
            try
            {
                List<NhanVien> list = nvBus.GetAll();
                cboNhanVien.DataSource = null;
                cboNhanVien.DataSource = list;
                cboNhanVien.DisplayMember = "TenNV";
                cboNhanVien.ValueMember = "MaNV";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải nhân viên: " + ex.Message); }
        }

        private void LoadComboKhachHang()
        {
            try
            {
                List<KhachHang> list = new List<KhachHang>();
                list.Add(new KhachHang { MaKH = 0, TenKH = "Khách lẻ", DienThoai = "", DiemTichLuy = 0 });
                list.AddRange(khBus.GetAll());
                cboKhachHang.DataSource = null;
                cboKhachHang.DataSource = list;
                cboKhachHang.DisplayMember = "TenKH";
                cboKhachHang.ValueMember = "MaKH";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải khách hàng: " + ex.Message); }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            SanPham sp = dgvSanPham.Rows[e.RowIndex].DataBoundItem as SanPham;
            if (sp == null) return;
            txtMaSP.Text = sp.MaSP;
            txtTenSP.Text = sp.TenSP;
            txtGiaBan.Text = sp.GiaBan.ToString();
            txtSoLuongTon.Text = sp.SoLuongTon.ToString();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            try
            {
                SanPham sp = new SanPham
                {
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    GiaBan = decimal.Parse(txtGiaBan.Text),
                    SoLuongTon = int.Parse(txtSoLuongTon.Text)
                };
                if (string.IsNullOrEmpty(sp.MaSP) || string.IsNullOrEmpty(sp.TenSP))
                { MessageBox.Show("Vui lòng nhập đầy đủ thông tin!"); return; }
                if (sp.GiaBan <= 0 || sp.SoLuongTon < 0)
                { MessageBox.Show("Giá bán > 0 và số lượng tồn >= 0!"); return; }
                if (spBus.Insert(sp))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadSanPham();
                    btnLamMoiSP_Click(null, null);
                }
                else MessageBox.Show("Thêm thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            try
            {
                SanPham sp = new SanPham
                {
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    GiaBan = decimal.Parse(txtGiaBan.Text),
                    SoLuongTon = int.Parse(txtSoLuongTon.Text)
                };
                if (string.IsNullOrEmpty(sp.MaSP)) { MessageBox.Show("Chọn sản phẩm cần sửa!"); return; }
                if (spBus.Update(sp))
                { MessageBox.Show("Cập nhật thành công!"); LoadSanPham(); }
                else MessageBox.Show("Cập nhật thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            try
            {
                string maSP = txtMaSP.Text.Trim();
                if (string.IsNullOrEmpty(maSP)) { MessageBox.Show("Chọn sản phẩm cần xóa!"); return; }
                if (MessageBox.Show("Xóa sản phẩm " + maSP + "?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                if (spBus.Delete(maSP))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadSanPham();
                    btnLamMoiSP_Click(null, null);
                }
                else MessageBox.Show("Xóa thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoiSP_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtGiaBan.Text = "";
            txtSoLuongTon.Text = "";
            txtTimKiem.Text = "";
            LoadSanPham();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try { dgvSanPham.DataSource = spBus.Search(txtTimKiem.Text.Trim()); }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            KhachHang kh = dgvKhachHang.Rows[e.RowIndex].DataBoundItem as KhachHang;
            if (kh == null) return;
            txtMaKH.Text = kh.MaKH.ToString();
            txtTenKH.Text = kh.TenKH;
            txtDienThoai.Text = kh.DienThoai;
            txtDiemTichLuy.Text = kh.DiemTichLuy.ToString();
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            try
            {
                KhachHang kh = new KhachHang
                {
                    TenKH = txtTenKH.Text.Trim(),
                    DienThoai = txtDienThoai.Text.Trim(),
                    DiemTichLuy = string.IsNullOrEmpty(txtDiemTichLuy.Text) ? 0 : int.Parse(txtDiemTichLuy.Text)
                };
                if (string.IsNullOrEmpty(kh.TenKH)) { MessageBox.Show("Nhập tên khách hàng!"); return; }
                if (khBus.Insert(kh))
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadKhachHang();
                    LoadComboKhachHang();
                    btnLamMoiKH_Click(null, null);
                }
                else MessageBox.Show("Thêm thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaKH.Text)) { MessageBox.Show("Chọn khách hàng cần sửa!"); return; }
                KhachHang kh = new KhachHang
                {
                    MaKH = int.Parse(txtMaKH.Text),
                    TenKH = txtTenKH.Text.Trim(),
                    DienThoai = txtDienThoai.Text.Trim(),
                    DiemTichLuy = string.IsNullOrEmpty(txtDiemTichLuy.Text) ? 0 : int.Parse(txtDiemTichLuy.Text)
                };
                if (khBus.Update(kh))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadKhachHang();
                    LoadComboKhachHang();
                }
                else MessageBox.Show("Cập nhật thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaKH.Text)) { MessageBox.Show("Chọn khách hàng cần xóa!"); return; }
                int maKH = int.Parse(txtMaKH.Text);
                if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                if (khBus.Delete(maKH))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadKhachHang();
                    LoadComboKhachHang();
                    btnLamMoiKH_Click(null, null);
                }
                else MessageBox.Show("Xóa thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDienThoai.Text = "";
            txtDiemTichLuy.Text = "";
            LoadKhachHang();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            NhanVien nv = dgvNhanVien.Rows[e.RowIndex].DataBoundItem as NhanVien;
            if (nv == null) return;
            txtMaNV.Text = nv.MaNV.ToString();
            txtTenNV.Text = nv.TenNV;
            txtSDT.Text = nv.SoDienThoai;
            txtChucVu.Text = nv.ChucVu;
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nv = new NhanVien
                {
                    TenNV = txtTenNV.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    ChucVu = txtChucVu.Text.Trim()
                };
                if (string.IsNullOrEmpty(nv.TenNV)) { MessageBox.Show("Nhập tên nhân viên!"); return; }
                if (nvBus.Insert(nv))
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadNhanVien();
                    LoadComboNhanVien();
                    btnLamMoiNV_Click(null, null);
                }
                else MessageBox.Show("Thêm thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text)) { MessageBox.Show("Chọn nhân viên cần sửa!"); return; }
                NhanVien nv = new NhanVien
                {
                    MaNV = int.Parse(txtMaNV.Text),
                    TenNV = txtTenNV.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    ChucVu = txtChucVu.Text.Trim()
                };
                if (nvBus.Update(nv))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadNhanVien();
                    LoadComboNhanVien();
                }
                else MessageBox.Show("Cập nhật thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text)) { MessageBox.Show("Chọn nhân viên cần xóa!"); return; }
                int maNV = int.Parse(txtMaNV.Text);
                if (MessageBox.Show("Xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                if (nvBus.Delete(maNV))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadNhanVien();
                    LoadComboNhanVien();
                    btnLamMoiNV_Click(null, null);
                }
                else MessageBox.Show("Xóa thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtSDT.Text = "";
            txtChucVu.Text = "";
            LoadNhanVien();
        }

        private void btnThemGio_Click(object sender, EventArgs e)
        {
            try
            {
                string maSP = txtMaSPBan.Text.Trim();
                if (string.IsNullOrEmpty(maSP)) { MessageBox.Show("Nhập mã sản phẩm!"); return; }
                int soLuong;
                if (!int.TryParse(txtSoLuongBan.Text, out soLuong) || soLuong <= 0)
                { MessageBox.Show("Số lượng phải là số nguyên dương!"); return; }
                SanPham sp = spBus.GetByMa(maSP);
                if (sp == null) { MessageBox.Show("Không tìm thấy sản phẩm!"); return; }
                int daCo = 0;
                CartItem existing = null;
                foreach (CartItem it in cart)
                {
                    if (it.MaSP == maSP) { daCo = it.SoLuong; existing = it; break; }
                }
                if (daCo + soLuong > sp.SoLuongTon)
                { MessageBox.Show("Không đủ tồn kho! Còn lại: " + sp.SoLuongTon); return; }
                if (existing != null)
                {
                    existing.SoLuong += soLuong;
                    cart.ResetBindings();
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        MaSP = sp.MaSP,
                        TenSP = sp.TenSP,
                        SoLuong = soLuong,
                        DonGia = sp.GiaBan
                    });
                }
                txtMaSPBan.Text = "";
                txtSoLuongBan.Text = "";
                CapNhatTongTien();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoaGio_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) { MessageBox.Show("Chọn dòng cần xóa!"); return; }
            CartItem it = dgvGioHang.CurrentRow.DataBoundItem as CartItem;
            if (it != null) { cart.Remove(it); CapNhatTongTien(); }
        }

        private void btnLamMoiGio_Click(object sender, EventArgs e)
        {
            cart.Clear();
            CapNhatTongTien();
        }

        private void CapNhatTongTien()
        {
            decimal tong = 0;
            foreach (CartItem it in cart) tong += it.ThanhTien;
            lblTongTienHienThi.Text = "Tổng tiền: " + tong.ToString("N0") + " VNĐ";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboNhanVien.SelectedValue == null) { MessageBox.Show("Chọn nhân viên bán hàng!"); return; }
                if (cart.Count == 0) { MessageBox.Show("Giỏ hàng trống!"); return; }
                int maNV = Convert.ToInt32(cboNhanVien.SelectedValue);
                int? maKH = null;
                if (cboKhachHang.SelectedValue != null)
                {
                    int v = Convert.ToInt32(cboKhachHang.SelectedValue);
                    if (v > 0) maKH = v;
                }
                decimal tongTien = 0;
                List<ChiTietHoaDon> chiTiet = new List<ChiTietHoaDon>();
                foreach (CartItem it in cart)
                {
                    tongTien += it.ThanhTien;
                    chiTiet.Add(new ChiTietHoaDon
                    {
                        MaSP = it.MaSP,
                        SoLuong = it.SoLuong,
                        DonGia = it.DonGia
                    });
                }
                string error;
                int maHD;
                if (hdBus.ThanhToan(maNV, maKH, tongTien, chiTiet, out error, out maHD))
                {
                    int diem = (int)(tongTien / 100000);
                    MessageBox.Show("Thanh toán thành công!\nMã hóa đơn: " + maHD +
                        "\nTổng tiền: " + tongTien.ToString("N0") + " VNĐ" +
                        "\nĐiểm tích lũy cộng: " + (maKH.HasValue ? diem.ToString() : "0 (khách lẻ)"));
                    cart.Clear();
                    CapNhatTongTien();
                    LoadSanPham();
                    LoadKhachHang();
                    LoadComboKhachHang();
                }
                else MessageBox.Show("Thanh toán thất bại!\n" + error);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnDoanhThuNgay_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dt = hdBus.DoanhThuNgay(DateTime.Now);
                lblKetQua.Text = "Doanh thu hôm nay (" + DateTime.Now.ToString("dd/MM/yyyy") + "): " + dt.ToString("N0") + " VNĐ";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnDoanhThuThang_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dt = hdBus.DoanhThuThang(DateTime.Now.Month, DateTime.Now.Year);
                lblKetQua.Text = "Doanh thu tháng " + DateTime.Now.Month + "/" + DateTime.Now.Year + ": " + dt.ToString("N0") + " VNĐ";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnTop5_Click(object sender, EventArgs e)
        {
            try { dgvTop5.DataSource = hdBus.Top5SanPham(); }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}