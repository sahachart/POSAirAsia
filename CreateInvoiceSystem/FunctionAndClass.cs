using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace CreateInvoiceSystem
{
    public class FunctionAndClass
    {
        public static string ActionID = "";
        public static int SliptID = 0;
        public static string Strconn = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();
        public static SqlConnection sqlconn = new SqlConnection(Strconn);
        public static SqlTransaction transql;

        public static SqlDataReader IexcuteDataBySqlReader(SqlCommand sqlcmd)
        {
            SqlConnection conn = new SqlConnection(Strconn);
            conn.Open();
            try
            {
                sqlcmd.Connection = conn;
                SqlDataReader dr = sqlcmd.ExecuteReader();
                return dr;
            }
            catch
            {
                conn.Close();
                return null;
            }
        }

        public static DataTable IexcuteDataByDataTable(SqlCommand sqlcmd) 
        {
            SqlConnection conn = new SqlConnection(Strconn);
            conn.Open();
            try
            {
                sqlcmd.Connection = conn;
                DataTable dt = new DataTable();
                dt.Load(sqlcmd.ExecuteReader());
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool IexcuteData(SqlCommand sqlcmd)
        {
            SqlConnection conn = new SqlConnection(Strconn);
            SqlTransaction tran = null;
            conn.Open();
            try
            {
                tran = conn.BeginTransaction();
                sqlcmd.Connection = conn;
                sqlcmd.Transaction = tran;
                sqlcmd.ExecuteNonQuery();
                tran.Commit();
                conn.Close();
                return true;
            }
            catch(Exception ex)
            {
                tran.Rollback();
                conn.Close();
                return false;
            }
        }

        public static int IexcuteDataByScalar(SqlCommand sqlcmd)
        {
            int i = 0;
            SqlConnection conn = new SqlConnection(Strconn);
            SqlTransaction tran = null;
            conn.Open();
            try
            {
                tran = conn.BeginTransaction();
                sqlcmd.Connection = conn;
                sqlcmd.Transaction = tran;
                i = (int)sqlcmd.ExecuteScalar();
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            conn.Close();
            return i;
        }
    }
}