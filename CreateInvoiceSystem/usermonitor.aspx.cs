using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreateInvoiceSystem.Model;

namespace CreateInvoiceSystem
{
    public partial class usermonitor : System.Web.UI.Page
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
                var items = context.M_Station.Where(x => (x.IsActive == "Y") && x.Active == "Y").ToList();

                this.dlstation.DataValueField = "Station_Id";
                this.dlstation.DataTextField = "Station_Name";
                this.dlstation.DataSource = items;
                this.dlstation.DataBind();

                this.dlstation.Items.Insert(0, new ListItem("", ""));

                var itemsgrp = context.M_UserGroup.Where(x => (x.IsActive == "Y")).ToList();
                
                this.dlgroup.DataValueField = "UserGroupId";
                this.dlgroup.DataTextField = "UserGroupName";
                this.dlgroup.DataSource = itemsgrp;
                this.dlgroup.DataBind();

                this.dlgroup.Items.Insert(0, new ListItem("", ""));

                
            }

            DAO.MasterDA da = new DAO.MasterDA();
            M_AppSystem appsystem = da.GetLockSystem();
            if (appsystem != null)
            {
                this.CheckBox1.Checked = (appsystem.LockSystem == "Y") ? true : false;
            }
        }

        protected List<Model.Monitoruser> GetData()
        {
            DAO.MasterDA da = new DAO.MasterDA();
            return da.GetMonitoruserlist(this.tcode.Text, this.dlgroup.SelectedValue.ToString()
                , this.dlstation.SelectedValue.ToString());
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<Monitoruser>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Monitoruser dataitem = (Monitoruser)e.Row.DataItem;
                e.Row.Cells[2].Text = dataitem.TimeStamp.ToString("HH:mm:ss");
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.tcode.Text = "";
            this.dlstation.SelectedIndex = -1;
            this.dlgroup.SelectedIndex = -1;
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            da.locksystem(this.CheckBox1.Checked, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
        }
    }
}