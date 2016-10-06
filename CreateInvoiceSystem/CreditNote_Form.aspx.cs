using CreateInvoiceSystem.DAO;
using CreateInvoiceSystem.Model;
using CreateInvoiceSystem.Reports;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class CreditNote_Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DoPreview();

            }
        }

        private void DoPreview()
        {

            //string CN_No = "CN0716-00001";

            if (Request.QueryString["inv_no"] != null)
            {
                string inv_no = Request.QueryString["inv_no"].ToString();

                var context = new POSINVEntities();

                

                DataTable dt = InvoiceDA.GetBookingByInv_No(inv_no);

                string inv_id = dt.Rows[0]["INV_ID"].ToString();
                string CN_ID = dt.Rows[0]["CN_ID"].ToString();
                string Booking_No = dt.Rows[0]["Booking_No"].ToString();

                string POS_CODE = Context.Request.Cookies["authenuser"].Values["POS_CODE"];


                DataReports ds = InvoiceCNDA.GetInvoiceCN_Report(CN_ID, Booking_No);

                string flight = "";

                Guid INV_ID = Guid.Parse(inv_id);

                T_Invoice_Info _Invoice_Info = context.T_Invoice_Info.Where(it => it.INV_ID == INV_ID).First();

                var ListFlightFeeService = new List<ABB_2_Model>();

                decimal amount = _Invoice_Info.TOTAL - (_Invoice_Info.VAT + _Invoice_Info.AIRPORTTAX);

                decimal Fair = amount - (_Invoice_Info.Service + _Invoice_Info.Fuel + _Invoice_Info.INSURANCE + _Invoice_Info.ADMINFEE + _Invoice_Info.OTHER);

                var FareDescription = string.Empty;
                var PaxCount = (_Invoice_Info.PAXCountADT + _Invoice_Info.PAXCountCHD).ToString("#,##0");
                var PassengerNames = "&nbsp;" + _Invoice_Info.PassengerName.Replace(",", "<br/>&nbsp;");

                var FarePrice = "<br/>0.00";
                var Vat = "0";
                var fares = _Invoice_Info.T_Invoice_Flight.OrderBy(f => f.SEQ).ToList();
                if (fares != null && fares.Count() > 0)
                {
                    var FareList = fares.Select(x => x.CarrierCode + x.FlightNumber + " " + x.DepartureStation + "-" + x.ArrivalStation +
                                                     "<br/>&nbsp;" + x.DepartureDate.ToString("dd/MM/yyyy HH:mm") + " - " + x.ArraivalDate.ToString("dd/MM/yyyy HH:mm"))
                                        .ToList();
                    FareDescription = "&nbsp;" + String.Join("<br/>&nbsp;", FareList);

                    var BaseFare = fares.Select(x => x.T_Invoice_Fare.Where(s => s.ChargeCode == "").Sum(c => c.TotalTH)).ToList();
                    FarePrice = "<br/>" + String.Join("<br/><br/>", BaseFare.Select(x => x.ToString("#,#0.00")).ToList());

                    if (fares.Count(x => x.FlightNumber.Length == 3) == 0)
                    {
                        Vat = "7";
                    }
                }

                var FlightDescription = String.Format("ค่าตั๋วโดยสารและค่าธรรมเนียมอื่นๆ<br/>{0}<br/>PASSENGER &nbsp;{1} PAX<br/>{2}", FareDescription, PaxCount, PassengerNames);

                //ListFlightFeeService.Add(new ABB_2_Model()
                //{
                //    pay_desc = FlightDescription,
                //    pay_amt = Fair.ToString("#,#0.00")
                //});

                if (_Invoice_Info.Fuel != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าน้ำมัน/Fuel Sercharg",
                        pay_amt = _Invoice_Info.Fuel.ToString("#,#0.00")
                    });
                }
                if (_Invoice_Info.Service != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าบริการ/Service",
                        pay_amt = _Invoice_Info.Service.ToString("#,#0.00")
                    });
                }
                if (_Invoice_Info.INSURANCE != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าประกัน/Insurance",
                        pay_amt = _Invoice_Info.INSURANCE.ToString("#,#0.00")
                    });
                }
                if (_Invoice_Info.ADMINFEE != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "ค่าบริการ/Admin Fee",
                        pay_amt = _Invoice_Info.ADMINFEE.ToString("#,#0.00")
                    });
                }
                if (_Invoice_Info.OTHER != 0)
                {
                    ListFlightFeeService.Add(new ABB_2_Model()
                    {
                        pay_desc = "อื่นๆ/Other",
                        pay_amt = _Invoice_Info.OTHER.ToString("#,#0.00")
                    });
                }

                

                //var fares = context.T_Invoice_Flight.Where(f => f.INV_ID == INV_ID).OrderBy(f => f.SEQ).ToList();
                //if (fares != null)
                //{
                //    foreach (var itm in fares)
                //    {
                //        flight += itm.CarrierCode + itm.FlightNumber + " " + itm.DepartureStation + "-" + itm.ArrivalStation;
                //    }
                //    ds.DT_CreditNote_Form[0].DESC = flight;

                //}

                DataReports.DT_CreditNoteDT_FormRow dr = ds.DT_CreditNoteDT_Form.NewDT_CreditNoteDT_FormRow();
                dr.Seq = 1;
                dr.Detail = flight;
                dr.Qty = 1;
                dr.Price = ds.DT_CreditNote_Form[0].SubTotal;
                dr.Amount = ds.DT_CreditNote_Form[0].SubTotal;
                ds.DT_CreditNoteDT_Form.AddDT_CreditNoteDT_FormRow(dr);

                ReportDataSource reportDataSource1 = new ReportDataSource();
                reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DT_CreditNote_Form";
                reportDataSource1.Value = ds.DT_CreditNote_Form;

                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "DT_CreditNoteDT_Form";
                reportDataSource2.Value = ds.DT_CreditNoteDT_Form;

                ReportDataSource reportDataSource3 = new ReportDataSource();
                reportDataSource3 = new ReportDataSource();
                reportDataSource3.Name = "DT_FlightService";
                reportDataSource3.Value = ListFlightFeeService;
                

                var Header = MasterDA.GetMessageList(MasterDA.SlipType.CN, true, POS_CODE);
                var HeaderDesc = String.Join("<br/><br/>", Header.MessageModels.Select(x => x.Descriptions));

                var Footer = MasterDA.GetMessageList(MasterDA.SlipType.CN, false, POS_CODE);
                var FooterDesc = String.Join("<br/><br/>", Footer.MessageModels.Select(x => x.Descriptions));

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_CreditNote_I.rdlc");
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.SizeToReportContent = true;

                var p = new ReportParameter[] { 
                new ReportParameter("Header", HeaderDesc),
                new ReportParameter("Footer", FooterDesc),
                new ReportParameter("Vat", Vat),
                new ReportParameter("FlightDescription", FlightDescription),
                new ReportParameter("Fair", FarePrice)
                    //new ReportParameter("REP",str.objAbb1.REP == null ? "0.00" : str.objAbb1.REP),
                };
                ReportViewer1.LocalReport.SetParameters(p);
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                ReportViewer1.LocalReport.DataSources.Add(reportDataSource2);
                ReportViewer1.LocalReport.DataSources.Add(reportDataSource3);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}