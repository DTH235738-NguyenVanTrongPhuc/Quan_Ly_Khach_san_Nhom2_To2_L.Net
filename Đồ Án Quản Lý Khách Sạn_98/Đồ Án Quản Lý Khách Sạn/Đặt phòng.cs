using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    public partial class frmDat_Phong : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        public frmDat_Phong()
        {
            InitializeComponent();
        }

        private void frmDat_Phong_Load(object sender, EventArgs e)
        {
            RefreshDataGridView_DatPhong();
            RefreshDataGridView_Phong();
            txtMaKH.Focus();
            // Vô hiệu ô Mã đặt phòng Vì nó tự tạo
            txtMaDP.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtCccd.ReadOnly = true;
            txtQuoctich.ReadOnly = true;
            txtSdt.ReadOnly = true;

            btnDat.Enabled = false;
            btnNhan.Enabled = false;
            btnHuy.Enabled = false;
        }
        // ----------------------------------- Reset bảng Đặt phòng -----------------------------------
        public void RefreshDataGridView_DatPhong()
        {
            string query = "SELECT * FROM DatPhong";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDat_Phong.DataSource = dt;
                dgvDat_Phong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
        //----------------------------------- Reset bảng khách hàng ----------------------------------

        public void ClearInput()
        {
            txtMaDP.Text = "";
            txtMaphong.Text = "";
            //txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtCccd.Text = "";
            rdbNam.Checked = false;
            rdbNu.Checked = false;
            txtSdt.Text = "";
            txtQuoctich.Text = "";
            dtpNgaydat.Value = DateTime.Now;
            dtpNgaynhan.Value = DateTime.Now;
            dtpNgaytra.Value = DateTime.Now;
        }

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

        // Kiểm tra tính hợp lệ của Ngày đặt - nhận - trả phòng
        public bool CheckNgayDatNhanTra()
        {
            DateTime ngayDat = dtpNgaydat.Value;
            DateTime ngayNhan = dtpNgaynhan.Value;
            DateTime ngayTra = dtpNgaytra.Value;

            // Kiểm tra ngày nhận có trước ngày đặt không
            if (dtpNgaydat.Value == dtpNgaytra.Value || dtpNgaynhan.Value == dtpNgaytra.Value)
            {
                MessageBox.Show("Mời chọn ngày đặt/ nhận/ trả phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (ngayNhan < ngayDat)
            {
                MessageBox.Show("Ngày nhận phòng không được trước ngày đặt !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra ngày trả có trước ngày nhận không
            if (ngayTra < ngayNhan)
            {
                MessageBox.Show("Ngày trả phòng không được trước ngày nhận !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        //------------------------------ Nhập thông tin đặt phòng ------------------------------
        private void btnNhapthongtin_Click(object sender, EventArgs e)
        {
            ClearInput();
            txtMaKH.Focus();
            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Lấy thông tin từ phòng để đưa lên TextBox
            txtMaphong.Text = dgvPhong.CurrentRow.Cells["MaPhong"].Value.ToString();
            
            //Chuyển qua form Khách hàng để nhập thông tin
            if (txtMaphong.Text != "")
            {
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Mời nhập Mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string makh = txtMaKH.Text;
                    frmKhach_Hang frmkhachhang = new frmKhach_Hang(makh);
                    frmkhachhang.ShowDialog();
                }
            }
            btnDat.Enabled = true;
            txtTenKH.ReadOnly = false;
            txtCccd.ReadOnly = false;
            txtQuoctich.ReadOnly = false;
            txtSdt.ReadOnly = false;
        }

        //================================= Xử lý Sự kiện =================================
        //------------------------------ Đặt phòng ------------------------------
        private void btnDat_Click(object sender, EventArgs e)
        {
            btnNhapthongtin.Enabled = true;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT * 
                                 FROM KhachHang kh
                                 WHERE kh.MaKH = @MaKH";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtTenKH.Text = reader["TenKH"].ToString();
                        txtCccd.Text = reader["CCCD"].ToString();
                        string gt = reader["GioiTinh"].ToString();
                        if (gt == "Nam")
                            rdbNam.Checked = true;
                        else if (gt == "Nữ")
                            rdbNu.Checked = true;
                        txtSdt.Text = reader["SoDienThoai"].ToString();
                        txtQuoctich.Text = reader["QuocTich"].ToString();
                    }
                }
            }
            string madatphong = txtMaDP.Text.Trim();
            string maphong = txtMaphong.Text.Trim();
            string makhachhang = txtMaKH.Text.Trim();
            string tenkhachhang = txtTenKH.Text.Trim();
            string gioitinh = GioiTinh();
            string quoctich = txtQuoctich.Text.Trim();
            DateTime ngaydat = dtpNgaydat.Value;
            DateTime ngaynhan = dtpNgaynhan.Value;
            DateTime ngaytra = dtpNgaytra.Value;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Kiểm tra đã nhập đầy đủ thông tin chưa
                if (string.IsNullOrWhiteSpace(maphong))
                {
                    MessageBox.Show("Bạn chưa chọn phòng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (string.IsNullOrWhiteSpace(txtCccd.Text))
                {
                    MessageBox.Show("Bạn chưa nhâp căn cước công dân khách hàng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(gioitinh))
                {
                    MessageBox.Show("Bạn chưa chọn giới tính !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(quoctich))
                {
                    MessageBox.Show("Bạn chưa nhập quốc tịch !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }    
                // Kiểm tra CCCD và SĐT có là số không
                if (!CheckCCCD(out string cccd))
                    return;
                if (!CheckSDT(out string sdt))
                    return;
                if (!CheckNgayDatNhanTra())
                    return;
                DialogResult result = MessageBox.Show($"Xác nhận đặt phòng {maphong} !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    conn.Open();
                    {
                        RefreshDataGridView_DatPhong();
                        RefreshDataGridView_Phong();
                        MessageBox.Show("Cảm ơn bạn đã lựa chọn khách sạn của chúng tôi !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult dr = MessageBox.Show("Bạn muốn sử dụng dịch vụ nào không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            btnDat_Dich_Vu.Enabled = true;
                        }
                        // --------------------------- Cập nhật bảng Đặt phòng ---------------------------
                        // Thêm đặt phòng 
                        string insertQuery = "INSERT INTO DatPhong (MaPhong, MaKH, TenKH, NgayDat, NgayNhan, NgayTra) "
                                           + "VALUES (@MaPhong, @MaKH, @TenKH, @NgayDat, @NgayNhan, @NgayTra)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaPhong", maphong);
                            cmd.Parameters.AddWithValue("@MaKH", makhachhang);
                            cmd.Parameters.AddWithValue("@TenKH", tenkhachhang);
                            cmd.Parameters.AddWithValue("@NgayDat", ngaydat);
                            cmd.Parameters.AddWithValue("@NgayNhan", ngaynhan);
                            cmd.Parameters.AddWithValue("@NgayTra", ngaytra);
                            cmd.ExecuteNonQuery();
                        }

                        // --------------------------- Cập nhật bảng Phòng ---------------------------
                        string trangthai = "ĐÃ ĐẶT TRƯỚC";
                        string updateQuery = "UPDATE Phong SET TrangThai = @TrangThai "
                                       + "WHERE MaPhong = @MaPhong";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", trangthai);
                            cmd.Parameters.AddWithValue("@MaPhong", maphong);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    return;
                }    
            }

            // Load DataGridView
            RefreshDataGridView_DatPhong();
            RefreshDataGridView_Phong();

            // Vô hiệu hoá nút Huỷ phòng và Đặt
            btnHuy.Enabled = false;
            btnDat.Enabled = false;
            // Mở nút Nhận phòng, Đặt dịch vụ và Nhập thông tin
            btnNhan.Enabled = true;
        }

                //----------------------------------- Chuyển qua form Đặt dịch vụ -----------------------------------
        private void btnDat_Dich_Vu_Click(object sender, EventArgs e)
        {
            if (dgvDat_Phong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn Mã đạt phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string madp = dgvDat_Phong.CurrentRow.Cells["MaDatPhong"].Value.ToString();
            frmDat_Dich_Vu frmdatdichvu = new frmDat_Dich_Vu(madp);
            frmdatdichvu.ShowDialog();
        }

        // ----------------------------------- Nhận phòng -----------------------------------
        private void btnNhan_Click(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtMaDP.Text = dgvDat_Phong.CurrentRow.Cells["MaDatPhong"].Value.ToString();
            txtMaphong.Text = dgvDat_Phong.CurrentRow.Cells["MaPhong"].Value.ToString();

            string maphong = txtMaphong.Text;
            string makhachhang = txtMaKH.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                {
                    // --------------------------- Cập nhật bảng Phòng ---------------------------
                    string trangthai = "ĐANG THUÊ";
                    string updateQuery = "UPDATE Phong SET TrangThai = @TrangThai "
                                       + "WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", trangthai);
                        cmd.Parameters.AddWithValue("@MaPhong", maphong);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            MessageBox.Show($"Khách hàng {makhachhang} đã nhận phòng {maphong} !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNhan.Enabled = false;
            btnNhapthongtin.Enabled = true;
        }

        //----------------------------------- Huỷ thông tin đặt phòng -----------------------------------
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInput();
            dgvPhong.ClearSelection();
            // Xoá thông tin khách hàng vừa nhập ở bảng khách hàng
            string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaKH", txtMaKH.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            btnDat.Enabled = false;
            btnNhan.Enabled = false;
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

        private void btnXemphong_Click(object sender, EventArgs e)
        {
            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show("Mời chọn phòng muốn xem !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            txtMaphong.Text = dgvPhong.CurrentRow.Cells["MaPhong"].Value.ToString();
            switch (txtMaphong.Text)
            {
                case "P001":
                    frmSingleroom frmsingleroom = new frmSingleroom();
                    frmsingleroom.ShowDialog();
                    break;
                case "P002":
                    frmDoubleroom frmdoubleroom = new frmDoubleroom();
                    frmdoubleroom.ShowDialog();
                    break;
                case "P003":
                    frmLuxury frmluxury = new frmLuxury();
                    frmluxury.ShowDialog();
                    break;
                case "P004":
                    frmFamilyroom frmfamily = new frmFamilyroom();
                    frmfamily.ShowDialog();
                    break;
                case "P005":
                    frmVip frmvip = new frmVip();
                    frmvip.ShowDialog();
                    break;
                default:
                    MessageBox.Show("Không có thông tin phòng. Vui lòng xe phòng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

        }
    }
}
