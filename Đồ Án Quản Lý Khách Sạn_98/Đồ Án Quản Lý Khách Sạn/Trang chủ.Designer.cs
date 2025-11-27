namespace Đồ_Án_Quản_Lý_Khách_Sạn
{
    partial class formTrang_Chu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formTrang_Chu));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDang_nhap = new System.Windows.Forms.Button();
            this.btndatphong = new System.Windows.Forms.Button();
            this.btntraphong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Navy;
            this.label1.Font = new System.Drawing.Font("Mistral", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(144, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(526, 44);
            this.label1.TabIndex = 22;
            this.label1.Text = "ONE THOUSAND AND ONE NIGHTS HOTEL";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 42);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(783, 419);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // btnDang_nhap
            // 
            this.btnDang_nhap.BackColor = System.Drawing.Color.Blue;
            this.btnDang_nhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDang_nhap.ForeColor = System.Drawing.Color.White;
            this.btnDang_nhap.Location = new System.Drawing.Point(84, -1);
            this.btnDang_nhap.Name = "btnDang_nhap";
            this.btnDang_nhap.Size = new System.Drawing.Size(146, 37);
            this.btnDang_nhap.TabIndex = 24;
            this.btnDang_nhap.Text = "SIGN IN";
            this.btnDang_nhap.UseVisualStyleBackColor = false;
            this.btnDang_nhap.Click += new System.EventHandler(this.btnDang_nhap_Click);
            // 
            // btndatphong
            // 
            this.btndatphong.BackColor = System.Drawing.Color.Blue;
            this.btndatphong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btndatphong.ForeColor = System.Drawing.Color.White;
            this.btndatphong.Location = new System.Drawing.Point(488, -1);
            this.btndatphong.Name = "btndatphong";
            this.btndatphong.Size = new System.Drawing.Size(146, 37);
            this.btndatphong.TabIndex = 25;
            this.btndatphong.Text = "Đặt Phòng";
            this.btndatphong.UseVisualStyleBackColor = false;
            this.btndatphong.Click += new System.EventHandler(this.btndatphong_Click);
            // 
            // btntraphong
            // 
            this.btntraphong.BackColor = System.Drawing.Color.Blue;
            this.btntraphong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btntraphong.ForeColor = System.Drawing.Color.White;
            this.btntraphong.Location = new System.Drawing.Point(640, -1);
            this.btntraphong.Name = "btntraphong";
            this.btntraphong.Size = new System.Drawing.Size(146, 37);
            this.btntraphong.TabIndex = 26;
            this.btntraphong.Text = "Trả Phòng";
            this.btntraphong.UseVisualStyleBackColor = false;
            this.btntraphong.Click += new System.EventHandler(this.btntraphong_Click);
            // 
            // formTrang_Chu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(798, 463);
            this.Controls.Add(this.btntraphong);
            this.Controls.Add(this.btndatphong);
            this.Controls.Add(this.btnDang_nhap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "formTrang_Chu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRANG CHỦ";
            this.Load += new System.EventHandler(this.formTrang_Chu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDang_nhap;
        private System.Windows.Forms.Button btndatphong;
        private System.Windows.Forms.Button btntraphong;
    }
}

