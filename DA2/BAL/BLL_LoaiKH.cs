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
    public class BLL_LoaiKH
    {
        DALayer db = null;
        public BLL_LoaiKH()
        {
            db = new DALayer();
        }
        public DataSet SelectLoaiKH()
        {
            return db.ExecuteQueryDataSet("spViewLoaiKH", CommandType.StoredProcedure, null);
        }
        //
        //Thêm loại khách hàng
        public bool InsertLoaiKH(ref string err, string MaLKH, string TenLKH)
        {
            return db.MyExecuteNonQuery("spInsertLoaiKH", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLKH", MaLKH),
                new SqlParameter("@TenLKH", TenLKH));
        }
        //
        //Xóa loại khách hàng
        public bool DeleteLoaiKH(ref string err, string MaLKH)
        {
            return db.MyExecuteNonQuery("spDeleteLoaiKH", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLKH", MaLKH));
        }
        //
        //Sửa loại khách hàng
        public bool UpdateLoaiKH(ref string err, string MaLKH, string TenLKH)
        {
            return db.MyExecuteNonQuery("spUpdateLoaiKH", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLKH", MaLKH),
                new SqlParameter("@TenLKH", TenLKH));
        }
    }
}
