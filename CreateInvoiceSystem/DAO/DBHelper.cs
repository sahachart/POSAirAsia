using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CreateInvoiceSystem
{
    public class DBHelper
    {
        static private string ConnectionString = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();

        static public SqlParameter ParameterAdd(string ParameterName, object data)
        {
            if (data != null)
                return new SqlParameter(ParameterName, data);
            else
                return new SqlParameter(ParameterName, DBNull.Value);

        }

        static public PosDS GetBookingByBookingNo(PosDS ds, string BookingNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_BookingCurrentUpdate WHERE PNR_No = @PNR_No ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@PNR_No", BookingNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_Booking.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetPaasengerByTransactionId(PosDS ds, string TransactionId)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_PassengerCurrentUpdate WHERE TransactionId = @TransactionId AND Action <> -1");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@TransactionId", TransactionId));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_Passenger.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }


        static public PosDS GetSegmentByTransactionId(PosDS ds, string TransactionId)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_SegmentCurrentUpdate WHERE TransactionId = @TransactionId ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@TransactionId", TransactionId));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_Segment.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }


        static public PosDS GetPaxFareBySegmentTId(PosDS ds, string SegmentTId)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_PaxFareCurrentUpdate WHERE SegmentTId = @SegmentTId ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@SegmentTId", SegmentTId));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_PaxFare.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetBookingServiceChargeBySegmentTId(PosDS ds, string PaxFareTId)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_BookingServiceChargeCurrentUpdate WHERE PaxFareTId = @PaxFareTId ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@PaxFareTId", PaxFareTId));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_BookingServiceCharge.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetPassengerFeeByPassengerTId(PosDS ds, string PassengerTId)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from T_PassengerFeeCurrentUpdate WHERE PassengerTId = @PassengerTId ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@PassengerTId", PassengerTId));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_PassengerFee.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetPassengerFeeServiceChargeByPassengerTId(PosDS ds, string PassengerFeeTId)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from T_PassengerFeeServiceChargeCurrentUpdate WHERE PassengerFeeTId = @PassengerFeeTId AND Action <> -1");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@PassengerFeeTId", PassengerFeeTId));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_PassengerFeeServiceCharge.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }


        static public PosDS GetPaymentByTransactionId(PosDS ds, string TransactionId)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_PaymentCurrentUpdate WHERE TransactionId = @TransactionId ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@TransactionId", TransactionId));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_Payment.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetABB_ByPNR_No(PosDS ds, string PNR_No)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_ABB WHERE PNR_No = @PNR_No ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@PNR_No", PNR_No));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_ABB.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetBookingAllData(string BookingNo)
        {
            PosDS ds = new PosDS();
            Guid TransactionId;
            ds = GetBookingByBookingNo(ds, BookingNo);
            if (ds.T_Booking.Count > 0)
            {
                TransactionId = ds.T_Booking[0].TransactionId;
                ds = GetPaasengerByTransactionId(ds, TransactionId.ToString());
                ds = GetSegmentByTransactionId(ds, TransactionId.ToString());
                ds = GetPaymentByTransactionId(ds, TransactionId.ToString());
                ds = GetABB_ByPNR_No(ds, BookingNo);
                foreach (PosDS.T_SegmentRow item in ds.T_Segment)
                {
                    ds = GetPaxFareBySegmentTId(ds, item.SegmentTId.ToString());
                }

                foreach (PosDS.T_PaxFareRow item in ds.T_PaxFare)
                {
                    ds = GetBookingServiceChargeBySegmentTId(ds, item.PaxFareTId.ToString());
                }

                foreach (PosDS.T_PassengerRow item in ds.T_Passenger)
                {
                    ds = GetPassengerFeeByPassengerTId(ds, item.PassengerTId.ToString());
                }

                foreach (PosDS.T_PassengerFeeRow item in ds.T_PassengerFee)
                {
                    ds = GetPassengerFeeServiceChargeByPassengerTId(ds, item.PassengerFeeTId.ToString());
                }
            }



            return ds;
        }


        public static decimal GetCurrencyActiveDate(string Code, DateTime DateActive)
        {
            decimal ExChangeRate = 1;
            if (Code != null)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from M_Currency WHERE Currency_Code = @Currency_Code AND IsActive = 'Y' AND Create_Date <= @Create_Date");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@Currency_Code", Code));
                com.Parameters.Add(new SqlParameter("@Create_Date", DateActive));

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        if (result.Rows.Count > 0)
                        {
                            ExChangeRate = Convert.ToDecimal(result.Rows[0]["ExChangeRate"]);
                        }
                        else
                        {
                            sb = new StringBuilder();
                            sb.Append("select * from M_Currency WHERE Currency_Code = @Currency_Code AND IsActive = 'Y' AND Create_Date >= @Create_Date");
                            com = new SqlCommand(sb.ToString(), conn);
                            com.CommandType = System.Data.CommandType.Text;
                            com.Parameters.Add(new SqlParameter("@Currency_Code", Code));
                            com.Parameters.Add(new SqlParameter("@Create_Date", DateActive));
                            using (SqlDataReader reader2 = com.ExecuteReader())
                            {
                                using (DataTable result2 = new DataTable())
                                {
                                    result2.Load(reader2);
                                    if (result2.Rows.Count > 0)
                                    {
                                        ExChangeRate = Convert.ToDecimal(result2.Rows[0]["ExChangeRate"]);
                                    }
                                }
                            }
                        }
                    }
                }


            }
            return ExChangeRate;
        }

        static public DataTable GetExceptionByDate(DateTime date)
        {
            try
            {


                StringBuilder sb = new StringBuilder();
                sb.Append("select * from LOG_EXCEPTION WHERE Application= 'BATCH' AND Create_Date >= @Create_Date  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@Create_Date", date));
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

        static public PosDS GetABB_AllData(string ABBNo)
        {
            PosDS ds = new PosDS();

            try
            {
                ds = GetABBByABBNo(ds, ABBNo);
                ds = GetPaasengerByABBNo(ds, ABBNo);
                ds = GetSegmentByABBNo(ds, ABBNo);
                ds = GetPaxFareByABBNo(ds, ABBNo);
                ds = GetBookingServiceChargeByABBNo(ds, ABBNo);
                ds = GetPassengerFeeByABBNo(ds, ABBNo);
                ds = GetPassengerFeeServiceChargeByABBNo(ds, ABBNo);
                ds = GetPaymentByABBNo(ds, ABBNo);

                if (ds.T_ABB.Count > 0)
                {
                    string ABBTid = ds.T_ABB[0].ABBTid.ToString();
                    ds = GetABBPaxByABBTid(ds, ABBTid);
                    ds = GetABBFareByABBTid(ds, ABBTid);
                    ds = GetABBPaymentByABBTid(ds, ABBTid);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        static public PosDS GetPaasengerByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_PassengerCurrentUpdate WHERE ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_Passenger.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetSegmentByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_SegmentCurrentUpdate WHERE ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_Segment.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetPaxFareByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_PaxFareCurrentUpdate WHERE ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_PaxFare.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetBookingServiceChargeByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_BookingServiceChargeCurrentUpdate WHERE ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_BookingServiceCharge.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetPassengerFeeByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from T_PassengerFeeCurrentUpdate WHERE ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_PassengerFee.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetPassengerFeeServiceChargeByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from T_PassengerFeeServiceChargeCurrentUpdate WHERE ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_PassengerFeeServiceCharge.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetPaymentByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_PaymentCurrentUpdate WHERE ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_Payment.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetABBByABBNo(PosDS ds, string ABBNo)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_ABB WHERE TaxInvoiceNo = @TaxInvoiceNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@TaxInvoiceNo", ABBNo));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_ABB.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetABBPaxByABBTid(PosDS ds, string ABBTid)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_ABBPAX WHERE ABBTid = @ABBTid ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBTid", ABBTid));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_ABBPAX.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetABBFareByABBTid(PosDS ds, string ABBTid)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_ABBFARE WHERE ABBTid = @ABBTid ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBTid", ABBTid));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_ABBFARE.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        static public PosDS GetABBPaymentByABBTid(PosDS ds, string ABBTid)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from T_ABBPAYMENTMETHOD WHERE ABBTid = @ABBTid ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBTid", ABBTid));
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, ds.T_ABBPAYMENTMETHOD.TableName);

            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }


        static public DataTable GetFlightFeeGroup(string Flight_Fee_Code)
        {
            try
            {


                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM M_FlightFee WHERE FlightFeeGroup_ID =  (SELECT TOP 1 FlightFeeGroup_ID FROM M_FlightFee WHERE Flight_Fee_Code = @Flight_Fee_Code)");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@Flight_Fee_Code", Flight_Fee_Code));
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        #region Master Data


        ///////////////   Master Data /////////////////////////////


        static public DataTable GetMasterDataByTableName(string TableName)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from " + TableName + " WHERE IsActive = 'Y'  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);

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
        static public DataTable GetAgency()
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from M_Agency WHERE IsActive = 'Y'  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);

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

        static public DataTable GetAgencyTypeByCode(string Agency_Code)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from M_AgencyType WHERE Agent_type_code = '" + Agency_Code + "' AND IsActive = 'Y'  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);

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

        static public DataTable GetGetAgencyType()
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from M_AgencyType WHERE IsActive = 'Y'  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);

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

        static public DataTable GetPaymentType()
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from M_PaymentType WHERE IsActive = 'Y'  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);

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

        static public DataTable GetPosMachine()
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from M_POS_Machine WHERE IsActive = 'Y'  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);

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

        static public DataTable GetPosMachineByCode(string poscode)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from M_POS_Machine WHERE POS_Code = '" + poscode + "' AND IsActive = 'Y'  ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);

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
        #endregion

        #region Execute


        //////////////////////  Exec
        protected SqlConnection Sqlconn;
        protected SqlTransaction SqlTran;

        public void DBOpenConnection()
        {
            Sqlconn = new SqlConnection(ConnectionString);
            if (Sqlconn.State == System.Data.ConnectionState.Open)
                Sqlconn.Close();
            Sqlconn.Open();
        }

        public void DBCloseConnection()
        {
            if (Sqlconn != null)
                Sqlconn.Close();
        }

        public void BeginTran()
        {
            SqlTran = Sqlconn.BeginTransaction();
         
        }

        public void EndTran(bool success)
        {
            try
            {
                if (success)
                    SqlTran.Commit();
                else
                    SqlTran.Rollback();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Booking


        public bool InsertBooking(PosDS.T_BookingRow data, string ABBNo)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_BookingCurrentUpdate ");
                sb.Append("(TransactionId, BookingId, Load_Date, PNR_No, Booking_Date, CurrencyCode, PaxCount, BookingParentID, ParentRecordLocator, GroupName, BookingStatus, TotalCost, ABBNo, Create_By, Create_Date, Update_By, Update_Date) VALUES ");
                sb.Append("(@TransactionId, @BookingId, @Load_Date, @PNR_No, @Booking_Date, @CurrencyCode, @PaxCount, @BookingParentID, @ParentRecordLocator, @GroupName, @BookingStatus, @TotalCost, @ABBNo, @Create_By, @Create_Date, @Update_By, @Update_Date)");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@BookingId", data.BookingId));
                com.Parameters.Add(ParameterAdd("@Load_Date", data.Load_Date));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@Booking_Date", data.Booking_Date));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@PaxCount", data.PaxCount));
                com.Parameters.Add(ParameterAdd("@BookingParentID", data.BookingParentID));
                com.Parameters.Add(ParameterAdd("@ParentRecordLocator", data.ParentRecordLocator));
                com.Parameters.Add(ParameterAdd("@GroupName", data.GroupName));
                com.Parameters.Add(ParameterAdd("@BookingStatus", data.BookingStatus));
                com.Parameters.Add(ParameterAdd("@TotalCost", data.TotalCost));

                com.Parameters.Add(ParameterAdd("@ABBNo", ABBNo));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));


                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBooking(PosDS.T_BookingRow data, string ABBNo)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_BookingCurrentUpdate SET  ");
                sb.Append("BookingId = @BookingId, ");
                sb.Append("Load_Date = @Load_Date, ");
                //sb.Append("PNR_No = @PNR_No, ");
                sb.Append("Booking_Date = @Booking_Date, ");
                sb.Append("CurrencyCode = @CurrencyCode, ");
                sb.Append("PaxCount = @PaxCount, ");
                sb.Append("BookingParentID = @BookingParentID, ");
                sb.Append("ParentRecordLocator = @ParentRecordLocator, ");
                sb.Append("GroupName = @GroupName, ");
                sb.Append("BookingStatus = @BookingStatus, ");
                sb.Append("TotalCost = @TotalCost, ");
                sb.Append("ABBNo = @ABBNo, ");
                sb.Append("Update_By = @Update_By, ");
                sb.Append("Update_Date = @Update_Date ");
                sb.Append("WHERE PNR_No = @PNR_No ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@BookingId", data.BookingId));
                com.Parameters.Add(ParameterAdd("@Load_Date", data.Load_Date));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@Booking_Date", data.Booking_Date));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@PaxCount", data.PaxCount));
                com.Parameters.Add(ParameterAdd("@BookingParentID", data.BookingParentID));
                com.Parameters.Add(ParameterAdd("@ParentRecordLocator", data.ParentRecordLocator));
                com.Parameters.Add(ParameterAdd("@GroupName", data.GroupName));
                com.Parameters.Add(ParameterAdd("@BookingStatus", data.BookingStatus));
                com.Parameters.Add(ParameterAdd("@TotalCost", data.TotalCost));
                com.Parameters.Add(ParameterAdd("@ABBNo", ABBNo));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));


                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool InsertBooking(PosDS.T_BookingRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_BookingCurrentUpdate ");
                sb.Append("(TransactionId, BookingId, Load_Date, PNR_No, Booking_Date, CurrencyCode, PaxCount, BookingParentID, ParentRecordLocator, GroupName, BookingStatus, TotalCost, ABBNo, Create_By, Create_Date, Update_By, Update_Date) VALUES ");
                sb.Append("(@TransactionId, @BookingId, @Load_Date, @PNR_No, @Booking_Date, @CurrencyCode, @PaxCount, @BookingParentID, @ParentRecordLocator, @GroupName, @BookingStatus, @TotalCost, @ABBNo, @Create_By, @Create_Date, @Update_By, @Update_Date)");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@BookingId", data.BookingId));
                com.Parameters.Add(ParameterAdd("@Load_Date", data.Load_Date));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@Booking_Date", data.Booking_Date));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@PaxCount", data.PaxCount));
                com.Parameters.Add(ParameterAdd("@BookingParentID", data.BookingParentID));
                com.Parameters.Add(ParameterAdd("@ParentRecordLocator", data.ParentRecordLocator));
                com.Parameters.Add(ParameterAdd("@GroupName", data.GroupName));
                com.Parameters.Add(ParameterAdd("@BookingStatus", data.BookingStatus));
                com.Parameters.Add(ParameterAdd("@TotalCost", data.TotalCost));

                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));


                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBooking(PosDS.T_BookingRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_BookingCurrentUpdate SET  ");
                sb.Append("BookingId = @BookingId, ");
                sb.Append("Load_Date = @Load_Date, ");
                //sb.Append("PNR_No = @PNR_No, ");
                sb.Append("Booking_Date = @Booking_Date, ");
                sb.Append("CurrencyCode = @CurrencyCode, ");
                sb.Append("PaxCount = @PaxCount, ");
                sb.Append("BookingParentID = @BookingParentID, ");
                sb.Append("ParentRecordLocator = @ParentRecordLocator, ");
                sb.Append("GroupName = @GroupName, ");
                sb.Append("BookingStatus = @BookingStatus, ");
                sb.Append("TotalCost = @TotalCost, ");
                sb.Append("ABBNo = @ABBNo, ");
                sb.Append("Update_By = @Update_By, ");
                sb.Append("Update_Date = @Update_Date ");
                sb.Append("WHERE PNR_No = @PNR_No ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@BookingId", data.BookingId));
                com.Parameters.Add(ParameterAdd("@Load_Date", data.Load_Date));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@Booking_Date", data.Booking_Date));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@PaxCount", data.PaxCount));
                com.Parameters.Add(ParameterAdd("@BookingParentID", data.BookingParentID));
                com.Parameters.Add(ParameterAdd("@ParentRecordLocator", data.ParentRecordLocator));
                com.Parameters.Add(ParameterAdd("@GroupName", data.GroupName));
                com.Parameters.Add(ParameterAdd("@BookingStatus", data.BookingStatus));
                com.Parameters.Add(ParameterAdd("@TotalCost", data.TotalCost));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));


                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }




        #endregion


        public bool InsertPassenger(PosDS.T_PassengerRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO T_PassengerCurrentUpdate  ");
            sb.Append("(PassengerTId,TransactionId, PassengerId, FirstName, LastName, Title, PaxType, ABBNo, Create_By, Create_Date, Update_By, Update_Date) VALUES");
            sb.Append("(@PassengerTId,@TransactionId, @PassengerId, @FirstName, @LastName, @Title, @PaxType, @ABBNo, @Create_By, @Create_Date,@Update_By, @Update_Date) ");


            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PassengerTId", data.PassengerTId));
            com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
            com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
            com.Parameters.Add(ParameterAdd("@FirstName", data.FirstName));
            com.Parameters.Add(ParameterAdd("@LastName", data.LastName));
            com.Parameters.Add(ParameterAdd("@Title", data.Title));
            com.Parameters.Add(ParameterAdd("@PaxType", data.PaxType));
            if (!data.IsABBNoNull())
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
            com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        public bool InsertPassengerFee(PosDS.T_PassengerFeeRow data)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_PassengerFeeCurrentUpdate  ");
                sb.Append("(PassengerFeeTId,PassengerTId,PassengerId,PassengerFeeNo,FeeCreateDate ,ABBNo,Create_By ,Create_Date,Update_By ,Update_Date,Action) VALUES");
                sb.Append("(@PassengerFeeTId,@PassengerTId,@PassengerId,@PassengerFeeNo,@FeeCreateDate ,@ABBNo,@Create_By ,@Create_Date,@Update_By ,@Update_Date,@Action) ");


                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PassengerFeeTId", data.PassengerFeeTId));
                com.Parameters.Add(ParameterAdd("@PassengerTId", data.PassengerTId));
                com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
                com.Parameters.Add(ParameterAdd("@PassengerFeeNo", data.PassengerFeeNo));
                com.Parameters.Add(ParameterAdd("@FeeCreateDate", data.FeeCreateDate));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Action", 0));

                int row = com.ExecuteNonQuery();
                return row > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertPaxFare(PosDS.T_PaxFareRow data)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_PaxFareCurrentUpdate  ");
                sb.Append("(PaxFareTId,SegmentTId,SeqmentNo,SeqmentPaxNo,PaxType,ABBNo,Create_By,Create_Date,Update_By,Update_Date,Action) VALUES");
                sb.Append("(@PaxFareTId,@SegmentTId,@SeqmentNo,@SeqmentPaxNo,@PaxType,@ABBNo,@Create_By,@Create_Date,@Update_By,@Update_Date,@Action) ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PaxFareTId", data.PaxFareTId));
                com.Parameters.Add(ParameterAdd("@SegmentTId", data.SegmentTId));
                com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
                com.Parameters.Add(ParameterAdd("@SeqmentPaxNo", data.SeqmentPaxNo));
                com.Parameters.Add(ParameterAdd("@PaxType", data.PaxType));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Action", 0));


                int row = com.ExecuteNonQuery();
                return row > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertPayment(PosDS.T_PaymentRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_PaymentCurrentUpdate ");
                sb.Append("(PaymentTId,TransactionId,PaymentID,ReferenceType,ReferenceID,PaymentMethodType,PaymentMethodCode,CurrencyCode,PaymentAmount,CollectedCurrencyCode,CollectedAmount,QuotedCurrencyCode,QuotedAmount,Status,ParentPaymentID,AgentCode,OrganizationCode,DomainCode,LocationCode,ApprovalDate,ABBNo,Create_By,Create_Date,Update_By,Update_Date,BankName,BranchNo,AccountNo,AccountNoID) VALUES ");
                sb.Append("(@PaymentTId,@TransactionId,@PaymentID,@ReferenceType,@ReferenceID,@PaymentMethodType,@PaymentMethodCode,@CurrencyCode,@PaymentAmount,@CollectedCurrencyCode,@CollectedAmount,@QuotedCurrencyCode,@QuotedAmount,@Status,@ParentPaymentID,@AgentCode,@OrganizationCode,@DomainCode,@LocationCode,@ApprovalDate,@ABBNo,@Create_By,@Create_Date,@Update_By,@Update_Date,@BankName,@BranchNo,@AccountNo,@AccountNoID)");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@PaymentTId", data.PaymentTId));
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@PaymentID", data.PaymentID));
                com.Parameters.Add(ParameterAdd("@ReferenceType", data.ReferenceType));
                com.Parameters.Add(ParameterAdd("@ReferenceID", data.ReferenceID));
                com.Parameters.Add(ParameterAdd("@PaymentMethodType", data.PaymentMethodType));
                com.Parameters.Add(ParameterAdd("@PaymentMethodCode", data.PaymentMethodCode));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@PaymentAmount", data.PaymentAmount));
                com.Parameters.Add(ParameterAdd("@CollectedCurrencyCode", data.CollectedCurrencyCode));
                com.Parameters.Add(ParameterAdd("@CollectedAmount", data.CollectedAmount));
                com.Parameters.Add(ParameterAdd("@QuotedCurrencyCode", data.QuotedCurrencyCode));
                com.Parameters.Add(ParameterAdd("@QuotedAmount", data.QuotedAmount));

                com.Parameters.Add(ParameterAdd("@Status", data.Status));
                com.Parameters.Add(ParameterAdd("@ParentPaymentID", data.ParentPaymentID));
                com.Parameters.Add(ParameterAdd("@AgentCode", data.AgentCode));
                com.Parameters.Add(ParameterAdd("@OrganizationCode", data.OrganizationCode));
                com.Parameters.Add(ParameterAdd("@DomainCode", data.DomainCode));

                com.Parameters.Add(ParameterAdd("@LocationCode", data.LocationCode));
                com.Parameters.Add(ParameterAdd("@ApprovalDate", data.ApprovalDate));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));


                com.Parameters.Add(ParameterAdd("@BankName", data.BankName));
                com.Parameters.Add(ParameterAdd("@BranchNo", data.BranchNo));
                com.Parameters.Add(ParameterAdd("@AccountNo", data.AccountNo));
                com.Parameters.Add(ParameterAdd("@AccountNoID", data.AccountNoID));
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertBookingServiceCharge(PosDS.T_BookingServiceChargeRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO T_BookingServiceChargeCurrentUpdate ");
            sb.Append(" (BookingServiceChargeTId ,PaxFareTId ,SeqmentNo,SeqmentPaxNo,ServiceChargeNo,PassengerId ,ChargeType,CollectType ,ChargeCode,TicketCode,CurrencyCode,Amount,BaseAmount,ChargeDetail,ForeignCurrencyCode ,ForeignAmount,ABBNo,Create_By,Create_Date,Update_By,Update_Date,SEQ,QTY,Action,FareCreateDate) VALUES ");
            sb.Append(" (@BookingServiceChargeTId ,@PaxFareTId ,@SeqmentNo,@SeqmentPaxNo,@ServiceChargeNo,@PassengerId ,@ChargeType,@CollectType ,@ChargeCode,@TicketCode,@CurrencyCode,@Amount,@BaseAmount,@ChargeDetail,@ForeignCurrencyCode ,@ForeignAmount,@ABBNo,@Create_By,@Create_Date,@Update_By,@Update_Date,@SEQ,@QTY,@Action,@FareCreateDate)");

            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@BookingServiceChargeTId", data.BookingServiceChargeTId));
            com.Parameters.Add(ParameterAdd("@PaxFareTId", data.PaxFareTId));
            com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
            com.Parameters.Add(ParameterAdd("@SeqmentPaxNo", data.SeqmentPaxNo));
            com.Parameters.Add(ParameterAdd("@ServiceChargeNo", data.ServiceChargeNo));
            com.Parameters.Add(ParameterAdd("@PassengerId", DBNull.Value)); // ฟิวนี้ไม่ให้แล้ว
            com.Parameters.Add(ParameterAdd("@ChargeType", data.ChargeType));
            com.Parameters.Add(ParameterAdd("@ChargeCode", data.ChargeCode));
            com.Parameters.Add(ParameterAdd("@ChargeDetail", data.ChargeDetail));
            com.Parameters.Add(ParameterAdd("@TicketCode", data.TicketCode));
            com.Parameters.Add(ParameterAdd("@CollectType", data.CollectType));
            com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
            com.Parameters.Add(ParameterAdd("@Amount", data.Amount));
            com.Parameters.Add(ParameterAdd("@ForeignCurrencyCode", data.ForeignCurrencyCode));
            com.Parameters.Add(ParameterAdd("@ForeignAmount", data.ForeignAmount));
            com.Parameters.Add(ParameterAdd("@BaseAmount", data.BaseAmount));
            if (!data.IsABBNoNull())
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
            com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
            com.Parameters.Add(ParameterAdd("@FareCreateDate", data.FareCreateDate));

            com.Parameters.Add(ParameterAdd("@SEQ", data.SEQ));
            com.Parameters.Add(ParameterAdd("@QTY", data.QTY));
            com.Parameters.Add(ParameterAdd("@Action", 0));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        public bool InsertPassServiceCharge(PosDS.T_PassengerFeeServiceChargeRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO T_PassengerFeeServiceChargeCurrentUpdate ");
            sb.Append(" (PassengerFeeTId,PassengerId,PassengerFeeNo,FeeCreateDate,ServiceChargeNo,ChargeType,CollectType,ChargeCode,TicketCode,BaseCurrencyCode,CurrencyCode,Amount,BaseAmount,ChargeDetail,ForeignCurrencyCode,ForeignAmount,ABBNo,Create_By,Create_Date,Update_By,Update_Date,SEQ,Action ) VALUES ");
            sb.Append(" (@PassengerFeeTId,@PassengerId,@PassengerFeeNo,@FeeCreateDate,@ServiceChargeNo,@ChargeType,@CollectType,@ChargeCode,@TicketCode,@BaseCurrencyCode,@CurrencyCode,@Amount,@BaseAmount,@ChargeDetail,@ForeignCurrencyCode,@ForeignAmount,@ABBNo,@Create_By,@Create_Date,@Update_By,@Update_Date,@SEQ,@Action )");

            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Clear();

            com.Parameters.Add(ParameterAdd("@PassengerFeeTId", data.PassengerFeeTId));
            com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
            com.Parameters.Add(ParameterAdd("@PassengerFeeNo", data.PassengerFeeNo));
            com.Parameters.Add(ParameterAdd("@FeeCreateDate", data.FeeCreateDate));
            com.Parameters.Add(ParameterAdd("@ServiceChargeNo", data.ServiceChargeNo));
            com.Parameters.Add(ParameterAdd("@ChargeType", data.ChargeType));
            com.Parameters.Add(ParameterAdd("@ChargeCode", data.ChargeCode));
            com.Parameters.Add(ParameterAdd("@ChargeDetail", data.ChargeDetail));
            com.Parameters.Add(ParameterAdd("@TicketCode", data.TicketCode));
            com.Parameters.Add(ParameterAdd("@CollectType", data.CollectType));
            com.Parameters.Add(ParameterAdd("@BaseCurrencyCode", data.BaseCurrencyCode));
            com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
            com.Parameters.Add(ParameterAdd("@Amount", data.Amount));
            com.Parameters.Add(ParameterAdd("@ForeignCurrencyCode", data.ForeignCurrencyCode));
            com.Parameters.Add(ParameterAdd("@ForeignAmount", data.ForeignAmount));
            com.Parameters.Add(ParameterAdd("@BaseAmount", data.BaseAmount));
            if (!data.IsABBNoNull())
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
            com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));

            com.Parameters.Add(ParameterAdd("@SEQ", data.SEQ));
            com.Parameters.Add(ParameterAdd("@Action", 0));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        public bool InsertSegment(PosDS.T_SegmentRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_SegmentCurrentUpdate ");
                sb.Append("(SegmentTId,TransactionId,SeqmentNo,DepartureStation,ArrivalStation,STD,STA,CarrierCode,FlightNumber,ABBNo,Create_By,Create_Date,Update_By,Update_Date,Action) VALUES ");
                sb.Append("(@SegmentTId,@TransactionId,@SeqmentNo,@DepartureStation,@ArrivalStation,@STD,@STA,@CarrierCode,@FlightNumber,@ABBNo,@Create_By,@Create_Date,@Update_By,@Update_Date,@Action)");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@SegmentTId", data.SegmentTId));
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
                com.Parameters.Add(ParameterAdd("@DepartureStation", data.DepartureStation));
                com.Parameters.Add(ParameterAdd("@ArrivalStation", data.ArrivalStation));
                com.Parameters.Add(ParameterAdd("@STD", data.STD));
                com.Parameters.Add(ParameterAdd("@STA", data.STA));
                com.Parameters.Add(ParameterAdd("@CarrierCode", data.CarrierCode));
                com.Parameters.Add(ParameterAdd("@FlightNumber", data.FlightNumber));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Action", 0));


                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePassenger(PosDS.T_PassengerRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE T_PassengerCurrentUpdate  SET ");
            sb.AppendLine("TransactionId = @TransactionId, ");
            sb.AppendLine("PassengerId = @PassengerId, ");
            sb.AppendLine("FirstName = @FirstName, ");
            sb.AppendLine("LastName = @LastName, ");
            sb.AppendLine("Title = @Title, ");
            sb.AppendLine("PaxType = @PaxType, ");
            sb.AppendLine("ABBNo = @ABBNo, ");
            sb.AppendLine("Update_By = @Update_By, ");
            sb.AppendLine("Update_Date = @Update_Date, ");
            sb.AppendLine("Action = @Action ");
            sb.AppendLine("WHERE PassengerTId = @PassengerTId");


            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PassengerTId", data.PassengerTId));
            com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
            com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
            com.Parameters.Add(ParameterAdd("@FirstName", data.FirstName));
            com.Parameters.Add(ParameterAdd("@LastName", data.LastName));
            com.Parameters.Add(ParameterAdd("@Title", data.Title));
            com.Parameters.Add(ParameterAdd("@PaxType", data.PaxType));
            if (!data.IsABBNoNull())
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
            com.Parameters.Add(ParameterAdd("@Action", data.Action));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        public bool UpdatePassengerAbbNo(PosDS.T_PassengerRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE T_PassengerCurrentUpdate  SET ");
            sb.AppendLine("ABBNo = @ABBNo ");
            sb.AppendLine("WHERE PassengerTId = @PassengerTId AND ABBNo IS NULL ");


            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PassengerTId", data.PassengerTId));
            com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));


            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        public bool UpdatePassengerFee(PosDS.T_PassengerFeeRow data)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_PassengerFeeCurrentUpdate  SET ");
                sb.AppendLine("PassengerTId = @PassengerTId, ");
                sb.AppendLine("PassengerId = @PassengerId, ");
                sb.AppendLine("PassengerFeeNo = @PassengerFeeNo, ");
                sb.AppendLine("FeeCreateDate = @FeeCreateDate, ");
                sb.AppendLine("ABBNo = @ABBNo, ");
                sb.AppendLine("Update_By = @Update_By, ");
                sb.AppendLine("Update_Date = @Update_Date, ");
                sb.AppendLine("Action = @Action ");
                sb.AppendLine("WHERE PassengerFeeTId = @PassengerFeeTId ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PassengerFeeTId", data.PassengerFeeTId));
                com.Parameters.Add(ParameterAdd("@PassengerTId", data.PassengerTId));
                com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
                com.Parameters.Add(ParameterAdd("@PassengerFeeNo", data.PassengerFeeNo));
                com.Parameters.Add(ParameterAdd("@FeeCreateDate", data.FeeCreateDate));
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Action", data.Action));

                int row = com.ExecuteNonQuery();
                return row > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdatePassengerFeeAbbNo(PosDS.T_PassengerFeeRow data)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_PassengerFeeCurrentUpdate  SET ");
                sb.AppendLine("ABBNo = @ABBNo ");
                sb.AppendLine("WHERE PassengerFeeTId = @PassengerFeeTId AND ABBNo IS NULL ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PassengerFeeTId", data.PassengerFeeTId));
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));


                int row = com.ExecuteNonQuery();
                return row > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePaxFare(PosDS.T_PaxFareRow data)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_PaxFareCurrentUpdate  SET ");
                sb.AppendLine("SegmentTId = @SegmentTId, ");
                sb.AppendLine("SeqmentNo = @SeqmentNo, ");
                sb.AppendLine("SeqmentPaxNo = @SeqmentPaxNo, ");
                sb.AppendLine("PaxType = @PaxType, ");
                sb.AppendLine("ABBNo = @ABBNo, ");
                sb.AppendLine("Update_By = @Update_By, ");
                sb.AppendLine("Update_Date = @Update_Date, ");
                sb.AppendLine("Action = @Action ");

                sb.AppendLine("WHERE PaxFareTId = @PaxFareTId  ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PaxFareTId", data.PaxFareTId));
                com.Parameters.Add(ParameterAdd("@SegmentTId", data.SegmentTId));
                com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
                com.Parameters.Add(ParameterAdd("@SeqmentPaxNo", data.SeqmentPaxNo));
                com.Parameters.Add(ParameterAdd("@PaxType", data.PaxType));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Action", data.Action));

                int row = com.ExecuteNonQuery();
                return row > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePaxFareAbbNo(PosDS.T_PaxFareRow data)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_PaxFareCurrentUpdate  SET ");
                sb.AppendLine("ABBNo = @ABBNo ");
                sb.AppendLine("WHERE PaxFareTId = @PaxFareTId AND ABBNo IS NULL ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PaxFareTId", data.PaxFareTId));
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));



                int row = com.ExecuteNonQuery();
                return row > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePayment(PosDS.T_PaymentRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_PaymentCurrentUpdate SET ");
                sb.AppendLine("TransactionId = @TransactionId, ");
                sb.AppendLine("PaymentID = @PaymentID, ");
                sb.AppendLine("ReferenceType = @ReferenceType, ");
                sb.AppendLine("ReferenceID = @ReferenceID, ");
                sb.AppendLine("PaymentMethodType = @PaymentMethodType, ");
                sb.AppendLine("PaymentMethodCode = @PaymentMethodCode, ");
                sb.AppendLine("CurrencyCode = @CurrencyCode, ");
                sb.AppendLine("PaymentAmount = @PaymentAmount, ");
                sb.AppendLine("CollectedCurrencyCode = @CollectedCurrencyCode, ");
                sb.AppendLine("CollectedAmount = @CollectedAmount, ");
                sb.AppendLine("QuotedCurrencyCode = @QuotedCurrencyCode, ");
                sb.AppendLine("QuotedAmount = @QuotedAmount, ");
                sb.AppendLine("Status = @Status, ");
                sb.AppendLine("ParentPaymentID = @ParentPaymentID, ");
                sb.AppendLine("AgentCode = @AgentCode, ");
                sb.AppendLine("OrganizationCode = @OrganizationCode, ");
                sb.AppendLine("DomainCode = @DomainCode, ");
                sb.AppendLine("LocationCode = @LocationCode, ");
                sb.AppendLine("ApprovalDate = @ApprovalDate, ");
                sb.AppendLine("ABBNo = @ABBNo, ");
                sb.AppendLine("Update_By = @Update_By, ");
                sb.AppendLine("Update_Date = @Update_Date, ");
                sb.AppendLine("BankName = @BankName, ");
                sb.AppendLine("BranchNo = @BranchNo, ");
                sb.AppendLine("AccountNo = @AccountNo, ");
                sb.AppendLine("AccountNoID = @AccountNoID ");
                sb.AppendLine("WHERE PaymentTId = @PaymentTId  ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@PaymentTId", data.PaymentTId));
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@PaymentID", data.PaymentID));
                com.Parameters.Add(ParameterAdd("@ReferenceType", data.ReferenceType));
                com.Parameters.Add(ParameterAdd("@ReferenceID", data.ReferenceID));
                com.Parameters.Add(ParameterAdd("@PaymentMethodType", data.PaymentMethodType));
                com.Parameters.Add(ParameterAdd("@PaymentMethodCode", data.PaymentMethodCode));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@PaymentAmount", data.PaymentAmount));
                com.Parameters.Add(ParameterAdd("@CollectedCurrencyCode", data.CollectedCurrencyCode));
                com.Parameters.Add(ParameterAdd("@CollectedAmount", data.CollectedAmount));
                com.Parameters.Add(ParameterAdd("@QuotedCurrencyCode", data.QuotedCurrencyCode));
                com.Parameters.Add(ParameterAdd("@QuotedAmount", data.QuotedAmount));
                com.Parameters.Add(ParameterAdd("@Status", data.Status));
                com.Parameters.Add(ParameterAdd("@ParentPaymentID", data.ParentPaymentID));
                com.Parameters.Add(ParameterAdd("@AgentCode", data.AgentCode));
                com.Parameters.Add(ParameterAdd("@OrganizationCode", data.OrganizationCode));
                com.Parameters.Add(ParameterAdd("@DomainCode", data.DomainCode));
                com.Parameters.Add(ParameterAdd("@LocationCode", data.LocationCode));
                com.Parameters.Add(ParameterAdd("@ApprovalDate", data.ApprovalDate));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));

                com.Parameters.Add(ParameterAdd("@BankName", data.BankName));
                com.Parameters.Add(ParameterAdd("@BranchNo", data.BranchNo));
                com.Parameters.Add(ParameterAdd("@AccountNo", data.AccountNo));
                com.Parameters.Add(ParameterAdd("@AccountNoID", data.AccountNoID));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePaymentAbbNo(PosDS.T_PaymentRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_PaymentCurrentUpdate SET ");
                sb.AppendLine("ABBNo = @ABBNo ");
                sb.AppendLine("WHERE PaymentTId = @PaymentTId AND ABBNo IS NULL ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@PaymentTId", data.PaymentTId));
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));



                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool UpdateBookingServiceCharge(PosDS.T_BookingServiceChargeRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE T_BookingServiceChargeCurrentUpdate SET ");
            sb.AppendLine("PaxFareTId= @PaxFareTId, ");
            sb.AppendLine("SeqmentNo= @SeqmentNo, ");
            sb.AppendLine("SeqmentPaxNo= @SeqmentPaxNo, ");
            sb.AppendLine("ServiceChargeNo= @ServiceChargeNo, ");
            sb.AppendLine("PassengerId= @PassengerId, ");
            sb.AppendLine("ChargeType= @ChargeType, ");
            sb.AppendLine("CollectType= @CollectType, ");
            sb.AppendLine("ChargeCode= @ChargeCode, ");
            sb.AppendLine("TicketCode= @TicketCode, ");
            sb.AppendLine("CurrencyCode= @CurrencyCode, ");
            sb.AppendLine("Amount= @Amount, ");
            sb.AppendLine("BaseAmount= @BaseAmount, ");
            sb.AppendLine("ChargeDetail= @ChargeDetail, ");
            sb.AppendLine("ForeignCurrencyCode= @ForeignCurrencyCode, ");
            sb.AppendLine("ForeignAmount= @ForeignAmount, ");
            sb.AppendLine("ABBNo= @ABBNo, ");
            sb.AppendLine("Update_By= @Update_By, ");
            sb.AppendLine("Update_Date= @Update_Date, ");
            sb.AppendLine("FareCreateDate= @FareCreateDate, ");
            sb.AppendLine("SEQ= @SEQ, ");
            sb.AppendLine("QTY= @QTY, ");
            sb.AppendLine("Action= @Action ");
            sb.AppendLine("WHERE BookingServiceChargeTId = @BookingServiceChargeTId  ");

            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@BookingServiceChargeTId", data.BookingServiceChargeTId));
            com.Parameters.Add(ParameterAdd("@PaxFareTId", data.PaxFareTId));
            com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
            com.Parameters.Add(ParameterAdd("@SeqmentPaxNo", data.SeqmentPaxNo));
            com.Parameters.Add(ParameterAdd("@ServiceChargeNo", data.ServiceChargeNo));
            com.Parameters.Add(ParameterAdd("@PassengerId", DBNull.Value)); // ฟิวนี้ไม่ให้แล้ว
            com.Parameters.Add(ParameterAdd("@ChargeType", data.ChargeType));
            com.Parameters.Add(ParameterAdd("@CollectType", data.CollectType));
            com.Parameters.Add(ParameterAdd("@ChargeCode", data.ChargeCode));
            com.Parameters.Add(ParameterAdd("@TicketCode", data.TicketCode));
            com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
            com.Parameters.Add(ParameterAdd("@Amount", data.Amount));
            com.Parameters.Add(ParameterAdd("@BaseAmount", data.BaseAmount));
            com.Parameters.Add(ParameterAdd("@ChargeDetail", data.ChargeDetail));
            com.Parameters.Add(ParameterAdd("@ForeignCurrencyCode", data.ForeignCurrencyCode));
            com.Parameters.Add(ParameterAdd("@ForeignAmount", data.ForeignAmount));
            if (!data.IsABBNoNull())
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
            com.Parameters.Add(ParameterAdd("@FareCreateDate", data.FareCreateDate));
            com.Parameters.Add(ParameterAdd("@SEQ", data.SEQ));
            com.Parameters.Add(ParameterAdd("@QTY", data.QTY));
            com.Parameters.Add(ParameterAdd("@Action", data.Action));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        public bool UpdateBookingServiceChargeAbbNo(PosDS.T_BookingServiceChargeRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE T_BookingServiceChargeCurrentUpdate SET ");
            sb.AppendLine("ABBNo= @ABBNo ");
            sb.AppendLine("WHERE BookingServiceChargeTId = @BookingServiceChargeTId AND ABBNo IS NULL ");

            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@BookingServiceChargeTId", data.BookingServiceChargeTId));
            com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }


        public bool UpdatePassServiceCharge(PosDS.T_PassengerFeeServiceChargeRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Update T_PassengerFeeServiceChargeCurrentUpdate Set ");
            sb.AppendLine("PassengerFeeTId = @PassengerFeeTId, ");
            sb.AppendLine("PassengerId = @PassengerId, ");
            sb.AppendLine("PassengerFeeNo = @PassengerFeeNo, ");
            sb.AppendLine("FeeCreateDate = @FeeCreateDate, ");
            sb.AppendLine("ServiceChargeNo = @ServiceChargeNo, ");
            sb.AppendLine("ChargeType = @ChargeType, ");
            sb.AppendLine("CollectType = @CollectType, ");
            sb.AppendLine("ChargeCode = @ChargeCode, ");
            sb.AppendLine("TicketCode = @TicketCode, ");
            sb.AppendLine("BaseCurrencyCode = @BaseCurrencyCode, ");
            sb.AppendLine("CurrencyCode = @CurrencyCode, ");
            sb.AppendLine("Amount = @Amount, ");
            sb.AppendLine("BaseAmount = @BaseAmount, ");
            sb.AppendLine("ChargeDetail = @ChargeDetail, ");
            sb.AppendLine("ForeignCurrencyCode = @ForeignCurrencyCode, ");
            sb.AppendLine("ForeignAmount = @ForeignAmount, ");
            sb.AppendLine("ABBNo = @ABBNo, ");
            sb.AppendLine("Update_By = @Update_By, ");
            sb.AppendLine("Update_Date = @Update_Date, ");
            sb.AppendLine("SEQ = @SEQ, ");
            sb.AppendLine("Action = @Action ");
            sb.AppendLine("WHERE PassengerFeeServiceChargeTId = @PassengerFeeServiceChargeTId ");

            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Clear();

            com.Parameters.Add(ParameterAdd("@PassengerFeeServiceChargeTId", data.PassengerFeeServiceChargeTId));
            com.Parameters.Add(ParameterAdd("@PassengerFeeTId", data.PassengerFeeTId));
            com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
            com.Parameters.Add(ParameterAdd("@PassengerFeeNo", data.PassengerFeeNo));
            com.Parameters.Add(ParameterAdd("@FeeCreateDate", data.FeeCreateDate));
            com.Parameters.Add(ParameterAdd("@ServiceChargeNo", data.ServiceChargeNo));
            com.Parameters.Add(ParameterAdd("@ChargeType", data.ChargeType));
            com.Parameters.Add(ParameterAdd("@CollectType", data.CollectType));
            com.Parameters.Add(ParameterAdd("@ChargeCode", data.ChargeCode));
            com.Parameters.Add(ParameterAdd("@TicketCode", data.TicketCode));
            com.Parameters.Add(ParameterAdd("@BaseCurrencyCode", data.BaseCurrencyCode));
            com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
            com.Parameters.Add(ParameterAdd("@Amount", data.Amount));
            com.Parameters.Add(ParameterAdd("@BaseAmount", data.BaseAmount));
            com.Parameters.Add(ParameterAdd("@ChargeDetail", data.ChargeDetail));
            com.Parameters.Add(ParameterAdd("@ForeignCurrencyCode", data.ForeignCurrencyCode));
            com.Parameters.Add(ParameterAdd("@ForeignAmount", data.ForeignAmount));
            if (!data.IsABBNoNull())
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
            com.Parameters.Add(ParameterAdd("@SEQ", data.SEQ));
            com.Parameters.Add(ParameterAdd("@Action", data.Action));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        public bool UpdatePassServiceChargeAbbNo(PosDS.T_PassengerFeeServiceChargeRow data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Update T_PassengerFeeServiceChargeCurrentUpdate Set ");
            sb.AppendLine("ABBNo = @ABBNo ");
            sb.AppendLine("WHERE PassengerFeeTId = @PassengerFeeTId AND ABBNo IS NULL ");

            SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
            com.Transaction = SqlTran;
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Clear();

            com.Parameters.Add(ParameterAdd("@PassengerFeeTId", data.PassengerFeeTId));
            com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));

            int row = com.ExecuteNonQuery();
            return row > 0;
        }


        public bool UpdateSegment(PosDS.T_SegmentRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_SegmentCurrentUpdate SET ");
                sb.AppendLine("TransactionId = @TransactionId, ");
                sb.AppendLine("SeqmentNo = @SeqmentNo, ");
                sb.AppendLine("DepartureStation = @DepartureStation, ");
                sb.AppendLine("ArrivalStation = @ArrivalStation, ");
                sb.AppendLine("STD = @STD, ");
                sb.AppendLine("STA = @STA, ");
                sb.AppendLine("CarrierCode = @CarrierCode, ");
                sb.AppendLine("FlightNumber = @FlightNumber, ");
                sb.AppendLine("ABBNo = @ABBNo, ");
                sb.AppendLine("Update_By = @Update_By, ");
                sb.AppendLine("Update_Date = @Update_Date, ");
                sb.AppendLine("Action = @Action ");
                sb.AppendLine("WHERE SegmentTId = @SegmentTId ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@SegmentTId", data.SegmentTId));
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
                com.Parameters.Add(ParameterAdd("@DepartureStation", data.DepartureStation));
                com.Parameters.Add(ParameterAdd("@ArrivalStation", data.ArrivalStation));
                com.Parameters.Add(ParameterAdd("@STD", data.STD));
                com.Parameters.Add(ParameterAdd("@STA", data.STA));
                com.Parameters.Add(ParameterAdd("@CarrierCode", data.CarrierCode));
                com.Parameters.Add(ParameterAdd("@FlightNumber", data.FlightNumber));
                if (!data.IsABBNoNull())
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Action", data.Action));


                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateSegmentAbbNo(PosDS.T_SegmentRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_SegmentCurrentUpdate SET ");
                sb.AppendLine("ABBNo = @ABBNo ");
                sb.AppendLine("WHERE SegmentTId = @SegmentTId AND ABBNo IS NULL ");

                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(ParameterAdd("@SegmentTId", data.SegmentTId));
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));


                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool InsertABB(PosDS.T_ABBRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_ABB  ");
                sb.Append("(ABBTid,TransactionId,PNR_No,TaxInvoiceNo,PAXCountADT,PAXCountCHD,ADMINFEE ,Fuel,Service,OTHER,VAT,AIRPORTTAX,INSURANCE,REP,TOTAL,ExchangeRateTH,PrintCount,STATUS_INV,Create_By,Create_Date,Update_By,Update_Date,Discount,POS_Id,ADMINFEE_Ori,Fuel_Ori ,Service_Ori,OTHER_Ori,VAT_Ori,AIRPORTTAX_Ori,INSURANCE_Ori,REP_Ori,TOTAL_Ori,Discount_Ori) VALUES");
                sb.Append("(@ABBTid,@TransactionId,@PNR_No,@TaxInvoiceNo,@PAXCountADT,@PAXCountCHD,@ADMINFEE ,@Fuel,@Service,@OTHER,@VAT,@AIRPORTTAX,@INSURANCE,@REP,@TOTAL,@ExchangeRateTH,@PrintCount,@STATUS_INV,@Create_By,@Create_Date,@Update_By,@Update_Date,@Discount,@POS_Id,@ADMINFEE_Ori,@Fuel_Ori ,@Service_Ori,@OTHER_Ori,@VAT_Ori,@AIRPORTTAX_Ori,@INSURANCE_Ori,@REP_Ori,@TOTAL_Ori,@Discount_Ori) ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBTid", data.ABBTid));
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@TaxInvoiceNo", data.TaxInvoiceNo));
                com.Parameters.Add(ParameterAdd("@PAXCountADT", data.PAXCountADT));
                com.Parameters.Add(ParameterAdd("@PAXCountCHD", data.PAXCountCHD));
                com.Parameters.Add(ParameterAdd("@ADMINFEE", data.ADMINFEE));
                com.Parameters.Add(ParameterAdd("@Fuel", data.Fuel));
                com.Parameters.Add(ParameterAdd("@Service", data.Service));
                com.Parameters.Add(ParameterAdd("@OTHER", data.OTHER));
                com.Parameters.Add(ParameterAdd("@VAT", data.VAT));
                com.Parameters.Add(ParameterAdd("@AIRPORTTAX", data.AIRPORTTAX));
                com.Parameters.Add(ParameterAdd("@INSURANCE", data.INSURANCE));
                com.Parameters.Add(ParameterAdd("@REP", data.REP));
                com.Parameters.Add(ParameterAdd("@TOTAL", data.TOTAL));
                com.Parameters.Add(ParameterAdd("@ExchangeRateTH", data.ExchangeRateTH));
                com.Parameters.Add(ParameterAdd("@PrintCount", data.PrintCount));
                com.Parameters.Add(ParameterAdd("@STATUS_INV", data.STATUS_INV));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Discount", data.Discount));
                com.Parameters.Add(ParameterAdd("@POS_Id", data.POS_Id));

                com.Parameters.Add(ParameterAdd("@ADMINFEE_Ori", data.ADMINFEE_Ori));
                com.Parameters.Add(ParameterAdd("@Fuel_Ori", data.Fuel_Ori));
                com.Parameters.Add(ParameterAdd("@Service_Ori", data.Service_Ori));
                com.Parameters.Add(ParameterAdd("@OTHER_Ori", data.OTHER_Ori));
                com.Parameters.Add(ParameterAdd("@VAT_Ori", data.VAT_Ori));
                com.Parameters.Add(ParameterAdd("@AIRPORTTAX_Ori", data.AIRPORTTAX_Ori));
                com.Parameters.Add(ParameterAdd("@INSURANCE_Ori", data.INSURANCE_Ori));
                com.Parameters.Add(ParameterAdd("@REP_Ori", data.REP_Ori));
                com.Parameters.Add(ParameterAdd("@TOTAL_Ori", data.TOTAL_Ori));
                com.Parameters.Add(ParameterAdd("@Discount_Ori", data.Discount_Ori));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertABBPAX(PosDS.T_ABBPAXRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_ABBPAX  ");
                sb.Append("(ABBPAXId,ABBTid,PNR_No,SEQ,FirstName,LastName,Create_By,Create_Date,Update_By,Update_Date) VALUES");
                sb.Append("(@ABBPAXId,@ABBTid,@PNR_No,@SEQ,@FirstName,@LastName,@Create_By,@Create_Date,@Update_By,@Update_Date) ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBPAXId", data.ABBPAXId));
                com.Parameters.Add(ParameterAdd("@ABBTid", data.ABBTid));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@SEQ", data.SEQ));
                com.Parameters.Add(ParameterAdd("@FirstName", data.FirstName));
                com.Parameters.Add(ParameterAdd("@LastName", data.LastName));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertABBFARE(PosDS.T_ABBFARERow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_ABBFARE  ");
                sb.Append("(ABBFAREId,ABBTid,PNR_No,SEQ,CarrierCode ,FlightNumber,DepartureStation,ArrivalStation,STD,STA,Amount,CurrencyCode,Create_By,Create_Date,Update_By,Update_Date,Amount_Ori,ExchangeRateTH) VALUES");
                sb.Append("(@ABBFAREId,@ABBTid,@PNR_No,@SEQ,@CarrierCode ,@FlightNumber,@DepartureStation,@ArrivalStation,@STD,@STA,@Amount,@CurrencyCode,@Create_By,@Create_Date,@Update_By,@Update_Date,@Amount_Ori,@ExchangeRateTH) ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBFAREId", data.ABBFAREId));
                com.Parameters.Add(ParameterAdd("@ABBTid", data.ABBTid));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@SEQ", data.SEQ));
                com.Parameters.Add(ParameterAdd("@CarrierCode", data.CarrierCode));
                com.Parameters.Add(ParameterAdd("@FlightNumber", data.FlightNumber));
                com.Parameters.Add(ParameterAdd("@DepartureStation", data.DepartureStation));
                com.Parameters.Add(ParameterAdd("@ArrivalStation", data.ArrivalStation));
                com.Parameters.Add(ParameterAdd("@STD", data.STD));
                com.Parameters.Add(ParameterAdd("@STA", data.STA));
                com.Parameters.Add(ParameterAdd("@Amount", data.Amount));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Amount_Ori", data.Amount_Ori));
                com.Parameters.Add(ParameterAdd("@ExchangeRateTH", data.ExchangeRateTH));
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertABBPAYMENTMETHOD(PosDS.T_ABBPAYMENTMETHODRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_ABBPAYMENTMETHOD  ");
                sb.Append("( ABBPAYId ,ABBTid,PNR_No,SEQ,PaymentMethodCode,PaymentType_Name,PaymentAmount,CurrencyCode,Create_By,Create_Date,Update_By,Update_Date,ExchangeRateTH,PaymentAmount_Ori,AgentCode,ApprovalDate) VALUES");
                sb.Append("( @ABBPAYId ,@ABBTid,@PNR_No,@SEQ,@PaymentMethodCode,@PaymentType_Name,@PaymentAmount,@CurrencyCode,@Create_By,@Create_Date,@Update_By,@Update_Date,@ExchangeRateTH,@PaymentAmount_Ori,@AgentCode,@ApprovalDate) ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBPAYId", data.ABBPAYId));
                com.Parameters.Add(ParameterAdd("@ABBTid", data.ABBTid));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@SEQ", data.SEQ));
                com.Parameters.Add(ParameterAdd("@PaymentMethodCode", data.PaymentMethodCode));
                com.Parameters.Add(ParameterAdd("@PaymentType_Name", data.PaymentType_Name));
                com.Parameters.Add(ParameterAdd("@PaymentAmount", data.PaymentAmount));
                com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));

                com.Parameters.Add(ParameterAdd("@ExchangeRateTH", data.ExchangeRateTH));
                com.Parameters.Add(ParameterAdd("@PaymentAmount_Ori", data.PaymentAmount_Ori));
                com.Parameters.Add(ParameterAdd("@AgentCode", data.AgentCode));
                com.Parameters.Add(ParameterAdd("@ApprovalDate", data.ApprovalDate));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertLogsGenABB(PosDS.T_LogsGenABBRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_LogsGenABB  ");
                sb.Append("( LogId,TransactionId,PNR_No,Msg,Create_By,Create_Date,Update_By ,Update_Date) VALUES");
                sb.Append("( @LogId,@TransactionId,@PNR_No,@Msg,@Create_By,@Create_Date,@Update_By ,@Update_Date) ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@LogId", data.LogId));
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@Msg", data.Msg));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public bool InsertWaitingForManualGenABB(PosDS.T_WaitingForManualGenABBRow data)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_WaitingForManualGenABB  ");
                sb.Append("( TransactionId,PNR_No,Msg,Create_By,Create_Date,Update_By,Update_Date) VALUES");
                sb.Append("( @TransactionId,@PNR_No,@Msg,@Create_By,@Create_Date,@Update_By,@Update_Date) ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@Msg", data.Msg));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));

                com.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteWaitingForManualGenABB_ByPNR_No(string PNR_No)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE T_WaitingForManualGenABB  WHERE PNR_No = @PNR_No");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PNR_No", PNR_No));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateABB(PosDS.T_ABBRow data)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE T_ABB  SET ");
                sb.AppendLine("PAXCountADT = @PAXCountADT, ");
                sb.AppendLine("PAXCountCHD = @PAXCountCHD, ");
                sb.AppendLine("ADMINFEE = @ADMINFEE, ");
                sb.AppendLine("Fuel = @Fuel, ");
                sb.AppendLine("Service = @Service, ");
                sb.AppendLine("OTHER = @OTHER, ");
                sb.AppendLine("VAT = @VAT, ");
                sb.AppendLine("AIRPORTTAX = @AIRPORTTAX, ");
                sb.AppendLine("INSURANCE = @INSURANCE, ");
                sb.AppendLine("REP = @REP, ");
                sb.AppendLine("TOTAL = @TOTAL, ");
                sb.AppendLine("Update_By = @Update_By, ");
                sb.AppendLine("Update_Date = @Update_Date, ");
                sb.AppendLine("Discount = @Discount, ");
                sb.AppendLine("POS_Id = @POS_Id ");
                sb.AppendLine("WHERE ABBTid = @ABBTid ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBTid", data.ABBTid));
                com.Parameters.Add(ParameterAdd("@PAXCountADT", data.PAXCountADT));
                com.Parameters.Add(ParameterAdd("@PAXCountCHD", data.PAXCountCHD));
                com.Parameters.Add(ParameterAdd("@ADMINFEE", data.ADMINFEE));
                com.Parameters.Add(ParameterAdd("@Fuel", data.Fuel));
                com.Parameters.Add(ParameterAdd("@Service", data.Service));
                com.Parameters.Add(ParameterAdd("@OTHER", data.OTHER));
                com.Parameters.Add(ParameterAdd("@VAT", data.VAT));
                com.Parameters.Add(ParameterAdd("@AIRPORTTAX", data.AIRPORTTAX));
                com.Parameters.Add(ParameterAdd("@INSURANCE", data.INSURANCE));
                com.Parameters.Add(ParameterAdd("@REP", data.REP));
                com.Parameters.Add(ParameterAdd("@TOTAL", data.TOTAL));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                com.Parameters.Add(ParameterAdd("@Update_Date", data.Update_Date));
                com.Parameters.Add(ParameterAdd("@Discount", data.Discount));
                com.Parameters.Add(ParameterAdd("@POS_Id", data.POS_Id));
                int r = com.ExecuteNonQuery();
                return r > -1;
            }
            catch (Exception)
            {
                throw;
            }
        }



        static public bool UpdateAgencyType(int AgencyTypeId, long Last_Inv_No)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE M_AgencyType SET Last_Inv_No = @Last_Inv_No WHERE AgencyTypeId = @AgencyTypeId ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(new SqlParameter("@AgencyTypeId", AgencyTypeId));
                com.Parameters.Add(new SqlParameter("@Last_Inv_No", Last_Inv_No));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public bool UpdatePOSMachine(string POS_Code, long Last_Inv_No)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE M_POS_Machine SET Last_Inv_No = @Last_Inv_No WHERE POS_Code = @POS_Code ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;

                com.Parameters.Add(new SqlParameter("@POS_Code", POS_Code));
                com.Parameters.Add(new SqlParameter("@Last_Inv_No", Last_Inv_No));

                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public bool DeleteAllByPNR_No(string PNR_No)
        {
            try
            {

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand("sp_DeleteAllDataByPNR_No", conn);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.Add(new SqlParameter("@PNR_No", PNR_No));
                int r = com.ExecuteNonQuery();
                return r > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteAbbPaxByABBTid(Guid ABBTid)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("DELETE  T_ABBPAX ");
                sb.AppendLine("WHERE ABBTid = @ABBTid ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBTid", ABBTid));
                int r = com.ExecuteNonQuery();
                return r > -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteT_ABBPAYMENTMETHODByABBTid(Guid ABBTid)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("DELETE  T_ABBPAYMENTMETHOD ");
                sb.AppendLine("WHERE ABBTid = @ABBTid ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBTid", ABBTid));
                int r = com.ExecuteNonQuery();
                return r > -1;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public bool DeleteABBFareByABBTid(Guid ABBTid)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("DELETE  T_ABBFARE ");
                sb.AppendLine("WHERE ABBTid = @ABBTid ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ABBTid", ABBTid));
                int r = com.ExecuteNonQuery();
                return r > -1;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public bool InsertFlightFee(string Flight_Fee_Code ,string UserCode)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO M_FlightFee  ");
                sb.Append("(Flight_Fee_Code,Flight_Fee_Name,DisplayAbb,AbbGroup,IsActive,Create_By,Create_Date,Update_By ,Update_Date) VALUES");
                sb.Append("(@Flight_Fee_Code,@Flight_Fee_Name,@DisplayAbb,@AbbGroup,@IsActive,@Create_By,@Create_Date,@Update_By ,@Update_Date) ");
                SqlCommand com = new SqlCommand(sb.ToString(), Sqlconn);
                com.Transaction = SqlTran;
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Flight_Fee_Code", Flight_Fee_Code));
                com.Parameters.Add(ParameterAdd("@Flight_Fee_Name", Flight_Fee_Code));
                com.Parameters.Add(ParameterAdd("@DisplayAbb", "Y"));
                com.Parameters.Add(ParameterAdd("@AbbGroup", "SERVICE"));
                com.Parameters.Add(ParameterAdd("@IsActive", "Y"));
                com.Parameters.Add(ParameterAdd("@Create_By", UserCode));
                com.Parameters.Add(ParameterAdd("@Create_Date", DateTime.Now));
                com.Parameters.Add(ParameterAdd("@Update_By", UserCode));
                com.Parameters.Add(ParameterAdd("@Update_Date", DateTime.Now));
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public bool InsertAgency(string Agency_Code, string Agency_Type_Code, string UserCode)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("INSERT INTO M_Agency  ");
                sb.AppendLine("(Agency_Code,Agency_Name,GenInvoice,IsActive,Create_By,Create_Date,Update_By,Update_Date,Agency_Type_Code) VALUES");
                sb.AppendLine("(@Agency_Code,@Agency_Name,@GenInvoice,@IsActive,@Create_By,@Create_Date,@Update_By,@Update_Date,@Agency_Type_Code) ");
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Agency_Code", Agency_Code));
                com.Parameters.Add(ParameterAdd("@Agency_Name", Agency_Code));
                com.Parameters.Add(ParameterAdd("@GenInvoice", "Y"));
                com.Parameters.Add(ParameterAdd("@IsActive", "Y"));
                com.Parameters.Add(ParameterAdd("@Create_By", UserCode));
                com.Parameters.Add(ParameterAdd("@Create_Date", DateTime.Now));
                com.Parameters.Add(ParameterAdd("@Update_By", UserCode));
                com.Parameters.Add(ParameterAdd("@Update_Date", DateTime.Now));
                com.Parameters.Add(ParameterAdd("@Agency_Type_Code", Agency_Type_Code));
                com.ExecuteNonQuery();
                conn.Close();


                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public bool UpdateAgencyType(string Agency_Code, string Agency_Type_Code, string UserCode)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("UPDATE M_Agency SET ");
                sb.AppendLine("Agency_Type_Code = @Agency_Type_Code, ");
                sb.AppendLine("GenInvoice = @GenInvoice, ");
                sb.AppendLine("Update_By = @Update_By,");
                sb.AppendLine("Update_Date = @Update_Date");
                sb.AppendLine("WHERE Agency_Code = @Agency_Code AND IsActive = 'Y'");
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Agency_Code", Agency_Code));
                com.Parameters.Add(ParameterAdd("@Agency_Type_Code", Agency_Type_Code));
                com.Parameters.Add(ParameterAdd("@GenInvoice", "Y"));
                com.Parameters.Add(ParameterAdd("@Update_By", UserCode));
                com.Parameters.Add(ParameterAdd("@Update_Date", DateTime.Now));

                com.ExecuteNonQuery();
                conn.Close();


                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


    }
}