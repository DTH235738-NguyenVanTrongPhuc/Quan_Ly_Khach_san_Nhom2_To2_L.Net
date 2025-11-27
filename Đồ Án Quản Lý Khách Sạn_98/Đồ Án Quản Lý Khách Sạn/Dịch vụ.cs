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
    public partial class frmDich_Vu : Form
    {
        int thaotac = 0;
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";

        public frmDich_Vu()
        {
            InitializeComponent();
        }

        private void frmDich_Vu_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
            RefreshTenDV();
            txtMaDV.ReadOnly = true;
            txtGiaDV.ReadOnly = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        public void RefreshDataGridView()
        {
            string query = "SELECT * FROM DichVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDich_Vu.DataSource = dt;
                dgvDich_Vu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void RefreshTenDV()
        {
            cboTenDV.Items.Clear(); // Xóa item cũ
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM DichVu";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cboTenDV.Items.Add(dr["TenDV"].ToString());
                    }
                }
            }
        }
        public void ClearInput()
        {
            txtMaDV.Clear();
            cboTenDV.Text = "";
            txtGiaDV.Clear();
        }

        // Kiểm tra giá dịch vụ
        private bool TryGetGiaTien(out int giaTien)
        {
            if (!int.TryParse(txtGiaDV.Text.Trim(), out giaTien))
            {
                MessageBox.Show("Giá tiền phải là số nguyên (không chứa ký tự khác) !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Kiểm tra dịch vụ đã được sử dụng chưa: Đã sử dụng - True / Chưa sử dụng - False
        public bool DichVu_DaSudung(string madv)
        {
            string checkDV_DaSuDung = "SELECT COUNT(*) FROM ChiTietDichVu WHERE MaDV = @MaDV";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand checkCmd = new SqlCommand(checkDV_DaSuDung, conn))
            {
                conn.Open();
                checkCmd.Parameters.AddWithValue("@MaDV", madv);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        //====================== Xử lý sự kiện ======================

        //---------------------- Thêm dịch vụ ---------------------
        private void btnThem_Click(object sender, EventArgs e)
        {
            thaotac = 1;
            ClearInput();
            txtMaDV.Focus();
            txtMaDV.ReadOnly = false; // Tắt chế độ đọc --> Cho phép nhập
            txtGiaDV.ReadOnly = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
        }

        //---------------------- Xoá dịch vụ ---------------------
        private void btnXoa_Click(object sender, EventArgs e)
        {
            thaotac = 2;

            if (dgvDich_Vu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string madv = dgvDich_Vu.CurrentRow.Cells["MaDV"].Value.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa dịch {madv}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (DichVu_DaSudung(madv))
                {
                    MessageBox.Show($"Dịch vụ {madv} đang được sử dụng, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string query = "DELETE FROM DichVu WHERE MaDV = @MaDV";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDV", madv);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                RefreshDataGridView();
                RefreshTenDV();
                MessageBox.Show($"Xóa dịch vụ {madv} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                thaotac = 0;
            }
        }

        //---------------------- Sửa dịch vụ ---------------------
        private void btnSua_Click(object sender, EventArgs e)
        {            
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            thaotac = 3;    
            if (dgvDich_Vu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtMaDV.Text = dgvDich_Vu.CurrentRow.Cells["MaDV"].Value.ToString();
            cboTenDV.Text = dgvDich_Vu.CurrentRow.Cells["TenDV"].Value.ToString();
            txtGiaDV.Text = dgvDich_Vu.CurrentRow.Cells["GiaDV"].Value.ToString();

            txtMaDV.ReadOnly = true; // Mở chế độ đọc --> Ko cho chỉnh sửa
        }

        //---------------------- Lưu dịch vụ ---------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (thaotac == 0)
            {
                MessageBox.Show("Vui lòng chọn thao tác (Thêm/Xoá/Sửa) !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string madv = txtMaDV.Text.Trim();
            string tendv = cboTenDV.Text.Trim();

            // Kiểm tra đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrWhiteSpace(madv))
            {
                MessageBox.Show("Bạn chưa nhập mã dịch vụ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tendv))
            {
                MessageBox.Show("Bạn chưa nhập tên dịch vụ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            if (string.IsNullOrWhiteSpace(txtGiaDV.Text))
            {
                MessageBox.Show("Bạn chưa nhập giá dịch vụ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            if (!TryGetGiaTien(out int giaTien))
                return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (thaotac == 1) // Thêm
                {
                    // Kiểm tra trùng mã phòng
                    string checkMaDV = "SELECT COUNT(*) FROM DichVu WHERE MaDV = @MaDV";
                    using (SqlCommand checkCmd = new SqlCommand(checkMaDV, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaDV", madv);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Mã dịch vụ đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    string checkTenDV = "SELECT COUNT(*) FROM DichVu WHERE TenDV = @TenDV";
                    using (SqlCommand checkCmd = new SqlCommand(checkTenDV, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenDV", tendv);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Tên dịch vụ đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO DichVu (MaDV, TenDV, GiaDV) VALUES (@MaDV, @TenDV, @GiaDV)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDV", madv);
                        cmd.Parameters.AddWithValue("@TenDV", tendv);
                        cmd.Parameters.AddWithValue("@GiaDV", giaTien);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Thêm dịch vụ {madv} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (thaotac == 3) // Sửa
                {
                    string updateQuery = "UPDATE DichVu SET TenDV = @TenDv, GiaDV = @GiaDV WHERE MaDV = @MaDV";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDV", madv);
                        cmd.Parameters.AddWithValue("@TenDV", tendv);
                        cmd.Parameters.AddWithValue("@GiaDV", giaTien);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Cập nhật thông tin dịch vụ {madv} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            RefreshDataGridView();
            RefreshTenDV();
            ClearInput();
            dgvDich_Vu.ClearSelection();
            thaotac = 0;
            txtMaDV.ReadOnly = true;
            txtGiaDV.ReadOnly = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        //---------------------- Huỷ ---------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            thaotac = 0;
            dgvDich_Vu.ClearSelection();
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        //---------------------- Thoát ---------------------
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
