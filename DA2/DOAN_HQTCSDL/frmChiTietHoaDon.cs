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
    public partial class frmChiTietHoaDon : Form
    {

        BAL_DanhMucChiTietHoaDon dbDanhMucChiTietHoaDon = null;
        BAL_NhaCungCap dbNhaCungCap = null;
        BLL_SanPham dbSanPham = null;
        BLL_NhanVien dbNhanVien = null;
        BLL_KhachHang dbKhachHang = null;
        BAL_LoaiSP dbLoaiSP = null;
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
        public frmChiTietHoaDon()
        {
            InitializeComponent();
        }
        public void LoadData()  //load dữ liệu
        {
            try
            {
                dbSanPham = new BLL_SanPham();
                dt = new DataTable();
                dt.Clear();
                dt = dbSanPham.SelectChonSanPham().Tables[0];
                dgvChonSanPham.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbDanhMucChiTietHoaDon = new BAL_DanhMucChiTietHoaDon();
                dt = new DataTable();
                dt.Clear();
                dt = dbDanhMucChiTietHoaDon.SelectDanhMucChiTietHoaDon().Tables[0];
                dgvChiTietHoaDon.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbNhanVien = new BLL_NhanVien();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhanVien.SelectNhanVien().Tables[0];
                cmbNguoiLapHD.DataSource = dt;
                cmbNguoiLapHD.DisplayMember = "HoTen";
                cmbNguoiLapHD.ValueMember = "MaNV";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbKhachHang = new BLL_KhachHang();
                dt = new DataTable();
                dt.Clear();
                dt = dbKhachHang.SelectKhachHang().Tables[0];
                cmbKhachHang.DataSource = dt;
                cmbKhachHang.DisplayMember = "TenKH";
                cmbKhachHang.ValueMember = "MaKH";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            lblDonGia.Visible = false;
            label4.Visible = false;
            panelCTHD.Enabled = false;
            dgvChonSanPham.Enabled = true;
            dgvChiTietHoaDon.Enabled = true;
        }
        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvChiTietHoaDon.CurrentCell.RowIndex;
                txtMaHD.Text = dgvChiTietHoaDon.Rows[r].Cells[0].Value.ToString();
                nupSoLuong.Value = Convert.ToInt32(dgvChiTietHoaDon.Rows[r].Cells[4].Value.ToString());
                datetimeNgayLapHD.Text = dgvChiTietHoaDon.Rows[r].Cells[6].Value.ToString();
                cmbKhachHang.Text = dgvChiTietHoaDon.Rows[r].Cells[3].Value.ToString();
                cmbNguoiLapHD.Text = dgvChiTietHoaDon.Rows[r].Cells[7].Value.ToString();
                try
                {
                    ImageConverter Iconver = new ImageConverter();
                    pictureSanPham.Image = (Image)Iconver.ConvertFrom(dgvChiTietHoaDon.Rows[r].Cells[8].Value);
                }
                catch
                {
                    pictureSanPham.Image = null;
                }
            }
            catch
            {
               

            }
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat =
              MessageBox.Show("Bạn có muốn thoát không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes) Application.Exit();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //
            cmbKhachHang.Enabled = true;
            them = true;
            panelCTHD.Enabled = true;
            txtMaHD.Enabled = true;
            txtMaHD.ResetText();
            nupSoLuong.Value = 1;
            cmbNguoiLapHD.ResetText();
            cmbKhachHang.ResetText();
            datetimeNgayLapHD.Value = DateTime.Today;
            lblDonGia.Visible = true;
            label4.Visible = true;
            dgvChiTietHoaDon.Enabled = false;
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
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            them = false;
            cmbKhachHang.Enabled = false;
            panelCTHD.Enabled = true;
            txtMaHD.Enabled = false;
            lblDonGia.Visible = true;
            label4.Visible = true;
        }

        private void dgvChonSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvChonSanPham.CurrentCell.RowIndex;
                try
                {
                    ImageConverter Iconver = new ImageConverter();
                    pictureSanPham.Image = (Image)Iconver.ConvertFrom(dgvChonSanPham.Rows[r].Cells[8].Value);
                }
                catch
                {
                    pictureSanPham.Image = null;
                }
               
                lblMaSP.Text = dgvChonSanPham.Rows[r].Cells[1].Value.ToString();
                lblDonGia.Text = string.Format("{0:0,0 VND}", Convert.ToDecimal(dgvChonSanPham.Rows[r].Cells[4].Value) * Convert.ToDecimal(nupSoLuong.Value));
            }
            catch
            {

            }
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
                    int r = dgvChiTietHoaDon.CurrentCell.RowIndex;
                    //Lấy MaPhim của record hiện hành
                    string MaHD = txtMaHD.Text;
                    //Khai báo biến
                    bool f = false;
                    string err = "";
                    //Gọi Strored Procedure để Update data
                    f = dbDanhMucChiTietHoaDon.DeleteHoaDon(ref err, MaHD);
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
                bool f2 = false;
                bool f3 = false;
                string err = "";
                int r = dgvChonSanPham.CurrentCell.RowIndex;
                int soluongconlai =Convert.ToInt32( dgvChonSanPham.Rows[r].Cells[6].Value.ToString());
                if((soluongconlai- Convert.ToInt32(nupSoLuong.Value)) >= 0)
                    {
                    try
                    {

                        f = dbDanhMucChiTietHoaDon.InsertHoaDon(ref err, txtMaHD.Text, Convert.ToDateTime(datetimeNgayLapHD.Value)
                            , cmbNguoiLapHD.SelectedValue.ToString(), cmbKhachHang.SelectedValue.ToString());
                        //Kiểm tra để thông báo
                        if (f == true)
                        {
                            try
                            {
                                r = dgvChonSanPham.CurrentCell.RowIndex;
                                f2 = dbDanhMucChiTietHoaDon.InsertChiTietHoaDon(ref err, txtMaHD.Text
                                    , dgvChonSanPham.Rows[r].Cells[1].Value.ToString(), Convert.ToInt32(nupSoLuong.Value));
                                if (f2 == true)
                                {
                                    f3 = dbDanhMucChiTietHoaDon.UpdateSoLuongSanPham(ref err, dgvChonSanPham.Rows[r].Cells[1].Value.ToString(),
                                        Convert.ToInt32(nupSoLuong.Value));
                                    if (f3 == true)
                                    {
                                        MessageBox.Show("Thêm thành công!\n");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error:" + err);
                                    }
                                }
                                else
                                    MessageBox.Show("Error:" + err);
                            }
                            catch (SqlException)
                            {
                                MessageBox.Show("Thêm lỗi!!!", "Thông báo",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        else
                            MessageBox.Show("Error:" + err + cmbNguoiLapHD.SelectedValue.ToString());
                    }

                    catch (SqlException)
                    {
                        MessageBox.Show("Thêm lỗi!!!", "Thông báo",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sản phẩm trong kho không đủ!\n");
                }
              
                //Sau khi thêm xong Load lại data
                LoadData();
                //}
            }
            else
            {
                bool f = false; // biến cờ
                bool f2 = false;
                string err = "";
                
                try
                {

                    f = dbDanhMucChiTietHoaDon.UpdateHoaDon(ref err, txtMaHD.Text, Convert.ToDateTime(datetimeNgayLapHD.Value)
                        , cmbNguoiLapHD.SelectedValue.ToString(), cmbKhachHang.SelectedValue.ToString());
                    //Kiểm tra để thông báo
                    if (f == true)
                    {
                        try
                        {
                            int r = dgvChonSanPham.CurrentCell.RowIndex;
                            f2 = dbDanhMucChiTietHoaDon.UpdateChiTietHoaDon(ref err, txtMaHD.Text
                                , dgvChonSanPham.Rows[r].Cells[1].Value.ToString(), Convert.ToInt32(nupSoLuong.Value));
                            if (f2 == true)
                            {
                                MessageBox.Show("Sửa thành công!\n");
                            }
                            else
                                MessageBox.Show("Error:" + err);
                        }
                        catch (SqlException)
                        {
                            MessageBox.Show("Sửa lỗi!!!", "Thông báo",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                        MessageBox.Show("Error:" + err + cmbNguoiLapHD.SelectedValue.ToString());
                }

                catch (SqlException)
                {
                    MessageBox.Show("Sửa lỗi!!!", "Thông báo",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Sau khi thêm xong Load lại data
                LoadData();
                //}
            }
        }

        private void nupSoLuong_ValueChanged(object sender, EventArgs e)
        {
            int r = dgvChonSanPham.CurrentCell.RowIndex;
            
            lblDonGia.Text = string.Format("{0:0,0 VND}", Convert.ToDecimal(dgvChonSanPham.Rows[r].Cells[4].Value) * Convert.ToDecimal(nupSoLuong.Value));
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmM = new frmMain();
            frmM.ShowDialog();
            this.Close();
        }

        private void nupSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                int r = dgvChonSanPham.CurrentCell.RowIndex;

                lblDonGia.Text = string.Format("{0:0,0 VND}", Convert.ToDecimal(dgvChonSanPham.Rows[r].Cells[4].Value) * Convert.ToDecimal(nupSoLuong.Value));
            }
        }

        private void cmbLoaiSP_SelectedValueChanged(object sender, EventArgs e)
        {          
            //try
            //{
            //    string err = "";
            //    dbDanhMucChiTietHoaDon = new BAL_DanhMucChiTietHoaDon();
            //    dt = new DataTable();
            //    dt.Clear();
            //    dt = dbDanhMucChiTietHoaDon.LocSanPham(ref err, cmbLoaiSP.SelectedValue.ToString()).Tables[0];
            //    dgvChonSanPham.DataSource = dt;
            //    dgvChonSanPham_CellClick(null, null);
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //try
            //{
            //    dbDanhMucChiTietHoaDon = new BAL_DanhMucChiTietHoaDon();
            //    dt = new DataTable();
            //    dt.Clear();
            //    dt = dbDanhMucChiTietHoaDon.SelectDanhMucChiTietHoaDon().Tables[0];
            //    dgvChiTietHoaDon.DataSource = dt;
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            ////
            //try
            //{
            //    dbNhanVien = new BAL_NhanVien();
            //    dt = new DataTable();
            //    dt.Clear();
            //    dt = dbNhanVien.SelectNhanVien().Tables[0];
            //    cmbNguoiLapHD.DataSource = dt;
            //    cmbNguoiLapHD.DisplayMember = "HoTen";
            //    cmbNguoiLapHD.ValueMember = "MaNV";
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            ////
            //try
            //{
            //    dbKhachHang = new BAL_KhachHang();
            //    dt = new DataTable();
            //    dt.Clear();
            //    dt = dbKhachHang.SelectKhachHang().Tables[0];
            //    cmbKhachHang.DataSource = dt;
            //    cmbKhachHang.DisplayMember = "TenKH";
            //    cmbKhachHang.ValueMember = "MaKH";
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //
            
        }
        public void LoadDataKeyWord()
        {
            try
            {
                dbSanPham = new BLL_SanPham();
                dt = new DataTable();
                dt.Clear();
                dt = dbSanPham.SelectChonSanPham().Tables[0];
                dgvChonSanPham.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                string err = "";
                dbDanhMucChiTietHoaDon = new BAL_DanhMucChiTietHoaDon();
                dt = new DataTable();
                dt.Clear();
                dt = dbDanhMucChiTietHoaDon.SearchDanhMucChiTietHoaDon(ref err, txtSearch.Text).Tables[0];
                dgvChiTietHoaDon.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbNhanVien = new BLL_NhanVien();
                dt = new DataTable();
                dt.Clear();
                dt = dbNhanVien.SelectNhanVien().Tables[0];
                cmbNguoiLapHD.DataSource = dt;
                cmbNguoiLapHD.DisplayMember = "HoTen";
                cmbNguoiLapHD.ValueMember = "MaNV";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            try
            {
                dbKhachHang = new BLL_KhachHang();
                dt = new DataTable();
                dt.Clear();
                dt = dbKhachHang.SelectKhachHang().Tables[0];
                cmbKhachHang.DataSource = dt;
                cmbKhachHang.DisplayMember = "TenKH";
                cmbKhachHang.ValueMember = "MaKH";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //
            lblDonGia.Visible = false;
            label4.Visible = false;
            panelCTHD.Enabled = false;
            dgvChonSanPham.Enabled = true;
            dgvChiTietHoaDon.Enabled = true;
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataKeyWord();
        }
    }
}
