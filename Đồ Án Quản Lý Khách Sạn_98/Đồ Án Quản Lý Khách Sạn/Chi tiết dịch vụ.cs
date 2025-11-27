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
    public partial class frmCTDV : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        public frmCTDV()
        {
            InitializeComponent();
        }

        private void frmCTDV_Load(object sender, EventArgs e)
        {
            RefreshDatagridview();
        }

        public void RefreshDatagridview()
        {
            string query = "SELECT * FROM ChiTietDichVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCTDV.DataSource = dt;
                dgvCTDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }    
        }
        // ----------------------------Xem chi tiết  dịch vụ ----------------------------
        private void btnXemchitiet_Click(object sender, EventArgs e)
        {
            txtMactdv.Text = dgvCTDV.CurrentRow.Cells["MaCTDV"].Value.ToString();
            txtMadp.Text = dgvCTDV.CurrentRow.Cells["MaDatPhong"].Value.ToString();
            txtMadv.Text = dgvCTDV.CurrentRow.Cells["MaDV"].Value.ToString();
            txtSoluong.Text = dgvCTDV.CurrentRow.Cells["Soluong"].Value.ToString();

            if (dgvCTDV.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn chi tiết muồn xem !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                {
                    string query = @"SELECT ct.MaCTDV, dp.MaDatPhong, dp.MaPhong, kh.MaKH, kh.TenKH, ct.MaDV, dv.TenDV, ct.SoLuong
                                 FROM DatPhong dp
                                 JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                                 JOIN ChiTietDichVu ct ON ct.MaDatPhong = dp.MaDatPhong
                                 JOIN DichVu dv ON ct.MaDV = dv.MaDV
                                 WHERE ct.MaCTDV = @MaCTDV";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDatPhong", txtMactdv.Text);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtMactdv.Text = reader["MaCTDV"].ToString();
                            txtMadp.Text = reader["MaDP"].ToString();
                            txtMaphong.Text = reader["MaPhong"].ToString();
                            txtMakh.Text = reader["MaKH"].ToString();
                            txtTenkh.Text = reader["TenKH"].ToString();
                            txtMadv.Text = reader["MaDV"].ToString();
                            txtTendv.Text = reader["TenDV"].ToString();
                            txtSoluong.Text = reader["SoLuong"].ToString();
                        }
                    }
                }
            }    
        }
    }
}
