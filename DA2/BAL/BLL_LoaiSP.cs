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
    public class BAL_LoaiSP
    {
        DALayer db = null;
        public BAL_LoaiSP()
        {
            db = new DALayer();
        }
        public DataSet SelectLoaiSP()
        {
            return db.ExecuteQueryDataSet("spViewLoaiSP", CommandType.StoredProcedure, null);
        }
        //
        //Thêm loại sản phẩm
        public bool InsertLoaiSP(ref string err,string MaLSP,string TenLSP)
        {
            return db.MyExecuteNonQuery("spInsertLoaiSP", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLSP", MaLSP),
                new SqlParameter("@TenLSP", TenLSP));
        }
        //
        //Xóa loại sản phẩm
        public bool DeleteLoaiSP(ref string err, string MaLSP)
        {
            return db.MyExecuteNonQuery("spDeleteLoaiSP", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLSP", MaLSP));
        }
        //
        //Sửa loại sản phẩm
        public bool UpdateLoaiSP(ref string err, string MaLSP, string TenLSP)
        {
            return db.MyExecuteNonQuery("spUpdateLoaiSP", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLSP", MaLSP),
                new SqlParameter("@TenLSP", TenLSP));
        }
    }
}
