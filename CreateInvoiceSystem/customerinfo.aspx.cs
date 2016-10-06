using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class customerinfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindProv();
                servicepos service = new servicepos();
                var res = service.GetCustomerList();
                ViewState["data"] = res;
                Binding();
            }
        }

        void BindProv()
        {
            using (var context = new POSINVEntities())
            {
                var grp = context.M_Province.Where(x => (x.IsActive == "Y")).ToList();
                this.province.DataValueField = "ProvinceCode";
                this.province.DataTextField = "ProvinceName";
                this.province.DataSource = grp;
                this.province.DataBind();

                this.province.Items.Insert(0, new ListItem("", ""));

                var items = context.M_Country.Where(x => (x.IsActive == "Y") ).ToList();

                this.country.DataValueField = "CountryCode";
                this.country.DataTextField = "CountryName";
                this.country.DataSource = items;
                this.country.DataBind();

                this.country.Items.Insert(0, new ListItem("", ""));


            }
        }

        private void Binding()
        {
            GridView1.DataSource = ViewState["data"] as List<M_Customer>;
            GridView1.DataBind();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            servicepos service = new servicepos();
            var res = service.GetCustomerListByCode(this.TextBox_customer_code.Text, this.TextBox_customer_name.Text);
            ViewState["data"] = res;
            Binding();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.TextBox_customer_code.Text = "";
            this.TextBox_customer_name.Text = "";
            servicepos service = new servicepos();
            var res = service.GetCustomerList();
            ViewState["data"] = res;
            Binding();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            servicepos service = new servicepos();
            var res = service.GetCustomerList();
            ViewState["data"] = res;
            Binding();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int index = Convert.ToInt32(e.Keys[0]);
            //DAO.MasterDA da = new DAO.MasterDA();
            //da.(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            servicepos service = new servicepos();
            M_Customer cust = new M_Customer();
            cust.CustomerID = Guid.Parse(e.Keys[0].ToString());
            cust.last_upd_by = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            cust.last_upd_dt = DateTime.Now;
            var delres = service.SaveCustomer(cust, "delete");
            if (!delres.IsSuccessfull)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + delres.Message + "');", true);
            }
            else
            {
                var res = service.GetCustomerList();
                ViewState["data"] = res;
                Binding();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[1].Text;
                foreach (ImageButton button in e.Row.Cells[8].Controls.OfType<ImageButton>())
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

        }
    }
}