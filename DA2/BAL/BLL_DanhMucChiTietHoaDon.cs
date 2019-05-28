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
    public class BAL_DanhMucChiTietHoaDon
    {
        DALayer db = null;
        public BAL_DanhMucChiTietHoaDon()
        {
            db = new DALayer();
        }
        //
        public DataSet LocSanPham(ref string err,string search)
        {
            return db.ExecuteNonQueryDataSet("spLocSanPham", CommandType.StoredProcedure, ref err,
                new SqlParameter("@search", search));
        }
        //
        public DataSet SearchDanhMucChiTietHoaDon(ref string err, string search)
        {
            return db.ExecuteNonQueryDataSet("spSearchDanhMucChiTietHoaDon", CommandType.StoredProcedure, ref err, new SqlParameter("@search", search));
        }
        //
        //
        public DataSet SelectChonSanPham()
        {
            return db.ExecuteQueryDataSet("dbo.spViewChonSanPham", CommandType.StoredProcedure, null);
        }
        //
        public DataSet SelectDanhMucChiTietHoaDon()
        {
            return db.ExecuteQueryDataSet("spViewDanhMucChiTietHoaDon", CommandType.StoredProcedure, null);
        }

        //
        //Thêm hóa đơn
        public bool InsertHoaDon(ref string err, string MaHD,DateTime NgayLapHD,string NguoiLapHD,string MaKH)
        {
            return db.MyExecuteNonQuery("spInsertHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaHD", MaHD),
                new SqlParameter("@NgayLapHD", NgayLapHD),
                new SqlParameter("@NguoiLapHD", NguoiLapHD),
                new SqlParameter("@MaKH", MaKH)
               );
        }
        //
        //thêm chi tiết háo đơn
        public bool InsertChiTietHoaDon(ref string err,string MaHD,string MaSP,int SoLuong)
        {
            return db.MyExecuteNonQuery("spInsertChiTietHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaHD", MaHD),
                new SqlParameter("@MaSP", MaSP),
                new SqlParameter("@SoLuong", SoLuong)
                );
        }
        //
        //Xóa chi tiết háo đơn
        //public bool DeleteChiTietHoaDon(ref string err, string MaHD)
        //{
        //    return db.MyExecuteNonQuery("spDeleteChiTietHoaDon", CommandType.StoredProcedure, ref err,
        //        new SqlParameter("@MaHD", MaHD));
        //}
        //
        public bool DeleteHoaDon(ref string err,string MaHD)
        {
            return db.MyExecuteNonQuery("spDeleteHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaHD", MaHD));
        }
        //
        
        public bool UpdateHoaDon(ref string err, string MaHD, DateTime NgayLapHD, string NguoiLapHD, string MaKH)
        {
            return db.MyExecuteNonQuery("spUpdateHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaHD", MaHD),
                new SqlParameter("@NgayLapHD", NgayLapHD),
                new SqlParameter("@NguoiLapHD", NguoiLapHD),
                new SqlParameter("@MaKH", MaKH)
               );
        }
        //
        public bool UpdateSoLuongSanPham(ref string err,string MaSP,int SoLuong)
        {
            return db.MyExecuteNonQuery("spUpdateSoLuong", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaSP", MaSP),
                new SqlParameter("@SoLuong", SoLuong));
        }
        //thêm chi tiết háo đơn
        public bool UpdateChiTietHoaDon(ref string err, string MaHD, string MaSP, int SoLuong)
        {
            return db.MyExecuteNonQuery("spUpdateChiTietHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaHD", MaHD),
                new SqlParameter("@MaSP", MaSP),
                new SqlParameter("@SoLuong", SoLuong)
               );
        }
    }
}
