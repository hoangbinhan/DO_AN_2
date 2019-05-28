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
namespace DOAN_HQTCSDL
{
    public partial class frmThongKe : Form
    {
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
        DataTable dt = null;
        DataTable dtdgv = null;
        BAL_LoaiSP dbLoaiSP = null;
        BLL_ThongKe dbThongKe = null;
        decimal tien = 0;
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void lblTKTLSP_MouseHover(object sender, EventArgs e)
        {
            lblTKTLSP.Font = new Font(lblTKTLSP.Font, FontStyle.Underline);
        }

        private void lblTKTThang_MouseHover(object sender, EventArgs e)
        {
            lblTKTThang.Font = new Font(lblTKTThang.Font, FontStyle.Underline);
        }

        private void lblTongDT_MouseHover(object sender, EventArgs e)
        {
            lblTongDT.Font = new Font(lblTongDT.Font, FontStyle.Underline);
        }

        private void lblTKTLSP_MouseLeave(object sender, EventArgs e)
        {
            lblTKTLSP.Font = new Font(lblTKTLSP.Font, FontStyle.Regular);
        }

        private void lblTKTThang_MouseLeave(object sender, EventArgs e)
        {
            lblTKTThang.Font = new Font(lblTKTThang.Font, FontStyle.Regular);
        }

        private void lblTongDT_MouseLeave(object sender, EventArgs e)
        {
            lblTongDT.Font = new Font(lblTongDT.Font, FontStyle.Regular);
        }
        //
        private void combobox_selectLSP(object sender, EventArgs e)
        {
            try
            {
                string err = "";
                dbThongKe = new BLL_ThongKe();
                dt = new DataTable();
                dt.Clear();
                dt = dbThongKe.getThongKeTheoLSP(ref err, cmbchange.SelectedValue.ToString()).Tables[0];
                dgvThongKe.DataSource = dt;
                this.dgvThongKe.Columns["DonGia"].DefaultCellStyle.Format = "###,###,###,### VNĐ";
                //
                int tongtien = 0;
                //DataTable test = new DataTable();
                //test=dbThongKe.DoanhThuTheoLSP(ref err, cmbchange.SelectedValue.ToString()).Tables[0];                                
                tongtien = dbThongKe.DoanhThuTheoLSP(ref err, cmbchange.SelectedValue.ToString());

                lblDoanhThu.Text=tongtien.ToString();

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(lblDoanhThu.Text, System.Globalization.NumberStyles.AllowThousands);
                lblDoanhThu.Text = String.Format(culture, "{0:N0}", value)+" VNĐ";
                
            }
            catch { }
        }
        //
        private void combobox_selectTheoThang(object sender, EventArgs e)
        {
            try
            {
                int tongtien = 0;
                
                string err = "";
                dbThongKe = new BLL_ThongKe();
                dt = new DataTable();
                dt.Clear();
                dt = dbThongKe.getThongKeTheoThang(ref err, Convert.ToInt32(cmbchange.SelectedValue)).Tables[0];
                dgvThongKe.DataSource = dt;
                this.dgvThongKe.Columns["DonGia"].DefaultCellStyle.Format = "###,###,###,### VNĐ";
                
                tongtien = dbThongKe.DoanhThuTheoThang(ref err, Convert.ToInt32(cmbchange.SelectedValue));
                lblDoanhThu.Text = tongtien.ToString() ;

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(lblDoanhThu.Text, System.Globalization.NumberStyles.AllowThousands);
                lblDoanhThu.Text = String.Format(culture, "{0:N0}", value) + " VNĐ";
            }
            catch { }
        }
        //

        private void lblTKTLSP_Click(object sender, EventArgs e)
        {
            cmbchange.Show();
            string err = "";
            DataTable dt = new DataTable();          
            try
            {
                dbLoaiSP = new BAL_LoaiSP();
                dt = new DataTable();
                dt.Clear();
                dt = dbLoaiSP.SelectLoaiSP().Tables[0];
                cmbchange.DataSource = dt;
                cmbchange.DisplayMember = "TenLSP";
                cmbchange.ValueMember = "MaLSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu!!!/n" + ex, "Thông báo ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmbchange.SelectedIndexChanged += new System.EventHandler(this.combobox_selectTheoThang);
            cmbchange.SelectedIndexChanged += new System.EventHandler(this.combobox_selectLSP);
          
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
           
        }

        private void lblTKTThang_Click(object sender, EventArgs e)
        {
            cmbchange.Show();
            DataTable dt = new DataTable();
            dt.Columns.Add("TenThang");
            dt.Columns.Add("Thang");
            for (int i = 1; i <= 12; i++)
            {
                string[] ob = { "Tháng " + i.ToString(),i.ToString() };
                dt.Rows.Add(ob);
            }
            cmbchange.DataSource = dt;
            cmbchange.DisplayMember = "TenThang";
            cmbchange.ValueMember = "Thang";
            cmbchange.SelectedIndexChanged += new System.EventHandler(this.combobox_selectLSP);
            cmbchange.SelectedIndexChanged += new System.EventHandler(this.combobox_selectTheoThang);
        
        }

        private void lblTongDT_Click(object sender, EventArgs e)
        {
            try
            {
                cmbchange.Hide();
                string err = "";
                dbThongKe = new BLL_ThongKe();
                dt = new DataTable();
                dt.Clear();
                dt = dbThongKe.getTongThongKe(ref err).Tables[0];
                dgvThongKe.DataSource = dt;
                this.dgvThongKe.Columns["DonGia"].DefaultCellStyle.Format = "###,###,###,### VNĐ";
                //
                int tongtien = 0;
                //DataTable test = new DataTable();
                //test = dbThongKe.DoanhThuTheoLSP(ref err, cmbchange.SelectedValue.ToString()).Tables[0];
                tongtien = dbThongKe.TongDoanhThu(ref err);
                lblDoanhThu.Text = tongtien.ToString() ;

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(lblDoanhThu.Text, System.Globalization.NumberStyles.AllowThousands);
                lblDoanhThu.Text = String.Format(culture, "{0:N0}", value) + " VNĐ";
            }
            catch { }
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmM = new frmMain();
            frmM.ShowDialog();
            this.Close();
        }

        private void cmbchange_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void lblDoanhThu_Click(object sender, EventArgs e)
        {

        }
    }
}
