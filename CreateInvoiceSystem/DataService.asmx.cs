using CreateInvoiceSystem.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace CreateInvoiceSystem
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataService : WebService
    {
        [WebMethod]
        public string ADAuthentication(string UserName, string Password)
        {
            string IsAuthen = "";
            try
            {
                CreateInvoiceSystem.Service.ADAuthentication aAuthent = new CreateInvoiceSystem.Service.ADAuthentication();
                IsAuthen = aAuthent.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return IsAuthen;
        }

        [WebMethod]
        public string ADAuthenticationPrifile(string UserName, string Password)
        {
            try
            {
                CreateInvoiceSystem.Service.ADAuthentication aAuthent = new CreateInvoiceSystem.Service.ADAuthentication();
                return  aAuthent.AuthenticatePrifile(UserName, Password);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [WebMethod]
        public string BatchCall_UpdateBookingByPNR_No(string PNR_No)
        {
            string UserCode = "000";
            DAO.GetDataSkySpeed getdata = new GetDataSkySpeed();
            com.airasia.acebooking.Booking bookingdata = getdata.GetData(PNR_No);
            if (bookingdata != null)
            {
                PosDS dsDB = new PosDS();
                PosDS ds;
                dsDB = DBHelper.GetBookingByBookingNo(dsDB, PNR_No);
                //if (dsDB.T_Booking.Count == 0)
                //{
                //    ds = CreateABB.LoadXMLToDS(bookingdata, UserCode, 1);
                //}
                //else
                //{
                ds = CreateABB.LoadXMLToDS(bookingdata, UserCode, 0);
                //}

                BatchCall_UpdateBooking(ds, UserCode);
            }
            return "";
        }


        [WebMethod]
        public bool BatchCall_UpdateBooking(PosDS ds, string UserCode)
        {
            PosDS dsDB = new PosDS();
            string Msg = string.Empty;
            string PNR_No = ds.T_Booking[0].PNR_No;
            dsDB = DBHelper.GetBookingByBookingNo(dsDB, PNR_No);

            if (dsDB.T_Booking.Count == 0)
            {
                ds = SetDSRowStateByABBNull(ds, 1);
                ds = CreateAbb(ds, UserCode);
                bool success = PosSave(ds);
                if (ds.ResultMessage.Count > 0)
                    Msg = ds.ResultMessage[0].Message;

            }
            else
            {
                if (dsDB.T_Booking[0].TotalCost != ds.T_Booking[0].TotalCost)
                {
                    PosDS dsSave = ProcessDataDevide(ds);
                    dsSave = UpdateItem(ds);
                    dsSave = ChangeFlight(ds);
                    dsSave = CreateAbb(dsSave, UserCode);
                    if (dsSave.ResultMessage.Count > 0)
                    {
                        Msg = dsSave.ResultMessage[0].Message;
                    }
                    if (Msg == string.Empty)
                    {
                        bool success = PosSave(dsSave);
                    }
                }
                else
                {
                    InsertWaitingForManualGenABB(dsDB.T_Booking[0].PNR_No, "ยอดรวม Total Cost เท่าเดิม", dsDB.T_Booking[0].TransactionId, UserCode);
                }
            }


            return false;
        }



        [WebMethod]
        public PosDS CreateAbb(PosDS ds, string UserCode)
        {
            try
            {
                DateTime CurrentDate = DateTime.Now;
                DataTable dtPosMachine = DBHelper.GetPosMachine();
                DataTable dtAgencyType = DBHelper.GetGetAgencyType();
                DataTable dtAgency = DBHelper.GetAgency();
                DataTable dtPaymentType = DBHelper.GetPaymentType();
                DataTable dtFlightFee = DBHelper.GetMasterDataByTableName("M_FlightFee");


                int PaxADT_Count = ds.T_Passenger.Select("PaxType = 'ADT' AND Action <> -1  ").Length;
                int PaxCHD_Count = ds.T_Passenger.Select("PaxType = 'CHD' AND Action <> -1  ").Length;
                int Pos_Id = 1;
                DataRow[] dr = (DataRow[])dtPosMachine.Select("POS_Code ='" + UserCode + "'");
                if (dr.Length > 0)
                    Pos_Id = Convert.ToInt32(dr[0]["POS_Id"]);

                var pay_date = ds.T_Payment.Where(w => w.Status == "Approved" && w.IsABBNoNull()).OrderBy(o => o.ApprovalDate).AsEnumerable().Select(row => row.Field<DateTime>("ApprovalDate").Date).Distinct();
                if (pay_date.Count() == 0)
                {
                    PosDS.ResultMessageRow msg = ds.ResultMessage.NewResultMessageRow();
                    msg.Message = "ไม่พบยอด Pament";
                    ds.ResultMessage.AddResultMessageRow(msg);
                    return ds;
                }
                foreach (var drP in pay_date)
                {
                    DateTime date = Convert.ToDateTime(drP);
                    DateTime DateWhere = drP.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    List<PosDS.T_PaymentRow> drPay = ds.T_Payment.Where(it => it.Status == "Approved" && it.ApprovalDate.Date == drP.Date && it.IsABBNoNull()).ToList(); ;
                    decimal Pay_Amount = drPay.Sum(s => s.PaymentAmount);
                    PosDS.T_BookingRow drBooking = ds.T_Booking[0];

                    string PaymentCurrency = drPay[0].CurrencyCode;
                    DateTime PaymentDate = drPay[0].ApprovalDate;
                    // คำนวน ExChangeRate
                    decimal ExChangeRate = 1;
                    if (PaymentCurrency != "THB")
                        ExChangeRate = DBHelper.GetCurrencyActiveDate(PaymentCurrency, PaymentDate);
                    string ABB_No = RunningAbb(drPay[0]);

                    if (Pay_Amount < 0)
                    {
                        drPay.ForEach(it => { it.ABBNo = ABB_No; it.Action = 1; });

                        #region Insert T_ABB

                        PosDS.T_ABBRow drAbb = ds.T_ABB.NewT_ABBRow();
                        drAbb.ABBTid = Guid.NewGuid();
                        drAbb.TransactionId = drBooking.TransactionId;
                        drAbb.PNR_No = drBooking.PNR_No;
                        drAbb.TaxInvoiceNo = ABB_No;
                        drAbb.PAXCountADT = PaxADT_Count;
                        drAbb.PAXCountCHD = PaxCHD_Count;


                        drAbb.ADMINFEE = 0;
                        drAbb.Fuel = 0;
                        drAbb.INSURANCE = 0;
                        drAbb.Service = 0;
                        drAbb.OTHER = 0;
                        drAbb.VAT = 0;
                        drAbb.AIRPORTTAX = 0;
                        drAbb.REP = 0;
                        drAbb.Discount = 0;

                        drAbb.ADMINFEE_Ori = 0;
                        drAbb.Fuel_Ori = 0;
                        drAbb.INSURANCE_Ori = 0;
                        drAbb.Service_Ori = 0;
                        drAbb.OTHER_Ori = 0;
                        drAbb.VAT_Ori = 0;
                        drAbb.AIRPORTTAX_Ori = 0;
                        drAbb.REP_Ori = 0;
                        drAbb.Discount_Ori = 0;


                        drAbb.PrintCount = 0;
                        drAbb.STATUS_INV = "ACTIVE";
                        drAbb.Create_By = UserCode;
                        drAbb.Create_Date = drBooking.Create_Date;
                        drAbb.Update_By = UserCode;
                        drAbb.Update_Date = drBooking.Create_Date;
                        PosDS.T_PaymentRow[] drPayA = (PosDS.T_PaymentRow[])ds.T_Payment.Select("Status = 'Approved' ");
                        drAbb.TOTAL = Pay_Amount * ExChangeRate;
                        drAbb.TOTAL_Ori = Pay_Amount;
                        drAbb.ExchangeRateTH = ExChangeRate;
                        drAbb.POS_Id = Pos_Id;
                        drAbb.Row_State = 1;
                        ds.T_ABB.AddT_ABBRow(drAbb);
                        #endregion

                        #region Insert T_ABBPAX

                        for (int i = 0; i < ds.T_Passenger.Count; i++)
                        {
                            PosDS.T_ABBPAXRow drABBPAX = ds.T_ABBPAX.NewT_ABBPAXRow();
                            drABBPAX.ABBPAXId = Guid.NewGuid();
                            drABBPAX.ABBTid = drAbb.ABBTid;
                            drABBPAX.PNR_No = drBooking.PNR_No;
                            drABBPAX.SEQ = i;
                            drABBPAX.FirstName = ds.T_Passenger[i].FirstName.ToUpper();
                            drABBPAX.LastName = ds.T_Passenger[i].LastName.ToUpper();
                            drABBPAX.Create_By = UserCode;
                            drABBPAX.Create_Date = CurrentDate;
                            drABBPAX.Update_By = UserCode;
                            drABBPAX.Update_Date = CurrentDate;
                            drABBPAX.Row_State = 1;
                            ds.T_ABBPAX.AddT_ABBPAXRow(drABBPAX);
                        }
                        #endregion

                        #region Insert T_ABBPAYMENTMETHOD
                        PosDS.T_ABBPAYMENTMETHODRow drABB_pay;
                        var dtPay = from pay in ds.T_Payment.AsEnumerable()
                                    join paytype in dtPaymentType.AsEnumerable() on pay.PaymentMethodCode equals paytype["PaymentType_Code"].ToString()
                                    where paytype["IsActive"].ToString() == "Y"
                                    select new
                                    {
                                        PaymentMethodCode = pay.PaymentMethodCode,
                                        PaymentType_Name = paytype["PaymentType_Name"].ToString(),
                                        PaymentAmount = pay.PaymentAmount,
                                        CurrencyCode = pay.CurrencyCode,
                                        ApprovalDate = pay.ApprovalDate,
                                        AgentCode = pay.AgentCode
                                    };



                        int seq = 0;
                        dtPay = dtPay.OrderBy(o => o.ApprovalDate);
                        foreach (var item in dtPay)
                        {
                            drABB_pay = ds.T_ABBPAYMENTMETHOD.NewT_ABBPAYMENTMETHODRow();
                            drABB_pay.ABBPAYId = Guid.NewGuid();
                            drABB_pay.ABBTid = drAbb.ABBTid;
                            drABB_pay.PNR_No = drBooking.PNR_No;
                            drABB_pay.SEQ = seq;
                            drABB_pay.CurrencyCode = item.CurrencyCode;
                            drABB_pay.PaymentMethodCode = item.PaymentMethodCode;
                            drABB_pay.PaymentType_Name = item.PaymentType_Name;
                            drABB_pay.PaymentAmount = item.PaymentAmount * ExChangeRate;

                            drABB_pay.PaymentAmount_Ori = item.PaymentAmount;
                            drABB_pay.ExchangeRateTH = ExChangeRate;
                            drABB_pay.ApprovalDate = item.ApprovalDate;
                            drABB_pay.AgentCode = item.AgentCode;
                            drABB_pay.Create_By = UserCode;
                            drABB_pay.Create_Date = CurrentDate;
                            drABB_pay.Update_By = UserCode;
                            drABB_pay.Update_Date = CurrentDate;
                            drABB_pay.Row_State = 1;
                            ds.T_ABBPAYMENTMETHOD.AddT_ABBPAYMENTMETHODRow(drABB_pay);
                            seq++;
                        }

                        #endregion

                        #region Insert ABBFARE
                        PosDS.T_ABBFARERow drAbbFare = ds.T_ABBFARE.NewT_ABBFARERow();
                        var dtSeg = from seg in ds.T_Segment.AsEnumerable()
                                    join pax in ds.T_PaxFare.AsEnumerable() on seg.SegmentTId equals pax.SegmentTId
                                    join service in ds.T_BookingServiceCharge.AsEnumerable() on pax.PaxFareTId equals service.PaxFareTId
                                    where service.ChargeType == "FarePrice"
                                    select new
                                    {
                                        CarrierCode = seg.CarrierCode,
                                        FlightNumber = seg.FlightNumber,
                                        DepartureStation = seg.DepartureStation,
                                        ArrivalStation = seg.ArrivalStation,
                                        STD = seg.STD,
                                        STA = seg.STA,
                                        Amount = service.Amount,
                                        CurrencyCode = service.CurrencyCode
                                    };

                        var dtGseg = dtSeg.GroupBy(it => new { it.CarrierCode, it.FlightNumber, it.DepartureStation, it.ArrivalStation, it.STA, it.STD, it.CurrencyCode })
                             .Select(s => new
                             {
                                 s.Key.CarrierCode,
                                 s.Key.FlightNumber,
                                 s.Key.DepartureStation,
                                 s.Key.ArrivalStation,
                                 s.Key.STA,
                                 s.Key.STD,
                                 s.Key.CurrencyCode,
                                 AMT = s.Sum(ss => ss.Amount)

                             });
                        seq = 0;
                        foreach (var item in dtGseg.OrderBy(o => o.STD))
                        {
                            drAbbFare = ds.T_ABBFARE.NewT_ABBFARERow();
                            drAbbFare.ABBFAREId = Guid.NewGuid();
                            drAbbFare.ABBTid = drAbb.ABBTid;
                            drAbbFare.SEQ = seq++;
                            drAbbFare.PNR_No = drBooking.PNR_No;
                            drAbbFare.CarrierCode = item.CarrierCode;
                            drAbbFare.FlightNumber = item.FlightNumber;
                            drAbbFare.DepartureStation = item.DepartureStation;
                            drAbbFare.ArrivalStation = item.ArrivalStation;
                            drAbbFare.STA = item.STA;
                            drAbbFare.STD = item.STD;
                            drAbbFare.CurrencyCode = item.CurrencyCode;
                            drAbbFare.Amount_Ori = item.AMT * (PaxADT_Count + PaxCHD_Count);
                            drAbbFare.Amount = (item.AMT * ExChangeRate) * (PaxADT_Count + PaxCHD_Count);
                            drAbbFare.Create_By = UserCode;
                            drAbbFare.Create_Date = CurrentDate;
                            drAbbFare.Update_By = UserCode;
                            drAbbFare.Update_Date = CurrentDate;
                            drAbbFare.Row_State = 1;
                            ds.T_ABBFARE.AddT_ABBFARERow(drAbbFare);
                        }



                        #endregion

                    }
                    else
                    {
                        #region ประกาศตัวแปร

                        string AgencyCode = string.Empty;
                        string AgencyTypeCode = string.Empty;
                        decimal AdminFee_Amount = 0;
                        decimal AdminFee_DiscountAmount = 0;
                        decimal FUEL_Amount = 0;
                        decimal FUEL_DiscountAmount = 0;
                        decimal SERVICE_Amount = 0;
                        decimal SERVICE_DiscountAmount = 0;
                        decimal VAT_Amount = 0;
                        decimal VAT_DiscountAmount = 0;
                        decimal AIRPORTTAX_Amount = 0;
                        decimal AIRPORTTAX_DiscountAmount = 0;
                        decimal INSURANCE_Amount = 0;
                        decimal INSURANCE_DiscountAmount = 0;
                        decimal REP_Amount = 0;
                        decimal REP_DiscountAmount = 0;
                        decimal FarePrice_Amount = 0;
                        decimal FarePrice_DiscountAmount = 0;
                        decimal Other_Amount = 0;
                        decimal All_Discount = 0;
                        #endregion


                        PosDS dsCal = new PosDS();
                        PosDS.T_PassengerFeeServiceChargeRow[] drTempPassServ = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("IsFlag = 0 AND   FeeCreateDate <= #" + Convert.ToString(DateWhere) + "#");
                        PosDS.T_BookingServiceChargeRow[] drTempBookServ = (PosDS.T_BookingServiceChargeRow[])ds.T_BookingServiceCharge.Select("IsFlag = 0 AND   FareCreateDate <= #" + Convert.ToString(DateWhere) + "#");
                        foreach (PosDS.T_PassengerFeeServiceChargeRow item in drTempPassServ)
                            dsCal.T_PassengerFeeServiceCharge.ImportRow(item);
                        foreach (PosDS.T_BookingServiceChargeRow item in drTempBookServ)
                            dsCal.T_BookingServiceCharge.ImportRow(item);



                        AdminFee_Amount = GetSumAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "ADMINFEE", dtFlightFee);
                        AdminFee_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "ADMINFEE", dtFlightFee);

                        FUEL_Amount = GetSumAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "FUEL", dtFlightFee);
                        FUEL_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "FUEL", dtFlightFee);

                        SERVICE_Amount = GetSumAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "SERVICE", dtFlightFee);
                        SERVICE_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "SERVICE", dtFlightFee);

                        VAT_Amount = GetSumAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "VAT", dtFlightFee);
                        VAT_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "VAT", dtFlightFee);

                        AIRPORTTAX_Amount = GetSumAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX", dtFlightFee);
                        AIRPORTTAX_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX", dtFlightFee);

                        INSURANCE_Amount = GetSumAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "INSURANCE", dtFlightFee);
                        INSURANCE_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "INSURANCE", dtFlightFee);

                        REP_Amount = GetSumAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "REP", dtFlightFee);
                        REP_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsCal, PaxADT_Count, PaxCHD_Count, "REP", dtFlightFee);

                        FarePrice_Amount = GetSumAmountByFarePrice(ds, dsCal, PaxADT_Count, PaxCHD_Count);
                        FarePrice_DiscountAmount = GetSumDiscountAmountByFarePrice(ds, dsCal, PaxADT_Count, PaxCHD_Count);

                        Other_Amount = GetSumAmountByOther(ds, dsCal, PaxADT_Count, PaxCHD_Count);


                        // คำนวน ExChangeRate

                        if (PaymentCurrency != "THB")
                            ExChangeRate = DBHelper.GetCurrencyActiveDate(PaymentCurrency, ds.T_Booking[0].Booking_Date);

                        AdminFee_Amount = (AdminFee_Amount - AdminFee_DiscountAmount);
                        FUEL_Amount = (FUEL_Amount - FUEL_DiscountAmount);
                        SERVICE_Amount = (SERVICE_Amount - SERVICE_DiscountAmount);
                        VAT_Amount = (VAT_Amount - VAT_DiscountAmount);
                        AIRPORTTAX_Amount = (AIRPORTTAX_Amount - AIRPORTTAX_DiscountAmount);
                        INSURANCE_Amount = (INSURANCE_Amount - INSURANCE_DiscountAmount);
                        REP_Amount = (REP_Amount - REP_DiscountAmount);
                        FarePrice_Amount = (FarePrice_Amount - FarePrice_DiscountAmount);
                        //Other_Amount = Other_Amount ;
                        All_Discount = GetAllDiscount(ds, dsCal);

                        decimal TOTAL_Ori = Pay_Amount;
                        decimal charge_total = AdminFee_Amount + FUEL_Amount + SERVICE_Amount + VAT_Amount + AIRPORTTAX_Amount + INSURANCE_Amount + Other_Amount + FarePrice_Amount;
                        if (TOTAL_Ori != charge_total)
                        {
                            PosDS.ResultMessageRow msg = ds.ResultMessage.NewResultMessageRow();
                            msg.Message = "ข้อมูลอาจมีการเปลี่ยน Flight หรือซื้อ เพิ่ม ยอดชำระ ไม่เท่ากับ ค่าใช้จ่าย แต่ละ ABB";
                            ds.ResultMessage.AddResultMessageRow(msg);
                            return ds;
                        }


                        #region Insert T_ABB

                        PosDS.T_ABBRow drAbb = ds.T_ABB.NewT_ABBRow();
                        drAbb.ABBTid = Guid.NewGuid();
                        drAbb.TransactionId = drBooking.TransactionId;
                        drAbb.PNR_No = drBooking.PNR_No;
                        drAbb.TaxInvoiceNo = ABB_No;
                        drAbb.PAXCountADT = PaxADT_Count;
                        drAbb.PAXCountCHD = PaxCHD_Count;

                        drAbb.ADMINFEE = AdminFee_Amount * ExChangeRate;
                        drAbb.Fuel = FUEL_Amount * ExChangeRate;
                        drAbb.INSURANCE = INSURANCE_Amount * ExChangeRate;
                        drAbb.Service = SERVICE_Amount * ExChangeRate;
                        drAbb.OTHER = Other_Amount * ExChangeRate;
                        drAbb.VAT = VAT_Amount * ExChangeRate;
                        drAbb.AIRPORTTAX = AIRPORTTAX_Amount * ExChangeRate;
                        drAbb.REP = REP_Amount * ExChangeRate;
                        drAbb.Discount = All_Discount * ExChangeRate;
                        drAbb.ADMINFEE_Ori = AdminFee_Amount;
                        drAbb.Fuel_Ori = FUEL_Amount;
                        drAbb.INSURANCE_Ori = INSURANCE_Amount;
                        drAbb.Service_Ori = SERVICE_Amount;
                        drAbb.OTHER_Ori = Other_Amount;
                        drAbb.VAT_Ori = VAT_Amount;
                        drAbb.AIRPORTTAX_Ori = AIRPORTTAX_Amount;
                        drAbb.REP_Ori = REP_Amount;
                        drAbb.Discount_Ori = All_Discount;


                        drAbb.PrintCount = 0;
                        drAbb.STATUS_INV = "ACTIVE";
                        drAbb.Create_By = UserCode;
                        drAbb.Create_Date = drBooking.Create_Date;
                        drAbb.Update_By = UserCode;
                        drAbb.Update_Date = drBooking.Create_Date;
                        //PosDS.T_PaymentRow[] drPayA = (PosDS.T_PaymentRow[])ds.T_Payment.Select("Status = 'Approved' ");
                        drAbb.TOTAL_Ori = drPay.Sum(it => it.PaymentAmount);
                        drAbb.TOTAL = drAbb.TOTAL_Ori * ExChangeRate;
                        drAbb.ExchangeRateTH = ExChangeRate;
                        drAbb.POS_Id = Pos_Id;
                        drAbb.Row_State = 1;
                        ds.T_ABB.AddT_ABBRow(drAbb);
                        #endregion

                        #region Insert T_ABBPAX
                        int i = 0;
                        foreach (PosDS.T_PassengerRow item in ds.T_Passenger.Select("Action <> -1"))
                        {
                            PosDS.T_ABBPAXRow drABBPAX = ds.T_ABBPAX.NewT_ABBPAXRow();
                            drABBPAX.ABBPAXId = Guid.NewGuid();
                            drABBPAX.ABBTid = drAbb.ABBTid;
                            drABBPAX.PNR_No = drBooking.PNR_No;
                            drABBPAX.SEQ = i++;
                            drABBPAX.FirstName = item.FirstName.ToUpper();
                            drABBPAX.LastName = item.LastName.ToUpper();
                            drABBPAX.Create_By = UserCode;
                            drABBPAX.Create_Date = CurrentDate;
                            drABBPAX.Update_By = UserCode;
                            drABBPAX.Update_Date = CurrentDate;
                            drABBPAX.Row_State = 1;
                            ds.T_ABBPAX.AddT_ABBPAXRow(drABBPAX);
                        }
                        #endregion

                        #region Insert T_ABBPAYMENTMETHOD
                        PosDS.T_ABBPAYMENTMETHODRow drABB_pay;
                        var dtPay = from pay in drPay
                                    join paytype in dtPaymentType.AsEnumerable() on pay.PaymentMethodCode equals paytype["PaymentType_Code"].ToString()
                                    where paytype["IsActive"].ToString() == "Y"
                                    select new
                                    {
                                        PaymentMethodCode = pay.PaymentMethodCode,
                                        PaymentType_Name = paytype["PaymentType_Name"].ToString(),
                                        PaymentAmount = pay.PaymentAmount,
                                        CurrencyCode = pay.CurrencyCode,
                                        ApprovalDate = pay.ApprovalDate,
                                        AgentCode = pay.AgentCode
                                    };



                        int seq = 0;
                        dtPay = dtPay.OrderBy(o => o.ApprovalDate);
                        foreach (var item in dtPay)
                        {
                            drABB_pay = ds.T_ABBPAYMENTMETHOD.NewT_ABBPAYMENTMETHODRow();
                            drABB_pay.ABBPAYId = Guid.NewGuid();
                            drABB_pay.ABBTid = drAbb.ABBTid;
                            drABB_pay.PNR_No = drBooking.PNR_No;
                            drABB_pay.SEQ = seq;
                            drABB_pay.CurrencyCode = item.CurrencyCode;
                            drABB_pay.PaymentMethodCode = item.PaymentMethodCode;
                            drABB_pay.PaymentType_Name = item.PaymentType_Name;
                            drABB_pay.PaymentAmount = item.PaymentAmount * ExChangeRate;
                            drABB_pay.PaymentAmount_Ori = item.PaymentAmount;
                            drABB_pay.ExchangeRateTH = ExChangeRate;
                            drABB_pay.AgentCode = AgencyCode;
                            drABB_pay.ApprovalDate = item.ApprovalDate;
                            drABB_pay.Create_By = UserCode;
                            drABB_pay.Create_Date = CurrentDate;
                            drABB_pay.Update_By = UserCode;
                            drABB_pay.Update_Date = CurrentDate;
                            drABB_pay.Row_State = 1;
                            ds.T_ABBPAYMENTMETHOD.AddT_ABBPAYMENTMETHODRow(drABB_pay);
                            seq++;
                        }

                        #endregion

                        #region Insert ABBFARE
                        PosDS.T_ABBFARERow drAbbFare = ds.T_ABBFARE.NewT_ABBFARERow();
                        var dtSeg = from seg in ds.T_Segment.AsEnumerable()
                                    join pax in ds.T_PaxFare.AsEnumerable() on seg.SegmentTId equals pax.SegmentTId
                                    join service in ds.T_BookingServiceCharge.AsEnumerable() on pax.PaxFareTId equals service.PaxFareTId
                                    where service.ChargeType == "FarePrice"
                                    select new
                                    {
                                        CarrierCode = seg.CarrierCode,
                                        FlightNumber = seg.FlightNumber,
                                        DepartureStation = seg.DepartureStation,
                                        ArrivalStation = seg.ArrivalStation,
                                        STD = seg.STD,
                                        STA = seg.STA,
                                        PaxType = pax.PaxType,
                                        Amount = service.Amount,
                                        CurrencyCode = service.CurrencyCode
                                    };

                        var dtGseg = dtSeg.GroupBy(it => new { it.CarrierCode, it.FlightNumber, it.DepartureStation, it.ArrivalStation, it.STA, it.STD, it.PaxType, it.CurrencyCode })
                             .Select(s => new
                             {
                                 s.Key.CarrierCode,
                                 s.Key.FlightNumber,
                                 s.Key.DepartureStation,
                                 s.Key.ArrivalStation,
                                 s.Key.STA,
                                 s.Key.STD,
                                 s.Key.CurrencyCode,
                                 s.Key.PaxType,
                                 AMT = s.Sum(ss => ss.Amount)

                             });
                        seq = 0;


                        foreach (var item in ds.T_Segment.OrderBy(o => o.STD))
                        {
                            drAbbFare = ds.T_ABBFARE.NewT_ABBFARERow();
                            drAbbFare.ABBFAREId = Guid.NewGuid();
                            drAbbFare.ABBTid = drAbb.ABBTid;
                            drAbbFare.SEQ = seq++;
                            drAbbFare.PNR_No = drBooking.PNR_No;
                            drAbbFare.CarrierCode = item.CarrierCode;
                            drAbbFare.FlightNumber = item.FlightNumber;
                            drAbbFare.DepartureStation = item.DepartureStation;
                            drAbbFare.ArrivalStation = item.ArrivalStation;
                            drAbbFare.STA = item.STA;
                            drAbbFare.STD = item.STD;
                            drAbbFare.CurrencyCode = PaymentCurrency;
                            if (FarePrice_Amount == 0)
                            {
                                drAbbFare.Amount_Ori = 0;
                                drAbbFare.Amount = 0;
                            }
                            else
                            {
                                decimal sum_ADT = 0;
                                decimal sum_CHD = 0;
                                foreach (var item2 in dtGseg)
                                {
                                    if (item2.CarrierCode.Trim() == item.CarrierCode.Trim() && item2.FlightNumber.Trim() == item.FlightNumber.Trim())
                                    {
                                        if (item2.PaxType == "ADT")
                                            sum_ADT += item2.AMT * PaxADT_Count;
                                        if(item2.PaxType == "CHD")
                                            sum_CHD += item2.AMT * PaxCHD_Count;

                                    }
                                }
                                drAbbFare.Amount_Ori = sum_ADT + sum_CHD;
                                drAbbFare.Amount = (drAbbFare.Amount_Ori * ExChangeRate);
                            }
                            drAbbFare.ExchangeRateTH = ExChangeRate;
                            drAbbFare.Create_By = UserCode;
                            drAbbFare.Create_Date = CurrentDate;
                            drAbbFare.Update_By = UserCode;
                            drAbbFare.Update_Date = CurrentDate;
                            drAbbFare.Row_State = 1;
                            ds.T_ABBFARE.AddT_ABBFARERow(drAbbFare);
                        }

                        //foreach (var item in dtGseg.OrderBy(o => o.STD))
                        //{
                        //    drAbbFare = ds.T_ABBFARE.NewT_ABBFARERow();
                        //    drAbbFare.ABBFAREId = Guid.NewGuid();
                        //    drAbbFare.ABBTid = drAbb.ABBTid;
                        //    drAbbFare.SEQ = seq++;
                        //    drAbbFare.PNR_No = drBooking.PNR_No;
                        //    drAbbFare.CarrierCode = item.CarrierCode;
                        //    drAbbFare.FlightNumber = item.FlightNumber;
                        //    drAbbFare.DepartureStation = item.DepartureStation;
                        //    drAbbFare.ArrivalStation = item.ArrivalStation;
                        //    drAbbFare.STA = item.STA;
                        //    drAbbFare.STD = item.STD;
                        //    drAbbFare.CurrencyCode = item.CurrencyCode;
                        //    if (FarePrice_Amount == 0)
                        //    {
                        //        drAbbFare.Amount_Ori = 0;
                        //        drAbbFare.Amount = 0;
                        //    }
                        //    else
                        //    {
                        //        drAbbFare.Amount_Ori = item.AMT;
                        //        drAbbFare.Amount = (drAbbFare.Amount_Ori * ExChangeRate);
                        //    }
                        //    drAbbFare.ExchangeRateTH = ExChangeRate;
                        //    drAbbFare.Create_By = UserCode;
                        //    drAbbFare.Create_Date = CurrentDate;
                        //    drAbbFare.Update_By = UserCode;
                        //    drAbbFare.Update_Date = CurrentDate;
                        //    drAbbFare.Row_State = 1;
                        //    ds.T_ABBFARE.AddT_ABBFARERow(drAbbFare);
                        //}

                        #endregion

                        foreach (PosDS.T_PassengerFeeServiceChargeRow item in drTempPassServ)
                        {
                            item.IsFlag = true;
                            item.ABBNo = ABB_No;

                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }
                        foreach (PosDS.T_BookingServiceChargeRow item in drTempBookServ)
                        {
                            item.IsFlag = true;
                            item.ABBNo = ABB_No;
                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }

                        foreach (PosDS.T_BookingRow item in ds.T_Booking)
                        {
                            item.ABBNo = ABB_No;
                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }


                        foreach (PosDS.T_PassengerRow item in ds.T_Passenger.Select("ABBNo IS NULL"))
                        {
                            item.ABBNo = ABB_No;
                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }

                        foreach (PosDS.T_PassengerFeeRow item in ds.T_PassengerFee.Select("ABBNo IS NULL"))
                        {
                            item.ABBNo = ABB_No;
                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }


                        foreach (PosDS.T_SegmentRow item in ds.T_Segment.Select("ABBNo IS NULL"))
                        {
                            item.ABBNo = ABB_No;
                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }

                        foreach (PosDS.T_PaxFareRow item in ds.T_PaxFare.Select("ABBNo IS NULL"))
                        {
                            item.ABBNo = ABB_No;
                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }

                        foreach (PosDS.T_PaymentRow item in drPay)
                        {
                            item.ABBNo = ABB_No;
                            item.IsFlag = true;
                            if (item.Row_State == 0)
                                item.Row_State = 2;
                        }
                    }




                }
            }
            catch (Exception ex)
            {
                PosDS.ResultMessageRow msg = ds.ResultMessage.NewResultMessageRow();
                msg.Message = ex.Message;
                ds.ResultMessage.AddResultMessageRow(msg);
                return ds;
            }
            return ds;
        }
        
        
        [WebMethod]
         public PosDS ProcessDataDevide(PosDS ds)
        {
            DBHelper da = new DBHelper();
            string PNR_No = string.Empty;
            PNR_No = ds.T_Booking[0].PNR_No;
            PosDS dsDB = DBHelper.GetBookingAllData(PNR_No);
            PosDS dsXML = ds;
            int PaxCountDB = Convert.ToInt32(dsDB.T_Booking[0].PaxCount);
            int PaxCountXML = Convert.ToInt32(dsXML.T_Booking[0].PaxCount);
            CompareBooking(ref dsDB, ref dsXML);

            ComparePassenger(ref dsDB, ref dsXML);
            ComparePassengerFee(ref dsDB, ref dsXML);
            ComparePassengerFeeServiceCharge(ref dsDB, ref dsXML);
            CompareSegment(ref dsDB, ref dsXML);
            ComparePax(ref dsDB, ref dsXML);
            CompareBookingServiceCharge(ref dsDB, ref dsXML);
            ComparePayment(ref dsDB, ref dsXML);


            if (PaxCountDB > PaxCountXML) // Devile
            {

                foreach (PosDS.T_BookingServiceChargeRow item in dsDB.T_BookingServiceCharge)
                {
                    int count = 1;
                    var obj = dsDB.T_PaxFare.Where(it => it.PaxFareTId == item.PaxFareTId).FirstOrDefault();
                    if (obj != null)
                    {
                        string PaxType = obj.PaxType;
                        count = dsDB.T_Passenger.Where(it => it.PaxType == PaxType && it.Action != -1).ToList().Count;
                    }
                    item.QTY = count;
                    item.Action = 2;
                }
            }
            return dsDB;
        }


        [WebMethod]
        public bool PosSave(PosDS ds)
        {

            DBHelper da = new DBHelper();

            try
            {
                da.DBOpenConnection();
                da.BeginTran();
                bool success;
                #region T_Booking

                foreach (PosDS.T_BookingRow item in ds.T_Booking)
                {
                    if (item.Row_State == 1)
                        success = da.InsertBooking(item);
                    else if (item.Row_State == 2)
                        success = da.UpdateBooking(item);
                }

                #endregion

                #region Insert T_Passenger

                foreach (PosDS.T_PassengerRow item in ds.T_Passenger)
                {
                    if (item.Row_State == 1)
                        success = da.InsertPassenger(item);
                    else if (item.Row_State == 2)
                        success = da.UpdatePassenger(item);
                }

                #endregion

                #region T_Payment


                foreach (PosDS.T_PaymentRow item in ds.T_Payment)
                {
                    if (item.Row_State == 1)
                        success = da.InsertPayment(item);
                    else if (item.Row_State == 2)
                        success = da.UpdatePayment(item);
                }
                #endregion

                #region T_BookingServiceCharge

                foreach (PosDS.T_BookingServiceChargeRow item in ds.T_BookingServiceCharge)
                {
                    if (item.Row_State == 1)
                        success = da.InsertBookingServiceCharge(item);
                    else if (item.Row_State == 2)
                        success = da.UpdateBookingServiceCharge(item);
                }

                #endregion

                #region Segment
                foreach (PosDS.T_SegmentRow item in ds.T_Segment)
                {
                    if (item.Row_State == 1)
                        success = da.InsertSegment(item);
                    else if (item.Row_State == 2)
                        success = da.UpdateSegment(item);
                }
                #endregion

                #region T_PaxFare


                foreach (PosDS.T_PaxFareRow item in ds.T_PaxFare)
                {
                    if (item.Row_State == 1)
                        success = da.InsertPaxFare(item);
                    else if (item.Row_State == 2)
                        success = da.UpdatePaxFare(item);
                }

                #endregion

                #region PassengerFee
                foreach (PosDS.T_PassengerFeeRow item in ds.T_PassengerFee)
                {
                    if (item.Row_State == 1)
                        success = da.InsertPassengerFee(item);
                    else if (item.Row_State == 2)
                        success = da.UpdatePassengerFee(item);
                }
                #endregion

                #region T_PassengerFeeServiceCharge

                foreach (PosDS.T_PassengerFeeServiceChargeRow item in ds.T_PassengerFeeServiceCharge)
                {
                    if (item.Row_State == 1)
                        success = da.InsertPassServiceCharge(item);
                    else if (item.Row_State == 2)
                        success = da.UpdatePassServiceCharge(item);
                }

                #endregion

                #region T_ABB


                foreach (PosDS.T_ABBRow item in ds.T_ABB)
                {
                    if (item.Row_State == 1)
                        success = da.InsertABB(item);
                }

                #endregion

                #region T_ABBPAX

                foreach (PosDS.T_ABBPAXRow item in ds.T_ABBPAX)
                {
                    if (item.Row_State == 1)
                        success = da.InsertABBPAX(item);
                }
                #endregion

                #region T_ABBPAYMENTMETHOD

                foreach (PosDS.T_ABBPAYMENTMETHODRow item in ds.T_ABBPAYMENTMETHOD)
                {
                    if (item.Row_State == 1)
                        success = da.InsertABBPAYMENTMETHOD(item);
                }

                #endregion

                #region ABBFARE

                foreach (PosDS.T_ABBFARERow item in ds.T_ABBFARE)
                {
                    if (item.Row_State == 1)
                        success = da.InsertABBFARE(item);
                }

                #endregion

                da.EndTran(true);
                da.DBCloseConnection();
                return true;

            }
            catch (Exception ex)
            {
                da.EndTran(false);
                da.DBCloseConnection();
                return false;
            }

        }

        #region Method For ProcessBooking

        private decimal GetSumAmountByAbbGroup(PosDS ds, PosDS dsCal, int PaxADT_Count, int PaxCHD_Count, string AbbGroup, DataTable dtFlightFee)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                              join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                              table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "ADT" && table1.IsABBNoNull()
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "CHD" && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount * PaxCHD_Count,
                          };
                decimal sum2 = results.Sum(x => x.Amount);


                // Fee
                results = from table1 in dsCal.T_PassengerFeeServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount,
                          };
                decimal sum3 = results.Sum(x => x.Amount);

                // ds.T_BookingServiceCharge.Where(it => it.ChargeType != "Discount" && it.ChargeType != "PromotionDiscount");

                SumAmount = sum1 + sum2 + sum3;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }

        private decimal GetSumDuscountAmountByAbbGroup(PosDS ds, PosDS dsCal, int PaxADT_Count, int PaxCHD_Count, string AbbGroup, DataTable dtFlightFee)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                              join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                              table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "ADT" && table1.IsABBNoNull()
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "CHD" && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount * PaxCHD_Count,
                          };
                decimal sum2 = results.Sum(x => x.Amount);


                // Fee
                results = from table1 in dsCal.T_PassengerFeeServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount,
                          };
                decimal sum3 = results.Sum(x => x.Amount);


                SumAmount = sum1 + sum2 + sum3;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }

        private decimal GetSumAmountByFarePrice(PosDS ds, PosDS dsCal, int PaxADT_Count, int PaxCHD_Count)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType == "FarePrice" && pax_fare.PaxType == "ADT" && table1.IsABBNoNull()
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType == "FarePrice" && pax_fare.PaxType == "CHD" && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount * PaxCHD_Count,
                          };
                decimal sum2 = results.Sum(x => x.Amount);


                SumAmount = sum1 + sum2;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }

        private decimal GetSumDiscountAmountByFarePrice(PosDS ds, PosDS dsCal, int PaxADT_Count, int PaxCHD_Count)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType == "" && pax_fare.PaxType == "ADT" && (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table1.IsABBNoNull()
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType == "" && pax_fare.PaxType == "CHD" && (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount * PaxCHD_Count,
                          };
                decimal sum2 = results.Sum(x => x.Amount);


                SumAmount = sum1 + sum2;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }

        private decimal GetSumAmountByOther(PosDS ds, PosDS dsCal, int PaxADT_Count, int PaxCHD_Count)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType != "FarePrice" && table1.ChargeCode == "" && pax_fare.PaxType == "ADT" && table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table1.IsABBNoNull()
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType != "FarePrice" && table1.ChargeCode == "" && pax_fare.PaxType == "CHD" && table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount * PaxCHD_Count,
                          };
                decimal sum2 = results.Sum(x => x.Amount);


                SumAmount = sum1 + sum2;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }

        private decimal GetAllDiscount(PosDS ds, PosDS dsCal)
        {
            decimal SumAmount = 0;

            try
            {
                // FEE
                var results = from table1 in dsCal.T_BookingServiceCharge.AsEnumerable()
                              where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table1.IsABBNoNull()
                              select new
                              {
                                  Amount = table1.Amount
                              };
                SumAmount = results.Sum(x => x.Amount);

                results = from table1 in dsCal.T_PassengerFeeServiceCharge.AsEnumerable()
                          where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table1.IsABBNoNull()
                          select new
                          {
                              Amount = table1.Amount
                          };

                SumAmount = SumAmount + results.Sum(x => x.Amount);


            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }

        public string RunningAbb(PosDS.T_PaymentRow item)
        {
            DataTable dtAgency = DBHelper.GetAgency();
            string AgencyCode = string.Empty;
            string AgencyTypeCode = string.Empty;
            long Last_INV = 1;
            string ABB_No = string.Empty;
            DateTime PaymentDate = item.ApprovalDate;
            DataRow[] drAgency = dtAgency.Select("Agency_Code='" + item.AgentCode + "'");
            if (drAgency.Length > 0 && drAgency[0]["Agency_Code"].ToString() != string.Empty)
            {
                AgencyCode = drAgency[0]["Agency_Code"].ToString();
                AgencyTypeCode = (drAgency[0]["Agency_Type_Code"] == null || drAgency[0]["Agency_Type_Code"].ToString() == string.Empty) ? "000" : drAgency[0]["Agency_Type_Code"].ToString();

                DataTable dtmac = DBHelper.GetAgencyTypeByCode(AgencyTypeCode);
                if (dtmac.Rows.Count > 0)
                {
                    Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                    ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + AgencyTypeCode + Last_INV.ToString("00000000");
                    DBHelper.UpdateAgencyType(Convert.ToInt32(dtmac.Rows[0]["AgencyTypeId"]), Last_INV);
                }
                dtmac.Dispose();

            }


            if (AgencyCode == string.Empty)
            {
                string PosCode = "000"; // Default ตาม Store
                DataTable dtmac = DBHelper.GetPosMachineByCode(PosCode);
                if (dtmac.Rows.Count > 0)
                {
                    Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                    DBHelper.UpdatePOSMachine(PosCode, Last_INV);
                }
                ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + "001" + Last_INV.ToString("00000000");
                dtmac.Dispose();
            }

            return ABB_No;
        }

        #endregion



        #region  Method

        #region Compare Data

        void CompareBooking(ref PosDS dsDB, ref PosDS dsXML)
        {
            dsDB.T_Booking[0].Action = 2;
            dsDB.T_Booking[0].Row_State = 2;
            dsDB.T_Booking[0].PaxCount = dsXML.T_Booking[0].PaxCount;
            dsDB.T_Booking[0].TotalCost = dsXML.T_Booking[0].TotalCost;
        }

        void ComparePassenger(ref PosDS dsDB, ref PosDS dsXML)
        {
            foreach (PosDS.T_PassengerRow item_Db in dsDB.T_Passenger)
            {
                int status = -1;
                foreach (PosDS.T_PassengerRow item_Xml in dsXML.T_Passenger)
                {
                    if (item_Db.PassengerId == item_Xml.PassengerId)
                    {
                        status = 0;
                        break;
                    }
                }

                if (status == -1)
                {
                    item_Db.Action = -1;
                }


            }

            // Check Add 

            foreach (PosDS.T_PassengerRow item_Xml in dsXML.T_Passenger)
            {
                PosDS.T_PassengerRow[] dr = (PosDS.T_PassengerRow[])dsDB.T_Passenger.Select("PassengerId = " + item_Xml.PassengerId);
                if (dr.Length == 0)
                {
                    item_Xml.TransactionId = dsDB.T_Booking[0].TransactionId;
                    item_Xml.Action = 1;
                    item_Xml.Row_State = 1;
                    dsDB.T_Passenger.ImportRow(item_Xml);
                }
            }
        }

        void CompareSegment(ref PosDS dsDB, ref PosDS dsXML)
        {
            foreach (PosDS.T_SegmentRow item_Db in dsDB.T_Segment)
            {
                int status = -1;
                foreach (PosDS.T_SegmentRow item_Xml in dsXML.T_Segment)
                {
                    string DB_Flight = item_Db.CarrierCode + item_Db.FlightNumber;
                    string Xml_Flight = item_Xml.CarrierCode + item_Xml.FlightNumber;

                    if (DB_Flight == Xml_Flight)
                    {
                        status = 0;
                        break;
                    }
                }

                if (status == -1)
                {
                    item_Db.Action = -1;
                }


            }

            // Check Add 

            foreach (PosDS.T_SegmentRow item_Xml in dsXML.T_Segment)
            {
                PosDS.T_SegmentRow[] dr = (PosDS.T_SegmentRow[])dsDB.T_Segment.Select("CarrierCode = '" + item_Xml.CarrierCode + "' AND  FlightNumber = '" + item_Xml.FlightNumber + "'");
                if (dr.Length == 0)
                {
                    item_Xml.TransactionId = dsDB.T_Booking[0].TransactionId;
                    item_Xml.Action = 1;
                    item_Xml.Row_State = 1;
                    dsDB.T_Segment.ImportRow(item_Xml);
                }
            }
        }

        void ComparePax(ref PosDS dsDB, ref PosDS dsXML)
        {
            foreach (PosDS.T_PaxFareRow item_Db in dsDB.T_PaxFare)
            {
                int status = -1;
                foreach (PosDS.T_PaxFareRow item_Xml in dsXML.T_PaxFare)
                {
                    string DB_Compare = item_Db.SeqmentPaxNo;
                    string Xml_Compare = item_Xml.SeqmentPaxNo;

                    if (DB_Compare == Xml_Compare)
                    {
                        status = 0;
                        break;
                    }
                }

                if (status == -1)
                {
                    item_Db.Action = -1;
                }


            }

            // Check Add 

            foreach (PosDS.T_PaxFareRow item_Xml in dsXML.T_PaxFare)
            {
                PosDS.T_PaxFareRow[] dr = (PosDS.T_PaxFareRow[])dsDB.T_PaxFare.Select("SeqmentPaxNo = '" + item_Xml.SeqmentPaxNo + "'");
                if (dr.Length == 0)
                {
                    item_Xml.Row_State = 1;
                    item_Xml.Action = 1;
                    dsDB.T_PaxFare.ImportRow(item_Xml);
                }
            }
        }

        void CompareBookingServiceCharge(ref PosDS dsDB, ref PosDS dsXML)
        {
            foreach (PosDS.T_BookingServiceChargeRow item_Db in dsDB.T_BookingServiceCharge)
            {
                int status = -1;
                foreach (PosDS.T_BookingServiceChargeRow item_Xml in dsXML.T_BookingServiceCharge)
                {
                    string DB_Compare = item_Db.ServiceChargeNo;
                    string Xml_Compare = item_Xml.ServiceChargeNo;

                    if (DB_Compare == Xml_Compare)
                    {
                        status = 0;
                        break;
                    }
                }

                if (status == -1)
                {
                    item_Db.Action = -1;
                }


            }

            // Check Add 

            foreach (PosDS.T_BookingServiceChargeRow item_Xml in dsXML.T_BookingServiceCharge)
            {
                PosDS.T_BookingServiceChargeRow[] dr = (PosDS.T_BookingServiceChargeRow[])dsDB.T_BookingServiceCharge.Select("ServiceChargeNo = '" + item_Xml.ServiceChargeNo + "'");
                if (dr.Length == 0)
                {
                    item_Xml.Row_State = 1;
                    item_Xml.Action = 1;
                    dsDB.T_BookingServiceCharge.ImportRow(item_Xml);
                }
            }
        }

        void ComparePassengerFee(ref PosDS dsDB, ref PosDS dsXML)
        {
            //foreach (PosDS.T_PassengerFeeRow item_Db in dsDB.T_PassengerFee)
            //{
            //    int status = -1;
            //    foreach (PosDS.T_PassengerFeeRow item_Xml in dsXML.T_PassengerFee)
            //    {
            //        string DB_Compare = item_Db.PassengerFeeNo.ToString();
            //        string Xml_Compare = item_Xml.PassengerFeeNo.ToString();

            //        if (DB_Compare == Xml_Compare)
            //        {
            //            status = 0;
            //            break;
            //        }
            //    }

            //    if (status == -1)
            //    {
            //        item_Db.Action = -1;
            //    }


            //}

            // Check Add 

            foreach (PosDS.T_PassengerFeeRow item_Xml in dsXML.T_PassengerFee)
            {
                PosDS.T_PassengerFeeRow[] dr = (PosDS.T_PassengerFeeRow[])dsDB.T_PassengerFee.Select("PassengerFeeNo = '" + item_Xml.PassengerFeeNo + "'");
                if (dr.Length == 0)
                {
                    PosDS.T_PassengerRow[] drPass = (PosDS.T_PassengerRow[])dsDB.T_Passenger.Select("PassengerId = '" + item_Xml.PassengerId + "'");
                    if (drPass.Length > 0)
                        item_Xml.PassengerTId = drPass[0].PassengerTId;
                    item_Xml.Action = 1;
                    item_Xml.Row_State = 1;
                    dsDB.T_PassengerFee.ImportRow(item_Xml);
                }
            }
        }

        void ComparePassengerFeeServiceCharge(ref PosDS dsDB, ref PosDS dsXML)
        {
            foreach (PosDS.T_PassengerFeeServiceChargeRow item_Db in dsDB.T_PassengerFeeServiceCharge)
            {
                int status = -1;
                foreach (PosDS.T_PassengerFeeServiceChargeRow item_Xml in dsXML.T_PassengerFeeServiceCharge)
                {
                    string DB_Compare = item_Db.ServiceChargeNo;
                    string Xml_Compare = item_Xml.ServiceChargeNo;

                    if (DB_Compare == Xml_Compare)
                    {
                        status = 0;
                        break;
                    }
                }

                if (status == -1)
                {
                    item_Db.Action = -1;
                }


            }

            // Check Add 

            foreach (PosDS.T_PassengerFeeServiceChargeRow item_Xml in dsXML.T_PassengerFeeServiceCharge)
            {
                PosDS.T_PassengerFeeServiceChargeRow[] dr = (PosDS.T_PassengerFeeServiceChargeRow[])dsDB.T_PassengerFeeServiceCharge.Select("ServiceChargeNo = '" + item_Xml.ServiceChargeNo + "'");
                if (dr.Length == 0)
                {
                    item_Xml.Row_State = 1;
                    item_Xml.Action = 1;
                    dsDB.T_PassengerFeeServiceCharge.ImportRow(item_Xml);
                }
            }
        }

        void ComparePayment(ref PosDS dsDB, ref PosDS dsXML)
        {
            foreach (PosDS.T_PaymentRow item_Db in dsDB.T_Payment)
            {
                int status = -1;
                foreach (PosDS.T_PaymentRow item_Xml in dsXML.T_Payment)
                {
                    string DB_Compare = item_Db.PaymentID.ToString();
                    string Xml_Compare = item_Xml.PaymentID.ToString();

                    if (DB_Compare == Xml_Compare)
                    {
                        status = 0;
                        break;
                    }
                }

                if (status == -1)
                {
                    item_Db.Action = -1;
                }


            }

            // Check Add 

            foreach (PosDS.T_PaymentRow item_Xml in dsXML.T_Payment)
            {
                PosDS.T_PaymentRow[] dr = (PosDS.T_PaymentRow[])dsDB.T_Payment.Select("PaymentID = '" + item_Xml.PaymentID + "'");
                if (dr.Length == 0)
                {
                    item_Xml.Row_State = 1;
                    item_Xml.Action = 1;
                    item_Xml.TransactionId = dsDB.T_Booking[0].TransactionId;
                    dsDB.T_Payment.ImportRow(item_Xml);
                }
            }
        }

        #endregion


        public PosDS UpdateItem(PosDS ds)
        {
            PosDS.T_PassengerFeeServiceChargeRow[] drpassSV = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("Action = -1 AND ChargeCode <> 'VAT'");
            foreach (PosDS.T_PassengerFeeServiceChargeRow item in drpassSV)
            {
                DataTable dt = DBHelper.GetFlightFeeGroup(item.ChargeCode);
                if (dt.Rows.Count > 0)
                {
                    PosDS.T_PassengerFeeServiceChargeRow[] drpassSV_NEW = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("Action = 1 AND PassengerID = '" + item.PassengerId + "' AND ChargeCode <> 'VAT' AND IsFlag = 0  ");

                    if (drpassSV_NEW.Length > 0)
                    {
                        item.Row_State = 2;
                        item.Action = 2;
                        item.IsFlag = true;
                        drpassSV_NEW[0].PassengerFeeTId = item.PassengerFeeTId;
                        drpassSV_NEW[0].Row_State = 1;
                        drpassSV_NEW[0].IsFlag = true;
                        drpassSV_NEW[0].Action = 0;
                        drpassSV_NEW[0].BaseAmount = drpassSV_NEW[0].Amount;
                        drpassSV_NEW[0].Amount = drpassSV_NEW[0].Amount - item.Amount;

                        decimal vat = Convert.ToDecimal(drpassSV_NEW[0].BaseAmount) * 0.07M;
                        vat = Math.Round(vat, 2);

                        decimal vat_Old = Convert.ToDecimal(item.Amount) * 0.07M;
                        vat_Old = Math.Round(vat_Old, 2);

                        PosDS.T_PassengerFeeServiceChargeRow[] drpassSV_VAT_NEW = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("ACtion = 1 AND PassengerID = '" + item.PassengerId + "' AND ChargeCode = 'VAT' AND Amount = " + vat + " AND IsFlag = 0 ");

                        PosDS.T_PassengerFeeServiceChargeRow[] drpassSV_VAT_Old = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("ACtion = -1 AND PassengerID = '" + item.PassengerId + "' AND ChargeCode = 'VAT' AND Amount = " + vat_Old + " AND IsFlag = 0  ");

                        if (drpassSV_VAT_NEW.Length > 0)
                        {
                            drpassSV_VAT_NEW[0].Row_State = 1;
                            drpassSV_VAT_NEW[0].Action = 0;
                            drpassSV_VAT_NEW[0].BaseAmount = drpassSV_VAT_NEW[0].Amount;
                            drpassSV_VAT_NEW[0].Amount = drpassSV_VAT_NEW[0].Amount - vat_Old;
                            drpassSV_VAT_NEW[0].PassengerFeeTId = drpassSV_VAT_Old[0].PassengerFeeTId;
                            drpassSV_VAT_NEW[0].IsFlag = true;
                            drpassSV_VAT_Old[0].Row_State = 2;
                            drpassSV_VAT_Old[0].Action = 2;
                            drpassSV_VAT_Old[0].IsFlag = true;
                        }
                    }
                }
            }

            foreach (var item in ds.T_PassengerFeeServiceCharge)
            {
                item.IsFlag = false;
            }
            return ds;
        }


        public PosDS ChangeFlight(PosDS dsDB)
        {
            PosDS.T_BookingServiceChargeRow[] drFlightOld = (PosDS.T_BookingServiceChargeRow[])dsDB.T_BookingServiceCharge.Select("Action = -1");
            foreach (PosDS.T_BookingServiceChargeRow item in drFlightOld)
            {
                PosDS.T_BookingServiceChargeRow[] drSel = (PosDS.T_BookingServiceChargeRow[])dsDB.T_BookingServiceCharge.Select("Action = 1 AND ChargeCode ='" + item.ChargeCode + "'");
                if (drSel.Length > 0)
                {
                    decimal diff = drSel[0].Amount - item.Amount;
                    drSel[0].Amount = diff;
                    drSel[0].ForeignAmount = diff;


                    item.Action = 2; // Change Flight
                }
            }


            PosDS.T_PassengerFeeServiceChargeRow[] drPassServ_Old = (PosDS.T_PassengerFeeServiceChargeRow[])dsDB.T_PassengerFeeServiceCharge.Select("Action = -1");
            PosDS.T_PassengerFeeServiceChargeRow[] drPassServ_New = (PosDS.T_PassengerFeeServiceChargeRow[])dsDB.T_PassengerFeeServiceCharge.Select("Action = 1");
            foreach (PosDS.T_PassengerFeeServiceChargeRow item in drPassServ_Old)
            {
                drPassServ_New = (PosDS.T_PassengerFeeServiceChargeRow[])dsDB.T_PassengerFeeServiceCharge.Select("Action = 1 AND PassengerId =" + item.PassengerId + " AND ChargeCode ='" + item.ChargeCode + "' AND Amount=" + item.Amount);
                if (drPassServ_New.Length > 0)
                {
                    item.Action = 0;
                    drPassServ_New[0].Action = 0;
                }
            }


            return dsDB;
        }


        public PosDS SetDSRowStateByABBNull(PosDS ds, int Row_State)
        {
            foreach (PosDS.T_BookingRow item in ds.T_Booking.Select("ABBNo is null"))
                item.Row_State = Row_State;
            foreach (PosDS.T_PassengerRow item in ds.T_Passenger.Select("ABBNo is null"))
                item.Row_State = Row_State;
            foreach (PosDS.T_PassengerFeeRow item in ds.T_PassengerFee.Select("ABBNo is null"))
                item.Row_State = Row_State;
            foreach (PosDS.T_PassengerFeeServiceChargeRow item in ds.T_PassengerFeeServiceCharge.Select("ABBNo is null"))
                item.Row_State = Row_State;
            foreach (PosDS.T_SegmentRow item in ds.T_Segment.Select("ABBNo is null"))
                item.Row_State = Row_State;
            foreach (PosDS.T_PaxFareRow item in ds.T_PaxFare.Select("ABBNo is null"))
                item.Row_State = Row_State;
            foreach (PosDS.T_BookingServiceChargeRow item in ds.T_BookingServiceCharge.Select("ABBNo is null"))
                item.Row_State = Row_State;
            foreach (PosDS.T_PaymentRow item in ds.T_Payment.Select("ABBNo is null"))
                item.Row_State = Row_State;
            return ds;
        }

        public void InsertWaitingForManualGenABB(string PNR_No, string Msg, Guid guid, string UserCode)
        {
            PosDS ds = new PosDS();
            PosDS.T_WaitingForManualGenABBRow drWait = ds.T_WaitingForManualGenABB.NewT_WaitingForManualGenABBRow();
            drWait.TransactionId = guid;
            drWait.Msg = Msg;
            drWait.PNR_No = PNR_No;
            drWait.Create_By = UserCode;
            drWait.Create_Date = DateTime.Now;
            drWait.Update_By = UserCode;
            drWait.Update_Date = DateTime.Now;
            DBHelper.InsertWaitingForManualGenABB(drWait);
        }

        #endregion

        ///// 
        [WebMethod]
        public string GetAdminMessage()
        {
            string Message = string.Empty;
            List<M_SendMessage> lst = MasterDA.GetM_SendMessage();
            foreach (M_SendMessage item in lst)
            {
                Message += item.DescMessage;
                Message += " ";
            }
            return Message;
        }
    }
}
