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

namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    public partial class frmTra_Phong : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        public frmTra_Phong()
        {
            InitializeComponent();
        }

        private void frmTra_Phong_Load(object sender, EventArgs e)
        {
            RefreshDataGridView_TraPhong();
            RefreshDataGridView_Phong();
            btnTra.Enabled = false;
            btnHuy.Enabled = false;
        }

        public void RefreshDataGridView_TraPhong()
        {
            string query = "SELECT * FROM DatPhong";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTra_Phong.DataSource = dt;
                dgvTra_Phong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        //----------------------------------- Reset bảng phòng ----------------------------------
        public void RefreshDataGridView_Phong()
        { 
            string query = "SELECT * FROM Phong "
                        + "WHERE TrangThai = N'TRỐNG'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPhong.DataSource = dt;
                dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        public void ClearInput()
        {
            txtMaDP.Text = "";
            txtMaphong.Text = "";
            txtMaKH.Text = "";
            txtTenKH.Text = "";
        }

        //------------------------------ Nhập thông tin trả phòng ------------------------------
        private void btnNhapthongtin_Click(object sender, EventArgs e)
        {
            ClearInput();
            txtMaDP.Focus();
            
            btnHuy.Enabled = true;
            btnTra.Enabled = true;
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            btnNhapthongtin.Enabled = false;

            // Lấy thông tin Mã phòng từ bảng Phòng để hiển thị lên TextBox
            if (dgvTra_Phong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phòng muốn trả !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnTra.Enabled = false;
                btnHuy.Enabled = false;
                return;
            }
            // Lấy thông tin Mã phòng từ bảng Phòng để hiển thị lên TextBox
            txtMaDP.Text = dgvTra_Phong.CurrentRow.Cells["MaDatPhong"].Value.ToString();
            txtMaphong.Text = dgvTra_Phong.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtMaKH.Text = dgvTra_Phong.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dgvTra_Phong.CurrentRow.Cells["TenKH"].Value.ToString();
            dtpNgaydat.Value = (DateTime)dgvTra_Phong.CurrentRow.Cells["NgayDat"].Value;
            dtpNgaynhan.Value = (DateTime)dgvTra_Phong.CurrentRow.Cells["NgayNhan"].Value;
            dtpNgaytra.Value = (DateTime)dgvTra_Phong.CurrentRow.Cells["NgayTra"].Value;

            string madatphong = txtMaDP.Text.Trim();
            string maphong = txtMaphong.Text.Trim();
            string makhachhang = txtMaKH.Text.Trim();
            string tenkhachhang = txtTenKH.Text.Trim();
            DateTime ngaydat = dtpNgaydat.Value;
            DateTime ngaynhan = dtpNgaynhan.Value;
            DateTime ngaytra = dtpNgaytra.Value;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Kiểm tra đã nhập đầy đủ thông tin chưa
                if (string.IsNullOrWhiteSpace(madatphong))
                {
                    MessageBox.Show("Bạn chưa chọn mã đặt phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(maphong))
                {
                    MessageBox.Show("Bạn chưa chọn mã phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(makhachhang))
                {
                    MessageBox.Show("Bạn chưa nhập mã khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tenkhachhang))
                {
                    MessageBox.Show("Bạn chưa nhập tên khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show($"Xác nhận trả phòng {maphong} !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int madp = int.Parse(txtMaDP.Text);
                    frmThanh_Toan frmthanhtoan = new frmThanh_Toan(madp);
                    frmthanhtoan.ShowDialog();

                    // Cập nhật lại DataGidView
                    RefreshDataGridView_TraPhong();
                    RefreshDataGridView_Phong();
                    ClearInput();
                    btnTra.Enabled = false;
                    btnNhapthongtin.Enabled = true;
                }
            }
        }

        //----------------------------------- Huỷ thông tin đặt phòng -----------------------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            btnTra.Enabled = false;
            btnHuy.Enabled = false;
        }
        //----------------------------------- Thoát -----------------------------------
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
