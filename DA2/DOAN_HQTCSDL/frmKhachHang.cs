using System;
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
using System.Text.RegularExpressions;

namespace DOAN_HQTCSDL
{
    public partial class frmKhachHang : Form
    {
        BLL_LoaiKH dbLoaiKH = null; //khởi tạo đối tượng loại khách hàng
        BLL_KhachHang dbKhachHang = null; //khởi tạo đối tượng khách hàng
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

        public frmKhachHang()
        {
            InitializeComponent();
        }
        public void LoadData()  //load dữ liệu
        {
            //đổ dữ liệu lên datagridview
            try
            {
                dbKhachHang = new BLL_KhachHang();
                dt = new DataTable();
                dt.Clear();
                dt = dbKhachHang.SelectKhachHang().Tables[0];
                dgvKhachHang.DataSource = dt;
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
                dbLoaiKH = new BLL_LoaiKH();
                dt = new DataTable();
                dt.Clear();
                dt = dbLoaiKH.SelectLoaiKH().Tables[0];
                cmbLoaiKH.DataSource = dt;
                cmbLoaiKH.DisplayMember = "TenLKH";
                cmbLoaiKH.ValueMember = "MaLKH";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //          
            rdNam.Checked = false;
            rdNu.Checked = false;
            //
            gpKhachHang.Enabled = false;
            txtMaKH.ResetText();
            txtTenKH.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();

            txtEmail.ResetText();
            cmbLoaiKH.ResetText();

        }
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvKhachHang.CurrentCell.RowIndex;
            txtMaKH.Text = dgvKhachHang.Rows[r].Cells[0].Value.ToString();
            txtTenKH.Text = dgvKhachHang.Rows[r].Cells[1].Value.ToString();
            txtSDT.Text = dgvKhachHang.Rows[r].Cells[2].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.Rows[r].Cells[3].Value.ToString();
            txtEmail.Text = dgvKhachHang.Rows[r].Cells[5].Value.ToString();
            cmbLoaiKH.Text = dgvKhachHang.Rows[r].Cells[6].Value.ToString();
            string gioitinh = dgvKhachHang.Rows[r].Cells[4].Value.ToString();
            if (gioitinh == "Nam")
                rdNam.Checked = true;
            else
                rdNu.Checked = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
              MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            gpKhachHang.Enabled = true;
            txtMaKH.Enabled = true;
            txtMaKH.ResetText();
            txtTenKH.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();
            rdNam.Checked = false;
            rdNu.Checked = false;
            txtEmail.ResetText();
            cmbLoaiKH.ResetText();
            //btnLuu.Enabled = true;
            //btnHuy.Enabled = true;
            //btnThem.Enabled = false;
            //btnSua.Enabled = false;
            //btnXoa.Enabled = false;
            //btnThoat.Enabled = false;
            txtMaKH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            dgvKhachHang_CellClick(null, null);
            gpKhachHang.Enabled = true;
            //btnLuu.Enabled = true;
            //btnHuy.Enabled = true;
            //btnThem.Enabled = false;
            //btnSua.Enabled = false;
            //btnXoa.Enabled = false;
            //btnThoat.Enabled = false;
            txtMaKH.Enabled = false;
            txtTenKH.Focus();
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
                    int r = dgvKhachHang.CurrentCell.RowIndex;
                    //Lấy MaPhim của record hiện hành
                    string MaKH = txtMaKH.Text;

                    //Khai báo biến
                    bool f = false;
                    string err = "";
                    //Gọi Strored Procedure để Update data
                    f = dbKhachHang.DeleteKhachHang(ref err, MaKH);
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        //check mail
        public static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        //check sdt
        public static bool IsValidPhone(string value)
        {
            string pattern = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
            return Regex.IsMatch(value, pattern);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (isEmail(txtEmail.Text) == false)
            {
                MessageBox.Show("Email không hợp lệ");
            }
            else if (IsValidPhone(txtSDT.Text) == false)
            {
                MessageBox.Show("SDT không hợp lệ");
            }
            else {
                if (them == true)
                {
                    bool f = false; // biến cờ
                    string err = "";
                    try
                    {
                        string gioitinh;
                        if (rdNam.Checked == true)
                            gioitinh = "Nam";
                        else
                            gioitinh = "Nữ";
                        //Thực hiện câu lệnh sql
                        //Gọi Strored Procedure để Insert data
                        f = dbKhachHang.InsertKhachHang(ref err, txtMaKH.Text, cmbLoaiKH.SelectedValue.ToString(),
                            txtTenKH.Text, txtSDT.Text, txtDiaChi.Text, gioitinh.ToString(), txtEmail.Text
                            );
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
                        string gioitinh;
                        if (rdNam.Checked == true)
                            gioitinh = "Nam";
                        else
                            gioitinh = "Nữ";
                        //Thực hiện câu lệnh sql
                        //Gọi Strored Procedure để Insert data
                        f = dbKhachHang.UpdateKhachHang(ref err, txtMaKH.Text, cmbLoaiKH.SelectedValue.ToString(),
                            txtTenKH.Text, txtSDT.Text, txtDiaChi.Text, gioitinh, txtEmail.Text
                            );

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
            } }
                   

        private void btnThoat_Click(object sender, EventArgs e)
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
        //
        public void LoadDataKeyWord()
        {
            //đổ dữ liệu lên datagridview
            try
            {
                dbKhachHang = new BLL_KhachHang();
                dt = new DataTable();
                dt.Clear();
                string err = "";
                dt = dbKhachHang.SearchKhachHang(ref err,txtSearch.Text.ToString()).Tables[0];
                dgvKhachHang.DataSource = dt;
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
                dbLoaiKH = new BLL_LoaiKH();
                dt = new DataTable();
                dt.Clear();
                dt = dbLoaiKH.SelectLoaiKH().Tables[0];
                cmbLoaiKH.DataSource = dt;
                cmbLoaiKH.DisplayMember = "TenLKH";
                cmbLoaiKH.ValueMember = "MaLKH";
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
