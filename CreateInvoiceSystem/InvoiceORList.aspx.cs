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
    public partial class InvoiceORList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CriteriaInvoiceModel obj = new CriteriaInvoiceModel();
                // var res = MasterDA.GetAllABBList(obj);
                var res = InvoiceCNDA.GetInvoiceORList("");
                ViewState["data"] = res;
                Binding();
            }
        }
        private void Binding()
        {
            try
            {
                GridView1.DataSource = (DataTable)ViewState["data"];
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
                string item = e.Row.Cells[5].Text;
                foreach (ImageButton button in e.Row.Cells[1].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Edit")
                    {
                        button.Attributes["ID"] = "ed_" + item;
                        button.Attributes["onclick"] = "return false;";
                        button.Attributes["value"] = item;

                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string WhereClause = string.Empty;
                DateTime Date;
                if (tbx_booking_no.Text != string.Empty)
                {
                    WhereClause += " AND book.PNR_No Like '%" + tbx_booking_no.Text + "%' ";
                }

                if (tbx_date_from.Text != string.Empty)
                {
                    try
                    {
                        Date = DateTime.ParseExact(tbx_date_from.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        WhereClause += "AND Convert(varchar(8), book.Booking_Date ,112) >= '" + Date.ToString("yyyyMMdd") + "' ";
                    }
                    catch { tbx_date_from.Text = string.Empty; }

                }


                if (tbx_date_to.Text != string.Empty)
                {
                    try
                    {
                        Date = DateTime.ParseExact(tbx_date_to.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        WhereClause += "AND Convert(varchar(8), book.Booking_Date ,112) <= '" + Date.ToString("yyyyMMdd") + "' ";
                    }
                    catch { tbx_date_to.Text = string.Empty; }
                }

                if (tbx_passenger.Text != string.Empty)
                {
                    string PassengerName = tbx_passenger.Text;
                    WhereClause += "AND (Select  Count(*) FROM T_PassengerCurrentUpdate pass WHERE pass.TransactionId = T_ABB.TransactionId AND (pass.FirstName like '%" + PassengerName + "%' OR pass.LastName like '%" + PassengerName + "%') ) > 0 ";
                }

                if (ddlStatus.SelectedItem.Value.ToString() != "All")
                {
                    string PassengerName = tbx_passenger.Text;
                    WhereClause += "AND ISNULL( inv.Status,'') = '" + ddlStatus.SelectedItem.Value.ToString() + "'";
                }

                var res = InvoiceCNDA.GetInvoiceORList(WhereClause);
                ViewState["data"] = res;
                Binding();
            }
            catch (Exception ex)
            {
                LogException.Save(ex, Url, CookiesMenager.EmpID);
                throw;
            }
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