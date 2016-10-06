using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class MWebservice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSessionURL.Text = Properties.Settings.Default.CreateInvoiceSystem_com_airasia_acesession_SessionService;
            lblBookingURL.Text = Properties.Settings.Default.CreateInvoiceSystem_com_airasia_acebooking_BookingService;
            lblUsername.Text = System.Configuration.ConfigurationManager.AppSettings["usernamesky"];
            lblpassword.Text = System.Configuration.ConfigurationManager.AppSettings["passwordsky"];
        }
    }
}