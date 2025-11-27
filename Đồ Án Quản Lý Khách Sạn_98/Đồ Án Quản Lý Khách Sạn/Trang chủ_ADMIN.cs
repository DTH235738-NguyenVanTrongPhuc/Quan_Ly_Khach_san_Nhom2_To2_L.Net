using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    public partial class frmTrang_Chu_Admin : Form
    {
        public frmTrang_Chu_Admin()
        {
            InitializeComponent();
        }
        private void frmTrang_Chu_Admin_Load(object sender, EventArgs e)
        {

        }

        // CLick nút Xem
        // Dùng Show để có thể chuyển qua lại giữa các form
       

        private void btnDang_Xuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnquanlyphong_Click(object sender, EventArgs e)
        {
            frmPhong p=new frmPhong();
            p.ShowDialog();
        }

        private void btncapnhatnv_Click(object sender, EventArgs e)
        {
            frmCap_Nhat_Nhan_Su capnhatnv= new frmCap_Nhat_Nhan_Su();
            capnhatnv.ShowDialog();
        }

        private void btnquanlynv_Click(object sender, EventArgs e)
        {
            frmNhan_Su nv=new frmNhan_Su();
            nv.ShowDialog();
        }

        private void btnquanlykh_Click(object sender, EventArgs e)
        {
            frmKhach_Hang kh=new frmKhach_Hang();
            kh.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
