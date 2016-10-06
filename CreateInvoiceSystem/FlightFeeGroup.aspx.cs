using CreateInvoiceSystem.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class FlightFeeGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["data"] = MasterDA.GetFlightFeeGroup(string.Empty);
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<M_FlightFeeGroup>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void btn_Search_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["data"] = MasterDA.GetFlightFeeGroup(this.txt_GroupNameSearch.Text);
            BindGrid();
        }

        protected void btn_Clear_Click(object sender, ImageClickEventArgs e)
        {
            this.txt_GroupNameSearch.Text = string.Empty;
            ViewState["data"] = MasterDA.GetFlightFeeGroup(string.Empty);
            BindGrid();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                M_FlightFeeGroup dataitem = (M_FlightFeeGroup)e.Row.DataItem;
                string item = e.Row.Cells[1].Text;
                foreach (ImageButton button in e.Row.Cells[3].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ViewState["data"] = MasterDA.GetFlightFeeGroup(this.txt_GroupNameSearch.Text);
            BindGrid();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Guid ID = Guid.Parse(e.Keys[0].ToString());
            DAO.MasterDA da = new DAO.MasterDA();
            var Output = da.DeleteFlightFeeGroup(ID);
            if (Output != string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + Output + "');", true);
            }
            else
            {
                ViewState["data"] = MasterDA.GetFlightFeeGroup(this.txt_GroupNameSearch.Text);
                BindGrid();
            }
        }
    }
}