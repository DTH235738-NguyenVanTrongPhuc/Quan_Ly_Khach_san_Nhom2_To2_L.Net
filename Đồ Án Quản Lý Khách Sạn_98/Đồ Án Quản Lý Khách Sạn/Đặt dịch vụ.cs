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
    public partial class frmDat_Dich_Vu : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        string madp;
        public frmDat_Dich_Vu(string madp)
        {
            InitializeComponent();
            this.madp = madp;
        }

        private void frmDat_Dich_Vu_Load(object sender, EventArgs e)
        {
            RefreshDaTaGridView_DatDichVu();
            RefreshDataGridView_DichVu();
            txtMadp.Text = madp.ToString();

            btnDat.Enabled = false;
            btnHuy.Enabled = false;
        }

        // ------------------------------ Reset bảng Chi Tiết Dịch Vụ ------------------------------
        public void RefreshDaTaGridView_DatDichVu()
        {
            string query = "SELECT * FROM ChiTietDichVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDat_Dich_Vu.DataSource = dt;
                dgvDat_Dich_Vu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        // ------------------------------ Reset bảng Chi Tiết Dịch Vụ ------------------------------
        public void RefreshDataGridView_DichVu()
        {
            string query = "SELECT * FROM DichVu ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDich_Vu.DataSource = dt;
                dgvDich_Vu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        // Kiểm tra số lượng
        public bool CheckSoLuong (out int soluong)
        {
            if (!int.TryParse(txtSoluong.Text.Trim(), out soluong))
            {
                MessageBox.Show("Số lượng phải là số nguyên (không chứa ký tự khác)!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void ClearInput()
        {
            txtMadatdv.Text = "";
            txtMadv.Text = "";
            txtSoluong.Text = "";
        }
        //================================= Xử lý Sự kiện =================================
        //---------------------------------Nhập thông tin ---------------------------------
        private void btnNhapthongtin_Click(object sender, EventArgs e)
        {
            ClearInput();
            txtMadv.Focus();
            txtMadv.Text = dgvDich_Vu.CurrentRow.Cells["MaDV"].Value.ToString();
            if (dgvDich_Vu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnDat.Enabled = true;
            btnHuy.Enabled = true;
        }
        //--------------------------------- Đặt dịch vụ --------------------------------
        private void btnDat_Click(object sender, EventArgs e)
        {
            btnNhapthongtin.Enabled = false;
            // Lấy thông tin Mã dịch vụ từ bảng dịch vụ đưa lên TextBox
            if (dgvDich_Vu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ muốn đặt !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            txtMadv.Text = dgvDich_Vu.CurrentRow.Cells["Madv"].Value.ToString();
            string madatdv = txtMadatdv.Text;
            string madv = txtMadv.Text;
            DateTime ngaydat = dtpNgaydat.Value;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Kiểm tra đã nhập đầy đủ thông tin chưa
                if (string.IsNullOrWhiteSpace(madv))
                {
                    MessageBox.Show("Bạn chưa chọn dịch vụ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSoluong.Text))
                {
                    MessageBox.Show("Bạn chưa nhập số lượng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm ra số lượng có phải số không
                if (!CheckSoLuong(out int soluong))
                    return;
                DialogResult result = MessageBox.Show($"Xác nhận đặt dịch vụ {madv} ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    conn.Open();
                    {
                        string query = "INSERT INTO ChiTietDichVu (MaDatPhong, MaDV, SoLuong) "
                                     + "VALUES (@MaDP, @MaDV, @SoLuong)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaDP", madp);
                            cmd.Parameters.AddWithValue("@MaDV", madv);
                            cmd.Parameters.AddWithValue("@SoLuong", soluong);
                            cmd.ExecuteNonQuery();
                        }    
                    }
                }
            }
            // Cập nhật DataGridView
            RefreshDaTaGridView_DatDichVu();
            MessageBox.Show($"Bạn đã đặt dịch vụ {madv} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDat.Enabled = false;
            btnHuy.Enabled = false;
            btnNhapthongtin.Enabled = true;

        }
        //--------------------------------- Huỷ thông tin dịch vụ --------------------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMadv.Text = "";
            txtSoluong.Text = "";
            btnDat.Enabled = false;
        }
        //--------------------------------- Thoát --------------------------------
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
