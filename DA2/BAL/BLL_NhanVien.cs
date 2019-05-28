using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using System.IO;

namespace BAL
{
    public class BLL_NhanVien
    {
        DALayer db = null;
        public BLL_NhanVien()
        {
            db = new DALayer();
        }

        public DataSet SearchNhanVien(ref string err, string search)
        {
            return db.ExecuteNonQueryDataSet("spSearchNhanVien", CommandType.StoredProcedure, ref err, new SqlParameter("@search", search));
        }
        //
        //
        public DataSet SelectNhanVien()
        {
            return db.ExecuteQueryDataSet("spViewNhanVien", CommandType.StoredProcedure, null);
        }
        public bool UpdateSanPhamSauKhiMua(ref string err, string MaSP,int SoLuong)
        {
            return db.MyExecuteNonQuery("spUpdateSoLuong", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaSP", MaSP),
                new SqlParameter("@SoLuong", SoLuong));
        }
        //
        //Thêm sản phẩm
        public bool InsertNhanVien(ref string err, string MaNV, string HoTen, string GioiTinh,
             string SDT, string DiaChi, DateTime NgaySinh, byte[] Picture)
        {
            return db.MyExecuteNonQuery("spInsertNhanVien", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNV", MaNV),
                new SqlParameter("@HoTen", HoTen),
                new SqlParameter("@GioiTinh", GioiTinh),
                new SqlParameter("@SDT", SDT),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@NgaySinh", NgaySinh),
                new SqlParameter("@Picture", Picture));
        }
        //
        //Xóa khách hàng
        public bool DeleteNhanVien(ref string err, string MaNV)
        {
            return db.MyExecuteNonQuery("spDeleteNhanVien", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNV", MaNV));
        }
        //
        //Sửa loại sản phẩm
        public bool UpdateNhanVien(ref string err, string MaNV, string HoTen, string GioiTinh,
             string SDT, string DiaChi, DateTime NgaySinh, byte[] Picture)
        {
             return db.MyExecuteNonQuery("spUpdateNhanVien", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNV", MaNV),
                new SqlParameter("@HoTen", HoTen),
                new SqlParameter("@GioiTinh", GioiTinh),
                new SqlParameter("@SDT", SDT),
                new SqlParameter("@DiaChi", DiaChi),
                new SqlParameter("@NgaySinh", NgaySinh),
                new SqlParameter("@Picture", Picture));
        }

    }
}
