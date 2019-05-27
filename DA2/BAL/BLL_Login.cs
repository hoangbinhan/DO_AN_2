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
    class BLL_Login
    {
        DALayer db = null;
        public BLL_Login()
        {
            db = new DALayer();
        }
        public DataSet DangNhap(string tenTK, string pass)
        {
            return db.ExecuteScalar("spLogin", CommandType.StoredProcedure, new SqlParameter("@tenTK", tenTK), 
                new SqlParameter("@matkhau", pass));
        }
        //
        public DataSet LayThongTinNV(string tenTK, string pass)
        {
            return db.ExecuteScalar("spLayThongTinNV", CommandType.StoredProcedure, new SqlParameter("@tenTK", tenTK), new SqlParameter("@matkhau", pass));
        }
    }
}
