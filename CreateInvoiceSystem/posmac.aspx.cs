using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class posmac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindStation();
                ViewState["data"] = GetData();
                BindGrid();
            }
        }

        void BindStation()
        {
            using (var context = new POSINVEntities())
            {
                var items = context.M_Station.Where(x => (x.IsActive == "Y") && x.Active == "Y" && x.Station_Code != "System").ToList();

                this.dlstation.DataValueField = "Station_Code";
                this.dlstation.DataTextField = "Station_Name";
                this.dlstation.DataSource = items;
                this.dlstation.DataBind();

                this.dlstation.Items.Insert(0, new ListItem("", ""));

                this.dlstationadd.DataValueField = "Station_Code";
                this.dlstationadd.DataTextField = "Station_Name";
                this.dlstationadd.DataSource = items;
                this.dlstationadd.DataBind();

                this.dlstationadd.Items.Insert(0, new ListItem("", ""));

                var itemsslip = context.M_SlipMessage.Where(x => (x.IsActive == "Y") && x.Active == "Y").ToList();

                this.dlslipadd.DataValueField = "SlipMessage_Code";
                this.dlslipadd.DataTextField = "SlipMessage_Code";
                this.dlslipadd.DataSource = itemsslip;
                this.dlslipadd.DataBind();

                this.dlslipadd.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected List<M_POS_Machine> GetData()
        {
            using (var context = new POSINVEntities())
            {
                var items = context.M_POS_Machine.Where(x => (x.IsActive == "Y" && x.POS_Code != "System" && x.POS_Code.Contains(this.tcode.Text)
                    && x.POS_MacNo.Contains(this.tname.Text) && x.Station_Code.Contains(this.dlstation.SelectedValue))).ToList();
                return items;
            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<M_POS_Machine>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.Keys[0]);
            DAO.MasterDA da = new DAO.MasterDA();
            var Output = da.DeletePOS(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
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
                M_POS_Machine dataitem = (M_POS_Machine)e.Row.DataItem;
                if (dataitem.Active.ToString() == "Y")
                {
                    CheckBox chk = (CheckBox)e.Row.Cells[8].FindControl("chkRow");
                    chk.Checked = true;
                }
                ListItem itm = this.dlstation.Items.FindByValue(dataitem.Station_Code);
                if (itm != null)
                {
                    e.Row.Cells[4].Text = itm.Text;
                }
                string item = e.Row.Cells[1].Text;
                foreach (ImageButton button in e.Row.Cells[9].Controls.OfType<ImageButton>())
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
            this.dlstation.SelectedIndex = 0;
            this.dlslipadd.SelectedIndex = 0;
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