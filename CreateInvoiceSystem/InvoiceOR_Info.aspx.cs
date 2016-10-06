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
    public partial class InvoiceOR_Info : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                string abb_no = this.Request["key"].ToString();
                DataTable dt = InvoiceDA.GetBookingByABB_NO(abb_no);
                hidfTranID.Value = dt.Rows[0]["TransactionId"].ToString();
                txtBookingNo.Text = dt.Rows[0]["PNR_No"].ToString();
                tbx_date_booking.Text = Convert.ToDateTime(dt.Rows[0]["Booking_date"]).ToString("dd/MM/yyyy");
                tbx_pos_no.Text = dt.Rows[0]["ABBNo"].ToString();

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
                string ABB_No = this.Request["key"].ToString();
                string INV_No = this.Request["INV_No"].ToString();
                DataTable dt = InvoiceDA.GetInvoiceByINVNo(INV_No);
                if (dt.Rows.Count > 0)
                {
                    hidfCustCode.Value = dt.Rows[0]["Cust_code"].ToString();
                    txtCustCode.Text = dt.Rows[0]["Cust_code"].ToString();
                    txtFullName.Text = dt.Rows[0]["first_name"].ToString() + " " + dt.Rows[0]["last_name"].ToString();
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
                    {
                        lblStatus.Text = dt.Rows[0]["Status"].ToString();
                        if (dt.Rows[0]["Status"].ToString().ToUpper() == "OR")
                        {
                            btnOR.Attributes.Add("class", "btn btn-primary disabled");
                            lblOR_Date.Text = Convert.ToDateTime(dt.Rows[0]["Cancel_Date"]).ToString("dd/MM/yyyy");
                            divOR_History.Visible = true;

                        }


                    }
                    else
                    {
                        lblStatus.Text = "Full Tax";
                        btnOR.Attributes.Add("onclick", "Req_OR();");

                    }
                    if (dt.Rows[0]["Update_Date"] != null)
                        tbx_update_date.Text = Convert.ToDateTime(dt.Rows[0]["Update_Date"]).ToString("dd/MM/yyyy");
                    if (dt.Rows[0]["UpDate_By"] != null)
                        tbx_update_by.Text = dt.Rows[0]["UpDate_By"].ToString();



                    //DataTable dtCN = InvoiceCNDA.GetInvoiceCN_ByCNID(dt.Rows[0]["CN_ID"].ToString());
                    //if (dtCN.Rows.Count > 0)
                    //{
                    //    hidfCN_ID.Value = dtCN.Rows[0]["CN_ID"].ToString();
                    //    divCN_History.Visible = true;
                    //    rptCN.DataSource = dtCN;
                    //    rptCN.DataBind();
                    //}

                }




                res_fee = MasterDA.GetFeeDetail(BookingNo);
                rptFee.DataSource = res_fee;
                rptFee.DataBind();
                decimal sum = (decimal)res_fee.Sum(it => it.amt);
                lblTotal.Text = sum.ToString("#,###,##0.00");

                MasterDA DA = new MasterDA();
                Guid TranID = Guid.Parse(hidfTranID.Value);
                rptPayment.DataSource = DA.GetPaymentList(TranID, BookingNo);
                rptPayment.DataBind();
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
                lblName.Text = item.first_name + " " + item.last_name;
                imgAction.ImageUrl = "Images/Text-Edit-icon.png";
                imgAction.Attributes.Add("onclick", "ChooseCus('" + hidfCusVal.ClientID + "');");

                hidfCusVal.Value = "|" + item.prfx_name + "|" + item.first_name + "|" + item.last_name + "|" + item.TaxID + "|" + item.Addr_1 + "|" + item.Addr_2 + "|" + item.Addr_3;
            }
        }

        protected void rptFee_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FeeDetail item = (FeeDetail)e.Item.DataItem;



                Label lblDetail = (Label)e.Item.FindControl("lblDetail");
                Label lblQty = (Label)e.Item.FindControl("lblQty");
                Label lblPrice = (Label)e.Item.FindControl("lblPrice");
                Label lblDiscount = (Label)e.Item.FindControl("lblDiscount");
                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                Label lblFeePassName = (Label)e.Item.FindControl("lblFeePassName");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trdata");

                lblFeePassName.Text = item.FirstName + " " + item.LastName;
                lblDetail.Text = item.ChargeDetail;
                lblQty.Text = item.qty.ToString("0");
                lblPrice.Text = (item.price ?? 0).ToString("#,###,##0.00"); ;
                lblDiscount.Text = (item.Discount ?? 0).ToString("#,###,##0.00");
                lblAmount.Text = (item.amt ?? 0).ToString("#,###,##0.00");





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
                lblPaymentAmount.Text = item.CollectedAmount.ToString("#,###,##0.00");
                lblPaymentAmountTHB.Text = item.PaymentAmount.ToString("#,###,##0.00");

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
            var key = this.Request["key"].ToString();
            var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_flight = DA.GetFlightList(res.TransactionId);
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
            var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_passenger = DA.GetPassengerList(res.TransactionId);
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
            var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_flight = DA.GetFlightList(res.TransactionId);
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
            var res = MasterDA.GetABBByInvoice(key);
            if (true)
            {
                res_payment = DA.GetPaymentList(res.TransactionId, res.PNR_No);
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
                        <td>" + item.CollectedAmount.ToString("#,##0.00") + @"</td>
                        <td>" + item.PaymentAmount.ToString("#,##0.00") + @"</td>
                        </tr>";
                    count++;
                }
            }
            return htmlStr;
        }



        protected void btnSaveOR_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = false;
                if (hidfInvoiceNo.Value != string.Empty)
                {
                    success = InvoiceDA.UpdteInvoicey_OR(hidfInvoiceNo.Value);
                }


                if(success)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSuccess", "SaveSuccess('" + hidfInvoiceNo.Value + "');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSuccess", "SaveFail();", true);

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
            Response.Redirect("InvoiceORList.aspx");
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