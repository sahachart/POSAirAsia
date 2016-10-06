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
    public partial class invoice_info : BasePage
    {
        public MasterDA _MasterDA = new MasterDA();
        public T_BookingCurrentUpdate _BookingCurrentUpdate = null;
        public string abb_no = string.Empty;
        public string PNR_NO = string.Empty;
        public string INV_No = string.Empty;

        public List<T_SegmentCurrentUpdate> res_flight { get; set; }
        public List<T_PassengerCurrentUpdate> res_passenger { get; set; }
        public List<FeeDetail> res_fare { get; set; }
        public List<FeeDetail> res_fee { get; set; }
        public List<T_PaymentCurrentUpdate> res_payment { get; set; }
        public List<T_ABB> res_abb { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            abb_no = this.Request["key"].ToString();
            PNR_NO = this.Request["PNR_NO"].ToString();
            INV_No = this.Request["INV_No"] != null ? this.Request["INV_No"].ToString() : string.Empty;
            _BookingCurrentUpdate = _MasterDA.GetBooking(PNR_NO);

            if (!IsPostBack)
            {
                BindProv();


                txtInvoiceNo.Text = INV_No;


                hidfTranID.Value = _BookingCurrentUpdate.TransactionId.ToString();
                txtBookingNo.Text = PNR_NO;
                tbx_date_booking.Text = _BookingCurrentUpdate.Booking_Date.ToString("dd/MM/yyyy");
                tbx_pos_no.Text = abb_no;


                tbx_cashier_no.Text = CookiesMenager.EmpID;
                tbx_create_by.Text = CookiesMenager.EmpID;
                tbx_create_date.Text = DateTime.Now.ToString("dd/MM/yyyy");


                DoLoadData();

            }
        }

        private void DoLoadData()
        {
            try
            {
                string BookingNo = txtBookingNo.Text;

                DataTable dt = InvoiceDA.GetInvoiceByINVNo(INV_No);
                if (dt.Rows.Count > 0)
                {
                    var INV_ID = Guid.Parse(dt.Rows[0]["INV_ID"].ToString());
                    hidfCustCode.Value = dt.Rows[0]["CustomerID"].ToString();
                    txtCustCode.Text = dt.Rows[0]["CustomerID"].ToString();
                    txtFullName.Text = dt.Rows[0]["first_name"].ToString();// +" " + dt.Rows[0]["last_name"].ToString();
                    tbx_booking_taxid.Text = dt.Rows[0]["TaxID"].ToString();
                    tbx_booking_address1.Text = dt.Rows[0]["Address"].ToString();
                    //tbx_booking_address2.Text = dt.Rows[0]["Addr_2"].ToString();
                    //tbx_booking_address3.Text = dt.Rows[0]["Addr_3"].ToString();
                    //txtInvoiceNo.Text = dt.Rows[0]["INV_No"].ToString();
                    tbx_create_date.Text = Convert.ToDateTime(dt.Rows[0]["Create_Date"]).ToString("dd/MM/yyyy");
                    tbx_pos_no1.Text = dt.Rows[0]["POS_Code"].ToString();
                    txtBranchNo.Text = dt.Rows[0]["BranchNo"].ToString();
                    hidfOpenMode.Value = "EDIT";
                    hidfInvoiceNo.Value = dt.Rows[0]["INV_No"].ToString();
                    hidfRowID.Value = dt.Rows[0]["TransactionId"].ToString();

                    if (dt.Rows[0]["Update_Date"] != null)
                        tbx_update_date.Text = Convert.ToDateTime(dt.Rows[0]["Update_Date"]).ToString("dd/MM/yyyy");
                    if (dt.Rows[0]["UpDate_By"] != null)
                        tbx_update_by.Text = dt.Rows[0]["UpDate_By"].ToString();


                    btnPrint.Enabled = true;


                    res_flight = InvoiceDA.GetInvoiceFlight(INV_ID);
                    res_passenger = InvoiceDA.GetInvoicePassenger(INV_ID);
                    res_fare = InvoiceDA.GetInvoiceFare(INV_ID);
                    res_fee = InvoiceDA.GetInvoiceFee(INV_ID);
                    res_payment = InvoiceDA.GetInvoicePayment(INV_ID);
                }
                else
                {
                    MasterDA DA = new MasterDA();
                    res_flight = DA.GetFlightList(_BookingCurrentUpdate.TransactionId);
                    res_passenger = DA.GetPassengerList(_BookingCurrentUpdate.TransactionId);
                    res_fare = MasterDA.GetFareDetail(BookingNo).Where(x => x.TaxInvoiceNo != null && abb_no.Contains(x.TaxInvoiceNo)).ToList();
                    res_fee = MasterDA.GetFeeDetail(BookingNo).Where(x => x.TaxInvoiceNo != null && abb_no.Contains(x.TaxInvoiceNo)).ToList();
                    res_payment = DA.GetPaymentList(Guid.Parse(hidfTranID.Value), BookingNo).Where(x => x.ABBNo != null && abb_no.Contains(x.ABBNo)).ToList();
                    //txtInvoiceNo.Text = InvoiceDA.GetRunningInvoiceNo("IV");
                }

                txtBranchNo.Text = CookiesMenager.Station_Code;
                tbx_pos_no1.Text = CookiesMenager.POS_Code;

                //tbx_pos_no1.Text = CookiesMenager.POS_Code;
                #region Fare
                
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

                rptFee.DataSource = res_fee;
                rptFee.DataBind();
                decimal FeeTotalAmonut = (decimal)res_fee.Sum(it => it.Amount);
                decimal FeeTotalAmonutTH = (decimal)res_fee.Sum(it => it.AmountTHB);
                lbl_FeeTotalAmonut.Text = FeeTotalAmonut.ToString("#,###,##0.00");
                lbl_FeeTotalAmonutTH.Text = FeeTotalAmonutTH.ToString("#,###,##0.00");

                #endregion

                lbl_TotalFareAndFee.Text = (TotalFareTH + FeeTotalAmonutTH).ToString("#,###,##0.00");

                #region Payment
                
                rptPayment.DataSource = res_payment;
                rptPayment.DataBind();

                var PaymentAmount = res_payment.Sum(x => x.PaymentAmount);
                var PaymentAmountTHB = res_payment.Sum(x => x.PaymentAmountTHB);

                lbl_TotalPayment.Text = PaymentAmount.ToString("#,###,##0.00");
                lbl_TotalPaymentTHB.Text = PaymentAmountTHB.ToString("#,###,##0.00");

                #endregion
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


        protected void rptCustomer_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                M_Customer item = (M_Customer)e.Item.DataItem;
                Image imgAction = (Image)e.Item.FindControl("imgAction");
                Label lblName = (Label)e.Item.FindControl("lblName");
                Label lblTaxID = (Label)e.Item.FindControl("lblTaxID");
                HiddenField hidfCusVal = (HiddenField)e.Item.FindControl("hidfCusVal");
                lblName.Text = item.first_name; //+ " " + item.last_name;
                lblTaxID.Text = item.TaxID;
                imgAction.ImageUrl = "Images/Text-Edit-icon.png";
                imgAction.Attributes.Add("onclick", "ChooseCus('" + hidfCusVal.ClientID + "');");

                hidfCusVal.Value = item.CustomerID.ToString() + "|" +
                                   item.prfx_name + "|" + item.first_name + "|" + "" + "|" +
                                   item.TaxID + "|" + item.Addr_1 +
                                   ((item.Addr_2 != null && item.Addr_2 != string.Empty) ? "\n" + item.Addr_2 : string.Empty) +
                                   ((item.Addr_3 != null && item.Addr_3 != string.Empty) ? "\n" + item.Addr_3 : string.Empty) +
                                   ((item.Addr_4 != null && item.Addr_4 != string.Empty) ? "\n" + item.Addr_4 : string.Empty) +
                                   ((item.Addr_5 != null && item.Addr_5 != string.Empty) ? "\n" + item.Addr_5 : string.Empty);//+
                                   //item.ProvinceName;
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


        public string TableFlight()
        {
            MasterDA DA = new MasterDA();
            var key = this.Request["key"].ToString();
            //var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                
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
            var key = this.Request["key"].ToString();
            //var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                
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
            var key = this.Request["key"].ToString();
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
            MasterDA DA = new MasterDA();
            var key = this.Request["key"].ToString();
            //var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_payment = DA.GetPaymentList(_BookingCurrentUpdate.TransactionId, txtBookingNo.Text);
            }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MasterDA da = new MasterDA();
            rptCustomer.DataSource = da.GetCustomerForSearch(hidfSearch.Value.ToString());
            rptCustomer.DataBind();

            txtSearchCustomer.Text = hidfSearch.Value.ToString();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                T_Invoice_Info inv = new T_Invoice_Info();
                T_Invoice_Detail invDT = new T_Invoice_Detail();

                if (hidfOpenMode.Value == "NEW")
                {
                    txtInvoiceNo.Text = InvoiceDA.GenerateRunning(InvoiceDA.RunningType.IV, tbx_pos_no1.Text);
                    inv.TransactionId = Guid.Parse(hidfTranID.Value.ToString());
                }
                else
                {
                    inv.TransactionId = Guid.Parse(hidfRowID.Value.ToString());
                }

                inv.INV_No = txtInvoiceNo.Text;
                inv.INVDate = DateTime.Now;
                inv.Booking_No = txtBookingNo.Text;
                inv.Booking_Date = DateTime.ParseExact(tbx_date_booking.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                inv.CustomerID = Guid.Parse(txtCustCode.Text);
                inv.Address = tbx_booking_address1.Text;
                inv.Receipt_By = CookiesMenager.EmpID;
                inv.Receipt_Date = DateTime.Now;
                inv.POS_Code = tbx_pos_no1.Text;
                inv.BranchNo = txtBranchNo.Text;
                inv.Create_By = CookiesMenager.EmpID;
                inv.UpDate_By = CookiesMenager.EmpID;

                MasterDA DA = new MasterDA();


                var context = new POSINVEntities();

                Guid TranID = Guid.Parse(hidfTranID.Value);
                var PaymentList = DA.GetPaymentList(TranID, txtBookingNo.Text).Where(x => x.ABBNo != null && abb_no.Contains(x.ABBNo)).ToList();
                var Pay = PaymentList.OrderBy(x => x.Create_Date).FirstOrDefault();
                if (Pay != null && context.M_PaymentType.Count(x => x.PaymentType_Code == Pay.PaymentMethodCode) > 0)
                {
                    inv.PayType = context.M_PaymentType.First(x => x.PaymentType_Code == Pay.PaymentMethodCode).PaymentType_Id;
                }

                bool success;

                if (hidfOpenMode.Value == "NEW")
                {
                    success = InvoiceDA.InsertInvoice(inv, tbx_pos_no.Text);
                    InvoiceDA.UpdateRunningInvoiceNo("IV", txtInvoiceNo.Text, CookiesMenager.POS_Code, CookiesMenager.EmpID);
                }
                else
                {
                    success = InvoiceDA.UpdateInvoice(inv);
                }
                //if (success)
                //{
                //    InvoiceDA.DeleteInvoiceDetailByReceipt_no(txtInvoiceNo.Text);
                //    foreach (T_Invoice_Detail data in lst)
                //    {
                //        success = InvoiceDA.InsertInvoiceDetail(data);
                //    }
                //}


                rptCustomer.DataSource = null;
                rptCustomer.DataBind();
                var UrlText = String.Format("invoice_info.aspx?PNR_NO={0}&key={1}&INV_No={2}", PNR_NO, tbx_pos_no.Text, inv.INV_No);
                Response.Redirect(UrlText);
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("invoicelist.aspx");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //InvoiceDA.UpdteIsPrint(hidfTranID.Value);

            if (txtBookingNo.Text != string.Empty && tbx_pos_no.Text != string.Empty)
                Response.Redirect("~/fulltax_form.aspx?BookingNo=" + txtBookingNo.Text + "&AbbNo=" + tbx_pos_no.Text);// fix
        }

        protected void btnSelCust_Click(object sender, EventArgs e)
        {
            string str = hidfSelCus.Value;
        }

        void BindProv()
        {
            using (var context = new POSINVEntities())
            {
                var grp = context.M_Province.Where(x => (x.IsActive == "Y")).ToList();
                this.province.DataValueField = "ProvinceCode";
                this.province.DataTextField = "ProvinceName";
                this.province.DataSource = grp;
                this.province.DataBind();

                this.province.Items.Insert(0, new ListItem("", ""));

                var items = context.M_Country.Where(x => (x.IsActive == "Y")).ToList();

                this.country.DataValueField = "CountryCode";
                this.country.DataTextField = "CountryName";
                this.country.DataSource = items;
                this.country.DataBind();

                this.country.Items.Insert(0, new ListItem("", ""));


            }
        }



    }
}