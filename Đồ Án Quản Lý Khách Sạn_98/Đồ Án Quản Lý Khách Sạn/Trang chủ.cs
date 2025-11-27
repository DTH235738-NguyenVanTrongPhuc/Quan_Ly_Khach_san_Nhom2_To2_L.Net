using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    public partial class formTrang_Chu : Form
    {
//Form Trang Chủ
        public formTrang_Chu()
        {
            InitializeComponent();
            
        }
        private void formTrang_Chu_Load(object sender, EventArgs e)
        {

        }

        // Chuyển từ trang chủ sang trang đăng nhập (Ấn Sign In)
        // Dùng ShowDialog để bắt buộc người dùng phải đăng nhập trước khi vào trang chủ
        private void btnDang_nhap_Click(object sender, EventArgs e)
        {
            frmDang_Nhap frmdang_nhap = new frmDang_Nhap();
            frmdang_nhap.ShowDialog();
        }

        private void btndatphong_Click(object sender, EventArgs e)
        {
            frmDat_Phong dt = new frmDat_Phong();
            dt.ShowDialog();
        }

        private void btntraphong_Click(object sender, EventArgs e)
        {
            frmTra_Phong tp = new frmTra_Phong();
           tp.ShowDialog();
        }
        // Chuyển sang thao tác muốn thực hiện (Ấn Xem)
        // Dùng Show để có thể chuyển qua lại giữa các form

    }
}
