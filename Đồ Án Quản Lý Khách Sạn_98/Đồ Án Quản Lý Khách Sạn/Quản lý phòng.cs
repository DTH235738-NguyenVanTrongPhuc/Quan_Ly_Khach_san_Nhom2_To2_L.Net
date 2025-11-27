using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Đồ_Án_Quản_Lý_Khách_Sạn.frmPhong;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    public partial class frmPhong : Form
    {

        int thaotac = 0; // Biến để xác định thao tác 1-Thêm, 2-Xoá, 3-Sửa
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";

        public frmPhong()
        {
            InitializeComponent();
            cboLoaiphong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangthai.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        //====================== Kết nối dữ liệu vào DataGridView ======================
        private void frmPhong_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();

            txtMaphong.ReadOnly = true;
            txtGiatien.ReadOnly = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }
        // Phương thức để làm mới DataGridView
        public void RefreshDataGridView()
        {
            string query = "SELECT * FROM Phong";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPhong.DataSource = dt;
                dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        // Xoá dữ liệu trong các TextBox và ComboBox sau khi thêm/sửa/xoá
        public void ClearInput()
        {
            txtMaphong.Clear();
            cboLoaiphong.SelectedIndex = -1;
            cboTrangthai.SelectedIndex = -1;
            txtGiatien.Clear();
        }
        private bool CheckGiaTien(out int giatien)
        {
            if (!int.TryParse(txtGiatien.Text.Trim(), out giatien))
            {
                MessageBox.Show("Giá tiền phải là số nguyên (không chứa ký tự khác)!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Giới hạn hợp lệ 
            if (giatien < 100000 || giatien > 10000000)
            {
                MessageBox.Show("Giá tiền phải nằm trong khoảng từ 100,000đ đến 10,000,000đ!",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Kiểm tra phòng đã được sử dụng hay chưa: Rồi - True / Chưa - False
        // Dùng để tránh lỗi về khoá ngoại nếu trong bảng đặt phòng có phòng muốn xoá
        public bool Phong_DaSudung(string maphong)
        {
            string checkPhong_DaSuDung = "SELECT COUNT(*) FROM DatPhong WHERE MaPhong = @MaPhong";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand checkCmd = new SqlCommand(checkPhong_DaSuDung, conn))
            {
                conn.Open();
                checkCmd.Parameters.AddWithValue("@MaPhong", maphong);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        //====================== Xử lý sự kiện ======================

        //---------------------- Thêm phòng ---------------------
        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearInput();
            txtMaphong.ReadOnly = false;  // Tắt chế độ chỉ đọc cho Mã phòng --> Cho phép nhập
            txtMaphong.ReadOnly = false;
            txtGiatien.ReadOnly = false;
            txtMaphong.Focus();
            thaotac = 1;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
        }

        //---------------------- Xoá phòng ---------------------
        private void btnXoa_Click(object sender, EventArgs e)
        {
            thaotac = 2;

            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maphong = dgvPhong.CurrentRow.Cells["MaPhong"].Value.ToString();

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa phòng {maphong}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (Phong_DaSudung(maphong))
                {
                    MessageBox.Show($"Dịch vụ {maphong} đang được sử dụng, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string query = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maphong);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                RefreshDataGridView();
                MessageBox.Show($"Xóa phòng {maphong} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                thaotac = 0;
            }
        }

        //---------------------- Sửa phòng ---------------------
        private void btnSua_Click(object sender, EventArgs e)
        {
            thaotac = 3;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;

            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtMaphong.Text = dgvPhong.CurrentRow.Cells["MaPhong"].Value.ToString();
            cboLoaiphong.Text = dgvPhong.CurrentRow.Cells["LoaiPhong"].Value.ToString();
            cboTrangthai.Text = dgvPhong.CurrentRow.Cells["TrangThai"].Value.ToString();
            txtGiatien.Text = dgvPhong.CurrentRow.Cells["GiaTien"].Value.ToString();

            // Đặt chế độ chỉ đọc cho Mã phòng --> Ko đc phép chỉnh sửa
            txtMaphong.ReadOnly = true;
        }

        //---------------------- Lưu phòng ---------------------
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maphong = txtMaphong.Text.Trim();
            string loaiphong = cboLoaiphong.Text.Trim();
            string trangthai = cboTrangthai.Text.Trim();

            if (thaotac == 0)
            {
                MessageBox.Show("Vui lòng chọn thao tác (Thêm/Xoá/Sửa) !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Check xem có nhập đầy đủ thông tin chưa
            if (string.IsNullOrWhiteSpace(maphong))
            {
                MessageBox.Show("Bạn chưa nhập mã phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(loaiphong))
            {
                MessageBox.Show("Bạn chưa chọn loại phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(trangthai))
            {
                MessageBox.Show("Bạn chưa chọn trạng thái phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    

            if (string.IsNullOrWhiteSpace(txtGiatien.Text))
            {
                MessageBox.Show("Bạn chưa nhập giá phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            
            // Kiểm tra giá tiền hợp lệ không
            if (!CheckGiaTien(out int giaTien))
                return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (thaotac == 1) // Thêm
                {
                    // Kiểm tra trùng mã phòng
                    string checkQuery = "SELECT COUNT(*) FROM Phong WHERE MaPhong = @MaPhong";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaPhong", maphong);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show($"Mã phòng {maphong} đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Phong (MaPhong, LoaiPhong, TrangThai, GiaTien) VALUES (@MaPhong, @LoaiPhong, @TrangThai, @GiaTien)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maphong);
                        cmd.Parameters.AddWithValue("@LoaiPhong", loaiphong);
                        cmd.Parameters.AddWithValue("@TrangThai", trangthai);
                        cmd.Parameters.AddWithValue("@GiaTien", giaTien);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Thêm phòng {maphong} thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (thaotac == 3) // Sửa
                {
                    string updateQuery = "UPDATE Phong SET LoaiPhong = @LoaiPhong, TrangThai = @TrangThai, GiaTien = @GiaTien WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maphong);
                        cmd.Parameters.AddWithValue("@LoaiPhong", loaiphong);
                        cmd.Parameters.AddWithValue("@TrangThai", trangthai);
                        cmd.Parameters.AddWithValue("@GiaTien", giaTien);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Cập nhật thông tin phòng {maphong} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            RefreshDataGridView();
            ClearInput();
            dgvPhong.ClearSelection(); // Xoá lựa chọn trên bảng
            thaotac = 0;
            txtMaphong.ReadOnly = true;
            txtGiatien.ReadOnly = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }
        //---------------------- Huỷ ---------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            thaotac = 0;
            dgvPhong.ClearSelection(); // Xoá lựa chọn trên bảng
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