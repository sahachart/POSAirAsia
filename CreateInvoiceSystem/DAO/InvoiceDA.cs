using CreateInvoiceSystem.DAO;
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
    public class InvoiceDA
    {
        static private string ConnectionString = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();

        static public DataTable GetInvoiceList(string WhereClause)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT TOP 100	book.PNR_No,book.Booking_Date,inv.INV_No ,T_ABB.TaxInvoiceNo, ");
                sb.AppendLine("M_Customer.first_name + ' ' +M_Customer.last_name  AS CustName , ");
                sb.AppendLine("T_ABB.Create_By,T_ABB.Create_Date ,T_ABB.Update_By , T_ABB.Update_Date ");
                sb.AppendLine("FROM T_ABB ");
                sb.AppendLine("LEFT JOIN T_BookingCurrentUpdate book ON book.ABBNo = T_ABB.TaxInvoiceNo  ");
                sb.AppendLine("LEFT JOIN T_Invoice_Info inv ON inv.Booking_No = book.PNR_No ");
                sb.AppendLine("LEFT JOIN M_Customer  ON M_Customer.CustomerID = inv.CustomerID");
                sb.AppendLine("WHERE book.PNR_No is not Null " + WhereClause + " ");
                sb.AppendLine("ORDER BY T_ABB.Create_Date DESC");

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

        static public List<Object> GetInvoiceList(string BookingNo, DateTime? FromDate, DateTime? ToDate, string PassengerName, bool? IsPrint, string Status)
        {
            var Output = new List<Object>();
            using (var context = new POSINVEntities())
            {

                var Invoices = context.T_Invoice_Info.Where(x => true);
                if (BookingNo != string.Empty)
                {
                    Invoices = Invoices.Where(x => x.Booking_No == BookingNo);
                }
                if (IsPrint.HasValue && IsPrint.Value)
                {
                    Invoices = Invoices.Where(x => x.IsPrint == IsPrint);
                }
                if (Status != string.Empty && Status.ToUpper() != "ALL")
                {
                    if (Status.ToUpper() == "FULL")
                    {
                        Invoices = Invoices.Where(x => x.Status == null);
                    }
                    else
                    {
                        Invoices = Invoices.Where(x => x.Status == Status);
                    }
                }

                var ListInvoice = Invoices.ToList();

                if (FromDate.HasValue || ToDate.HasValue)
                {
                    var Booking = context.T_BookingCurrentUpdate.Where(x => true);
                    if (FromDate.HasValue)
                    {
                        Booking = Booking.Where(x => x.Booking_Date >= FromDate.Value);
                    }
                    if (ToDate.HasValue)
                    {
                        var SearchDate = ToDate.Value.AddDays(1).AddSeconds(-1);
                        Booking = Booking.Where(x => x.Booking_Date <= SearchDate);
                    }

                    var BookingNos = Booking.Select(x => x.PNR_No).ToList();

                    ListInvoice = ListInvoice.Where(x => BookingNos.Count(c => c.Equals(x.Booking_No)) > 0).ToList();
                }

                if (PassengerName != string.Empty)
                {
                    var Customer = context.M_Customer.Where(x => x.first_name.Contains(PassengerName)).Select(s => s.CustomerID).ToList();

                    ListInvoice = ListInvoice.Where(x => Customer.Count(c => c.Equals(x.CustomerID)) > 0).ToList();
                }

                foreach (var Invoice in ListInvoice.OrderBy(x => x.Status).ThenBy(c => c.Booking_No))
                {
                    var ABB = String.Join(", ", context.T_ABB.Where(x => x.INV_ID == Invoice.INV_ID).Select(s => s.TaxInvoiceNo).ToList());
                    var BookingCurrentUpdate = context.T_BookingCurrentUpdate.FirstOrDefault(x => x.PNR_No == Invoice.Booking_No);
                    var Station = context.M_Station.FirstOrDefault(x => x.Station_Code == Invoice.BranchNo);
                    var Customer = context.M_Customer.First(x => x.CustomerID == Invoice.CustomerID);

                    DateTime? BookingDate = null;
                    if (BookingCurrentUpdate != null)
                    {
                        BookingDate = BookingCurrentUpdate.Booking_Date;
                    }

                    Object item = new
                    {
                        PNR_No = Invoice.Booking_No,
                        Booking_Date = BookingDate,
                        TaxInvoiceNo = ABB != string.Empty ? ABB : Invoice.ABB_NO,
                        INV_No = Invoice.INV_No,
                        INV_No2 = Invoice.INV_No,
                        CustName = Customer.first_name, //+ " " + Customer.last_name,
                        Branch = Station != null ? Station.Station_Name : "",
                        Create_By = Invoice.Create_By,
                        Create_Date = Invoice.Create_Date,
                        Update_By = Invoice.UpDate_By,
                        Update_Date = Invoice.Update_Date,
                        Status = Invoice.Status == null ? "Full Tax" : Invoice.Status
                    };


                    Output.Add(item);
                }

            }
            return Output;
        }

        static public DataTable GetBookingByABB_NO(string abb_no)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * ");
                sb.Append("from T_BookingCurrentUpdate book ");
                sb.Append("LEFT JOIN T_PassengerCurrentUpdate pass ON book.TransactionId = pass.TransactionId ");
                sb.Append("LEFT JOIN T_PaymentCurrentUpdate pay ON book.TransactionId = pay.TransactionId ");
                sb.Append("WHERE book.ABBNo = @ABBNo ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@ABBNo", abb_no));

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

        static public DataTable GetBookingByInv_No(string Inv_No)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * ");
                sb.Append("from T_Invoice_Info  ");
                sb.Append("WHERE Inv_No = @Inv_No ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@Inv_No", Inv_No));

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

        static public DataTable GetInvoiceByINVNo(string INV_No)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * ");
                sb.Append("from T_Invoice_Info ");
                sb.Append("LEFT JOIN M_Customer ON M_Customer.CustomerID = T_Invoice_Info.CustomerID ");
                //sb.Append("LEFT JOIN M_Province ON M_Province.ProvinceCode = M_Customer.Prvn_Code ");
                sb.Append("WHERE INV_No = @INV_No");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@INV_No", INV_No));

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

        static public DataTable GetFeeByBookingNo(string BooingNo, string ABBNos)
        {
            try
            {
                var ABBNo = ABBNos.Split(',');
                var s_chargeCondition = "(1 = 1)";
                var pass_feeCondition = "(1 = 1)";
                if (ABBNo.Count() > 1)
                {
                    s_chargeCondition = "( s_charge.ABBNo = '" + String.Join("' OR s_charge.ABBNo = '", ABBNo) + "' )";
                    pass_feeCondition = "( pass_fee.ABBNo = '" + String.Join("' OR pass_fee.ABBNo = '", ABBNo) + "' )";
                }
                else if (ABBNo.Count() == 1)
                {
                    s_chargeCondition = "s_charge.ABBNo = '" + ABBNo[0] + "' ";
                    pass_feeCondition = "pass_fee.ABBNo = '" + ABBNo[0] + "' ";
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT	s_charge.ChargeType,s_charge.ChargeCode, FF.Flight_Fee_Code ,");
                sb.AppendLine("FF.Flight_Fee_Name,s_charge.ChargeDetail,s_charge.ABBNo,  s_charge.CurrencyCode, s_charge.Amount ,  s_charge.ForeignCurrencyCode, s_charge.ForeignAmount , ");
                sb.AppendLine("book.Booking_Date AS Date_Curr,'Booking' AS Come ");
                sb.AppendLine("FROM T_BookingServiceChargeCurrentUpdate s_charge ");
                sb.AppendLine("LEFT JOIN T_PassengerCurrentUpdate pass ON s_charge.PassengerId = pass.PassengerId ");
                sb.AppendLine("LEFT JOIN T_SegmentCurrentUpdate seg ON seg.TransactionId = pass.TransactionId and seg.SeqmentNo = s_charge.SeqmentNo ");
                sb.AppendLine("LEFT JOIN T_BookingCurrentUpdate book ON book.TransactionId = seg.TransactionId ");
                sb.AppendLine("LEFT JOIN M_FlightFee FF ON FF.Flight_Fee_Code = s_charge.ChargeCode ");
                sb.AppendLine("where book.PNR_No = @BooingNo AND " + s_chargeCondition);
                sb.AppendLine("Union");
                sb.AppendLine("SELECT	pass_fee_sv.ChargeType,pass_fee_sv.ChargeCode, FF.Flight_Fee_Code, ");
                sb.AppendLine("FF.Flight_Fee_Name,pass_fee_sv.ChargeDetail,pass_fee_sv.ABBNo,  pass_fee_sv.CurrencyCode, pass_fee_sv.Amount, ");
                sb.AppendLine("pass_fee_sv.ForeignCurrencyCode, pass_fee_sv.ForeignAmount ,pass_fee.FeeCreateDate AS Date_Curr, 'Fee' AS Come ");
                sb.AppendLine("FROM T_PassengerFeeServiceChargeCurrentUpdate pass_fee_sv ");
                sb.AppendLine("LEFT JOIN T_PassengerCurrentUpdate pass ON  pass.PassengerId = pass_fee_sv.PassengerId ");
                sb.AppendLine("LEFT JOIN M_FlightFee FF ON  FF.Flight_Fee_Code = pass_fee_sv.ChargeCode ");
                sb.AppendLine("LEFT JOIN T_ABB  on pass_fee_sv.ABBNo = T_ABB.TaxInvoiceNo ");
                sb.AppendLine("LEFT JOIN T_BookingCurrentUpdate book ON book.TransactionId = pass.TransactionId ");
                sb.AppendLine("LEFT JOIN T_PassengerFeeCurrentUpdate pass_fee  ON pass_fee.PassengerFeeTId = pass_fee_sv.PassengerFeeTId ");
                sb.AppendLine("WHERE book.PNR_No = @BooingNo AND " + pass_feeCondition);


                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(new SqlParameter("@BooingNo", BooingNo));
                //com.Parameters.Add(new SqlParameter("@AbbNo", AbbNo));

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

        static public List<T_SegmentCurrentUpdate> GetInvoiceFlight(Guid INV_ID)
        {
            var context = new POSINVEntities();

            return context.T_Invoice_Flight.Where(x => x.INV_ID == INV_ID).OrderBy(c => c.DepartureDate).ToList().Select(s => new T_SegmentCurrentUpdate()
            {
                STD = s.DepartureDate,
                STA = s.ArraivalDate,
                DepartureStation = s.DepartureStation,
                ArrivalStation = s.ArrivalStation,
                CarrierCode = s.CarrierCode,
                FlightNumber = s.FlightNumber
            }).ToList();

        }

        static public List<T_PassengerCurrentUpdate> GetInvoicePassenger(Guid INV_ID)
        {
            var context = new POSINVEntities();

            if (context.T_Invoice_Info.Count(x => x.INV_ID == INV_ID) > 0)
            {
                return context.T_Invoice_Info.First(x => x.INV_ID == INV_ID).PassengerName.Split(',').Select(s => new T_PassengerCurrentUpdate() { FirstName = s }).ToList();
            }
            return null;
        }

        static public List<FeeDetail> GetInvoiceFare(Guid INV_ID)
        {
            var context = new POSINVEntities();

            return context.T_Invoice_Fare.Where(x => x.INV_ID == INV_ID).Select(s => new FeeDetail()
            {
                PaxType = s.Pax_Type,
                ChargeCode = s.ChargeCode,
                qty = s.Quantity,
                CurrencyCode = s.Currency,
                Exc = s.ExchangeRateTH,
                Amount = s.Amount,
                Discount = s.DiscountTH,
                AmountTHB = s.AmountTH,
                TotalAmountTHB = s.TotalTH,
                AbbGroup = s.Group
            }).ToList();
        }

        static public List<FeeDetail> GetInvoiceFee(Guid INV_ID)
        {
            var context = new POSINVEntities();
            return context.T_Invoice_Fee.Where(x => x.INV_ID == INV_ID).Select(s => new FeeDetail()
            {
                FirstName = s.PassengerName,
                LastName = "",
                ChargeCode = s.ChargeCode,
                qty = s.Quantity,
                CurrencyCode = s.Currency,
                Exc = s.ExchangeRateTH,
                Amount = s.Amount,
                Discount = s.DiscountTH,
                AmountTHB = s.AmountTH,
                AbbGroup = s.Group
            }).ToList();
        }

        static public List<T_PaymentCurrentUpdate> GetInvoicePayment(Guid INV_ID)
        {
            var context = new POSINVEntities();
            return context.T_Invoice_Payment.Where(x => x.INV_ID == INV_ID).OrderBy(c => c.SEQ).ToList().Select(s => new T_PaymentCurrentUpdate()
            {
                AgentCode = s.AgentCode,
                ApprovalDate = s.ApprovalDate,
                PaymentMethodType = s.PaymentType_Name,
                CurrencyCode = s.CurrencyCode,
                PaymentAmount = s.PaymentAmount_Ori,
                PaymentAmountTHB = s.PaymentAmount
            }).ToList();
        }

        public enum RunningType
        {
            ABB = 0,
            IV = 1,
            AK = 2,
            QZ = 3,
            CN = 4
        }

        static public string GenerateRunning(RunningType _RunningType,string POS_Code)
        {
            var context = new POSINVEntities();

            if (context.M_POS_Machine.Count(x => x.POS_Code == POS_Code) > 0)
            {
                var POS = context.M_POS_Machine.First(x => x.POS_Code == POS_Code);
                var entry = context.Entry(POS);
                entry.State = EntityState.Modified;
                

                int Running = 1;
                var LastRunning = "";
                if (_RunningType == RunningType.IV)
                {
                    LastRunning = POS.Last_TAX_No;
                    entry.Property(e => e.Last_TAX_No).IsModified = true;
                    POS.Last_TAX_No = LastRunning;
                }
                else if (_RunningType == RunningType.AK)
                {
                    LastRunning = POS.Last_AK_No;
                }
                else if (_RunningType == RunningType.QZ)
                {
                    LastRunning = POS.Last_QZ_No;
                }
                else if (_RunningType == RunningType.CN)
                {
                    LastRunning = POS.Last_Refund_Inv_No;
                }

                if (int.TryParse(LastRunning, out Running))
                {
                    Running = Running + 1; 
                }
                else
                {
                    Running = 1;
                }

                LastRunning = Running.ToString();

                var No = String.Format("{0}{1}{2}{3}-{4}", DateTime.Today.Year.ToString("00").Substring(2, 2), DateTime.Today.Month.ToString("00"), POS.Station_Code, POS_Code, Running.ToString("00000"));

                if (_RunningType == RunningType.IV)
                {
                    No = "IV" + No;
                    entry.Property(e => e.Last_TAX_No).IsModified = true;
                    POS.Last_TAX_No = LastRunning;
                }
                else if (_RunningType == RunningType.AK)
                {
                    No = "AA" + No;
                    entry.Property(e => e.Last_AK_No).IsModified = true;
                    POS.Last_AK_No = LastRunning;
                }
                else if (_RunningType == RunningType.QZ)
                {
                    No = "TAA" + No;
                    entry.Property(e => e.Last_QZ_No).IsModified = true;
                    POS.Last_QZ_No = LastRunning;
                }
                else if (_RunningType == RunningType.CN)
                {
                    No = "CN" + No;
                    entry.Property(e => e.Last_Refund_Inv_No).IsModified = true;
                    POS.Last_Refund_Inv_No = LastRunning;
                }

                context.SaveChanges();

                return No.Length > 3 ? No : string.Empty;
            }

            return string.Empty;
        }

        static public bool InsertInvoice(T_Invoice_Info _Invoice, string ABBNos)
        {
            try
            {
                var INVNo = GenerateRunning(RunningType.IV, _Invoice.POS_Code);
                Guid INV_Id = Guid.NewGuid();
                var ABBNo = ABBNos.Replace(" ", "").Split(',');

                var context = new POSINVEntities();
                _Invoice.INV_ID = INV_Id;
                _Invoice.INV_No = INVNo;
                _Invoice.ABB_NO = String.Join(", ", ABBNo);
                _Invoice.Create_Date = DateTime.Now;
                _Invoice.Update_Date = DateTime.Now;
                context.T_Invoice_Info.Add(_Invoice);

                decimal ADMINFEE = 0;
                decimal Fuel = 0;
                decimal Service = 0;
                decimal OTHER = 0;
                decimal VAT = 0;
                decimal AIRPORTTAX = 0;
                decimal INSURANCE = 0;
                decimal REP = 0;
                decimal Discount = 0;
                decimal TOTAL = 0;
                decimal ExchangeRateTH = 0;
                decimal ADMINFEE_Ori = 0;
                decimal Fuel_Ori = 0;
                decimal Service_Ori = 0;
                decimal OTHER_Ori = 0;
                decimal VAT_Ori = 0;
                decimal AIRPORTTAX_Ori = 0;
                decimal INSURANCE_Ori = 0;
                decimal REP_Ori = 0;
                decimal Discount_Ori = 0;
                decimal TOTAL_Ori = 0;

                var SeqPayment = 0;

                var T_Invoice_Flights = new List<T_Invoice_Flight>();

                foreach (var item in ABBNo)
                {
                    #region T_ABB

                    if (context.T_ABB.Count(x => x.TaxInvoiceNo == item) > 0)
                    {
                        var _ABB = context.T_ABB.First(x => x.TaxInvoiceNo == item);
                        context.T_ABB.Attach(_ABB);
                        _ABB.INV_ID = INV_Id;

                        _Invoice.PAXCountADT = _ABB.PAXCountADT;
                        _Invoice.PAXCountCHD = _ABB.PAXCountCHD;
                        _Invoice.PassengerName = _ABB.Passenger_Name;
                        ADMINFEE += _ABB.ADMINFEE;
                        Fuel += _ABB.Fuel;
                        Service += _ABB.Service;
                        OTHER += _ABB.OTHER;
                        VAT += _ABB.VAT;
                        AIRPORTTAX += _ABB.AIRPORTTAX;
                        INSURANCE += _ABB.INSURANCE;
                        REP += _ABB.REP;
                        Discount += _ABB.Discount.GetValueOrDefault();
                        TOTAL += _ABB.TOTAL;
                        ExchangeRateTH += _ABB.ExchangeRateTH;
                        ADMINFEE_Ori += _ABB.ADMINFEE_Ori;
                        Fuel_Ori += _ABB.Fuel_Ori;
                        Service_Ori += _ABB.Service_Ori;
                        OTHER_Ori += _ABB.OTHER_Ori;
                        VAT_Ori += _ABB.VAT_Ori;
                        AIRPORTTAX_Ori += _ABB.AIRPORTTAX_Ori;
                        INSURANCE_Ori += _ABB.INSURANCE_Ori;
                        REP_Ori += _ABB.REP_Ori;
                        Discount_Ori += _ABB.Discount_Ori;
                        TOTAL_Ori += _ABB.TOTAL_Ori;

                        #region Payment

                        if (context.T_ABBPAYMENTMETHOD.Count(x => x.ABBTid == _ABB.ABBTid) > 0)
                        {
                            foreach (var _ABBPAYMENTMETHOD in context.T_ABBPAYMENTMETHOD.Where(x => x.ABBTid == _ABB.ABBTid).OrderBy(c => c.SEQ))
                            {
                                context.T_Invoice_Payment.Add(new T_Invoice_Payment()
                                {
                                    ID = Guid.NewGuid(),
                                    INV_ID = INV_Id,
                                    SEQ = SeqPayment++,
                                    PaymentMethodCode = _ABBPAYMENTMETHOD.PaymentMethodCode,
                                    PaymentType_Name = _ABBPAYMENTMETHOD.PaymentType_Name,
                                    PaymentAmount = _ABBPAYMENTMETHOD.PaymentAmount,
                                    PaymentAmount_Ori = _ABBPAYMENTMETHOD.PaymentAmount_Ori,
                                    CurrencyCode = _ABBPAYMENTMETHOD.CurrencyCode,
                                    ExchangeRateTH = _ABBPAYMENTMETHOD.ExchangeRateTH,
                                    AgentCode = _ABBPAYMENTMETHOD.AgentCode,
                                    ApprovalDate = _ABBPAYMENTMETHOD.ApprovalDate
                                });
                            }
                        }

                        #endregion

                        #region Flight



                        if (context.T_ABBFARE.Count(x => x.ABBTid == _ABB.ABBTid) > 0)
                        {
                            T_Invoice_Flights = new List<T_Invoice_Flight>();
                            foreach (var _ABBFARE in context.T_ABBFARE.Where(x => x.ABBTid == _ABB.ABBTid).OrderBy(c => c.SEQ))
                            {
                                T_Invoice_Flights.Add(new T_Invoice_Flight()
                                {
                                    ID = Guid.NewGuid(),
                                    INV_ID = INV_Id,
                                    SEQ = _ABBFARE.SEQ,
                                    DepartureDate = _ABBFARE.STD,
                                    ArraivalDate = _ABBFARE.STA,
                                    DepartureStation = _ABBFARE.DepartureStation,
                                    ArrivalStation = _ABBFARE.ArrivalStation,
                                    CarrierCode = _ABBFARE.CarrierCode,
                                    FlightNumber = _ABBFARE.FlightNumber
                                });
                            }
                        }



                        #endregion
                    }

                    #endregion


                }

                if (T_Invoice_Flights.Count() > 0)
                {
                    T_Invoice_Flights.ForEach(x => context.T_Invoice_Flight.Add(x));
                }


                _Invoice.ADMINFEE = ADMINFEE;
                _Invoice.Fuel = Fuel;
                _Invoice.Service = Service;
                _Invoice.OTHER = OTHER;
                _Invoice.VAT = VAT;
                _Invoice.AIRPORTTAX = AIRPORTTAX;
                _Invoice.INSURANCE = INSURANCE;
                _Invoice.REP = REP;
                _Invoice.Discount = Discount;
                _Invoice.TOTAL = TOTAL;
                _Invoice.ExchangeRateTH = ExchangeRateTH;
                _Invoice.ADMINFEE_Ori = ADMINFEE_Ori;
                _Invoice.Fuel_Ori = Fuel_Ori;
                _Invoice.Service_Ori = Service_Ori;
                _Invoice.OTHER_Ori = OTHER_Ori;
                _Invoice.VAT_Ori = VAT_Ori;
                _Invoice.AIRPORTTAX_Ori = AIRPORTTAX_Ori;
                _Invoice.INSURANCE_Ori = INSURANCE_Ori;
                _Invoice.REP_Ori = REP_Ori;
                _Invoice.Discount_Ori = Discount_Ori;
                _Invoice.TOTAL_Ori = TOTAL_Ori;

                var FareDetails = MasterDA.GetFareDetail(_Invoice.Booking_No).Where(x => x.TaxInvoiceNo != null && ABBNos.Contains(x.TaxInvoiceNo)).ToList();
                foreach (var FareDetail in FareDetails)
                {
                    Guid? Flight_ID = null;
                    if (T_Invoice_Flights.Count(x => (x.DepartureStation + x.ArrivalStation + x.CarrierCode + x.FlightNumber) == FareDetail.SeqmentNo) > 0)
                    {
                        Flight_ID = T_Invoice_Flights.First(x => (x.DepartureStation + x.ArrivalStation + x.CarrierCode + x.FlightNumber) == FareDetail.SeqmentNo).ID;
                    }
                    context.T_Invoice_Fare.Add(new T_Invoice_Fare()
                    {
                        ID = Guid.NewGuid(),
                        INV_ID = INV_Id,
                        Pax_Type = FareDetail.PaxType,
                        ChargeCode = FareDetail.ChargeCode,
                        Quantity = FareDetail.qty,
                        Currency = FareDetail.CurrencyCode,
                        Amount = FareDetail.Amount.GetValueOrDefault(),
                        ExchangeRateTH = FareDetail.Exc.GetValueOrDefault(1),
                        DiscountTH = FareDetail.Discount.GetValueOrDefault(),
                        AmountTH = FareDetail.AmountTHB,
                        TotalTH = FareDetail.TotalAmountTHB,
                        Group = FareDetail.AbbGroup ?? string.Empty,
                        Flight_ID = Flight_ID
                    });
                }


                var FeeDetails = MasterDA.GetFeeDetail(_Invoice.Booking_No).Where(x => x.TaxInvoiceNo != null && ABBNos.Contains(x.TaxInvoiceNo)).ToList();
                foreach (var FeeDetail in FeeDetails)
                {
                    context.T_Invoice_Fee.Add(new T_Invoice_Fee()
                    {
                        ID = Guid.NewGuid(),
                        INV_ID = INV_Id,
                        PassengerName = FeeDetail.FirstName + " " + FeeDetail.LastName,
                        ChargeCode = FeeDetail.ChargeCode,
                        Quantity = FeeDetail.qty,
                        Currency = FeeDetail.CurrencyCode,
                        Amount = FeeDetail.Amount.GetValueOrDefault(),
                        ExchangeRateTH = FeeDetail.Exc.GetValueOrDefault(1),
                        DiscountTH = FeeDetail.Discount.GetValueOrDefault(),
                        AmountTH = FeeDetail.AmountTHB,
                        Group = FeeDetail.AbbGroup
                    });
                }

                context.SaveChanges();
                //StringBuilder sb = new StringBuilder();
                //sb.Append("INSERT INTO T_Invoice_Info ");
                //sb.Append("(TransactionId ,INV_No,Ref_INV_No,INVDate,CustomerID,Receipt_By,Receipt_Date,Booking_No,Booking_Date,PayType ");
                //sb.Append(" ,BankName,BranchNo,AcountNo,Cheque_No,Cheque_Date,INVType,Remark,VatType,Create_By,Create_Date,Update_Date,UpDate_By ");
                //sb.Append(" ,Address,POS_Code,SlipMessage_Code,IsPrint,CN_ID,IsCancel,Cancel_Date,OR_Reason,Status,INV_Id,ABB_NO) VALUES");

                //sb.Append("(@TransactionId,@INV_No,@Ref_INV_No,@INVDate,@CustomerID,@Receipt_By,@Receipt_Date,@Booking_No,@Booking_Date,@PayType ");
                //sb.Append(" ,@BankName,@BranchNo,@AcountNo,@Cheque_No,@Cheque_Date,@INVType,@Remark,@VatType,@Create_By,GETDATE(),GETDATE(),@UpDate_By ");
                //sb.Append(" ,@Address,@POS_Code,@SlipMessage_Code,@IsPrint,@CN_ID,@IsCancel,@Cancel_Date,@OR_Reason,@Status,@INV_Id,@ABB_NO)");

                //SqlConnection conn = new SqlConnection(ConnectionString);
                //if (conn.State == System.Data.ConnectionState.Open)
                //    conn.Close();
                //conn.Open();
                //SqlCommand com = new SqlCommand(sb.ToString(), conn);
                //com.CommandType = System.Data.CommandType.Text;
                //com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                //com.Parameters.Add(ParameterAdd("@INV_No", data.INV_No));
                //com.Parameters.Add(ParameterAdd("@Ref_INV_No", data.Ref_INV_No));
                //com.Parameters.Add(ParameterAdd("@INVDate", data.INVDate));
                //com.Parameters.Add(ParameterAdd("@CustomerID", data.CustomerID));
                //com.Parameters.Add(ParameterAdd("@Receipt_By", data.Receipt_By));
                //com.Parameters.Add(ParameterAdd("@Receipt_Date", data.Receipt_Date));
                //com.Parameters.Add(ParameterAdd("@Booking_No", data.Booking_No));
                //com.Parameters.Add(ParameterAdd("@Booking_Date", data.Booking_Date));
                //com.Parameters.Add(ParameterAdd("@PayType", data.PayType));
                //com.Parameters.Add(ParameterAdd("@BankName", data.BankName));
                //com.Parameters.Add(ParameterAdd("@BranchNo", data.BranchNo));
                //com.Parameters.Add(ParameterAdd("@AcountNo", data.AcountNo));
                //com.Parameters.Add(ParameterAdd("@Cheque_No", data.Cheque_No));
                //com.Parameters.Add(ParameterAdd("@Cheque_Date", data.Cheque_Date));
                //com.Parameters.Add(ParameterAdd("@INVType", data.INVType));
                //com.Parameters.Add(ParameterAdd("@Remark", data.Remark));
                //com.Parameters.Add(ParameterAdd("@VatType", data.VatType));
                //com.Parameters.Add(ParameterAdd("@Create_By", data.Create_By));
                //com.Parameters.Add(ParameterAdd("@UpDate_By", data.UpDate_By));


                //com.Parameters.Add(ParameterAdd("@Address", data.Address));
                //com.Parameters.Add(ParameterAdd("@POS_Code", data.POS_Code));
                //com.Parameters.Add(ParameterAdd("@SlipMessage_Code", data.SlipMessage_Code));
                //com.Parameters.Add(ParameterAdd("@IsPrint", 0));
                //com.Parameters.Add(ParameterAdd("@CN_ID", data.CN_ID));
                //com.Parameters.Add(ParameterAdd("@IsCancel", false));
                //com.Parameters.Add(ParameterAdd("@Cancel_Date", data.Cancel_Date));
                //com.Parameters.Add(ParameterAdd("@OR_Reason", data.OR_Reason));
                //com.Parameters.Add(ParameterAdd("@Status", data.Status));

                //com.Parameters.Add(ParameterAdd("@INV_Id", INV_Id));
                //com.Parameters.Add(ParameterAdd("@ABB_NO", ABBNos.Replace(",",", ")));

                //int row = com.ExecuteNonQuery();

                //var ABBNo = "( '" + ABBNos.Replace(",","','") + "' )";

                //sb = new StringBuilder();
                //sb.Append("Update T_ABB SET ");
                //sb.Append("INV_ID = '" + INV_Id.ToString() + "' ");
                //sb.Append("WHERE TaxInvoiceNo in " + ABBNo);

                //com = new SqlCommand(sb.ToString(), conn);
                //com.ExecuteNonQuery();

                //conn.Close();
                return true;
            }
            catch (Exception)
            {

                throw;
            }


            return false;
        }

        static public bool UpdateInvoice(T_Invoice_Info data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_Info SET ");
                sb.Append("INV_No = @INV_No,");
                sb.Append("Ref_INV_No = @Ref_INV_No,");
                //sb.Append("INVDate = @INVDate,");
                sb.Append("CustomerID = @CustomerID,");
                sb.Append("Receipt_By = @Receipt_By,");
                sb.Append("Receipt_Date = @Receipt_Date,");
                sb.Append("Booking_No = @Booking_No,");
                sb.Append("Booking_Date = @Booking_Date,");
                sb.Append("PayType = @PayType,");
                sb.Append("BankName = @BankName,");
                sb.Append("BranchNo = @BranchNo,");
                sb.Append("AcountNo = @AcountNo,");
                sb.Append("Cheque_No = @Cheque_No,");
                sb.Append("Cheque_Date = @Cheque_Date,");
                sb.Append("INVType = @INVType,");
                sb.Append("Remark = @Remark,");
                sb.Append("VatType = @VatType,");
                sb.Append("Update_Date = GETDATE(),");
                sb.Append("UpDate_By = @UpDate_By,");
                sb.Append("Address = @Address,");
                sb.Append("POS_Code = @POS_Code,");
                sb.Append("SlipMessage_Code = @SlipMessage_Code,");
                sb.Append("OR_Reason = @OR_Reason,");
                sb.Append("Status = @Status ");
                sb.Append("WHERE TransactionId = @TransactionId ");




                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@INV_No", data.INV_No));
                com.Parameters.Add(ParameterAdd("@Ref_INV_No", data.Ref_INV_No));
                //com.Parameters.Add(ParameterAdd("@INVDate", data.INVDate));
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
                com.Parameters.Add(ParameterAdd("@UpDate_By", data.UpDate_By));
                com.Parameters.Add(ParameterAdd("@Address", data.Address));
                com.Parameters.Add(ParameterAdd("@POS_Code", data.POS_Code));
                com.Parameters.Add(ParameterAdd("@SlipMessage_Code", data.SlipMessage_Code));
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


        }

        static public bool UpdateInvoice_ByCN(T_Invoice_CN data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_Info SET ");
                sb.Append("CN_ID = @CN_ID,");
                sb.Append("Status = 'CN' ");
                sb.Append("WHERE TransactionId = @TransactionId AND INV_No = @INV_No ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@INV_No", data.INV_No));
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


        static public bool UpdateInvoice_M_ByCN(T_Invoice_CN data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_Manual SET ");
                sb.Append("CN_ID = @CN_ID,");
                sb.Append("Status = 'CN' ");
                sb.Append("WHERE TransactionId = @TransactionId AND Receipt_no = @Receipt_no ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@TransactionId", data.TransactionId));
                com.Parameters.Add(ParameterAdd("@Receipt_no", data.INV_No));
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

        static public bool InsertInvoiceDetail(T_Invoice_Detail data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO T_Invoice_Detail ");
                sb.Append("(Receipt_no ,Flight_Fee_Code ,Flight_Fee_Name ,Other ,Qty ,UnitQTY,AmountTH) VALUES");
                sb.Append("(@Receipt_no ,@Flight_Fee_Code ,@Flight_Fee_Name ,@Other ,@Qty ,@UnitQTY,@AmountTH)");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Receipt_no", data.Receipt_no));
                com.Parameters.Add(ParameterAdd("@Flight_Fee_Code", data.Flight_Fee_Code));
                com.Parameters.Add(ParameterAdd("@Flight_Fee_Name", data.Flight_Fee_Name));
                com.Parameters.Add(ParameterAdd("@Other", data.Other));
                com.Parameters.Add(ParameterAdd("@Qty", data.Qty));
                com.Parameters.Add(ParameterAdd("@UnitQTY", data.UnitQTY));
                com.Parameters.Add(ParameterAdd("@AmountTH", data.AmountTH));

                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        static public bool UpdateInvoiceDetail(T_Invoice_Detail data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_Detail SET ");
                sb.Append("Receipt_no = @Receipt_no, ");
                sb.Append("Flight_Fee_Code = @Flight_Fee_Code, ");
                sb.Append("Flight_Fee_Name = @Flight_Fee_Name, ");
                sb.Append("Other = @Other, ");
                sb.Append("Qty = @Qty, ");
                sb.Append("UnitQTY = @UnitQTY, ");
                sb.Append("AmountTH = @AmountTH ");
                sb.Append("WHERE ID  = @ID ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@ID", data.ID));
                com.Parameters.Add(ParameterAdd("@Receipt_no", data.Receipt_no));
                com.Parameters.Add(ParameterAdd("@Flight_Fee_Code", data.Flight_Fee_Code));
                com.Parameters.Add(ParameterAdd("@Flight_Fee_Name", data.Flight_Fee_Name));
                com.Parameters.Add(ParameterAdd("@Other", data.Other));
                com.Parameters.Add(ParameterAdd("@Qty", data.Qty));
                com.Parameters.Add(ParameterAdd("@UnitQTY", data.UnitQTY));
                com.Parameters.Add(ParameterAdd("@AmountTH", data.AmountTH));

                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }

        }

        static public bool DeleteInvoiceDetailByReceipt_no(string Receipt_no)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DELETE T_Invoice_Detail  ");
                sb.Append("WHERE Receipt_no  = @Receipt_no ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Receipt_no", Receipt_no));
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

        static public string GetRunningInvoiceNo(string Prefix)
        {
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append("select * ");
                sb.Append("from T_Invoice_Seq  ");
                sb.Append("WHERE Prefix = '" + Prefix + "' ");

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
                        long num = Convert.ToInt64(result.Rows[0]["Seq_Number"]);
                        string No = Prefix + DateTime.Today.Month.ToString("00") + DateTime.Today.Year.ToString("00").Substring(2, 2) + "-" + (num + 1).ToString("00000");
                        return No;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public bool UpdateRunningInvoiceNo(string Prefix, string data, string POS_Code, string Update_by)
        {
            try
            {
                string[] sp = data.Split('-');

                long Seq_Number = 0;
                if (sp.Length == 2)
                    Seq_Number = Convert.ToInt64(sp[1]);
                string Curr_Month = data.Substring(2, 2);
                string Curr_Year = data.Substring(4, 2);

                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_Seq SET ");
                sb.Append("Last_Sequence = @Last_Sequence,");
                sb.Append("Seq_Number = @Seq_Number,");
                sb.Append("Curr_Month = @Curr_Month,");
                sb.Append("Curr_Year = @Curr_Year,");
                sb.Append("Last_POS_Code = @Last_POS_Code,");
                sb.Append("Last_Modify_date = GETDATE(),");
                sb.Append("Last_Update_by = @Last_Update_by ");
                sb.Append("WHERE Prefix = '" + Prefix + "' ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@Last_Sequence", data));
                com.Parameters.Add(ParameterAdd("@Seq_Number", Seq_Number));
                com.Parameters.Add(ParameterAdd("@Curr_Month", Curr_Month));
                com.Parameters.Add(ParameterAdd("@Curr_Year", Curr_Year));
                com.Parameters.Add(ParameterAdd("@Last_POS_Code", POS_Code));
                com.Parameters.Add(ParameterAdd("@Last_Update_by", Update_by));

                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }


        static public bool UpdteIsPrint(string INV_No)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_Info SET ");
                sb.Append("IsPrint = 1");
                sb.Append("WHERE INV_No = @INV_No ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@INV_No", INV_No));

                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }


        static public bool UpdteInvoicey_OR(string INV_No)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE T_Invoice_Info SET ");
                sb.Append("IsCancel = 1,");
                sb.Append("Cancel_Date = GETDATE(),");
                sb.Append("Status = 'OR'");
                sb.Append("WHERE INV_No = @INV_No ");

                SqlConnection conn = new SqlConnection(ConnectionString);
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand com = new SqlCommand(sb.ToString(), conn);
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.Add(ParameterAdd("@INV_No", INV_No));

                int row = com.ExecuteNonQuery();

                conn.Close();
                return row > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        static public void UpdateABB_INV_Id(string INV_No)
        {
            using (var context = new POSINVEntities())
            {
                var _Invoice_Info = context.T_Invoice_Info.Where(m => m.INV_No == INV_No).First();

                var T_ABBs = context.T_ABB.Where(m => m.INV_ID == _Invoice_Info.INV_ID);

                foreach (var ABB in T_ABBs)
                {
                    context.T_ABB.Attach(ABB);
                    ABB.INV_ID = null;
                }

                context.SaveChanges();
            }

        }
    }
}