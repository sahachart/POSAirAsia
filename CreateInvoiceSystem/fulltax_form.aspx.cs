using CreateInvoiceSystem.DAO;
using CreateInvoiceSystem.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class fulltax_form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Preveiw_form();
            }
        }
        private void Preveiw_form()
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;

            ReportViewer1.AsyncRendering = false;
            ReportViewer1.SizeToReportContent = true;

            string userid = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            string POS_CODE = Context.Request.Cookies["authenuser"].Values["POS_CODE"];
            string INV_No = this.Request["INV_No"].ToString();
            string Mode = this.Request["Mode"].ToString();

            InvoiceDA.UpdteIsPrint(INV_No);

            #region Header/Footer

            var Header = MasterDA.GetMessageList(MasterDA.SlipType.Invoice, true, POS_CODE);
            var HeaderDesc = String.Join("<br/>", Header.MessageModels.Select(x => x.Descriptions));

            var Footers = MasterDA.GetMessageList(MasterDA.SlipType.Invoice, false, POS_CODE);
            var FooterDesc = String.Join("<br/>", Footers.MessageModels.Select(x => x.Descriptions));

            #endregion

            #region GetData
            
            var user_obj = MasterDA.GetUserDetail(userid, POS_CODE);
            var context = new POSINVEntities();

            T_Invoice_Info _Invoice_Info = context.T_Invoice_Info.Where(it => it.INV_No == INV_No).FirstOrDefault();

            var PassengerNames = "&nbsp;" + _Invoice_Info.PassengerName.Replace(",", "<br/>&nbsp;");

            List<FullTaxModel> model = new List<FullTaxModel>();
            FullTaxModel obj = new FullTaxModel();
            obj.DocumentDate = DateTime.Now;
            if (context.T_PaymentCurrentUpdate.Count(x => _Invoice_Info.ABB_NO.Contains( x.ABBNo)) > 0)
            {
                obj.DocumentDate = context.T_PaymentCurrentUpdate.Where(x => _Invoice_Info.ABB_NO.Contains(x.ABBNo)).OrderBy(c => c.Create_Date).First().ApprovalDate;
            }

            obj.Bill_Collector = user_obj.FirstName + " " + user_obj.LastName;

            var ListFlightFeeService = new List<ABB_2_Model>();
            string FairDesc = string.Empty;

            var SumService = _Invoice_Info.Service;// ABBs.Sum(x => x.Service);
            var SumFuel = _Invoice_Info.Fuel;// ABBs.Sum(x => x.Fuel);
            var SumINSURANCE = _Invoice_Info.INSURANCE;// ABBs.Sum(x => x.INSURANCE);
            var SumADMINFEE = _Invoice_Info.ADMINFEE;// ABBs.Sum(x => x.ADMINFEE);
            var SumOTHER = _Invoice_Info.OTHER;// ABBs.Sum(x => x.OTHER);

            var SumTotal = _Invoice_Info.TOTAL;// ABBs.Sum(x => x.TOTAL);
            var SumAirportTax = _Invoice_Info.AIRPORTTAX;// ABBs.Sum(x => x.AIRPORTTAX);
            var SumVAT = _Invoice_Info.VAT;// ABBs.Sum(x => x.VAT);
            var SumREP = _Invoice_Info.REP;// ABBs.Sum(x => x.REP);


            decimal amount = SumTotal - (SumVAT + SumAirportTax);
            decimal Fair = amount;
            decimal FairOfficial = 0;

            if (Mode == "1")
            {
                FairDesc = "ค่าตั๋วโดยสารและค่าธรรมเนียมอื่นๆ";

                Fair = amount - (SumService + SumFuel + SumINSURANCE + SumADMINFEE + SumOTHER);

                
                if (SumFuel != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าน้ำมัน/Fuel Sercharg",
                        pay_amt = SumFuel.ToString("#,#0.00")
                    });
                }
                if (SumService != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าบริการ/Service",
                        pay_amt = SumService.ToString("#,#0.00")
                    });
                }
                if (SumINSURANCE != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าประกัน/Insurance",
                        pay_amt = SumINSURANCE.ToString("#,#0.00")
                    });
                }
                if (SumADMINFEE != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าบริการ/Admin Fee",
                        pay_amt = SumADMINFEE.ToString("#,#0.00")
                    });
                }
                if (SumOTHER != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "อื่นๆ/Other",
                        pay_amt = SumOTHER.ToString("#,#0.00")
                    });
                }
            }
            else
            {
                FairDesc = "ค่าตั๋วโดยสาร";

                FairOfficial = (SumService + SumFuel + SumINSURANCE + SumADMINFEE + SumOTHER);
            }

            obj.Amount = Fair;

            obj.AMT = amount; //มูลค่าสินค้า/บริการ
            obj.Vat = SumVAT;
            obj.Amount_Total = amount + SumVAT; //จำนวนเงินรวม
            obj.AirportTax = SumAirportTax;
            obj.Amount_Rep = SumREP;
            obj.Grand_Total = SumTotal;

            obj.Amount_Service = SumService;
            obj.InvoiceNo = _Invoice_Info.INV_No;
            obj.Ref = "";
            
            
            obj.Qty = 1;

            var mword = TextToMoneyWord.ThaiBaht(obj.Grand_Total.ToString());
            obj.toWord = mword;

            

            string PayType = "";
            if (context.M_PaymentType.Count(x => x.PaymentType_Id == _Invoice_Info.PayType) > 0)
            {
                PayType = context.M_PaymentType.First(x => x.PaymentType_Id == _Invoice_Info.PayType).PaymentType_Name;
            }

            obj.DESC = "";

            var FarePrice = "<br/>0.00";
            var Vat = "0";
            var fares = _Invoice_Info.T_Invoice_Flight.OrderBy(f => f.SEQ).ToList();
            if (fares != null && fares.Count() > 0)
            {
                var FareDesc = fares.Select(x => x.CarrierCode + x.FlightNumber + " " + x.DepartureStation + "-" + x.ArrivalStation +
                                                 "<br/>&nbsp;" + x.DepartureDate.ToString("dd/MM/yyyy HH:mm") + " - " + x.ArraivalDate.ToString("dd/MM/yyyy HH:mm"))
                                    .ToList();
                obj.DESC = "&nbsp;" + String.Join("<br/>&nbsp;", FareDesc);

                var BaseFare = fares.Select(x => x.T_Invoice_Fare.Where(s => s.ChargeCode == "").Sum(c => c.TotalTH)).ToList();
                if (BaseFare.Count() > 0 && FairOfficial != 0)
                {
                    var LastSeq = BaseFare.Count() - 1;
                    BaseFare[LastSeq] = BaseFare[LastSeq] + FairOfficial;
                }
                
                FarePrice = "<br/>" + String.Join("<br/><br/>", BaseFare.Select( x=> x.ToString("#,#0.00")).ToList());

                if (fares.Count(x => x.FlightNumber.Length == 3) == 0)
                {
                    Vat = "7";
                }
            }


            var PaxCount = (_Invoice_Info.PAXCountADT + _Invoice_Info.PAXCountCHD).ToString("#,##0");
            //obj.DESC = flight + "<br/>PASSENGERS<br/>&nbsp;" + (FirstABB.PAXCountADT + FirstABB.PAXCountCHD).ToString("#,##0 PAX");

            var res_cus = context.M_Customer.Where(m => m.CustomerID == _Invoice_Info.CustomerID).FirstOrDefault();
            obj.Customer = res_cus.first_name + 
                (res_cus.Branch_No != null && res_cus.Branch_No.Trim() != "" ? " " + res_cus.Branch_No : "") + 
                "<br/>" + _Invoice_Info.Address.Replace("\n", "<br/>") + " " + " " + "<br/>" +
                "เลขประจำตัวผู้เสียภาษี : " + res_cus.TaxID;

            model.Add(obj);

            #endregion



            #region Report Parameter

            var Invoice_Payments = _Invoice_Info.T_Invoice_Payment.OrderBy(f => f.SEQ).ToList();

            var ABB_3_Models = new List<ABB_3_Model>();

            

            ReportViewer1.LocalReport.DataSources.Clear();

            ReportParameter[] _ReportParameter;
            if (Header.IsA4)
            {

                //var Payments = context.T_PaymentCurrentUpdate.Where(x => x.ABBNo != null && ABBs.Count(s => s.TaxInvoiceNo == x.ABBNo) > 0).OrderBy(c => c.ApprovalDate).ToList();

                var IsCash = "0";
                var IsCheque = "0";
                var IsTransfer = "0";
                var BankTransfer = string.Empty;
                var AccountNo = string.Empty;
                var BankCheque = string.Empty;
                var Branch = string.Empty;
                var ChequeNo = string.Empty;
                var ChequeDate = string.Empty;

                foreach (var item in Invoice_Payments)
                {
                    var ABB_3_Model = new ABB_3_Model();
                    ABB_3_Model.pay_type = "ชำระโดย " + item.PaymentType_Name + (item.CurrencyCode.ToUpper() != "THB" ? " (Exchange Rate " + item.ExchangeRateTH.ToString("#,#0.000000") + " THB : 1 " + item.CurrencyCode + ")" : "");
                    ABB_3_Model.pay_amt = item.CurrencyCode + " " + item.PaymentAmount_Ori.ToString("#,#0.00");
                    ABB_3_Models.Add(ABB_3_Model);
                }
                

                

                //var PayCodes = Payments.GroupBy(s => s.PaymentMethodCode).Select(x => x.Key).ToList();
                //var _PaymentTypes = context.M_PaymentType.Where(x => x.PaymentType_Code != null && PayCodes.Count(s => s == x.PaymentType_Code) > 0).Select(c => c.PaymentType_Name).ToList();

                //PayType = String.Join(", ", _PaymentTypes);
                
                //if (Payments.Count(x => x.PaymentMethodCode == "1Q" || x.PaymentMethodCode == "CK") > 0)
                //{
                //    IsCheque = "1";
                //    var Cheque = Payments.Where(x => x.PaymentMethodCode == "1Q" || x.PaymentMethodCode == "CK").OrderBy(x => x.ApprovalDate).First();
                //    BankCheque = Cheque.BankName ?? string.Empty;
                //    ChequeNo = Cheque.AccountNo ?? string.Empty;
                //    ChequeDate = Cheque.ApprovalDate.ToString("dd/MM/yyyy");
                //}
                //if (Payments.Count(x => x.PaymentMethodCode != "1Q" && x.PaymentMethodCode != "CK") > 0)
                //{
                //    IsCash = "1";
                //}


                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_Full_Tax.rdlc");
                _ReportParameter = new ReportParameter[] { 
                    new ReportParameter("Header", HeaderDesc),
                    new ReportParameter("Footer", FooterDesc),
                    new ReportParameter("FairDesc", FairDesc),
                    new ReportParameter("PaxCount", PaxCount),
                    new ReportParameter("PassengerName", PassengerNames),
                    new ReportParameter("PayType", PayType),
                    new ReportParameter("Vat", Vat),
                    new ReportParameter("IsCash", IsCash),
                    new ReportParameter("IsCheque", IsCheque),
                    new ReportParameter("IsTransfer", IsTransfer),
                    new ReportParameter("BankTransfer", BankTransfer),
                    new ReportParameter("AccountNo", AccountNo),
                    new ReportParameter("BankCheque", BankCheque),
                    new ReportParameter("Branch", Branch),
                    new ReportParameter("ChequeNo", ChequeNo),
                    new ReportParameter("ChequeDate", ChequeDate),
                    new ReportParameter("ABBNo", _Invoice_Info.ABB_NO.Replace(",","<br/>")),
                    new ReportParameter("FarePrice", FarePrice),
                };
            }
            else
            {
                foreach (var item in Invoice_Payments)
                {
                    var ABB_3_Model = new ABB_3_Model();
                    ABB_3_Model.pay_type = "ชำระโดย " + item.PaymentType_Name;
                    ABB_3_Model.tax_type = item.CurrencyCode.ToUpper() != "THB" ? "(Exchange Rate " + item.ExchangeRateTH.ToString("#,#0.000000") + " THB : 1 " + item.CurrencyCode + ")" : string.Empty;
                    ABB_3_Model.pay_amt = item.CurrencyCode + " " + item.PaymentAmount_Ori.ToString("#,#0.00");
                    ABB_3_Models.Add(ABB_3_Model);
                }

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_FullTaxSlip.rdlc");
                _ReportParameter = new ReportParameter[] { 
                    new ReportParameter("Header", HeaderDesc),
                    new ReportParameter("Footer", FooterDesc),
                    new ReportParameter("FairDesc", FairDesc),
                    new ReportParameter("PayType", PayType),
                    new ReportParameter("PaxCount", PaxCount),
                    new ReportParameter("PassengerName", PassengerNames),
                    new ReportParameter("ABBNo", _Invoice_Info.ABB_NO.Replace(",","<br/>")),
                    new ReportParameter("Vat", Vat),
                    new ReportParameter("PNR_No", _Invoice_Info.Booking_No),
                };
            }

            ReportViewer1.LocalReport.SetParameters(_ReportParameter);
            

            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DS_FULL_TAX";
            reportDataSource1.Value = model;

            ReportDataSource reportDataSource2 = new ReportDataSource();
            reportDataSource2.Name = "DS_PAY";
            reportDataSource2.Value = ABB_3_Models;
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource2);

            ReportDataSource reportDataSource3 = new ReportDataSource();
            reportDataSource3.Name = "DS_SERVICE";
            reportDataSource3.Value = ListFlightFeeService;
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource3);

            ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            ReportViewer1.LocalReport.Refresh();

            #endregion
         

        }
    }
}