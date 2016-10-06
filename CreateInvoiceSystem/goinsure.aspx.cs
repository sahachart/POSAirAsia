using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class goinsure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindFee();
                //ViewState["data"] = GetData();
                //BindGrid();
            }
        }

        void BindFee()
        {
            using (var context = new POSINVEntities())
            {
                var items = context.M_FlightFee.Where(x => (x.IsActive == "Y")).ToList();
                
                this.dllfee.DataValueField = "Flight_Fee_Code";
                this.dllfee.DataTextField = "Flight_Fee_Name";
                this.dllfee.DataSource = items;
                this.dllfee.DataBind();

                this.dllfee.Items.Insert(0, new ListItem("",""));

                this.DropDownList1.DataValueField = "Flight_Fee_Code";
                this.DropDownList1.DataTextField = "Flight_Fee_Name";
                this.DropDownList1.DataSource = items;
                this.DropDownList1.DataBind();

                this.DropDownList1.Items.Insert(0, new ListItem("", ""));
            }
        }

        //protected List<M_Goinsure> GetData()
        //{
        //    using (var context = new POSINVEntities())
        //    {
        //        var items = context.M_Goinsure.Where(x => (x.IsActive == "Y" && x.Ginsure_Code.Contains(this.tcode.Text)
        //            && x.Ginsure_Name.Contains(this.tname.Text) && x.FlightFee.Contains(this.dllfee.SelectedValue))).ToList();
        //        return items;
        //    }
        //}

        protected void BindGrid()
        {
            //GridView1.DataSource = ViewState["data"] as List<M_Goinsure>;
            //GridView1.DataBind();
            //this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int index = Convert.ToInt32(e.Keys[0]);
            //DAO.MasterDA da = new DAO.MasterDA();
            //da.DeleteGoInsure(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            //ViewState["data"] = GetData();
            //BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[1].Text;
                foreach (ImageButton button in e.Row.Cells[4].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
                ListItem itm = DropDownList1.Items.FindByValue(e.Row.Cells[3].Text);
                if (itm != null)
                {
                    e.Row.Cells[3].Text = itm.Text;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ViewState["data"] = GetData();
            //BindGrid();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //ViewState["data"] = GetData();
            //BindGrid();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //this.tcode.Text = "";
            //this.tname.Text = "";
            //this.dllfee.SelectedIndex = 0;
            //ViewState["data"] = GetData();
            //BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView1.PageIndex = e.NewPageIndex;
            //ViewState["data"] = GetData();
            //BindGrid();
        }
    }
}