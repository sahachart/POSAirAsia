using CreateInvoiceSystem.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class updatebooking : BasePage
    {
        //
        public T_BookingCurrentUpdate res_booking { get; set; }
        public List<T_SegmentCurrentUpdate> res_flight { get; set; }
        public List<T_PassengerCurrentUpdate> res_passenger { get; set; }

        public List<FeeDetail> res_fare { get; set; }
        public List<FeeDetail> res_fee { get; set; }
        public List<FeeDetail> res_fare_sky { get; set; }
        public List<FeeDetail> res_fee_sky { get; set; }
        public List<T_PaymentCurrentUpdate> res_payment { get; set; }
        public List<T_PaymentCurrentUpdate> res_payment_sky { get; set; }
        public List<T_ABB> res_abb { get; set; }

        private com.airasia.acebooking.Booking bookingdata;

        protected void InitData()
        {
            this.errortext.Text = "";
            if (this.tbx_booking_no.Text == "")
            {
                return;
            }
            //Session["bookingdata"] = null;
            //Session["res_fee_sky"] = null;
            //bookingdata = null;
            //res_fee_sky = null;
            //rptFeeSky.DataSource = null;
            //rptFeeSky.DataBind();
            //lblTotalFeeSky.Text = "0.00";

            MasterDA DA = new MasterDA();
            var res = DA.GetBooking(this.tbx_booking_no.Text);
            Session["res_booking"] = res;
            if (res != null && res.TransactionId.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                this.transactionid.Value = res.TransactionId.ToString();
                this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");
                hidfBookingID.Value = res.BookingId.ToString();
                res_flight = DA.GetFlightList(res.TransactionId);

                res_passenger = DA.GetPassengerList(res.TransactionId);

                res_fee = MasterDA.GetFeeDetail(res.PNR_No);
                res_fare = MasterDA.GetFareDetail(res.PNR_No);

                //res_fee = UpdateBookingDA.GetServiceCharge(res.PNR_No);

                res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);

                res_abb = DA.GetABBList(res.PNR_No);

                Session["res_flight"] = res_flight;

                Session["res_passenger"] = res_passenger;

                Session["res_fare"] = res_fare;

                Session["res_fee"] = res_fee;

                Session["res_payment"] = res_payment;

                Session["res_abb"] = res_abb;

                GetLabelLinkList();


                rptFlightDetail.DataSource = res_flight;
                rptFlightDetail.DataBind();

                PasssengerBinding();

                #region Fare & Fee

                rptFare.DataSource = res_fare;
                rptFare.DataBind();

                rptFee.DataSource = res_fee;
                rptFee.DataBind();
                
                #endregion

                BindingTotal();

                rptPayment.DataSource = res_payment;
                rptPayment.DataBind();

                var sum = (decimal)res_payment.Sum(it => it.PaymentAmountTHB);
                lblTotalPay.Text = sum.ToString("#,###,##0.00");

                //lblTotal.Text
                ddlSegment.Items.Clear();
                for (int i = 0; i < res_flight.Count; i++) {
                    var PaxFares = MasterDA.GetPaxFareBySegmentId(res_flight[i].SegmentTId);
                    for (int x = 0; x < PaxFares.Count; x++)
                    {
                        var Description = String.Format("{0} {1} - {2}", res_flight[i].CarrierCode, res_flight[i].FlightNumber, PaxFares[x].PaxType);
                        ddlSegment.Items.Add(new ListItem(Description, PaxFares[x].PaxFareTId.ToString().ToLower()));
                    }
                }

                ddlFarePassenger.Items.Clear();
                for (int i = 0; i < res_passenger.Count; i++)
                    ddlFarePassenger.Items.Add(new ListItem(res_passenger[i].FirstName + " " + res_passenger[i].LastName, res_passenger[i].PassengerId.ToString()));


                ddlFlightAbbNo.Items.Clear();
                ddlPassABBNo.Items.Clear();
                ddlFareAbbNo.Items.Clear();
                ddlFeeAbbNo.Items.Clear();
                ddlPayAbbNo.Items.Clear();
                ddlFlightAbbNo.Items.Add(new ListItem("New ABB", ""));
                ddlPassABBNo.Items.Add(new ListItem("New ABB", ""));
                ddlFareAbbNo.Items.Add(new ListItem("New ABB", ""));
                ddlFeeAbbNo.Items.Add(new ListItem("New ABB", ""));
                ddlPayAbbNo.Items.Add(new ListItem("New ABB", ""));
                foreach (T_ABB item in res_abb)
                {
                    ddlFlightAbbNo.Items.Add(new ListItem(item.TaxInvoiceNo, item.TaxInvoiceNo));
                    ddlPassABBNo.Items.Add(new ListItem(item.TaxInvoiceNo, item.TaxInvoiceNo));
                    ddlFareAbbNo.Items.Add(new ListItem(item.TaxInvoiceNo, item.TaxInvoiceNo));
                    ddlFeeAbbNo.Items.Add(new ListItem(item.TaxInvoiceNo, item.TaxInvoiceNo));
                    ddlPayAbbNo.Items.Add(new ListItem(item.TaxInvoiceNo, item.TaxInvoiceNo));
                }
                
            }

        }

        private void DoClearData()
        {
            tbx_booking_no.Text = "";
            tbx_booking_date.Text = "";


            bookingdata = null;
            res_flight = new List<T_SegmentCurrentUpdate>();
            res_passenger = new List<T_PassengerCurrentUpdate>();
            res_fare = new List<FeeDetail>();
            res_fee = new List<FeeDetail>();
            res_payment = new List<T_PaymentCurrentUpdate>();
            res_abb = new List<T_ABB>();

            res_fare_sky = new List<FeeDetail>();
            res_fee_sky = new List<FeeDetail>();
            res_payment_sky = new List<T_PaymentCurrentUpdate>();

            Session["bookingdata"] = bookingdata;
            Session["res_flight"] = res_flight;
            Session["res_passenger"] = res_passenger;
            Session["res_fare"] = res_fare;
            Session["res_fee"] = res_fee;
            Session["res_payment"] = res_payment;
            Session["res_abb"] = res_abb;
            Session["res_fare_sky"] = res_fare_sky;
            Session["res_fee_sky"] = res_fee_sky;
            Session["res_payment_sky"] = res_payment_sky;


            rptFlightDetail.DataSource = null;
            rptFlightDetail.DataBind();

            rptPassenger.DataSource = null;
            rptPassenger.DataBind();

            rptFare.DataSource = null;
            rptFare.DataBind();

            rptFee.DataSource = null;
            rptFee.DataBind();

            rptPayment.DataSource = null;
            rptPayment.DataBind();

            rptPayment.DataSource = null;
            rptPayment.DataBind();

            rpt_SkyFare.DataSource = null;
            rpt_SkyFare.DataBind();

            rptFeeSky.DataSource = null;
            rptFeeSky.DataBind();

            lbl_TotalFareSkyAmountOri.Text = "0";
            lbl_TotalFareSkyTH.Text = "0";
            lbl_TotalFareSkyAmountTH.Text = "0";
            lblTotalFeeSky.Text = "0";
            lblTotal.Text = "0";
            lblTotalPay.Text = "0";
            lblTotalPaySky.Text = "0";

            GetLabelLinkList();
        }

        #region Private Method

        private void PasssengerBinding()
        {
            rptPassenger.DataSource = res_passenger;
            rptPassenger.DataBind();

            ddlFeePassName.Items.Clear();
            for (int i = 0; i < res_passenger.Count; i++)
            {
                ddlFeePassName.Items.Add(new ListItem(res_passenger[i].FirstName + " " + res_passenger[i].LastName, res_passenger[i].PassengerId.ToString()));
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hidfTranID_New.Value = Guid.NewGuid().ToString();
                this.transactionid.Value = Guid.NewGuid().ToString();
                res_flight = new List<T_SegmentCurrentUpdate>();
                res_passenger = new List<T_PassengerCurrentUpdate>();
                res_fare = new List<FeeDetail>();
                res_fee = new List<FeeDetail>();
                res_payment = new List<T_PaymentCurrentUpdate>();
                res_abb = new List<T_ABB>();
                res_fare_sky = new List<FeeDetail>();
                res_fee_sky = new List<FeeDetail>();
                res_payment_sky = new List<T_PaymentCurrentUpdate>();
                //bookingdata = new com.airasia.acebooking.Booking();
                Session["res_booking"] = res_booking;
                Session["res_flight"] = res_flight;
                Session["res_passenger"] = res_passenger;
                Session["res_fare"] = res_fare;
                Session["res_fee"] = res_fee;
                Session["res_fare_sky"] = res_fare_sky;
                Session["res_fee_sky"] = res_fee_sky;
                Session["res_payment"] = res_payment;
                Session["res_abb"] = res_abb;
                Session["bookingdata"] = null;
                tbx_booking_date.ReadOnly = true;
                InitControl();
            }
            else
            {
                res_booking = (T_BookingCurrentUpdate)Session["res_booking"];
                res_flight = (List<T_SegmentCurrentUpdate>)Session["res_flight"];
                res_passenger = (List<T_PassengerCurrentUpdate>)Session["res_passenger"];
                res_fare = (List<FeeDetail>)Session["res_fare"];
                res_fee = (List<FeeDetail>)Session["res_fee"];
                res_fare_sky = (List<FeeDetail>)Session["res_fare_sky"];
                res_fee_sky = (List<FeeDetail>)Session["res_fee_sky"];
                res_payment = (List<T_PaymentCurrentUpdate>)Session["res_payment"];
                res_payment_sky = (List<T_PaymentCurrentUpdate>)Session["res_payment_sky"];
                res_abb = (List<T_ABB>)Session["res_abb"];
                bookingdata = (com.airasia.acebooking.Booking)Session["bookingdata"];
                GetLabelLinkList();

                

            }
        }

        private void InitControl()
        {
            List<M_FlightFee> lstFlightFee = MasterDA.GetFlightFee();
            for (int i = 0; i < lstFlightFee.Count; i++)
            {
                ddlFareChargeCode.Items.Add(new ListItem(lstFlightFee[i].Flight_Fee_Code, lstFlightFee[i].Flight_Fee_Code));
                //ddlFareTicket.Items.Add(new ListItem(lstFlightFee[i].Flight_Fee_Code, lstFlightFee[i].Flight_Fee_Code));


                ddlFeeChargeCode.Items.Add(new ListItem(lstFlightFee[i].Flight_Fee_Code, lstFlightFee[i].Flight_Fee_Code));
                //ddlFeeTicCode.Items.Add(new ListItem(lstFlightFee[i].Flight_Fee_Code, lstFlightFee[i].Flight_Fee_Code));
            }

            hidfCurrentDate.Value = DateTime.Today.ToString("dd/MM/yyyy");
        }

        //cancel
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DoClearData();
            //cancel
            //InitData();
            //bookingdata = null;
        }
        //search
        protected void imgDatabaseLoad_Click(object sender, ImageClickEventArgs e)
        {
            //search
            var PNR_No = tbx_booking_no.Text;
            DoClearData();
            tbx_booking_no.Text = PNR_No;
            InitData();
        }
        //reset
        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            DoClearData();
        }
        //gen
        protected void btnCreateABB_Click(object sender, EventArgs e)
        {
            string Meg = string.Empty;
            //gen
            errortext.Text = "";

            Meg = CreateABB.OnGenABB(tbx_booking_no.Text, CookiesMenager.EmpID, CookiesMenager.POS_Code);

            if (Meg == string.Empty)
            {
                InitData();
            }
            else
            {
                this.errortext.Text = "ไม่สามารถออกใบ ABB ได้ <br>" + Meg + " <br>กรุณาติดต่อแผนกการเงิน";
            }

            #region Old


            //string msg = MasterDA.CreateABB(this.tbx_booking_no.Text, Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToUpper(),
            //    Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToUpper(), Context.Request.Cookies["authenuser"].Values["Station_Code"].ToUpper());

            //if (msg != "")
            //{
            //    this.errortext.Text = "ไม่สามารถออกใบ ABB ได้ <br>" + msg + " <br>กรุณาติดต่อแผนกการเงิน";
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
            #endregion
        }

        //ws
        protected void imgSkyspeedLoad_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (this.tbx_booking_no.Text != string.Empty)
                {
                    MasterDA DA = new MasterDA();
                    DAO.GetDataSkySpeed getdata = new GetDataSkySpeed();
                    bookingdata = getdata.GetData(this.tbx_booking_no.Text);
                    if (bookingdata != null && bookingdata.BookingID != 0)
                    {
                        var res = DA.GetBooking(this.tbx_booking_no.Text);
                        if (res != null && res.TransactionId.ToString() == "00000000-0000-0000-0000-000000000000")
                        {
                            getdata.LoadToDB(bookingdata, Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToUpper());
                            InitData();
                        }

                        this.errortext.Text = "";


                        Session["bookingdata"] = bookingdata;

                        res_fare_sky = GetFareSky();
                        Session["res_fare_sky"] = res_fare_sky;
                        rpt_SkyFare.DataSource = res_fare_sky;
                        rpt_SkyFare.DataBind();

                        decimal TotalFareSkyAmountOri = (decimal)res_fare_sky.Sum(it => it.Amount.GetValueOrDefault());
                        decimal TotalFareSkyAmountTH = (decimal)res_fare_sky.Sum(it => it.AmountTHB);
                        decimal TotalFareSkyTH = (decimal)res_fare_sky.Sum(it => it.TotalAmountTHB);
                        lbl_TotalFareSkyAmountOri.Text = TotalFareSkyAmountOri.ToString("#,###,##0.00");
                        lbl_TotalFareSkyAmountTH.Text = TotalFareSkyAmountTH.ToString("#,###,##0.00");
                        lbl_TotalFareSkyTH.Text = TotalFareSkyTH.ToString("#,###,##0.00");



                        res_fee_sky = GetFeeSky();
                        int i = 0;
                        res_fee_sky.ForEach(it => it.RowNo = i++);
                        Session["res_fee_sky"] = res_fee_sky;
                        rptFeeSky.DataSource = res_fee_sky;
                        rptFeeSky.DataBind();

                        decimal sum = (decimal)res_fee_sky.Sum(it => it.AmountTHB);
                        lblTotalFeeSky.Text = sum.ToString("#,###,##0.00");


                        res_payment_sky = GetPaymentSky();
                        Session["res_payment_sky"] = res_payment_sky;
                        sum = (decimal)res_payment_sky.Sum(it => it.PaymentAmountTHB);
                        lblTotalPaySky.Text = sum.ToString("#,###,##0.00");
                    }
                    else
                    {
                        errortext.Text = "ไม่พบ Booking No.";
                    }

                }
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                errortext.Text = "ไม่พบ Booking No.";
            }
        }

        public string TableABB_Invoice()
        {
            string htmlStr = "";

            if (this.tbx_booking_no.Text != "")
            {
                MasterDA da = new MasterDA();
                var ListABBs = da.GetABBList(this.tbx_booking_no.Text);

                foreach (var ABB in ListABBs)
                {
                    var ABBNo = String.Format(@"<a target='' href='#' onclick=""popup('{0}');return false;"">{1}</a>", ABB.ABBTid, ABB.TaxInvoiceNo);
                    var ABBCheckBox = string.Empty;
                    var INV_No = string.Empty;
                    var ORbtn = "-";
                    string Button = "<input  type='button' value='OR' text='OR' class='btn btn-block' disabled />";

                    if (ABB.T_Invoice_Info != null)
                    {
                        ABBCheckBox = String.Format(@"<input type='checkbox' name='chk_ABB' value='{0}' style='transform: scale(1.5);' disabled />", ABB.ABBTid);
                        INV_No = String.Format(@"<a target='' href='#' onclick=""print_Invoice('{0}');return false;"">{0}</a>", ABB.T_Invoice_Info.INV_No);
                    }
                    else
                    {
                        ABBCheckBox = String.Format(@"<input type='checkbox' name='chk_ABB' value='{0}' style='transform: scale(1.5);' checked />", ABB.TaxInvoiceNo);
                        INV_No = "-";
                        Button = "<input  type='button' value='OR' text='OR' class='btn  btn-default' onclick='ConfirmOR(\"" + ABB.TaxInvoiceNo + "\")' />";
                        ORbtn = String.Format(@"<img type='image' src='Images/meanicons_24-20.png' style='height:24px;width:24px;' onclick=""ConfirmOR('{0}');"" />", ABB.TaxInvoiceNo);
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
            string htmlStr = "";
            if (res_flight != null)
            {
                int count = 0;
                foreach (var item in res_flight)
                {
                    string strEvent = count + ",'" + item.SegmentTId + "','" + item.CarrierCode + "','" + item.FlightNumber + "','" + item.DepartureStation + "','" + item.ArrivalStation + "','" + item.STD.ToString("dd/MM/yyyy") + "','" + item.STA.ToString("dd/MM/yyyy") + "','" + item.STD.ToString("HH:mm") + "','" + item.STA.ToString("HH:mm") + "'";
                    htmlStr +=
                        @"<tr><td><img src='Images/Text-Edit-icon.png' onclick=""editflight(" + strEvent + ");\" Style='cursor: pointer; width: 18px;' /></td><td style='display:none;'>" + item.ABBNo + @"</td><td>" + item.CarrierCode + item.FlightNumber + @"</td>
                        <td>" + item.STD.ToString("dd/MM/yyyy") + @"</td>
                        <td>" + item.DepartureStation + @"</td>
                        <td>" + item.ArrivalStation + @"</td>
                        <td>" + item.STD.ToString("HH:mm") + @"</td>
                        <td>" + item.STA.ToString("HH:mm") + @"</td>
                        <td><img onclick=""$('#MainContent_fare2').val(" + count + @");$('#btnDelFlight').click();"" src='Images/meanicons_24-20.png' Style='cursor: pointer; width: 18px;' /></td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }

        #region OnItemDataBound Event


        protected void rptFlightDetail_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                T_SegmentCurrentUpdate item = (T_SegmentCurrentUpdate)e.Item.DataItem;
                Image imgAction = (Image)e.Item.FindControl("imgAction");
                Image imgDel = (Image)e.Item.FindControl("imgDel");

                Label lblFlightNo = (Label)e.Item.FindControl("lblFlightNo");
                Label lblFlightDate = (Label)e.Item.FindControl("lblFlightDate");
                Label lblFrom = (Label)e.Item.FindControl("lblFrom");
                Label lblTo = (Label)e.Item.FindControl("lblTo");
                Label lblFromTime = (Label)e.Item.FindControl("lblFromTime");
                Label lblArriveTime = (Label)e.Item.FindControl("lblArriveTime");
                Label lblFlightAbbNo = (Label)e.Item.FindControl("lblFlightAbbNo");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                lblFlightNo.Text = item.CarrierCode + item.FlightNumber;
                lblFlightDate.Text = item.STD.ToString("dd/MM/yyyy");
                lblFrom.Text = item.DepartureStation;
                lblTo.Text = item.ArrivalStation;
                lblFromTime.Text = item.STD.ToString("HH:mm");
                lblArriveTime.Text = item.STA.ToString("HH:mm");
                lblFlightAbbNo.Text = item.ABBNo;
                imgAction.ImageUrl = "Images/Text-Edit-icon.png";

                if (item.RowState == 1)
                    imgAction.ImageUrl = "Images/add.png";
                else if (item.RowState == 2)
                    imgAction.ImageUrl = "Images/Text-Edit-icon.png";
                else if (item.RowState == -1)
                    tr.Style.Add("display", "none");


                item.STD_Date = item.STD.ToString("dd/MM/yyyy");
                item.STA_Date = item.STA.ToString("dd/MM/yyyy");
                item.STD_Time = item.STD.ToString("HH:mm");
                item.STA_Time = item.STA.ToString("HH:mm");

                string data = JsonConvert.SerializeObject(item);
                data = data.Replace("\"", "&quot;");
                string strEvent = "\"Edit\",\"" + tr.ClientID + "\",\"" + data+"\"";
               // string strEvent = "'Edit','" + tr.ClientID + "','" + item.RowNo + "','" + item.SegmentTId + "','" + item.CarrierCode + "','" + item.FlightNumber + "','" + item.DepartureStation + "','" + item.ArrivalStation + "','" + item.STD.ToString("dd/MM/yyyy") + "','" + item.STA.ToString("dd/MM/yyyy") + "','" + item.STD.ToString("HH:mm") + "','" + item.STA.ToString("HH:mm") + "','" + item.ABBNo + "'";
                imgAction.Attributes.Add("onclick", "editflight(" + strEvent + ");");
                imgDel.Attributes.Add("onclick", "$('#hidfDelFlight').val('" + item.SegmentTId + "');$('#btnDelFlight').click();");
            }
        }

        protected void rptPassenger_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                T_PassengerCurrentUpdate item = (T_PassengerCurrentUpdate)e.Item.DataItem;
                Image imgAction = (Image)e.Item.FindControl("imgAction");
                Image imgDel = (Image)e.Item.FindControl("imgDel");

                Label lblNo = (Label)e.Item.FindControl("lblNo");
                Label lblPassName = (Label)e.Item.FindControl("lblPassName");
                Label lblPassAbbNo = (Label)e.Item.FindControl("lblPassAbbNo");
                lblNo.Text = item.RowNo.ToString();
                lblPassName.Text = item.FirstName + " " + item.LastName;
                lblPassAbbNo.Text = item.ABBNo;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");
                
                imgAction.ImageUrl = "Images/Text-Edit-icon.png";

                if (item.RowState == 1)
                {
                    imgAction.ImageUrl = "Images/add.png";
                }
                else if (item.RowState == 2)
                {
                    imgAction.ImageUrl = "Images/Text-Edit-icon.png";
                }
                else if (item.RowState == -1)
                    tr.Style.Add("display", "none");

                string strEvent = "'EDIT','" + tr.ClientID + "','" + item.RowNo + "','" + item.PassengerTId + "','" + item.PassengerId + "','" + item.Title + "','" + item.FirstName + "','" + item.LastName + "','" + item.PaxType + "','" + item.ABBNo + "'";
                imgAction.Attributes.Add("onclick", "editpassenger(" + strEvent + ");");
                imgDel.Attributes.Add("onclick", "$('#hidfDelPass').val('" + item.PassengerTId + "');$('#btnDelPass').click();");
            }
        }

        protected void rptPayment_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                T_PaymentCurrentUpdate item = (T_PaymentCurrentUpdate)e.Item.DataItem;
                Image imgAction = (Image)e.Item.FindControl("imgAction");
                Image imgDel = (Image)e.Item.FindControl("imgDel");

                Label lblPaymentAgent = (Label)e.Item.FindControl("lblPaymentAgent");
                Label lblPaymentDate = (Label)e.Item.FindControl("lblPaymentDate");
                Label lblPaymentType = (Label)e.Item.FindControl("lblPaymentType");
                Label lblCurrency = (Label)e.Item.FindControl("lblCurrency");
                Label lblPaymentAmount = (Label)e.Item.FindControl("lblPaymentAmount");
                Label lblPaymentAmountTHB = (Label)e.Item.FindControl("lblPaymentAmountTHB");
                Label lblPayAbbNo = (Label)e.Item.FindControl("lblPayAbbNo");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                lblPaymentAgent.Text = item.AgentCode;
                lblPaymentDate.Text = item.ApprovalDate.ToString("dd/MM/yyyy");
                lblPaymentType.Text = item.PaymentMethodType;
                lblCurrency.Text = item.CurrencyCode;
                lblPaymentAmount.Text = item.PaymentAmount.ToString("#,###,##0.00");
                lblPaymentAmountTHB.Text = item.PaymentAmountTHB.ToString("#,###,##0.00");
                lblPayAbbNo.Text = item.ABBNo;
                imgAction.ImageUrl = "Images/Text-Edit-icon.png";
                if (item.RowState == 1)
                    imgAction.ImageUrl = "Images/add.png";
                else if (item.RowState == 2)
                    imgAction.ImageUrl = "Images/Text-Edit-icon.png";
                else if (item.RowState == -1)
                    tr.Style.Add("display", "none");

                string strEvent = "'EDIT','" + tr.ClientID + "','" + item.RowNo + "','" + item.PaymentTId + "','" + item.PaymentID + "','" + item.PaymentMethodCode + "','" + item.PaymentMethodType + "','" + item.AgentCode + "','" + item.ApprovalDate.ToString("dd/MM/yyyy") + "','" + item.CurrencyCode + "','" + item.PaymentAmount.ToString("0.00") + "','" + item.CollectedCurrencyCode + "','" + item.CollectedAmount.ToString("0.00") + "','" + item.ABBNo + "'";
                imgAction.Attributes.Add("onclick", "editpayment(" + strEvent + ");");
                imgDel.Attributes.Add("onclick", "$('#hidfDelPay').val('" + item.PaymentTId + "');$('#btnDelPay').click();");
            }
        }


        protected void rptFee_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FeeDetail item = (FeeDetail)e.Item.DataItem;
                Image imgAction = (Image)e.Item.FindControl("imgAction");
                Image imgDel = (Image)e.Item.FindControl("imgDel");

                Label lblDetail = (Label)e.Item.FindControl("lblDetail");
                Label lblQty = (Label)e.Item.FindControl("lblQty");
                Label lblPrice = (Label)e.Item.FindControl("lblPrice");
                Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");
                Label lblEx = (Label)e.Item.FindControl("lblEx");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                Label lblFeePassName = (Label)e.Item.FindControl("lblFeePassName");
                Label lblFeeABB_No = (Label)e.Item.FindControl("lblFeeABB_No");
                Label lbl_FeeCreateDate = (Label)e.Item.FindControl("lbl_FeeCreateDate");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                lblFeePassName.Text = item.FirstName + " " + item.LastName;
                lblDetail.Text = item.ChargeDetail;
                lblQty.Text = item.qty.ToString("0");
                lblFeeABB_No.Text = item.TaxInvoiceNo;
                lblEx.Text = (item.Exc ?? 1).ToString("0.####");
                lbl_FeeCreateDate.Text = item.FeeCreateDate.ToString("dd-MM-yyyy");
                if (item.ChargeType.ToLower() == "discount" || item.ChargeType.ToLower() == "promotiondiscount")
                {
                    lblPrice.Text = "0";
                    lblDiscount.Text = (item.Discount ?? 0).ToString("#,###,##0.00");
                }
                else
                {
                    lblPrice.Text = (item.Amount ?? 0).ToString("#,###,##0.00");
                    lblDiscount.Text = "0";
                }

                lblAmount.Text = item.AmountTHB.ToString("#,###,##0.00");
                imgAction.ImageUrl = "Images/Text-Edit-icon.png";


                if (item.RowState == 1)
                    imgAction.ImageUrl = "Images/add.png";
                else if (item.RowState == -1)
                    tr.Style.Add("display", "none");
                //(mode,trid, index, ServiceChargeTId, SegmentTId, PassengerId, ChargeType, ChargeCode, ChargeDetail, TicketCode, CollectType, CurrencyCode, Amount, ForeignCurrencyCode, ForeignAmount, abb_no)
                var strEvent = String.Format("'EDIT','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}'",
                    tr.ClientID, item.RowNo, item.RowID, item.SegmentTId, 
                    item.PassengerId, item.ChargeType, item.ChargeCode, item.ChargeDetail, 
                    item.TicketCode, item.CollectType, item.CurrencyCode, ((decimal)item.Amount).ToString("0.00"),
                    item.ForeignCurrencyCode, item.ForeignAmount.ToString("0.00"), item.TaxInvoiceNo
                    );
                string xstrEvent = "'EDIT','" + tr.ClientID + "','" + item.RowNo + "','" + item.RowID + "','" + item.SegmentTId + "','" + item.PassengerId + "','" + item.ChargeType + "','" + item.ChargeCode + "','" + item.ChargeDetail + "','" + item.TicketCode + "','" + item.CollectType + "','" + item.CurrencyCode + "','" + ((decimal)item.Amount).ToString("0.00") + "','" + item.ForeignCurrencyCode + "','" + item.ForeignAmount.ToString("0.00") + "','" + item.TaxInvoiceNo + "'";

                imgAction.Attributes.Add("onclick", "editfee(" + strEvent + ");");
                imgDel.Attributes.Add("onclick", "$('#hidfDelFee').val('" + item.RowID + "');$('#btnDelFee').click();");

            }
        }


        protected void rptFeeSky_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FeeDetail item = (FeeDetail)e.Item.DataItem;
                Image imgAction = (Image)e.Item.FindControl("imgAction");
                Image imgDel = (Image)e.Item.FindControl("imgDel");

                Label lblPassenger = (Label)e.Item.FindControl("lblPassenger");
                Label lblDetail = (Label)e.Item.FindControl("lblDetail");
                Label lblQty = (Label)e.Item.FindControl("lblQty");
                Label lblPrice = (Label)e.Item.FindControl("lblPrice");
                Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");
                lblPassenger.Text = item.FirstName + " " + item.LastName;
                lblDetail.Text = item.ChargeDetail;
                lblQty.Text = item.qty.ToString("0");
                lblPrice.Text = (item.price ?? 0).ToString("#,###,##0.00"); ;
                lblDiscount.Text = (item.Discount ?? 0).ToString("#,###,##0.00");
                lblAmount.Text = item.AmountTHB.ToString("#,###,##0.00");

                imgAction.Attributes.Add("onclick", " $('#hidfFeeSkyUpdate').val('" + item.index + "|" + item.RowNo + "');  $('#btnFeeSkyUpdate').click();");

            }
        }

        #endregion

        public string TablePassenger()
        {
            string htmlStr = "";
            int count = 0;
            if (res_flight != null)
            {
                foreach (var item in res_passenger)
                {
                    string strdel = "";
                    //if (item.ABBNo == null || item.ABBNo == "")
                    //{
                    strdel = @"<img onclick=""$('#MainContent_delpass').val(" + count + @");$('#MainContent_Button7').click();"" src='Images/meanicons_24-20.png' Style='cursor: pointer; width: 18px;' />";
                    //}
                    htmlStr +=
                        @"<tr><td><img src='Images/Text-Edit-icon.png' Style='cursor: pointer; width: 18px;' onclick=""editpassenger(" + count +",'" +item.PassengerId+"','" + item.Title + "','" + item.FirstName+"','"+ item.LastName+"','"+item.PaxType + @"');"" /></td>
                        <td>" + (count + 1).ToString() + @"</td>
                        <td style='display:none;'>" + item.ABBNo + @"</td>
                        <td style='display:none;'>" + item.PassengerId.Value.ToString() + @"</td>
                        <td>" + item.FirstName + " " + item.LastName + @"</td><td>" + item.PaxType + @"</td>
                        <td style='display:none;'>" + strdel + @"</td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }
        public string TableFee()
        {
            string htmlStr = "";
            int count = 0;
            if (res_fee != null)
            {
                foreach (var item in res_fee)
                {
                    htmlStr +=
                        @"<tr>
                        <td><img src='Images/Text-Edit-icon.png' Style='cursor: pointer; width: 18px;' onclick=""editfee(" + count + @");"" /></td>
                        <td style='display:none;'>" + item.TaxInvoiceNo + @"</td>
                        <td>" + item.Header + @"</td>
                        <td style='display:none;'>" + item.ChargeCode + @"</td>
                        <td>" + item.qty.ToString("#,##0") + @"</td>
                        <td style='display:none;'>" + item.CurrencyCode + @"</td>
                        <td style='display:none;'>" + item.Amount.Value.ToString("#,##0.00") + @"</td>
                        <td style='display:none;'>" + item.DiscountOri.Value.ToString("#,##0.00") + @"</td>
                        <td style='display:none;'>" + item.Exc.Value.ToString("#,##0.00") + @"</td>
                        <td>" + item.price.Value.ToString("#,##0.00") + @"</td>
                        <td>" + item.Discount.Value.ToString("#,##0.00") + @"</td>
                        <td>" + item.amt.Value.ToString("#,##0.00") + @"</td>
                        <td style='display:none;'>" + item.AbbGroup + @"</td>
                        <td><img onclick=""$('#MainContent_delfee').val(" + count + @");$('#MainContent_Button9').click();"" src='Images/meanicons_24-20.png' Style='cursor: pointer; width: 18px;' /></td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }
        public string TablePayment()
        {
            string htmlStr = "";
            int count = 0;
            if (res_fee != null)
            {
                string param = string.Empty;
                foreach (var item in res_payment)
                {
                    string strdel = "", stredit = "";
                    param = count + ",'" + item.PaymentTId + "','" + item.PaymentMethodCode + "','" + item.PaymentMethodType+ "','" + item.AgentCode + "','" + item.ApprovalDate + "','" + item.CurrencyCode + "','" + item.PaymentAmount + "','" + item.CollectedCurrencyCode + "','" + item.CollectedAmount + "'";
                    //if (item.ABBNo == null || item.ABBNo == "")
                    //{
                    stredit = @"<img src='Images/Text-Edit-icon.png' Style='cursor: pointer; width: 18px;'  onclick=""editpayment(" + param + @");""  />";
                        strdel = @"<img onclick=""$('#MainContent_delpay').val(" + count + @");$('#MainContent_delpaybtn').click();"" src='Images/meanicons_24-20.png' Style='cursor: pointer; width: 18px;' />";
                    //}
                    htmlStr +=
                        @"<tr>
                        <td>" + stredit + @"</td>
                        <td style='display:none;'>" + item.ABBNo + @"</td>
                        <td>" + item.AgentCode + @"</td>
                        <td>" + item.ApprovalDate.ToString("dd/MM/yyyy") + @"</td>
                        <td>" + item.PaymentMethodCode + @"</td>
                        <td>" + item.CurrencyCode + @"</td>
                        <td>" + item.CollectedAmount.ToString("#,##0.00") + @"</td>
                        <td>" + item.PaymentAmount.ToString("#,##0.00") + @"</td>
                        <td>" + strdel + @"</td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }
        public string GetLabelLinkList()
        {
            string htmlStr = "";
            int count = 0;
            if (res_fee != null)
            {
                foreach (var item in res_abb)
                {
                    //this.tbx_abb.Text = item.TaxInvoiceNo;
                    if (count > 0)
                    {
                        //onClick="divFunction()">
                        //<a id="myLink" href="#" onclick="popup();return false;">link text</a>
                        htmlStr += string.Format(@",<a target='' href='#' onclick=""popup('{0}');return false;"">{1}</a>", item.ABBTid, item.TaxInvoiceNo);
                        //@",<a target='_blank' href='abbno.aspx?id=" + item.ABBTid + "'>" + item.TaxInvoiceNo + "</a>";
                    }
                    else if (count == 0)
                    {
                        htmlStr += string.Format(@"<a target='' href='#' onclick=""popup('{0}');return false;"">{1}</a>", item.ABBTid, item.TaxInvoiceNo);
                        //@"<a target='_blank' href='abbno.aspx?id="+ item.ABBTid +"'>"+ item.TaxInvoiceNo +"</a>";
                    }
                    count++;
                }
            }
            if (MasterDA.ValidNullABB(this.tbx_booking_no.Text))
            {
                this.btnCreateABB.Enabled = true;
            }
            else
            {
                this.btnCreateABB.Enabled = false;
            }


            if (res_abb != null && res_abb.Count(x => x.INV_ID == null) > 0)
            {
                this.btn_CreateInvoice.Enabled = true;
            }
            else
            {
                this.btn_CreateInvoice.Enabled = false;
            }

            return htmlStr;
        }

        public string TableFlightSky()
        {
            string htmlStr = "";
            if (bookingdata != null)
            {
                int jcnt = 0;
                foreach (com.airasia.acebooking.Journey jou in bookingdata.Journeys)
                {
                    int count = 0;
                    foreach (com.airasia.acebooking.Segment seq in jou.Segments)
                    {
                        string ss = "";
                        if (seq.STD.ToString("yyyyMMdd") == seq.STA.ToString("yyyyMMdd"))
                        {
                            ss = seq.STD.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            ss = seq.STD.ToString("dd/MM/yyyy") + "-" + seq.STA.ToString("dd/MM/yyyy");
                        }
                        htmlStr +=
                        @"<tr>
                            <td><img onclick=""$('#MainContent_fare1').val('" + jcnt + "_" + count + @"');$('#btnFlightSkyUpdate').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                            <td>" + seq.FlightDesignator.CarrierCode.Trim() + seq.FlightDesignator.FlightNumber.Trim() + @"</td>
                            <td>" + ss + @"</td>
                            <td>" + seq.DepartureStation + @"</td>
                            <td>" + seq.ArrivalStation + @"</td>
                            <td>" + seq.STD.ToString("HH:mm") + @"</td>
                            <td>" + seq.STA.ToString("HH:mm") + @"</td>
                        </tr>";
                        count++;
                    }
                    jcnt++;
                }
            }
            return htmlStr;
        }
        public string TablePassengersky()
        {
            string htmlStr = "";
            int count = 0;
            if (bookingdata != null)
            {
                foreach (com.airasia.acebooking.Passenger pax in bookingdata.Passengers)
                {
                
                    htmlStr +=
                        @"<tr><td><img onclick=""$('#hidfPassSkyUpdate').val('" + count + @"');$('#btnPassSkyUpdate').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                        <td>" + (count + 1).ToString() + @"</td>
                        <td style='display:none;'>" + pax.PassengerID.ToString() + @"</td>
                        <td>" + pax.Names[0].FirstName + " " + pax.Names[0].LastName + @"</td>
                        <td style='display:none;'>" + pax.PassengerTypeInfo.PaxType + @"</td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }
        public string TableFeesky()
        {

            string htmlStr = "";
            int count = 0;
            if (bookingdata != null)
            {
                DateTime bookingDate = bookingdata.BookingInfo.BookingDate;
              
                var paxc = bookingdata.Passengers.ToList();

                int adtpax = paxc.Where(p => p.PassengerTypeInfo.PaxType == "ADT").Count();
                int chdpax = paxc.Where(p => p.PassengerTypeInfo.PaxType == "CHD").Count();

                int ispax = 0, jr = 0, sem = 0, fr = 0, pxfr = 0, sr = 0;

                foreach (com.airasia.acebooking.Journey jou in bookingdata.Journeys)
                {
                    sem = 0;
                    foreach (com.airasia.acebooking.Segment seq in jou.Segments)
                    {
                        fr = 0;
                        foreach (com.airasia.acebooking.Fare fare in seq.Fares)
                        {
                            pxfr = 0;
                            foreach (com.airasia.acebooking.PaxFare paxfare in fare.PaxFares)
                            {
                                sr = 0;
                                foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfare.ServiceCharges)
                                {
                                    int mantype = adtpax;
                                    if (paxfare.PaxType == "CHD")
                                    {
                                        mantype = chdpax;
                                    }

                                    if (servchgr.ChargeType != com.airasia.acebooking.ChargeType.Discount &&
                                    servchgr.ChargeType != com.airasia.acebooking.ChargeType.PromotionDiscount)
                                    {
                                        if (servchgr.CurrencyCode != "THB")
                                        {
                                            decimal exchangerate = 1;
                                            //M_Currency curr = MasterDA.GetCurrency(servchgr.CurrencyCode);
                                            MasterDA da = new MasterDA();
                                            M_Currency curr = da.GetCurrencyListByCode(servchgr.CurrencyCode, null).Where(it => it.Create_Date != null && ((DateTime)it.Create_Date) <= bookingDate).OrderByDescending(it => it.Create_Date).Take(1).FirstOrDefault();
                                            if (curr != null)
                                            {
                                                exchangerate = curr.ExChangeRate;
                                            }

                                            var discountamt = paxfare.ServiceCharges.Where(pf => pf.ChargeCode == servchgr.ChargeCode && (pf.ChargeType == com.airasia.acebooking.ChargeType.Discount
                                                || pf.ChargeType == com.airasia.acebooking.ChargeType.PromotionDiscount)).ToList();
                                            decimal dc = 0;
                                            if (discountamt.Count() > 0)
                                            {
                                                dc = discountamt[0].Amount;
                                            }
                                            string chgcode = servchgr.ChargeCode;
                                            if (servchgr.ChargeType == com.airasia.acebooking.ChargeType.FarePrice)
                                            {
                                                chgcode = "Base Fare";
                                            }
                                            htmlStr += @"<tr>
                                            <td><img onclick=""$('#MainContent_fee').val('" + ispax + "_" + jr + "_" + sem + "_" + fr + "_" + pxfr + "_" + sr + @"');$('#MainContent_Button8').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                                            <td>" + seq.FlightDesignator.CarrierCode + seq.FlightDesignator.FlightNumber + " " + paxfare.PaxType + @"</td>
                                            <td style='display: none;'>" + chgcode + @"</td>
                                            <td >" + mantype.ToString() + @"</td>
                                            <td style='display: none;'>" + servchgr.CurrencyCode + @"</td>
                                            <td style='display: none;'>" + servchgr.Amount.ToString("#,##0.00") + @"</td>
                                            <td style='display: none;'>" + dc.ToString("#,##0.00") + @"</td>
                                            <td style='display: none;'>" + exchangerate.ToString("#,##0.00") + @"</td>
                                            <td>" + (servchgr.Amount * exchangerate).ToString("#,##0.00") + @"</td>
                                            <td>" + (dc * exchangerate).ToString("#,##0.00") + @"</td>
                                            <td>" + (((servchgr.Amount * mantype) - (dc * mantype)) * exchangerate).ToString("#,##0.00") + @"</td>
                                            </tr>";
                                        }
                                        else
                                        {
                                            var discountamt = paxfare.ServiceCharges.Where(pf => pf.ChargeCode == servchgr.ChargeCode && ( pf.ChargeType == com.airasia.acebooking.ChargeType.Discount
                                                || pf.ChargeType == com.airasia.acebooking.ChargeType.PromotionDiscount)).ToList();
                                            decimal dc = 0;
                                            if (discountamt.Count() > 0)
                                            {
                                                dc = discountamt[0].Amount;
                                            }
                                            string chgcode = servchgr.ChargeCode;
                                            if (servchgr.ChargeType == com.airasia.acebooking.ChargeType.FarePrice)
                                            {
                                                chgcode = "Base Fare";
                                            }
                                            htmlStr += @"<tr>
                                            <td><img onclick=""$('#MainContent_fee').val('" + ispax + "_" + jr + "_" + sem + "_" + fr + "_" + pxfr + "_" + sr + @"');$('#MainContent_Button8').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                                            <td  >" + seq.FlightDesignator.CarrierCode + seq.FlightDesignator.FlightNumber + " " + paxfare.PaxType + @"</td>
                                            <td style='display: none;'>" + chgcode + @"</td>
                                            <td >" + mantype.ToString() + @"</td>
                                            <td  style='display: none;'>" + servchgr.CurrencyCode + @"</td>
                                            <td  style='display: none;'>" + servchgr.Amount.ToString("#,##0.00") + @"</td>
                                            <td  style='display: none;'>" + dc.ToString("#,##0.00") + @"</td>
                                            <td  style='display: none;'>" + 1.ToString("#,##0.00") + @"</td>
                                            <td>" + servchgr.Amount.ToString("#,##0.00") + @"</td>
                                            <td>" + dc.ToString("#,##0.00") + @"</td>
                                            <td>" + ((servchgr.Amount * mantype) - (dc * mantype)).ToString("#,##0.00") + @"</td>
                                            </tr>";
                                        }
                                    }
                                    sr++;
                                }
                                pxfr++;
                            }
                            fr++;
                        }
                        sem++;
                    }
                    jr++;
                }
                ispax = 1; jr = 0; sem = 0; fr = 0; pxfr = 0; sr = 0;
                foreach (com.airasia.acebooking.Passenger pax in bookingdata.Passengers)
                {
                    sem = 0;
                    if (pax.PassengerFees != null)
                    {
                        foreach (com.airasia.acebooking.PassengerFee paxfee in pax.PassengerFees)
                        {
                            fr = 0;
                            foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfee.ServiceCharges)
                            {
                                if (servchgr.ChargeType != com.airasia.acebooking.ChargeType.Discount && 
                                    servchgr.ChargeType != com.airasia.acebooking.ChargeType.PromotionDiscount)
                                {
                                    if (servchgr.CurrencyCode != "THB")
                                    {
                                        decimal exchangerate = 1;
                                        M_Currency curr = MasterDA.GetCurrency(servchgr.CurrencyCode);
                                        if (curr != null)
                                        {
                                            exchangerate = curr.ExChangeRate;
                                        }

                                        var discountamt = paxfee.ServiceCharges.Where(pf => pf.ChargeCode == servchgr.ChargeCode && ( pf.ChargeType == com.airasia.acebooking.ChargeType.Discount
                                            || pf.ChargeType == com.airasia.acebooking.ChargeType.PromotionDiscount)).ToList();
                                        decimal dc = 0;
                                        if (discountamt.Count() > 0)
                                        {
                                            dc = discountamt[0].Amount;
                                        }
                                        htmlStr += @"<tr>
                                            <td><img onclick=""$('#MainContent_fee').val('" + ispax + "_" + jr + "_" + sem + "_" + fr + "_" + pxfr + "_" + sr + @"');$('#MainContent_Button8').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                                            <td>" + pax.Names[0].FirstName + " " + pax.Names[0].LastName + @"</td>
                                            <td style='display: none;'>" + servchgr.ChargeCode + @"</td>
                                            <td>1</td>
                                            <td style='display: none;'>" + servchgr.CurrencyCode + @"</td>
                                            <td style='display: none;'>" + servchgr.Amount.ToString("#,##0.00") + @"</td>
                                            <td style='display: none;'>" + dc.ToString("#,##0.00") + @"</td>
                                            <td style='display: none;'>" + exchangerate.ToString("#,##0.00") + @"</td>
                                            <td>" + (servchgr.Amount * exchangerate).ToString("#,##0.00") + @"</td>
                                            <td>" + (dc * exchangerate).ToString("#,##0.00") + @"</td>
                                            <td>" + ((servchgr.Amount - dc) * exchangerate).ToString("#,##0.00") + @"</td>
                                            </tr>";
                                    }
                                    else
                                    {
                                        var discountamt = paxfee.ServiceCharges.Where(pf => pf.ChargeCode == servchgr.ChargeCode && ( pf.ChargeType == com.airasia.acebooking.ChargeType.Discount
                                            || pf.ChargeType == com.airasia.acebooking.ChargeType.PromotionDiscount)).ToList();
                                        decimal dc = 0;
                                        if (discountamt.Count() > 0)
                                        {
                                            dc = discountamt[0].Amount;
                                        }
                                        htmlStr += @"<tr>
                                            <td><img onclick=""$('#MainContent_fee').val('" + ispax + "_" + jr + "_" + sem + "_" + fr + "_" + pxfr + "_" + sr + @"');$('#MainContent_Button8').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                                            <td>" + pax.Names[0].FirstName + " " + pax.Names[0].LastName + @"</td>
                                            <td style='display: none;'>" + servchgr.ChargeCode + @"</td>
                                            <td>1</td>
                                            <td style='display: none;'>" + servchgr.CurrencyCode + @"</td>
                                            <td style='display: none;'>" + servchgr.Amount.ToString("#,##0.00") + @"</td>
                                            <td style='display: none;'>" + dc.ToString("#,##0.00") + @"</td>
                                            <td style='display: none;'>" + 1.ToString("#,##0.00") + @"</td>
                                            <td>" + servchgr.Amount.ToString("#,##0.00") + @"</td>
                                            <td>" + dc.ToString("#,##0.00") + @"</td>
                                            <td>" + (servchgr.Amount - dc).ToString("#,##0.00") + @"</td>
                                            </tr>";
                                    }
                                }
                                fr++;
                            }
                            sem++;
                        }
                    }
                    jr++;
                }
            }
            return htmlStr;
        }

        private List<FeeDetail> GetFareSky()
        {
            try
            {
                List<FeeDetail> lstFee = new List<FeeDetail>();
                List<FeeDetail> lst = new List<FeeDetail>();
                if (bookingdata != null)
                {
                    MasterDA da = new MasterDA();
                    DateTime bookingDate = bookingdata.BookingInfo.BookingDate;
                    var paxc = bookingdata.Passengers.ToList();

                    int adtpax = paxc.Where(p => p.PassengerTypeInfo.PaxType == "ADT").Count();
                    int chdpax = paxc.Where(p => p.PassengerTypeInfo.PaxType == "CHD").Count();

                    int ispax = 0, jr = 0, sem = 0, fr = 0, pxfr = 0, sr = 0;

                    foreach (com.airasia.acebooking.Journey jou in bookingdata.Journeys)
                    {
                        sem = 0;
                        foreach (com.airasia.acebooking.Segment seq in jou.Segments)
                        {
                            fr = 0;
                            foreach (com.airasia.acebooking.Fare fare in seq.Fares)
                            {
                                pxfr = 0;
                                foreach (com.airasia.acebooking.PaxFare paxfare in fare.PaxFares)
                                {
                                    sr = 0;
                                    foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfare.ServiceCharges)
                                    {
                                        int mantype = adtpax;
                                        if (paxfare.PaxType == "CHD")
                                        {
                                            mantype = chdpax;
                                        }

                                        if (servchgr.ChargeType != com.airasia.acebooking.ChargeType.Discount &&
                                        servchgr.ChargeType != com.airasia.acebooking.ChargeType.PromotionDiscount)
                                        {
                                            decimal exchangerate = 1;
                                            if (servchgr.CurrencyCode != "THB")
                                            {

                                                exchangerate = MasterDA.GetCurrencyActiveDate(servchgr.CurrencyCode, bookingDate);

                                                //M_Currency curr = da.GetCurrencyListByCode(servchgr.CurrencyCode, null).Where(it => it.Create_Date != null && ((DateTime)it.Create_Date) <= seq.SalesDate).OrderByDescending(it => it.Create_Date).Take(1).FirstOrDefault();
                                                //if (curr != null)
                                                //    exchangerate = curr.ExChangeRate;
                                            }
                                            var discountamt = paxfare.ServiceCharges.Where(pf => pf.ChargeCode == servchgr.ChargeCode && (pf.ChargeType == com.airasia.acebooking.ChargeType.Discount
                                                || pf.ChargeType == com.airasia.acebooking.ChargeType.PromotionDiscount)).ToList();
                                            decimal dc = 0;
                                            if (discountamt.Count() > 0)
                                            {
                                                dc = discountamt[0].Amount;
                                            }
                                            string chgcode = servchgr.ChargeCode;
                                            if (servchgr.ChargeType == com.airasia.acebooking.ChargeType.FarePrice)
                                            {
                                                chgcode = "Base Fare";
                                            }



                                            FeeDetail item = new FeeDetail();
                                            item.PaxType = paxfare.PaxType;
                                            item.Header = seq.FlightDesignator.CarrierCode + seq.FlightDesignator.FlightNumber + " " + paxfare.PaxType;
                                            item.DepartureDate = seq.STD;
                                            item.SeqmentNo = seq.DepartureStation + seq.ArrivalStation + seq.FlightDesignator.CarrierCode + seq.FlightDesignator.FlightNumber.Trim();
                                            item.SeqmentPaxNo = item.SeqmentNo + paxfare.PaxType;
                                            item.ServiceChargeNo = item.SeqmentPaxNo + servchgr.ChargeCode + servchgr.TicketCode + servchgr.Amount.ToString("0.0000");
                                            item.TicketCode = servchgr.TicketCode;
                                            item.ChargeType = servchgr.ChargeType.ToString();
                                            item.CollectType = servchgr.CollectType.ToString();
                                            item.ForeignCurrencyCode = servchgr.ForeignCurrencyCode;
                                            item.ForeignAmount = servchgr.ForeignAmount;
                                            item.FeeCreateDate = seq.SalesDate;

                                            item.ChargeCode = chgcode;
                                            item.Amount = servchgr.Amount;
                                            item.AmountTHB = servchgr.Amount * exchangerate;
                                            item.Exc = exchangerate;
                                            //item.amt = ((servchgr.Amount * mantype) - (dc * mantype)) * exchangerate;
                                            //item.qty = mantype;
                                            item.price = servchgr.Amount;
                                            item.CurrencyCode = servchgr.CurrencyCode;
                                            item.Discount = dc * exchangerate;
                                            item.ChargeDetail = servchgr.ChargeDetail;
                                            item.index = ispax + "_" + jr + "_" + sem + "_" + fr + "_" + pxfr + "_" + sr;
                                            item.TableName = "T_BookingServiceChargeCurrentUpdate";

                                            lst.Add(item);
                                        }
                                        sr++;
                                    }
                                    pxfr++;
                                }
                                fr++;
                            }
                            sem++;
                        }
                        jr++;
                    }

                    var PassengerCount = bookingdata.Passengers.Count();
                    FeeDetail fee = new FeeDetail();
                    foreach (FeeDetail item in lst)
                    {
                        fee = new FeeDetail();

                        fee.RowID = Guid.NewGuid();
                        fee.PaxType = item.PaxType;
                        fee.Header = item.Header;
                        fee.DepartureDate = item.DepartureDate;
                        fee.SeqmentNo = item.SeqmentNo;
                        fee.SeqmentPaxNo = item.SeqmentPaxNo;
                        fee.ServiceChargeNo = item.ServiceChargeNo;
                        fee.TicketCode = item.TicketCode;
                        fee.ChargeType = item.ChargeType;
                        fee.CollectType = item.CollectType;
                        fee.ForeignCurrencyCode = item.ForeignCurrencyCode;
                        fee.ForeignAmount = item.ForeignAmount;
                        fee.FeeCreateDate = item.FeeCreateDate;

                        fee.ChargeCode = item.ChargeCode;
                        fee.Amount = item.Amount;
                        fee.AmountTHB = item.AmountTHB;
                        fee.Exc = item.Exc;
                        fee.price = item.price;
                        fee.CurrencyCode = item.CurrencyCode;
                        fee.Discount = item.Discount;
                        fee.TotalAmountTHB = item.TotalAmountTHB;
                        fee.ChargeDetail = item.ChargeDetail;
                        fee.index = item.index;
                        fee.TableName = item.TableName;

                        fee.qty = item.PaxType == "ADT" ? adtpax : chdpax;

                        fee.TotalAmountTHB = fee.AmountTHB * fee.qty;

                        lstFee.Add(fee);
                    }
                }

                lstFee = lstFee.OrderBy(x => x.DepartureDate).ThenBy(it => it.PaxType).ToList();

                return lstFee;
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }
        private List<FeeDetail> GetFeeSky()
        {
            try
            {
                List<FeeDetail> lstFee = new List<FeeDetail>();
                List<FeeDetail> lst = new List<FeeDetail>();
                if (bookingdata != null)
                {
                    DateTime bookingDate = bookingdata.BookingInfo.BookingDate;
                    int ispax = 1, jr = 0, sem = 0, fr = 0, pxfr = 0, sr = 0;
                    foreach (com.airasia.acebooking.Passenger pax in bookingdata.Passengers)
                    {
                        sem = 0;
                        if (pax.PassengerFees != null)
                        {
                            foreach (com.airasia.acebooking.PassengerFee paxfee in pax.PassengerFees)
                            {
                                fr = 0;
                                foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfee.ServiceCharges)
                                {
                                    if (servchgr.ChargeType != com.airasia.acebooking.ChargeType.Discount &&
                                        servchgr.ChargeType != com.airasia.acebooking.ChargeType.PromotionDiscount)
                                    {
                                        decimal exchangerate = 1;
                                        if (servchgr.CurrencyCode != "THB")
                                        {
                                            exchangerate = MasterDA.GetCurrencyActiveDate(servchgr.CurrencyCode, bookingDate);
                                            //M_Currency curr = MasterDA.GetCurrency(servchgr.CurrencyCode);
                                            //M_Currency curr = da.GetCurrencyListByCode(servchgr.CurrencyCode, null).Where(it => it.Create_Date != null && ((DateTime)it.Create_Date).ToString("ddMMyyyy") == paxfee.CreatedDate.ToString("ddMMyyyy")).OrderByDescending(it => it.Create_Date).FirstOrDefault();
                                            //if (curr != null)
                                            //    exchangerate = curr.ExChangeRate;

                                        }

                                        var discountamt = paxfee.ServiceCharges.Where(pf => pf.ChargeCode == servchgr.ChargeCode && (pf.ChargeType == com.airasia.acebooking.ChargeType.Discount
                                            || pf.ChargeType == com.airasia.acebooking.ChargeType.PromotionDiscount)).ToList();
                                        decimal dc = 0;
                                        if (discountamt.Count() > 0)
                                        {
                                            dc = discountamt[0].Amount;
                                        }

                                        int qty = 1;
                                        FeeDetail item = new FeeDetail();
                                        item.Header = pax.Names[0].FirstName + " " + pax.Names[0].LastName;
                                        item.PassengerId = pax.PassengerID;
                                        item.FirstName = pax.Names[0].FirstName;
                                        item.LastName = pax.Names[0].LastName;
                                        item.TicketCode = servchgr.TicketCode;
                                        item.ChargeType = servchgr.ChargeType.ToString();
                                        item.ChargeCode = servchgr.ChargeCode;
                                        item.CollectType = servchgr.CollectType.ToString();
                                        item.ForeignCurrencyCode = servchgr.ForeignCurrencyCode;
                                        item.ForeignAmount = servchgr.ForeignAmount;
                                        item.FeeCreateDate = paxfee.CreatedDate;

                                        item.Amount = ((servchgr.Amount * qty) - (dc * qty)) * exchangerate;
                                        item.AmountTHB = ((servchgr.Amount * qty) - (dc * qty)) * exchangerate;
                                        item.amt = item.Amount;
                                        item.qty = qty;
                                        item.price = servchgr.Amount;
                                        item.CurrencyCode = servchgr.CurrencyCode;
                                        item.Discount = dc * exchangerate;
                                        item.ChargeDetail = servchgr.ChargeDetail;
                                        item.index = ispax + "_" + jr + "_" + sem + "_" + fr + "_" + pxfr + "_" + sr;
                                        item.TableName = "T_PassengerFeeServiceChargeCurrentUpdate";
                                        lst.Add(item);

                                    }
                                    fr++;
                                }
                                sem++;
                            }
                        }
                        jr++;
                    }
                }


                lstFee.AddRange(lst);

                lstFee = lstFee.OrderBy(it => it.ChargeDetail).ThenBy(it => it.FirstName).ToList();
                //lst = lst.OrderBy(it => it.ChargeDetail).ThenBy(it => it.ChargeCode).ToList();
                return lstFee;
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }

        private List<T_PaymentCurrentUpdate> GetPaymentSky()
        {
            List<T_PaymentCurrentUpdate> lst = new List<T_PaymentCurrentUpdate>();
            T_PaymentCurrentUpdate item;
            if (bookingdata != null)
            {
                DateTime bookingDate = bookingdata.BookingInfo.BookingDate;
                var SkyPay = bookingdata.Payments.Where(x => x.Status == com.airasia.acebooking.BookingPaymentStatus.Approved);
                foreach (com.airasia.acebooking.Payment payment in SkyPay)
                 {
                    item = new T_PaymentCurrentUpdate();
                    item.AgentCode = payment.PointOfSale.AgentCode;
                    item.ApprovalDate = payment.ApprovalDate;
                    item.PaymentMethodCode = payment.PaymentMethodCode;
                    item.CurrencyCode = payment.CurrencyCode;
                    item.PaymentAmount = payment.PaymentAmount;
                    item.PaymentAmountTHB = payment.PaymentAmount;
                    item.ReferenceID = payment.ReferenceID;
                    item.ReferenceType = payment.ReferenceType.ToString() ;
                    if (item.CurrencyCode != "THB")
                    {
                        decimal ex_rate = MasterDA.GetCurrencyActiveDate(item.CurrencyCode, bookingDate);
                        item.PaymentAmountTHB = ex_rate * payment.PaymentAmount;
                    }
                    item.CollectedCurrencyCode = payment.CollectedCurrencyCode;
                    item.CollectedAmount = payment.CollectedAmount;
                    item.QuotedCurrencyCode = payment.QuotedCurrencyCode;
                    item.QuotedAmount = payment.QuotedAmount;
                    item.Status = payment.Status.ToString();
                    item.PaymentMethodType = payment.PaymentMethodType.ToString();
                    item.PaymentMethodCode = payment.PaymentMethodCode;
                    item.CurrencyCode = payment.CurrencyCode;
                    item.PaymentAmount = payment.PaymentAmount;
                    item.ParentPaymentID = payment.ParentPaymentID;
                    if (payment.PointOfSale != null)
                    {
                        item.AgentCode = payment.PointOfSale.AgentCode;
                        item.OrganizationCode = payment.PointOfSale.OrganizationCode;
                        item.DomainCode = payment.PointOfSale.DomainCode;
                        item.LocationCode = payment.PointOfSale.LocationCode;
                    }

                    if (payment.PaymentFields != null && payment.PaymentFields.Length > 0)
                    {
                        foreach (var data in payment.PaymentFields)
                        {
                            switch (data.FieldName.ToUpper())
                            {
                                case "ISSNAME":
                                    item.BankName = data.FieldValue;
                                    break;
                                case "ISSCTRY":
                                    item.BranchNo = data.FieldValue;
                                    break;
                            }
                        }
                    }

                    item.AccountNo = payment.AccountNumber;
                    item.AccountNoID = payment.AccountNumberID.ToString();

                    lst.Add(item);
                }
            }
            return lst;
        }
        
        public string TablePaymentsky()
        {
            try
            {


                string htmlStr = "";
                int count = 0;
                if (bookingdata != null)
                {
                    MasterDA da = new MasterDA();
                    foreach (var payment in res_payment_sky)
                    {
                        if (payment.CurrencyCode == "THB")
                        {
                            htmlStr +=
                                @"<tr>
                            <td><img onclick=""$('#hidfPaySkyUpdate').val(" + count + @");$('#btnPaySkyUpdate').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                            <td>" + payment.AgentCode + @"</td>
                            <td>" + payment.ApprovalDate.ToString("dd/MM/yyyy") + @"</td>
                            <td>" + payment.PaymentMethodCode + @"</td>
                            <td>" + payment.CurrencyCode + @"</td>
                            <td>" + payment.CollectedAmount.ToString("#,##0.00") + @"</td>
                            <td>" + payment.PaymentAmount.ToString("#,##0.00") + @"</td>
                            </tr>";

                        }
                        else
                        {
                            M_Currency curr = da.GetCurrencyListByCode(payment.CurrencyCode, null).Where(it => it.Create_Date != null && (((DateTime)it.Create_Date) <= payment.ApprovalDate)).OrderByDescending(it => it.Create_Date).Take(1).FirstOrDefault();
                            decimal rate = 0;
                            if (curr != null)
                            {
                                rate = curr.ExChangeRate;
                                htmlStr +=
                                    @"<tr>
                            <td><img onclick=""$('#hidfPaySkyUpdate').val(" + count + @");$('#btnPaySkyUpdate').click();"" src='Images/back-20.png' Style='cursor: pointer; width: 18px;' /></td>
                            <td>" + payment.AgentCode + @"</td>
                            <td>" + payment.ApprovalDate.ToString("dd/MM/yyyy") + @"</td>
                            <td>" + payment.PaymentMethodCode + @"</td>
                            <td>" + payment.CurrencyCode + @"</td>
                            <td>" + payment.CollectedAmount.ToString("#,##0.00") + @"</td>
                            <td>" + (payment.PaymentAmount * rate).ToString("#,##0.00") + @"</td>
                            </tr>";
                            }
                        }



                        count++;
                    }
                }
                return htmlStr;
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;

            }

        }


        
        #region Update From Sky Speed
        
       
        protected void btnFlightSkyUpdate_Click(object sender, EventArgs e)
        {
            if (this.fare1.Value != "" && bookingdata != null)
            {
                string[] str = this.fare1.Value.Split('_');
                int index = int.Parse(str[0]);
                int fare = int.Parse(str[1]);
                com.airasia.acebooking.Segment obj = bookingdata.Journeys[index].Segments[fare];
                string segid = obj.DepartureStation + obj.ArrivalStation + obj.FlightDesignator.CarrierCode + obj.FlightDesignator.FlightNumber;
                //List<T_SegmentCurrentUpdate> lstFligth = this.res_flight.Where(x => x.CarrierCode == obj.FlightDesignator.CarrierCode && x.FlightNumber.Trim() == obj.FlightDesignator.FlightNumber.Trim()).ToList();
                T_SegmentCurrentUpdate item = new T_SegmentCurrentUpdate();

                //if (lstFligth.Count > 0)
                //{
                //    item = lstFligth[0];

                //    if (item.RowState == 1)
                //        return;
                //    item.RowState = 2;
                //}
                //else
                {
                    item.RowState = 1;
                    res_flight.Add(item);
                    item.TransactionId = new Guid(hidfTranID_New.Value);
                }

                
                item.SeqmentNo = obj.DepartureStation + obj.ArrivalStation + obj.FlightDesignator.CarrierCode + obj.FlightDesignator.FlightNumber.Trim();
                item.DepartureStation = obj.DepartureStation;
                item.ArrivalStation = obj.ArrivalStation;
                item.STD = obj.STD;
                item.STA = obj.STA;
                item.CarrierCode = obj.FlightDesignator.CarrierCode;
                item.FlightNumber = obj.FlightDesignator.FlightNumber.Trim();
                //if (res_flight.Count > 0)
                //    item.TransactionId = res_flight[0].TransactionId;
                //else
                //    item.TransactionId = Guid.NewGuid();
                //if (res_abb.Count > 0)
                //    item.ABBNo = res_abb[0].TaxInvoiceNo;

                item.Create_By = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
                item.Create_Date = DateTime.Now;
                item.Update_By = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
                item.Update_Date = DateTime.Now;


                Session["res_flight"] = res_flight;
                rptFlightDetail.DataSource = res_flight;
                rptFlightDetail.DataBind();

                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "alert('This flightNumber already exists.');", true);
                //}
            }
        }

        protected void btnPassSkyUpdate_Click(object sender, EventArgs e)
        {
            if (hidfPassSkyUpdate.Value != "" && bookingdata != null)
            {
         
                int index = int.Parse(hidfPassSkyUpdate.Value);
                com.airasia.acebooking.Passenger obj = bookingdata.Passengers[index];

                //List<T_PassengerCurrentUpdate> lstFligth = res_passenger.Where(x => x.PassengerId == obj.PassengerID).ToList();
                T_PassengerCurrentUpdate item = new T_PassengerCurrentUpdate();

                //if (lstFligth.Count > 0)
                //{
                //    item = lstFligth[0];

                //    if (item.RowState == 1)
                //        return;
                //    item.RowState = 2;
                //}
                //else
                {
                    item.RowState = 1;
                    item.TransactionId = new Guid(hidfTranID_New.Value);
                    res_passenger.Add(item);
                }


                item.PassengerId = obj.PassengerID;

                if (obj.Names.Length > 0)
                {
                    item.FirstName = obj.Names[0].FirstName;
                    item.LastName = obj.Names[0].LastName;
                    item.Title = obj.Names[0].Title;
                }

                item.PaxType = obj.PassengerTypeInfo.PaxType;

                //if (res_abb.Count > 0)
                //    item.ABBNo = res_abb[0].TaxInvoiceNo;

           
                    

                item.Create_By = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
                item.Create_Date = DateTime.Now;
                item.Update_By = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
                item.Update_Date = DateTime.Now;

                for (int i = 0; i < res_passenger.Count; i++)
                    res_passenger[i].RowNo = (i + 1);
                //Session["res_passenger"] = res_passenger;
                //rptPassenger.DataSource = res_passenger;
                //rptPassenger.DataBind();
                PasssengerBinding();
            }
        }

        protected void btnPaySkyUpdate_Click(object sender, EventArgs e)
        {
            if (hidfPaySkyUpdate.Value != "" && bookingdata != null)
            {

                int index = int.Parse(hidfPaySkyUpdate.Value);
                com.airasia.acebooking.Payment obj = bookingdata.Payments[index];

                //List<T_PaymentCurrentUpdate> lstFligth = res_payment.Where(x => x.PaymentID == obj.PaymentID).ToList();
                T_PaymentCurrentUpdate item = res_payment_sky[index];

                //if (lstFligth.Count > 0)
                //{
                //    item = lstFligth[0];

                //    if (item.RowState == 1)
                //        return;
                //    item.RowState = 2;
                //}
                //else
                {
                    item.RowState = 1;
                    item.TransactionId = new Guid(hidfTranID_New.Value);
                    res_payment.Add(item);
                }


                //item.PaymentID = obj.PaymentID;
                //item.ReferenceType = obj.ReferenceType.ToString();
                //item.ReferenceID = obj.ReferenceID;

                //item.PaymentMethodType = obj.PaymentMethodType.ToString();
                //item.PaymentMethodCode = obj.PaymentMethodCode;
                //item.CurrencyCode = obj.CurrencyCode;
                //item.PaymentAmount = obj.PaymentAmount;
            
                //item.CollectedCurrencyCode = obj.CollectedCurrencyCode;
                //item.CollectedAmount = obj.CollectedAmount;
                //item.QuotedCurrencyCode = obj.QuotedCurrencyCode;
                //item.QuotedAmount = obj.QuotedAmount;
                //item.Status = obj.Status.ToString() ;

                //item.ApprovalDate = obj.ApprovalDate;

                //item.ParentPaymentID = obj.ParentPaymentID;
                //if (obj.PointOfSale != null)
                //{
                //    item.AgentCode = obj.PointOfSale.AgentCode;
                //    item.OrganizationCode = obj.PointOfSale.OrganizationCode;
                //    item.DomainCode = obj.PointOfSale.DomainCode;
                //    item.LocationCode = obj.PointOfSale.LocationCode;
                //}

                //if (res_abb.Count > 0)
                //    item.ABBNo = res_abb[0].TaxInvoiceNo;

                //if (res_payment.Count > 0)
                //    item.TransactionId = res_payment[0].TransactionId;
                //else
                //    item.TransactionId = Guid.NewGuid();



                //if (obj.Names.Length > 0)
                //{
                //    item.FirstName = obj.Names[0].FirstName;
                //    item.LastName = obj.Names[0].LastName;
                //    item.Title = obj.Names[0].Title;
                //}

                //item.PaxType = obj.PassengerTypeInfo.PaxType;
                item.Create_By = CookiesMenager.EmpID;
                item.Create_Date = DateTime.Now;
                item.Update_By = CookiesMenager.EmpID;
                item.Update_Date = DateTime.Now;

                for (int i = 0; i < res_payment.Count; i++)
                    res_payment[i].RowNo = (i + 1);
                Session["res_payment"] = res_payment;
                rptPayment.DataSource = res_payment;
                rptPayment.DataBind();

                decimal sum = (decimal)res_payment.Where(it => it.RowState != -1).Sum(it => it.PaymentAmountTHB);
                lblTotalPay.Text = sum.ToString("#,###,##0.00");
            }
        }


        protected void btnFeeSkyUpdate_Click(object sender, EventArgs e)
        {
            if (hidfFeeSkyUpdate.Value != "" && bookingdata != null)
            {
                string[] sp = hidfFeeSkyUpdate.Value.ToString().Split('|');
                string[] str = sp[0].Split('_');
               //if (int.Parse(str[0]) == 0)
               {
                   int index = Convert.ToInt32(sp[1]);
                   //com.airasia.acebooking.BookingServiceCharge obj = bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].Fares[int.Parse(str[3])].PaxFares[int.Parse(str[4])].ServiceCharges[int.Parse(str[5])];
                   //string head = bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].FlightDesignator.CarrierCode + bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].FlightDesignator.FlightNumber + " " + bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].Fares[int.Parse(str[3])].PaxFares[int.Parse(str[4])].PaxType;
                   //List<FeeDetail> lst = this.res_fee.Where(x => x.ChargeCode == (obj.ChargeCode == string.Empty ? "Base Fare" : obj.ChargeCode) && x.ChargeType == obj.ChargeType.ToString()).ToList();
                   FeeDetail item = this.res_fee_sky[index];
                   //FeeDetail item = new FeeDetail();
                   
                   //if (lst.Count == 1)
                   //{
                   //    item = lst[0];
                   //    if (item.RowState == 1)
                   //        return;
                   //    item.RowState = 2;
                   //}
                   //else
                   {
                       item.RowState = 1;
                       res_fee.Add(item);
                   }
                   //item.PassengerId = lstSky.PassengerId;
                   //item.FirstName = lstSky.FirstName;
                   //item.LastName = lstSky.LastName;
                   //item.ChargeType = lstSky.ChargeType.ToString();
                   //item.ChargeCode = lstSky.ChargeCode;
                   //item.ChargeDetail = lstSky.ChargeDetail;
                   //item.CollectType = lstSky.CollectType.ToString();
                   //item.TicketCode = lstSky.TicketCode;
                   //item.CurrencyCode = lstSky.CurrencyCode;
                   //item.Amount = lstSky.Amount;
                   //item.ForeignCurrencyCode = lstSky.ForeignCurrencyCode;
                   //item.AmountTHB = lstSky.AmountTHB;
                   //item.Create_By = CookiesMenager.EmpID;
                   //item.Update_By = CookiesMenager.EmpID;
                   //item.FeeCreateDate = lstSky.FeeCreateDate;

                   //item.amt = lstSky.amt;
                   //item.price = lstSky.price;
                   //item.qty = lstSky.qty;
                   //item.Discount = lstSky.Discount;
                   //item.DiscountOri = lstSky.DiscountOri;

                   item.Create_By = CookiesMenager.EmpID;
                   item.Update_By = CookiesMenager.EmpID;

                   for (int i = 0; i < res_fee.Count; i++)
                       res_fee[i].RowNo = i;
                   Session["res_fee"] = res_fee;
                   rptFee.DataSource = res_fee;
                   rptFee.DataBind();


                   decimal sum = (decimal)res_fee.Where(it => it.RowState != -1).Sum(it => it.AmountTHB);
                   lblTotal.Text = sum.ToString("#,###,##0.00");
               }
            }
        }


        #endregion

        #region Delete Event


        protected void btnDelFlight_Click(object sender, EventArgs e)
        {
            if (hidfDelFlight.Value != "")
            {
                List<T_SegmentCurrentUpdate> lst = res_flight.Where(it => it.SegmentTId.ToString() == hidfDelFlight.Value.ToString()).ToList();
                if (lst.Count > 0)
                {
                    if (lst[0].RowState == 1)
                    {
                        res_flight.Remove(lst[0]);
                    }
                    else 
                    {
                        lst[0].RowState = -1;
                    }
                    Session["res_flight"] = res_flight;
                    rptFlightDetail.DataSource = res_flight;
                    rptFlightDetail.DataBind();
                }

                //int i = 0;
                //res_flight.ForEach(it => it.RowNo = i++);
               
            }
        }

        protected void btnDelPass_Click(object sender, EventArgs e)
        {
            if (hidfDelPass.Value != "")
            {
                List<T_PassengerCurrentUpdate> lst = res_passenger.Where(it => it.PassengerTId.ToString() == hidfDelPass.Value.ToString()).ToList();
                if (lst.Count > 0)
                {
                    if (lst[0].RowState == 1)
                    {
                        res_passenger.Remove(lst[0]);
                    }
                    else
                    {
                        lst[0].RowState = -1;
                    }

                    for (int i = 0; i < res_passenger.Count; i++)
                        res_passenger[i].RowNo = (i + 1);

                    PasssengerBinding();
                    RecalFare();
                    //Session["res_passenger"] = res_passenger;
                    //rptPassenger.DataSource = res_passenger;
                    //rptPassenger.DataBind();
                }
            }
        }

        protected void btnDelPay_Click(object sender, EventArgs e)
        {
            if (hidfDelPay.Value != "")
            {
                List<T_PaymentCurrentUpdate> lst = res_payment.Where(it => it.PaymentTId.ToString() == hidfDelPay.Value.ToString()).ToList();
                if (lst.Count > 0)
                {
                    if (lst[0].RowState == 1)
                    {
                        res_payment.Remove(lst[0]);
                    }
                    else
                    {
                        lst[0].RowState = -1;
                    }

                    for (int i = 0; i < res_payment.Count; i++)
                        res_payment[i].RowNo = (i + 1);


                    Session["res_payment"] = res_payment;
                    rptPayment.DataSource = res_payment;
                    rptPayment.DataBind();

                    decimal sum = (decimal)res_payment.Sum(it => it.PaymentAmountTHB);
                    lblTotalPay.Text = sum.ToString("#,###,##0.00");
                }
            }
        }

        protected void btnDelFee_Click(object sender, EventArgs e)
        {
            if (hidfDelFee.Value != "")
            {
                var RowId = Guid.Parse(hidfDelFee.Value);
                FeeDetail fee = res_fee.FirstOrDefault(x => x.RowID == RowId);

                if (fee.RowState == 1)
                {
                    res_fee.Remove(fee);
                }
                else
                {
                    fee.RowState = -1;
                }
                Session["res_fee"] = res_fee;
                rptFee.DataSource = res_fee;
                rptFee.DataBind();

                BindingTotal();
            }
        }

        #endregion

        protected void Button8_Click(object sender, EventArgs e)
        {
            if (this.fee.Value != "" && bookingdata != null)
            {
                string[] str = this.fee.Value.Split('_');
                if (int.Parse(str[0]) == 0)
                {
                    com.airasia.acebooking.BookingServiceCharge obj = bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].Fares[int.Parse(str[3])].PaxFares[int.Parse(str[4])].ServiceCharges[int.Parse(str[5])];
                    string head = bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].FlightDesignator.CarrierCode + bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].FlightDesignator.FlightNumber + " " + bookingdata.Journeys[int.Parse(str[1])].Segments[int.Parse(str[2])].Fares[int.Parse(str[3])].PaxFares[int.Parse(str[4])].PaxType;
                    var found = this.res_fee.Where(x => x.Header == head && x.ChargeCode == obj.ChargeCode);
                    if (found.Count() == 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "alert('This chargeCode already exists.');", true);
                    }
                }
                else
                {
                    com.airasia.acebooking.BookingServiceCharge obj = bookingdata.Passengers[int.Parse(str[1])].PassengerFees[int.Parse(str[2])].ServiceCharges[int.Parse(str[3])];
                    string head = bookingdata.Passengers[int.Parse(str[1])].Names[0].FirstName + " " + bookingdata.Passengers[int.Parse(str[1])].Names[0].LastName;
                    var found = this.res_fee.Where(x => x.Header == head && x.ChargeCode == obj.ChargeCode);
                    if (found.Count() == 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "script", "alert('This chargeCode already exists.');", true);
                    }
                }
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            if (this.delfee.Value != "" && bookingdata != null)
            {
                int index = int.Parse(this.delfee.Value);
                this.res_fee.RemoveAt(index);
                Session["res_fee"] = res_fee;
            }
        }

        #region DoSave
        
        
        protected void btnSaveFlight_Click(object sender, EventArgs e)
        {
            try
            {
                string val = hidfFlightSaveVal.Value;

                if (val != "")
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(val);
                    if (dt.Rows.Count > 0)
                    {
                        T_SegmentCurrentUpdate seg;
                        string mode = dt.Rows[0]["Mode"].ToString();
                        if (mode == "NEW")
                        {
                            seg = new T_SegmentCurrentUpdate();
                            seg.SeqmentNo = dt.Rows[0]["DepartureStation"].ToString().Trim() + dt.Rows[0]["ArrivalStation"].ToString().Trim() + dt.Rows[0]["CarrierCode"].ToString().Trim() + dt.Rows[0]["FlightNumber"].ToString().Trim();
                
                            seg.RowState = 1;

                            res_flight.Add(seg);
                        }
                        else
                        {
                             seg = res_flight.Where(it => it.SegmentTId.ToString() == dt.Rows[0]["SegmentTId"].ToString()).FirstOrDefault();
                             if (seg.RowState != 1)
                                 seg.RowState = 2;
                        }

                        seg.CarrierCode = dt.Rows[0]["CarrierCode"].ToString();
                        seg.FlightNumber = dt.Rows[0]["FlightNumber"].ToString();
                        seg.DepartureStation = dt.Rows[0]["DepartureStation"].ToString();
                        seg.ArrivalStation = dt.Rows[0]["ArrivalStation"].ToString();
                        if (dt.Rows[0]["date_STD"].ToString() != "")
                        {
                            string date = dt.Rows[0]["date_STD"].ToString() + " " + dt.Rows[0]["time_STD"].ToString();
                            seg.STD = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                        }

                        if (dt.Rows[0]["date_STA"].ToString() != "")
                        {
                            string date = dt.Rows[0]["date_STA"].ToString() + " " + dt.Rows[0]["time_STA"].ToString();
                            seg.STA = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                        }
             
                        seg.ABBNo = dt.Rows[0]["ABBNo"].ToString();
                        seg.Create_By = CookiesMenager.EmpID;
                        seg.Update_By = CookiesMenager.EmpID;

                        
                        Session["res_flight"] = res_flight;
                        rptFlightDetail.DataSource = res_flight;
                        rptFlightDetail.DataBind();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnSavePass_Click(object sender, EventArgs e)
        {
            if (hidfPassSaveVal.Value != "")
            {
                string val = hidfPassSaveVal.Value.ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(val);
                if (dt.Rows.Count > 0)
                {
                    T_PassengerCurrentUpdate passenger;
                    string mode = dt.Rows[0]["Mode"].ToString();
                    if (mode == "NEW")
                    {
                        passenger = new T_PassengerCurrentUpdate();
                        passenger.RowState = 1;
                        passenger.RowNo = res_passenger.Where(x => x.RowState != -1).Max(s => s.RowNo) + 1;
                        res_passenger.Add(passenger);
                    }
                    else
                    {
                        passenger = res_passenger.Where(it => it.PassengerTId.ToString() == dt.Rows[0]["PassengerTId"].ToString()).FirstOrDefault();
                        if (passenger.RowState != 1)
                            passenger.RowState = 2;
                    }
                    //passenger.PassengerId = Convert.ToInt64(dt.Rows[0]["PassengerId"]);
                    passenger.Title = dt.Rows[0]["Title"].ToString();
                    passenger.FirstName = dt.Rows[0]["FirstName"].ToString();
                    passenger.LastName = dt.Rows[0]["LastName"].ToString();
                    passenger.PaxType = dt.Rows[0]["PaxType"].ToString();
                    passenger.ABBNo = dt.Rows[0]["ABBNo"].ToString();
                    passenger.Create_By = CookiesMenager.EmpID;
                    passenger.Update_By = CookiesMenager.EmpID;
                }
 
                PasssengerBinding();

                RecalFare();
            }
        }


        protected void btnSaveFare_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidfFareSaveVal.Value != "")
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(hidfFareSaveVal.Value.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        string mode = dt.Rows[0]["Mode"].ToString();

                        
                        //var PaxType = dt.Rows[0]["PaxType"].ToString();
                        var PaxFareTId = dt.Rows[0]["PaxFareTId"].ToString();

                        FeeDetail service_charge;
                        if (mode == "NEW")
                        {
                            service_charge = new FeeDetail();
                            service_charge.RowState = 1;
                            service_charge.TableName = "T_BookingServiceChargeCurrentUpdate";
                            service_charge.RowID = Guid.NewGuid();
                            res_fare.Add(service_charge);
                        }
                        else
                        {
                            var RowId = Guid.Parse(dt.Rows[0]["PassengerFeeServiceChargeTId"].ToString());
                            service_charge = res_fare.First(x => x.RowID == RowId);
                            if (service_charge.RowState != 1)
                                service_charge.RowState = 2;
                        }

                        //if (!string.IsNullOrEmpty(dt.Rows[0]["SegmentTId"].ToString()))
                        //{
                        //    string SegmentTId = dt.Rows[0]["SegmentTId"].ToString();
                        //    T_SegmentCurrentUpdate seg = res_flight.Where(it => it.SegmentTId.ToString().ToUpper() == SegmentTId.ToUpper()).FirstOrDefault();
                        //    List<T_PaxFareCurrentUpdate> lst_pax = MasterDA.GetPaxList(seg.SegmentTId);
                        //    if (lst_pax.Count > 0)
                        //    {
                        //        service_charge.PaxFareTId =  lst_pax[0].PaxFareTId.ToString();
                        //        service_charge.SeqmentPaxNo = lst_pax[0].SeqmentPaxNo;
                        //    }
                        //    else
                        //    {
                        //        T_PaxFareCurrentUpdate data = new T_PaxFareCurrentUpdate();
                        //        data.PaxFareTId = Guid.NewGuid();
                        //        data.SegmentTId = seg.SegmentTId;
                        //        data.SeqmentNo = seg.SeqmentNo;
                        //        data.SeqmentPaxNo = seg.SeqmentNo + PaxType;
                        //        data.PaxType = PaxType;
                        //        data.Create_By = CookiesMenager.EmpID;
                        //        data.Update_By = CookiesMenager.EmpID;

                        //        service_charge.PaxFareTId = data.PaxFareTId.ToString();
                        //        MasterDA.PaxFareInsert(data);
                        //    }
                        //    service_charge.SeqmentNo = seg.SeqmentNo;
                        //}
                        service_charge.PaxFareTId = PaxFareTId;

                        service_charge.ChargeType = dt.Rows[0]["ChargeType"].ToString();
                        service_charge.ChargeCode = dt.Rows[0]["ChargeCode"].ToString();
                        service_charge.ChargeDetail = dt.Rows[0]["ChargeDetail"].ToString();
                        service_charge.TicketCode = dt.Rows[0]["TicketCode"].ToString();
                        service_charge.CollectType = dt.Rows[0]["CollectType"].ToString();
                        service_charge.CurrencyCode = dt.Rows[0]["CurrencyCode"].ToString();
                        if (ddlSegment.Items.FindByValue(PaxFareTId).Text.Contains("ADT"))
                        {
                            service_charge.PaxType = "ADT";
                            service_charge.qty = res_passenger.Count(x => x.PaxType == "ADT");
                        }
                        else
                        {
                            service_charge.PaxType = "CHD";
                            service_charge.qty = res_passenger.Count(x => x.PaxType == "CHD");
                        }
                        //service_charge.qty = 1;
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Amount"].ToString()))
                            service_charge.Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                        else
                            service_charge.Amount = 0;
                        service_charge.ForeignCurrencyCode = dt.Rows[0]["ForeignCurrencyCode"].ToString();
                        if (!string.IsNullOrEmpty(dt.Rows[0]["ForeignAmount"].ToString()))
                            service_charge.ForeignAmount = Convert.ToDecimal(dt.Rows[0]["ForeignAmount"]);
                        else
                            service_charge.ForeignAmount = 0;

                        service_charge.price = service_charge.Amount;
                        service_charge.FeeCreateDate = DateTime.Now;
                        service_charge.BaseAmount = 0;
                        service_charge.TaxInvoiceNo = dt.Rows[0]["ABBNo"].ToString();
                        service_charge.Create_By = CookiesMenager.EmpID;
                        service_charge.Update_By = CookiesMenager.EmpID;


                        if (service_charge.CurrencyCode == "THB")
                        {
                            service_charge.AmountTHB = (decimal)service_charge.Amount;
                        }
                        else
                        {
                            decimal Ex_rate = 1;
                            if (service_charge.CurrencyCode != string.Empty)
                               Ex_rate = MasterDA.GetCurrencyActiveDate(service_charge.CurrencyCode, service_charge.FeeCreateDate);
                            service_charge.AmountTHB = (decimal)service_charge.Amount * Ex_rate;
                        }

                        service_charge.TotalAmountTHB = service_charge.AmountTHB * service_charge.qty;

                        Session["res_fare"] = res_fare;
                        rptFare.DataSource = res_fare;
                        rptFare.DataBind();

                        BindingTotal();
                    }
                }

            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }


        protected void btnSaveFee_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidfFeeSaveVal.Value != "")
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(hidfFeeSaveVal.Value.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        string mode = dt.Rows[0]["Mode"].ToString();
                        FeeDetail service_charge;
                        if (mode == "NEW")
                        {
                            service_charge = new FeeDetail();
                            service_charge.RowState = 1;
                            service_charge.TableName = "T_PassengerFeeServiceChargeCurrentUpdate";
                            service_charge.RowID = Guid.NewGuid();
                            res_fee.Add(service_charge);
                        }
                        else
                        {
                            var RowId = Guid.Parse(dt.Rows[0]["PassengerFeeServiceChargeTId"].ToString());
                            service_charge = res_fee.First(x => x.RowID == RowId);
                            if (service_charge.RowState != 1)
                                service_charge.RowState = 2;
                        }
                        service_charge.PassengerId = Convert.ToInt64(dt.Rows[0]["PassengerId"].ToString());
                        if (service_charge.PassengerId != null)
                        {
                            T_PassengerCurrentUpdate pass = res_passenger.Where(it => it.PassengerId == service_charge.PassengerId).FirstOrDefault();
                            if (pass != null)
                            {
                                service_charge.FirstName = pass.FirstName;
                                service_charge.LastName = pass.LastName;
                            }
                        }

                        service_charge.ChargeType = dt.Rows[0]["ChargeType"].ToString();
                        service_charge.ChargeCode = dt.Rows[0]["ChargeCode"].ToString();
                        service_charge.ChargeDetail = dt.Rows[0]["ChargeDetail"].ToString();
                        service_charge.TicketCode = dt.Rows[0]["TicketCode"].ToString();
                        service_charge.CollectType = dt.Rows[0]["CollectType"].ToString();
                        service_charge.CurrencyCode = dt.Rows[0]["CurrencyCode"].ToString();
                        service_charge.qty = 1;
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Amount"].ToString()))
                            service_charge.Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                        else
                            service_charge.Amount = 0;
                        service_charge.ForeignCurrencyCode = dt.Rows[0]["ForeignCurrencyCode"].ToString();
                        if (!string.IsNullOrEmpty(dt.Rows[0]["ForeignAmount"].ToString()))
                            service_charge.ForeignAmount = Convert.ToDecimal(dt.Rows[0]["ForeignAmount"]);
                        else
                            service_charge.ForeignAmount = 0;

                        service_charge.price = service_charge.Amount;
                        service_charge.FeeCreateDate = DateTime.Now;
                        service_charge.BaseAmount = 0;
                        service_charge.TaxInvoiceNo = dt.Rows[0]["ABBNo"].ToString();
                        service_charge.Create_By = CookiesMenager.EmpID;
                        service_charge.Update_By = CookiesMenager.EmpID;


                        if (service_charge.CurrencyCode == "THB")
                        {
                            service_charge.AmountTHB = (decimal)service_charge.Amount;
                        }
                        else
                        {
                            decimal Ex_rate = 1;
                            if (service_charge.CurrencyCode != string.Empty)
                                Ex_rate = MasterDA.GetCurrencyActiveDate(service_charge.CurrencyCode, service_charge.FeeCreateDate);
                            service_charge.AmountTHB = (decimal)service_charge.Amount * Ex_rate;
                        }



                       // int index = Convert.ToInt32(dt.Rows[0]["index"]);
                        //if (res_fee.Count > index)
                        //{
                        //    FeeDetail fee = res_fee[index];

                        //    if (!string.IsNullOrEmpty(dt.Rows[0]["PassengerId"].ToString()))
                        //    {
                        //        fee.PassengerId = Convert.ToInt64(dt.Rows[0]["PassengerId"]);
                        //        T_PassengerCurrentUpdate pass = res_passenger.Where(it=> it.PassengerId.ToString() == dt.Rows[0]["PassengerId"].ToString()).FirstOrDefault();
                        //        fee.FirstName = pass.FirstName;
                        //        fee.LastName = pass.LastName;
                        //    }
                        //    fee.ChargeType = dt.Rows[0]["ChargeType"].ToString();
                        //    fee.ChargeCode = dt.Rows[0]["ChargeCode"].ToString();
                        //    fee.ChargeDetail = dt.Rows[0]["ChargeDetail"].ToString();
                        //    fee.TicketCode = dt.Rows[0]["TicketCode"].ToString();
                        //    fee.CollectType = dt.Rows[0]["CollectType"].ToString();
                        //    fee.CurrencyCode = dt.Rows[0]["CurrencyCode"].ToString();
                        //    if (!string.IsNullOrEmpty(dt.Rows[0]["Amount"].ToString()))
                        //        fee.Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                        //    else
                        //        fee.Amount = 0;
                        //    fee.ForeignCurrencyCode = dt.Rows[0]["ForeignCurrencyCode"].ToString();
                        //    if (!string.IsNullOrEmpty(dt.Rows[0]["ForeignAmount"].ToString()))
                        //        fee.ForeignAmount = Convert.ToDecimal(dt.Rows[0]["ForeignAmount"]);
                        //    else
                        //        fee.ForeignAmount = 0;

                        //    if (fee.RowState != 1)
                        //    {
                        //        fee.RowState = 2;
                        //        fee.Update_By = CookiesMenager.EmpID;
                        //    }
                        //}

                        
                        Session["res_fee"] = res_fee;
                        rptFee.DataSource = res_fee;
                        rptFee.DataBind();

                        BindingTotal();
                    }
                }

            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }


        protected void btnSavePayment_Click(object sender, EventArgs e)
        {
            try
            {

                if (hidfPaySaveVal.Value != "")
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(hidfPaySaveVal.Value.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        string mode = dt.Rows[0]["Mode"].ToString();
                        T_PaymentCurrentUpdate pay;
                        if (mode == "NEW")
                        {
                            pay = new T_PaymentCurrentUpdate();
                            pay.RowState = 1;
                            res_payment.Add(pay);
                        }
                        else
                        {
                            pay = res_payment.Where(it => it.PaymentTId.ToString() == dt.Rows[0]["PaymentTId"].ToString()).FirstOrDefault();
                            if (pay.RowState != 1)
                                pay.RowState = 2;
                        }
                        if (hidfBookingID.Value != string.Empty)
                            pay.ReferenceID = Convert.ToInt64(hidfBookingID.Value);
                        pay.ReferenceType = "Booking";
                        //pay.PaymentID = Convert.ToInt64(dt.Rows[0]["PaymentID"]);
                        pay.PaymentMethodCode = dt.Rows[0]["PaymentMethodCode"].ToString();
                        pay.PaymentMethodType = dt.Rows[0]["PaymentMethodType"].ToString();
                        pay.AgentCode = dt.Rows[0]["AgentCode"].ToString();
                        pay.ApprovalDate = DateTime.ParseExact(dt.Rows[0]["ApprovalDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        pay.CurrencyCode = dt.Rows[0]["CurrencyCode"].ToString();
                        pay.PaymentAmount = Convert.ToDecimal(dt.Rows[0]["PaymentAmount"]);
                        pay.CollectedCurrencyCode = dt.Rows[0]["CollectedCurrencyCode"].ToString();
                        pay.CollectedAmount = Convert.ToDecimal(dt.Rows[0]["CollectedAmount"]);

                        if (pay.RowState == 1) // New
                        {
                            pay.QuotedCurrencyCode = pay.CurrencyCode;
                            pay.QuotedAmount = pay.PaymentAmount;
                            pay.OrganizationCode = pay.AgentCode;
                            pay.Status = "Approved";
                            pay.ParentPaymentID = 0;
                            pay.DomainCode = "";
                            pay.LocationCode = "";
                        }

                        pay.Create_By = CookiesMenager.EmpID;
                        pay.Update_By = CookiesMenager.EmpID;
                        if (pay.CurrencyCode == "THB")
                        {
                            pay.PaymentAmountTHB = pay.PaymentAmount;
                        }
                        else
                        {
                            decimal ExChangeRate = MasterDA.GetCurrencyActiveDate(pay.CurrencyCode, pay.ApprovalDate);
                            pay.PaymentAmountTHB = pay.PaymentAmount * ExChangeRate;
                        }
                        pay.ABBNo = dt.Rows[0]["ABBNo"].ToString();
                        Session["res_payment"] = res_payment;
                        rptPayment.DataSource = res_payment;
                        rptPayment.DataBind();

                        decimal sum = (decimal)res_payment.Where(it => it.RowState != -1).Sum(it => it.PaymentAmountTHB);
                        lblTotalPay.Text = sum.ToString("#,###,##0.00");
                    }
                }

            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }

        private bool ValidateBeforeSave()
        {
            if (tbx_booking_no.Text == "")
            {
                return false;
            }

            decimal sumpayment = (decimal)res_payment.Sum(it => it.PaymentAmount);
            decimal sumpayment_sky = (decimal)res_payment_sky.Sum(it => it.PaymentAmount);

            decimal totalFare = Convert.ToDecimal(lbl_FareTotalFareTH.Text);
            decimal totalFee = Convert.ToDecimal(lblTotal.Text);
            decimal totalBookingAmount = totalFare + totalFee;

            decimal totalPay = Convert.ToDecimal(lblTotalPay.Text);

            bool IsSuccess = true;
            var strError = "พบข้อมูลผิดพลาด";

            if (totalBookingAmount != totalPay)
            {
                strError += "\\n- ยอดชำระเงินไม่เท่ากับยอดค่าใช้จ่ายทั้งหมด";
                IsSuccess = false;
            }

            if (bookingdata == null)
            {
                strError += "\\n- กรุณาดึงข้อมูลออนไลน์ก่อนบันทึกข้อมูล";
                IsSuccess = false;
            }
            else if (sumpayment > sumpayment_sky)
            {
                strError += "\\n- ยอด Payment ซ้าย ไม่สามารถมากกว่า ขวา";
                IsSuccess = false;
            }

            if (!IsSuccess)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('" + strError + "');", true);
            }

            return IsSuccess;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (!ValidateBeforeSave())
                {
                    return;
                }

                decimal sumpayment = (decimal)res_payment.Sum(it => it.PaymentAmount);
                decimal sumpayment_sky = (decimal)res_payment_sky.Sum(it => it.PaymentAmount);

                decimal totalFee = Convert.ToDecimal(lblTotal.Text) + Convert.ToDecimal(lbl_FareTotalFareTH.Text);
                decimal totalPay = Convert.ToDecimal(lblTotalPay.Text);

                Guid TrandsactionID = Guid.Parse(transactionid.Value);

                T_LogBookingCurrentUpdate log = new T_LogBookingCurrentUpdate();
                log.Create_By = CookiesMenager.EmpID;
                log.Create_Date = DateTime.Now;
                log.FormName = "UpdateBooking";
                log.TransactionId = TrandsactionID;
                log.PNR_No = tbx_booking_no.Text;

                bool success = false;
                string strRet = string.Empty;

                #region Save T_SegmentCurrentUpdate


                foreach (T_SegmentCurrentUpdate item in res_flight)
                {
                    log.ABBNo = item.ABBNo;
                    log.TableName = "T_SegmentCurrentUpdate";
                    if (item.RowState == 1)
                    {
                        item.TransactionId = TrandsactionID;
                        success = UpdateBookingDA.InsertFlight(item, ref strRet);
                        log.Method = "I";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == 2)
                    {
                        success = UpdateBookingDA.UpdateFlight(item, ref strRet);
                        log.Method = "U";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == -1)
                    {
                        success = UpdateBookingDA.DeleteFlight(item, ref strRet);
                        log.Method = "D";
                        log.Msg = strRet;
                    }

                    if (item.RowState != 0)
                    {
                        UpdateBookingDA.InsertLogBookingUpdate(log);
                    }

                }
                #endregion

                #region Save T_PassengerCurrentUpdate


                foreach (T_PassengerCurrentUpdate item in res_passenger)
                {
                    log.ABBNo = item.ABBNo;
                    log.TableName = "T_PassengerCurrentUpdate";
                    if (item.RowState == 1)
                    {
                        item.TransactionId = TrandsactionID;
                        item.PassengerId = UpdateBookingDA.GetGenId("PASSENGER", CookiesMenager.POS_Code, CookiesMenager.Station_Code, CookiesMenager.EmpID);
                        success = UpdateBookingDA.InsertPassenger(item, ref strRet);
                        log.Method = "I";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == 2)
                    {
                        success = UpdateBookingDA.UpdatePassenger(item, ref strRet);
                        log.Method = "U";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == -1)
                    {
                        success = UpdateBookingDA.DeletePassenger(item, ref strRet);
                        log.Method = "D";
                        log.Msg = strRet;
                    }

                    if (item.RowState != 0)
                    {
                        UpdateBookingDA.InsertLogBookingUpdate(log);
                    }
                }
                #endregion

                #region Save FareDetail

                foreach (FeeDetail item in res_fare)
                {
                    log.ABBNo = item.TaxInvoiceNo;
                    log.TableName = "T_BookingServiceChargeCurrentUpdate";
                    if (item.RowState == 1)
                    {
                        success = UpdateBookingDA.InsertFare(item, ref strRet);
                        log.Method = "I";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == 2)
                    {
                        success = UpdateBookingDA.UpdateFare(item, ref strRet);
                        log.Method = "U";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == -1)
                    {
                        success = UpdateBookingDA.DeleteFare(item, ref strRet);
                        log.Method = "D";
                        log.Msg = strRet;
                    }

                    if (item.RowState != 0)
                    {
                        UpdateBookingDA.InsertLogBookingUpdate(log);
                    }
                }

                #endregion

                #region Save FeeDetail


                foreach (FeeDetail item in res_fee)
                {
                    log.ABBNo = item.TaxInvoiceNo;

                    log.TableName = "T_PassengerFeeServiceChargeCurrentUpdate";
                    if (item.RowState == 1)
                    {
                        DataTable dt = UpdateBookingDA.GetPassengerFeeByPassengerId(item.PassengerId.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            item.PaxFareTId = dt.Rows[0]["PassengerFeeTId"].ToString();
                            item.FeeCreateDate = Convert.ToDateTime(dt.Rows[0]["FeeCreateDate"]);
                        }
                        item.ServiceChargeNo = item.PassengerId + item.ChargeCode + item.TicketCode + item.Amount;
                        item.BaseCurrencyCode = "";
                        item.BaseAmount = 0;
                        success = UpdateBookingDA.InsertFeeServiceCharge(item, ref strRet);
                        log.Method = "I";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == 2)
                    {
                        success = UpdateBookingDA.UpdateFeeServiceCharge(item, ref strRet);
                        log.Method = "U";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == -1)
                    {
                        success = UpdateBookingDA.DeleteFeeServiceCharge(item, ref strRet);
                        log.Method = "D";
                        log.Msg = strRet;
                    }



                    if (item.RowState != 0)
                    {
                        UpdateBookingDA.InsertLogBookingUpdate(log);
                    }
                }

                #endregion

                #region Save T_PaymentCurrentUpdate


                foreach (T_PaymentCurrentUpdate item in res_payment)
                {
                    log.ABBNo = item.ABBNo;
                    log.TableName = "T_PaymentCurrentUpdate";
                    if (item.RowState == 1)
                    {
                        item.TransactionId = TrandsactionID;
                        item.PaymentID = UpdateBookingDA.GetGenId("PaymentID", CookiesMenager.POS_Code, CookiesMenager.Station_Code, CookiesMenager.EmpID);
                        success = UpdateBookingDA.InsertPayment(item, ref strRet);
                        log.Method = "I";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == 2)
                    {
                        success = UpdateBookingDA.UpdatePayment(item, ref strRet);
                        log.Method = "U";
                        log.Msg = strRet;
                    }
                    else if (item.RowState == -1)
                    {
                        success = UpdateBookingDA.DeletePayment(item, ref strRet);
                        log.Method = "D";
                        log.Msg = strRet;
                    }

                    if (item.RowState != 0)
                    {
                        UpdateBookingDA.InsertLogBookingUpdate(log);
                    }
                }

                #endregion

                UpdateAbb();

                string BookingNo = tbx_booking_no.Text;
                DoClearData();
                tbx_booking_no.Text = BookingNo;
                InitData();




            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        
        }



        private void UpdateAbb()
        {
            //List<FeeDetail> lstFee = res_fee.Where(it => (it.RowState == 2 || it.RowState == -1) && it.TaxInvoiceNo != string.Empty).ToList();
            //List<T_PaymentCurrentUpdate> lstpay = res_payment.Where(it => (it.RowState == 2 || it.RowState == -1) && it.ABBNo != string.Empty).ToList();
            //if (lstFee.Count > 0 || lstpay.Count > 0)
            //{
            //    string[] AbbNo;
            //    string[] AbbNo1 = lstFee.Select(s => s.TaxInvoiceNo).Distinct().ToArray();
            //    string[] AbbNo2 = lstFee.Select(s => s.TaxInvoiceNo).Distinct().ToArray();

            //    AbbNo = AbbNo1.Union(AbbNo2).ToArray<string>();
            //    for (int i = 0; i < AbbNo.Length; i++)
            //    {
            //        CreateABB.UpdateABB(AbbNo[i]);
            //    }
            //}
            UpdateBookingDA.ManagePaxFare(tbx_booking_no.Text, CookiesMenager.EmpID);

            bool success = false;
            foreach (T_ABB item in res_abb)
            {
                success = CreateABB.UpdateABB(item.TaxInvoiceNo, CookiesMenager.EmpID, CookiesMenager.POS_Code);
            }

        }

        #endregion

        protected void btnORABB_Click(object sender, EventArgs e)
        {
            string ABB_No = hidfORABB.Value.ToString();
            if (ABB_No != string.Empty)
            {
                UpdateBookingDA.UpdateAbbInactiveByABB_No(ABB_No,CookiesMenager.EmpID);
                InitData();
            }
        }

        protected void rptFare_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FeeDetail item = (FeeDetail)e.Item.DataItem;

                Image FareImgAction = (Image)e.Item.FindControl("FareImgAction");
                Label lbl_FarePaxType = (Label)e.Item.FindControl("lbl_FarePaxType");
                Label lbl_FareChargeCode = (Label)e.Item.FindControl("lbl_FareChargeCode");
                Label lbl_FareQuantity = (Label)e.Item.FindControl("lbl_FareQuantity");
                Label lbl_FareCurrency = (Label)e.Item.FindControl("lbl_FareCurrency");
                Label lbl_FareAmount = (Label)e.Item.FindControl("lbl_FareAmount");
                Label lbl_FareExchangeRateTH = (Label)e.Item.FindControl("lbl_FareExchangeRateTH");
                Label lbl_FareDiscountTH = (Label)e.Item.FindControl("lbl_FareDiscountTH");
                Label lbl_FareAmountTH = (Label)e.Item.FindControl("lbl_FareAmountTH");
                Label lbl_FareTotalTH = (Label)e.Item.FindControl("lbl_FareTotalTH");
                Label lbl_FeeABBNo = (Label)e.Item.FindControl("lbl_FeeABBNo");
                Label lbl_FareCreateDate = (Label)e.Item.FindControl("lbl_FareCreateDate");
                Image FareImgDel = (Image)e.Item.FindControl("FareImgDel");

                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                FareImgAction.ImageUrl = "Images/Text-Edit-icon.png";
                var strEvent = String.Format("'EDIT','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}'",
                    tr.ClientID, item.RowNo, item.RowID, item.PaxFareTId.ToLower(), item.PaxType, 
                    item.ChargeType, item.ChargeCode, item.ChargeDetail, item.TicketCode, item.CollectType, 
                    item.CurrencyCode, ((decimal)item.Amount).ToString("0.00"), item.ForeignCurrencyCode, item.ForeignAmount.ToString("0.00"), item.TaxInvoiceNo
                    );
                //string strEvent = "'EDIT','" + tr.ClientID + "','" + item.RowNo + "','" + item.RowID + "','" + item.SegmentTId + "','" + item.PassengerId + "','" + item.ChargeType + "','" + item.ChargeCode + "','" + item.ChargeDetail + "','" + item.TicketCode + "','" + item.CollectType + "','" + item.CurrencyCode + "','" + ((decimal)item.Amount).ToString("0.00") + "','" + item.ForeignCurrencyCode + "','" + item.ForeignAmount.ToString("0.00") + "','" + item.TaxInvoiceNo + "'";
                FareImgAction.Attributes.Add("onclick", "editfare(" + strEvent + ");");
                if (item.RowState == 1) FareImgAction.ImageUrl = "Images/add.png";
                else if (item.RowState == -1) tr.Style.Add("display", "none");

                lbl_FarePaxType.Text = item.PaxType;
                lbl_FareChargeCode.Text = item.ChargeType != "FarePrice" ? item.ChargeCode : "Base Fare";
                lbl_FareQuantity.Text = item.qty.ToString("#,###");
                lbl_FareCurrency.Text = item.CurrencyCode;
                lbl_FareExchangeRateTH.Text = item.Exc.GetValueOrDefault(1).ToString("#,###,##0.00");
                lbl_FareAmount.Text = item.Amount.GetValueOrDefault().ToString("#,###,##0.00");
                lbl_FareDiscountTH.Text = item.Discount.GetValueOrDefault().ToString("#,###,##0.00");
                lbl_FareAmountTH.Text = item.AmountTHB.ToString("#,###,##0.00");
                lbl_FareTotalTH.Text = item.TotalAmountTHB.ToString("#,###,##0.00");
                lbl_FeeABBNo.Text = item.TaxInvoiceNo;
                lbl_FareCreateDate.Text = item.FareCreateDate.ToString("dd-MM-yyyy");
                FareImgDel.Attributes.Add("onclick", "$('#hidfDelFare').val('" + item.RowID + "');$('#btn_DeleteFare').click();");

            }
        }

        protected void rpt_SkyFare_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FeeDetail item = (FeeDetail)e.Item.DataItem;

                Image SkyFareImgAction = (Image)e.Item.FindControl("SkyFareImgAction");
                Label lbl_SkyFarePaxType = (Label)e.Item.FindControl("lbl_SkyFarePaxType");
                Label lbl_SkyFareChargeCode = (Label)e.Item.FindControl("lbl_SkyFareChargeCode");
                Label lbl_SkyFareQuantity = (Label)e.Item.FindControl("lbl_SkyFareQuantity");
                Label lbl_SkyFareCurrency = (Label)e.Item.FindControl("lbl_SkyFareCurrency");
                Label lbl_SkyFareAmount = (Label)e.Item.FindControl("lbl_SkyFareAmount");
                Label lbl_SkyFareExchangeRateTH = (Label)e.Item.FindControl("lbl_SkyFareExchangeRateTH");
                Label lbl_SkyFareDiscountTH = (Label)e.Item.FindControl("lbl_SkyFareDiscountTH");
                Label lbl_SkyFareAmountTH = (Label)e.Item.FindControl("lbl_SkyFareAmountTH");
                Label lbl_SkyFareTotalTH = (Label)e.Item.FindControl("lbl_SkyFareTotalTH");

                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                //FareImgAction.ImageUrl = "Images/Text-Edit-icon.png";
                //string strEvent = "'EDIT','" + tr.ClientID + "','" + item.RowNo + "','" + item.RowID + "','" + item.SegmentTId + "','" + item.PassengerId + "','" + item.ChargeType + "','" + item.ChargeCode + "','" + item.ChargeDetail + "','" + item.TicketCode + "','" + item.CollectType + "','" + item.CurrencyCode + "','" + ((decimal)item.Amount).ToString("0.00") + "','" + item.ForeignCurrencyCode + "','" + item.ForeignAmount.ToString("0.00") + "','" + item.TaxInvoiceNo + "'";
                //FareImgAction.Attributes.Add("onclick", "editfare(" + strEvent + ");");
                //if (item.RowState == 1) FareImgAction.ImageUrl = "Images/add.png";
                //else if (item.RowState == -1) tr.Style.Add("display", "none");
                SkyFareImgAction.Attributes.Add("onclick", " $('#hidfFareSkyUpdate').val('" + item.RowID + "');  $('#btn_FareSkyUpdate').click();");

                lbl_SkyFarePaxType.Text = item.PaxType;
                lbl_SkyFareChargeCode.Text = item.ChargeCode;
                lbl_SkyFareQuantity.Text = item.qty.ToString("#,###");
                lbl_SkyFareCurrency.Text = item.CurrencyCode;
                lbl_SkyFareExchangeRateTH.Text = item.Exc.GetValueOrDefault(1).ToString("#,###,##0.00");
                lbl_SkyFareAmount.Text = item.Amount.GetValueOrDefault().ToString("#,###,##0.00");
                lbl_SkyFareDiscountTH.Text = item.Discount.GetValueOrDefault().ToString("#,###,##0.00");
                lbl_SkyFareAmountTH.Text = item.AmountTHB.ToString("#,###,##0.00");
                lbl_SkyFareTotalTH.Text = item.TotalAmountTHB.ToString("#,###,##0.00");

            }
        }


        private void RecalFare()
        {
            MasterDA DA = new MasterDA();

            var passengerOri = DA.GetPassengerList(Guid.Parse(this.transactionid.Value));

            var ADTCountOri = passengerOri.Count(x => x.PaxType == "ADT");
            var CHDCountOri = passengerOri.Count(x => x.PaxType == "CHD");

            var ADTCount = res_passenger.Count(x => x.PaxType == "ADT");
            var CHDCount = res_passenger.Count(x => x.PaxType == "CHD");

            foreach (var fare in res_fare.Where(x => x.PaxType == "ADT"))
            {
                if (ADTCountOri != ADTCount && fare.RowState == 0)
                {
                    fare.RowState = 2;
                }
                fare.qty = ADTCount;
                fare.AmountTHB = fare.Amount.GetValueOrDefault(0) * fare.Exc.GetValueOrDefault(1);
                fare.TotalAmountTHB = fare.AmountTHB * fare.qty;
            }

            foreach (var fare in res_fare.Where(x => x.PaxType == "CHD"))
            {
                if (CHDCountOri != CHDCount && fare.RowState == 0)
                {
                    fare.RowState = 2;
                }
                fare.qty = CHDCount;
                fare.AmountTHB = fare.Amount.GetValueOrDefault(0) * fare.Exc.GetValueOrDefault(1);
                fare.TotalAmountTHB = fare.AmountTHB * fare.qty;
            }

            Session["res_fare"] = res_fare;
            rptFare.DataSource = res_fare;
            rptFare.DataBind();

            BindingTotal();
        }

        private void BindingTotal()
        {
            var Fare = res_fare.Where(it => it.RowState != -1);
            decimal TotalFareAmountOri = (decimal)Fare.Sum(it => it.Amount.GetValueOrDefault());
            decimal TotalFareAmountTH = (decimal)Fare.Sum(it => it.AmountTHB);
            decimal TotalFareTH = (decimal)Fare.Sum(it => it.TotalAmountTHB);
            lbl_FareTotalFareAmountOri.Text = TotalFareAmountOri.ToString("#,###,##0.00");
            lbl_FareTotalFareAmountTH.Text = TotalFareAmountTH.ToString("#,###,##0.00");
            lbl_FareTotalFareTH.Text = TotalFareTH.ToString("#,###,##0.00");

            var Fee = res_fee.Where(it => it.RowState != -1);
            decimal TotalFeeTH = (decimal)Fee.Sum(it => it.AmountTHB);
            lblTotal.Text = TotalFeeTH.ToString("#,###,##0.00");

            lblTotalFareFee.Text = (TotalFareTH + TotalFeeTH).ToString("#,###,##0.00");
        }

        protected void btn_DeleteFare_Click(object sender, EventArgs e)
        {
            if (hidfDelFare.Value != "")
            {
                var RowId = Guid.Parse(hidfDelFare.Value);
                FeeDetail fare = res_fare.FirstOrDefault(x => x.RowID == RowId);

                if (fare != null)
                {
                    if (fare.RowState == 1)
                    {
                        res_fare.Remove(fare);
                    }
                    else
                    {
                        fare.RowState = -1;
                    }

                    Session["res_fare"] = res_fare;
                    rptFare.DataSource = res_fare;
                    rptFare.DataBind();

                    BindingTotal();
                }
                
            }
        }

        protected void btn_FareSkyUpdate_Click(object sender, EventArgs e)
        {
            if (hidfFareSkyUpdate.Value != "" && bookingdata != null)
            {
                var RowId = Guid.Parse(hidfFareSkyUpdate.Value.ToString());

                

                if (this.res_fare_sky.Count(x => x.RowID == RowId) > 0)
                {
                    FeeDetail item = this.res_fare_sky.First(x => x.RowID == RowId);

                    item.RowState = 1;
                    item.Create_By = CookiesMenager.EmpID;
                    item.Update_By = CookiesMenager.EmpID;
                    item.FareCreateDate = item.FeeCreateDate;
                    if (res_fare.Count(x => x.SeqmentPaxNo == item.SeqmentPaxNo) > 0)
                    {
                        item.PaxFareTId = res_fare.First(x => x.SeqmentPaxNo == item.SeqmentPaxNo).PaxFareTId;
                    }

                    res_fare.Add(item);

                    for (int i = 0; i < res_fare.Count; i++)
                        res_fare[i].RowNo = i;

                    Session["res_fare"] = res_fare;
                    rptFare.DataSource = res_fare;
                    rptFare.DataBind();

                    BindingTotal();
                }
                

            }
        }
    }
}