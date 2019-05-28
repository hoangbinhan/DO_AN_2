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
using System.IO;
using System.Text.RegularExpressions;

namespace DOAN_HQTCSDL
{
    public partial class frmNhanVien : Form
    {
        BLL_NhanVien dbNhanVien = null; //khởi tạo đối tượng loại sản phẩm
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
        public frmNhanVien()
        {
            InitializeComponent();
        }
        public void LoadData()  //load dữ liệu
        {
            try
            {
                dbNhanVien = new BLL_NhanVien();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhanVien.SelectNhanVien().Tables[0];
                dgvNhanVien.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            gpNhanVien.Enabled = false;
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();
            rdNam.Checked = false;
            rdNu.Checked = false;
            PictureBoxNV.Image = null;
        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            gpNhanVien.Enabled = true;
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();
            rdNam.Checked = false;
            rdNu.Checked = false;
            txtMaNV.Enabled = true;
            txtMaNV.Focus();
            PictureBoxNV.Image = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            dgvNhanVien_CellClick(null, null);
            gpNhanVien.Enabled = true;          
            txtMaNV.Enabled = false;
            txtTenNV.Focus();
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
                    int r = dgvNhanVien.CurrentCell.RowIndex;
                    //Lấy MaPhim của record hiện hành
                    string MaNV = txtMaNV.Text;

                    //Khai báo biến
                    bool f = false;
                    string err = "";
                    //Gọi Strored Procedure để Update data
                    f = dbNhanVien.DeleteNhanVien(ref err,MaNV);
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

        //check sdt
        public static bool IsValidPhone(string value)
        {
            string pattern = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
            return Regex.IsMatch(value, pattern);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (IsValidPhone(txtSDT.Text) == false)
            {
                MessageBox.Show("SDT không hợp lệ");
            }
            else
            {
                if (them == true)
                {
                    bool f = false; // biến cờ
                    string err = "";
                    byte[] picture = null;
                    try
                    {
                        MemoryStream stream = null;
                        try
                        {
                            stream = new MemoryStream();
                            //pictureBoxNhaCC.Image.Save(stream, ImageFormat.Jpeg);
                            PictureBoxNV.Image.Save(stream, PictureBoxNV.Image.RawFormat);
                            //picture = stream.ToArray();
                            picture = stream.GetBuffer();
                            stream.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //Thực hiện câu lệnh sql
                        //Gọi Strored Procedure để Insert data
                        string gioitinh;
                        if (rdNam.Checked == true)
                            gioitinh = "Nam";
                        else
                            gioitinh = "Nữ";

                        f = dbNhanVien.InsertNhanVien(ref err, txtMaNV.Text, txtTenNV.Text, gioitinh, txtSDT.Text, txtDiaChi.Text, DateTimeNgaySinh.Value, picture);
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
                    byte[] picture = null;
                    bool f = false; //biến cờ
                    string err = "";
                    try
                    {
                        MemoryStream stream = null;
                        try
                        {
                            stream = new MemoryStream();
                            //pictureBoxNhaCC.Image.Save(stream, ImageFormat.Jpeg);
                            PictureBoxNV.Image.Save(stream, PictureBoxNV.Image.RawFormat);
                            //picture = stream.ToArray();
                            picture = stream.GetBuffer();
                            stream.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //Thực hiện câu lệnh sql
                        //Gọi Strored Procedure để Insert data
                        string gioitinh;
                        if (rdNam.Checked == true)
                            gioitinh = "Nam";
                        else
                            gioitinh = "Nữ";

                        f = dbNhanVien.UpdateNhanVien(ref err, txtMaNV.Text, txtTenNV.Text, gioitinh, txtSDT.Text, txtDiaChi.Text, DateTimeNgaySinh.Value, picture);

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
             MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvNhanVien.CurrentCell.RowIndex;
                txtMaNV.Text = dgvNhanVien.Rows[r].Cells[0].Value.ToString();
                txtTenNV.Text = dgvNhanVien.Rows[r].Cells[1].Value.ToString();
                string gioitinh = dgvNhanVien.Rows[r].Cells[2].Value.ToString();
                if (gioitinh == "Nam")
                    rdNam.Checked = true;
                else
                    rdNu.Checked = true;
                txtSDT.Text = dgvNhanVien.Rows[r].Cells[3].Value.ToString();
                txtDiaChi.Text = dgvNhanVien.Rows[r].Cells[4].Value.ToString();
                DateTimeNgaySinh.Value = Convert.ToDateTime(dgvNhanVien.Rows[r].Cells[5].Value.ToString());
                try
                {
                    ImageConverter Iconver = new ImageConverter();
                    PictureBoxNV.Image = (Image)Iconver.ConvertFrom(dgvNhanVien.Rows[r].Cells[6].Value);
                }
                catch
                {
                    PictureBoxNV.Image = null;
                }
            }
            catch  { }
        }

        private void btnAddPick_Click(object sender, EventArgs e)
        {
            try
            {
                ofdOpenfile.ShowDialog();
                string file = ofdOpenfile.FileName;
                if (string.IsNullOrEmpty(file))
                    return;
                Image myImage = Image.FromFile(file);
                PictureBoxNV.Image = myImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadDataKeyWord()
        {
            //đổ dữ liệu lên datagridview
            try
            {
                dbNhanVien = new BLL_NhanVien();
                dt = new DataTable();
                dt.Clear();
                string err = "";
                dt = dbNhanVien.SearchNhanVien(ref err, txtSearch.Text.ToString()).Tables[0];
                dgvNhanVien.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            
            //
            //
            gpNhanVien.Enabled = false;
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();
            PictureBoxNV.Image = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataKeyWord();
        }
    }
}
