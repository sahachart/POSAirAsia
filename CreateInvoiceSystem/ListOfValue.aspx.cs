using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class ListOfValue : System.Web.UI.Page
    {
        protected string[] itemtype = { "Country", "Province", "Currency Exchange", "Payment Type", "Station", "Slip Message", "Flight Type", "Flight Fee", "POS / POS Machine", "Code Fee", "Agency", "CN Reason", "Service for Goinsure", "User Group" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //ViewState["data"] = GetData();
                //BindGrid();
            }
        }

        //protected List<M_ListOfValue> GetData()
        //{
        //    using (var context = new POSINVEntities())
        //    {
        //        var listOfValue = context.M_ListOfValue.Where(x => x.IsActive == "Y").ToList();
        //        return listOfValue;
        //    }
        //}

        protected void BindGrid()
        {
            //GridView1.DataSource = ViewState["data"] as List<M_ListOfValue>;
            //GridView1.DataBind();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int index = Convert.ToInt32(e.Keys[0]);
            //DAO.MasterDA da = new DAO.MasterDA();
            //da.DeleteLov(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            //ViewState["data"] = GetData();
            //BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[1].Text;
                foreach (Button button in e.Row.Cells[3].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
                int _value = int.Parse(e.Row.Cells[2].Text) - 1;
                e.Row.Cells[2].Text = this.itemtype[_value];
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ViewState["data"] = GetData();
            //BindGrid();
        }
    }
}