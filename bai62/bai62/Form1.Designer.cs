namespace bai62
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpNhanVien = new System.Windows.Forms.GroupBox();
            this.grpChamCong = new System.Windows.Forms.GroupBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.txtLuongCoBan = new System.Windows.Forms.TextBox();
            this.txtHeSoLuong = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.cboTimPhongBan = new System.Windows.Forms.ComboBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.btnNapCong = new System.Windows.Forms.Button();
            this.btnLuuCong = new System.Windows.Forms.Button();
            this.btnChotLuong = new System.Windows.Forms.Button();
            this.dgvChamCong = new System.Windows.Forms.DataGridView();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout();
           
            this.grpNhanVien.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(15, 28), AutoSize = true, Text = "Họ tên:" });
            this.grpNhanVien.Controls.Add(this.txtHoTen);
            this.grpNhanVien.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(235, 28), AutoSize = true, Text = "Ngày sinh:" });
            this.grpNhanVien.Controls.Add(this.dtpNgaySinh);
            this.grpNhanVien.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(435, 28), AutoSize = true, Text = "CCCD:" });
            this.grpNhanVien.Controls.Add(this.txtCCCD);
            this.grpNhanVien.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(15, 63), AutoSize = true, Text = "Phòng ban:" });
            this.grpNhanVien.Controls.Add(this.cboPhongBan);
            this.grpNhanVien.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(245, 63), AutoSize = true, Text = "Lương cơ bản:" });
            this.grpNhanVien.Controls.Add(this.txtLuongCoBan);
            this.grpNhanVien.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(435, 63), AutoSize = true, Text = "Hệ số lương:" });
            this.grpNhanVien.Controls.Add(this.txtHeSoLuong);
            this.grpNhanVien.Controls.Add(this.btnThem);
            this.grpNhanVien.Controls.Add(this.btnSua);
            this.grpNhanVien.Controls.Add(this.btnXoa);
            this.grpNhanVien.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(255, 97), AutoSize = true, Text = "Tìm:" });
            this.grpNhanVien.Controls.Add(this.txtTimKiem);
            this.grpNhanVien.Controls.Add(this.cboTimPhongBan);
            this.grpNhanVien.Controls.Add(this.btnTim);
            this.grpNhanVien.Controls.Add(this.dgvNhanVien);
            this.grpNhanVien.Location = new System.Drawing.Point(12, 12);
            this.grpNhanVien.Size = new System.Drawing.Size(790, 370);
            this.grpNhanVien.Text = "1. Hồ sơ nhân sự (3-Tier CRUD)";
           
            this.txtHoTen.Location = new System.Drawing.Point(75, 25);
            this.txtHoTen.Size = new System.Drawing.Size(150, 23);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(305, 25);
            this.dtpNgaySinh.Size = new System.Drawing.Size(120, 23);
            this.txtCCCD.Location = new System.Drawing.Point(480, 25);
            this.txtCCCD.Size = new System.Drawing.Size(130, 23);
            this.cboPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhongBan.Location = new System.Drawing.Point(95, 60);
            this.cboPhongBan.Size = new System.Drawing.Size(140, 23);
            this.txtLuongCoBan.Location = new System.Drawing.Point(335, 60);
            this.txtLuongCoBan.Size = new System.Drawing.Size(90, 23);
            this.txtHeSoLuong.Location = new System.Drawing.Point(515, 60);
            this.txtHeSoLuong.Size = new System.Drawing.Size(60, 23);
            
            this.btnThem.Location = new System.Drawing.Point(15, 92);
            this.btnThem.Size = new System.Drawing.Size(75, 27);
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Location = new System.Drawing.Point(95, 92);
            this.btnSua.Size = new System.Drawing.Size(75, 27);
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Location = new System.Drawing.Point(175, 92);
            this.btnXoa.Size = new System.Drawing.Size(75, 27);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
          
            this.txtTimKiem.Location = new System.Drawing.Point(290, 93);
            this.txtTimKiem.Size = new System.Drawing.Size(110, 23);
            this.cboTimPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimPhongBan.Location = new System.Drawing.Point(405, 92);
            this.cboTimPhongBan.Size = new System.Drawing.Size(130, 23);
            this.btnTim.Location = new System.Drawing.Point(545, 92);
            this.btnTim.Size = new System.Drawing.Size(90, 27);
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Location = new System.Drawing.Point(15, 125);
            this.dgvNhanVien.Size = new System.Drawing.Size(760, 235);
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);
            
            this.grpChamCong.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(15, 30), AutoSize = true, Text = "Tháng:" });
            this.grpChamCong.Controls.Add(this.cboThang);
            this.grpChamCong.Controls.Add(new System.Windows.Forms.Label() { Location = new System.Drawing.Point(130, 30), AutoSize = true, Text = "Năm:" });
            this.grpChamCong.Controls.Add(this.cboNam);
            this.grpChamCong.Controls.Add(this.btnNapCong);
            this.grpChamCong.Controls.Add(this.btnLuuCong);
            this.grpChamCong.Controls.Add(this.btnChotLuong);
            this.grpChamCong.Controls.Add(this.dgvChamCong);
            this.grpChamCong.Location = new System.Drawing.Point(12, 390);
            this.grpChamCong.Size = new System.Drawing.Size(790, 330);
            this.grpChamCong.Text = "2. Chấm công (Disconnected Model) & 3. Chốt lương (SqlTransaction)";
           
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.Location = new System.Drawing.Point(65, 27);
            this.cboThang.Size = new System.Drawing.Size(60, 23);
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNam.Location = new System.Drawing.Point(170, 27);
            this.cboNam.Size = new System.Drawing.Size(80, 23);
            
            this.btnNapCong.Location = new System.Drawing.Point(270, 25);
            this.btnNapCong.Size = new System.Drawing.Size(110, 27);
            this.btnNapCong.Text = "Nạp chấm công";
            this.btnNapCong.Click += new System.EventHandler(this.btnNapCong_Click);
            this.btnLuuCong.Location = new System.Drawing.Point(390, 25);
            this.btnLuuCong.Size = new System.Drawing.Size(110, 27);
            this.btnLuuCong.Text = "Lưu chấm công";
            this.btnLuuCong.Click += new System.EventHandler(this.btnLuuCong_Click);
            this.btnChotLuong.Location = new System.Drawing.Point(510, 25);
            this.btnChotLuong.Size = new System.Drawing.Size(110, 27);
            this.btnChotLuong.Text = "Chốt lương tháng";
            this.btnChotLuong.Click += new System.EventHandler(this.btnChotLuong_Click);
           
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.HeaderText = "Họ tên";
            this.colHoTen.ReadOnly = true;
            this.colHoTen.Width = 160;
            this.dgvChamCong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colHoTen });
            this.dgvChamCong.AllowUserToAddRows = false;
            this.dgvChamCong.Location = new System.Drawing.Point(15, 60);
            this.dgvChamCong.Size = new System.Drawing.Size(760, 250);
            
            this.ClientSize = new System.Drawing.Size(820, 735);
            this.Controls.Add(this.grpNhanVien);
            this.Controls.Add(this.grpChamCong);
            this.Name = "Form1";
            this.Text = "Bài 6.2 - Hệ thống quản lý nhân sự & tính lương (bai62)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpNhanVien;
        private System.Windows.Forms.GroupBox grpChamCong;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.TextBox txtLuongCoBan;
        private System.Windows.Forms.TextBox txtHeSoLuong;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.ComboBox cboTimPhongBan;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.Button btnNapCong;
        private System.Windows.Forms.Button btnLuuCong;
        private System.Windows.Forms.Button btnChotLuong;
        private System.Windows.Forms.DataGridView dgvChamCong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
    }
}
