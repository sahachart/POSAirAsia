using CreateInvoiceSystem.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class FlightFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["data"] = GetData();
                BindGrid();

                
                this.ddl_FlightFeeGroup.DataValueField = "ID";
                this.ddl_FlightFeeGroup.DataTextField = "NameTH";
                this.ddl_FlightFeeGroup.DataSource = MasterDA.GetFlightFeeGroup(string.Empty);
                this.ddl_FlightFeeGroup.DataBind();

                this.ddl_FlightFeeGroup.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected List<M_FlightFee> GetData()
        {
            using (var context = new POSINVEntities())
            {
                if (dllgrp.SelectedValue.ToString() == "")
                {
                    var items = context.M_FlightFee.Where(x => (x.IsActive == "Y" && x.Flight_Fee_Code.Contains(this.tcode.Text) && x.Flight_Fee_Name.Contains(this.tname.Text))).ToList();
                    return items;
                }
                else
                {
                    string valdd = this.dllgrp.SelectedValue.ToString();
                    var items = context.M_FlightFee.Where(x => (x.IsActive == "Y" && x.Flight_Fee_Code.Contains(this.tcode.Text) && x.Flight_Fee_Name.Contains(this.tname.Text) && x.AbbGroup == valdd)).ToList();
                    return items;
                }
            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<M_FlightFee>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.Keys[0]);
            DAO.MasterDA da = new DAO.MasterDA();
            var Output = da.DeleteFlightFee(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
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
                M_FlightFee dataitem = (M_FlightFee)e.Row.DataItem;
                if (dataitem.DisplayAbb.ToString() == "Y")
                {
                    CheckBox chk = (CheckBox)e.Row.Cells[3].FindControl("chkRow");
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
            this.dllgrp.SelectedIndex = 0;
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