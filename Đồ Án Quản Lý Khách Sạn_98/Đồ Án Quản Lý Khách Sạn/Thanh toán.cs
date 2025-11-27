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
    public partial class frmThanh_Toan : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        int madp;
        public frmThanh_Toan(int madp)
        {
            InitializeComponent();
            this.madp = madp;
        }

        private void frmThanh_Toan_Load(object sender, EventArgs e)
        {
            gbxPhuongthucTT.Enabled = false;
            gbxChiphi.Enabled = false;
            RefreshDataGridView_ThanhToan();
            HienThiThongTinThanhToan(madp);
            LoadDanhSachDichVu(madp);
            TinhTien();
        }

        public void RefreshDataGridView_ThanhToan()
        {
            string query = "SELECT * FROM ThanhToan";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvThanh_Toan.DataSource = dt;
                dgvThanh_Toan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        // Thông tin dịch vụ đã sử dụng
        private void LoadDanhSachDichVu(int madp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @" SELECT dv.MaDV, dv.TenDV, ctdv.SoLuong, dv.GiaDV, ctdv.SoLuong * dv.GiaDV AS ThanhTien
                                  FROM ChiTietDichVu ctdv
                                  JOIN DichVu dv ON ctdv.MaDV = dv.MaDV
                                  WHERE ctdv.MaDatPhong = @MaDatPhong";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@MaDatPhong", madp);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDich_Vu_Su_Dung.DataSource = dt;
                    dgvDich_Vu_Su_Dung.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }

        // Hiển thị thông tin thanh toán của khách hàng
        public void HienThiThongTinThanhToan(int madp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT dp.MaDatPhong, dp.MaPhong, p.LoaiPhong, p.GiaTien, dp.MaKH, kh.TenKH, dp.NgayNhan, dp.NgayTra
                                 FROM DatPhong dp
                                 JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                                 JOIN Phong p ON dp.MaPhong = p.MaPhong
                                 WHERE dp.MaDatPhong = @MaDatPhong";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDatPhong", madp);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtMaDP.Text = reader["MaDatPhong"].ToString();
                        txtMaphong.Text = reader["MaPhong"].ToString();
                        txtMaKH.Text = reader["MaKH"].ToString();
                        txtTenKH.Text = reader["TenKH"].ToString();
                        txtLoaiphong.Text = reader["LoaiPhong"].ToString();
                        txtTienphong.Text = reader["GiaTien"].ToString();
                        dtpNgaynhan.Value = Convert.ToDateTime(reader["NgayNhan"]);
                        dtpNgaytra.Value = Convert.ToDateTime(reader["NgayTra"]);
                    }
                }
            }
        }

        // Tính tiền  Phòng - Dịch Vụ - Tổng Tiền
        private void TinhTien()
        {
            int giaPhong = 0;
            int.TryParse(txtTienphong.Text, out giaPhong);

            int soNgay = (dtpNgaytra.Value - dtpNgaynhan.Value).Days + 1;
            int tienPhong = giaPhong * soNgay;

            int tienDichVu = 0;
            foreach (DataGridViewRow row in dgvDich_Vu_Su_Dung.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    int thanhTien;
                    if (int.TryParse(row.Cells["ThanhTien"].Value.ToString(), out thanhTien))
                        tienDichVu += thanhTien;
                }
            }

            txtTongtienphong.Text = tienPhong.ToString();
            txtTiendv.Text = tienDichVu.ToString();
            txtThanhtien.Text = (tienPhong + tienDichVu).ToString();
        }

        // ------------------------ Thanh toán ------------------------
        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            gbxPhuongthucTT.Enabled = true;
                
        }
            private void btnChon_Click(object sender, EventArgs e)
            {
                btnThanhtoan.Enabled = false;
                if (rdbChuyenkhoan.Checked == false && rdbTienmat.Checked == false)
                {
                    MessageBox.Show("Bạn chưa chọn phương thức thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            string maPhong = txtMaphong.Text;
            string tenKhachHang = txtTenKH.Text;
            string thanhtien = txtThanhtien.Text;
            string tongtienphong = txtTongtienphong.Text;
            string tiendichvu = txtTiendv.Text;
            string loaiphong = txtLoaiphong.Text;
            DateTime ngayNhan = dtpNgaynhan.Value;
            DateTime ngayTra = dtpNgaytra.Value;
            string chuyenkhoan = rdbChuyenkhoan.Text;
            string tienmat = rdbTienmat.Text;
            string giaphong = txtTienphong.Text;

            // Tạo Form HoaDon và truyền dữ liệu qua constructor
            frmHoa_Don hd = new frmHoa_Don(madp);
            hd.MaPhong = maPhong;
            hd.TenKhachHang = tenKhachHang;
            hd.ThanhTien = thanhtien;
            hd.NgayNhan = ngayNhan.ToString("yyyy-MM-dd");
            hd.NgayTra = ngayTra.ToString("yyyy-MM-dd");
            hd.tongtienphong = tongtienphong;
            hd.tongdv = tiendichvu;
            hd.loaiphong = loaiphong;
            hd.Giaphong = giaphong;
            if (rdbChuyenkhoan.Checked)
            {
                hd.thanhtoan = chuyenkhoan;
                hd.thanhtoan = "chuyển Khoản";

            }
            else if (rdbTienmat.Checked)
            {
                hd.thanhtoan = tienmat;
                hd.thanhtoan = "Tiền mặt";

            }

            hd.ShowDialog();
        }
        // ------------------------ Thanh toán thành công ------------------------
        private void btnDathanhtoan_Click(object sender, EventArgs e)
        {
           
            string maphong = txtMaphong.Text;
            string makhachhang = txtMaKH.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Xóa ChiTietDichVu
                string deleteCTDV = "DELETE FROM ChiTietDichVu WHERE MaDatPhong = @MaDatPhong";
                using (SqlCommand cmd = new SqlCommand(deleteCTDV, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDatPhong", madp);
                    cmd.ExecuteNonQuery();
                }

                // Xóa ThanhToan
                string deleteTT = "DELETE FROM ThanhToan WHERE MaDatPhong = @MaDatPhong";
                using (SqlCommand cmd = new SqlCommand(deleteTT, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDatPhong", madp);
                    cmd.ExecuteNonQuery();
                }

                // Xóa DatPhong
                string deleteDP = "DELETE FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
                using (SqlCommand cmd = new SqlCommand(deleteDP, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDatPhong", madp);
                    cmd.ExecuteNonQuery();
                }

                // Kiểm tra nếu khách hàng còn booking khác
                string checkKH = "SELECT COUNT(*) FROM DatPhong WHERE MaKH = @MaKH";
                int countKH;
                using (SqlCommand cmd = new SqlCommand(checkKH, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", makhachhang);
                    countKH = (int)cmd.ExecuteScalar();
                }

                // Nếu không còn booking thì xóa khách hàng
                if (countKH == 0)
                {
                    string deleteKH = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
                    using (SqlCommand cmd = new SqlCommand(deleteKH, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", makhachhang);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật trạng thái phòng
                string updatePhong = "UPDATE Phong SET TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
                using (SqlCommand cmd = new SqlCommand(updatePhong, conn))
                {
                    cmd.Parameters.AddWithValue("@TrangThai", "TRỐNG");
                    cmd.Parameters.AddWithValue("@MaPhong", maphong);
                    cmd.ExecuteNonQuery();
                }
            }

            RefreshDataGridView_ThanhToan();
            MessageBox.Show("ĐÃ THANH TOÁN THÀNH CÔNG !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // ------------------------ Thoát ------------------------
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
