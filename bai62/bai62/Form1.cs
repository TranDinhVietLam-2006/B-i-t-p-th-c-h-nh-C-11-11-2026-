using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace bai62
{
    // ==================== Tầng 1: Entity ====================
    public class NhanVien
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public int MaPB { get; set; }
        public decimal LuongCoBan { get; set; }
        public double HeSoLuong { get; set; }
    }

    // ==================== Tầng 2: DAL (truy cập dữ liệu) ====================
    public static class NhanVienDAL
    {
        public const string ConnStr =
     @"Server=(localdb)\MSSQLLocalDB;Database=HRMDB;Trusted_Connection=True;TrustServerCertificate=True";


        public static DataTable GetPhongBan()
        {
            using (var da = new SqlDataAdapter("SELECT MaPB, TenPB FROM PhongBan", ConnStr))
            { var dt = new DataTable(); da.Fill(dt); return dt; }
        }

        // Tìm kiếm parameterized: theo tên, CCCD, phòng ban
        public static DataTable TimKiem(string tuKhoa, int maPB)
        {
            const string sql = @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.CCCD, nv.MaPB, pb.TenPB,
                                        nv.LuongCoBan, nv.HeSoLuong
                                 FROM NhanVien nv JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                                 WHERE (nv.HoTen LIKE @tuKhoa OR nv.CCCD LIKE @tuKhoa)
                                   AND (@maPB = 0 OR nv.MaPB = @maPB)";
            using (var da = new SqlDataAdapter(sql, ConnStr))
            {
                da.SelectCommand.Parameters.AddWithValue("@tuKhoa", "%" + tuKhoa + "%");
                da.SelectCommand.Parameters.AddWithValue("@maPB", maPB);
                var dt = new DataTable(); da.Fill(dt); return dt;
            }
        }

        public static void Them(NhanVien nv)
        {
            const string sql = @"INSERT INTO NhanVien(HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong)
                                 VALUES(@HoTen, @NgaySinh, @CCCD, @MaPB, @LuongCoBan, @HeSoLuong)";
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@CCCD", (object)nv.CCCD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaPB", nv.MaPB);
                cmd.Parameters.AddWithValue("@LuongCoBan", nv.LuongCoBan);
                cmd.Parameters.AddWithValue("@HeSoLuong", nv.HeSoLuong);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        // Sửa hồ sơ + chuyển phòng ban
        public static void Sua(NhanVien nv)
        {
            const string sql = @"UPDATE NhanVien
                                 SET HoTen=@HoTen, NgaySinh=@NgaySinh, CCCD=@CCCD,
                                     MaPB=@MaPB, LuongCoBan=@LuongCoBan, HeSoLuong=@HeSoLuong
                                 WHERE MaNV=@MaNV";
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@CCCD", (object)nv.CCCD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaPB", nv.MaPB);
                cmd.Parameters.AddWithValue("@LuongCoBan", nv.LuongCoBan);
                cmd.Parameters.AddWithValue("@HeSoLuong", nv.HeSoLuong);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        // Xóa (có transaction vì xóa cả dữ liệu liên quan)
        public static void Xoa(int maNV)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (var tr = conn.BeginTransaction())
                {
                    try
                    {
                        Exec(conn, tr, "DELETE FROM BangLuong     WHERE MaNV = @id", maNV);
                        Exec(conn, tr, "DELETE FROM BangChamCong  WHERE MaNV = @id", maNV);
                        Exec(conn, tr, "DELETE FROM NhanVien       WHERE MaNV = @id", maNV);
                        tr.Commit();
                    }
                    catch { tr.Rollback(); throw; }
                }
            }
        }

        private static void Exec(SqlConnection conn, SqlTransaction tr, string sql, int maNV)
        {
            using (var cmd = new SqlCommand(sql, conn, tr))
            { cmd.Parameters.AddWithValue("@id", maNV); cmd.ExecuteNonQuery(); }
        }
    }

    // ==================== Tầng 3: BLL (nghiệp vụ) ====================
    public static class NhanVienBLL
    {
        public static DataTable LayPhongBan() => NhanVienDAL.GetPhongBan();
        public static DataTable TimKiem(string tuKhoa, int maPB) => NhanVienDAL.TimKiem(tuKhoa, maPB);
        public static void Them(NhanVien nv) { KiemTra(nv); NhanVienDAL.Them(nv); }
        public static void Sua(NhanVien nv) { KiemTra(nv); NhanVienDAL.Sua(nv); }
        public static void Xoa(int maNV) => NhanVienDAL.Xoa(maNV);

        private static void KiemTra(NhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.HoTen))
                throw new Exception("Họ tên không được để trống.");
            if (nv.LuongCoBan <= 0)
                throw new Exception("Lương cơ bản phải > 0.");
            if (nv.HeSoLuong < 0)
                throw new Exception("Hệ số lương phải >= 0.");
        }
    }

    // ==================== Giao diện ====================
    public partial class Form1 : Form
    {
        // Chấm công ngắt kết nối (Disconnected Model)
        private SqlDataAdapter congAdapter;
        private SqlCommandBuilder congBuilder;
        private DataTable congTable;
        private string congThangNam;

        public Form1()
        {
            InitializeComponent();
        }

        // ---------- Load form ----------
        private void Form1_Load(object sender, EventArgs e)
        {
            // ComboBox chọn phòng ban khi nhập/sửa
            cboPhongBan.DataSource = NhanVienBLL.LayPhongBan();
            cboPhongBan.ValueMember = "MaPB";
            cboPhongBan.DisplayMember = "TenPB";
            cboPhongBan.DropDownStyle = ComboBoxStyle.DropDownList;

            // ComboBox phòng ban khi tìm kiếm (có "Tất cả")
            DataTable dtPB = NhanVienBLL.LayPhongBan();
            DataRow r = dtPB.NewRow(); r["MaPB"] = 0; r["TenPB"] = "-- Tất cả --";
            dtPB.Rows.InsertAt(r, 0);
            cboTimPhongBan.DataSource = dtPB;
            cboTimPhongBan.ValueMember = "MaPB";
            cboTimPhongBan.DisplayMember = "TenPB";
            cboTimPhongBan.DropDownStyle = ComboBoxStyle.DropDownList;

            // Tháng / Năm chấm công
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
            for (int y = 2024; y <= 2035; y++) cboNam.Items.Add(y);
            cboThang.SelectedItem = DateTime.Today.Month;
            cboNam.SelectedItem = DateTime.Today.Year;

            TaiDanhSach();
        }

        private string LayThangNam()
        {
            int thang = (int)cboThang.SelectedItem;
            int nam = (int)cboNam.SelectedItem;
            return thang.ToString("00") + "/" + nam;   // ví dụ: "11/2026"
        }

        private void TaiDanhSach()
        {
            int maPB = Convert.ToInt32(cboTimPhongBan.SelectedValue);
            dgvNhanVien.DataSource = NhanVienBLL.TimKiem(txtTimKiem.Text.Trim(), maPB);
        }

        // ---------- 1. CRUD nhân viên (3-Tier) ----------
        private NhanVien DocThongTin(int maNV)
        {
            decimal luongCB;
            double heSo;
            if (!decimal.TryParse(txtLuongCoBan.Text.Trim(), out luongCB))
                throw new Exception("Lương cơ bản không hợp lệ.");
            if (txtHeSoLuong.Text.Trim() == "") heSo = 1.0;      // Default 1.0
            else if (!double.TryParse(txtHeSoLuong.Text.Trim(), out heSo))
                throw new Exception("Hệ số lương không hợp lệ.");
            if (cboPhongBan.SelectedValue == null)
                throw new Exception("Chọn phòng ban.");

            return new NhanVien
            {
                MaNV = maNV,
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                CCCD = txtCCCD.Text.Trim(),
                MaPB = Convert.ToInt32(cboPhongBan.SelectedValue),
                LuongCoBan = luongCB,
                HeSoLuong = heSo
            };
        }

        private int MaNVDangChon()
        {
            if (dgvNhanVien.CurrentRow == null)
                throw new Exception("Chọn một nhân viên trong danh sách.");
            return Convert.ToInt32(dgvNhanVien.CurrentRow.Cells["MaNV"].Value);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try { NhanVienBLL.Them(DocThongTin(0)); TaiDanhSach(); MessageBox.Show("Đã thêm nhân viên."); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try { NhanVienBLL.Sua(DocThongTin(MaNVDangChon())); TaiDanhSach(); MessageBox.Show("Đã cập nhật (kể cả chuyển phòng ban)."); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int maNV = MaNVDangChon();
                if (MessageBox.Show("Xóa nhân viên này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { NhanVienBLL.Xoa(maNV); TaiDanhSach(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvNhanVien.Rows[e.RowIndex];
            txtHoTen.Text = r.Cells["HoTen"].Value + "";
            dtpNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);
            txtCCCD.Text = r.Cells["CCCD"].Value + "";
            cboPhongBan.SelectedValue = Convert.ToInt32(r.Cells["MaPB"].Value);
            txtLuongCoBan.Text = r.Cells["LuongCoBan"].Value + "";
            txtHeSoLuong.Text = r.Cells["HeSoLuong"].Value + "";
        }

        // ---------- 2. Chấm công ngắt kết nối (DataAdapter + CommandBuilder) ----------
        private bool DaChotLuong(string thangNam)
        {
            using (var conn = new SqlConnection(NhanVienDAL.ConnStr))
            using (var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM BangLuong WHERE ThangNam = @tn", conn))
            {
                cmd.Parameters.AddWithValue("@tn", thangNam);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;   // tháng đã chốt => chấm công bị khóa
            }
        }

        private void btnNapCong_Click(object sender, EventArgs e)
        {
            try
            {
                congThangNam = LayThangNam();

                // Tháng đã chốt lương => khóa dữ liệu chấm công
                if (DaChotLuong(congThangNam))
                { MessageBox.Show("Tháng " + congThangNam + " đã chốt lương, chấm công bị khóa!", "Cảnh báo"); return; }

                // Tự tạo dòng chấm công cho nhân viên chưa có trong tháng
                using (var conn = new SqlConnection(NhanVienDAL.ConnStr))
                using (var cmd = new SqlCommand(@"
                    INSERT INTO BangChamCong(MaNV, ThangNam, SoNgayCong, SoGioOT)
                    SELECT nv.MaNV, @tn, 0, 0 FROM NhanVien nv
                    WHERE NOT EXISTS (SELECT 1 FROM BangChamCong
                                      WHERE MaNV = nv.MaNV AND ThangNam = @tn)", conn))
                {
                    cmd.Parameters.AddWithValue("@tn", congThangNam);
                    conn.Open(); cmd.ExecuteNonQuery();
                }

                // Nạp toàn bộ chấm công tháng vào DataTable (Disconnected Model)
                congAdapter = new SqlDataAdapter(
                    "SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT " +
                    "FROM BangChamCong WHERE ThangNam = @ThangNam", NhanVienDAL.ConnStr);
                congAdapter.SelectCommand.Parameters.AddWithValue("@ThangNam", congThangNam);
                congTable = new DataTable();
                congAdapter.Fill(congTable);
                dgvChamCong.DataSource = congTable;
                DienHoTenChamCong();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        // Cột "Họ tên" là cột unbound (không thuộc DataTable => không ảnh hưởng Update)
        private void DienHoTenChamCong()
        {
            var ten = new Dictionary<int, string>();
            using (var da = new SqlDataAdapter("SELECT MaNV, HoTen FROM NhanVien", NhanVienDAL.ConnStr))
            {
                DataTable dt = new DataTable(); da.Fill(dt);
                foreach (DataRow r in dt.Rows)
                    ten[Convert.ToInt32(r["MaNV"])] = r["HoTen"].ToString();
            }
            foreach (DataGridViewRow g in dgvChamCong.Rows)
                if (!g.IsNewRow)
                {
                    int maNV = Convert.ToInt32(g.Cells["MaNV"].Value);
                    g.Cells["colHoTen"].Value = ten.ContainsKey(maNV) ? ten[maNV] : "";
                }
        }

        // Lưu chấm công: SqlCommandBuilder + adapter.Update() => 1 thao tác đồng bộ
        private void btnLuuCong_Click(object sender, EventArgs e)
        {
            if (congTable == null) { MessageBox.Show("Hãy bấm 'Nạp chấm công' trước!"); return; }
            if (DaChotLuong(congThangNam))
            { MessageBox.Show("Tháng " + congThangNam + " đã chốt lương, không được sửa chấm công!", "Cảnh báo"); return; }

            try
            {
                dgvChamCong.EndEdit();
                congBuilder = new SqlCommandBuilder(congAdapter);   // sinh INSERT/UPDATE/DELETE
                int n = congAdapter.Update(congTable);               // đồng bộ về SQL Server
                MessageBox.Show("Đã lưu " + n + " dòng chấm công về SQL Server.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }

        // ---------- 3. Chốt lương tháng (SqlTransaction) ----------
        private void btnChotLuong_Click(object sender, EventArgs e)
        {
            try
            {
                string tn = LayThangNam();

                // Kiểm tra có dữ liệu chấm công trong tháng
                int soDong = 0;
                using (var conn = new SqlConnection(NhanVienDAL.ConnStr))
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM BangChamCong WHERE ThangNam = @tn", conn))
                {
                    cmd.Parameters.AddWithValue("@tn", tn);
                    conn.Open(); soDong = (int)cmd.ExecuteScalar();
                }
                if (soDong == 0) { MessageBox.Show("Chưa có dữ liệu chấm công tháng " + tn); return; }

                if (MessageBox.Show("Chốt lương tháng " + tn + " và khóa chấm công?",
                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                using (var conn = new SqlConnection(NhanVienDAL.ConnStr))
                {
                    conn.Open();
                    using (SqlTransaction tr = conn.BeginTransaction())
                    {
                        try
                        {
                            // Xóa lương cũ nếu chốt lại
                            using (var cmd = new SqlCommand(
                                "DELETE FROM BangLuong WHERE ThangNam = @tn", conn, tr))
                            { cmd.Parameters.AddWithValue("@tn", tn); cmd.ExecuteNonQuery(); }

                            // Công thức: (LuongCoBan * HeSoLuong / 22) * SoNgayCong + SoGioOT * 50000
                            using (var cmd = new SqlCommand(@"
                                INSERT INTO BangLuong(MaNV, ThangNam, ThucLinh)
                                SELECT cc.MaNV, cc.ThangNam,
                                       CAST((nv.LuongCoBan * nv.HeSoLuong / 22) * cc.SoNgayCong
                                            + (cc.SoGioOT * 50000) AS DECIMAL(18,2))
                                FROM BangChamCong cc
                                JOIN NhanVien nv ON nv.MaNV = cc.MaNV
                                WHERE cc.ThangNam = @tn", conn, tr))
                            {
                                cmd.Parameters.AddWithValue("@tn", tn);
                                int n = cmd.ExecuteNonQuery();
                            }
                            tr.Commit();
                            MessageBox.Show("Đã chốt lương tháng " + tn +
                                ". Dữ liệu chấm công đã bị khóa.");
                        }
                        catch
                        {
                            tr.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi"); }
        }
    }
}
