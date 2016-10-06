using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class usermanage : System.Web.UI.Page
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

        protected List<Model.EmpModel> GetData()
        {
            DAO.MasterDA da = new DAO.MasterDA();
            return da.GetEmplist(this.tcode.Text, this.tname.Text, this.dlgrp.SelectedValue.ToString()
                , this.dllStation.SelectedValue.ToString());
        }

        void BindStation()
        {
            using (var context = new POSINVEntities())
            {
                var grp = context.M_UserGroup.Where(x => (x.IsActive == "Y")).ToList();
                this.dlgrp.DataValueField = "UserGroupCode";
                this.dlgrp.DataTextField = "UserGroupName";
                this.dlgrp.DataSource = grp;
                this.dlgrp.DataBind();

                this.dlgrp.Items.Insert(0, new ListItem("", ""));

                this.dlUserGroup.DataValueField = "UserGroupCode";
                this.dlUserGroup.DataTextField = "UserGroupName";
                this.dlUserGroup.DataSource = grp;
                this.dlUserGroup.DataBind();

                this.dlUserGroup.Items.Insert(0, new ListItem("", ""));

                var items = context.M_Station.Where(x => (x.IsActive == "Y") && x.Active == "Y" && x.Station_Code != "System").ToList();

                this.dllStation.DataValueField = "Station_Code";
                this.dllStation.DataTextField = "Station_Name";
                this.dllStation.DataSource = items;
                this.dllStation.DataBind();

                this.dllStation.Items.Insert(0, new ListItem("", ""));

                this.dlstn.DataValueField = "Station_Code";
                this.dlstn.DataTextField = "Station_Name";
                this.dlstn.DataSource = items;
                this.dlstn.DataBind();

                this.dlstn.Items.Insert(0, new ListItem("", ""));

                //var menu = context.M_Menu.ToList();
                //this.dldefmenu.DataValueField = "Menu_Code";
                //this.dldefmenu.DataTextField = "Menu_Name";
                //this.dldefmenu.DataSource = menu;
                //this.dldefmenu.DataBind();

                //this.dldefmenu.Items.Insert(0, new ListItem("", ""));

            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<Model.EmpModel>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.Keys[0]);
            DAO.MasterDA da = new DAO.MasterDA();
            da.DeleteEmp(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[1].Text;
                foreach (ImageButton button in e.Row.Cells[6].Controls.OfType<ImageButton>())
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
            this.tname.Text = "";
            this.tcode.Text = "";
            this.dlgrp.SelectedIndex = 0;
            this.dllStation.SelectedIndex = 0;
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