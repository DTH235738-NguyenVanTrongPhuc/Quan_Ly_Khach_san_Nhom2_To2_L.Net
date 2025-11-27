namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    partial class frmDat_Dich_Vu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpNgaydat = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSoluong = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMadp = new System.Windows.Forms.TextBox();
            this.txtMadv = new System.Windows.Forms.TextBox();
            this.txtMadatdv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnDat = new System.Windows.Forms.Button();
            this.dgvDat_Dich_Vu = new System.Windows.Forms.DataGridView();
            this.dgvDich_Vu = new System.Windows.Forms.DataGridView();
            this.btnNhapthongtin = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDat_Dich_Vu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDich_Vu)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpNgaydat);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSoluong);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMadp);
            this.groupBox1.Controls.Add(this.txtMadv);
            this.groupBox1.Controls.Add(this.txtMadatdv);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox1.Location = new System.Drawing.Point(19, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 196);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin đặt phòng";
            // 
            // dtpNgaydat
            // 
            this.dtpNgaydat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtpNgaydat.Location = new System.Drawing.Point(125, 149);
            this.dtpNgaydat.Name = "dtpNgaydat";
            this.dtpNgaydat.Size = new System.Drawing.Size(200, 23);
            this.dtpNgaydat.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.ForeColor = System.Drawing.Color.Gold;
            this.label5.Location = new System.Drawing.Point(19, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 17);
            this.label5.TabIndex = 61;
            this.label5.Text = "Ngày đặt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(19, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 59;
            this.label3.Text = "Số lượng";
            // 
            // txtSoluong
            // 
            this.txtSoluong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSoluong.Location = new System.Drawing.Point(125, 119);
            this.txtSoluong.Name = "txtSoluong";
            this.txtSoluong.Size = new System.Drawing.Size(200, 23);
            this.txtSoluong.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.ForeColor = System.Drawing.Color.Gold;
            this.label9.Location = new System.Drawing.Point(19, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 17);
            this.label9.TabIndex = 57;
            this.label9.Text = "Mã đặt dịch vụ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(18, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Mã đặt phòng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(18, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Mã dịch vụ";
            // 
            // txtMadp
            // 
            this.txtMadp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMadp.Location = new System.Drawing.Point(125, 61);
            this.txtMadp.Name = "txtMadp";
            this.txtMadp.Size = new System.Drawing.Size(200, 23);
            this.txtMadp.TabIndex = 29;
            // 
            // txtMadv
            // 
            this.txtMadv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMadv.Location = new System.Drawing.Point(125, 89);
            this.txtMadv.Name = "txtMadv";
            this.txtMadv.Size = new System.Drawing.Size(200, 23);
            this.txtMadv.TabIndex = 30;
            // 
            // txtMadatdv
            // 
            this.txtMadatdv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMadatdv.Location = new System.Drawing.Point(125, 33);
            this.txtMadatdv.Name = "txtMadatdv";
            this.txtMadatdv.Size = new System.Drawing.Size(200, 23);
            this.txtMadatdv.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 39);
            this.label1.TabIndex = 63;
            this.label1.Text = "ĐẶT DỊCH VỤ";
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(585, 411);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(138, 45);
            this.btnThoat.TabIndex = 64;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(404, 411);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(154, 45);
            this.btnHuy.TabIndex = 63;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDat
            // 
            this.btnDat.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDat.ForeColor = System.Drawing.Color.White;
            this.btnDat.Location = new System.Drawing.Point(222, 411);
            this.btnDat.Name = "btnDat";
            this.btnDat.Size = new System.Drawing.Size(154, 45);
            this.btnDat.TabIndex = 59;
            this.btnDat.Text = "Đặt";
            this.btnDat.UseVisualStyleBackColor = false;
            this.btnDat.Click += new System.EventHandler(this.btnDat_Click);
            // 
            // dgvDat_Dich_Vu
            // 
            this.dgvDat_Dich_Vu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDat_Dich_Vu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDat_Dich_Vu.Location = new System.Drawing.Point(19, 253);
            this.dgvDat_Dich_Vu.Name = "dgvDat_Dich_Vu";
            this.dgvDat_Dich_Vu.RowHeadersWidth = 51;
            this.dgvDat_Dich_Vu.RowTemplate.Height = 24;
            this.dgvDat_Dich_Vu.Size = new System.Drawing.Size(704, 152);
            this.dgvDat_Dich_Vu.TabIndex = 65;
            // 
            // dgvDich_Vu
            // 
            this.dgvDich_Vu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDich_Vu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDich_Vu.Location = new System.Drawing.Point(365, 23);
            this.dgvDich_Vu.Name = "dgvDich_Vu";
            this.dgvDich_Vu.RowHeadersWidth = 51;
            this.dgvDich_Vu.RowTemplate.Height = 24;
            this.dgvDich_Vu.Size = new System.Drawing.Size(358, 224);
            this.dgvDich_Vu.TabIndex = 66;
            // 
            // btnNhapthongtin
            // 
            this.btnNhapthongtin.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnNhapthongtin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnNhapthongtin.ForeColor = System.Drawing.Color.White;
            this.btnNhapthongtin.Location = new System.Drawing.Point(19, 411);
            this.btnNhapthongtin.Name = "btnNhapthongtin";
            this.btnNhapthongtin.Size = new System.Drawing.Size(174, 45);
            this.btnNhapthongtin.TabIndex = 67;
            this.btnNhapthongtin.Text = "Nhập thông tin";
            this.btnNhapthongtin.UseVisualStyleBackColor = false;
            this.btnNhapthongtin.Click += new System.EventHandler(this.btnNhapthongtin_Click);
            // 
            // frmDat_Dich_Vu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(735, 477);
            this.Controls.Add(this.btnNhapthongtin);
            this.Controls.Add(this.dgvDich_Vu);
            this.Controls.Add(this.dgvDat_Dich_Vu);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDat);
            this.Name = "frmDat_Dich_Vu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ĐẶT DỊCH VỤ";
            this.Load += new System.EventHandler(this.frmDat_Dich_Vu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDat_Dich_Vu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDich_Vu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMadp;
        private System.Windows.Forms.TextBox txtMadv;
        private System.Windows.Forms.TextBox txtMadatdv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSoluong;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnDat;
        private System.Windows.Forms.DataGridView dgvDat_Dich_Vu;
        private System.Windows.Forms.DataGridView dgvDich_Vu;
        private System.Windows.Forms.Button btnNhapthongtin;
        private System.Windows.Forms.DateTimePicker dtpNgaydat;
        private System.Windows.Forms.Label label5;
    }
}