using CreateInvoiceSystem.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem
{
    public class CreateABB
    {
        static DataTable dtAgency = new DataTable();
        static DataTable dtAgencyType = new DataTable();
        static DataTable dtPaymentType = new DataTable();
        static DataTable dtPosMachine = new DataTable();
        static DataTable dtFlightFee = new DataTable();
        static public string OnGenABB(string BookingNo, string UserCode, string POS_Code)
        {
            //DBHelper da = new DBHelper();
            try
            {
                DateTime CurrentDate = DateTime.Now;

                dtAgency = DBHelper.GetAgency();
                dtAgencyType = DBHelper.GetGetAgencyType();
                dtPaymentType = DBHelper.GetPaymentType();
                dtPosMachine = DBHelper.GetPosMachine();
                dtFlightFee = DBHelper.GetMasterDataByTableName("M_FlightFee");


                PosDS dsAll = DBHelper.GetBookingAllData(BookingNo);
                PosDS dsNew = GetDSNew(dsAll);
                CreateM_FlightFee(dsNew);

                int flag = 0;

                if (flag == 0)
                    flag = ValidateAgency(dsNew, UserCode);
                if (flag == 1)
                {
                    return "ไม่พบ AgentCode";
                }
                if (flag == 0)
                    flag = ValidatePaymentType(dsNew, dtPaymentType);
                if (flag == 2)
                {
                    return "ไม่พบ PaymentType";
                }
                else if (flag == 3)
                {
                    return "PaymentType ไม่สามารถออก Invoice ได้";
                }

                //if (flag == 0)
                //    flag = ValidatePaymentAmount(dsAll); // ยอดทั้งหมด
                //if (flag == 4)
                //{
                //    return "ยอดชำระเงินไม่เท่ากับยอดค่าใช้จ่ายทั้งหมด";
                //}

                if (dsNew.T_Payment.Count == 0)
                {
                    return "ไม่มียอดชำระใหม่";
                }

                int Pos_Id = 1;
                DataRow[] dr = (DataRow[])dtPosMachine.Select("POS_Code ='" + POS_Code + "'");
                if (dr.Length > 0)
                    Pos_Id = Convert.ToInt32(dr[0]["POS_Id"]);

                var pay_date = dsNew.T_Payment.AsEnumerable().Select(row => row.Field<DateTime>("ApprovalDate").Date).Distinct();

                decimal SumFareNew = dsNew.T_BookingServiceCharge.Where(it => it.ChargeType != "Discount" && it.ChargeType != "PromotionDiscount").Sum(s => s.Amount * s.QTY);
                decimal SumFareNewDis = dsNew.T_BookingServiceCharge.Where(it => it.ChargeType == "Discount" || it.ChargeType == "PromotionDiscount").Sum(s => s.Amount * s.QTY);

                decimal SumFeeNew = dsNew.T_PassengerFeeServiceCharge.Where(it => it.ChargeType != "Discount" && it.ChargeType != "PromotionDiscount").Sum(s => s.Amount);
                decimal SumFeeNewDis = dsNew.T_PassengerFeeServiceCharge.Where(it => it.ChargeType == "Discount" || it.ChargeType == "PromotionDiscount").Sum(s => s.Amount);


                decimal SumPaymentNew = dsNew.T_Payment.Where(it => it.Status == "Approved").Sum(s => s.PaymentAmount);

                decimal FareFee = (SumFareNew - SumFareNewDis) + (SumFeeNew - SumFeeNewDis);
                if (FareFee == SumPaymentNew)
                {
                    SetRowState4Update(dsAll);
                    DataService dv = new DataService();


                    PosDS dss = dv.CreateAbb(dsAll, UserCode);
                    if (dss.ResultMessage.Count > 0)
                        return dss.ResultMessage[0].Message;

                    bool success = dv.PosSave(dss);
                    if (dss.ResultMessage.Count > 0)
                        return dss.ResultMessage[0].Message;
                    else
                        return string.Empty;
                    // return ProcessBookingMorePay(dsAll);


                    #region Old
                    
                    
                    //string AgencyCode = string.Empty;
                    //string AgencyTypeCode = string.Empty;
                    //long Last_INV = 1;
                    //string ABB_No = string.Empty;
                    //decimal AdminFee_Amount =v 0;
                    //decimal AdminFee_DiscountAmount = 0;
                    //decimal FUEL_Amount = 0;
                    //decimal FUEL_DiscountAmount = 0;
                    //decimal SERVICE_Amount = 0;
                    //decimal SERVICE_DiscountAmount = 0;
                    //decimal VAT_Amount = 0;
                    //decimal VAT_DiscountAmount = 0;
                    //decimal AIRPORTTAX_Amount = 0;
                    //decimal AIRPORTTAX_DiscountAmount = 0;
                    //decimal INSURANCE_Amount = 0;
                    //decimal INSURANCE_DiscountAmount = 0;
                    //decimal REP_Amount = 0;
                    //decimal REP_DiscountAmount = 0;
                    //decimal FarePrice_Amount = 0;
                    //decimal FarePrice_DiscountAmount = 0;
                    //decimal Other_Amount = 0;
                    //decimal All_Discount = 0;

                    //int PaxADT_Count = dsAll.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "ADT");
                    //int PaxCHD_Count = dsAll.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "CHD");

                    //string PaymentCurrency = dsNew.T_Payment.Take(1).FirstOrDefault().CurrencyCode;
                    //DateTime BookingDate = dsAll.T_Booking[0].Booking_Date;
                    //DateTime PaymentDate = dsNew.T_Payment.Where(it => it.Status == "Approved").Max(it => it.ApprovalDate);

                    //#region AgencyCode


                    //foreach (PosDS.T_PaymentRow item in dsNew.T_Payment)
                    //{
                    //    DataRow[] drAgency = dtAgency.Select("Agency_Code='" + item.AgentCode + "'");
                    //    if (drAgency.Length > 0 && drAgency[0]["Agency_Code"].ToString() != string.Empty)
                    //    {
                    //        AgencyCode = drAgency[0]["Agency_Code"].ToString();
                    //        AgencyTypeCode = (drAgency[0]["Agency_Type_Code"] == null || drAgency[0]["Agency_Type_Code"].ToString() == string.Empty) ? "000" : drAgency[0]["Agency_Type_Code"].ToString();

                    //        //DataRow[] drAgencyType = dtAgencyType.Select("Agent_type_code = '" + AgencyTypeCode + "'");
                    //        DataTable dtmac = DBHelper.GetAgencyTypeByCode(AgencyTypeCode);
                    //        if (dtmac.Rows.Count > 0)
                    //        {
                    //            Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                    //            ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + AgencyTypeCode + Last_INV.ToString("00000000");
                    //            DBHelper.UpdateAgencyType(Convert.ToInt32(dtmac.Rows[0]["AgencyTypeId"]), Last_INV);
                    //        }
                    //        dtmac.Dispose();
                    //        continue;
                    //    }
                    //}

                    //if (AgencyCode == string.Empty)
                    //{
                    //    string PosCode = "000"; // Default ตาม Store
                    //    // DataRow[] drPosMachine = dtPosMachine.Select("POS_Code = '" + PosCode + "' and IsActive = 'Y'");
                    //    DataTable dtmac = DBHelper.GetPosMachineByCode(PosCode);
                    //    if (dtmac.Rows.Count > 0)
                    //    {
                    //        Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                    //        DBHelper.UpdatePOSMachine(PosCode, Last_INV);
                    //    }
                    //    ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + "001" + Last_INV.ToString("00000000");
                    //    dtmac.Dispose();
                    //}
                    //#endregion


                    //if (ABB_No != string.Empty)
                    //{
                    //    //int PaxADT_Count = PaxADT_Count_New;
                    //    //int PaxCHD_Count = PaxCHD_Count_New;

                    //    AdminFee_Amount = GetSumAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "ADMINFEE");
                    //    AdminFee_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "ADMINFEE");

                    //    FUEL_Amount = GetSumAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "FUEL");
                    //    FUEL_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "FUEL");

                    //    SERVICE_Amount = GetSumAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "SERVICE");
                    //    SERVICE_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "SERVICE");

                    //    VAT_Amount = GetSumAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "VAT");
                    //    VAT_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "VAT");

                    //    AIRPORTTAX_Amount = GetSumAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX");
                    //    AIRPORTTAX_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX");

                    //    INSURANCE_Amount = GetSumAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "INSURANCE");
                    //    INSURANCE_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "INSURANCE");

                    //    REP_Amount = GetSumAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "REP");
                    //    REP_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsNew, dsAll, PaxADT_Count, PaxCHD_Count, "REP");

                    //    FarePrice_Amount = GetSumAmountByFarePrice(dsNew, PaxADT_Count, PaxCHD_Count);
                    //    FarePrice_DiscountAmount = GetSumDiscountAmountByFarePrice(dsNew, PaxADT_Count, PaxCHD_Count);

                    //    Other_Amount = GetSumAmountByOther(dsNew, PaxADT_Count, PaxCHD_Count);


                    //    // คำนวน ExChangeRate
                    //    decimal ExChangeRate = 1;
                    //    if (PaymentCurrency != "THB")
                    //        ExChangeRate = DBHelper.GetCurrencyActiveDate(PaymentCurrency, BookingDate);

                    //    AdminFee_Amount = AdminFee_Amount - AdminFee_DiscountAmount;
                    //    FUEL_Amount = FUEL_Amount - FUEL_DiscountAmount;
                    //    SERVICE_Amount = SERVICE_Amount - SERVICE_DiscountAmount;
                    //    VAT_Amount = VAT_Amount - VAT_DiscountAmount;
                    //    AIRPORTTAX_Amount = AIRPORTTAX_Amount - AIRPORTTAX_DiscountAmount;
                    //    INSURANCE_Amount = INSURANCE_Amount - INSURANCE_DiscountAmount;
                    //    REP_Amount = REP_Amount - REP_DiscountAmount;
                    //    FarePrice_Amount = FarePrice_Amount - FarePrice_DiscountAmount;
                    //    //Other_Amount = Other_Amount ;
                    //    All_Discount = GetAllDiscount(dsNew);



                    //    da.DBOpenConnection();
                    //    da.BeginTran();
                    //    bool success = false;


                    //    #region Update T_Booking

                    //    PosDS.T_BookingRow drBooking = dsAll.T_Booking[0]; // Update Booking จาก Booking ล่าสุด ใช้ ds
                    //    Guid TransactionId = drBooking.TransactionId;
                    //    drBooking.TransactionId = TransactionId;
                    //    success = da.UpdateBooking(drBooking, ABB_No);

                    //    #endregion

                    //    #region Update T_Passenger

                    //    foreach (PosDS.T_PassengerRow item in dsNew.T_Passenger)
                    //    {
                    //        item.ABBNo = ABB_No;
                    //        success = da.UpdatePassengerAbbNo(item);
                    //    }

                    //    #endregion

                    //    #region Update T_Payment


                    //    foreach (PosDS.T_PaymentRow item in dsNew.T_Payment)
                    //    {
                    //        item.ABBNo = ABB_No;
                    //        success = da.UpdatePaymentAbbNo(item);
                    //    }
                    //    #endregion

                    //    #region Update T_BookingServiceCharge

                    //    foreach (PosDS.T_BookingServiceChargeRow item in dsNew.T_BookingServiceCharge)
                    //    {
                    //        item.ABBNo = ABB_No;
                    //        success = da.UpdateBookingServiceChargeAbbNo(item);
                    //    }

                    //    #endregion

                    //    #region Update Segment
                    //    foreach (PosDS.T_SegmentRow item in dsNew.T_Segment)
                    //    {
                    //        item.ABBNo = ABB_No;
                    //        success = da.UpdateSegmentAbbNo(item);
                    //    }
                    //    #endregion

                    //    #region Update T_PaxFare


                    //    foreach (PosDS.T_PaxFareRow item in dsNew.T_PaxFare)
                    //    {
                    //        item.ABBNo = ABB_No;
                    //        success = da.UpdatePaxFareAbbNo(item);
                    //    }

                    //    #endregion

                    //    #region Update PassengerFee
                    //    foreach (PosDS.T_PassengerFeeRow item in dsNew.T_PassengerFee)
                    //    {
                    //        item.ABBNo = ABB_No;
                    //        success = da.UpdatePassengerFeeAbbNo(item);
                    //    }
                    //    #endregion

                    //    #region Update T_PassengerFeeServiceCharge

                    //    foreach (PosDS.T_PassengerFeeServiceChargeRow item in dsNew.T_PassengerFeeServiceCharge)
                    //    {
                    //        item.ABBNo = ABB_No;
                    //        success = da.UpdatePassServiceChargeAbbNo(item);
                    //    }

                    //    #endregion

                    //    #region Insert T_ABB

                    //    PosDS.T_ABBRow drAbb = dsNew.T_ABB.NewT_ABBRow();
                    //    drAbb.ABBTid = Guid.NewGuid();
                    //    drAbb.TransactionId = drBooking.TransactionId;
                    //    drAbb.PNR_No = drBooking.PNR_No;
                    //    drAbb.TaxInvoiceNo = ABB_No;
                    //    drAbb.PAXCountADT = PaxADT_Count;
                    //    drAbb.PAXCountCHD = PaxCHD_Count;

                    //    drAbb.ADMINFEE = AdminFee_Amount * ExChangeRate;
                    //    drAbb.Fuel = FUEL_Amount * ExChangeRate;
                    //    drAbb.INSURANCE = INSURANCE_Amount * ExChangeRate;
                    //    drAbb.Service = SERVICE_Amount * ExChangeRate;
                    //    drAbb.OTHER = Other_Amount * ExChangeRate;
                    //    drAbb.VAT = VAT_Amount * ExChangeRate;
                    //    drAbb.AIRPORTTAX = AIRPORTTAX_Amount * ExChangeRate;
                    //    drAbb.REP = REP_Amount * ExChangeRate;
                    //    drAbb.Discount = All_Discount * ExChangeRate;

                    //    drAbb.ADMINFEE_Ori = AdminFee_Amount;
                    //    drAbb.Fuel_Ori = FUEL_Amount;
                    //    drAbb.INSURANCE_Ori = INSURANCE_Amount;
                    //    drAbb.Service_Ori = SERVICE_Amount;
                    //    drAbb.OTHER_Ori = Other_Amount;
                    //    drAbb.VAT_Ori = VAT_Amount;
                    //    drAbb.AIRPORTTAX_Ori = AIRPORTTAX_Amount;
                    //    drAbb.REP_Ori = REP_Amount;
                    //    drAbb.Discount_Ori = All_Discount;

                    //    drAbb.PrintCount = 0;
                    //    drAbb.STATUS_INV = "ACTIVE";
                    //    drAbb.Create_By = UserCode;
                    //    drAbb.Create_Date = drBooking.Create_Date;
                    //    drAbb.Update_By = UserCode;
                    //    drAbb.Update_Date = drBooking.Create_Date;
                    //    PosDS.T_PaymentRow[] drPayA = (PosDS.T_PaymentRow[])dsNew.T_Payment.Select("Status = 'Approved' ");
                    //    drAbb.TOTAL_Ori = drPayA.Sum(it => it.PaymentAmount);
                    //    drAbb.TOTAL = drAbb.TOTAL_Ori * ExChangeRate;
                    //    drAbb.ExchangeRateTH = ExChangeRate;
                    //    drAbb.POS_Id = Pos_Id;
                    //    dsNew.T_ABB.AddT_ABBRow(drAbb);
                    //    success = da.InsertABB(drAbb);
                    //    #endregion

                    //    #region Insert T_ABBPAX

                    //    for (int i = 0; i < dsAll.T_Passenger.Count; i++) // Save Pax ทั้งหมด
                    //    {
                    //        PosDS.T_ABBPAXRow drABBPAX = dsAll.T_ABBPAX.NewT_ABBPAXRow();
                    //        drABBPAX.ABBPAXId = Guid.NewGuid();
                    //        drABBPAX.ABBTid = drAbb.ABBTid;
                    //        drABBPAX.PNR_No = drBooking.PNR_No;
                    //        drABBPAX.SEQ = i;
                    //        drABBPAX.FirstName = dsAll.T_Passenger[i].FirstName.ToUpper();
                    //        drABBPAX.LastName = dsAll.T_Passenger[i].LastName.ToUpper();
                    //        drABBPAX.Create_By = UserCode;
                    //        drABBPAX.Create_Date = CurrentDate;
                    //        drABBPAX.Update_By = UserCode;
                    //        drABBPAX.Update_Date = CurrentDate;
                    //        success = da.InsertABBPAX(drABBPAX);
                    //    }
                    //    #endregion

                    //    #region Insert T_ABBPAYMENTMETHOD
                    //    PosDS.T_ABBPAYMENTMETHODRow drABB_pay;
                    //    var dtPay = from pay in dsNew.T_Payment.AsEnumerable()
                    //                join paytype in dtPaymentType.AsEnumerable() on pay.PaymentMethodCode equals paytype["PaymentType_Code"].ToString()
                    //                where paytype["IsActive"].ToString() == "Y"
                    //                select new
                    //                {
                    //                    PaymentMethodCode = pay.PaymentMethodCode,
                    //                    PaymentType_Name = paytype["PaymentType_Name"].ToString(),
                    //                    PaymentAmount = pay.PaymentAmount,
                    //                    CurrencyCode = pay.CurrencyCode,
                    //                    ApprovalDate = pay.ApprovalDate,
                    //                    AgentCode = pay.AgentCode
                    //                };



                    //    int seq = 0;
                    //    dtPay = dtPay.OrderBy(o => o.ApprovalDate);
                    //    foreach (var item in dtPay)
                    //    {
                    //        drABB_pay = dsNew.T_ABBPAYMENTMETHOD.NewT_ABBPAYMENTMETHODRow();
                    //        drABB_pay.ABBPAYId = Guid.NewGuid();
                    //        drABB_pay.ABBTid = drAbb.ABBTid;
                    //        drABB_pay.PNR_No = drBooking.PNR_No;
                    //        drABB_pay.SEQ = seq;
                    //        drABB_pay.CurrencyCode = item.CurrencyCode;
                    //        drABB_pay.PaymentMethodCode = item.PaymentMethodCode;
                    //        drABB_pay.PaymentType_Name = item.PaymentType_Name;
                    //        drABB_pay.PaymentAmount = item.PaymentAmount * ExChangeRate;
                    //        drABB_pay.PaymentAmount_Ori = item.PaymentAmount;
                    //        drABB_pay.ExchangeRateTH = ExChangeRate;
                    //        drABB_pay.ApprovalDate = item.ApprovalDate;
                    //        drABB_pay.AgentCode = item.AgentCode;
                    //        drABB_pay.Create_By = UserCode;
                    //        drABB_pay.Create_Date = CurrentDate;
                    //        drABB_pay.Update_By = UserCode;
                    //        drABB_pay.Update_Date = CurrentDate;
                    //        dsNew.T_ABBPAYMENTMETHOD.AddT_ABBPAYMENTMETHODRow(drABB_pay);
                    //        success = da.InsertABBPAYMENTMETHOD(drABB_pay);
                    //        seq++;
                    //    }

                    //    #endregion

                    //    #region Insert ABBFARE
                    //    PosDS.T_ABBFARERow drAbbFare = dsNew.T_ABBFARE.NewT_ABBFARERow();
                    //    var dtSeg = from seg in dsNew.T_Segment.AsEnumerable()
                    //                join pax in dsNew.T_PaxFare.AsEnumerable() on seg.SegmentTId equals pax.SegmentTId
                    //                join service in dsNew.T_BookingServiceCharge.AsEnumerable() on pax.PaxFareTId equals service.PaxFareTId
                    //                where service.ChargeType == "FarePrice"
                    //                select new
                    //                {
                    //                    CarrierCode = seg.CarrierCode,
                    //                    FlightNumber = seg.FlightNumber,
                    //                    DepartureStation = seg.DepartureStation,
                    //                    ArrivalStation = seg.ArrivalStation,
                    //                    STD = seg.STD,
                    //                    STA = seg.STA,
                    //                    Amount = service.Amount,
                    //                    CurrencyCode = service.CurrencyCode
                    //                };

                    //    var dtGseg = dtSeg.GroupBy(it => new { it.CarrierCode, it.FlightNumber, it.DepartureStation, it.ArrivalStation, it.STA, it.STD, it.CurrencyCode })
                    //         .Select(s => new
                    //         {
                    //             s.Key.CarrierCode,
                    //             s.Key.FlightNumber,
                    //             s.Key.DepartureStation,
                    //             s.Key.ArrivalStation,
                    //             s.Key.STA,
                    //             s.Key.STD,
                    //             s.Key.CurrencyCode,
                    //             AMT = s.Sum(ss => ss.Amount)

                    //         });
                    //    seq = 0;
                    //    foreach (var item in dtGseg)
                    //    {
                    //        drAbbFare = dsNew.T_ABBFARE.NewT_ABBFARERow();
                    //        drAbbFare.ABBFAREId = Guid.NewGuid();
                    //        drAbbFare.ABBTid = drAbb.ABBTid;
                    //        drAbbFare.SEQ = seq++;
                    //        drAbbFare.PNR_No = drBooking.PNR_No;
                    //        drAbbFare.CarrierCode = item.CarrierCode;
                    //        drAbbFare.FlightNumber = item.FlightNumber;
                    //        drAbbFare.DepartureStation = item.DepartureStation;
                    //        drAbbFare.ArrivalStation = item.ArrivalStation;
                    //        drAbbFare.STA = item.STA;
                    //        drAbbFare.STD = item.STD;
                    //        drAbbFare.CurrencyCode = item.CurrencyCode;
                    //        drAbbFare.Amount_Ori = item.AMT * (PaxADT_Count + PaxCHD_Count);
                    //        drAbbFare.Amount = (item.AMT * ExChangeRate) * (PaxADT_Count + PaxCHD_Count); 
                    //        drAbbFare.Create_By = UserCode;
                    //        drAbbFare.Create_Date = CurrentDate;
                    //        drAbbFare.Update_By = UserCode;
                    //        drAbbFare.Update_Date = CurrentDate;
                    //        dsNew.T_ABBFARE.AddT_ABBFARERow(drAbbFare);
                    //        success = da.InsertABBFARE(drAbbFare);
                    //    }



                    //    #endregion

                    //    #region Delete WaitingForManualGenABB
                    //    success = da.DeleteWaitingForManualGenABB_ByPNR_No(BookingNo);
                    //    #endregion

                    //    #region Insert LogsGenABB
                    //    PosDS.T_LogsGenABBRow drLogGen = dsNew.T_LogsGenABB.NewT_LogsGenABBRow();
                    //    drLogGen.LogId = Guid.NewGuid();
                    //    drLogGen.TransactionId = TransactionId;
                    //    drLogGen.PNR_No = BookingNo;
                    //    drLogGen.Msg = "Done";
                    //    drLogGen.Create_By = UserCode;
                    //    drLogGen.Create_Date = CurrentDate;
                    //    drLogGen.Update_By = UserCode;
                    //    drLogGen.Update_Date = CurrentDate;
                    //    dsNew.T_LogsGenABB.AddT_LogsGenABBRow(drLogGen);
                    //    success = da.InsertLogsGenABB(drLogGen);

                    //    #endregion

                    //    da.EndTran(true);
                    //    da.DBCloseConnection();

                    //}
                    #endregion

                }
                else if (SumPaymentNew < 0)
                {
                    // สำหรับ Payment ติดลบ เคส Devide
                    ProcessBookingPaymentOnly(dsAll, dsNew, "", "");
                }
                else
                {
                    return "ยอดชำระเงินไม่เท่ากับยอดค่าใช้จ่ายทั้งหมด";
                }

            }
            catch (Exception ex)
            {
                //da.EndTran(false);
                //da.DBCloseConnection();
                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserCode);
                return "Create ABB ไม่สำเร็จ(Exception)";
            }
            return string.Empty;
        }

        private string ProcessBookingOneAbb(PosDS ds)
        {
            return "";
        }


       static  private string ProcessBookingMorePay(PosDS ds)
        {
            DBHelper da = new DBHelper();
            string PNR_No = string.Empty;
            string UserCode = "";
            try
            {

                bool success = false;
                DateTime CurrentDate = ds.T_Booking[0].Create_Date;

                int Pos_Id = 1;
                DataRow[] dr = (DataRow[])dtPosMachine.Select("POS_Code ='" + UserCode + "'");
                if (dr.Length > 0)
                    Pos_Id = Convert.ToInt32(dr[0]["POS_Id"]);
                //CreateM_FlightFee(ds);
                // Get ABB Value
                PNR_No = ds.T_Booking[0].PNR_No;
                PosDS dsAll = DBHelper.GetBookingAllData(PNR_No);
                PosDS dsNew = GetDSNew(dsAll);
                Guid TransactionId = ds.T_Booking[0].TransactionId;
                int PaxADT_Count = ds.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "ADT");
                int PaxCHD_Count = ds.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "CHD");
                DateTime PaymentDate = ((PosDS.T_PaymentRow[])ds.T_Payment.Select("Status = 'Approved' AND ABBNo is null")).Max(it => it.ApprovalDate);
                DateTime BookingDate = ds.T_Booking[0].Booking_Date;
                string PaymentCurrency = ds.T_Payment.Take(1).FirstOrDefault().CurrencyCode;
                string CollectedCurrencyCode = ds.T_Payment.Take(1).FirstOrDefault().CollectedCurrencyCode;

                da.DBOpenConnection();
                da.BeginTran();

                string ABB_No = string.Empty;

                PosDS.T_BookingRow drBooking = ds.T_Booking[0];
                var pay_date = ds.T_Payment.Select("ABBNo is null").AsEnumerable().Select(row => row.Field<DateTime>("ApprovalDate").Date).Distinct();
                string First_Abb = "";
                int ii = 0;
                foreach (var drP in pay_date)
                {
                    DateTime DateWhere = drP.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    PosDS.T_PaymentRow drPayment;
                    string w = " ABBNo is null AND  IsFlag = 0 AND ApprovalDate <= #" + Convert.ToString(DateWhere) + "#";
                    PosDS.T_PaymentRow[] drArrPay = (PosDS.T_PaymentRow[])ds.T_Payment.Select("Status = 'Approved' AND " + w);
                    if (drArrPay.Length > 0)
                    {

                        drPayment = drArrPay[0];
                    }
                    else
                        continue;

                    #region ประกาศตัวแปร

                    string AgencyCode = string.Empty;
                    string AgencyTypeCode = string.Empty;
                    long Last_INV = 1;
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

                    #region GEN ABB No


                    if (drPayment.PaymentMethodCode == "AG")
                    {
                        DataRow[] drAgency = dtAgency.Select("Agency_Code='" + drPayment.AgentCode + "'");
                        if (drAgency.Length > 0 && drAgency[0]["Agency_Code"].ToString() != string.Empty)
                        {
                            AgencyCode = drAgency[0]["Agency_Code"].ToString();
                            AgencyTypeCode = (drAgency[0]["Agency_Type_Code"] == null || drAgency[0]["Agency_Type_Code"].ToString() == string.Empty) ? "000" : drAgency[0]["Agency_Type_Code"].ToString();

                            //DataRow[] drAgencyType = dtAgencyType.Select("Agent_type_code = '" + AgencyTypeCode + "'");
                            DataTable dtmac = DBHelper.GetAgencyTypeByCode(AgencyTypeCode);
                            if (dtmac.Rows.Count > 0)
                            {
                                Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                                ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + AgencyTypeCode + Last_INV.ToString("00000000");
                                DBHelper.UpdateAgencyType(Convert.ToInt32(dtmac.Rows[0]["AgencyTypeId"]), Last_INV);
                            }
                            dtmac.Dispose();

                        }
                    }

                    if (AgencyCode == string.Empty)
                    {
                        string PosCode = "000"; // Default ตาม Store
                        // DataRow[] drPosMachine = dtPosMachine.Select("POS_Code = '" + PosCode + "' and IsActive = 'Y'");
                        DataTable dtmac = DBHelper.GetPosMachineByCode(PosCode);
                        if (dtmac.Rows.Count > 0)
                        {
                            Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                            DBHelper.UpdatePOSMachine(PosCode, Last_INV);
                        }
                        ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + "001" + Last_INV.ToString("00000000");
                        dtmac.Dispose();
                    }
                    #endregion

                    if (ii == 0)
                    {
                        ii++;
                        First_Abb = ABB_No;
                    }

                    foreach (var item in drArrPay)
                    {
                        item.IsFlag = true;
                        item.ABBNo = ABB_No;
                    }

                    PosDS dsTemp = new PosDS();
                    PosDS.T_PassengerFeeServiceChargeRow[] drTempPassServ = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select(" ABBNo is null AND IsFlag = 0 AND   FeeCreateDate <= #" + Convert.ToString(DateWhere) + "#");
                    PosDS.T_BookingServiceChargeRow[] drTempBookServ = (PosDS.T_BookingServiceChargeRow[])ds.T_BookingServiceCharge.Select(" ABBNo is null AND IsFlag = 0 AND   FareCreateDate <= #" + Convert.ToString(DateWhere) + "#");

                    foreach (PosDS.T_PassengerFeeServiceChargeRow item in drTempPassServ)
                    {
                        item.ABBNo = ABB_No;
                        item.IsFlag = true;
                        dsTemp.T_PassengerFeeServiceCharge.ImportRow(item);
                    }
                    foreach (PosDS.T_BookingServiceChargeRow item in drTempBookServ)
                    {
                        item.ABBNo = ABB_No;
                        item.IsFlag = true;
                        dsTemp.T_BookingServiceCharge.ImportRow(item);
                    }

                    foreach (PosDS.T_PaxFareRow item in ds.T_PaxFare)
                    {
                        dsTemp.T_PaxFare.ImportRow(item);
                    }


                    if (ABB_No != string.Empty)
                    {

                        AdminFee_Amount = GetSumAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "ADMINFEE");
                        AdminFee_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "ADMINFEE");

                        FUEL_Amount = GetSumAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "FUEL");
                        FUEL_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "FUEL");

                        SERVICE_Amount = GetSumAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "SERVICE");
                        SERVICE_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "SERVICE");

                        VAT_Amount = GetSumAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "VAT");
                        VAT_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "VAT");

                        AIRPORTTAX_Amount = GetSumAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX");
                        AIRPORTTAX_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX");

                        INSURANCE_Amount = GetSumAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "INSURANCE");
                        INSURANCE_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "INSURANCE");

                        REP_Amount = GetSumAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "REP");
                        REP_DiscountAmount = GetSumDuscountAmountByAbbGroup(dsTemp, dsAll, PaxADT_Count, PaxCHD_Count, "REP");

                        FarePrice_Amount = GetSumAmountByFarePrice(dsTemp, PaxADT_Count, PaxCHD_Count);
                        FarePrice_DiscountAmount = GetSumDiscountAmountByFarePrice(dsTemp, PaxADT_Count, PaxCHD_Count);

                        Other_Amount = GetSumAmountByOther(dsTemp, PaxADT_Count, PaxCHD_Count);


                        // คำนวน ExChangeRate
                        decimal ExChangeRate = 1;
                        if (PaymentCurrency != "THB")
                            ExChangeRate = DBHelper.GetCurrencyActiveDate(PaymentCurrency, BookingDate);

                        AdminFee_Amount = (AdminFee_Amount - AdminFee_DiscountAmount);
                        FUEL_Amount = (FUEL_Amount - FUEL_DiscountAmount);
                        SERVICE_Amount = (SERVICE_Amount - SERVICE_DiscountAmount);
                        VAT_Amount = (VAT_Amount - VAT_DiscountAmount);
                        AIRPORTTAX_Amount = (AIRPORTTAX_Amount - AIRPORTTAX_DiscountAmount);
                        INSURANCE_Amount = (INSURANCE_Amount - INSURANCE_DiscountAmount);
                        REP_Amount = (REP_Amount - REP_DiscountAmount);
                        FarePrice_Amount = (FarePrice_Amount - FarePrice_DiscountAmount);
                        //Other_Amount = Other_Amount ;
                        All_Discount = GetAllDiscount(dsTemp);


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
                        drAbb.TOTAL_Ori = drArrPay.Where(it => it.Status == "Approved").Sum(it => it.PaymentAmount);
                        drAbb.TOTAL = drAbb.TOTAL_Ori * ExChangeRate;
                        drAbb.ExchangeRateTH = ExChangeRate;
                        drAbb.POS_Id = Pos_Id;
                        ds.T_ABB.AddT_ABBRow(drAbb);
                        success = da.InsertABB(drAbb);
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
                            success = da.InsertABBPAX(drABBPAX);
                        }
                        #endregion

                        #region Insert T_ABBPAYMENTMETHOD
                        PosDS.T_ABBPAYMENTMETHODRow drABB_pay;
                        var dtPay = from pay in drArrPay
                                    join paytype in dtPaymentType.AsEnumerable() on pay.PaymentMethodCode equals paytype["PaymentType_Code"].ToString()
                                    where paytype["IsActive"].ToString() == "Y"
                                    select new
                                    {
                                        PaymentMethodCode = pay.PaymentMethodCode,
                                        PaymentType_Name = paytype["PaymentType_Name"].ToString(),
                                        PaymentAmount = pay.PaymentAmount,
                                        CurrencyCode = pay.CurrencyCode,
                                        ApprovalDate = pay.ApprovalDate,
                                        AgentCode= pay.AgentCode
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
                            drABB_pay.AgentCode = AgencyCode;
                            drABB_pay.ApprovalDate = item.ApprovalDate;
                            drABB_pay.ExchangeRateTH = ExChangeRate;
                            drABB_pay.Create_By = UserCode;
                            drABB_pay.Create_Date = CurrentDate;
                            drABB_pay.Update_By = UserCode;
                            drABB_pay.Update_Date = CurrentDate;
                            ds.T_ABBPAYMENTMETHOD.AddT_ABBPAYMENTMETHODRow(drABB_pay);
                            success = da.InsertABBPAYMENTMETHOD(drABB_pay);
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

                        var dtGseg = dtSeg.GroupBy(it => new { it.CarrierCode, it.FlightNumber, it.DepartureStation, it.ArrivalStation, it.STA, it.STD, it.CurrencyCode,it.Amount })
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
                            if (FarePrice_Amount == 0)
                            {
                                drAbbFare.Amount_Ori = 0;
                                drAbbFare.Amount = 0;
                            }
                            else
                            {
                                drAbbFare.Amount_Ori = item.AMT * (PaxADT_Count + PaxCHD_Count);
                                drAbbFare.Amount = (drAbbFare.Amount_Ori * ExChangeRate);
                            }
                            drAbbFare.ExchangeRateTH = ExChangeRate;
                            drAbbFare.Create_By = UserCode;
                            drAbbFare.Create_Date = CurrentDate;
                            drAbbFare.Update_By = UserCode;
                            drAbbFare.Update_Date = CurrentDate;
                            ds.T_ABBFARE.AddT_ABBFARERow(drAbbFare);
                            success = da.InsertABBFARE(drAbbFare);
                        }



                        #endregion

                    


                        decimal charge_total = AdminFee_Amount + FUEL_Amount + SERVICE_Amount + VAT_Amount + AIRPORTTAX_Amount + INSURANCE_Amount + Other_Amount + FarePrice_Amount;
                        if (drAbb.TOTAL_Ori != charge_total)
                        {
                            da.EndTran(false);
                            da.DBCloseConnection();
                            return "ข้อมูลอาจมีการเปลี่ยน Flight หรือซื้อ เพิ่ม ยอดชำระ ไม่เท่ากับ ค่าใช้จ่าย แต่ละ ABB";
                        }
                    }
                    else
                    {
                        da.EndTran(false);
                        da.DBCloseConnection();
                        return "สร้างเลข ABB ไม่สำเร็จ";
                    }
                }

                ABB_No = First_Abb;
                #region Update T_Booking

                success = da.UpdateBooking(drBooking, ABB_No);

                #endregion

                #region Update T_Passenger

                foreach (PosDS.T_PassengerRow item in ds.T_Passenger)
                {
                    item.ABBNo = ABB_No;
                    success = da.UpdatePassengerAbbNo(item);
                }

                #endregion

                #region Update T_Payment


                foreach (PosDS.T_PaymentRow item in ds.T_Payment.Select("Status = 'Approved'"))
                {
                    success = da.UpdatePaymentAbbNo(item);
                }
                #endregion

                #region Update T_BookingServiceCharge

                foreach (PosDS.T_BookingServiceChargeRow item in ds.T_BookingServiceCharge)
                {
                    success = da.UpdateBookingServiceChargeAbbNo(item);
                }

                #endregion

                #region Update Segment
                foreach (PosDS.T_SegmentRow item in ds.T_Segment)
                {
                    item.ABBNo = ABB_No;
                    success = da.UpdateSegmentAbbNo(item);
                }
                #endregion

                #region Update T_PaxFare


                foreach (PosDS.T_PaxFareRow item in dsNew.T_PaxFare)
                {
                    item.ABBNo = ABB_No;
                    success = da.UpdatePaxFareAbbNo(item);
                }

                #endregion

                #region Update PassengerFee
                foreach (PosDS.T_PassengerFeeRow item in ds.T_PassengerFee)
                {
                    item.ABBNo = ABB_No;
                    success = da.UpdatePassengerFeeAbbNo(item);
                }
                #endregion

                #region Update T_PassengerFeeServiceCharge

                foreach (PosDS.T_PassengerFeeServiceChargeRow item in ds.T_PassengerFeeServiceCharge)
                {
                    success = da.UpdatePassServiceChargeAbbNo(item);
                }

                #endregion


                #region Insert LogsGenABB
                PosDS.T_LogsGenABBRow drLogGen = ds.T_LogsGenABB.NewT_LogsGenABBRow();
                drLogGen.LogId = Guid.NewGuid();
                drLogGen.TransactionId = TransactionId;
                drLogGen.PNR_No = PNR_No;
                drLogGen.Msg = "Done";
                drLogGen.Create_By = UserCode;
                drLogGen.Create_Date = CurrentDate;
                drLogGen.Update_By = UserCode;
                drLogGen.Update_Date = CurrentDate;
                ds.T_LogsGenABB.AddT_LogsGenABBRow(drLogGen);
                success = da.InsertLogsGenABB(drLogGen);

                #endregion

                da.EndTran(true);
                da.DBCloseConnection();
                return string.Empty;
            }
            catch (Exception ex)
            {
                da.EndTran(false);
                da.DBCloseConnection();
                LogException.Save(ex, PNR_No, UserCode);
                return string.Empty;
            }

            return "";
        }

        static private string ProcessBookingPaymentOnly(PosDS dsAll, PosDS dsNew, string UserCode, string POS_Code)
        {
            DBHelper da = new DBHelper();
            DateTime CurrentDate = DateTime.Now;
            try
            {
                PosDS.T_BookingRow drBooking = dsAll.T_Booking[0];
                string PaymentCurrency = dsNew.T_Payment.Take(1).FirstOrDefault().CurrencyCode;
                DateTime BookingDate = dsAll.T_Booking[0].Booking_Date;
                int PaxADT_Count = dsAll.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "ADT" && it.Action == 0);
                int PaxCHD_Count = dsAll.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "CHD" && it.Action == 0);
                string ABB_No = GenarateABB_NO(dsAll);
                bool success = false;
                int Pos_Id = 1;
                DataRow[] dr = (DataRow[])dtPosMachine.Select("POS_Code ='" + POS_Code + "'");
                if (dr.Length > 0)
                    Pos_Id = Convert.ToInt32(dr[0]["POS_Id"]);
                // คำนวน ExChangeRate
                decimal ExChangeRate = 1;
                if (PaymentCurrency != "THB")
                    ExChangeRate = DBHelper.GetCurrencyActiveDate(PaymentCurrency, BookingDate);
                da.DBOpenConnection();
                da.BeginTran();

                #region Update T_Payment


                foreach (PosDS.T_PaymentRow item in dsNew.T_Payment)
                {
                    item.ABBNo = ABB_No;
                    success = da.UpdatePaymentAbbNo(item);
                }
                #endregion

                #region Insert T_ABB

                PosDS.T_ABBRow drAbb = dsNew.T_ABB.NewT_ABBRow();
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
                drAbb.Create_Date = CurrentDate;
                drAbb.Update_By = UserCode;
                drAbb.Update_Date = CurrentDate;

                drAbb.TOTAL_Ori = 0;
                drAbb.TOTAL = 0;
                drAbb.ExchangeRateTH = ExChangeRate;
                drAbb.POS_Id = Pos_Id;

                success = da.InsertABB(drAbb);
                #endregion

                #region Insert T_ABBPAX

                for (int i = 0; i < dsAll.T_Passenger.Count; i++) // Save Pax ทั้งหมด
                {
                    PosDS.T_ABBPAXRow drABBPAX = dsAll.T_ABBPAX.NewT_ABBPAXRow();
                    drABBPAX.ABBPAXId = Guid.NewGuid();
                    drABBPAX.ABBTid = drAbb.ABBTid;
                    drABBPAX.PNR_No = drBooking.PNR_No;
                    drABBPAX.SEQ = i;
                    drABBPAX.FirstName = dsAll.T_Passenger[i].FirstName.ToUpper();
                    drABBPAX.LastName = dsAll.T_Passenger[i].LastName.ToUpper();
                    drABBPAX.Create_By = UserCode;
                    drABBPAX.Create_Date = CurrentDate;
                    drABBPAX.Update_By = UserCode;
                    drABBPAX.Update_Date = CurrentDate;
                    success = da.InsertABBPAX(drABBPAX);
                }
                #endregion

                #region Insert T_ABBPAYMENTMETHOD
                PosDS.T_ABBPAYMENTMETHODRow drABB_pay;
                var dtPay = from pay in dsNew.T_Payment.AsEnumerable()
                            join paytype in dtPaymentType.AsEnumerable() on pay.PaymentMethodCode equals paytype["PaymentType_Code"].ToString()
                            where paytype["IsActive"].ToString() == "Y"
                            select new
                            {
                                PaymentMethodCode = pay.PaymentMethodCode,
                                PaymentType_Name = paytype["PaymentType_Name"].ToString(),
                                PaymentAmount = pay.PaymentAmount,
                                CurrencyCode = pay.CurrencyCode,
                                ApprovalDate = pay.ApprovalDate
                            };



                int seq = 0;
                dtPay = dtPay.OrderBy(o => o.ApprovalDate);
                foreach (var item in dtPay)
                {
                    drABB_pay = dsNew.T_ABBPAYMENTMETHOD.NewT_ABBPAYMENTMETHODRow();
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

                    drABB_pay.Create_By = UserCode;
                    drABB_pay.Create_Date = CurrentDate;
                    drABB_pay.Update_By = UserCode;
                    drABB_pay.Update_Date = CurrentDate;
                    
                    success = da.InsertABBPAYMENTMETHOD(drABB_pay);
                    seq++;
                }

                #endregion

                #region Insert ABBFARE
                PosDS.T_ABBFARERow drAbbFare = dsNew.T_ABBFARE.NewT_ABBFARERow();
                var dtSeg = from seg in dsNew.T_Segment.AsEnumerable()
                            join pax in dsNew.T_PaxFare.AsEnumerable() on seg.SegmentTId equals pax.SegmentTId
                            join service in dsNew.T_BookingServiceCharge.AsEnumerable() on pax.PaxFareTId equals service.PaxFareTId
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
                foreach (var item in dtGseg)
                {
                    drAbbFare = dsNew.T_ABBFARE.NewT_ABBFARERow();
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
                    dsNew.T_ABBFARE.AddT_ABBFARERow(drAbbFare);
                    success = da.InsertABBFARE(drAbbFare);
                }



                #endregion

                da.EndTran(true);
                da.DBCloseConnection();
                return string.Empty;
            }
            catch (Exception ex)
            {
                da.EndTran(false);
                da.DBCloseConnection();
                return ex.Message;
            }

        }

        #region Method For ProcessBooking


        static private PosDS CreateServiceCharge(PosDS ds)
        {
            try
            {


                PosDS.T_BookingServiceChargeDataTable dt = (PosDS.T_BookingServiceChargeDataTable)ds.T_BookingServiceCharge.Clone();

                PosDS.T_PassengerRow[] drPass = (PosDS.T_PassengerRow[])ds.T_Passenger.Select("PaxType = 'ADT'");
                foreach (PosDS.T_PassengerRow pass in drPass)
                {
                    foreach (PosDS.T_BookingServiceChargeRow service in ds.T_BookingServiceCharge)
                    {
                        service.BookingServiceChargeTId = Guid.NewGuid();
                        service.PassengerId = pass.PassengerId;
                        dt.ImportRow(service);
                    }
                }

                drPass = (PosDS.T_PassengerRow[])ds.T_Passenger.Select("PaxType = 'CHD'");

                foreach (PosDS.T_PassengerRow pass in drPass)
                {
                    foreach (PosDS.T_BookingServiceChargeRow service in ds.T_BookingServiceCharge)
                    {
                        service.BookingServiceChargeTId = Guid.NewGuid();
                        service.PassengerId = pass.PassengerId;
                        dt.ImportRow(service);
                    }
                }
                ds.T_BookingServiceCharge.Clear();
                ds.T_BookingServiceCharge.Merge(dt);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        static private decimal GetSumAmountByAbbGroup(PosDS ds, PosDS dsAll, int PaxADT_Count, int PaxCHD_Count, string AbbGroup)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                              join pax_fare in dsAll.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                              table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "ADT" 
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax ADT
                results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          join pax_fare in dsAll.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "CHD" 
                          select new
                          {
                              Amount = table1.Amount * PaxCHD_Count,
                          };
                decimal sum2 = results.Sum(x => x.Amount);


                // Fee
                results = from table1 in ds.T_PassengerFeeServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup 
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
        static private decimal GetSumAmountByAbbGroup(PosDS ds, PosDS dsAll, string AbbGroup)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                              join pax_fare in dsAll.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                              table2["AbbGroup"].ToString() == AbbGroup 
                              select new
                              {
                                  Amount = table1.Amount
                              };
                decimal sum1 = results.Sum(x => x.Amount);


                // Fee
                results = from table1 in ds.T_PassengerFeeServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          where table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount" && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup 
                          select new
                          {
                              Amount = table1.Amount,
                          };
                decimal sum2 = results.Sum(x => x.Amount);

                // ds.T_BookingServiceCharge.Where(it => it.ChargeType != "Discount" && it.ChargeType != "PromotionDiscount");

                SumAmount = sum1 + sum2;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }

        static private decimal GetSumDuscountAmountByAbbGroup(PosDS ds, PosDS dsAll, int PaxADT_Count, int PaxCHD_Count, string AbbGroup)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                              join pax_fare in dsAll.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                              table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "ADT" 
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          join pax_fare in dsAll.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup && pax_fare.PaxType == "CHD" 
                          select new
                          {
                              Amount = table1.Amount * PaxCHD_Count,
                          };
                decimal sum2 = results.Sum(x => x.Amount);


                // Fee
                results = from table1 in ds.T_PassengerFeeServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup 
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
        static private decimal GetSumDuscountAmountByAbbGroup(PosDS ds, PosDS dsAll, string AbbGroup)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                              join pax_fare in dsAll.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                              table2["AbbGroup"].ToString() == AbbGroup 
                              select new
                              {
                                  Amount = table1.Amount
                              };
                decimal sum1 = results.Sum(x => x.Amount);


                // Fee
                results = from table1 in ds.T_PassengerFeeServiceCharge.AsEnumerable()
                          join table2 in dtFlightFee.AsEnumerable() on table1.ChargeCode equals table2["Flight_Fee_Code"].ToString()
                          where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount") && table2["IsActive"].ToString() == "Y" &&
                          table2["AbbGroup"].ToString() == AbbGroup 
                          select new
                          {
                              Amount = table1.Amount,
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

        static private decimal GetSumAmountByFarePrice(PosDS ds, int PaxADT_Count, int PaxCHD_Count)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType == "FarePrice" && pax_fare.PaxType == "ADT" 
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType == "FarePrice" && pax_fare.PaxType == "CHD"
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

        static private decimal GetSumAmountByFarePrice(PosDS ds)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType == "FarePrice"
                              select new
                              {
                                  Amount = table1.Amount
                              };
                decimal sum1 = results.Sum(x => x.Amount);
                SumAmount = sum1;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }


        static private decimal GetSumDiscountAmountByFarePrice(PosDS ds, int PaxADT_Count, int PaxCHD_Count)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType == "" && pax_fare.PaxType == "ADT" && (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount")
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType == "" && pax_fare.PaxType == "CHD" && (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount")
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

        static private decimal GetSumDiscountAmountByFarePrice(PosDS ds)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType == "" && (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount")
                              select new
                              {
                                  Amount = table1.Amount
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                SumAmount = sum1;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }


        static private decimal GetSumAmountByOther(PosDS ds, int PaxADT_Count, int PaxCHD_Count)
        {
            decimal SumAmount = 0;

            try
            {
                // Pax ADT
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType != "FarePrice" && table1.ChargeCode == "" && pax_fare.PaxType == "ADT" && table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount"
                              select new
                              {
                                  Amount = table1.Amount * PaxADT_Count,
                              };
                decimal sum1 = results.Sum(x => x.Amount);

                // Pax CHD
                results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                          join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                          where table1.ChargeType != "FarePrice" && table1.ChargeCode == "" && pax_fare.PaxType == "CHD" && table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount"
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

        static private decimal GetSumAmountByOther(PosDS ds)
        {
            decimal SumAmount = 0;

            try
            {

                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              join pax_fare in ds.T_PaxFare.AsEnumerable() on table1.PaxFareTId equals pax_fare.PaxFareTId
                              where table1.ChargeType != "FarePrice" && table1.ChargeCode == "" && table1.ChargeType != "Discount" && table1.ChargeType != "PromotionDiscount"
                              select new
                              {
                                  Amount = table1.Amount,
                              };
                decimal sum1 = results.Sum(x => x.Amount);



                SumAmount = sum1;
            }
            catch (Exception)
            {

                throw;
            }
            return SumAmount;
        }


        static private decimal GetAllDiscount(PosDS ds)
        {
            decimal SumAmount = 0;

            try
            {
                // FEE
                var results = from table1 in ds.T_BookingServiceCharge.AsEnumerable()
                              where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount")
                              select new
                              {
                                  Amount = table1.Amount
                              };
                SumAmount = results.Sum(x => x.Amount);

                results = from table1 in ds.T_PassengerFeeServiceCharge.AsEnumerable()
                          where (table1.ChargeType == "Discount" || table1.ChargeType == "PromotionDiscount")
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

        static private string GenarateABB_NO(PosDS ds)
        {
            string AgencyCode = string.Empty;
            string AgencyTypeCode = string.Empty;
            long Last_INV = 1;
            string ABB_No = string.Empty;
            DateTime PaymentDate = ds.T_Payment.Where(it => it.Status == "Approved").Max(it => it.ApprovalDate);

            #region AgencyCode


            foreach (PosDS.T_PaymentRow item in ds.T_Payment)
            {
                DataRow[] drAgency = dtAgency.Select("Agency_Code='" + item.AgentCode + "'");
                if (drAgency.Length > 0 && drAgency[0]["Agency_Code"].ToString() != string.Empty)
                {
                    AgencyCode = drAgency[0]["Agency_Code"].ToString();
                    AgencyTypeCode = (drAgency[0]["Agency_Type_Code"] == null || drAgency[0]["Agency_Type_Code"].ToString() == string.Empty) ? "000" : drAgency[0]["Agency_Type_Code"].ToString();

                    //DataRow[] drAgencyType = dtAgencyType.Select("Agent_type_code = '" + AgencyTypeCode + "'");
                    DataTable dtmac = DBHelper.GetAgencyTypeByCode(AgencyTypeCode);
                    if (dtmac.Rows.Count > 0)
                    {
                        Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                        ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + AgencyTypeCode + Last_INV.ToString("00000000");
                        DBHelper.UpdateAgencyType(Convert.ToInt32(dtmac.Rows[0]["AgencyTypeId"]), Last_INV);
                    }
                    dtmac.Dispose();
                    continue;
                }
            }

            if (AgencyCode == string.Empty)
            {
                string PosCode = "000"; // Default ตาม Store
                // DataRow[] drPosMachine = dtPosMachine.Select("POS_Code = '" + PosCode + "' and IsActive = 'Y'");
                DataTable dtmac = DBHelper.GetPosMachineByCode(PosCode);
                if (dtmac.Rows.Count > 0)
                {
                    Last_INV = (dtmac.Rows[0]["Last_Inv_No"] == null || dtmac.Rows[0]["Last_Inv_No"].ToString() == string.Empty) ? 1 : (Convert.ToInt64(dtmac.Rows[0]["Last_Inv_No"]) + 1);
                    DBHelper.UpdatePOSMachine(PosCode, Last_INV);
                }
                ABB_No = PaymentDate.Year.ToString().Substring(2, 2) + PaymentDate.Month.ToString("00") + "000" + "001" + Last_INV.ToString("00000000");
                dtmac.Dispose();
            }
            #endregion


            return ABB_No;
        }
        #endregion

        static private PosDS GetDSNew(PosDS dsAll)
        {
            PosDS dsNew = (PosDS)dsAll.Clone();
            PosDS.T_SegmentRow[] seg = (PosDS.T_SegmentRow[])dsAll.T_Segment.Select("ABBNo is null OR ABBNo = ''");
            PosDS.T_PaxFareRow[] pax = (PosDS.T_PaxFareRow[])dsAll.T_PaxFare.Select("ABBNo is null OR ABBNo = ''");

            PosDS.T_PassengerRow[] passNew = (PosDS.T_PassengerRow[])dsAll.T_Passenger.Select("ABBNo is null OR ABBNo = ''");
            PosDS.T_PassengerFeeRow[] passFeeNew = (PosDS.T_PassengerFeeRow[])dsAll.T_PassengerFee.Select("ABBNo is null OR ABBNo = ''");
            PosDS.T_BookingServiceChargeRow[] fareNew = (PosDS.T_BookingServiceChargeRow[])dsAll.T_BookingServiceCharge.Select("ABBNo is null OR ABBNo = ''");
            PosDS.T_PassengerFeeServiceChargeRow[] feeNew = (PosDS.T_PassengerFeeServiceChargeRow[])dsAll.T_PassengerFeeServiceCharge.Select("ABBNo is null OR ABBNo = ''");
            PosDS.T_PaymentRow[] payNew = (PosDS.T_PaymentRow[])dsAll.T_Payment.Select("ABBNo is null OR ABBNo = ''");


            foreach (PosDS.T_SegmentRow item in seg)
                dsNew.T_Segment.ImportRow(item);

            foreach (PosDS.T_PaxFareRow item in pax)
                dsNew.T_PaxFare.ImportRow(item);

            foreach (PosDS.T_PassengerRow item in passNew)
                dsNew.T_Passenger.ImportRow(item);

            foreach (PosDS.T_PassengerFeeRow item in passFeeNew)
                dsNew.T_PassengerFee.ImportRow(item);

            foreach (PosDS.T_BookingServiceChargeRow item in fareNew)
                dsNew.T_BookingServiceCharge.ImportRow(item);

            foreach (PosDS.T_PassengerFeeServiceChargeRow item in feeNew)
                dsNew.T_PassengerFeeServiceCharge.ImportRow(item);

            foreach (PosDS.T_PaymentRow item in payNew)
                dsNew.T_Payment.ImportRow(item);


            return dsNew;
        }


        #region Validation



        static private int ValidateAgency(PosDS ds, string UserCode)
        {
            PosDS.T_PaymentRow[] drPay = (PosDS.T_PaymentRow[])ds.T_Payment.Select("PaymentMethodCode = 'AG' AND Status = 'Approved' ");
            bool IsUpdate = false;
            if (drPay.Length > 0)
            {
                foreach (PosDS.T_PaymentRow item in drPay)
                {
                    DataRow[] dr = dtAgency.Select("Agency_Code='" + item.AgentCode + "'", "Agency_Type_Code DESC");
                    if (dr.Length == 0)
                    {
                        DBHelper.InsertAgency(item.AgentCode, "000", UserCode);
                        IsUpdate = true;
                    }
                    else
                    {
                        if (dr[0]["Agency_Type_Code"] == null || string.IsNullOrEmpty(dr[0]["Agency_Type_Code"].ToString()))
                        {
                            DBHelper.UpdateAgencyType(item.AgentCode, "000", UserCode);
                            IsUpdate = true;
                        }
                    }
                    //return 1;
                }
            }

            if (IsUpdate)
            {
                dtAgency = DBHelper.GetAgency();
                dtAgencyType = DBHelper.GetGetAgencyType();
            }
            return 0;
        }


        static private int ValidatePaymentType(PosDS ds, DataTable dtPaymentType)
        {
            PosDS.T_PaymentRow[] drPay = (PosDS.T_PaymentRow[])ds.T_Payment.Select("Status = 'Approved' ");
            if (drPay.Length > 0)
            {
                foreach (PosDS.T_PaymentRow item in drPay)
                {
                    DataRow[] dr = dtPaymentType.Select("PaymentType_Code ='" + item.PaymentMethodCode + "'");
                    if (dr.Length == 0)
                        return 2;
                    else if (dr[0]["GenInvoice"].ToString() == "N")
                        return 3;
                }
            }
            return 0;
        }

        static private int ValidatePaymentAmount(PosDS ds)
        {
            PosDS.T_PaymentRow[] drPay = (PosDS.T_PaymentRow[])ds.T_Payment.Select("Status = 'Approved' ");
            decimal Amount = drPay.Sum(it => it.PaymentAmount);
            decimal TotalCost = Convert.ToDecimal(ds.T_Booking[0].TotalCost);
            if (Amount != TotalCost)
            {
                return 4;
            }

            return 0;
        }


        #endregion



        static public bool UpdateABB(string ABB_No, string UserCode, string POS_Code)
        {
            DBHelper da = new DBHelper();

            try
            {
                PosDS ds = DBHelper.GetABB_AllData(ABB_No);
                string BookingNo = ds.T_ABB[0].PNR_No;
                PosDS dsAll = DBHelper.GetBookingAllData(BookingNo);

                dtAgency = DBHelper.GetAgency();
                dtAgencyType = DBHelper.GetGetAgencyType();
                dtPaymentType = DBHelper.GetPaymentType();
                dtPosMachine = DBHelper.GetPosMachine();
                dtFlightFee = DBHelper.GetMasterDataByTableName("M_FlightFee");
                int PaxADT_Count = dsAll.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "ADT");
                int PaxCHD_Count = dsAll.T_Passenger.Count(it => it.PassengerId.ToString() != string.Empty && it.PaxType.ToUpper() == "CHD");

                int Pos_Id = 1;
                DataRow[] dr = (DataRow[])dtPosMachine.Select("POS_Code ='" + POS_Code + "'");
                if (dr.Length > 0)
                    Pos_Id = Convert.ToInt32(dr[0]["POS_Id"]);

                CreateM_FlightFee(ds);
                bool success = false;
                DateTime CurrentDate = DateTime.Now;
                Guid ABBTid = ds.T_ABB[0].ABBTid;
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
                decimal TOTAL = 0;
                decimal All_Discount = 0;
                decimal ExChangeRate = ds.T_ABB[0].ExchangeRateTH;
                AdminFee_Amount = GetSumAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "ADMINFEE");
                AdminFee_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "ADMINFEE");

                FUEL_Amount = GetSumAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "FUEL");
                FUEL_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "FUEL");

                SERVICE_Amount = GetSumAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "SERVICE");
                SERVICE_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "SERVICE");

                VAT_Amount = GetSumAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "VAT");
                VAT_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "VAT");

                AIRPORTTAX_Amount = GetSumAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX");
                AIRPORTTAX_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "AIRPORTTAX");

                INSURANCE_Amount = GetSumAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "INSURANCE");
                INSURANCE_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "INSURANCE");

                REP_Amount = GetSumAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "REP");
                REP_DiscountAmount = GetSumDuscountAmountByAbbGroup(ds, dsAll, PaxADT_Count, PaxCHD_Count, "REP");

                FarePrice_Amount = GetSumAmountByFarePrice(ds, PaxADT_Count, PaxCHD_Count);
                FarePrice_DiscountAmount = GetSumDiscountAmountByFarePrice(ds, PaxADT_Count, PaxCHD_Count);



                AdminFee_Amount = (AdminFee_Amount - AdminFee_DiscountAmount);
                FUEL_Amount = (FUEL_Amount - FUEL_DiscountAmount);
                SERVICE_Amount = (SERVICE_Amount - SERVICE_DiscountAmount);
                VAT_Amount = (VAT_Amount - VAT_DiscountAmount);
                AIRPORTTAX_Amount = (AIRPORTTAX_Amount - AIRPORTTAX_DiscountAmount);
                INSURANCE_Amount = (INSURANCE_Amount - INSURANCE_DiscountAmount);
                REP_Amount = (REP_Amount - REP_DiscountAmount);
                FarePrice_Amount = (FarePrice_Amount - FarePrice_DiscountAmount);

                Other_Amount = GetSumAmountByOther(ds) ;
                All_Discount = GetAllDiscount(ds) ;
                TOTAL = ds.T_Payment.Sum(it => it.PaymentAmount) ;



                da.DBOpenConnection();
                da.BeginTran();

                #region Update T_ABB

                PosDS.T_ABBRow drAbb = ds.T_ABB[0];

                drAbb.PAXCountADT = PaxADT_Count;
                drAbb.PAXCountCHD = PaxCHD_Count;
                drAbb.ADMINFEE = AdminFee_Amount;

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


                drAbb.TOTAL_Ori = TOTAL;
                drAbb.TOTAL = drAbb.TOTAL_Ori * ExChangeRate;
                drAbb.Update_By = UserCode;
                drAbb.Update_Date = CurrentDate;
                drAbb.POS_Id = Pos_Id;
                success = da.UpdateABB(drAbb);

                #endregion

                #region  Pax
                bool IsUpdate = false;
                if (dsAll.T_Passenger.Count != ds.T_ABBPAX.Count)
                {
                    IsUpdate = true;
                }
                else
                {
                    foreach (PosDS.T_PassengerRow item in dsAll.T_Passenger)
                    {
                        string Name = item.FirstName.ToUpper() + item.LastName.ToUpper();
                        PosDS.T_ABBPAXRow[] drPax = (PosDS.T_ABBPAXRow[])ds.T_ABBPAX.Select("FirstName+LastName='" + Name + "'");
                        if (drPax.Length == 0)
                        {
                            IsUpdate = true;
                        }
                    }
                }

                if (IsUpdate)
                {
                    da.DeleteAbbPaxByABBTid(ABBTid);
                    PosDS.T_PassengerRow[] drPass = (PosDS.T_PassengerRow[])dsAll.T_Passenger.Select("", "Create_Date");
                    for (int i = 0; i < drPass.Length; i++) // Save Pax ทั้งหมด
                    {
                        PosDS.T_ABBPAXRow drABBPAX = dsAll.T_ABBPAX.NewT_ABBPAXRow();
                        drABBPAX.ABBPAXId = Guid.NewGuid();
                        drABBPAX.ABBTid = drAbb.ABBTid;
                        drABBPAX.PNR_No = BookingNo;
                        drABBPAX.SEQ = i;
                        drABBPAX.FirstName = drPass[i].FirstName.ToUpper();
                        drABBPAX.LastName = drPass[i].LastName.ToUpper();
                        drABBPAX.Create_By = UserCode;
                        drABBPAX.Create_Date = CurrentDate;
                        drABBPAX.Update_By = UserCode;
                        drABBPAX.Update_Date = CurrentDate;
                        success = da.InsertABBPAX(drABBPAX);
                    }
                }



                #endregion

                #region  T_ABBPAYMENTMETHOD
                IsUpdate = false;
                decimal sum1 = ds.T_Payment.Sum(s => s.PaymentAmount);
                decimal sum2 = ds.T_ABBPAYMENTMETHOD.Sum(s => s.PaymentAmount);
                if (ds.T_Payment.Count != ds.T_ABBPAYMENTMETHOD.Count)
                {
                    IsUpdate = true;
                }
                else if (sum1 != sum2)
                {
                    IsUpdate = true;
                }


                if (IsUpdate)
                {
                    da.DeleteT_ABBPAYMENTMETHODByABBTid(ABBTid);

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
                        drABB_pay.PNR_No = BookingNo;
                        drABB_pay.SEQ = seq;
                        drABB_pay.CurrencyCode = item.CurrencyCode;
                        drABB_pay.PaymentMethodCode = item.PaymentMethodCode;
                        drABB_pay.PaymentType_Name = item.PaymentType_Name;
                        drABB_pay.PaymentAmount_Ori = item.PaymentAmount;
                        drABB_pay.PaymentAmount = item.PaymentAmount * ExChangeRate;
                        drABB_pay.ApprovalDate = item.ApprovalDate;
                        drABB_pay.AgentCode = item.AgentCode;
                        drABB_pay.ExchangeRateTH = ExChangeRate;
                        drABB_pay.Create_By = UserCode;
                        drABB_pay.Create_Date = CurrentDate;
                        drABB_pay.Update_By = UserCode;
                        drABB_pay.Update_Date = CurrentDate;
                        success = da.InsertABBPAYMENTMETHOD(drABB_pay);
                        seq++;
                    }
                }
                #endregion

                #region Insert ABBFARE
                PosDS dsTemp = new PosDS();
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
                int seq2 = 0;

                foreach (var item in dtGseg)
                {
                    drAbbFare = dsTemp.T_ABBFARE.NewT_ABBFARERow();
                    drAbbFare.ABBFAREId = Guid.NewGuid();
                    drAbbFare.ABBTid = drAbb.ABBTid;
                    drAbbFare.SEQ = seq2++;
                    drAbbFare.PNR_No = BookingNo;
                    drAbbFare.CarrierCode = item.CarrierCode;
                    drAbbFare.FlightNumber = item.FlightNumber;
                    drAbbFare.DepartureStation = item.DepartureStation;
                    drAbbFare.ArrivalStation = item.ArrivalStation;
                    drAbbFare.STA = item.STA;
                    drAbbFare.STD = item.STD;
                    drAbbFare.CurrencyCode = item.CurrencyCode;
                    //drAbbFare.Amount_Ori = item.AMT * (PaxADT_Count + PaxCHD_Count);
                    //drAbbFare.Amount = (item.AMT * ExChangeRate) * (PaxADT_Count + PaxCHD_Count); 
                    if (FarePrice_Amount == 0)
                    {
                        drAbbFare.Amount_Ori = 0;
                        drAbbFare.Amount = 0;
                    }
                    else
                    {
                        drAbbFare.Amount_Ori = item.AMT * (PaxADT_Count + PaxCHD_Count);
                        drAbbFare.Amount = (drAbbFare.Amount_Ori * ExChangeRate);
                    }
                    drAbbFare.Create_By = UserCode;
                    drAbbFare.Create_Date = CurrentDate;
                    drAbbFare.Update_By = UserCode;
                    drAbbFare.Update_Date = CurrentDate;
                    dsTemp.T_ABBFARE.AddT_ABBFARERow(drAbbFare);
                    //success = da.InsertABBFARE(drAbbFare);
                }

                IsUpdate = false;
                sum1 = ds.T_ABBFARE.Sum(s => s.Amount);
                sum2 = dsTemp.T_ABBFARE.Sum(s => s.Amount);
                if (ds.T_ABBFARE.Count != dsTemp.T_ABBFARE.Count)
                {
                    IsUpdate = true;
                }
                else if (sum1 != sum2)
                {
                    IsUpdate = true;
                }
                if (IsUpdate)
                {
                    da.DeleteABBFareByABBTid(ABBTid);
                    foreach (PosDS.T_ABBFARERow item in dsTemp.T_ABBFARE)
                    {
                        success = da.InsertABBFARE(item);
                    }
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
                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, CookiesMenager.EmpID);
                throw;
            }
        }

        static public void ClearABB(string BookingNo)
        {
            var context = new POSINVEntities();

            foreach (var item in context.T_ABB.Where(x => x.PNR_No == BookingNo).ToList())
            {
                foreach (var data in context.T_BookingCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_BookingCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }
                foreach (var data in context.T_PassengerCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_PassengerCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }
                foreach (var data in context.T_PassengerFeeCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_PassengerFeeCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }
                foreach (var data in context.T_PassengerFeeServiceChargeCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_PassengerFeeServiceChargeCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }
                foreach (var data in context.T_SegmentCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_SegmentCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }
                foreach (var data in context.T_PaxFareCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_PaxFareCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }
                foreach (var data in context.T_PaymentCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_PaymentCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }
                foreach (var data in context.T_BookingServiceChargeCurrentUpdate.Where(x => x.ABBNo == item.TaxInvoiceNo).ToList())
                {
                    context.T_BookingServiceChargeCurrentUpdate.Attach(data);
                    data.ABBNo = null;
                }

                context.T_ABB.Remove(item);
            }
            foreach (var item in context.T_ABBFARE.Where(x => x.PNR_No == BookingNo).ToList())
            {
                context.T_ABBFARE.Remove(item);
            }
            foreach (var item in context.T_ABBPAX.Where(x => x.PNR_No == BookingNo).ToList())
            {
                context.T_ABBPAX.Remove(item);
            }
            foreach (var item in context.T_ABBPAYMENTMETHOD.Where(x => x.PNR_No == BookingNo).ToList())
            {
                context.T_ABBPAYMENTMETHOD.Remove(item);
            }

            context.SaveChanges();

        }


        static public void CreateM_FlightFee(PosDS ds)
        {
            DBHelper da = new DBHelper();

            try
            {


                string[] distinctRows1 = (from PosDS.T_BookingServiceChargeRow r in ds.T_BookingServiceCharge
                                          select r.ChargeCode).Distinct().ToArray();

                string[] distinctRows2 = (from PosDS.T_PassengerFeeServiceChargeRow r in ds.T_PassengerFeeServiceCharge
                                          select r.ChargeCode).Distinct().ToArray();

                da.DBOpenConnection();
                da.BeginTran();
                bool IsUpdate = false;
                for (int i = 0; i < distinctRows1.Length; i++)
                {
                    if (distinctRows1[i] != string.Empty)
                    {
                        DataRow[] dr = (DataRow[])dtFlightFee.Select("Flight_Fee_Code='" + distinctRows1[i] + "'");
                        if (dr.Length == 0)
                        {
                            da.InsertFlightFee(distinctRows1[i], "System");
                            IsUpdate = true;
                        }
                    }
                }
                for (int i = 0; i < distinctRows2.Length; i++)
                {
                    if (distinctRows2[i] != string.Empty)
                    {
                        DataRow[] dr = (DataRow[])dtFlightFee.Select("Flight_Fee_Code='" + distinctRows2[i] + "'");
                        if (dr.Length == 0)
                        {
                            da.InsertFlightFee(distinctRows2[i], "System");
                            IsUpdate = true;
                        }
                    }
                }

                da.EndTran(true);
                da.DBCloseConnection();


                if (IsUpdate)
                {
                    dtFlightFee = DBHelper.GetMasterDataByTableName("M_FlightFee");
                }
            }
            catch (Exception ex)
            {
                da.EndTran(false);
                da.DBCloseConnection();
                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, CookiesMenager.EmpID);
            }
        }


        static public bool UpdateDataOnline(string PNR_No, string User_id)
        {
            try
            {

                DAO.GetDataSkySpeed getdata = new GetDataSkySpeed();
                com.airasia.acebooking.Booking bookingdata = getdata.GetData(PNR_No);
                DataService sv = new DataService();
                if (bookingdata != null)
                {
                    PosDS dsDB = new PosDS();
                    dsDB = DBHelper.GetBookingByBookingNo(dsDB, PNR_No);
                    if (dsDB.T_Booking.Count == 0)
                    {
                        //getdata.LoadToDB(bookingdata, User_id);
                        //return true;
                        PosDS ds = LoadXMLToDS(bookingdata, User_id, 1);

                        bool success = sv.PosSave(ds);
                        return true;
                    }
                    else
                    {
                        Guid TransactionId = dsDB.T_Booking[0].TransactionId;
                        PosDS dsXML = LoadXMLToDS(bookingdata, User_id, 0);
                        dsDB = DBHelper.GetBookingAllData(PNR_No);

                        PosDS dsSave = sv.ProcessDataDevide(dsXML);
                        dsSave = sv.ChangeFlight(dsSave);
                        dsSave = sv.UpdateItem(dsSave);
                        sv.PosSave(dsSave);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }



        static public PosDS ProcessDataDevide(PosDS ds)
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

        static public PosDS UpdateItem(PosDS ds)
        {
            PosDS.T_PassengerFeeServiceChargeRow[] drpassSV = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("ACtion = -1 AND ChargeCode <> 'VAT'");
            foreach (PosDS.T_PassengerFeeServiceChargeRow item in drpassSV)
            {
                DataTable dt = DBHelper.GetFlightFeeGroup(item.ChargeCode);
                if (dt.Rows.Count > 0)
                {
                    PosDS.T_PassengerFeeServiceChargeRow[] drpassSV_NEW = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("ACtion = 1 AND PassengerID = '" + item.PassengerId + "' AND ChargeCode <> 'VAT' AND Row_State = 0  ");

                    if (drpassSV_NEW.Length > 0)
                    {
                        item.Action = 2;
                        drpassSV_NEW[0].Row_State = 1;
                        drpassSV_NEW[0].BaseAmount = drpassSV_NEW[0].Amount;
                        drpassSV_NEW[0].Amount = drpassSV_NEW[0].Amount - item.Amount;

                        decimal vat = Convert.ToDecimal(drpassSV_NEW[0].BaseAmount) * 0.07M;
                        vat = Math.Round(vat, 2);

                        decimal vat_Old = Convert.ToDecimal(item.Amount) * 0.07M;
                        vat_Old = Math.Round(vat_Old, 2);

                        PosDS.T_PassengerFeeServiceChargeRow[] drpassSV_VAT_NEW = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("ACtion = 1 AND PassengerID = '" + item.PassengerId + "' AND ChargeCode = 'VAT' AND Amount = " + vat + " AND Row_State = 0 ");

                        PosDS.T_PassengerFeeServiceChargeRow[] drpassSV_VAT_Old = (PosDS.T_PassengerFeeServiceChargeRow[])ds.T_PassengerFeeServiceCharge.Select("ACtion = -1 AND PassengerID = '" + item.PassengerId + "' AND ChargeCode = 'VAT' AND Amount = " + vat_Old + " AND Row_State = 0 ");

                        if (drpassSV_VAT_NEW.Length > 0)
                        {
                            drpassSV_VAT_NEW[0].Row_State = 1;
                            drpassSV_VAT_NEW[0].BaseAmount = drpassSV_VAT_NEW[0].Amount;
                            drpassSV_VAT_NEW[0].Amount = drpassSV_VAT_NEW[0].Amount - vat_Old;

                            drpassSV_VAT_Old[0].Row_State = 1;
                            drpassSV_VAT_Old[0].Action = 2;
                        }



                    }


                }
            }
            return ds;
        }

        #region Compare Data
        
        
        static void CompareBooking(ref PosDS dsDB, ref PosDS dsXML)
        {
            dsDB.T_Booking[0].Action = 2;
            dsDB.T_Booking[0].PaxCount = dsXML.T_Booking[0].PaxCount;
            dsDB.T_Booking[0].TotalCost = dsXML.T_Booking[0].TotalCost;
        }


        static void ComparePassenger(ref PosDS dsDB, ref PosDS dsXML)
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
                    item_Xml.Action = 1;
                    item_Xml.TransactionId = dsDB.T_Booking[0].TransactionId;
                    dsDB.T_Passenger.ImportRow(item_Xml);
                }
            }
        }

        static void CompareSegment(ref PosDS dsDB, ref PosDS dsXML)
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
                    dsDB.T_Segment.ImportRow(item_Xml);
                }
            }
        }

        static void ComparePax(ref PosDS dsDB, ref PosDS dsXML)
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
                    item_Xml.Action = 1;
                    dsDB.T_PaxFare.ImportRow(item_Xml);
                }
            }
        }

        static void CompareBookingServiceCharge(ref PosDS dsDB, ref PosDS dsXML)
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
                    item_Xml.Action = 1;
                    dsDB.T_BookingServiceCharge.ImportRow(item_Xml);
                }
            }
        }

        static void ComparePassengerFee(ref PosDS dsDB, ref PosDS dsXML)
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
                    dsDB.T_PassengerFee.ImportRow(item_Xml);
                }
            }
        }

        static void ComparePassengerFeeServiceCharge(ref PosDS dsDB, ref PosDS dsXML)
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
                    item_Xml.Action = 1;
                    dsDB.T_PassengerFeeServiceCharge.ImportRow(item_Xml);
                }
            }
        }

        static void ComparePayment(ref PosDS dsDB, ref PosDS dsXML)
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
                    item_Xml.TransactionId = dsDB.T_Booking[0].TransactionId;
                    item_Xml.Action = 1;
                    dsDB.T_Payment.ImportRow(item_Xml);
                }
            }
        }


        #endregion

        static private string SaveData(PosDS ds)
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
                    if (item.Action == 1)
                        success = da.InsertBooking(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdateBooking(item);
                }

                #endregion

                #region Insert T_Passenger

                foreach (PosDS.T_PassengerRow item in ds.T_Passenger)
                {
                    if (item.Action == 1)
                        success = da.InsertPassenger(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdatePassenger(item);
                }

                #endregion

                #region T_Payment


                foreach (PosDS.T_PaymentRow item in ds.T_Payment)
                {
                    if (item.Action == 1)
                        success = da.InsertPayment(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdatePayment(item);
                }
                #endregion

                #region T_BookingServiceCharge

                foreach (PosDS.T_BookingServiceChargeRow item in ds.T_BookingServiceCharge)
                {
                    if (item.Action == 1)
                        success = da.InsertBookingServiceCharge(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdateBookingServiceCharge(item);
                }

                #endregion

                #region Segment
                foreach (PosDS.T_SegmentRow item in ds.T_Segment)
                {
                    if (item.Action == 1)
                        success = da.InsertSegment(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdateSegment(item);
                }
                #endregion

                #region T_PaxFare


                foreach (PosDS.T_PaxFareRow item in ds.T_PaxFare)
                {
                    if (item.Action == 1)
                        success = da.InsertPaxFare(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdatePaxFare(item);
                }

                #endregion

                #region PassengerFee
                foreach (PosDS.T_PassengerFeeRow item in ds.T_PassengerFee)
                {
                    if (item.Action == 1)
                        success = da.InsertPassengerFee(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdatePassengerFee(item);
                }
                #endregion

                #region T_PassengerFeeServiceCharge

                foreach (PosDS.T_PassengerFeeServiceChargeRow item in ds.T_PassengerFeeServiceCharge)
                {
                    if (item.Action == 1)
                        success = da.InsertPassServiceCharge(item);
                    else if (item.Action == 2 || item.Action == -1)
                        success = da.UpdatePassServiceCharge(item);
                }

                #endregion

                #region T_ABB


                foreach (PosDS.T_ABBRow item in ds.T_ABB)
                {
                    if (item.Action == 1)
                        success = da.InsertABB(item);
                }

                #endregion

                #region T_ABBPAX

                foreach (PosDS.T_ABBPAXRow item in ds.T_ABBPAX)
                {
                    if (item.Action == 1)
                        success = da.InsertABBPAX(item);
                }
                #endregion

                #region T_ABBPAYMENTMETHOD

                foreach (PosDS.T_ABBPAYMENTMETHODRow item in ds.T_ABBPAYMENTMETHOD)
                {
                    if (item.Action == 1)
                        success = da.InsertABBPAYMENTMETHOD(item);
                }

                #endregion

                #region ABBFARE

                foreach (PosDS.T_ABBFARERow item in ds.T_ABBFARE)
                {
                    if (item.Action == 1)
                        success = da.InsertABBFARE(item);
                }

                #endregion

                da.EndTran(true);
                da.DBCloseConnection();
                return string.Empty;

            }
            catch (Exception ex)
            {
                da.EndTran(false);
                da.DBCloseConnection();
                return ex.Message;
            }
        }



        static void ChangeFlight(ref PosDS dsDB)
        {
            PosDS.T_BookingServiceChargeRow[] drFlightOld = (PosDS.T_BookingServiceChargeRow[])dsDB.T_BookingServiceCharge.Select("Action = -1");
            foreach (PosDS.T_BookingServiceChargeRow item in drFlightOld)
            {
                PosDS.T_BookingServiceChargeRow[] drSel = (PosDS.T_BookingServiceChargeRow[])dsDB.T_BookingServiceCharge.Select("Action = 1 AND ChargeCode ='"+ item.ChargeCode+"'");
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
                drPassServ_New = (PosDS.T_PassengerFeeServiceChargeRow[])dsDB.T_PassengerFeeServiceCharge.Select("Action = 1 AND PassengerId =" +item.PassengerId +" AND ChargeCode ='"+ item.ChargeCode+"' AND Amount=" +item.Amount);
                if (drPassServ_New.Length > 0)
                {
                    item.Action = 0;
                    drPassServ_New[0].Action = 0;
                }

            }
        }

        static public PosDS LoadXMLToDS(com.airasia.acebooking.Booking booking, string UserCode,int Row_State)
        {

            DateTime CurrentDate = DateTime.Now;
            PosDS ds = new PosDS();
            PosDS.T_BookingRow drBooking;
            PosDS.T_PaymentRow drPay;
            PosDS.T_PassengerRow drPass;
            PosDS.T_PassengerFeeRow drPassFee;
            PosDS.T_SegmentRow drSeg;
            PosDS.T_PaxFareRow drPax;
            PosDS.T_BookingServiceChargeRow drBookServ;
            PosDS.T_PassengerFeeServiceChargeRow drFeeServ;
            DateTime SaleDate;
            try
            {
                if (booking.BookingID != 0)
                {
                    Guid tid = Guid.NewGuid();

                    #region booking
                    string paxcount = booking.PaxCount.ToString();
                    string status = booking.BookingInfo.BookingStatus.ToString();
                    string Total = booking.BookingSum.TotalCost.ToString();
                    drBooking = ds.T_Booking.NewT_BookingRow();

                    drBooking.TransactionId = tid;
                    drBooking.BookingId = booking.BookingID;
                    drBooking.Load_Date = CurrentDate;
                    drBooking.PNR_No = booking.RecordLocator;
                    drBooking.Booking_Date = booking.BookingInfo.BookingDate;
                    drBooking.CurrencyCode = booking.CurrencyCode;
                    drBooking.PaxCount = paxcount;
                    drBooking.BookingParentID = booking.BookingParentID;
                    drBooking.ParentRecordLocator = booking.ParentRecordLocator;
                    drBooking.GroupName = booking.GroupName;
                    drBooking.BookingStatus = status;
                    drBooking.TotalCost = Total;
                    drBooking.Create_By = UserCode;
                    drBooking.Create_Date = CurrentDate;
                    drBooking.Update_By = UserCode;
                    drBooking.Update_Date = CurrentDate;
                    drBooking.Row_State = Row_State;
                    ds.T_Booking.AddT_BookingRow(drBooking);
                    #endregion

                    foreach (com.airasia.acebooking.Payment payment in booking.Payments)
                    {
                        #region add payment
                        drPay = ds.T_Payment.NewT_PaymentRow();
                        string ReferenceType = payment.ReferenceType.ToString();
                        string PaymentMethodType = payment.PaymentMethodType.ToString();
                        string PaymentStatus = payment.Status.ToString();

                        drPay.PaymentTId = Guid.NewGuid();
                        drPay.TransactionId = tid;
                        drPay.PaymentID = payment.PaymentID;
                        drPay.ReferenceType = ReferenceType;
                        drPay.ReferenceID = payment.ReferenceID;
                        drPay.PaymentMethodType = PaymentMethodType;
                        drPay.PaymentMethodCode = payment.PaymentMethodCode;
                        drPay.CurrencyCode = payment.CurrencyCode;
                        drPay.PaymentAmount = payment.PaymentAmount;
                        drPay.CollectedCurrencyCode = payment.CollectedCurrencyCode;
                        drPay.CollectedAmount = payment.CollectedAmount;
                        drPay.QuotedCurrencyCode = payment.QuotedCurrencyCode;
                        drPay.QuotedAmount = payment.QuotedAmount;
                        drPay.Status = PaymentStatus;
                        drPay.ParentPaymentID = payment.ParentPaymentID;
                        drPay.AgentCode = payment.PointOfSale.AgentCode;
                        drPay.OrganizationCode = payment.PointOfSale.OrganizationCode;
                        drPay.DomainCode = payment.PointOfSale.DomainCode;
                        drPay.LocationCode = payment.PointOfSale.LocationCode;
                        drPay.ApprovalDate = payment.ApprovalDate;
                        drPay.Create_By = UserCode;
                        drPay.Create_Date = CurrentDate;
                        drPay.Update_By = UserCode;
                        drPay.Update_Date = CurrentDate;
                        drPay.BankName = string.Empty;
                        drPay.BranchNo = string.Empty;
                        if (payment.PaymentFields != null && payment.PaymentFields.Length > 0)
                        {
                            foreach (var item in payment.PaymentFields)
                            {
                                switch (item.FieldName.ToUpper())
                                {
                                    case "ISSNAME":
                                        drPay.BankName = item.FieldValue;
                                        break;
                                    case "ISSCTRY":
                                        drPay.BranchNo = item.FieldValue;
                                        break;
                                }
                            }
                        }

                        drPay.AccountNo = payment.AccountNumber;
                        drPay.AccountNoID = payment.AccountNumberID.ToString();
                        drPay.Row_State = Row_State;
                        ds.T_Payment.AddT_PaymentRow(drPay);

                        #endregion
                    }

                    foreach (com.airasia.acebooking.Passenger pax in booking.Passengers)
                    {
                        #region add Passenger
                        drPass = ds.T_Passenger.NewT_PassengerRow();

                        Guid paxid = Guid.NewGuid();

                        drPass.PassengerTId = paxid;
                        drPass.TransactionId = tid;
                        drPass.PassengerId = pax.PassengerID;
                        drPass.FirstName = pax.Names[0].FirstName;
                        drPass.LastName = pax.Names[0].LastName;
                        drPass.Title = pax.Names[0].Title;
                        drPass.PaxType = pax.PassengerTypeInfo.PaxType;
                        drPass.Create_By = UserCode;
                        drPass.Create_Date = CurrentDate;
                        drPass.Update_By = UserCode;
                        drPass.Update_Date = CurrentDate;
                        drPass.Row_State = Row_State;
                        ds.T_Passenger.AddT_PassengerRow(drPass);

                        if (pax.PassengerFees != null)
                        {
                            foreach (com.airasia.acebooking.PassengerFee paxfee in pax.PassengerFees)
                            {
                                drPassFee = ds.T_PassengerFee.NewT_PassengerFeeRow();
                                long feeno = long.Parse(pax.PassengerID.ToString() + paxfee.FeeNumber.ToString());
                                Guid paxfeeid = Guid.NewGuid();

                                drPassFee.PassengerFeeTId = paxfeeid;
                                drPassFee.PassengerTId = paxid;
                                drPassFee.PassengerId = pax.PassengerID;
                                drPassFee.PassengerFeeNo = feeno;
                                drPassFee.FeeCreateDate = paxfee.CreatedDate;
                                drPassFee.Create_By = UserCode;
                                drPassFee.Create_Date = CurrentDate;
                                drPassFee.Update_By = UserCode;
                                drPassFee.Update_Date = CurrentDate;
                                drPassFee.Row_State = Row_State;
                                ds.T_PassengerFee.AddT_PassengerFeeRow(drPassFee);

                                foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfee.ServiceCharges)
                                {
                                    drFeeServ = ds.T_PassengerFeeServiceCharge.NewT_PassengerFeeServiceChargeRow();
                                    string servno = pax.PassengerID.ToString() + paxfee.FeeNumber.ToString() + servchgr.ChargeCode.Trim() + servchgr.TicketCode.Trim() + servchgr.Amount.ToString().Trim();
                                    string chgrtype = servchgr.ChargeType.ToString();
                                    string coltype = servchgr.CollectType.ToString();

                                    drFeeServ.PassengerFeeServiceChargeTId = Guid.NewGuid();
                                    drFeeServ.PassengerFeeTId = paxfeeid;
                                    drFeeServ.PassengerId = pax.PassengerID;
                                    drFeeServ.PassengerFeeNo = feeno;
                                    drFeeServ.FeeCreateDate = paxfee.CreatedDate;
                                    drFeeServ.ServiceChargeNo = servno;
                                    drFeeServ.ChargeType = chgrtype;
                                    drFeeServ.CollectType = coltype;
                                    drFeeServ.ChargeCode = servchgr.ChargeCode;
                                    drFeeServ.TicketCode = servchgr.TicketCode;
                                    drFeeServ.BaseCurrencyCode = (servchgr.BaseCurrencyCode == null) ? "" : servchgr.BaseCurrencyCode;
                                    drFeeServ.CurrencyCode = servchgr.CurrencyCode;
                                    drFeeServ.Amount = servchgr.Amount;
                                    drFeeServ.BaseAmount = servchgr.BaseAmount;
                                    drFeeServ.ChargeDetail = servchgr.ChargeDetail;
                                    drFeeServ.ForeignCurrencyCode = servchgr.ForeignCurrencyCode;
                                    drFeeServ.ForeignAmount = servchgr.ForeignAmount;
                                    drFeeServ.Create_By = UserCode;
                                    drFeeServ.Create_Date = CurrentDate;
                                    drFeeServ.Update_By = UserCode;
                                    drFeeServ.Update_Date = CurrentDate;
                                    drFeeServ.Row_State = Row_State;
                                    ds.T_PassengerFeeServiceCharge.AddT_PassengerFeeServiceChargeRow(drFeeServ);

                                }
                            }

                        }


                        #endregion
                    }

                    if (booking.Journeys != null)
                    {
                        foreach (com.airasia.acebooking.Journey jou in booking.Journeys)
                        {

                            foreach (com.airasia.acebooking.Segment seq in jou.Segments)
                            {
                                #region add Segment
                                drSeg = ds.T_Segment.NewT_SegmentRow();
                                Guid seqid = Guid.NewGuid();
                                string seqmentNo = seq.DepartureStation.Trim() + seq.ArrivalStation.Trim() + seq.FlightDesignator.CarrierCode.Trim() + seq.FlightDesignator.FlightNumber.Trim();
                                SaleDate = seq.SalesDate;
                                drSeg.SegmentTId = seqid;
                                drSeg.TransactionId = tid;
                                drSeg.SeqmentNo = seqmentNo;
                                drSeg.DepartureStation = seq.DepartureStation;
                                drSeg.ArrivalStation = seq.ArrivalStation;
                                drSeg.STD = seq.STD;
                                drSeg.STA = seq.STA;
                                drSeg.CarrierCode = seq.FlightDesignator.CarrierCode.Trim();
                                drSeg.FlightNumber = seq.FlightDesignator.FlightNumber.Trim();
                                drSeg.Create_By = UserCode;
                                drSeg.Create_Date = CurrentDate;
                                drSeg.Update_By = UserCode;
                                drSeg.Update_Date = CurrentDate;
                                drSeg.Row_State = Row_State;
                                ds.T_Segment.AddT_SegmentRow(drSeg);


                                foreach (com.airasia.acebooking.Fare fare in seq.Fares)
                                {
                                    foreach (com.airasia.acebooking.PaxFare paxfare in fare.PaxFares)
                                    {
                                        drPax = ds.T_PaxFare.NewT_PaxFareRow();
                                        Guid _paxfareid = Guid.NewGuid();
                                        string seqmentpaxNo = seqmentNo + paxfare.PaxType.Trim();

                                        drPax.PaxFareTId = _paxfareid;
                                        drPax.SegmentTId = seqid;
                                        drPax.SeqmentNo = seqmentNo;
                                        drPax.SeqmentPaxNo = seqmentpaxNo;
                                        drPax.PaxType = paxfare.PaxType;
                                        drPax.Create_By = UserCode;
                                        drPax.Create_Date = CurrentDate;
                                        drPax.Update_By = UserCode;
                                        drPax.Update_Date = CurrentDate;
                                        drPax.Row_State = Row_State;
                                        ds.T_PaxFare.AddT_PaxFareRow(drPax);
                                        var pax = booking.Passengers.Where(x => x.PassengerTypeInfo.PaxType == paxfare.PaxType).ToList();
                                        foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfare.ServiceCharges)
                                        {
                                            drBookServ = ds.T_BookingServiceCharge.NewT_BookingServiceChargeRow();
                                            string serviceChargeNo = seqmentpaxNo + servchgr.ChargeCode.Trim() + servchgr.TicketCode.Trim() + servchgr.Amount;
                                            string chgrtype = servchgr.ChargeType.ToString();
                                            string coltype = servchgr.CollectType.ToString();
                                            servchgr.BaseCurrencyCode = "";


                                            drBookServ.BookingServiceChargeTId = Guid.NewGuid();
                                            drBookServ.PaxFareTId = _paxfareid;
                                            drBookServ.SeqmentNo = seqmentNo;
                                            drBookServ.SeqmentPaxNo = seqmentpaxNo;
                                            drBookServ.ServiceChargeNo = serviceChargeNo;
                                            drBookServ.ChargeType = chgrtype;
                                            drBookServ.CollectType = coltype;
                                            drBookServ.ChargeCode = servchgr.ChargeCode;
                                            drBookServ.TicketCode = servchgr.TicketCode;
                                            drBookServ.CurrencyCode = servchgr.CurrencyCode;
                                            drBookServ.Amount = servchgr.Amount;
                                            drBookServ.BaseAmount = servchgr.BaseAmount;
                                            drBookServ.ChargeDetail = servchgr.ChargeDetail;
                                            drBookServ.ForeignCurrencyCode = servchgr.ForeignCurrencyCode;
                                            drBookServ.ForeignAmount = servchgr.ForeignAmount;
                                            drBookServ.FareCreateDate = SaleDate;
                                            drBookServ.Create_By = UserCode;
                                            drBookServ.Create_Date = CurrentDate;
                                            drBookServ.Update_By = UserCode;
                                            drBookServ.Update_Date = CurrentDate;
                                            drBookServ.Row_State = Row_State;
                                            drBookServ.QTY = pax.Count;
                                            ds.T_BookingServiceCharge.AddT_BookingServiceChargeRow(drBookServ);
                                        }
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return ds;
        }


        static PosDS SetRowState4Update(PosDS ds)
        {
            foreach (PosDS.T_BookingRow item in ds.T_Booking.Select("ABBNo is null"))
                item.Row_State = 2;
            foreach (PosDS.T_PassengerRow item in ds.T_Passenger.Select("ABBNo is null"))
                item.Row_State = 2;
            foreach (PosDS.T_PassengerFeeRow item in ds.T_PassengerFee.Select("ABBNo is null"))
                item.Row_State = 2;
            foreach (PosDS.T_PassengerFeeServiceChargeRow item in ds.T_PassengerFeeServiceCharge.Select("ABBNo is null"))
                item.Row_State = 2;
            foreach (PosDS.T_SegmentRow item in ds.T_Segment.Select("ABBNo is null"))
                item.Row_State = 2;
            foreach (PosDS.T_PaxFareRow item in ds.T_PaxFare.Select("ABBNo is null"))
                item.Row_State = 2;
            foreach (PosDS.T_BookingServiceChargeRow item in ds.T_BookingServiceCharge.Select("ABBNo is null"))
                item.Row_State = 2;
            foreach (PosDS.T_PaymentRow item in ds.T_Payment.Select("ABBNo is null"))
                item.Row_State = 2;
            return ds;
        }

    }
}