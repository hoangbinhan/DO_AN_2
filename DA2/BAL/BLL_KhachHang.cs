using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;
namespace BAL
{
    public class BLL_KhachHang
    {
        DALayer db = null;
        public BLL_KhachHang()
        {
            db = new DALayer();
        }

        public DataSet SearchKhachHang(ref string err,string search)
        {
            return db.ExecuteNonQueryDataSet("spSearchKhachHang", CommandType.StoredProcedure, ref err, new SqlParameter("@search", search));         
        }
        //
        //
        public DataSet SelectKhachHang()
        {
            return db.ExecuteQueryDataSet("spViewKhachHang", CommandType.StoredProcedure, null);
        }

        //
        //Thêm khách hàng
        public bool InsertKhachHang(ref string err, string MaKH,string MaLKH,string TenKH,string SDT,string DiaChi,string GioiTinh,string Email)
        {
            return db.MyExecuteNonQuery("spInsertKhachHang", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaKH", MaKH),
                new SqlParameter("@MaLKH", MaLKH),
                new SqlParameter("@TenKH", TenKH),
                new SqlParameter("@SDT", SDT),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@GioiTinh", GioiTinh),
                new SqlParameter("@Email", Email));
        }
        //
        //Xóa khách hàng
        public bool DeleteKhachHang(ref string err, string MaKH)
        {
            return db.MyExecuteNonQuery("spDeleteKhachHang", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaKH", MaKH));
        }
        //
        //Sửa loại sản phẩm
        public bool UpdateKhachHang(ref string err, string MaKH, string MaLKH, string TenKH, string SDT, string DiaChi, string GioiTinh, string Email)
        {
            return db.MyExecuteNonQuery("spUpdateKhachHang", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaKH", MaKH),
                new SqlParameter("@MaLKH", MaLKH),
                new SqlParameter("@TenKH", TenKH),
                new SqlParameter("@SDT", SDT),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@GioiTinh", GioiTinh),
                new SqlParameter("@Email", Email));
        }


    }
}
