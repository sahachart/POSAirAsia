using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class Station : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["data"] = GetData();
                BindGrid();
                BindSlip();
            }
        }

        protected List<M_Station> GetData()
        {
            using (var context = new POSINVEntities())
            {
                var items = context.M_Station.Where(x => (x.IsActive == "Y" && x.Station_Code != "System" && x.Station_Code.Contains(this.tcode.Text) && x.Station_Name.Contains(this.tname.Text) && x.Location_Name.Contains(this.tloc.Text))).ToList();
                return items;
            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<M_Station>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void BindSlip()
        {
            using (var context = new POSINVEntities())
            {
                var itemsslip = context.M_SlipMessage.Where(x => (x.IsActive == "Y") && x.Active == "Y" && x.SlipMessage_Code != "Default").OrderBy(c => c.SlipMessage_Code).ToList();

                var Defaultslip = context.M_SlipMessage.Where(x => (x.IsActive == "Y") && x.Active == "Y" && x.SlipMessage_Code == "Default").ToList();

                var slip = Defaultslip.Union(itemsslip);

                this.ddl_ABBSlip.DataValueField = "SlipMessage_Id";
                this.ddl_ABBSlip.DataTextField = "SlipMessage_Code";
                this.ddl_ABBSlip.DataSource = slip;
                this.ddl_ABBSlip.DataBind();
                //this.ddl_ABBSlip.Items.Insert(0, new ListItem("", ""));

                this.ddl_InvoiceSlip.DataValueField = "SlipMessage_Id";
                this.ddl_InvoiceSlip.DataTextField = "SlipMessage_Code";
                this.ddl_InvoiceSlip.DataSource = slip;
                this.ddl_InvoiceSlip.DataBind();
                //this.ddl_InvoiceSlip.Items.Insert(0, new ListItem("", ""));

                this.ddl_CreditNoteSlip.DataValueField = "SlipMessage_Id";
                this.ddl_CreditNoteSlip.DataTextField = "SlipMessage_Code";
                this.ddl_CreditNoteSlip.DataSource = slip;
                this.ddl_CreditNoteSlip.DataBind();
                //this.ddl_CreditNoteSlip.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.Keys[0]);
            DAO.MasterDA da = new DAO.MasterDA();
            var Output = da.DeleteStation(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            if (Output != string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + Output + "');", true);
            }
            else
            {
                ViewState["data"] = GetData();
                BindGrid();
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                M_Station dataitem = (M_Station)e.Row.DataItem;
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
            this.tname.Text = "";
            this.tloc.Text = "";
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