using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
namespace DAL
{
    public class DALayer
    {
        //Chuỗi kết nôi
        public string StrConnection = @"Data Source=DESKTOP-PIIBUUO\SQLEXPRESS;" +
            "Initial Catalog=QuanLyLinhKienPC;" +
            "Integrated Security=True";
        string strCon = null;
        //Đôi tượng kết nối
        public SqlConnection conn = null;
        //Lệnh
        public SqlCommand comm = null;
        //Đôi tượng đưa dữ liệu vào DataSet
        public SqlDataAdapter da = null;
        public DALayer()
        {
            StreamReader reader = new StreamReader("ConnectString.con");
            strCon = reader.ReadLine();
            //Kết nối sql
            conn = new SqlConnection(strCon);
            //Tạo lệnh
            comm = conn.CreateCommand();
        }
        //hàm connect database
        public string Connection()
        {
            try
            {
                conn = new SqlConnection(strCon);
                comm = conn.CreateCommand();
                string f = "y";
                return f;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        //Lấy dữ liệu từ sql lên
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct, params SqlParameter[] param)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            //vận chuyển dữ liệu lên DataSet ds
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        //Thực hiện các lệnh
        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error, params SqlParameter[] param)
        {
            bool f = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            //
            comm.Parameters.Clear();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            foreach (SqlParameter p in param)
                comm.Parameters.Add(p);
            try
            {
                comm.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return f;
        }

        public DataSet ExecuteNonQueryDataSet(string strSQL, CommandType ct, ref string error, params SqlParameter[] param)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            //
            comm.Parameters.Clear();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            foreach (SqlParameter p in param)
                comm.Parameters.Add(p);
            //vận chuyển dữ liệu lên DataSet ds
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //test
        public DataSet ExecuteScalar(string strSQL, CommandType ct, params SqlParameter[] param)
        {
            //int n;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandType = ct;
            foreach (SqlParameter p in param)
                cmd.Parameters.Add(p);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        //
        public int MyExecuteNonValue(string strSQL, CommandType ct, ref string error, params SqlParameter[] param)
        {
            int a = 0;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            //
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            foreach (SqlParameter p in param)
                comm.Parameters.Add(p);
            try
            {
                comm.ExecuteNonQuery();
                a = comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return a;
        }
        public int MyExecuteNonValuethang(string strSQL, CommandType ct, ref string error, int Thang)
        {
            int a = 0;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            //
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            comm.Parameters.Add(Thang);
            try
            {
                comm.ExecuteNonQuery();
                a = comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return a;
        }
        public int MyExecuteNonQuery3(string strSQL, CommandType ct, ref string error, params SqlParameter[] param)
        {
            int a = 0;


            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            //
            comm.Parameters.Clear();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            foreach (SqlParameter p in param)
                comm.Parameters.Add(p);
            try
            {
                // comm.ExecuteScalar();
                a = Convert.ToInt32(comm.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return a;
        }
    }
}
