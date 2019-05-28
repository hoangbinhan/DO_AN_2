using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BAL;
using System.Data.SqlClient;
using System.IO;
using System.Net;
namespace DOAN_HQTCSDL
{
    public partial class frmLogin : Form
    {
        public static bool quyen = true;
        //chuỗi kết nối
        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=QuanLyLinhKienPC;Integrated Security=True");

        //
        DALayer s = null;
        public string serveName;
        public string userName;
        public string password;
        public string database;

        //tạo database
        DataTable dt = null;
        public string conn = "";
        public string f = "";

        public frmLogin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
             MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userName = txtTaiKhoan.Text.ToString();
            password = txtMatKhau.Text.ToString();
            try
            {
                conn = @"Data Source=" + "(local)" + ";Database=" + "QuanLyLinhKienPC" + ";Integrated Security = true";
                //StreamWriter writer = new StreamWriter("ConnectString.con");
                //writer.WriteLine(conn);
                //writer.Close();
                s = new DALayer();
                if (s.Connection() == "y")
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM TaiKhoan WHERE TenTK ='" + userName + "' and Pass='" + password + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        if (userName == "admin")
                            quyen = true;
                        else
                            quyen = false;
                        this.Hide();
                        Form main = new frmMain();
                        main.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công");
                    }
                }
                else
                {
                    MessageBox.Show("Connect Failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không truy cập Database !!!/n" + ex, "Thông báo ",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnLogin_Click(null, null);
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}
