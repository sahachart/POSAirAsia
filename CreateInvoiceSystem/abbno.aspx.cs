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
    public partial class abbno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.Request["id"] != null)
                {
                    //GetAbb();
                    preview_form();
                }
            }
        }

//       void GetAbb()
//        {
//            Guid _guid = new Guid(this.Request["id"].ToString());
//            using (var context = new POSINVEntities())
//            {
//                try
//                {
//                    var found = context.T_ABB.FirstOrDefault(f => f.ABBTid == _guid
//                        && f.STATUS_INV != "CANCEL");

//                    if (found != null)
//                    {
//                        this.Label2.Text = found.PNR_No;
//                        this.Label4.Text = (found.PAXCountADT + found.PAXCountCHD).ToString();
//                        this.Label15.Text = found.TaxInvoiceNo;

//                        var paxs = context.T_ABBPAX.Where(f => f.ABBTid == _guid).ToList();

//                        if (paxs != null)
//                        {
//                            foreach (T_ABBPAX itm in paxs)
//                            {
//                                this.Label6.Text += itm.FirstName.ToString().ToUpper() + " " + itm.LastName.ToString().ToUpper() + "<br>";
//                            }
//                        }

//                        var fares = context.T_ABBFARE.Where(f => f.ABBTid == _guid).OrderBy(f => f.SEQ).ToList();
//                        string sfare = "";
//                        if (fares != null)
//                        {
//                            foreach (T_ABBFARE itm in fares)
//                            {
//                                string ss = "";
//                                if (itm.STD.ToString("yyyyMMdd") == itm.STA.ToString("yyyyMMdd"))
//                                {
//                                    ss = itm.STD.ToString("dd/MM/yyyy") + "(" + itm.STD.ToString("HH:mm") + "-" + itm.STA.ToString("HH:mm") + ")";
//                                }
//                                else
//                                {
//                                    ss = itm.STD.ToString("dd/MM/yyyy(HH:mm)") + "-" + itm.STA.ToString("dd/MM/yyyy(HH:mm)");
//                                }
//                                sfare += @"<tr>
//                                                <td>ค่าธรรมเนียมโดยสาร/Base Fare</td>
//                                                <td style=""text-align: right"">" + itm.Amount.ToString("#,#0.00") + @"</td>
//                                            </tr>
//                                            <tr>
//                                                <td colspan=""2"">" + itm.CarrierCode + itm.FlightNumber + " " + itm.DepartureStation + "-" + itm.ArrivalStation + " " + ss + @"</td>
//                                            </tr>";
                                
//                            }

                            
//                        }

//                        if (found.Fuel != 0)
//                        {
//                            sfare += @"<tr>
//                                                <td>ค่าน้ำมัน/Fuel Sercharg</td>
//                                                <td style=""text-align: right"">" + found.Fuel.ToString("#,#0.00") + @"</td>
//                                            </tr>";
//                        }
//                        if (found.Service != 0)
//                        {
//                            sfare += @"<tr>
//                                                <td>ค่าบริการ/Service</td>
//                                                <td style=""text-align: right"">" + found.Service.ToString("#,#0.00") + @"</td>
//                                            </tr>";
//                        }
//                        if (found.INSURANCE != 0)
//                        {
//                            sfare += @"<tr>
//                                                <td>ค่าประกัน/Insurance</td>
//                                                <td style=""text-align: right"">" + found.INSURANCE.ToString("#,#0.00") + @"</td>
//                                            </tr>";
//                        }
//                        if (found.ADMINFEE != 0)
//                        {
//                            sfare += @"<tr>
//                                                <td>ค่าบริการ/Admin Fee</td>
//                                                <td style=""text-align: right"">" + found.ADMINFEE.ToString("#,#0.00") + @"</td>
//                                            </tr>";
//                        }
//                        if (found.OTHER != 0)
//                        {
//                            sfare += @"<tr>
//                                                <td>อื่นๆ/Other</td>
//                                                <td style=""text-align: right"">" + found.OTHER.ToString("#,#0.00") + @"</td>
//                                            </tr>";
//                        }

//                        this.Label7.Text = sfare;

//                        this.Label9.Text = found.VAT.ToString("#,#0.00");
//                        this.Label11.Text = found.AIRPORTTAX.ToString("#,#0.00");
//                        this.Label13.Text = found.TOTAL.ToString("#,#0.00");

