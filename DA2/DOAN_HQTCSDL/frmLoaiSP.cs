﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BAL;

namespace DOAN_HQTCSDL
{
    public partial class frmLoaiSP : Form
    {
        BAL_LoaiSP dbLoaiSP = null; //khởi tạo đối tượng loại sản phẩm
        DataTable dt = null;    //tạo bảng
        bool them = false;  //biến cờ cho biết thêm hoặc sửa
        #region move form
        bool ismousedown = false; // Kiểm tra xem con trỏ chuột đã mousedown chưa
        Point mousedownPosition = new Point();
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ismousedown = true;
            mousedownPosition.X = e.X;
            mousedownPosition.Y = e.Y;

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            ismousedown = false;
            Cursor cur = Cursors.Arrow;
            this.Cursor = cur;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ismousedown == true)
            {
                Point newPoint = new Point();
                newPoint.X = this.Location.X + (e.X - mousedownPosition.X);
                newPoint.Y = this.Location.Y + (e.Y - mousedownPosition.Y);
                this.Location = newPoint;
            }
        }
        #endregion  // hàm di chuyển form
        public frmLoaiSP()
        {
            InitializeComponent();           
        }
        //
        public void LoadData()  //load dữ liệu
        {
            try
            {
                dbLoaiSP = new BAL_LoaiSP();
                dt = new DataTable();
                dt.Clear();
                dt = dbLoaiSP.SelectLoaiSP().Tables[0];             
                dgvLoaiSP.DataSource = dt;
            }
             catch (SqlException ex)
            {
             MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
            gpLoaiSP.Enabled = false;
            txtMaLoaiSP.ResetText();
            txtTenLoaiSP.ResetText();
            txtMaLoaiSP.Enabled = true;
        }

        private void frmLoaiSP_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            gpLoaiSP.Enabled = true;
            txtMaLoaiSP.Enabled = true;
            txtMaLoaiSP.ResetText();
            txtTenLoaiSP.ResetText();
            txtMaLoaiSP.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            gpLoaiSP.Enabled = true;
            txtMaLoaiSP.Enabled = false;
            txtTenLoaiSP.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
             MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Hiện hộp thoại hỏi 
            DialogResult result3;
            result3 = MessageBox.Show("Bạn có chắc muốn xóa ?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //Kiểm tra có chọn Yes or No
            if (result3 == DialogResult.Yes)
            {
                try
                {
                    //Lấy thứ tự của record hiện hành
                    int r = dgvLoaiSP.CurrentCell.RowIndex;
                    //Lấy MaPhim của record hiện hành
                    string MaLSP = txtMaLoaiSP.Text;          
                    
                    //Khai báo biến
                    bool f = false;
                    string err = "";
                    //Gọi Strored Procedure để Update data
                    f = dbLoaiSP.DeleteLoaiSP(ref err, MaLSP);
                    //Kiểm tra để thông báo
                    if (f == true)
                    {
                        //Sau khi Delete xong Load lại data
                        LoadData();
                        //Xóa trống các đối tượng trên panel                       
                        //
                        MessageBox.Show("Delete Thành công!!!");
                    }
                    else
                        MessageBox.Show("Error:" + err);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Delete lỗi!!!", "Thông báo",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmM = new frmMain();
            frmM.ShowDialog();
            this.Close();
        }
        private void dgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvLoaiSP.CurrentCell.RowIndex;
            txtMaLoaiSP.Text = dgvLoaiSP.Rows[r].Cells[0].Value.ToString();
            txtTenLoaiSP.Text = dgvLoaiSP.Rows[r].Cells[1].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(them == true)
            {               
                bool f = false; // biến cờ
                string err = "";
                try
                {
                    //Thực hiện câu lệnh sql
                    //Gọi Strored Procedure để Insert data
                    f = dbLoaiSP.InsertLoaiSP(ref err, txtMaLoaiSP.Text,txtTenLoaiSP.Text);
                    //Kiểm tra để thông báo
                    if (f == true)
                        MessageBox.Show("Thêm thành công!!!");
                    else
                        MessageBox.Show("Error:" + err);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Thêm lỗi!!!", "Thông báo",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Sau khi thêm xong Load lại data
                LoadData();
                //}
            }
            else
            {              
                bool f = false; //biến cờ
                string err = "";
                try
                {
                    //Thực hiện câu lệnh sql
                    //Gọi Strored Procedure để Insert data
                    f = dbLoaiSP.UpdateLoaiSP(ref err, txtMaLoaiSP.Text, txtTenLoaiSP.Text);
                       
                    //Kiểm tra để thông báo
                    if (f == true)
                        MessageBox.Show("Sửa thông tin thành công!!!");
                    else
                        MessageBox.Show("Error:" + err);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Sửa thông tin lỗi!!!", "Thông báo",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Sau khi thêm xong Load lại data
                LoadData();              
            }
        }
    }
}
