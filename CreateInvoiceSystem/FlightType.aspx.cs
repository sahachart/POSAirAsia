﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class FlightType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["data"] = GetData();
                BindGrid();
            }
        }

        protected List<M_FlightType> GetData()
        {
            using (var context = new POSINVEntities())
            {
                var items = context.M_FlightType.Where(x => (x.IsActive == "Y" && x.Flight_Type_Code.Contains(this.tcode.Text) && x.Flight_Type_Name.Contains(this.tname.Text))).ToList();
                return items;
            }
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<M_FlightType>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.Keys[0]);
            DAO.MasterDA da = new DAO.MasterDA();
            da.DeleteFlightType(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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