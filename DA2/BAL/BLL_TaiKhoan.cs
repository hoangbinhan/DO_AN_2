using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BLL_TaiKhoan
    {
        DALayer db = null;
        public BLL_TaiKhoan()
        {
            db = new DALayer();
        }

        public DataSet SearchTaiKhoan(ref string err, string search)
        {
            return db.ExecuteNonQueryDataSet("spSearchTaiKhoan", CommandType.StoredProcedure, ref err, new SqlParameter("@search", search));
        }
        //
        //
        public DataSet SelectTaiKhoan()
        {
            return db.ExecuteQueryDataSet("spViewTaiKhoan", CommandType.StoredProcedure, null);
        }

        //
        //Thêm khách hàng
        public bool InsertTaiKhoan(ref string err, string MaNV, string TenTK, string Pass, string Quyen)
        {
            return db.MyExecuteNonQuery("spInsertTaiKhoan", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNV", MaNV),
                new SqlParameter("@TenTK", TenTK),
                new SqlParameter("@Pass", Pass),
                new SqlParameter("@Quyen", Quyen));
        }
        //
        //Xóa khách hàng
        public bool DeleteTaiKhoan(ref string err, string TenTK)
        {
            return db.MyExecuteNonQuery("spDeleteTaiKHoan", CommandType.StoredProcedure, ref err,
                new SqlParameter("@TenTK", TenTK));
        }
        //
        //Sửa loại sản phẩm
        public bool UpdateTaiKhoan(ref string err, string TenTK, string Pass)
        {
            return db.MyExecuteNonQuery("spUpdateTaiKhoan", CommandType.StoredProcedure, ref err,
                new SqlParameter("@TenTK", TenTK),
                new SqlParameter("@Pass", Pass));
                
        }

    }
}
