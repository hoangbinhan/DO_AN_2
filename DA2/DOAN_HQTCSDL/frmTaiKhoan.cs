using BAL;
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

namespace DOAN_HQTCSDL
{
    public partial class frmTaiKhoan : Form
    {
        BLL_TaiKhoan dbTaiKhoan = null; //khởi tạo đối tượng tài khoản     
        BLL_NhanVien dbNhanVien = null;
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
        public frmTaiKhoan()
        {
            InitializeComponent();
        }
        public void LoadData()  //load dữ liệu
        {
            txtTenDangNhap.ResetText();
            txtMatKhau.ResetText();
            cmbNhanVien.ResetText();
            txtTenDangNhap.Enabled = false;
            txtMatKhau.Enabled = false;
            cmbNhanVien.Enabled = false;

            //đổ dữ liệu lên datagridview
            try
            {
                dbTaiKhoan = new BLL_TaiKhoan();
                dt = new DataTable();
                dt.Clear();
                dt = dbTaiKhoan.SelectTaiKhoan().Tables[0];
                dgvTaiKhoan.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            //đổ dữ liệu lên combobox
            try
            {
                dbNhanVien = new BLL_NhanVien();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhanVien.SelectNhanVien().Tables[0];
                cmbNhanVien.DataSource = dt;
                cmbNhanVien.DisplayMember = "HoTen";
                cmbNhanVien.ValueMember = "MaNV";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            txtTenDangNhap.ResetText();
            txtMatKhau.ResetText();
            cmbNhanVien.ResetText();
            txtTenDangNhap.Enabled = true;
            txtMatKhau.Enabled = true;
            cmbNhanVien.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            txtTenDangNhap.Enabled = false;
            txtMatKhau.Enabled = true;
            cmbNhanVien.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "admin")
            {
                MessageBox.Show("Không thể xóa tài khoản admin");

            }
            else
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
                        int r = dgvTaiKhoan.CurrentCell.RowIndex;
                        //Lấy MaPhim của record hiện hành
                        string TenTK = txtTenDangNhap.Text;

                        //Khai báo biến
                        bool f = false;
                        string err = "";
                        //Gọi Strored Procedure để Update data
                        f = dbTaiKhoan.DeleteTaiKhoan(ref err, TenTK);
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
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (them == true)
            {
                bool f = false; // biến cờ
                string err = "";
                try
                {
                    //Thực hiện câu lệnh sql
                    //Gọi Strored Procedure để Insert data
                    f = dbTaiKhoan.InsertTaiKhoan(ref err, cmbNhanVien.SelectedValue.ToString(), txtTenDangNhap.Text,txtMatKhau.Text,"NhanVien");
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
                    f = dbTaiKhoan.UpdateTaiKhoan(ref err, txtTenDangNhap.Text, txtMatKhau.Text);

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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
           MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
             MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmM = new frmMain();
            frmM.ShowDialog();
            this.Close();
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvTaiKhoan.CurrentCell.RowIndex;
            txtTenDangNhap.Text = dgvTaiKhoan.Rows[r].Cells[1].Value.ToString();
            txtMatKhau.Text = dgvTaiKhoan.Rows[r].Cells[2].Value.ToString();
            cmbNhanVien.Text = dgvTaiKhoan.Rows[r].Cells[0].Value.ToString();
        }

        public void LoadDataKeyWord()
        {
            //đổ dữ liệu lên datagridview
            try
            {
                dbTaiKhoan = new BLL_TaiKhoan();
                dt = new DataTable();
                dt.Clear();
                string err = "";
                dt = dbTaiKhoan.SearchTaiKhoan(ref err, txtSearch.Text.ToString()).Tables[0];
                dgvTaiKhoan.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            //đổ dữ liệu lên combobox
            try
            {
                dbNhanVien = new BLL_NhanVien();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhanVien.SelectNhanVien().Tables[0];
                cmbNhanVien.DataSource = dt;
                cmbNhanVien.DisplayMember = "TenNV";
                cmbNhanVien.ValueMember = "MaNV";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataKeyWord();
        }
    }
}
