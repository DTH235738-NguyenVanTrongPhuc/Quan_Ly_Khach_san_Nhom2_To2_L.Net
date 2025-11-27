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
    public partial class frmNhan_Su : Form
    {
        int thaotac = 0; // Biến để xác định thao tác 1-Thêm, 2-Xoá, 3-Sửa
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";

        public frmNhan_Su()
        {
            InitializeComponent();
            cboChucvu.DropDownStyle = ComboBoxStyle.DropDownList;
        }

//====================== Kết nối dữ liệu vào DataGridView ======================
        private void frmNhan_Su_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
            txtManv.ReadOnly = true;
            txtHoten.ReadOnly = true;
            txtSdt.ReadOnly = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        public void RefreshDataGridView()
        {
            string query = "SELECT * FROM NhanVien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNhan_Vien.DataSource = dt;
                dgvNhan_Vien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        public void ClearInput()
        {
            txtManv.Text = "";
            txtHoten.Text = "";
            rdbNam.Checked = false;
            rdbNu.Checked = false;
            dtpNgaysinh.Value = DateTime.Now;
            txtSdt.Text = "";
            cboChucvu.SelectedIndex = -1;
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
        // Lấy giá trị giới tính
        public string GioiTinh()
        {
            if (rdbNam.Checked)
                return "Nam";
            else if (rdbNu.Checked)
                return "Nữ";
            else
                return ""; // Trường hợp chưa chọn
        }

//=========================== Xử lý sự kiện ===========================
//---------------------- Thêm nhân viên ---------------------
        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearInput();
            txtManv.ReadOnly = false; // Tắt chế độ chỉ đọc cho Mã phòng --> Cho phép nhập
            txtManv.ReadOnly = false;
            txtHoten.ReadOnly = false;
            txtSdt.ReadOnly = false;
            txtManv.Focus();
            thaotac = 1;
            txtManv.ReadOnly = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
        }

        //---------------------- Xoá nhân viên ---------------------
        private void btnXoa_Click(object sender, EventArgs e)
        {
            thaotac = 2;
            if (dgvNhan_Vien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string manv = dgvNhan_Vien.CurrentRow.Cells["Manv"].Value.ToString();

            DialogResult result = MessageBox.Show($"Bạn có muốn xoá nhân viên {manv}?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM NhanVien WHERE Manv = @manv";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@manv", manv);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                RefreshDataGridView();
                MessageBox.Show($"Xoá nhân viên {manv} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                thaotac = 0;
            }
        }

        //---------------------- Sửa nhân viên ---------------------
        private void btnSua_Click(object sender, EventArgs e)
        {            
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            thaotac = 3;
            if (dgvNhan_Vien.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtManv.Text = dgvNhan_Vien.CurrentRow.Cells["Manv"].Value.ToString();
            txtHoten.Text = dgvNhan_Vien.CurrentRow.Cells["TenNV"].Value.ToString();
            dtpNgaysinh.Value = Convert.ToDateTime(dgvNhan_Vien.CurrentRow.Cells["Ngaysinh"].Value);
            cboChucvu.Text = dgvNhan_Vien.CurrentRow.Cells["Chucvu"].Value.ToString();
            txtSdt.Text = dgvNhan_Vien.CurrentRow.Cells["SoDienThoai"].Value.ToString();
            string gioitinh = dgvNhan_Vien.CurrentRow.Cells["GioiTinh"].Value.ToString();
            if (gioitinh == "Nam")
                rdbNam.Checked = true;
            else if (gioitinh == "Nữ")
                rdbNu.Checked = true;
            else
            {
                rdbNam.Checked = false;
                rdbNu.Checked = false;
            }
            txtManv.ReadOnly = true; // Mở chế độ chỉ đọc --> Ko cho nhập

        }
        //---------------------- Lưu nhân viên ---------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (thaotac == 0)
            {
                MessageBox.Show("Vui lòng chọn thao tác (Thêm/Xoá/Sửa) !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string manv = txtManv.Text.Trim();
            string tennv = txtHoten.Text.Trim();
            string chucvu = cboChucvu.Text.Trim();
            string gioitinh = GioiTinh();

            //Kiểm tra đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrWhiteSpace(manv))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tennv))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            if (string.IsNullOrWhiteSpace(chucvu))
            {
                MessageBox.Show("Bạn chưa chọn chức vụ nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(gioitinh))
            {
                MessageBox.Show("Bạn chưa chọn giới tính nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSdt.Text))
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Check Số điện thoại có phải là số hay không
            if (!CheckSDT(out string Sdt))
                return;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (thaotac == 1) // Thêm
                {
                    // Kiểm tra trùng mã nhân viên
                    string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNV", manv);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show($"Mã nhân viên {manv} đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO NhanVien (MaNV, TenNV, NgaySinh, GioiTinh, SoDienThoai, ChucVu) " 
                                       + "VALUES (@MaNV, @TenNV, @NgaySinh, @GioiTinh,  @SoDienThoai, @ChucVu)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", manv);
                        cmd.Parameters.AddWithValue("@TenNV", tennv);
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaysinh.Value);
                        cmd.Parameters.AddWithValue("@ChucVu", chucvu);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);
                        cmd.Parameters.AddWithValue("@SoDienThoai", Sdt);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Thêm nhân viên {manv} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else if (thaotac == 3) // Sửa
                {
                    string updateQuery = "UPDATE NhanVien SET TenNV = @TenNV, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, ChucVu = @ChucVu WHERE MaNV =  @MaNV";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", manv);
                        cmd.Parameters.AddWithValue("@TenNV", tennv);
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaysinh.Value);
                        cmd.Parameters.AddWithValue("@ChucVu", chucvu);
                        cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);
                        cmd.Parameters.AddWithValue("@SoDienThoai", Sdt);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Cập nhật thông tin nhân viên {manv} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            RefreshDataGridView();
            ClearInput();
            dgvNhan_Vien.ClearSelection(); // Xoá lựa chọn trên bảng
            thaotac = 0;
            txtManv.ReadOnly = true;
            txtHoten.ReadOnly = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        //---------------------- Huỷ ---------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            thaotac = 0;
            dgvNhan_Vien.ClearSelection(); // Xoá lựa chọn trên bảng
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
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
    }
}


