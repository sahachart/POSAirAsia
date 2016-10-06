using CreateInvoiceSystem.DAO;
using CreateInvoiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class billinvoicelist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //servicepos service = new servicepos();
                //GetInvoice();
                CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
                var res = MasterDA.GetInvoiceList(obj);
                ViewState["data"] = res;
                Binding();
            }
        }

        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    string dtf = Request.Form[this.tbx_date_from.UniqueID];
        //    string dtt = Request.Form[this.tbx_date_to.UniqueID];

        //    CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
        //    obj.booking_no = this.tbx_booking_no.Text;
        //    if (dtf != string.Empty)
        //    {
        //        //cri.date_from = DateTime.Parse(dtf, new System.Globalization.CultureInfo("en-GB"));
        //        obj.date_from = DateTime.Parse(dtf, new System.Globalization.CultureInfo("en-GB"));

        //    }
        //    if (dtf != string.Empty)
        //    {
        //        //cri.date_to = DateTime.Parse(dtt, new System.Globalization.CultureInfo("en-GB"));
        //        obj.date_to = DateTime.Parse(dtt, new System.Globalization.CultureInfo("en-GB"));

        //    }
        //    obj.passenger = this.tbx_passenger.Text;
        //    var res = MasterDA.GetInvoiceList(obj);
        //    ViewState["data"] = res;
        //    Binding();
        //}

        //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
        //    var res = MasterDA.GetInvoiceList(obj);
        //    ViewState["data"] = res;
        //    Binding();
        //}

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
            var res = MasterDA.GetInvoiceList(obj);
            ViewState["data"] = res;
            Binding();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = ((System.Web.UI.WebControls.HyperLink)(e.Row.Cells[1].Controls[0])).Text;//e.Row.Cells[6].Text;
                foreach (ImageButton button in e.Row.Cells[11].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Edit")
                    {
                        button.Attributes["ID"] = "print_" + item;
                        button.Attributes["onclick"] = String.Format("OpenPrint('{0}'); return false;", item);
                        button.Attributes["value"] = item;

                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        private void GetInvoice()
        {
            CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
            var res = MasterDA.GetInvoiceList(obj);
            ViewState["data"] = res;
            //Binding();
        }
        private void Binding()
        {
            var res = ViewState["data"] as List<T_Invoice_Manual>; 
            GridView1.DataSource = ViewState["data"] as List<T_Invoice_Manual>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string dtf = Request.Form[this.tbx_date_from.UniqueID];
            string dtt = Request.Form[this.tbx_date_to.UniqueID];

            CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
            obj.booking_no = this.tbx_booking_no.Text;
            if (dtf != string.Empty)
            {
                //cri.date_from = DateTime.Parse(dtf, new System.Globalization.CultureInfo("en-GB"));
                obj.date_from = DateTime.Parse(dtf, new System.Globalization.CultureInfo("en-GB"));

            }
            if (dtf != string.Empty)
            {
                //cri.date_to = DateTime.Parse(dtt, new System.Globalization.CultureInfo("en-GB"));
                obj.date_to = DateTime.Parse(dtt, new System.Globalization.CultureInfo("en-GB"));

            }
            obj.passenger = this.tbx_passenger.Text;
            var res = MasterDA.GetInvoiceList(obj);
            ViewState["data"] = res;
            Binding();
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            tbx_booking_no.Text = string.Empty;
            tbx_date_from.Text = string.Empty;
            tbx_date_to.Text = string.Empty;
            tbx_passenger.Text = string.Empty;

            btn_Search_Click(null, null);

        }
    }
}