using CreateInvoiceSystem.Reports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CreateInvoiceSystem
{
    public class InvoiceCNDA
    {
        static private string ConnectionString = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();

        static public DataTable GetInvoiceList(string WhereClause)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT	book.PNR_No,book.Booking_Date,inv.INV_No ,T_ABB.TaxInvoiceNo, ISNULL( inv.Status,'Full Tax') AS Status,  ");
                sb.AppendLine("M_Customer.first_name + ' ' +M_Customer.last_name  AS CustName , ");
                sb.AppendLine("T_ABB.Create_By,T_ABB.Create_Date ,T_ABB.Update_By , T_ABB.Update_Date  ");
                sb.AppendLine("FROM T_Invoice_Info inv ");
                sb.AppendLine("INNER JOIN T_ABB ON T_ABB.TransactionId = inv.TransactionId ");
                sb.AppendLine("INNER JOIN T_BookingCurrentUpdate book ON book.TransactionId = inv.TransactionId ");
                sb.AppendLine("LEFT JOIN M_Customer  ON M_Customer.CustomerID = inv.CustomerID ");
                sb.AppendLine("WHERE  inv.IsPrint = 1 AND ISNULL( inv.Status,'') <> 'OR'  " + WhereClause + " ");
                sb.AppendLine("ORDER BY ISNULL( inv.Status,''), T_ABB.Create_Date DESC");
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;


                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        static public DataTable GetInvoiceORList(string WhereClause)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT	book.PNR_No,book.Booking_Date,inv.INV_No ,T_ABB.TaxInvoiceNo, ISNULL( inv.Status,'Full Tax') AS Status,  ");
                sb.AppendLine("M_Customer.first_name + ' ' +M_Customer.last_name  AS CustName , ");
                sb.AppendLine("T_ABB.Create_By,T_ABB.Create_Date ,T_ABB.Update_By , T_ABB.Update_Date  ");
                sb.AppendLine("FROM T_Invoice_Info inv ");
                sb.AppendLine("INNER JOIN T_ABB ON T_ABB.TransactionId = inv.TransactionId ");
                sb.AppendLine("INNER JOIN T_BookingCurrentUpdate book ON book.TransactionId = inv.TransactionId ");
                sb.AppendLine("LEFT JOIN M_Customer  ON M_Customer.CustomerID = inv.CustomerID ");
                sb.AppendLine("WHERE inv.IsPrint = 0 " + WhereClause + " ");
                sb.AppendLine("ORDER BY  ISNULL( inv.Status,''),  T_ABB.Create_Date DESC");
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;


                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        static public DataTable Get_M_CNReason(string WhereClause)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT	* ");
                sb.AppendLine("FROM M_CNReason ");
                sb.AppendLine("WHERE 1=1 " + WhereClause);

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;


                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        static public bool InsertInvoiceCN(T_Invoice_CN data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_Invoice_CN ");
                sb.Append("(CN_ID,TransactionId,INV_No,PNR_No,CN_NO,CNReason_Id,CNReason_Code,Create_By,Create_Date) VALUES");
                sb.Append("(@CN_ID,@TransactionId,@INV_No,@PNR_No,@CN_NO,@CNReason_Id,@CNReason_Code,@Create_By,GETDATE()) ");


                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@CN_ID", data.CN_ID));
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@INV_No", data.INV_No));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@CN_NO", data.CN_NO));
                com.Parameters.Add(ParameterAdd("@CNReason_Id", data.CNReason_Id));
                com.Parameters.Add(ParameterAdd("@CNReason_Code", data.CNReason_Code));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));

                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        static public bool UpdateInvoiceCN(T_Invoice_CN data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_CN SET  ");
                sb.Append("CNReason_Id = @CNReason_Id,  ");
                sb.Append("CNReason_Code = @CNReason_Code  ");
                sb.Append("WHERE CN_ID = @CN_ID  ");
           
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@CNReason_Id", data.CNReason_Id));
                com.Parameters.Add(ParameterAdd("@CNReason_Code", data.CNReason_Code));
                com.Parameters.Add(ParameterAdd("@CN_ID", data.CN_ID));

                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        static public SqlParameter ParameterAdd(string ParameterName, object data)
        {
            if (data != null)
                return new SqlParameter(ParameterName, data);
            else
                return new SqlParameter(ParameterName, DBNull.Value);
        }


        static public DataReports GetInvoiceCN_Report(string CN_ID, string Booking_No)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT inv.INV_No,cn.CN_NO, inv.Booking_No,cn.CNReason_Code,reason.CNReason_Name,inv.Address,cus.TaxID,'' as cust_code , ");
                sb.AppendLine(" ( cus.first_name + ' ' + cus.last_name + ' ' + cus.Branch_No) as CustomerName , ");
                sb.AppendLine(" cn.Create_By,cn.Create_Date, (inv.TOTAL - inv.VAT) as SubTotal , (inv.VAT) as VAT ,(inv.TOTAL) as TOTAL,  emp.FirstName + ' ' + emp.LastName as EmpFullName  ");
                sb.AppendLine("FROM T_Invoice_CN cn ");
                sb.AppendLine("INNER JOIN T_Invoice_Info inv ON inv.INV_No = cn.INV_No ");
                sb.AppendLine("INNER JOIN M_CNReason reason ON reason.CNReason_Id = cn.CNReason_Id ");
                sb.AppendLine("INNER JOIN M_Customer cus ON cus.CustomerID = inv.CustomerID ");
                //sb.AppendLine("INNER JOIN T_ABB ON inv.ABB_NO like ('%' + T_ABB.TaxInvoiceNo + '%') ");
                sb.AppendLine("INNER JOIN M_Employee emp ON emp.UserCode = cn.Create_By ");
                sb.AppendLine("WHERE cn.CN_ID = @CN_ID ");
                //sb.AppendLine("GROUP BY inv.INV_No,cn.CN_NO, inv.Booking_No,cn.CNReason_Code,reason.CNReason_Name,inv.Address,cus.TaxID,cus.CustomerID, ");
                //sb.AppendLine("cus.prfx_name,cus.first_name,cus.last_name,cn.Create_By,cn.Create_Date,emp.FirstName,emp.LastName");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@CN_ID", CN_ID));
                //com.Parameters.Add(ParameterAdd("@Booking_No", Booking_No));

                DataReports ds = new DataReports();
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.DT_CreditNote_Form.TableName);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }


        static public DataReports GetInvoiceCN_M_Report(string Receipt_no)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT inv.Receipt_no as INV_No, cn.CN_NO, inv.Booking_No,cn.CNReason_Code,reason.CNReason_Name,inv.Customer_address1 as Address,inv.Card_no as TaxID, '' as cust_code , ");
                sb.AppendLine(" cn.Create_By,cn.Create_Date, Amount as SubTotal , inv.VAT ,inv.Net_balance as TOTAL,  emp.FirstName + ' ' + emp.LastName as EmpFullName  ");
                sb.AppendLine("FROM T_Invoice_CN cn ");
                sb.AppendLine("INNER JOIN T_Invoice_Manual inv ON inv.Receipt_no = cn.INV_No ");
                sb.AppendLine("INNER JOIN M_CNReason reason ON reason.CNReason_Id = cn.CNReason_Id ");
                sb.AppendLine("INNER JOIN M_Employee emp ON emp.UserCode = cn.Create_By ");
                sb.AppendLine("WHERE inv.Receipt_no = @Receipt_no ");


                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Receipt_no", Receipt_no));

                DataReports ds = new DataReports();
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.DT_CreditNote_Form.TableName);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }

        static public DataReports GetInvoiceCNDT_M_Report(string Receipt_no)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT ROW_NUMBER() OVER(ORDER BY dt.ID) as seq, dt.Flight_Fee_Name AS Detail , dt.UnitQTY AS Price,dt.Qty ,dt.AmountTH AS Amount  ");
                sb.AppendLine("From T_Invoice_Detail dt ");
                sb.AppendLine("INNER JOIN T_Invoice_Manual inv ON inv.Receipt_no = dt.Receipt_no ");
                sb.AppendLine("WHERE inv.Receipt_no = @Receipt_no ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Receipt_no", Receipt_no));

                DataReports ds = new DataReports();
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.DT_CreditNoteDT_Form.TableName);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }


        static public DataTable GetInvoiceCN_ByCNID(string CN_ID)
        {

            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT *, M_CNReason.CNReason_Name ");
                sb.AppendLine("FROM T_Invoice_CN  ");
                sb.AppendLine("LEFT JOIN M_CNReason ON M_CNReason.CNReason_Id = T_Invoice_CN.CNReason_Id   ");
                sb.AppendLine("WHERE CN_ID = @CN_ID ");


                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@CN_ID", CN_ID));

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        static public DataTable GetInvoiceCN_ByInvNo(string INV_No)
        {

            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT *, M_CNReason.CNReason_Name ");
                sb.AppendLine("FROM T_Invoice_CN  ");
                sb.AppendLine("LEFT JOIN M_CNReason ON M_CNReason.CNReason_Id = T_Invoice_CN.CNReason_Id   ");
                sb.AppendLine("WHERE INV_No = @INV_No ");


                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@INV_No", INV_No));

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // 
        static public DataTable GetInvoiceManualList(string WhereClause)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT TransactionId,Receipt_no,Receipt_type,Receipt_date,Station_Code,SlipMessage_Code ,Branch_name,Branch_address1,Branch_address2,Branch_address3,Branch_Tax_ID,POS_Code,Cashier_no,Booking_name ,Booking_no      ,Booking_date      ,Customer_address1      ,Customer_address2      ,Customer_address3      ,Customer_Tax_ID      ,Passenger_name      ,Route      ,Remark      ,Create_by      ,Update_by      ,Create_date      ,Update_date      ,VAT_CODE      ,Amount      ,Vat      ,Net_balance      ,Payment_type      ,Card_no      ,Bank      ,Document_State      ,IsPrint      ,IsActive      ,CN_ID      ,ISNULL( Status,'Full Tax') AS Status ");
                sb.AppendLine("FROM T_Invoice_Manual inv ");
                sb.AppendLine("WHERE inv.IsPrint = 1 " + WhereClause);
                sb.AppendLine("ORDER BY ISNULL( inv.Status,''),inv.Create_Date DESC");
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;


                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}