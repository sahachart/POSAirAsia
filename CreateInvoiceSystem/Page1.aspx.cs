using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class Page1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Exception ex = (Exception)Session["_Exception"];
                lblHearder.Text = "Error Page : " + ex.Message;
                lblDetail.Text = ex.StackTrace;

                Session.Remove("_Exception");
            }
        }
    }
}