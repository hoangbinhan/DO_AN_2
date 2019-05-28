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
    public class BLL_ThongKe
    {
        DALayer db =null;
        public BLL_ThongKe()
        {
            db = new DALayer();
        }
        public DataSet getThongKeTheoLSP(ref string err,string LSP)
        {
            return db.ExecuteNonQueryDataSet("SELECT * FROM [dbo].fThongKeTheoLSP(@LSP)", CommandType.Text, ref err,
                new SqlParameter("@LSP", LSP));
        }

        public DataSet getThongKeTheoThang(ref string err, int Thang)
        {
            return db.ExecuteNonQueryDataSet("select * from dbo.fThongKeTheoThang(@Thang)", CommandType.Text, ref err,
                new SqlParameter("@Thang", Thang));
        }
        //
        public DataSet getTongThongKe(ref string err)
        {
            return db.ExecuteNonQueryDataSet("SELECT * FROM dbo.fTongThongKe()", CommandType.Text, ref err);
        }

        public int DoanhThuTheoLSP(ref string err, string MaLSP)
        {
            return db.MyExecuteNonQuery3("SELECT dbo.ftongTienTheoLSP(@LSP)", CommandType.Text, ref err,
                new SqlParameter("@LSP",MaLSP));
        }
        //
        public int DoanhThuTheoThang(ref string err, int Thang)
        {
            return db.MyExecuteNonQuery3("SELECT [dbo].[fTongThongKeTheoThang](@Thang)", CommandType.Text, ref err,
               new SqlParameter("@Thang",Thang));
        }
        //
        public int TongDoanhThu(ref string err)
        {
            return db.MyExecuteNonQuery3("SELECT dbo.fTongTien()", CommandType.Text, ref err);
        }
    }
}
