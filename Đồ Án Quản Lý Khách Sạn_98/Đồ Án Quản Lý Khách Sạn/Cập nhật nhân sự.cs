using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    public partial class frmCap_Nhat_Nhan_Su : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        int thaotac = 0;
        public frmCap_Nhat_Nhan_Su()
        {
            InitializeComponent();
        }

        private void frmCap_Nhat_Nhan_Su_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
            
        }

        public void RefreshDataGridView()
        {
            string query = @"SELECT dn.Username, nv.TenNV, dn.ChucVu
                             FROM DangNhap dn
                             JOIN NhanVien nv ON dn.MaNV = nv.MaNV";
    
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDang_Nhap.DataSource = dt;
                dgvDang_Nhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        public void ClearInput()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtMaNV.Text = "";
            cboChucvu.Text = "";
        }

        // --------------------------- Thêm ---------------------------
        private void btnThem_Click(object sender, EventArgs e)
        {
            thaotac = 1;
            txtUsername.Focus();
            ClearInput();
        }

        // --------------------------- Xoá ---------------------------
        private void btnXoa_Click(object sender, EventArgs e)
        {
            thaotac = 2;

            if (dgvDang_Nhap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn Username để xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string user = dgvDang_Nhap.CurrentRow.Cells["Username"].Value.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa dịch {user}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM DangNhap WHERE Username = @Username";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                RefreshDataGridView();
                MessageBox.Show($"Xóa Username {user} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                thaotac = 0;
            }
        }

        // --------------------------- Sửa ---------------------------
        private void btnSua_Click(object sender, EventArgs e)
        {
            thaotac = 3;
            if (dgvDang_Nhap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn Username cần sửa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedUsername = dgvDang_Nhap.CurrentRow.Cells["Username"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT dn.Username, dn.Password, dn.MaNV, nv.TenNV, dn.ChucVu
                         FROM DangNhap dn
                         JOIN NhanVien nv ON dn.MaNV = nv.MaNV
                         WHERE dn.Username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", selectedUsername);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtUsername.Text = reader["Username"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                            txtMaNV.Text = reader["MaNV"].ToString();
                            txtTenNV.Text = reader["TenNV"].ToString();
                            cboChucvu.Text = reader["ChucVu"].ToString();
                        }
                    }
                }
            }
        }

        // --------------------------- Lưu ---------------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (thaotac == 0)
            {
                MessageBox.Show("Vui lòng chọn thao tác (Thêm/Xoá/Sửa) !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string user = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string manv = txtMaNV.Text.Trim();
            string tennv = txtTenNV.Text.Trim();
            string chucvu = cboChucvu.Text.Trim();

            // Kiểm tra đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrWhiteSpace(user))
            {
                MessageBox.Show("Bạn chưa nhập Username !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Bạn chưa nhập Password !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(manv))
            {
                MessageBox.Show("Bạn chưa nhập Mã số nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tennv))
            {
                MessageBox.Show("Bạn chưa nhập Tên nhân viên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(chucvu))
            {
                MessageBox.Show("Bạn chưa chọn Chức vụ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Kiểm tra MaNV có tồn tại trong NhanVien không
                string checkMaNVExist = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
                using (SqlCommand cmdCheck = new SqlCommand(checkMaNVExist, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MaNV", manv);
                    int existCount = (int)cmdCheck.ExecuteScalar();
                    if (existCount == 0)
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại trong bảng NhanVien!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (thaotac == 1) // Thêm
                {
                    // Kiểm tra trùng mã phòng
                    string checkUsername = "SELECT COUNT(*) FROM DangNhap WHERE Username = @Username";
                    using (SqlCommand checkCmd = new SqlCommand(checkUsername, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", user);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Username đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                   

                    string insertQuery = "INSERT INTO DangNhap (Username, Password, MaNV, ChucVu) VALUES (@Username, @Password, @MaNV, @ChucVu)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@MaNV", manv);
                        cmd.Parameters.AddWithValue("@ChucVu", chucvu);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show($"Thêm Username {user} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (thaotac == 3) // Sửa
                {
                    string updateQuery = "UPDATE DangNhap SET Username = @Username, Password = @Password, ChucVu = @ChucVu"+
                     " WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@ChucVu", chucvu);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Cập nhật Username {user} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            RefreshDataGridView();
            ClearInput();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
