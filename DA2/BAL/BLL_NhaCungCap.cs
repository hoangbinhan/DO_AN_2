using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BAL
{
    public class BAL_NhaCungCap
    {
        DALayer db = null;
        public BAL_NhaCungCap()
        {
            db = new DALayer();
        }

        public DataSet SearchKhachHang(ref string err, string search)
        {
            return db.ExecuteNonQueryDataSet("spSearchKhachHang", CommandType.StoredProcedure, ref err, new SqlParameter("@search", search));
        }
        //
        //
        public DataSet SelectNhaCungCap()
        {
            return db.ExecuteQueryDataSet("spViewNhaCungCap", CommandType.StoredProcedure, null);
        }

        //
        //Thêm khách hàng
        public bool InsertNhaCungCap(ref string err, string MaNCC, string TenNCC, string DiaChi, byte[] Picture)
        {
            return db.MyExecuteNonQuery("spInsertNhaCungCap", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNCC", MaNCC),
                new SqlParameter("@TenNCC", TenNCC),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@Picture", Picture)
               );
        }
        //
        //Xóa khách hàng
        public bool DeleteNhaCungCap(ref string err, string MaNCC)
        {
            return db.MyExecuteNonQuery("spDeleteNhaCungCap", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNCC", MaNCC));
        }
        //
        //Sửa loại sản phẩm
        public bool UpdateNhaCungCap(ref string err, string MaNCC, string TenNCC, string DiaChi, byte[] Picture)
        {
            return db.MyExecuteNonQuery("spUpdateNhaCungCap", CommandType.StoredProcedure, ref err,
               new SqlParameter("@MaNCC", MaNCC),
               new SqlParameter("@TenNCC", TenNCC),
               new SqlParameter("@DiaChi", DiaChi),
               new SqlParameter("@Picture", Picture)
              );
        }
        //
        public DataSet SearchNhaCungCap(ref string err, string search)
        {
            return db.ExecuteNonQueryDataSet("spSearchNhaCungCap", CommandType.StoredProcedure, ref err, new SqlParameter("@search", search));
        }

    }
}
