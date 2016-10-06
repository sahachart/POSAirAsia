using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class Slipmsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["data"] = GetData();
                BindGrid();
            }
        }

        protected List<M_SlipMessageSetup> GetData()
        {
            using (var context = new POSINVEntities())
            {
                var items = context.M_SlipMessageSetup.Where(x => (x.IsActive == "Y" && x.SlipMessage_Code.Contains(this.tcode.Text)
                    && (x.Header1.Contains(this.theader.Text) || x.Header2.Contains(this.theader.Text)
                    || x.Header3.Contains(this.theader.Text) || x.Header4.Contains(this.theader.Text)
                    || x.Header5.Contains(this.theader.Text) || x.Header6.Contains(this.theader.Text))
                    && (x.Footer1.Contains(this.tfooter.Text) || x.Footer2.Contains(this.tfooter.Text)
                    || x.Footer3.Contains(this.tfooter.Text) || x.Footer4.Contains(this.tfooter.Text)
                    || x.Footer5.Contains(this.tfooter.Text))
                    )).ToList();
                return items;
            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<M_SlipMessageSetup>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.Keys[0]);
            DAO.MasterDA da = new DAO.MasterDA();
            da.DeleteSlipMsg(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                M_SlipMessageSetup dataitem = (M_SlipMessageSetup)e.Row.DataItem;
                if (dataitem.Active.ToString() == "Y")
                {
                    CheckBox chk = (CheckBox)e.Row.Cells[4].FindControl("chkRow");
                    chk.Checked = true;
                }
                string item = e.Row.Cells[1].Text;
                foreach (ImageButton button in e.Row.Cells[5].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.tcode.Text = "";
            this.theader.Text = "";
            this.tfooter.Text = "";
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ViewState["data"] = GetData();
            BindGrid();
        }
    }
}