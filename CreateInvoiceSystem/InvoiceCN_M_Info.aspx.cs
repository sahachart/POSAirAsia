using CreateInvoiceSystem.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class InvoiceCN_M_Info : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getStation();
                GetMstPaymentList();
                InitialData();
            }
        }
        private void InitialData()
        {
            //this.tbx_date_booking.Text = "xx";
            //this.tbx_date_receipt.Text = "xx";
            this.tbx_create_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.tbx_update_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.tbx_pos_no.Text = Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToString();
            this.tbx_cashier_no.Text = Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString();
            this.tbx_create_by.Text = Context.Request.Cookies["authenuser"].Values["fname"].ToUpper() + " " + Context.Request.Cookies["authenuser"].Values["lastname"].ToUpper();
            this.tbx_update_by.Text = Context.Request.Cookies["authenuser"].Values["fname"].ToUpper() + " " + Context.Request.Cookies["authenuser"].Values["lastname"].ToUpper();


            DataTable dtReason = InvoiceCNDA.Get_M_CNReason("AND IsActive ='Y' ");
            Session["M_CNReason"] = dtReason;
            ddlReason.DataSource = dtReason;
            ddlReason.DataTextField = "CNReason_Name";
            ddlReason.DataValueField = "CNReason_Id";
            ddlReason.DataBind();


           string Inv_No =  Request.QueryString["key"].ToString();
           if (Inv_No != string.Empty)
           {
               DataTable dtCN = InvoiceCNDA.GetInvoiceCN_ByInvNo(Inv_No);
               if (dtCN.Rows.Count > 0)
               {
                   hidfCN_ID.Value = dtCN.Rows[0]["CN_ID"].ToString();
                   divCN_History.Visible = true;
                   rptCN.DataSource = dtCN;
                   rptCN.DataBind();
               }
           }
        }
        protected void GetMstPaymentList()
        {
            //MasterDA da = new MasterDA();
            //var res = MasterDA.GetMstPaymentTypeList();
            ////GetFlightFee
            //M_PaymentType addobj = new M_PaymentType();
            //addobj.PaymentType_Code = "";
            //addobj.PaymentType_Name = "Please selected...";
            //res.Add(addobj);
            //res.Sort((x, y) => x.PaymentType_Name.CompareTo(y.PaymentType_Name));
            //this.ddl_patment_type.DataSource = res;
            //this.ddl_patment_type.DataTextField = "PaymentType_Name";
            //this.ddl_patment_type.DataValueField = "PaymentType_Code";
            //this.ddl_patment_type.DataBind();
            //this.ddl_patment_type.SelectedValue = "";
            var res = MasterDA.GetFlightFee();
            M_FlightFee addobj = new M_FlightFee();
            addobj.Flight_Fee_Code = "";
            addobj.Flight_Fee_Name = "Please selected...";
            res.Add(addobj);
            res.Sort((x, y) => x.Flight_Fee_Name.CompareTo(y.Flight_Fee_Name));
            this.ddl_patment_type.DataSource = res;
            this.ddl_patment_type.DataTextField = "Flight_Fee_Name";
            this.ddl_patment_type.DataValueField = "Flight_Fee_Code";
            this.ddl_patment_type.DataBind();
            this.ddl_patment_type.SelectedValue = "";
        }
        protected void getStation()
        {
            //MasterDA da = new MasterDA();
            var res = MasterDA.GetStationList();
            M_Station addobj = new M_Station();
            addobj.Station_Code = "";
            addobj.Station_Name = "Select the station...";
            res.Add(addobj);
            res.Sort((x, y) => x.Station_Code.CompareTo(y.Station_Code));
            this.ddl_station.DataSource = res;
            this.ddl_station.DataTextField = "Station_Name";
            this.ddl_station.DataValueField = "Station_Code";
            this.ddl_station.DataBind();
            this.ddl_station.SelectedValue = "";

            var msg = MasterDA.GetMstSlipMessageList();
            M_SlipMessage obj_msg = new M_SlipMessage();
            obj_msg.SlipMessage_Code = "Select the slip message...";
            obj_msg.SlipMessage_Id = 0;
            msg.Add(obj_msg);
            msg.Sort((x, y) => x.SlipMessage_Id.CompareTo(y.SlipMessage_Id));
            this.ddl_slip.DataSource = msg;
            this.ddl_slip.DataTextField = "SlipMessage_Code";
            this.ddl_slip.DataValueField = "SlipMessage_Id";
            this.ddl_slip.DataBind();
            this.ddl_slip.SelectedValue = "0";

            var vat = MasterDA.GetMstVatList();
            M_VAT obj_vat = new M_VAT();
            obj_vat.VAT_NAME = "Select the slip message...";
            obj_vat.VAT_CODE = "";
            vat.Add(obj_vat);
            vat.Sort((x, y) => x.VAT_CODE.CompareTo(y.VAT_CODE));
            this.ddl_vat.DataSource = vat;
            this.ddl_vat.DataTextField = "VAT_VALUE";
            this.ddl_vat.DataValueField = "VAT_CODE";
            this.ddl_vat.DataBind();
            this.ddl_vat.SelectedValue = "EXTVAT";
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



        protected void btnSaveNPrint_Click(object sender, EventArgs e)
        {
            InitialData();
            ////////string Inv_No = Request.QueryString["key"].ToString();
            ////////string str = " openPopupPreview('CreditNote_M_Form.aspx?inv_no=" + Inv_No + "', 'Credit Note Preview', 900, 500);";
            ////////ScriptManager.RegisterStartupScript(this, this.GetType(), "CallBackSave", str, true);
            //try
            //{

            //    T_Invoice_CN invCN = new T_Invoice_CN();
            //    invCN.CN_ID = Guid.NewGuid();
            //    invCN.TransactionId = Guid.Parse(hidfTranID.Value);
            //    invCN.INV_No = tbx_receipt_no.Text;
            //    invCN.PNR_No = tbx_booking_no.Text;
            //    invCN.CN_NO = InvoiceDA.GetRunningInvoiceNo("CN");
            //    string val = ddlReason.SelectedValue.ToString();
            //    DataTable dtReason = (DataTable)Session["M_CNReason"];
            //    if (dtReason != null)
            //    {
            //        DataRow[] dr = dtReason.Select("CNReason_Id =" + val);
            //        if (dr.Length > 0)
            //        {
            //            invCN.CNReason_Id = Convert.ToInt32(val);
            //            invCN.CNReason_Code = dr[0]["CNReason_Code"].ToString();
            //        }
            //    }
            //    invCN.Create_By = CookiesMenager.EmpID;


            //    if (hidfCN_ID.Value != string.Empty)
            //    {
            //        invCN.CN_ID = Guid.Parse(hidfCN_ID.Value.ToString());
            //        InvoiceCNDA.UpdateInvoiceCN(invCN);
            //    }
            //    else
            //    {
            //        InvoiceCNDA.InsertInvoiceCN(invCN);
            //        InvoiceDA.UpdateInvoice_ByCN(invCN);
            //        AbbDA.UpdateABB_Inactive(hidfTranID.Value.ToString(), invCN.PNR_No);
            //        InvoiceDA.UpdateRunningInvoiceNo("CN", invCN.CN_NO, CookiesMenager.POS_Code, CookiesMenager.EmpID);
            //    }

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSuccess", "SaveSuccess('" + invCN.INV_No + "')", true);

            //}
            //catch (Exception ex)
            //{
            //    LogException.Save(ex, Url, CookiesMenager.EmpID);
            //    throw;
            //}
        }

    }
}