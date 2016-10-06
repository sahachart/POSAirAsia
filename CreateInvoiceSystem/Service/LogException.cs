using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace CreateInvoiceSystem
{
    public class LogException
    {
        static public void Save(Exception ex)
        {
            Save("", ex.Message, ex.StackTrace, "");
        }

        static public void Save(Exception ex, string url, string userId)
        {
            Save(url, ex.Message, ex.StackTrace, userId);
        }

        static public void Save(string url, string errorMsg, string trace, string userId)
        {
            KeepLogException(url, errorMsg, trace, userId);
        }

        /// <summary>
        /// LogException.Save(ex, Url, SessionMenager.StaffID);
        /// </summary>
        /// <param name="url"></param>
        /// <param name="errorMsg"></param>
        /// <param name="trace"></param>
        /// <param name="userId"></param>
        static public void KeepLogException(string url, string errorMsg, string trace, string userId)
        {
            try
            {
                string strConn = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConn);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = conn;
                sqlComm.CommandType = CommandType.Text;
                string strQuery = @"INSERT INTO [dbo].[LOG_EXCEPTION]
                                       ([URL]
                                       ,[ErrorMsg]
                                       ,[StackTrace]
                                       ,[Create_By]
                                       ,[Create_Date]
                                       ,Application)
                                 VALUES
                                       (@URL
                                       , @ErrorMsg
                                       , @StackTrace
                                       , @Create_By
                                       , GETDATE()
                                       ,'Application')";


                sqlComm.CommandType = System.Data.CommandType.Text;
                sqlComm.CommandText = strQuery;
                sqlComm.Parameters.Add(new SqlParameter("@URL", url));
                sqlComm.Parameters.Add(new SqlParameter("@ErrorMsg", errorMsg));
                sqlComm.Parameters.Add(new SqlParameter("@StackTrace", trace));
                sqlComm.Parameters.Add(new SqlParameter("@Create_By", userId));

                int row = sqlComm.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {

            }

        }

    }
}