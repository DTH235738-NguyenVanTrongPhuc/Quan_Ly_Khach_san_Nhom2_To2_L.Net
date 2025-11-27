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
    public partial class frmDang_Nhap : Form
    {
        string connectionString = "Data Source = LAPTOP-DN604OJP;Initial Catalog = QuanLyKhachSan1;Integrated Security = True";
        public frmDang_Nhap()
        {
            InitializeComponent();
        }
        private void frmDang_Nhap_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void btnDang_nhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra đã nhập đầy đủ thông tin chưa
            if (string.IsNullOrWhiteSpace(username) )
            {
                MessageBox.Show("Vui lòng nhập Username !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập Password !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM DangNhap WHERE Username=@Username AND Password=@Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // Nếu dùng hash thì hash trước khi so sánh

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Đăng nhập thành công
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        frmTrang_Chu_Admin frmtrangchuadmin = new frmTrang_Chu_Admin();
                        frmtrangchuadmin.Show();
                        this.Hide(); // Ẩn form đăng nhập
                    }
                    else
                    {
                        // Sai username hoặc password
                        MessageBox.Show("Đăng nhập thất bại! Vui lòng kiểm tra lại tài khoản và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
