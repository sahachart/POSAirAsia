using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CreateInvoiceSystem
{
    public class CustomerDA
    {
        static private string ConnectionString = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();

        static public bool InsertCustomer(T_Invoice_Info data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_Invoice_Info ");
                sb.Append("(CustomerID,first_name,last_name,prfx_name,gndr_code,Addr_1,Addr_2,Addr_3,Prvn_Code,Cntr_Code,Zip_Code,Home_Phone,Email,remark,crtd_dt,crtd_by,last_upd_dt,last_upd_by,Addr_4,IsActive ,TaxID) VALUES ");
                sb.Append("(@CustomerID,@first_name,@last_name,@prfx_name,@gndr_code,@Addr_1,@Addr_2,@Addr_3,@Prvn_Code,@Cntr_Code,@Zip_Code,@Home_Phone,@Email,@remark,@crtd_dt,@crtd_by,@last_upd_dt,@last_upd_by,@Addr_4,@IsActive ,@TaxID)  ");
         

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@INV_No", data.INV_No));
                com.Parameters.Add(ParameterAdd("@Ref_INV_No", data.Ref_INV_No));
                com.Parameters.Add(ParameterAdd("@INVDate", data.INVDate));
                com.Parameters.Add(ParameterAdd("@CustomerID", data.CustomerID));
                com.Parameters.Add(ParameterAdd("@Receipt_By", data.Receipt_By));
                com.Parameters.Add(ParameterAdd("@Receipt_Date", data.Receipt_Date));
                com.Parameters.Add(ParameterAdd("@Booking_No", data.Booking_No));
                com.Parameters.Add(ParameterAdd("@Booking_Date", data.Booking_Date));
                com.Parameters.Add(ParameterAdd("@PayType", data.PayType));
                com.Parameters.Add(ParameterAdd("@BankName", data.BankName));
                com.Parameters.Add(ParameterAdd("@BranchNo", data.BranchNo));
                com.Parameters.Add(ParameterAdd("@AcountNo", data.AcountNo));
                com.Parameters.Add(ParameterAdd("@Cheque_No", data.Cheque_No));
                com.Parameters.Add(ParameterAdd("@Cheque_Date", data.Cheque_Date));
                com.Parameters.Add(ParameterAdd("@INVType", data.INVType));
                com.Parameters.Add(ParameterAdd("@Remark", data.Remark));
                com.Parameters.Add(ParameterAdd("@VatType", data.VatType));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@UpDate_By", data.UpDate_By));


                com.Parameters.Add(ParameterAdd("@Address", data.Address));
                com.Parameters.Add(ParameterAdd("@POS_Code", data.POS_Code));
                com.Parameters.Add(ParameterAdd("@SlipMessage_Code", data.SlipMessage_Code));
                com.Parameters.Add(ParameterAdd("@IsPrint", 1));
                com.Parameters.Add(ParameterAdd("@CN_ID", data.CN_ID));
                com.Parameters.Add(ParameterAdd("@IsCancel", false));
                com.Parameters.Add(ParameterAdd("@Cancel_Date", data.Cancel_Date));
                com.Parameters.Add(ParameterAdd("@OR_Reason", data.OR_Reason));
                com.Parameters.Add(ParameterAdd("@Status", data.Status));


                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }


            return false;
        }




        static public SqlParameter ParameterAdd(string ParameterName, object data)
        {
            if (data != null)
                return new SqlParameter(ParameterName, data);
            else
                return new SqlParameter(ParameterName, DBNull.Value);

        }

    }
}