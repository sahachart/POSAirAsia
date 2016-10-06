using CreateInvoiceSystem.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class bookinginfo : BasePage
    {
        public MasterDA _MasterDA = new MasterDA();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tbx_booking_date.ReadOnly = true;
            }
            this.btn_CreateAbb.Enabled = false;
        }

        protected void btn_Seaarch_Click(object sender, EventArgs e)
        {
            MasterDA DA = new MasterDA();
            var res = DA.GetBooking(this.tbx_booking_no.Text);
            if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");

                res_flight = DA.GetFlightList(res.TransactionId);

                res_passenger = DA.GetPassengerList(res.TransactionId);

                res_fare = MasterDA.GetFareDetail(res.PNR_No);

                res_fee = MasterDA.GetFeeDetail(res.PNR_No);

                res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);

                res_abb = DA.GetABBList(res.PNR_No);
                //GetLabelLinkList();

                
            }
        }
        public List<T_SegmentCurrentUpdate> res_flight { get; set; }
        public List<T_PassengerCurrentUpdate> res_passenger { get; set; }
        public List<FeeDetail> res_fee { get; set; }
        public List<FeeDetail> res_fare { get; set; }
        public List<T_PaymentCurrentUpdate> res_payment { get; set; }
        public List<T_ABB> res_abb { get; set; }
        public string TableABB_Invoice()
        {
            string htmlStr = "";

            if (this.tbx_booking_no.Text != "")
            {
                var ListABBs = _MasterDA.GetABBList(this.tbx_booking_no.Text);

                foreach (var ABB in ListABBs)
                {
                    var ABBNo = String.Format(@"<a target='' href='#' onclick=""popup('{0}');return false;"">{1}</a>", ABB.ABBTid, ABB.TaxInvoiceNo);
                    var ABBCheckBox = string.Empty;
                    var INV_No = string.Empty;
                    var ORbtn = "-";

                    if (ABB.T_Invoice_Info != null)
                    {
                        ABBCheckBox = String.Format(@"<input type='checkbox' name='chk_ABB' value='{0}' style='transform: scale(1.5);' disabled />", ABB.ABBTid);
                        INV_No = String.Format(@"<a target='' href='#' onclick=""print_Invoice('{0}');return false;"">{0}</a>", ABB.T_Invoice_Info.INV_No);

                    }
                    else
                    {
                        ABBCheckBox = String.Format(@"<input type='checkbox' name='chk_ABB' value='{0}' style='transform: scale(1.5);' checked />", ABB.TaxInvoiceNo);
                        INV_No = "-";
                        ORbtn = String.Format(@"<img type='image' src='Images/meanicons_24-20.png' style='height:24px;width:24px;' onclick=""ORABB('{0}');"" />", ABB.TaxInvoiceNo);
                    }
                    
                    htmlStr +=
                        "<tr><td style='text-align: center;'>" + ABBCheckBox + @"</td>
                        <td>" + ABBNo + @"</td>
                        <td>" + MasterDA.GetPaymentFromABBNo(ABB.ABBTid) + @"</td>
                        <td style='text-align: center;'>" + ORbtn + @"</td>
                        <td>" + INV_No + @"</td>
                        </tr>";
                }
            }

            return htmlStr;
        }
        public string TableFlight()
        {
            string htmlStr ="";
            if (res_flight != null)
	        {
                foreach (var item in res_flight)
                {
                    string ss = "";
                    if (item.STD.ToString("yyyyMMdd") == item.STA.ToString("yyyyMMdd"))
                    {
                        ss = item.STD.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        ss = item.STD.ToString("dd/MM/yyyy") + "-" + item.STA.ToString("dd/MM/yyyy");
                    }
                    htmlStr +=
                        "<tr><td>" + item.CarrierCode+item.FlightNumber + @"</td>
                        <td>" + ss + @"</td>
                        <td>" + item.DepartureStation + @"</td>
                        <td>" + item.ArrivalStation + @"</td>
                        <td>" + item.STD.ToString("HH:mm") + @"</td>
                        <td>" + item.STA.ToString("HH:mm") + @"</td>
                        <td>" + item.ABBNo + @"</td>
                        </tr>";
                }
	        }
            return htmlStr;
        }
        public string TablePassenger()
        {
            string htmlStr = "";
            int count = 0;
            if (res_flight != null)
            {
                foreach (var item in res_passenger)
                {
                    htmlStr +=
                        @"<tr>
                        <td>" + (count+1).ToString() + @"</td>
                        <td>" + item.FirstName + " " + item.LastName + @"</td><td>" + item.PaxType + @"</td>
                        <td>" + item.ABBNo + @"</td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }

        public string TableFare()
        {
            string htmlStr = "";
            decimal sum = 0;
            if (res_fare != null)
            {
                foreach (FeeDetail item in res_fare.OrderBy(o => o.FareCreateDate))
                {
                    htmlStr +=
                        @"<tr>
                        <td style='text-align: center;'>" + item.PaxType + @"</td>
                        <td style='text-align: center;'>" + item.ChargeCode + @"</td>
                        <td style='text-align: center;'>" + item.FareCreateDate.ToString("dd-MM-yyyy") + @"</td>
                        <td style='text-align: center;'>" + item.qty.ToString("#,##0") + @"</td>
                        <td style='text-align: center;'>" + item.CurrencyCode + @"</td>
                        <td style='text-align: right;'>" + (item.Amount??0).ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" + (item.Exc?? 0).ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" +( item.Discount?? 0).ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" + item.AmountTHB.ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" + item.TotalAmountTHB.ToString("#,##0.00") + @"</td>
                        <td style='text-align: center;'>" + item.AbbGroup + @"</td>
                        <td style='text-align: center;'>" + item.TaxInvoiceNo    + @"</td>
                        </tr>";
                }
                sum = (decimal)res_fare.Sum(it => it.TotalAmountTHB);
            }


            lblTatalFare.Text = sum.ToString("#,###,##0.00");

            return htmlStr;
        }


        public string TableFee()
        {
            string htmlStr = "";
            int count = 0;
            decimal sum = 0;
            decimal sum_ori = 0;
            if (res_fee != null)
            {
                foreach (var item in res_fee.OrderBy(o => o.FeeCreateDate))
                {
                    htmlStr +=
                        @"<tr>
                        <td nowrap>" + item.FirstName + " " + item.LastName + @"</td>
                        <td style='text-align: center;'>" + item.ChargeCode + @"</td>
                        <td style='text-align: center;'>" + item.FeeCreateDate.ToString("dd-MM-yyyy") + @"</td>
                        <td style='text-align: center;'>" + item.qty.ToString("#,##0") + @"</td>
                        <td style='text-align: center;'>" + item.CurrencyCode + @"</td>
                        <td style='text-align: right;'>" + item.Exc.GetValueOrDefault().ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" + item.Amount.GetValueOrDefault().ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" + item.Discount.GetValueOrDefault().ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" + item.AmountTHB.ToString("#,##0.00") + @"</td>
                        <td style='text-align: center;'>" + item.AbbGroup + @"</td>
                        <td style='text-align: center;'>" + item.TaxInvoiceNo + @"</td>
                        </tr>";
                }
                sum = (decimal)res_fee.Sum(it => it.AmountTHB);
                sum_ori = (decimal)res_fee.Sum(it => it.Amount);
            }


            lblTotal.Text = sum.ToString("#,###,##0.00");
            lblOriFeeTotal.Text = sum_ori.ToString("#,###,##0.00");

            if (res_fee != null && res_fare != null)
            {
                decimal sum_fare = (decimal)res_fare.Sum(it => it.TotalAmountTHB);
                lblTotalFareFee.Text = (sum_fare + sum).ToString("#,###,##0.00");
            }

            return htmlStr;
        }
        public string TablePayment()
        {
            string htmlStr = "";
            int count = 0;
            decimal sumPayment  = 0;
            decimal sumPaymentTHB = 0;
            if (res_fee != null)
            {
                foreach (var item in res_payment)
                {
                    htmlStr +=
                        @"<tr>
                        <td style='text-align: center;'>" + item.AgentCode + @"</td>
                        <td style='text-align: center;'>" + item.ApprovalDate.ToString("dd/MM/yyyy") + @"</td>
                        <td style='text-align: center;'>" + item.PaymentMethodCode + @"</td>
                        <td style='text-align: center;'>" + item.CurrencyCode + @"</td>
                        <td style='text-align: right;'>" + item.PaymentAmount.ToString("#,##0.00") + @"</td>
                        <td style='text-align: right;'>" + item.PaymentAmountTHB.ToString("#,##0.00") + @"</td>
                        <td style='text-align: center;'>" + item.ABBNo + @"</td>
                        </tr>";
                    count++;
                }

                sumPayment = res_payment.Sum(x => x.PaymentAmount);
                sumPaymentTHB = res_payment.Sum(x => x.PaymentAmountTHB);

                
            }

            lbl_PaymentAll.Text = sumPayment.ToString("#,###,##0.00");
            lbl_PaymentAllTHB.Text = sumPaymentTHB.ToString("#,###,##0.00");

            return htmlStr;
        }
        public string GetLabelLinkList()
        {
            var ListABBs = _MasterDA.GetABBList(this.tbx_booking_no.Text);

            if (MasterDA.ValidNullABB(this.tbx_booking_no.Text))
            {
                this.btn_CreateAbb.Enabled = true;
            }
            else {
                this.btn_CreateAbb.Enabled = false;
            }

            if (ListABBs.Count(x => x.INV_ID == null) > 0)
            {
                this.btn_CreateInvoice.Enabled = true;
            }
            else
            {
                this.btn_CreateInvoice.Enabled = false;
            }

            return string.Empty;
        }

        public string GetInvoiceLink()
        {
            string htmlStr = "";

            if (tbx_booking_no.Text != "")
            {
                using (var context = new POSINVEntities())
                {
                    var Invoices = context.T_Invoice_Info.Where(x => x.Booking_No == tbx_booking_no.Text).Select(s => s.INV_No).ToList();

                }
            }

            return htmlStr;
        }

        protected void imgGetDataDB_Click(object sender, ImageClickEventArgs e)
        {
            this.errortext.Text = "";
            if (this.tbx_booking_no.Text =="")
            {
                return;
            }
            MasterDA DA = new MasterDA();
            var res = DA.GetBooking(this.tbx_booking_no.Text);
            if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");
                res_flight = DA.GetFlightList(res.TransactionId);
                res_passenger = DA.GetPassengerList(res.TransactionId);
                res_fare = MasterDA.GetFareDetail(res.PNR_No);
                res_fee = MasterDA.GetFeeDetail(res.PNR_No);
                res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);
                res_abb = DA.GetABBList(res.PNR_No);
                GetLabelLinkList();

                this.Label3.Text = "Data From DataBase";
                
            }
            else
            {
                imgGetDataOnline_Click(null, null);
                //this.errortext.Text = "ไม่พบข้อมูลในฐานข้อมูล กรุณาคลิกปุ่ม Get Data Online";
            }
        }

        protected void imgClearData_Click(object sender, ImageClickEventArgs e)
        {
            //MasterDA DA = new MasterDA();
            //var res = DA.GetBooking(this.tbx_booking_no.Text);
            this.tbx_booking_no.Text="";
            this.tbx_booking_date.Text = "";
            this.Label3.Text = "";
            this.errortext.Text = "";
            res_flight = null;
            res_passenger = null;
            res_fee = null;
            res_payment = null;
            res_abb = null;
            GetLabelLinkList();
            
        }

        protected void imgGetDataOnline_Click(object sender, ImageClickEventArgs e)
        {
            bool IsGetOnline = CreateABB.UpdateDataOnline(tbx_booking_no.Text, CookiesMenager.EmpID);
            MasterDA DA = new MasterDA();
            var res = DA.GetBooking(this.tbx_booking_no.Text);
            if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");

                res_flight = DA.GetFlightList(res.TransactionId);

                res_passenger = DA.GetPassengerList(res.TransactionId);

                res_fare = MasterDA.GetFareDetail(res.PNR_No);
                res_fee = MasterDA.GetFeeDetail(res.PNR_No);

                res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);

                res_abb = DA.GetABBList(res.PNR_No);

                GetLabelLinkList();

            }

            if (IsGetOnline)
                Label3.Text = "Data From SkySpeed";
            

            return;
            //if (this.tbx_booking_no.Text == "")
            //{
            //    return;
            //}
            //this.errortext.Text = "";
     
            //DAO.GetDataSkySpeed getdata = new GetDataSkySpeed();
            //com.airasia.acebooking.Booking bookingdata = getdata.GetData(this.tbx_booking_no.Text);


            //if (bookingdata != null && bookingdata.BookingID != 0)
            //{
            //    MasterDA DA = new MasterDA();
            //    string PNR_No = tbx_booking_no.Text;
            //    var res = DA.GetBooking(PNR_No);
            //    if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            //    {

            //        // Check Devide
            //        bool IsDevide = bookingdata.Payments.Where(it => it.PaymentAmount < 0).ToList().Count > 0;
            //        if (IsDevide)
            //        {
            //            // Devide Payment ติดลบ ลบออก Gen ใหม่
            //            bool success = DBHelper.DeleteAllByPNR_No(PNR_No);
            //            getdata.LoadToDB(bookingdata, CookiesMenager.EmpID);
            //            string Meg = CreateABB.OnGenABB(tbx_booking_no.Text, CookiesMenager.EmpID, CookiesMenager.POS_Code);
            //            if (Meg == string.Empty)
            //            {
            //                imgGetDataDB_Click(null, null);
            //                this.Label3.Text = "Data From SkySpeed";
            //            }
            //            else
            //            {
            //                this.errortext.Text = "ไม่สามารถออกใบ ABB ได้ <br>" + Meg + " <br>กรุณาติดต่อแผนกการเงิน";
            //            }
            //            return;
            //        }


            //        bool hasnew = getdata.LoadToDBNoneExist(res.TransactionId, bookingdata, Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToUpper());

            //        if (hasnew)
            //        {
            //            this.Label3.Text = "Data From SkySpeed";
            //        }

            //        res = DA.GetBooking(this.tbx_booking_no.Text);
            //        if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            //        {
            //            this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");

            //            res_flight = DA.GetFlightList(res.TransactionId);

            //            res_passenger = DA.GetPassengerList(res.TransactionId);

            //            res_fee = UpdateBookingDA.GetServiceCharge(res.PNR_No);

            //            res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);

            //            res_abb = DA.GetABBList(res.PNR_No);

            //            GetLabelLinkList();

            //        }
            //    }
            //    else
            //    {

            //        getdata.LoadToDB(bookingdata, Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToUpper());

            //        res = DA.GetBooking(this.tbx_booking_no.Text);
            //        if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            //        {
            //            this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");

            //            res_flight = DA.GetFlightList(res.TransactionId);

            //            res_passenger = DA.GetPassengerList(res.TransactionId);

            //            res_fee =  UpdateBookingDA.GetServiceCharge(res.PNR_No);

            //            res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);

            //            res_abb = DA.GetABBList(res.PNR_No);

            //            GetLabelLinkList();

            //            this.Label3.Text = "Data From SkySpeed";
            //        }
            //        #region
            //        //var pay = bookingdata.Payments.Where(x => x.Status == com.airasia.acebooking.BookingPaymentStatus.Approved).ToList();
            //        //if (pay.Count() == 0)
            //        //{
            //        //    this.errortext.Text = "ไม่สามารถออก Invoice ได้ รายการชำระเงินไม่อนุมัติ";

            //        //}
            //        //else
            //        //{
            //        //    bool geninvoice = true;
            //        //    using (var context = new POSINVEntities())
            //        //    {
            //        //        foreach (com.airasia.acebooking.Payment itmpay in pay)
            //        //        {
            //        //            var agent = context.M_Agency.Where(x => x.Agency_Code == itmpay.PointOfSale.AgentCode).ToList();
            //        //            if (agent != null)
            //        //            {
            //        //                if (agent[0].IsActive == "N" || agent[0].GenInvoice == "N")
            //        //                {
            //        //                    geninvoice = false;
            //        //                    break;
            //        //                }
            //        //            }
            //        //            else
            //        //            {
            //        //                geninvoice = false;
            //        //                break;
            //        //            }
            //        //        }
            //        //    }

            //        //    if (geninvoice == false)
            //        //    {
            //        //        this.errortext.Text = "AgentCode ไม่สามารถออก Invoice ได้";
            //        //    }
            //        //    else
            //        //    { 

            //        //    }
            //        //}
            //        #endregion
            //    }
            //}
            //else
            //{
            //    //errortext.Text = "ไม่พบ Booking No.";
            //    if (bookingdata.ExceptionMessage != string.Empty)
            //        errortext.Text = bookingdata.ExceptionMessage;
            //}
            
        }

        protected void btn_CreateAbb_Click(object sender, EventArgs e)
        {
            string Meg = string.Empty;
            //gen
            errortext.Text = "";

            if (CookiesMenager.EmpID == string.Empty)
            {
                this.errortext.Text = "ไม่พบ User Code ";
                return;
            }


            if(this.txt_CreateABBMode.Text == "")
            {
                var context = new POSINVEntities();

                if (context.T_ABB.Count(x => x.PNR_No == tbx_booking_no.Text && x.STATUS_INV == "ACTIVE") > 0)
                {
                    var CanOR = (context.T_ABB.Count(x => x.PNR_No == tbx_booking_no.Text && x.STATUS_INV == "ACTIVE" && x.PrintCount > 0) == 0) &&
                            (context.T_Invoice_Info.Count(x => x.Booking_No == tbx_booking_no.Text) == 0);
                    if (CanOR)
                    {
                        var ABBNos = String.Join(",", context.T_ABB.Where(x => x.PNR_No == tbx_booking_no.Text && x.STATUS_INV == "ACTIVE").Select(s => s.TaxInvoiceNo).ToList());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CreateABBMode", "ORCreateABB('" + ABBNos + "')", true);
                        imgGetDataDB_Click(null, null);
                        return;
                    }
                }
            }
            else if (this.txt_CreateABBMode.Text == "OR")
            {
                CreateABB.ClearABB(tbx_booking_no.Text);
            }

            Meg = CreateABB.OnGenABB(tbx_booking_no.Text, CookiesMenager.EmpID, CookiesMenager.POS_Code);

            if (Meg == string.Empty)
            {
                imgGetDataDB_Click(null, null);
            }
            else
            {
                this.errortext.Text = "ไม่สามารถออกใบ ABB ได้ <br>" + Meg + " <br>กรุณาติดต่อแผนกการเงิน";
            }

            this.txt_CreateABBMode.Text = "";

            //this.errortext.Text = "";
            //string msg = MasterDA.CreateABB(this.tbx_booking_no.Text, Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToUpper(),
            //    Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToUpper(), Context.Request.Cookies["authenuser"].Values["Station_Code"].ToUpper());

            //if (msg != "")
            //{
            //    this.errortext.Text = "ไม่สามารถออกใบ ABB ได้ เนื่องจากข้อมูลไม่ถูกต้อง <br>กรุณาติดต่อแผนกการเงิน";
            //}

            //MasterDA DA = new MasterDA();
            //var res = DA.GetBooking(this.tbx_booking_no.Text);
            //if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            //{
            //    this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");

            //    res_flight = DA.GetFlightList(res.TransactionId);

            //    res_passenger = DA.GetPassengerList(res.TransactionId);

            //    res_fee = MasterDA.GetFeeDetail(res.PNR_No);

            //    res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);

            //    res_abb = DA.GetABBList(res.PNR_No);

            //    GetLabelLinkList();

            //}
        }

        
    }
}