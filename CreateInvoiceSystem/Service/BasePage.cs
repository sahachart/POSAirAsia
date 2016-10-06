using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CreateInvoiceSystem
{
    public class BasePage : Page
    {
        public string Url { get { return HttpContext.Current.Request.Url.AbsoluteUri; } }

        protected override void OnInit(EventArgs e)
        {
            if (Context.Request.Cookies["authenuser"].Values["Emp_ID"] != null)
                CookiesMenager.EmpID = Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToUpper();

            if (Context.Request.Cookies["authenuser"].Values["POS_CODE"] != null)
                CookiesMenager.POS_Code = Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToUpper();

            if (Context.Request.Cookies["authenuser"].Values["Station_Code"] != null)
                CookiesMenager.Station_Code = Context.Request.Cookies["authenuser"].Values["Station_Code"].ToUpper();

            if (Context.Request.Cookies["authenuser"].Values["Station_Name"] != null)
                CookiesMenager.Station_Name = Context.Request.Cookies["authenuser"].Values["Station_Name"].ToUpper();
            
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            base.OnInit(e);
        }

        protected override void OnError(EventArgs e)
        {
            string EmpID = Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToUpper();
            Exception ex = Server.GetLastError();
            LogException.Save(ex, Url, EmpID);

            Session["_Exception"] = ex;
            Response.Redirect("~/Page1.aspx", true);
            //base.OnError(e);
        }

    }

    public class CookiesMenager
    {
        static public string EmpID { get; set; }

        static public string POS_Code { get; set; }

        static public string Station_Code { get; set; }

        static public string Station_Name { get; set; }
        

    }
}