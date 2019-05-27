using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_HQTCSDL
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnChiTietHoaDon_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmCTHD = new frmChiTietHoaDon();
            frmCTHD.ShowDialog();
            this.Close();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmKH = new frmKhachHang();
            frmKH.ShowDialog();
            this.Close();
        }

        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            if (lblTen.Text == "ADMIN")
            {
                this.Hide();
                Form frmQLTK = new frmTaiKhoan();
                frmQLTK.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nhân viên không có quyền truy cập chức năng này!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnTaiKhoan.Text = "Clock\n" + System.DateTime.Now.ToString();
        }

        private void btnLoaiSP_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmLSP = new frmLoaiSP();
            frmLSP.ShowDialog();
            this.Close();
        }

        private void btnLoaiKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmLKH = new frmLoaiKH();
            frmLKH.ShowDialog();
            this.Close();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmSP = new frmSanPham();
            frmSP.ShowDialog();
            this.Close();
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmNCC = new frmNhaCungCap();
            frmNCC.ShowDialog();
            this.Close();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmNV = new frmNhanVien();
            frmNV.ShowDialog();
            this.Close();
        }

        private void btnThongTIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmTT = new frmThongTin();
            frmTT.ShowDialog();
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }
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

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmTK = new frmThongKe();
            frmTK.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
             MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmLG = new frmLogin();
            frmLG.ShowDialog();
            this.Close();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmTK = new frmTaiKhoan();
            frmTK.ShowDialog();
            this.Close();
        }
    }
}
