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
    public partial class frmHoa_Don : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        int madp;
        public frmHoa_Don(int madp)
        {
            InitializeComponent();
            this.madp = madp;
        }

        private void frmHoa_Don_Load(object sender, EventArgs e)
        {
            LoadDanhSachDichVu(madp);
        }

        private void LoadDanhSachDichVu(int madp)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT dv.MaDV, dv.TenDV, ctdv.SoLuong, dv.GiaDV, 
                       ctdv.SoLuong * dv.GiaDV AS ThanhTien
                FROM ChiTietDichVu ctdv
                JOIN DichVu dv ON ctdv.MaDV = dv.MaDV
                WHERE ctdv.MaDatPhong = @MaDatPhong";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@MaDatPhong", madp);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvTT_DV.DataSource = dt;
                        dgvTT_DV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        // Format cột
                        if (dt.Columns.Count > 0)
                        {
                            dgvTT_DV.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                            dgvTT_DV.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                            dgvTT_DV.Columns["MaDV"].HeaderText = "Mã DV";
                            dgvTT_DV.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";
                            dgvTT_DV.Columns["SoLuong"].HeaderText = "Số Lượng";
                            dgvTT_DV.Columns["GiaDV"].HeaderText = "Đơn Giá";
                            dgvTT_DV.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách dịch vụ: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string MaPhong
        {
            set { txtMaphong.Text = value; }
        }

        public string TenKhachHang
        {
            set { txtTenKH.Text = value; }
        }

        public string ThanhTien
        {
            set { txtThanhTien.Text = value; }
        }

        public string NgayNhan
        {
            set { txtngaynhan.Text = value; }
        }

        public string NgayTra
        {
            set { txtngaytra.Text = value; }
        }
        public string tongtienphong
        {
            set { txtTienPhong.Text = value; }
        }
        public string tongdv
        {
            set { txtTienDV.Text = value; }
        }

        public string thanhtoan

        {
            set { txtloaithanhtoan.Text = value; }
        }
        public string loaiphong

        {
            set { txtLoaiphong.Text = value; }
        }
        public string Giaphong

        {
            set { txtGiaphong.Text = value; }
        }
    }
}