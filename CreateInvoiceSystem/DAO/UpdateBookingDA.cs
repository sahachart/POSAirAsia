using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CreateInvoiceSystem.DAO
{
    static public class UpdateBookingDA
    {
        static private string ConnectionString = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();

        static public long GetGenId(string Prefix, string POS_Code, string Station_Code, string UpdateBy)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand("sp_GetGenId", conn);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.Add(ParameterAdd("@Prefix", Prefix));
                com.Parameters.Add(ParameterAdd("@POS_Code", POS_Code));
                com.Parameters.Add(ParameterAdd("@Station_Code", Station_Code));
                com.Parameters.Add(ParameterAdd("@DocDate", DateTime.Now));
                com.Parameters.Add(ParameterAdd("@UpdateBy", UpdateBy));

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        if (result.Rows.Count > 0)
                        {
                            return Convert.ToInt64(result.Rows[0]["DocNo"]);
                        }
                    }
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Fligth


        static public bool InsertFlight(T_SegmentCurrentUpdate data, ref string strExec)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_SegmentCurrentUpdate ");
                sb.Append("(TransactionId, SeqmentNo, DepartureStation, ArrivalStation, STD, STA, CarrierCode, FlightNumber, ABBNo, Create_By, Create_Date, Update_By, Update_Date) VALUES");
                sb.Append("(@TransactionId, @SeqmentNo, @DepartureStation, @ArrivalStation, @STD, @STA, @CarrierCode, @FlightNumber, @ABBNo, @Create_By, GETDATE(), @Update_By, GETDATE()) ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
                com.Parameters.Add(ParameterAdd("@DepartureStation", data.DepartureStation));
                com.Parameters.Add(ParameterAdd("@ArrivalStation", data.ArrivalStation));
                com.Parameters.Add(ParameterAdd("@STD", data.STD));
                com.Parameters.Add(ParameterAdd("@STA", data.STA));
                com.Parameters.Add(ParameterAdd("@CarrierCode", data.CarrierCode));
                com.Parameters.Add(ParameterAdd("@FlightNumber", data.FlightNumber));
                if (!string.IsNullOrEmpty(data.ABBNo))
                    com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                else
                    com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
                strExec = CommandAsSql(com);
                int row = com.ExecuteNonQuery();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        static public bool UpdateFlight(T_SegmentCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE T_SegmentCurrentUpdate SET ");
            //sb.Append("SeqmentNo = @SeqmentNo, ");
            sb.Append("CarrierCode = @CarrierCode, ");
            sb.Append("FlightNumber = @FlightNumber, ");
            sb.Append("DepartureStation = @DepartureStation, ");
            sb.Append("ArrivalStation = @ArrivalStation, ");
            sb.Append("STD = @STD, ");
            sb.Append("STA = @STA, ");
            sb.Append("ABBNo = @ABBNo ");
            sb.Append("WHERE SegmentTId = @SegmentTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@SegmentTId", data.SegmentTId));
            //com.Parameters.Add(ParameterAdd("@SeqmentNo", data.SeqmentNo));
            com.Parameters.Add(ParameterAdd("@CarrierCode", data.CarrierCode));
            com.Parameters.Add(ParameterAdd("@FlightNumber", data.FlightNumber));
            com.Parameters.Add(ParameterAdd("@DepartureStation", data.DepartureStation));
            com.Parameters.Add(ParameterAdd("@ArrivalStation", data.ArrivalStation));
            com.Parameters.Add(ParameterAdd("@STD", data.STD));
            com.Parameters.Add(ParameterAdd("@STA", data.STA));
            if (!string.IsNullOrEmpty(data.ABBNo))
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));

            strExec = CommandAsSql(com);
            
            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        static public bool DeleteFlight(T_SegmentCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE T_SegmentCurrentUpdate  ");
            sb.Append("WHERE SegmentTId = @SegmentTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@SegmentTId", data.SegmentTId));
            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }
        #endregion

        #region Passenger
        static public bool InsertPassenger(T_PassengerCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO T_PassengerCurrentUpdate  ");
            sb.Append("(TransactionId, PassengerId, FirstName, LastName, Title, PaxType, ABBNo, Create_By, Create_Date, Update_By, Update_Date) VALUES");
            sb.Append("(@TransactionId, @PassengerId, @FirstName, @LastName, @Title, @PaxType, @ABBNo, @Create_By, GETDATE(),@Update_By, GETDATE()) ");


            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
            com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
            com.Parameters.Add(ParameterAdd("@FirstName", data.FirstName));
            com.Parameters.Add(ParameterAdd("@LastName", data.LastName));
            com.Parameters.Add(ParameterAdd("@Title", data.Title));
            com.Parameters.Add(ParameterAdd("@PaxType", data.PaxType));
            if (!string.IsNullOrEmpty(data.ABBNo))
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));

            int row = com.ExecuteNonQuery();
            strExec = CommandAsSql(com);
            return row > 0;
        }


        static public bool UpdatePassenger(T_PassengerCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE T_PassengerCurrentUpdate SET ");
            sb.Append("Title = @Title, ");
            sb.Append("FirstName = @FirstName, ");
            sb.Append("LastName = @LastName, ");
            sb.Append("PaxType = @PaxType, ");
            sb.Append("ABBNo = @ABBNo ");
            sb.Append("WHERE PassengerTId = @PassengerTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PassengerTId", data.PassengerTId));
            com.Parameters.Add(ParameterAdd("@Title", data.Title));
            com.Parameters.Add(ParameterAdd("@FirstName", data.FirstName));
            com.Parameters.Add(ParameterAdd("@LastName", data.LastName));
            com.Parameters.Add(ParameterAdd("@PaxType", data.PaxType));
            if (!string.IsNullOrEmpty(data.ABBNo))
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            int row = com.ExecuteNonQuery();
            strExec = CommandAsSql(com);
            return row > 0;
        }

        static public bool DeletePassenger(T_PassengerCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE T_PassengerCurrentUpdate  ");
            sb.Append("WHERE PassengerTId = @PassengerTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PassengerTId", data.PassengerTId));
            int row = com.ExecuteNonQuery();
            strExec = CommandAsSql(com);
            return row > 0;
        }

        static public bool ManagePaxFare(string BookingNo, string User)
        {

            try
            {
                var context = new POSINVEntities();

                var Booking = context.T_BookingCurrentUpdate.First(x => x.PNR_No == BookingNo);
                var PaxType = context.T_PassengerCurrentUpdate.Where(x => x.TransactionId == Booking.TransactionId).GroupBy(s => s.PaxType);
                var Segments = context.T_SegmentCurrentUpdate.Where(x => x.TransactionId == Booking.TransactionId).ToList();

                foreach (var Segment in Segments)
                {
                    if (context.T_PaxFareCurrentUpdate.Count(x => x.SegmentTId == Segment.SegmentTId) != PaxType.Count())
                    {
                        if (context.T_PaxFareCurrentUpdate.Count(x => x.SegmentTId == Segment.SegmentTId && x.PaxType == "ADT") == 0 && PaxType.Count(x => x.Key == "ADT") > 0)
                        {
                            context.T_PaxFareCurrentUpdate.Add(new T_PaxFareCurrentUpdate()
                            {
                                PaxFareTId = Guid.NewGuid(),
                                SegmentTId = Segment.SegmentTId,
                                SeqmentNo = Segment.SeqmentNo,
                                SeqmentPaxNo = Segment.SeqmentNo + "ADT",
                                PaxType = "ADT",
                                ABBNo = Segment.ABBNo,
                                Create_By = User,
                                Create_Date = DateTime.Now,
                                Update_By = User,
                                Update_Date = DateTime.Now,
                                Action = 0
                            });
                        }

                        if (context.T_PaxFareCurrentUpdate.Count(x => x.SegmentTId == Segment.SegmentTId && x.PaxType == "CHD") == 0 && PaxType.Count(x => x.Key == "CHD") > 0)
                        {
                            context.T_PaxFareCurrentUpdate.Add(new T_PaxFareCurrentUpdate()
                            {
                                PaxFareTId = Guid.NewGuid(),
                                SegmentTId = Segment.SegmentTId,
                                SeqmentNo = Segment.SeqmentNo,
                                SeqmentPaxNo = Segment.SeqmentNo + "CHD",
                                PaxType = "CHD",
                                ABBNo = null,
                                Create_By = User,
                                Create_Date = DateTime.Now,
                                Update_By = User,
                                Update_Date = DateTime.Now,
                                Action = 0
                            });
                        }

                        context.SaveChanges();
                    }
                }

                context.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region FEE

        static public bool InsertFare(FeeDetail data, ref string strExec)
        {
            try
            {
                var context = new POSINVEntities();

                var PaxFareTId = Guid.Parse(data.PaxFareTId);
                var PaxFare = context.T_PaxFareCurrentUpdate.FirstOrDefault(x => x.PaxFareTId == PaxFareTId);
                var Seq = context.T_BookingServiceChargeCurrentUpdate.Where(x => x.PaxFareTId == PaxFareTId).Max(m => m.SEQ) + 1;

                var BookingServiceCharge = new T_BookingServiceChargeCurrentUpdate()
                {
                    BookingServiceChargeTId = Guid.NewGuid(),
                    PaxFareTId = PaxFareTId,

                    ChargeType = data.ChargeType,
                    CollectType = data.CollectType,
                    ChargeCode = data.ChargeCode,
                    TicketCode = data.TicketCode,
                    CurrencyCode = data.CurrencyCode,
                    Amount = data.Amount.GetValueOrDefault(),
                    BaseAmount = data.BaseAmount.GetValueOrDefault(),
                    ChargeDetail = data.ChargeDetail,
                    ForeignCurrencyCode = data.ForeignCurrencyCode,
                    ForeignAmount = data.ForeignAmount,
                    ABBNo = string.IsNullOrEmpty(data.TaxInvoiceNo) ? null : data.TaxInvoiceNo,
                    Create_By = data.Create_By,
                    Create_Date = DateTime.Now,
                    Update_By = data.Update_By,
                    Update_Date = DateTime.Now,
                    FareCreateDate = DateTime.Now,
                    SEQ = Seq,
                    QTY = data.qty,
                    Action = 0

                };

                if (PaxFare != null)
                {
                    BookingServiceCharge.SeqmentNo = PaxFare.SeqmentNo;
                    BookingServiceCharge.SeqmentPaxNo = PaxFare.SeqmentPaxNo;
                    BookingServiceCharge.ServiceChargeNo = PaxFare.SeqmentPaxNo + PaxFare.PaxType + data.Amount.GetValueOrDefault().ToString();
                }

                context.T_BookingServiceChargeCurrentUpdate.Add(BookingServiceCharge);
                context.SaveChanges();
                context.Dispose();

                strExec = "InsertFare Success";
                return true;
            }
            catch (Exception ex)
            {
                strExec = "InsertFare Error" + ex;
                return false;
            }
        }

        static public bool UpdateFare(FeeDetail data, ref string strExec)
        {
            try
            {
                var context = new POSINVEntities();

                var BookingServiceCharge = context.T_BookingServiceChargeCurrentUpdate.FirstOrDefault(x => x.BookingServiceChargeTId == data.RowID);

                context.T_BookingServiceChargeCurrentUpdate.Attach(BookingServiceCharge);

                BookingServiceCharge.ChargeType = data.ChargeType;
                BookingServiceCharge.CollectType = data.CollectType;
                BookingServiceCharge.ChargeCode = data.ChargeCode;
                BookingServiceCharge.TicketCode = data.TicketCode;
                BookingServiceCharge.CurrencyCode = data.CurrencyCode;
                BookingServiceCharge.Amount = data.Amount.GetValueOrDefault();
                BookingServiceCharge.ChargeDetail = data.ChargeDetail;
                BookingServiceCharge.ForeignCurrencyCode = data.ForeignCurrencyCode;
                BookingServiceCharge.ForeignAmount = data.ForeignAmount;
                BookingServiceCharge.ABBNo = string.IsNullOrEmpty(data.TaxInvoiceNo) ? null : data.TaxInvoiceNo;
                BookingServiceCharge.Update_By = data.Update_By;
                BookingServiceCharge.Update_Date = DateTime.Now;
                BookingServiceCharge.QTY = data.qty;

                context.SaveChanges();
                context.Dispose();

                strExec = "InsertFare Success";
                return true;
            }
            catch (Exception ex)
            {
                strExec = "UpdateFare Error" + ex;
                return false;
            }
        }

        static public bool DeleteFare(FeeDetail data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE T_BookingServiceChargeCurrentUpdate  ");
            sb.Append("WHERE BookingServiceChargeTId = @BookingServiceChargeTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@BookingServiceChargeTId", data.RowID));

            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        static public bool InsertFeeServiceCharge(FeeDetail data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DECLARE @SEQ int = (SELECT ISNULL(Max(SEQ), -1) + 1 FROM T_PassengerFeeServiceChargeCurrentUpdate WHERE PassengerId = @PassengerId) ");
            sb.Append("INSERT INTO T_PassengerFeeServiceChargeCurrentUpdate ");
            sb.Append(" (PassengerFeeTId,PassengerId,PassengerFeeNo,FeeCreateDate,ServiceChargeNo,ChargeType,CollectType,ChargeCode,TicketCode,BaseCurrencyCode,CurrencyCode,Amount,BaseAmount,ChargeDetail,ForeignCurrencyCode,ForeignAmount,ABBNo,Create_By,Create_Date,Update_By,Update_Date,SEQ,Action ) VALUES ");
            sb.Append(" (@PassengerFeeTId,@PassengerId,@PassengerFeeNo,@FeeCreateDate,@ServiceChargeNo,@ChargeType,@CollectType,@ChargeCode,@TicketCode,@BaseCurrencyCode,@CurrencyCode,@Amount,@BaseAmount,@ChargeDetail,@ForeignCurrencyCode,@ForeignAmount,@ABBNo,@Create_By,GETDATE(),@Update_By,GETDATE(),@SEQ,0 )");


            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Clear();
            if (data.PaxFareTId != null)
                com.Parameters.Add(ParameterAdd("@PassengerFeeTId", data.PaxFareTId));
            else
                com.Parameters.Add(ParameterAdd("@PassengerFeeTId", DBNull.Value));

            if (data.PassengerId != null)
                com.Parameters.Add(ParameterAdd("@PassengerId", data.PassengerId));
            else
                com.Parameters.Add(ParameterAdd("@PassengerId", DBNull.Value));

            if (data.PassengerFeeNo != null)
                com.Parameters.Add(ParameterAdd("@PassengerFeeNo", data.PassengerFeeNo));
            else
                com.Parameters.Add(ParameterAdd("@PassengerFeeNo", DBNull.Value));

            if (data.FeeCreateDate != null)
                com.Parameters.Add(ParameterAdd("@FeeCreateDate", data.FeeCreateDate));
            else
                com.Parameters.Add(ParameterAdd("@FeeCreateDate", DBNull.Value));

            if (data.ServiceChargeNo != null)
                com.Parameters.Add(ParameterAdd("@ServiceChargeNo", data.ServiceChargeNo));
            else
                com.Parameters.Add(ParameterAdd("@ServiceChargeNo", DBNull.Value));

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

            if (!string.IsNullOrEmpty(data.TaxInvoiceNo))
                com.Parameters.Add(ParameterAdd("@ABBNo", data.TaxInvoiceNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));

            com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));

            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }


        

        static public bool UpdateFeeServiceCharge(FeeDetail data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE T_PassengerFeeServiceChargeCurrentUpdate SET ");
            sb.Append("ChargeType = @ChargeType, ");
            sb.Append("ChargeCode = @ChargeCode, ");
            sb.Append("ChargeDetail = @ChargeDetail, ");
            sb.Append("TicketCode = @TicketCode, ");
            sb.Append("CollectType = @CollectType, ");
            sb.Append("CurrencyCode = @CurrencyCode, ");
            sb.Append("Amount = @Amount, ");
            sb.Append("ForeignCurrencyCode = @ForeignCurrencyCode, ");
            sb.Append("ForeignAmount = @ForeignAmount, ");
            sb.Append("Update_By = @Update_By, ");
            sb.Append("Update_Date = GETDATE(), ");
            sb.Append("ABBNo = @ABBNo ");
            sb.Append("WHERE PassengerFeeServiceChargeTId = @PassengerFeeServiceChargeTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PassengerFeeServiceChargeTId", data.RowID));
            com.Parameters.Add(ParameterAdd("@ChargeType", data.ChargeType));
            com.Parameters.Add(ParameterAdd("@ChargeCode", data.ChargeCode));
            com.Parameters.Add(ParameterAdd("@ChargeDetail", data.ChargeDetail));
            com.Parameters.Add(ParameterAdd("@TicketCode", data.TicketCode));
            com.Parameters.Add(ParameterAdd("@CollectType", data.CollectType));
            com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
            com.Parameters.Add(ParameterAdd("@Amount", data.Amount));
            com.Parameters.Add(ParameterAdd("@ForeignCurrencyCode", data.ForeignCurrencyCode));
            com.Parameters.Add(ParameterAdd("@ForeignAmount", data.ForeignAmount));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            if (!string.IsNullOrEmpty(data.TaxInvoiceNo))
                com.Parameters.Add(ParameterAdd("@ABBNo", data.TaxInvoiceNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));
            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }

        


        static public bool DeleteFeeServiceCharge(FeeDetail data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE T_PassengerFeeServiceChargeCurrentUpdate  ");
            sb.Append("WHERE PassengerFeeServiceChargeTId = @PassengerFeeServiceChargeTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PassengerFeeServiceChargeTId", data.RowID));

            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }


        #endregion

        #region Payment


        static public bool InsertPayment(T_PaymentCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO T_PaymentCurrentUpdate  ");
            sb.Append("(TransactionId,PaymentID,ReferenceType ,ReferenceID,PaymentMethodType,PaymentMethodCode,CurrencyCode,PaymentAmount,CollectedCurrencyCode,CollectedAmount,QuotedCurrencyCode,QuotedAmount,Status,ParentPaymentID,AgentCode,OrganizationCode,DomainCode,LocationCode,ApprovalDate,ABBNo,Create_By,Create_Date,Update_By,Update_Date,BankName,BranchNo,AccountNo,AccountNoID) VALUES ");
            sb.Append("(@TransactionId,@PaymentID,@ReferenceType ,@ReferenceID,@PaymentMethodType,@PaymentMethodCode,@CurrencyCode,@PaymentAmount,@CollectedCurrencyCode,@CollectedAmount,@QuotedCurrencyCode,@QuotedAmount,@Status,@ParentPaymentID,@AgentCode,@OrganizationCode,@DomainCode,@LocationCode,@ApprovalDate,@ABBNo,@Create_By,GETDATE(),@Update_By,GETDATE(),@BankName,@BranchNo,@AccountNo,@AccountNoID)  ");


            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
            com.Parameters.Add(ParameterAdd("@PaymentID", data.PaymentID));
            com.Parameters.Add(ParameterAdd("@ReferenceType", data.ReferenceType));
            com.Parameters.Add(ParameterAdd("@ReferenceID", data.ReferenceID));
            com.Parameters.Add(ParameterAdd("@PaymentMethodCode", data.PaymentMethodCode));
            com.Parameters.Add(ParameterAdd("@PaymentMethodType", data.PaymentMethodType));
            com.Parameters.Add(ParameterAdd("@AgentCode", data.AgentCode));
            com.Parameters.Add(ParameterAdd("@ApprovalDate", data.ApprovalDate));
            com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
            com.Parameters.Add(ParameterAdd("@PaymentAmount", data.PaymentAmount));
            com.Parameters.Add(ParameterAdd("@CollectedCurrencyCode", data.CollectedCurrencyCode));
            com.Parameters.Add(ParameterAdd("@CollectedAmount", data.CollectedAmount));

            com.Parameters.Add(ParameterAdd("@QuotedCurrencyCode", data.QuotedCurrencyCode));
            com.Parameters.Add(ParameterAdd("@QuotedAmount", data.QuotedAmount));
            com.Parameters.Add(ParameterAdd("@Status", data.Status));
            com.Parameters.Add(ParameterAdd("@ParentPaymentID", data.ParentPaymentID));
            com.Parameters.Add(ParameterAdd("@OrganizationCode", data.OrganizationCode));

            com.Parameters.Add(ParameterAdd("@DomainCode", data.DomainCode));
            com.Parameters.Add(ParameterAdd("@LocationCode", data.LocationCode));
            if (!string.IsNullOrEmpty(data.ABBNo))
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));

            com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));

            com.Parameters.Add(ParameterAdd("@BankName", data.BankName));
            com.Parameters.Add(ParameterAdd("@BranchNo", data.BranchNo));
            com.Parameters.Add(ParameterAdd("@AccountNo", data.AccountNo));
            com.Parameters.Add(ParameterAdd("@AccountNoID", data.AccountNoID));

            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }


        static public bool UpdatePayment(T_PaymentCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE T_PaymentCurrentUpdate SET ");
            sb.Append("PaymentMethodCode = @PaymentMethodCode, ");
            sb.Append("PaymentMethodType = @PaymentMethodType, ");
            sb.Append("AgentCode = @AgentCode, ");
            sb.Append("ApprovalDate = @ApprovalDate, ");
            sb.Append("CurrencyCode = @CurrencyCode, ");
            sb.Append("PaymentAmount = @PaymentAmount, ");
            sb.Append("CollectedCurrencyCode = @CollectedCurrencyCode, ");
            sb.Append("CollectedAmount = @CollectedAmount, ");
            sb.Append("Update_By = @Update_By, ");
            sb.Append("Update_Date = GETDATE(), ");
            sb.Append("BankName = @BankName, ");
            sb.Append("BranchNo = @BranchNo, ");
            sb.Append("AccountNo = @AccountNo, ");
            sb.Append("AccountNoID = @AccountNoID, ");
            sb.Append("ABBNo = @ABBNo ");
            sb.Append("WHERE PaymentTId = @PaymentTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PaymentTId", data.PaymentTId));
            com.Parameters.Add(ParameterAdd("@PaymentMethodCode", data.PaymentMethodCode));
            com.Parameters.Add(ParameterAdd("@PaymentMethodType", data.PaymentMethodType));
            com.Parameters.Add(ParameterAdd("@AgentCode", data.AgentCode));
            com.Parameters.Add(ParameterAdd("@ApprovalDate", data.ApprovalDate));
            com.Parameters.Add(ParameterAdd("@CurrencyCode", data.CurrencyCode));
            com.Parameters.Add(ParameterAdd("@PaymentAmount", data.PaymentAmount));
            com.Parameters.Add(ParameterAdd("@CollectedCurrencyCode", data.CollectedCurrencyCode));
            com.Parameters.Add(ParameterAdd("@CollectedAmount", data.CollectedAmount));
            com.Parameters.Add(ParameterAdd("@Update_By", data.Update_By));
            if (!string.IsNullOrEmpty(data.ABBNo))
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
            else
                com.Parameters.Add(ParameterAdd("@ABBNo", DBNull.Value));

            com.Parameters.Add(ParameterAdd("@BankName", data.BankName));
            com.Parameters.Add(ParameterAdd("@BranchNo", data.BranchNo));
            com.Parameters.Add(ParameterAdd("@AccountNo", data.AccountNo));
            com.Parameters.Add(ParameterAdd("@AccountNoID", data.AccountNoID));

            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }


        static public bool DeletePayment(T_PaymentCurrentUpdate data, ref string strExec)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE T_PaymentCurrentUpdate ");
            sb.Append("WHERE PaymentTId = @PaymentTId ");

            SqlConnection conn = new SqlConnection(ConnectionString);
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand com = new SqlCommand(sb.ToString(), conn);
            com.CommandType = System.Data.CommandType.Text;
            com.Parameters.Add(ParameterAdd("@PaymentTId", data.PaymentTId));

            strExec = CommandAsSql(com);
            int row = com.ExecuteNonQuery();
            return row > 0;
        }


        #endregion


        static public List<FeeDetail> GetServiceCharge(string BookingNo)
        {
            try
            {
                using (var context = new POSINVEntities())
                {

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("DECLARE @BookingNo varchar(50) = '" + BookingNo + "' ");
                    sb.AppendLine("DECLARE @ExchangeRateTH decimal(18, 8) = (SELECT TOP 1 ExchangeRateTH FROM T_ABB WHERE PNR_No = @BookingNo)");
                    sb.AppendLine("SELECT s_charge.BookingServiceChargeTId AS RowID, s_charge.ServiceChargeNo AS ServiceChargeNo,	s_charge.ChargeType,s_charge.ChargeCode,s_charge.TicketCode, ");
                    sb.AppendLine("s_charge.ChargeDetail,s_charge.ABBNo AS TaxInvoiceNo,  s_charge.CurrencyCode, s_charge.Amount ,  s_charge.ForeignCurrencyCode, s_charge.ForeignAmount , 1 AS Qty,  ");
                    sb.AppendLine("book.Booking_Date, ");
                    sb.AppendLine("Convert(nvarchar(50), seg.SegmentTId) AS SegmentTId  , seg.SeqmentNo,");
                    sb.AppendLine("pass.PassengerId, pass.FirstName,pass.LastName,");
                    sb.AppendLine("@ExchangeRateTH as Exc, ");
                    sb.AppendLine("FF.AbbGroup, ");
                    sb.AppendLine("'T_BookingServiceChargeCurrentUpdate' AS TableName ");
                    sb.AppendLine("FROM T_BookingServiceChargeCurrentUpdate s_charge ");
                    sb.AppendLine("JOIN T_PaxFareCurrentUpdate pax ON pax.PaxFareTId = s_charge.PaxFareTId ");
                    sb.AppendLine("JOIN T_SegmentCurrentUpdate seg ON seg.SegmentTId = pax.SegmentTId ");
                    sb.AppendLine("JOIN T_BookingCurrentUpdate book ON book.TransactionId = seg.TransactionId  ");
                    sb.AppendLine("JOIN T_PassengerCurrentUpdate pass ON s_charge.PassengerId = pass.PassengerId   ");
                    sb.AppendLine("LEFT JOIN M_FlightFee FF ON FF.Flight_Fee_Code = s_charge.ChargeCode  ");
                    sb.AppendLine("where book.PNR_No = @BookingNo ");
                    sb.AppendLine("Union");
                    sb.AppendLine("SELECT pass_fee_sv.PassengerFeeServiceChargeTId AS RowID,pass_fee_sv.ServiceChargeNo AS ServiceChargeNo ,	pass_fee_sv.ChargeType,pass_fee_sv.ChargeCode,pass_fee_sv.TicketCode, ");
                    sb.AppendLine("pass_fee_sv.ChargeDetail,pass_fee_sv.ABBNo AS TaxInvoiceNo ,  pass_fee_sv.CurrencyCode, pass_fee_sv.Amount,  ");
                    sb.AppendLine("pass_fee_sv.ForeignCurrencyCode, pass_fee_sv.ForeignAmount, 1 AS Qty ,");
                    sb.AppendLine("book.Booking_Date, ");
                    sb.AppendLine("'' AS SegmentTId,'' AS SeqmentNo,");
                    sb.AppendLine("pass.PassengerId,pass.FirstName,pass.LastName,");
                    sb.AppendLine("@ExchangeRateTH as Exc, ");
                    sb.AppendLine("FF.AbbGroup, ");
                    sb.AppendLine("'T_PassengerFeeServiceChargeCurrentUpdate' AS TableName ");
                    sb.AppendLine("FROM T_PassengerFeeServiceChargeCurrentUpdate pass_fee_sv ");
                    sb.AppendLine("JOIN T_PassengerFeeCurrentUpdate pass_fee  ON pass_fee.PassengerFeeTId = pass_fee_sv.PassengerFeeTId   ");
                    sb.AppendLine("JOIN T_PassengerCurrentUpdate pass ON  pass.PassengerTId = pass_fee.PassengerTId  ");
                    sb.AppendLine("JOIN T_BookingCurrentUpdate book ON book.TransactionId = pass.TransactionId ");
                    sb.AppendLine("LEFT JOIN M_FlightFee FF ON FF.Flight_Fee_Code = pass_fee_sv.ChargeCode ");
                    sb.AppendLine("WHERE book.PNR_No =  @BookingNo ");
                    sb.AppendLine("ORDER BY FirstName,ChargeDetail ");
                    var _item = context.Database.SqlQuery<FeeDetail>(sb.ToString()).ToList();

                    int i = 0;
                    decimal ExchangeRateTH = 1;
                    foreach (FeeDetail item in _item)
                    {
                        if (item.ChargeType.ToLower() == "discount" || item.ChargeType.ToLower() == "promotiondiscount")
                        {
                            item.Discount = item.Amount;
                            item.Amount = item.Amount * -1;
                        }
                        item.RowNo = i++;

                        if (item.CurrencyCode == "THB")
                        {
                            item.AmountTHB = (item.Amount ?? 0);
                            item.Exc = 1;
                        }
                        else
                        {
                            if (item.Exc == null || string.IsNullOrEmpty(item.Exc.ToString()))
                            {
                                ExchangeRateTH = MasterDA.GetCurrencyActiveDate(item.CurrencyCode, item.Booking_Date);
                                item.AmountTHB = ExchangeRateTH * (item.Amount ?? 0);
                                item.Exc = ExchangeRateTH;
                            }
                            else
                            {
                                item.AmountTHB = (item.Exc ?? 1) * (item.Amount ?? 0);
                            }
                        }
                    }

                    return _item;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public decimal GetExchangeRateTH(string BookingNo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from T_ABB ");
                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PNR_No", BookingNo));

                using (SqlDataReader reader = com.ExecuteReader())
                {
                    using (DataTable result = new DataTable())
                    {
                        result.Load(reader);
                        if (result.Rows.Count > 0)
                        {
                            return Convert.ToDecimal(result.Rows[0]["ExchangeRateTH"]);
                        }
                    }
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        static public bool InsertLogBookingUpdate(T_LogBookingCurrentUpdate data )
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Declare @INV_No varchar(36) = (select INV_No from T_Invoice_Info inv join T_ABB abb on inv.INV_ID =  abb.INV_ID where abb.TaxInvoiceNo = @ABBNo)");
                sb.Append("INSERT INTO T_LogBookingCurrentUpdate ");
                sb.Append("(TransactionId,PNR_No,Method,FormName,Msg,Create_By ,Create_Date,INV_No,ABBNo,TableName) VALUES");
                sb.Append("(@TransactionId, @PNR_No, @Method, @FormName, @Msg, @Create_By, @Create_Date, @INV_No,@ABBNo,@TableName) ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@PNR_No", data.PNR_No));
                com.Parameters.Add(ParameterAdd("@Method", data.Method));
                com.Parameters.Add(ParameterAdd("@FormName", data.FormName));
                com.Parameters.Add(ParameterAdd("@Msg", data.Msg));
                com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                com.Parameters.Add(ParameterAdd("@Create_Date", data.Create_Date));
                //com.Parameters.Add(ParameterAdd("@INV_No", data.INV_No));
                com.Parameters.Add(ParameterAdd("@ABBNo", data.ABBNo));
                com.Parameters.Add(ParameterAdd("@TableName", data.TableName));


                int row = com.ExecuteNonQuery();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        static public DataTable GetPassengerFeeByPassengerId(string PassengerId)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * ");
                sb.Append("from T_PassengerFeeCurrentUpdate  ");
                sb.Append("WHERE PassengerId = @PassengerId ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@PassengerId", PassengerId));

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


        static public string ParameterValueForSQL(SqlParameter sp)
        {
            string retval = "";

            switch (sp.SqlDbType)
            {
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.Time:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                case SqlDbType.UniqueIdentifier:
                    if (sp.Value == DBNull.Value)
                    {
                        retval = "NULL";
                    }
                    else
                    {
                        retval = "'" + sp.Value.ToString().Replace("'", "''") + "'";
                    }
                    break;

                case SqlDbType.Bit:
                    if (sp.Value == DBNull.Value)
                    {
                        retval = "NULL";
                    }
                    else
                    {
                        retval = ((bool)sp.Value == false) ? "0" : "1";
                    }
                    break;

                default:
                    if (sp.Value == DBNull.Value)
                    {
                        retval = "NULL";
                    }
                    else
                    {
                        retval = sp.Value.ToString().Replace("'", "''");
                    }
                    break;
            }

            return retval;
        }


        static public string CommandAsSql(SqlCommand sc)
        {
            string sql = sc.CommandText;
            try
            {

                

                sql = sql.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                sql = System.Text.RegularExpressions.Regex.Replace(sql, @"\s+", " ");

                foreach (SqlParameter sp in sc.Parameters)
                {
                    string spName = sp.ParameterName;
                    string spValue = ParameterValueForSQL(sp);
                    sql = sql.Replace(spName, spValue);
                }

                sql = sql.Replace("= NULL", "IS NULL");
                sql = sql.Replace("!= NULL", "IS NOT NULL");

            }
            catch (Exception)
            {

                throw;
            }
            return sql;
        }


        static public SqlParameter ParameterAdd(string ParameterName, object data)
        {
            if (data != null)
                return new SqlParameter(ParameterName, data);
            else
                return new SqlParameter(ParameterName, DBNull.Value);
        }

        static public bool UpdateAbbInactiveByABB_No(string ABB_No, string Update_By)
        {
            try
            {

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand("sp_ABB_OR_AllUpdate", conn);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.Add(ParameterAdd("@ABB_No", ABB_No));
                com.Parameters.Add(ParameterAdd("@Update_By", Update_By));
                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}