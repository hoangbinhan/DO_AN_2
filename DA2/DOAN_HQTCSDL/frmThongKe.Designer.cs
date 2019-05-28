namespace DOAN_HQTCSDL
{
    partial class frmThongKe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThongKe));
            this.lblTKTLSP = new System.Windows.Forms.Label();
            this.dgvThongKe = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnTroVe = new System.Windows.Forms.Button();
            this.cmbchange = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDoanhThu = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTongDT = new System.Windows.Forms.Label();
            this.lblTKTThang = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTKTLSP
            // 
            this.lblTKTLSP.AutoSize = true;
            this.lblTKTLSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTKTLSP.ForeColor = System.Drawing.Color.White;
            this.lblTKTLSP.Location = new System.Drawing.Point(6, 102);
            this.lblTKTLSP.Name = "lblTKTLSP";
            this.lblTKTLSP.Size = new System.Drawing.Size(255, 24);
            this.lblTKTLSP.TabIndex = 0;
            this.lblTKTLSP.Text = "Thống kê theo loại sản phẩm";
            this.lblTKTLSP.Click += new System.EventHandler(this.lblTKTLSP_Click);
            this.lblTKTLSP.MouseLeave += new System.EventHandler(this.lblTKTLSP_MouseLeave);
            this.lblTKTLSP.MouseHover += new System.EventHandler(this.lblTKTLSP_MouseHover);
            // 
            // dgvThongKe
            // 
            this.dgvThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKe.Location = new System.Drawing.Point(293, 192);
            this.dgvThongKe.Name = "dgvThongKe";
            this.dgvThongKe.Size = new System.Drawing.Size(436, 235);
            this.dgvThongKe.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(286, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "THỐNG KÊ";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(66, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 35);
            this.label10.TabIndex = 54;
            this.label10.Text = "Trở về";
            // 
            // btnTroVe
            // 
            this.btnTroVe.BackColor = System.Drawing.Color.Indigo;
            this.btnTroVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTroVe.ForeColor = System.Drawing.Color.Indigo;
            this.btnTroVe.Image = ((System.Drawing.Image)(resources.GetObject("btnTroVe.Image")));
            this.btnTroVe.Location = new System.Drawing.Point(0, -1);
            this.btnTroVe.Name = "btnTroVe";
            this.btnTroVe.Size = new System.Drawing.Size(60, 40);
            this.btnTroVe.TabIndex = 53;
            this.btnTroVe.UseVisualStyleBackColor = false;
            this.btnTroVe.Click += new System.EventHandler(this.btnTroVe_Click);
            // 
            // cmbchange
            // 
            this.cmbchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbchange.FormattingEnabled = true;
            this.cmbchange.Location = new System.Drawing.Point(90, 18);
            this.cmbchange.Name = "cmbchange";
            this.cmbchange.Size = new System.Drawing.Size(261, 32);
            this.cmbchange.TabIndex = 55;
            this.cmbchange.SelectedValueChanged += new System.EventHandler(this.cmbchange_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDoanhThu);
            this.panel1.Controls.Add(this.cmbchange);
            this.panel1.Location = new System.Drawing.Point(293, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 115);
            this.panel1.TabIndex = 56;
            // 
            // lblDoanhThu
            // 
            this.lblDoanhThu.AutoSize = true;
            this.lblDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoanhThu.ForeColor = System.Drawing.Color.White;
            this.lblDoanhThu.Location = new System.Drawing.Point(183, 76);
            this.lblDoanhThu.Name = "lblDoanhThu";
            this.lblDoanhThu.Size = new System.Drawing.Size(60, 24);
            this.lblDoanhThu.TabIndex = 56;
            this.lblDoanhThu.Text = "label1";
            this.lblDoanhThu.Click += new System.EventHandler(this.lblDoanhThu_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTongDT);
            this.groupBox1.Controls.Add(this.lblTKTThang);
            this.groupBox1.Controls.Add(this.lblTKTLSP);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(13, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 367);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê";
            // 
            // lblTongDT
            // 
            this.lblTongDT.AutoSize = true;
            this.lblTongDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDT.ForeColor = System.Drawing.Color.White;
            this.lblTongDT.Location = new System.Drawing.Point(6, 226);
            this.lblTongDT.Name = "lblTongDT";
            this.lblTongDT.Size = new System.Drawing.Size(145, 24);
            this.lblTongDT.TabIndex = 2;
            this.lblTongDT.Text = "Tổng doanh thu";
            this.lblTongDT.Click += new System.EventHandler(this.lblTongDT_Click);
            this.lblTongDT.MouseLeave += new System.EventHandler(this.lblTongDT_MouseLeave);
            this.lblTongDT.MouseHover += new System.EventHandler(this.lblTongDT_MouseHover);
            // 
            // lblTKTThang
            // 
            this.lblTKTThang.AutoSize = true;
            this.lblTKTThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTKTThang.ForeColor = System.Drawing.Color.White;
            this.lblTKTThang.Location = new System.Drawing.Point(6, 168);
            this.lblTKTThang.Name = "lblTKTThang";
            this.lblTKTThang.Size = new System.Drawing.Size(185, 24);
            this.lblTKTThang.TabIndex = 1;
            this.lblTKTThang.Text = "Thống kê theo tháng";
            this.lblTKTThang.Click += new System.EventHandler(this.lblTKTThang_Click);
            this.lblTKTThang.MouseLeave += new System.EventHandler(this.lblTKTThang_MouseLeave);
            this.lblTKTThang.MouseHover += new System.EventHandler(this.lblTKTThang_MouseHover);
            // 
            // frmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(741, 439);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnTroVe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvThongKe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmThongKe";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKe)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTKTLSP;
        private System.Windows.Forms.DataGridView dgvThongKe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnTroVe;
        private System.Windows.Forms.ComboBox cmbchange;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTongDT;
        private System.Windows.Forms.Label lblTKTThang;
        private System.Windows.Forms.Label lblDoanhThu;
    }
}