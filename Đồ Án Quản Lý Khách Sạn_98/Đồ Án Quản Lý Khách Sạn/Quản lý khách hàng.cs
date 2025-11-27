using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    public partial class frmKhach_Hang : Form
    {

        int thaotac = 0; // Biến để xác định thao tác 1-Thêm, 2-Xoá, 3-Sửa
        string makh;
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";

        // Kết nối từ Form Trang chủ (KHÔNG TRUYỀN DỮ LIỆU)
        public frmKhach_Hang()
        {
            InitializeComponent();
        }
        // Kết nối từ Form ĐẶT PHÒNG (TRUYỀN  Mã Khách Hàng từ Đặt Phòng)
        public frmKhach_Hang(string makh)
        {
            InitializeComponent();
            this.makh = makh; 
        }

        //====================== Kết nối dữ liệu vào DataGridView ======================
        private void frmKhach_Hang_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
            LoadKhachHangComboBox();
            txtMaKH.Text = makh;
            txtMaKH.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtCccd.ReadOnly = true;
            txtSdt.ReadOnly = true;

            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        public void RefreshDataGridView()
        {
            string query = "SELECT * FROM KhachHang";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKhach_Hang.DataSource = dt;
                dgvKhach_Hang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        public void ClearInput()
        {
            txtTenKH.Text = "";
            txtCccd.Text = "";
            rdbNam.Checked = false;
            rdbNu.Checked = false;
            txtSdt.Text = "";
            txtQuoctich.Text = "";
        }
        // Kiểm tra CCCD
        public bool CheckCCCD(out string cccd)
        {
            cccd = txtCccd.Text.Trim();
            if (!cccd.All(char.IsDigit))
            {
                MessageBox.Show("CCCD phải chỉ chứa chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra độ dài
            if (cccd.Length != 12)
            {
                MessageBox.Show("CCCD phải có đúng 12 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        // Kiểm tra Số điện thoại
        public bool CheckSDT(out string sdt)
        {
            sdt = txtSdt.Text.Trim();
            if (!sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải chỉ chứa chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (sdt.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Lấy giới tính
        public string GioiTinh()
        {
            if (rdbNam.Checked)
                return "Nam";
            else if (rdbNu.Checked)
                return "Nữ";
            else
                return "";
        }

        // Kiểm tra khách hàng đã sử dụng phòng hay chưa: Rồi - True / Chưa - False
        // Nếu khách hàng đã ở rồi thì nên sẽ tự động ở khi Thanh Toán xong
        public bool KhachHang_DaSudungPhong(string makh)
        {
            string checkKhachHang_DaSudungPhong = "SELECT COUNT(*) FROM DatPhong WHERE MaKH = @MaKH";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand checkCmd = new SqlCommand(checkKhachHang_DaSudungPhong, conn))
            {
                conn.Open();
                checkCmd.Parameters.AddWithValue("@MaKH", makh);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        //====================== Xử lý sự kiện ======================

        //---------------------- Thêm khách hàng ---------------------
        private void btnThem_Click(object sender, EventArgs e)
        {
            thaotac = 1;
            ClearInput();
            txtMaKH.ReadOnly = false; // Tắt chế độ chỉ đọc cho Mã phòng --> Cho phép nhập
            txtTenKH.ReadOnly = false;
            txtCccd.ReadOnly = false;
            txtSdt.ReadOnly = false;
            txtTenKH.Focus();
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
        }
        //---------------------- Xoá khách hàng ---------------------
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhach_Hang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string makh = dgvKhach_Hang.CurrentRow.Cells["MaKH"].Value.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng {makh}?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                if (KhachHang_DaSudungPhong(makh))
                {
                    MessageBox.Show($"Dịch vụ {makh} đang được sử dụng, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                { 
                    string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", makh);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                RefreshDataGridView();
                MessageBox.Show($"Xóa khách hàng {makh} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                thaotac = 0;
            }
        }

        //---------------------- Sửa khách hàng ---------------------
        private void btnSua_Click(object sender, EventArgs e)
        {            
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            thaotac = 3;

            if (dgvKhach_Hang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //txtMaKH.Text = dgvKhach_Hang.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dgvKhach_Hang.CurrentRow.Cells["TenKH"].Value.ToString();
            txtCccd.Text = dgvKhach_Hang.CurrentRow.Cells["CCCD"].Value.ToString();
            string gioitinh = dgvKhach_Hang.CurrentRow.Cells["GioiTinh"].Value.ToString();
            if (gioitinh == "Nam")
                rdbNam.Checked = true;
            else if (gioitinh == "Nữ")
                rdbNu.Checked = true;
            else
            {
                rdbNam.Checked = false;
                rdbNu.Checked = false;
            }
            txtSdt.Text = dgvKhach_Hang.CurrentRow.Cells["SoDienThoai"].Value.ToString();
            txtQuoctich.Text = dgvKhach_Hang.CurrentRow.Cells["QuocTich"].Value.ToString();

            txtMaKH.ReadOnly = true; // Mở chế độ đọc --> Ko cho sửa
        }

        //---------------------- Lưu khách hàng ---------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (thaotac == 0)
            {
                MessageBox.Show("Vui lòng chọn thao tác (Thêm/Xoá/Sửa)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string makhachhang = txtMaKH.Text;
            string tenkh = txtTenKH.Text;
            string gioitinh = GioiTinh();
            string quoctich = txtQuoctich.Text;

            // Kiểm tra đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrWhiteSpace(makh))
            {
                MessageBox.Show("Bạn chưa nhập mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tenkh))
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 
            if (string.IsNullOrWhiteSpace(txtCccd.Text))
            {
                MessageBox.Show("Bạn chưa nhập CCCD khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            if (string.IsNullOrWhiteSpace(gioitinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSdt.Text))
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 
            if (string.IsNullOrWhiteSpace(quoctich))
            {
                MessageBox.Show("Bạn chưa nhập quốc tịch khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra CCCD và Số điện thoại có phải là số hay không
            if (!CheckCCCD(out string cccd))
                return;
            if (!CheckSDT(out string sdt))
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (thaotac == 1) // Thêm
                {
                    // Kiểm tra trùng mã Khách hàng
                    string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaKH", makh);
                        int count_MaKH = (int)checkCmd.ExecuteScalar();
                        if (count_MaKH > 0)
                        {
                            MessageBox.Show($"Mã khách hàng {makh} đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        int count_SDT = (int)checkCmd.ExecuteScalar();
                        if (count_SDT > 0)
                        {
                            MessageBox.Show($"Số điện thoại {sdt} đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    string checkSDT = "SELECT COUNT(*) FROM KhachHang WHERE SoDienThoai = @SDT";
                    using (SqlCommand checkCmd2 = new SqlCommand(checkSDT, conn))
                    {
                        checkCmd2.Parameters.AddWithValue("@SDT", sdt);

                        int count_SDT = (int)checkCmd2.ExecuteScalar();
                        if (count_SDT > 0)
                        {
                            MessageBox.Show($"Số điện thoại {sdt} đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    string checkCCCD = "SELECT COUNT(*) FROM KhachHang WHERE CCCD = @CCCD";
                    using (SqlCommand checkCmd3 = new SqlCommand(checkCCCD, conn))
                    {
                        checkCmd3.Parameters.AddWithValue("@CCCD", cccd);

                        int count_CCCD = (int)checkCmd3.ExecuteScalar();
                        if (count_CCCD > 0)
                        {
                            MessageBox.Show($"CCCD {cccd} đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO KhachHang (MaKH, TenKH, CCCD, GioiTinh, SoDienThoai, QuocTich) "
                                       + "VALUES (@MaKH, @TenKH, @CCCD, @GioiTinh, @SoDienThoai, @QuocTich)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", makhachhang);
                        cmd.Parameters.AddWithValue("@TenKH", tenkh);
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);
                        cmd.Parameters.AddWithValue("@SoDienThoai", sdt);
                        cmd.Parameters.AddWithValue("@QuocTich", quoctich);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Thêm khách hàng {makh} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else if (thaotac == 3) // Sửa
                {
                    string updateQuery = "UPDATE KhachHang SET TenKH = @TenKH, CCCD = @CCCD, GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, QuocTich = @QuocTich WHERE MaKH =  @MaKH";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", makh);
                        cmd.Parameters.AddWithValue("@TenKH", tenkh);
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);
                        cmd.Parameters.AddWithValue("@SoDienThoai", sdt);
                        cmd.Parameters.AddWithValue("@QuocTich", quoctich);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show($"Cập nhật thông tin Khách hàng {makh} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            RefreshDataGridView();
            ClearInput();
            dgvKhach_Hang.ClearSelection();
            thaotac = 0;
            txtMaKH.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtCccd.ReadOnly = true;
            txtSdt.ReadOnly = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        //---------------------- Huỷ ---------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            thaotac = 0;
            dgvKhach_Hang.ClearSelection();
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        //---------------------- Thoát ---------------------
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void LoadKhachHangToComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MaKH, TenKH FROM KhachHang";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboTimkiem.DataSource = dt;
                    cboTimkiem.DisplayMember = "TenKH";   // Hiện tên khách hàng
                    cboTimkiem.ValueMember = "MaKH";      // Giá trị là mã khách hàng
                    cboTimkiem.SelectedIndex = -1;        // Không chọn ai ban đầu
                }
            }
        }
       
        private void LoadKhachHangComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MaKH, TenKH FROM KhachHang";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboTimkiem.DataSource = dt;
                    cboTimkiem.DisplayMember = "TenKH";   // Hiển thị tên khách
                    cboTimkiem.ValueMember = "MaKH";      // Giá trị là mã khách
                    cboTimkiem.SelectedIndex = -1;        // Không chọn ai lúc đầu
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
         
            if (cboTimkiem.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần hiển thị!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maKH = cboTimkiem.SelectedValue.ToString();
            LoadThongTinKhachHang(maKH);
        }
        private void LoadThongTinKhachHang(string maKH)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT MaKH, TenKH, CCCD, GioiTinh, SoDienThoai, QuocTich 
                         FROM KhachHang 
                         WHERE MaKH = @maKH";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@maKH", maKH);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKhach_Hang.DataSource = dt;
                    dgvKhach_Hang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

    }
}