//                        var methodpay = context.T_ABBPAYMENTMETHOD.Where(f => f.ABBTid == _guid).OrderBy(f => f.SEQ).ToList();
//                        string spay = "";
//                        if (fares != null)
//                        {
//                            foreach (T_ABBPAYMENTMETHOD itm in methodpay)
//                            {
//                                spay += @"<tr>
//                                                <td style=""padding-left:25px;"">" + itm.PaymentType_Name + @"</td>
//                                                <td style=""text-align: right;padding-right:25px;"">" + itm.CurrencyCode + " " + itm.PaymentAmount.ToString("#,#0.00") + @"</td>
//                                            </tr>";
//                            }
//                        }

//                        this.Label16.Text = spay;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    this.Response.Write(ex.Message);
//                }
//            }
//        }

        private void preview_form()
        {
            string POS_CODE = Context.Request.Cookies["authenuser"].Values["POS_CODE"];
            var str = getdetail();

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            var Header = MasterDA.GetMessageList(MasterDA.SlipType.ABB, true, POS_CODE);
            if (Header.IsA4)
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_ABB_Full.rdlc");
                var p = new ReportParameter[] { 
                    new ReportParameter("PNR_No",str.objAbb1.PNR_No),
                    new ReportParameter("PAXCount",str.objAbb1.PAXCount),
                    //new ReportParameter("TaxInvoiceNo",header),
                    new ReportParameter("passenger_name",str.objAbb1.passenger_name.Replace("<br/>",", ")),
                    new ReportParameter("VAT",str.objAbb1.VAT),
                    new ReportParameter("AIRPORTTAX",str.objAbb1.AIRPORTTAX),
                    new ReportParameter("TOTAL",str.objAbb1.TOTAL),
                    new ReportParameter("printdate",str.objAbb1.print_date),
                    new ReportParameter("printtime",str.objAbb1.print_time),
                    new ReportParameter("printno",str.objAbb1.print_no == null ? "-": str.objAbb1.print_no ),
                    new ReportParameter("printreason",str.objAbb1.print_reason == null ? "-": str.objAbb1.print_reason),
                    new ReportParameter("Header",str.objAbb1.header),
                    new ReportParameter("Footer",str.objAbb1.footer),
                    new ReportParameter("REP",str.objAbb1.REP == null ? "0.00" : str.objAbb1.REP),
                    new ReportParameter("PID",str.objAbb1.pid),
                    new ReportParameter("User",str.objAbb1.userid),
                    new ReportParameter("POS",str.objAbb1.pos),
                    new ReportParameter("ABBNo",str.objAbb1.TaxInvoiceNo),
                    new ReportParameter("BookingDate",str.objAbb1.booking_date),
                };

                ReportViewer1.LocalReport.SetParameters(p);
            }
            else
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_ABB.rdlc");
                
                string header = str.objAbb1.header + "<br/>TAX INVOICE(ABB) TAX ID" + "<br/>" +
                    "PID: " + str.objAbb1.pid + "<br/>" + "User: " +
                    str.objAbb1.userid + "&nbsp;&nbsp; POS: " + str.objAbb1.pos + "<br/>" +
                    str.objAbb1.booking_date + "<br/>BNO: " + str.objAbb1.TaxInvoiceNo;
                var p = new ReportParameter[] { 
                    new ReportParameter("PNR_No",str.objAbb1.PNR_No),
                    new ReportParameter("PAXCount",str.objAbb1.PAXCount),
                    new ReportParameter("TaxInvoiceNo",header),
                    new ReportParameter("passenger_name",str.objAbb1.passenger_name),
                    new ReportParameter("VAT",str.objAbb1.VAT),
                    new ReportParameter("AIRPORTTAX",str.objAbb1.AIRPORTTAX),
                    new ReportParameter("TOTAL",str.objAbb1.TOTAL),
                    new ReportParameter("printdate",str.objAbb1.print_date),
                    new ReportParameter("printtime",str.objAbb1.print_time),
                    new ReportParameter("printno",str.objAbb1.print_no == null ? "-": str.objAbb1.print_no ),
                    new ReportParameter("printreason",str.objAbb1.print_reason == null ? "-": str.objAbb1.print_reason),
                    new ReportParameter("footer",str.objAbb1.footer),
                    new ReportParameter("REP",str.objAbb1.REP == null ? "0.00" : str.objAbb1.REP),
                    new ReportParameter("CashierName",str.objAbb1.username),
                };

                ReportViewer1.LocalReport.SetParameters(p);
            }
            
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource1 = new ReportDataSource();
            ReportDataSource reportDataSource2 = new ReportDataSource();
            ReportDataSource reportDataSource3 = new ReportDataSource();

            if (str.oList_3 != null)
            {
                //reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DS_PAY";
                reportDataSource1.Value = str.oList_3;
            }
            if (str.oList_2 != null)
            {
                //reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "DS_FEE";
                reportDataSource2.Value = str.oList_2;
            }
            if (str.servicelist != null)
            {
                //reportDataSource2 = new ReportDataSource();
                reportDataSource3.Name = "DS_SERVICE";
                reportDataSource3.Value = str.servicelist;
            }
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            ReportViewer1.LocalReport.Refresh();

        }

        protected ListValueModel getdetail()
        {
            ListValueModel val = new ListValueModel();
            Guid _guid = new Guid(this.Request["id"].ToString());
            ABB_1_Model obj1 = new ABB_1_Model();
            
            ABB_5_Model obj5 = new ABB_5_Model();
            List<ABB_5_Model> olist_5 = new List<ABB_5_Model>();
            
            ABB_3_Model obj3 = new ABB_3_Model();
            List<ABB_3_Model> olist_3 = new List<ABB_3_Model>();
            
            ABB_2_Model obj2 = new ABB_2_Model();
            List<ABB_2_Model> olist_2 = new List<ABB_2_Model>();

            ABB_2_Model objservice = new ABB_2_Model();
            List<ABB_2_Model> oslist = new List<ABB_2_Model>();

            string userid = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            string POS_CODE = Context.Request.Cookies["authenuser"].Values["POS_CODE"];

            obj1.userid = userid;
            var user_obj = MasterDA.GetUserDetail(userid, POS_CODE);
            string pid = user_obj.POS_MacNo;
            obj1.pid = pid;
            obj1.username = user_obj.FirstName + " " + user_obj.LastName;
            string posno = user_obj.POS_Code;
            obj1.pos = posno;
            var msgh_obj = MasterDA.GetMessageList(MasterDA.SlipType.ABB, true, POS_CODE);
            string header = @"";
            if (msgh_obj != null)
            {
                int ih = 0;
                foreach (var item in msgh_obj.MessageModels)
                {
                    header += ih == 0 ? item.Descriptions : @"<br/>" + item.Descriptions;
                    ih++;
                }
            }
            obj1.header = header;

            var msgd_obj = MasterDA.GetMessageList(MasterDA.SlipType.ABB, false, POS_CODE);
            string footer = @"";
            if (msgd_obj != null)
            {
                int ih = 0;
                foreach (var item in msgd_obj.MessageModels)
                {
                    footer += ih == 0 ? item.Descriptions : @"<br/>" + item.Descriptions;
                    ih++;
                }
            }
            obj1.footer = footer;
            
            using (var context = new POSINVEntities())
            {
                try
                {
                    MessageResponse msg = new MessageResponse();
                    msg = MasterDA.printcount_update(_guid);
                    var found = context.T_ABB.FirstOrDefault(f => f.ABBTid == _guid
                        && f.STATUS_INV != "CANCEL");

                    if (found != null)
                    {
                        obj1.PNR_No = found.PNR_No;
                        obj1.PAXCount = (found.PAXCountADT + found.PAXCountCHD).ToString();
                        obj1.TaxInvoiceNo = found.TaxInvoiceNo;
                        obj1.print_no = found.PrintCount.ToString();
                        var obj_booing = MasterDA.GetTBooking(found.PNR_No);
                        obj1.booking_date = obj_booing.Booking_Date.ToString("dd/MM/yyyy");
                        var paxs = context.T_ABBPAX.Where(f => f.ABBTid == _guid).ToList();
                        if (paxs != null)
                        {
                            string name ="";
                            int iname = 0;
                            foreach (T_ABBPAX itm in paxs)
                            {
                                name += (iname > 0 ? "<br/>":"") + itm.FirstName.ToString().ToUpper() + " " + itm.LastName.ToString().ToUpper();
                                iname++;
                            }
                            obj1.passenger_name = name;
                        }
                        var fares = context.T_ABBFARE.Where(f => f.ABBTid == _guid).OrderBy(f => f.SEQ).ToList();
                        if (fares != null)
                        {
                            foreach (T_ABBFARE itm in fares)
                            {
                                string ss = "";
                                if (itm.STD.ToString("yyyyMMdd") == itm.STA.ToString("yyyyMMdd"))
                                {
                                    ss = itm.STD.ToString("dd/MM/yyyy") + "(" + itm.STD.ToString("HH:mm") + "-" + itm.STA.ToString("HH:mm") + ")";
                                }
                                else
                                {
                                    ss = itm.STD.ToString("dd/MM/yyyy(HH:mm)") + "-" + itm.STA.ToString("dd/MM/yyyy(HH:mm)");
                                }
                                obj2 = new ABB_2_Model();
                                obj2.pay_desc = "ค่าธรรมเนียมโดยสาร/Base Fare";
                                obj2.pay_amt = itm.Amount.ToString("#,#0.00");
                                obj2.flight_desc = itm.CarrierCode + itm.FlightNumber + " " + itm.DepartureStation + "-" + itm.ArrivalStation + " " + ss;
                                olist_2.Add(obj2);
                            }
                        }

                        if (found.Fuel != 0)
                        {
                            objservice = new ABB_2_Model();
                            objservice.pay_desc = "ค่าน้ำมัน/Fuel Sercharg";
                            objservice.pay_amt = found.Fuel.ToString("#,#0.00");
                            oslist.Add(objservice);
                        }
                        if (found.Service != 0)
                        {
                            objservice = new ABB_2_Model();
                            objservice.pay_desc = "ค่าบริการ/Service";
                            objservice.pay_amt = found.Service.ToString("#,#0.00");
                            oslist.Add(objservice);
                        }
                        if (found.INSURANCE != 0)
                        {
                            objservice = new ABB_2_Model();
                            objservice.pay_desc = "ค่าประกัน/Insurance";
                            objservice.pay_amt = found.INSURANCE.ToString("#,#0.00");
                            oslist.Add(objservice);
                        }
                        if (found.ADMINFEE != 0)
                        {
                            objservice = new ABB_2_Model();
                            objservice.pay_desc = "ค่าบริการ/Admin Fee";
                            objservice.pay_amt = found.ADMINFEE.ToString("#,#0.00");
                            oslist.Add(objservice);
                        }
                        if (found.OTHER != 0)
                        {
                            objservice = new ABB_2_Model();
                            objservice.pay_desc = "อื่นๆ/Other";
                            objservice.pay_amt = found.OTHER.ToString("#,#0.00");
                            oslist.Add(objservice);
                        }
                        obj1.VAT = found.VAT.ToString("#,#0.00");
                        obj1.AIRPORTTAX = found.AIRPORTTAX.ToString("#,#0.00");
                        obj1.TOTAL = found.TOTAL.ToString("#,#0.00");
                        obj1.REP = found.REP.ToString("#,#0.00");

                        var methodpay = context.T_ABBPAYMENTMETHOD.Where(f => f.ABBTid == _guid).OrderBy(f => f.SEQ).ToList();
                        if (fares != null)
                        {
                            foreach (T_ABBPAYMENTMETHOD itm in methodpay)
                            {
                                obj3 = new ABB_3_Model();
                                obj3.pay_type = itm.PaymentType_Name;
                                obj3.pay_amt = itm.CurrencyCode + " " + itm.PaymentAmount_Ori.ToString("#,#0.00");
                                obj3.tax_type = itm.CurrencyCode.ToUpper() != "THB" ? "(Exchange Rate " + itm.ExchangeRateTH.ToString("#,#0.000000") + " THB : 1 " + itm.CurrencyCode + ")" : string.Empty;
                                olist_3.Add(obj3);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Response.Write(ex.Message);
                }
            }
            string print_date = DateTimeOffset.Now.ToString("dd/MM/yyyy");
            string print_time = DateTimeOffset.Now.ToString("HH:mm:ss");
            obj1.print_date = print_date;
            obj1.print_time = print_time;

            val.objAbb1 = obj1;
            val.oList_3 = olist_3;
            val.oList_2 = olist_2;
            val.servicelist = oslist;
            return val;
        }
    }
}