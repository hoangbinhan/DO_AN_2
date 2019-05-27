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

namespace DOAN_HQTCSDL
{
    public partial class frmSanPham : Form
    {
        BAL_LoaiSP dbLoaiSP=null;
        BAL_NhaCungCap dbNhaCungCap = null;
        BLL_SanPham dbSanPham = null;       
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
        public frmSanPham()
        {
            InitializeComponent();
        }
        public void LoadData()  //load dữ liệu
        {
            try
            {
                dbSanPham = new BLL_SanPham ();
                dt = new DataTable();
                dt.Clear();
                dt = dbSanPham.SelectSanPham().Tables[0];
                dgvSanPham.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbLoaiSP = new BAL_LoaiSP();
                dt = new DataTable();
                dt.Clear();
                dt = dbLoaiSP.SelectLoaiSP().Tables[0];
                cmbLoaiSP.DataSource = dt;
                cmbLoaiSP.DisplayMember = "TenLSP";
                cmbLoaiSP.ValueMember = "MaLSP";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbNhaCungCap = new BAL_NhaCungCap();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhaCungCap.SelectNhaCungCap().Tables[0];
                cmbNhaCungCap.DataSource = dt;
                cmbNhaCungCap.DisplayMember = "TenNCC";
                cmbNhaCungCap.ValueMember = "MaNCC";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            gpSanPham.Enabled = false;
            txtMaSP.ResetText();
            txtTenSP.ResetText();
            txtThongSo.ResetText();
            txtGiaBan.ResetText();
            cmbLoaiSP.ResetText();
            txtSoLuong.ResetText();
            cmbNhaCungCap.ResetText();
            pictureBoxSP.Image = null;
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvSanPham.CurrentCell.RowIndex;
            txtMaSP.Text = dgvSanPham.Rows[r].Cells[0].Value.ToString();
            txtTenSP.Text = dgvSanPham.Rows[r].Cells[1].Value.ToString();
            txtThongSo.Text = dgvSanPham.Rows[r].Cells[2].Value.ToString();
            txtGiaBan.Text = dgvSanPham.Rows[r].Cells[3].Value.ToString();
            cmbLoaiSP.Text = dgvSanPham.Rows[r].Cells[4].Value.ToString();
            txtSoLuong.Text = dgvSanPham.Rows[r].Cells[5].Value.ToString();
            cmbNhaCungCap.Text = dgvSanPham.Rows[r].Cells[6].Value.ToString();
            try
            {
                ImageConverter Iconver = new ImageConverter();
                pictureBoxSP.Image = (Image)Iconver.ConvertFrom(dgvSanPham.Rows[r].Cells[7].Value);
            }
            catch
            {
                pictureBoxSP.Image = null;
            }


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            gpSanPham.Enabled = true;
            txtMaSP.ResetText();
            txtTenSP.ResetText();
            txtGiaBan.ResetText();
            txtSoLuong.ResetText();
            txtThongSo.ResetText();
            cmbLoaiSP.ResetText();
            cmbNhaCungCap.ResetText();         
            txtMaSP.Enabled = true;
            txtMaSP.Focus();
            pictureBoxSP.Image = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;

            gpSanPham.Enabled = true;          
            txtMaSP.Enabled = false;
            txtTenSP.Focus();
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
                    int r = dgvSanPham.CurrentCell.RowIndex;
                    //Lấy MaPhim của record hiện hành
                    string MaSP = txtMaSP.Text;

                    //Khai báo biến
                    bool f = false;
                    string err = "";
                    //Gọi Strored Procedure để Update data
                    f = dbSanPham.DeleteSanPham(ref err, MaSP);
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
                        pictureBoxSP.Image.Save(stream, pictureBoxSP.Image.RawFormat);
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

                    f = dbSanPham.InsertSanPham(ref err, txtMaSP.Text, txtTenSP.Text, txtThongSo.Text, Convert.ToDecimal(txtGiaBan.Text),
                       cmbLoaiSP.SelectedValue.ToString(),Convert.ToInt32(txtSoLuong.Text),picture,cmbNhaCungCap.SelectedValue.ToString() );
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
                        pictureBoxSP.Image.Save(stream, pictureBoxSP.Image.RawFormat);
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
                    f = dbSanPham.UpdateSanPham(ref err, txtMaSP.Text, txtTenSP.Text, txtThongSo.Text, Convert.ToDecimal(txtGiaBan.Text),
                        cmbLoaiSP.SelectedValue.ToString(), Convert.ToInt32(txtSoLuong.Text), picture, cmbNhaCungCap.SelectedValue.ToString());

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

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmM = new frmMain();
            frmM.ShowDialog();
            this.Close();
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

        private void btnAddPick_Click(object sender, EventArgs e)
        {
            try
            {
                ofdOpenfile.ShowDialog();
                string file = ofdOpenfile.FileName;
                if (string.IsNullOrEmpty(file))
                    return;
                Image myImage = Image.FromFile(file);
                pictureBoxSP.Image = myImage;

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
                dbSanPham = new BLL_SanPham();
                dt = new DataTable();
                dt.Clear();
                string err = "";
                dt = dbSanPham.SearchSanPham(ref err, txtSearch.Text.ToString()).Tables[0];
                dgvSanPham.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbLoaiSP = new BAL_LoaiSP();
                dt = new DataTable();
                dt.Clear();
                dt = dbLoaiSP.SelectLoaiSP().Tables[0];
                cmbLoaiSP.DataSource = dt;
                cmbLoaiSP.DisplayMember = "TenLSP";
                cmbLoaiSP.ValueMember = "MaLSP";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbNhaCungCap = new BAL_NhaCungCap();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhaCungCap.SelectNhaCungCap().Tables[0];
                cmbNhaCungCap.DataSource = dt;
                cmbNhaCungCap.DisplayMember = "TenNCC";
                cmbNhaCungCap.ValueMember = "MaNCC";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            //
            gpSanPham.Enabled = false;
            txtMaSP.ResetText();
            txtTenSP.ResetText();
            txtThongSo.ResetText();
            txtGiaBan.ResetText();
            cmbLoaiSP.ResetText();
            txtSoLuong.ResetText();
            cmbNhaCungCap.ResetText();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataKeyWord();
        }
    }
}
