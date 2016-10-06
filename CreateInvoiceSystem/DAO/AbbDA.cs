using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CreateInvoiceSystem
{
    public class AbbDA
    {
        static private string ConnectionString = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();

        static public bool UpdateABB_Inactive(string TransactionId ,string PNR_No)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_ABB SET ");
                sb.Append("STATUS_INV = 'INACTIVE' ");
                sb.Append("WHERE TransactionId = @TransactionId AND PNR_No = @PNR_No ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@TransactionId", TransactionId));
                com.Parameters.Add(ParameterAdd("@PNR_No", PNR_No));



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

        static public string OR_ABBbyABBNo(string ABBNo)
        {

            using (var context = new POSINVEntities())
            {
                if (context.T_ABB.Count(x => x.TaxInvoiceNo == ABBNo) > 0)
                {
                    if (context.T_Invoice_Info.Count(x => x.ABB_NO.Contains(ABBNo)) > 0)
                    {
                        return "Can not OR because this ABB create Invoice already.";
                    }

                    var _ABB = context.T_ABB.First(x => x.TaxInvoiceNo == ABBNo);
                    context.T_ABB.Attach(_ABB);
                    _ABB.STATUS_INV = "INACTIVE";

                    foreach (var data in context.T_BookingCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_BookingCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }
                    foreach (var data in context.T_PassengerCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_PassengerCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }
                    foreach (var data in context.T_PassengerFeeCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_PassengerFeeCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }
                    foreach (var data in context.T_PassengerFeeServiceChargeCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_PassengerFeeServiceChargeCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }
                    foreach (var data in context.T_SegmentCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_SegmentCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }
                    foreach (var data in context.T_PaxFareCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_PaxFareCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }
                    foreach (var data in context.T_PaymentCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_PaymentCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }
                    foreach (var data in context.T_BookingServiceChargeCurrentUpdate.Where(x => x.ABBNo == _ABB.TaxInvoiceNo).ToList())
                    {
                        context.T_BookingServiceChargeCurrentUpdate.Attach(data);
                        data.ABBNo = null;
                    }

                    context.SaveChanges();

                    return "OR Success.";
                }
            }

            return "Not Found Data.";
        }
    }
}