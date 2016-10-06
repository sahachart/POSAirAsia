using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class currencymaster : System.Web.UI.Page
    {
        List<string> codeexist = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //dtp_CurrencyDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                GetData();
            }
        }
        private void Binding()
        {
            GridView1.DataSource = ViewState["data"] as List<M_Currency>;
            GridView1.DataBind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                M_Currency dataitem = (M_Currency)e.Row.DataItem;

                string item = e.Row.Cells[1].Text;

                if (codeexist.IndexOf(item) > -1)
                {
                    e.Row.Cells[2].Text = dataitem.Currency_Name;
                    foreach (ImageButton button in e.Row.Cells[6].Controls.OfType<ImageButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            e.Row.Cells[6].Controls.Remove(button);
                        }
                    }
                    
                }
                else
                {
                    this.codeexist.Add(item);
                    foreach (ImageButton button in e.Row.Cells[6].Controls.OfType<ImageButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                        }
                    }
                }
                
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            servicepos service = new servicepos();
            M_Currency p = new M_Currency();
            p.Currency_Id = int.Parse(e.Keys[0].ToString());
            p.Update_By = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            p.Update_Date = DateTime.Now;
            var res = service.SaveMstCurrency(p, "delete");
            if (res.IsSuccessfull)
            {
               GetData(); 
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetData();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DateTime? CurrencyDate = null;
            if (dtp_CurrencyDate.Text != string.Empty)
            {
                try
                {
                    CurrencyDate = DateTime.ParseExact(dtp_CurrencyDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { dtp_CurrencyDate.Text = string.Empty; }

            }
            servicepos service = new servicepos();
            var res = service.getMstcurrencyListByCode(this.TextBox_Currency_Code.Text, CurrencyDate);
            ViewState["data"] = res;
            Binding();
        }

        private void GetData()
        {
            DateTime? CurrencyDate = null;
            if (dtp_CurrencyDate.Text != string.Empty)
            {
                try
                {
                    CurrencyDate = DateTime.ParseExact(dtp_CurrencyDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { dtp_CurrencyDate.Text = string.Empty; }

            }
            servicepos service = new servicepos();
            var res = service.getMstcurrencyListByCode(TextBox_Currency_Code.Text, CurrencyDate); //service.getMstCurrencyList();
            ViewState["data"] = res;
            Binding();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.TextBox_Currency_Code.Text = "";
            GetData();
        }
    }
}