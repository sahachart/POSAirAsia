using CreateInvoiceSystem.DAO;
using CreateInvoiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class InvoiceCNList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
                // var res = MasterDA.GetAllABBList(obj);
                var res = InvoiceDA.GetInvoiceList("", null, null, "", true, "All");
                //ViewState["data"] = res;
                Binding(res);
            }
        }
        private void Binding(List<Object> Data)
        {
            try
            {
                GridView1.DataSource = Data;
                GridView1.DataBind();
                this.Label1.Visible = !(this.GridView1.Rows.Count > 0);

            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = ((System.Web.UI.WebControls.HyperLink)(e.Row.Cells[1].Controls[0])).Text;//e.Row.Cells[5].Text;
                foreach (ImageButton button in e.Row.Cells[12].Controls.OfType<ImageButton>())
                {
                    if (e.Row.Cells[6].Text.ToUpper() != "CN")
                    {
                        button.Visible = false;
                    }
                    if (button.CommandName == "Edit")
                    {
                        button.Attributes["ID"] = "print_" + item;
                        button.Attributes["onclick"] = String.Format("OpenPrint('{0}'); return false;", item);
                        button.Attributes["value"] = item;

                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetData();
        }

        private void GetData()
        {
            try
            {
                DateTime? FromDate = null;
                if (tbx_date_from.Text != string.Empty)
                {
                    try
                    {
                        FromDate = DateTime.ParseExact(tbx_date_from.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch { tbx_date_from.Text = string.Empty; }
                }

                DateTime? ToDate = null;
                if (tbx_date_to.Text != string.Empty)
                {
                    try
                    {
                        ToDate = DateTime.ParseExact(tbx_date_to.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch { tbx_date_to.Text = string.Empty; }
                }

                var res = InvoiceDA.GetInvoiceList(tbx_booking_no.Text, FromDate, ToDate, tbx_passenger.Text, true, ddlStatus.SelectedItem.Value.ToString());
                Binding(res);

            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }

        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            GetData();
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                tbx_booking_no.Text = string.Empty;
                tbx_date_from.Text = string.Empty;
                tbx_date_to.Text = string.Empty;
                tbx_passenger.Text = string.Empty;

                btn_Search_Click(null, null);

            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
        }
    
    }
}