using CreateInvoiceSystem.DAO;
using CreateInvoiceSystem.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace CreateInvoiceSystem
{
    public partial class InvoiceCN_Info : BasePage
    {
        public MasterDA _MasterDA = new MasterDA();

        public T_BookingCurrentUpdate _BookingCurrentUpdate = null;
        public T_Invoice_Info _Invoice_Info = null;
        public string INV_No = string.Empty;
        public string BookingNo = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            INV_No = this.Request["INV_No"].ToString();
            BookingNo = this.Request["Booking"].ToString();

            var context = new POSINVEntities();
            _Invoice_Info = context.T_Invoice_Info.Where(x => x.INV_No == INV_No).First();
            _BookingCurrentUpdate = _MasterDA.GetBooking(BookingNo);
            if (!IsPostBack)
            {



                hidfTranID.Value = _BookingCurrentUpdate.TransactionId.ToString();
                txtBookingNo.Text = BookingNo;
                tbx_date_booking.Text = _BookingCurrentUpdate.Booking_Date.ToString("dd/MM/yyyy");
                var ABBNos = String.Join(",", _Invoice_Info.T_ABB.Select(x => x.TaxInvoiceNo).ToList());
                tbx_pos_no.Text = ABBNos != string.Empty ? ABBNos : _Invoice_Info.ABB_NO;

                tbx_cashier_no.Text = CookiesMenager.EmpID;
                tbx_create_by.Text = CookiesMenager.EmpID;
                tbx_create_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DoLoadData();

                DataTable dtReason = InvoiceCNDA.Get_M_CNReason("AND IsActive ='Y' ");
                Session["M_CNReason"] = dtReason;
                ddlReason.DataSource = dtReason;
                ddlReason.DataTextField = "CNReason_Name";
                ddlReason.DataValueField = "CNReason_Id";
                ddlReason.DataBind();
            }
        }

        private void DoLoadData()
        {
            try
            {

                DataTable dt = InvoiceDA.GetInvoiceByINVNo(INV_No);
                if (dt.Rows.Count > 0)
                {
                    var INV_ID = Guid.Parse(dt.Rows[0]["INV_ID"].ToString());
                    hidfCustCode.Value = dt.Rows[0]["CustomerID"].ToString();
                    txtCustCode.Text = dt.Rows[0]["CustomerID"].ToString();
                    txtFullName.Text = dt.Rows[0]["first_name"].ToString(); //+ " " + dt.Rows[0]["last_name"].ToString();
                    tbx_booking_taxid.Text = dt.Rows[0]["TaxID"].ToString();
                    tbx_booking_address1.Text = dt.Rows[0]["Address"].ToString();
                    //tbx_booking_address2.Text = dt.Rows[0]["Addr_2"].ToString();
                    //tbx_booking_address3.Text = dt.Rows[0]["Addr_3"].ToString();
                    txtInvoiceNo.Text = dt.Rows[0]["INV_No"].ToString();
                    tbx_create_date.Text = Convert.ToDateTime(dt.Rows[0]["Create_Date"]).ToString("dd/MM/yyyy");
                    tbx_pos_no1.Text = dt.Rows[0]["POS_Code"].ToString();
                    txtBranchNo.Text = dt.Rows[0]["BranchNo"].ToString();
                    hidfOpenMode.Value = "EDIT";
                    hidfInvoiceNo.Value = dt.Rows[0]["INV_No"].ToString();
                    hidfRowID.Value = dt.Rows[0]["TransactionId"].ToString();
                    if (dt.Rows[0]["Status"] != null && dt.Rows[0]["Status"].ToString() != string.Empty)
                        lblStatus.Text = dt.Rows[0]["Status"].ToString();
                    else
                        lblStatus.Text = "Full Tax";
                    if (dt.Rows[0]["Update_Date"] != null)
                        tbx_update_date.Text = Convert.ToDateTime(dt.Rows[0]["Update_Date"]).ToString("dd/MM/yyyy");
                    if (dt.Rows[0]["UpDate_By"] != null)
                        tbx_update_by.Text = dt.Rows[0]["UpDate_By"].ToString();

                    

                    if (dt.Rows[0]["CN_ID"].ToString() != string.Empty)
                    {
                        DataTable dtCN = InvoiceCNDA.GetInvoiceCN_ByCNID(dt.Rows[0]["CN_ID"].ToString());
                        if (dtCN.Rows.Count > 0)
                        {
                            //btnCN.Enabled = false;
                            hidfCN_ID.Value = dtCN.Rows[0]["CN_ID"].ToString();
                            divCN_History.Visible = true;
                            rptCN.DataSource = dtCN;
                            rptCN.DataBind();


                        }
                    }

                    res_flight = InvoiceDA.GetInvoiceFlight(INV_ID);
                    res_passenger = InvoiceDA.GetInvoicePassenger(INV_ID);
                    



                    #region Fare

                    res_fare = InvoiceDA.GetInvoiceFare(INV_ID);
                    rptFare.DataSource = res_fare;
                    rptFare.DataBind();
                    decimal TotalFareAmountOri = (decimal)res_fare.Sum(it => it.Amount.GetValueOrDefault());
                    decimal TotalFareAmountTH = (decimal)res_fare.Sum(it => it.AmountTHB);
                    decimal TotalFareTH = (decimal)res_fare.Sum(it => it.TotalAmountTHB);
                    lbl_TotalFareAmountOri.Text = TotalFareAmountOri.ToString("#,###,##0.00");
                    lbl_TotalFareAmountTH.Text = TotalFareAmountTH.ToString("#,###,##0.00");
                    lbl_TotalFareTH.Text = TotalFareTH.ToString("#,###,##0.00");

                    #endregion

                    #region Fee

                    res_fee = InvoiceDA.GetInvoiceFee(INV_ID);
                    rptFee.DataSource = res_fee;
                    rptFee.DataBind();
                    decimal FeeTotalAmonut = (decimal)res_fee.Sum(it => it.Amount);
                    decimal FeeTotalAmonutTH = (decimal)res_fee.Sum(it => it.AmountTHB);
                    lbl_FeeTotalAmonut.Text = FeeTotalAmonut.ToString("#,###,##0.00");
                    lbl_FeeTotalAmonutTH.Text = FeeTotalAmonutTH.ToString("#,###,##0.00");

                    #endregion

                    lbl_TotalFareAndFee.Text = (TotalFareTH + FeeTotalAmonutTH).ToString("#,###,##0.00");

                    #region MyRegion
                    
                    res_payment = InvoiceDA.GetInvoicePayment(INV_ID);
                    rptPayment.DataSource = res_payment;
                    rptPayment.DataBind();

                    #endregion
                }




                
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }

        private void getData()
        {
            MasterDA DA = new MasterDA();
            var key = this.Request["key"].ToString();
            var res = MasterDA.GetABBByInvoice(key);
            if (res != null)
            {
                //this.tbx_booking_date.Text = res.Booking_Date.ToString("dd/MM/yyyy");


                res_passenger = DA.GetPassengerList(res.TransactionId);

                res_fee = MasterDA.GetFeeDetail(res.PNR_No);

                res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);

                res_abb = DA.GetABBList(res.PNR_No);
                //GetLabelLinkList();
            }
        }
        public List<T_SegmentCurrentUpdate> res_flight { get; set; }
        public List<T_PassengerCurrentUpdate> res_passenger { get; set; }
        public List<FeeDetail> res_fare { get; set; }
        public List<FeeDetail> res_fee { get; set; }
        public List<T_PaymentCurrentUpdate> res_payment { get; set; }
        public List<T_ABB> res_abb { get; set; }

        protected void rptCustomer_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                M_Customer item = (M_Customer)e.Item.DataItem;
                Image imgAction = (Image)e.Item.FindControl("imgAction");
                Label lblName = (Label)e.Item.FindControl("lblName");
                HiddenField hidfCusVal = (HiddenField)e.Item.FindControl("hidfCusVal");
                lblName.Text = item.first_name; //+ " " + item.last_name;
                imgAction.ImageUrl = "Images/Text-Edit-icon.png";
                imgAction.Attributes.Add("onclick", "ChooseCus('" + hidfCusVal.ClientID + "');");

                hidfCusVal.Value =  "|" + item.prfx_name + "|" + item.first_name + "|" + "" + "|" + item.TaxID + "|" + item.Addr_1 + "|" + item.Addr_2 + "|" + item.Addr_3;
            }
        }

        protected void rptFare_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FeeDetail item = (FeeDetail)e.Item.DataItem;

                Label lbl_PaxType = (Label)e.Item.FindControl("lbl_PaxType");
                Label lbl_ChargeCode = (Label)e.Item.FindControl("lbl_ChargeCode");
                Label lbl_Quantity = (Label)e.Item.FindControl("lbl_Quantity");
                Label lbl_Currency = (Label)e.Item.FindControl("lbl_Currency");
                Label lbl_Amount = (Label)e.Item.FindControl("lbl_Amount");
                Label lbl_ExchangeRateTH = (Label)e.Item.FindControl("lbl_ExchangeRateTH");
                Label lbl_DiscountTH = (Label)e.Item.FindControl("lbl_DiscountTH");
                Label lbl_AmountTH = (Label)e.Item.FindControl("lbl_AmountTH");
                Label lbl_TotalTH = (Label)e.Item.FindControl("lbl_TotalTH");
                Label lbl_Group = (Label)e.Item.FindControl("lbl_Group");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                lbl_PaxType.Text = item.PaxType;
                lbl_ChargeCode.Text = item.ChargeCode;
                lbl_Quantity.Text = item.qty.ToString("#,###");
                lbl_Currency.Text = item.CurrencyCode;
                lbl_ExchangeRateTH.Text = item.Exc.GetValueOrDefault(1).ToString("#,###,##0.00");
                lbl_Amount.Text = item.Amount.GetValueOrDefault().ToString("#,###,##0.00");
                lbl_DiscountTH.Text = item.Discount.GetValueOrDefault().ToString("#,###,##0.00");
                lbl_AmountTH.Text = item.AmountTHB.ToString("#,###,##0.00");
                lbl_TotalTH.Text = item.TotalAmountTHB.ToString("#,###,##0.00");
                lbl_Group.Text = item.AbbGroup;

            }
        }

        protected void rptFee_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FeeDetail item = (FeeDetail)e.Item.DataItem;

                Label lbl_PassengerName = (Label)e.Item.FindControl("lbl_PassengerName");
                Label lbl_FeeChargeCode = (Label)e.Item.FindControl("lbl_FeeChargeCode");
                Label lbl_FeeQuantity = (Label)e.Item.FindControl("lbl_FeeQuantity");
                Label lbl_FeeCurrency = (Label)e.Item.FindControl("lbl_FeeCurrency");
                Label lbl_FeeExchangeRateTH = (Label)e.Item.FindControl("lbl_FeeExchangeRateTH");
                Label lbl_FeeAmount = (Label)e.Item.FindControl("lbl_FeeAmount");
                Label lbl_FeeDiscountTH = (Label)e.Item.FindControl("lbl_FeeDiscountTH");
                Label lbl_FeeAmountTH = (Label)e.Item.FindControl("lbl_FeeAmountTH");
                Label lbl_FeeGroup = (Label)e.Item.FindControl("lbl_FeeGroup");

                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                lbl_PassengerName.Text = item.FirstName + " " + item.LastName;
                lbl_FeeChargeCode.Text = item.ChargeCode;
                lbl_FeeQuantity.Text = item.qty.ToString("#,##0");
                lbl_FeeCurrency.Text = item.CurrencyCode;
                lbl_FeeExchangeRateTH.Text = item.Exc.GetValueOrDefault().ToString("#,##0.00");
                lbl_FeeAmount.Text = item.Amount.GetValueOrDefault().ToString("#,##0.00");
                lbl_FeeDiscountTH.Text = item.Discount.GetValueOrDefault().ToString("#,##0.00");
                lbl_FeeAmountTH.Text = item.AmountTHB.ToString("#,##0.00");
                lbl_FeeGroup.Text = item.AbbGroup;

            }

        }

        protected void rptPayment_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                T_PaymentCurrentUpdate item = (T_PaymentCurrentUpdate)e.Item.DataItem;

                Label lblPaymentAgent = (Label)e.Item.FindControl("lblPaymentAgent");
                Label lblPaymentDate = (Label)e.Item.FindControl("lblPaymentDate");
                Label lblPaymentType = (Label)e.Item.FindControl("lblPaymentType");
                Label lblCurrency = (Label)e.Item.FindControl("lblCurrency");
                Label lblPaymentAmount = (Label)e.Item.FindControl("lblPaymentAmount");
                Label lblPaymentAmountTHB = (Label)e.Item.FindControl("lblPaymentAmountTHB");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                lblPaymentAgent.Text = item.AgentCode;
                lblPaymentDate.Text = item.ApprovalDate.ToString("dd/MM/yyyy");
                lblPaymentType.Text = item.PaymentMethodType;
                lblCurrency.Text = item.CurrencyCode;
                lblPaymentAmount.Text = item.PaymentAmount.ToString("#,###,##0.00");
                lblPaymentAmountTHB.Text = item.PaymentAmountTHB.ToString("#,###,##0.00");

            }
        }

        protected void rptCN_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView item = (DataRowView)e.Item.DataItem;

                Label lblCN_No = (Label)e.Item.FindControl("lblCN_No");
                Label lblCN_Date = (Label)e.Item.FindControl("lblCN_Date");
                Label lblReason = (Label)e.Item.FindControl("lblReason");
                Label lblCreateBy = (Label)e.Item.FindControl("lblCreateBy");


                lblCN_No.Text = item["CN_NO"].ToString();
                lblCN_Date.Text = Convert.ToDateTime(item["Create_Date"]).ToString("dd/MM/yyyy");
                lblReason.Text = item["CNReason_Name"].ToString();
                lblCreateBy.Text = item["Create_By"].ToString();

            }
        }


        public string TableFlight()
        {
            MasterDA DA = new MasterDA();
            //var key = this.Request["key"].ToString();
            //var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_flight = DA.GetFlightList(_BookingCurrentUpdate.TransactionId);
            }
            string htmlStr = "";
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
                        "<tr><td>" + item.CarrierCode + item.FlightNumber + @"</td>
                        <td>" + ss + @"</td>
                        <td>" + item.DepartureStation + @"</td>
                        <td>" + item.ArrivalStation + @"</td>
                        <td>" + item.STD.ToString("HH:mm") + @"</td>
                        <td>" + item.STA.ToString("HH:mm") + @"</td>
                        </tr>";
                }
            }
            return htmlStr;
        }
        public string TablePassenger()
        {
            MasterDA DA = new MasterDA();
            //var key = this.Request["key"].ToString();
            //var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_passenger = DA.GetPassengerList(_BookingCurrentUpdate.TransactionId);
            }
            string htmlStr = "";
            int count = 0;
            if (res_flight != null)
            {
                foreach (var item in res_passenger)
                {
                    htmlStr +=
                        @"<tr>
                        <td>" + (count + 1).ToString() + @"</td>
                        <td>" + item.FirstName + " " + item.LastName + @"</td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }
        public string TableFee()
        {
            MasterDA DA = new MasterDA();
            //var key = this.Request["key"].ToString();
            //var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_flight = DA.GetFlightList(_BookingCurrentUpdate.TransactionId);
            }
            string htmlStr = "";
            int count = 0;
            if (res_fee != null)
            {
                foreach (var item in res_fee)
                {
                    htmlStr +=
                        @"<tr>
                        <td>" + item.ChargeCode + @"</td>
                        <td>" + item.qty.ToString("#,##0") + @"</td>
                        <td>" + item.price.Value.ToString("#,##0.00") + @"</td>
                        <td>" + item.Discount.Value.ToString("#,##0.00") + @"</td>
                        <td>" + item.amt.Value.ToString("#,##0.00") + @"</td>
                        <td>" + item.AbbGroup + @"</td>
                        <td>" + item.TaxInvoiceNo + @"</td>
                        </tr>";
                }
            }
            return htmlStr;
        }
        public string TablePayment()
        {

            string htmlStr = "";
            int count = 0;
            if (res_payment != null)
            {
                foreach (var item in res_payment)
                {
                    htmlStr +=
                        @"<tr>
                        <td>" + item.AgentCode + @"</td>
                        <td>" + item.ApprovalDate.ToString("dd/MM/yyyy") + @"</td>
                        <td>" + item.PaymentMethodCode + @"</td>
                        <td>" + item.CurrencyCode + @"</td>
                        <td>" + item.PaymentAmount.ToString("#,##0.00") + @"</td>
                        <td>" + item.PaymentAmountTHB.ToString("#,##0.00") + @"</td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }



        protected void btnSaveNPrint_Click(object sender, EventArgs e)
        {
            try
            {

                T_Invoice_CN invCN = new T_Invoice_CN();
                invCN.CN_ID = Guid.NewGuid();
                invCN.TransactionId = Guid.Parse(hidfTranID.Value);
                invCN.INV_No = txtInvoiceNo.Text;
                invCN.PNR_No = txtBookingNo.Text;
                invCN.CN_NO = InvoiceDA.GenerateRunning(InvoiceDA.RunningType.CN, tbx_pos_no1.Text);
                //string val = ddlReason.SelectedValue.ToString();
                string val = hidfReasonID.Value;
                DataTable dtReason = (DataTable)Session["M_CNReason"];
                if (dtReason != null)
                {
                    DataRow[] dr = dtReason.Select("CNReason_Id =" + val);
                    if (dr.Length > 0)
                    {
                        invCN.CNReason_Id = Convert.ToInt32(val);
                        invCN.CNReason_Code = dr[0]["CNReason_Code"].ToString();
                    }
                }
                invCN.Create_By = CookiesMenager.EmpID;


                if (hidfCN_ID.Value != string.Empty)
                {
                    invCN.CN_ID = Guid.Parse(hidfCN_ID.Value.ToString());
                    InvoiceCNDA.UpdateInvoiceCN(invCN);
                }
                else
                {
                    InvoiceCNDA.InsertInvoiceCN(invCN);
                    InvoiceDA.UpdateInvoice_ByCN(invCN);
                    //AbbDA.UpdateABB_Inactive(hidfTranID.Value.ToString(), invCN.PNR_No);
                    InvoiceDA.UpdateRunningInvoiceNo("CN", invCN.CN_NO, CookiesMenager.POS_Code, CookiesMenager.EmpID);

                    InvoiceDA.UpdateABB_INV_Id(invCN.INV_No);
                }

                if (invCN.CN_ID.ToString() != string.Empty)
                {
                    DataTable dtCN = InvoiceCNDA.GetInvoiceCN_ByCNID(invCN.CN_ID.ToString());
                    if (dtCN.Rows.Count > 0)
                    {
                        //btnCN.Enabled = false;
                        hidfCN_ID.Value = dtCN.Rows[0]["CN_ID"].ToString();
                        lblStatus.Text = "CN";
                        divCN_History.Visible = true;
                        rptCN.DataSource = dtCN;
                        rptCN.DataBind();
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSuccess", "SaveSuccess('" + invCN.INV_No + "')", true);

            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceCNList.aspx");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtBookingNo.Text != string.Empty && tbx_pos_no.Text != string.Empty)
                Response.Redirect("~/fulltax_form.aspx?BookingNo=" + txtBookingNo.Text + "&AbbNo=" + tbx_pos_no.Text);
        }

        protected void btnSelCust_Click(object sender, EventArgs e)
        {
            string str = hidfSelCus.Value;
        }

    }
}