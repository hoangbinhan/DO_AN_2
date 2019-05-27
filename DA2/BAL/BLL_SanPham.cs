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
    public class BLL_SanPham
    {
        DALayer db = null;
        public BLL_SanPham()
        {
            db = new DALayer();
        }

        public DataSet SearchSanPham(ref string err, string search)
        {
            return db.ExecuteNonQueryDataSet("spSearchSanPham", CommandType.StoredProcedure, ref err, new SqlParameter("@search", search));
        }
        //
        //
        public DataSet SelectSanPham()
        {
            return db.ExecuteQueryDataSet("spViewSanPham", CommandType.StoredProcedure, null);
        }
        //
        public DataSet SelectChonSanPham()
        {
            return db.ExecuteQueryDataSet("dbo.spViewChonSanPham", CommandType.StoredProcedure, null);
        }
        //
        //Thêm sản phẩm
        public bool InsertSanPham(ref string err, string MaSP, string TenSP, string ThongTin, 
            Decimal GiaBan, string MaLoaiSP, int SoLuongTrongKho, byte[] Picture,string NhaCungCap)
        {
            return db.MyExecuteNonQuery("spInsertSanPham", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaSP", MaSP),
                new SqlParameter("@TenSP", TenSP),
                new SqlParameter("@ThongTin", ThongTin),
                new SqlParameter("@GiaBan", GiaBan),
                new SqlParameter("@MaLoaiSP", MaLoaiSP),
                new SqlParameter("@SoLuongTrongKho", SoLuongTrongKho),
                new SqlParameter("@Picture", Picture),
                new SqlParameter("@NhaCungCap", NhaCungCap));
        }
        //
        //Xóa khách hàng
        public bool DeleteSanPham(ref string err, string MaSP)
        {
            return db.MyExecuteNonQuery("spDeleteSanPham", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaSP", MaSP));
        }
        //
        //Sửa loại sản phẩm
        public bool UpdateSanPham(ref string err, string MaSP, string TenSP, string ThongTin,
            Decimal GiaBan, string MaLoaiSP, int SoLuongTrongKho, byte[] Picture, string NhaCungCap)
        {
            return db.MyExecuteNonQuery("spUpdateSanPham", CommandType.StoredProcedure, ref err,
               new SqlParameter("@MaSP", MaSP),
               new SqlParameter("@TenSP", TenSP),
               new SqlParameter("@ThongTin", ThongTin),
               new SqlParameter("@GiaBan", GiaBan),
               new SqlParameter("@MaLoaiSP", MaLoaiSP),
               new SqlParameter("@SoLuongTrongKho", SoLuongTrongKho),
               new SqlParameter("@Picture", Picture),
               new SqlParameter("@NhaCungCap", NhaCungCap));
        }


    }
}
