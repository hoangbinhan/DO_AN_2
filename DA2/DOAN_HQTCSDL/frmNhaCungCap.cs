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
using System.Drawing.Imaging;

namespace DOAN_HQTCSDL
{
    public partial class frmNhaCungCap : Form
    {
        BAL_NhaCungCap dbNhaCungCap = null; //khởi tạo đối tượng loại sản phẩm
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
        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        public void LoadData()  //load dữ liệu
        {
            try
            {
                dbNhaCungCap = new BAL_NhaCungCap();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhaCungCap.SelectNhaCungCap().Tables[0];
                dgvNhaCungCap.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            gpNhaCungCap.Enabled = false;
            txtMaNhaCungCap.ResetText();
            txtTenNhaCungCap.ResetText();
            txtDiaChi.ResetText();
            pictureBoxNhaCC.Image = null;
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string err = null;
            int r = dgvNhaCungCap.CurrentCell.RowIndex;
            txtMaNhaCungCap.Text = dgvNhaCungCap.Rows[r].Cells[0].Value.ToString();
            txtTenNhaCungCap.Text = dgvNhaCungCap.Rows[r].Cells[1].Value.ToString();
            txtDiaChi.Text = dgvNhaCungCap.Rows[r].Cells[2].Value.ToString();
            //dbNhaCungCap.GetPicture(ref err, txtMaNhaCungCap.Text);
            try
            {
                ImageConverter Iconver = new ImageConverter();
                pictureBoxNhaCC.Image = (Image)Iconver.ConvertFrom(dgvNhaCungCap.Rows[r].Cells[3].Value); 
                }
            catch
            {
                pictureBoxNhaCC.Image = null;
            }

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
                pictureBoxNhaCC.Image = myImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmM = new frmMain();
            frmM.ShowDialog();
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            gpNhaCungCap.Enabled = true;
            txtMaNhaCungCap.Enabled = true;
            txtTenNhaCungCap.ResetText();
            txtMaNhaCungCap.ResetText();
            txtDiaChi.ResetText();
            txtMaNhaCungCap.Focus();
            pictureBoxNhaCC.Image = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            gpNhaCungCap.Enabled = true;
            txtMaNhaCungCap.Enabled = false;
            txtTenNhaCungCap.Focus();
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
                    int r = dgvNhaCungCap.CurrentCell.RowIndex;
                    //Lấy MaPhim của record hiện hành
                    string MaNCC = txtMaNhaCungCap.Text;

                    //Khai báo biến
                    bool f = false;
                    string err = "";
                    //Gọi Strored Procedure để Update data
                    f = dbNhaCungCap.DeleteNhaCungCap(ref err, MaNCC);
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
           MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void btnLuu_Click(object sender, EventArgs e)
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
                        pictureBoxNhaCC.Image.Save(stream, pictureBoxNhaCC.Image.RawFormat);
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
                    f = dbNhaCungCap.InsertNhaCungCap(ref err, txtMaNhaCungCap.Text, txtTenNhaCungCap.Text,txtDiaChi.Text,picture);
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
                        pictureBoxNhaCC.Image.Save(stream, pictureBoxNhaCC.Image.RawFormat);
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
                    f = dbNhaCungCap.UpdateNhaCungCap(ref err, txtMaNhaCungCap.Text, txtTenNhaCungCap.Text, txtDiaChi.Text, picture);

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

        public void LoadDataKeyWord()
        {
            //đổ dữ liệu lên datagridview
            try
            {
                dbNhaCungCap = new BAL_NhaCungCap();
                dt = new DataTable();
                dt.Clear();
                string err = "";
                dt = dbNhaCungCap.SearchNhaCungCap(ref err, txtSearch.Text.ToString()).Tables[0];
                dgvNhaCungCap.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //            
            //

            gpNhaCungCap.Enabled = false;
            txtDiaChi.ResetText();
            txtMaNhaCungCap.ResetText();
            txtTenNhaCungCap.ResetText();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataKeyWord();
        }
    }
}
