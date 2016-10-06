using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;



namespace CreateInvoiceSystem
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            
            Page.PreLoad += master_Page_PreLoad;
        }
        private void LoadMessage() {
            SqlCommand sql = new SqlCommand();
            sql = new SqlCommand();
            sql.CommandText = "SP_Show_SendMassege";
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTime.Now;
            DataTable dt = FunctionAndClass.IexcuteDataByDataTable(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= (dt.Rows.Count - 1); i++)
                    {
                        LBShowMessage.Text += ", " + dt.Rows[i]["msg"].ToString();
                    }
                    LBShowMessage.Text = LBShowMessage.Text.Substring(1, LBShowMessage.Text.Length - 1);
                    trShowmsg.Visible = true;
                }
                else {
                    LBShowMessage.Text = "";
                    trShowmsg.Visible = false;
                }
            }
            else {
                LBShowMessage.Text = "";
                trShowmsg.Visible = false;
            }
        }
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                LoadMessage();
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;

                DAO.MasterDA da = new DAO.MasterDA();
                List<M_UserPermission> MenuPrivileges = da.GetMenuPrivilege(Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString()
                    , Context.Request.Cookies["authenuser"].Values["grp"].ToString());


                importdategrp.Visible = GetUserPermission(MenuPrivileges, "034");

                #region Booking
                
                bookinggrp.Visible = true;

                bookinginfsub.Visible = GetUserPermission(MenuPrivileges, "001");
                updatebooksub.Visible = GetUserPermission(MenuPrivileges, "002");
                batchsub.Visible = GetUserPermission(MenuPrivileges, "003");

                if (!bookinginfsub.Visible && !updatebooksub.Visible && !batchsub.Visible)
                {
                    bookinggrp.Visible = false;
                }

                #endregion

                #region Invoice

                invoicegrp.Visible = true;

                invoicesub.Visible = GetUserPermission(MenuPrivileges, "004");
                billsub.Visible = GetUserPermission(MenuPrivileges, "005");
                refundsub.Visible = GetUserPermission(MenuPrivileges, "006");

                if (!invoicesub.Visible && !billsub.Visible && !refundsub.Visible)
                {
                    invoicegrp.Visible = false;
                }

                #endregion

                #region Report

                reportgrp.Visible = true;

                rptbymaxsub.Visible = GetUserPermission(MenuPrivileges, "007");
                rpttaxsub.Visible = GetUserPermission(MenuPrivileges, "008");
                rptsumtaxsub.Visible = GetUserPermission(MenuPrivileges, "009");
                rptabbsub.Visible = GetUserPermission(MenuPrivileges, "010");
                rptvalidsub.Visible = GetUserPermission(MenuPrivileges, "012");
                rptbranchsub.Visible = GetUserPermission(MenuPrivileges, "013");
                rptrefundsub.Visible = GetUserPermission(MenuPrivileges, "014");
                rpteditinvoicesub.Visible = GetUserPermission(MenuPrivileges, "015");

                if (!rptbymaxsub.Visible && !rpttaxsub.Visible && !rptsumtaxsub.Visible && !rptabbsub.Visible &&
                    !rptvalidsub.Visible && !rptbranchsub.Visible && !rptrefundsub.Visible && !rpteditinvoicesub.Visible)
                {
                    reportgrp.Visible = false;
                }

                #endregion

                #region Master

                masterdatagrp.Visible = true;
                
                currsub.Visible = GetUserPermission(MenuPrivileges, "018");
                paysub.Visible = GetUserPermission(MenuPrivileges, "019");
                stationsub.Visible = GetUserPermission(MenuPrivileges, "020");
                slipsub.Visible = GetUserPermission(MenuPrivileges, "021");
                flighttypesub.Visible = GetUserPermission(MenuPrivileges, "022");
                flightfeegroupsub.Visible = GetUserPermission(MenuPrivileges, "023");
                flightsub.Visible = GetUserPermission(MenuPrivileges, "024");
                possub.Visible = GetUserPermission(MenuPrivileges, "025");
                agencytypesub.Visible = GetUserPermission(MenuPrivileges, "026");
                agencysub.Visible = GetUserPermission(MenuPrivileges, "027");
                cnsub.Visible = GetUserPermission(MenuPrivileges, "028");
                messsub.Visible = GetUserPermission(MenuPrivileges, "029");
                cussub.Visible = GetUserPermission(MenuPrivileges, "030");

                if (!currsub.Visible && !paysub.Visible && !stationsub.Visible && !slipsub.Visible &&
                    !flighttypesub.Visible && !flightfeegroupsub.Visible && !flightsub.Visible && !possub.Visible &&
                    !agencytypesub.Visible && !agencysub.Visible && !cnsub.Visible && !messsub.Visible && !cussub.Visible)
                {
                    masterdatagrp.Visible = false;
                }

                #endregion

                #region User Managment

                usermanagegrp.Visible = true;

                usergrpsub.Visible = GetUserPermission(MenuPrivileges, "031");
                usermanagesub.Visible = GetUserPermission(MenuPrivileges, "032");
                usermonitorsub.Visible = GetUserPermission(MenuPrivileges, "033");

                if (!usergrpsub.Visible && !usermanagesub.Visible && !usermonitorsub.Visible)
                {
                    usermanagegrp.Visible = false;
                }

                #endregion
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var _Cookie = Context.Request.Cookies["authenuser"];
                //if (DAO.MasterDA.IsSessionExpired(int.Parse(_Cookie.Values["userid"])))
                //{
                //    this.Response.Redirect("login.aspx");
                //}

                if (!Page.IsPostBack) {
                    if (Context.Request.Cookies["authenuser"] == null)
                    {
                        this.Response.Redirect("login.aspx");
                    }
                    DAO.MasterDA da = new DAO.MasterDA();
                    if (Context.Request.Cookies["authenuser"].Values["grpName"].ToString() != "Administrator")
                    {
                        M_AppSystem appsystem = da.GetLockSystem();
                        if (appsystem != null)
                        {
                            if (appsystem.LockSystem == "Y")
                            {
                                this.Response.Redirect("login.aspx");
                            }
                        }
                    }
            
                    M_Menu menu = da.GetMenuForId(this.Request.Path);
                    if (menu != null)
                    {
                        da.AddLog(Context.Request.Cookies["authenuser"].Values["userid"].ToUpper(),
                            Context.Request.Cookies["authenuser"].Values["Station_Id"].ToUpper(),
                            Context.Request.Cookies["authenuser"].Values["POS_Id"].ToUpper(),
                            menu.Menu_Id);
                    }
                    Label1.Text = Context.Request.Cookies["authenuser"].Values["fname"].ToUpper() + " " + Context.Request.Cookies["authenuser"].Values["lastname"].ToUpper();
                    lbPos.Text = "POS NO. : " + Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToString();
                    if (Context.Request.Cookies["authenuser"].Values["Station_Name"].ToString() == "," || Context.Request.Cookies["authenuser"].Values["Station_Name"].ToString() == "")
                    {
                        lbStation.Text = "Station : -";
                    }
                    else {
                        try
                        {
                            char[] delimiterChars = { ',' };
                            string[] V = Context.Request.Cookies["authenuser"].Values["Station_Name"].ToString().Split(delimiterChars);
                            foreach (string ct in V)
                            {
                                if (ct != "")
                                {
                                    lbStation.Text = "Station : " + ct;
                                    break;
                                }

                            }

                        }
                        catch {

                            lbStation.Text = "Station : " + Context.Request.Cookies["authenuser"].Values["Station_Name"].ToString();
                        }
                    }
                    lbGroupUser.Text = "User Group : " + Context.Request.Cookies["authenuser"].Values["grpName"].ToUpper();
                    string[] vers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                    this.Label2.Text = "Version : " + vers[0] + "." + vers[1];
                }

                var IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() : HttpContext.Current.Request.UserHostAddress;
                if (IpAddress == "::1")
                {
                    var Address = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                    IpAddress = Address != null ? Address.ToString() : string.Empty;
                }
                

                DAO.MasterDA.UpdateLogin(int.Parse(_Cookie.Values["userid"]), "", IpAddress, int.Parse(_Cookie.Values["POS_Id"]), true);
            }
            catch {


            }
          
        }


        protected void Page_Unload(object sender, EventArgs e)
        {
            //var _Cookie = Context.Request.Cookies["authenuser"];

            //DAO.MasterDA.UpdateLogin(int.Parse(_Cookie.Values["userid"]), "", "", null, false);
        }

        protected void btn_LogOut_Click(object sender, EventArgs e)
        {
            var _Cookie = Context.Request.Cookies["authenuser"];

            DAO.MasterDA.UpdateLogin(int.Parse(_Cookie.Values["userid"]), "", "", null, false);

            this.Response.Redirect("login.aspx");
        }

        private bool GetUserPermission(List<M_UserPermission> MenuPrivileges, string Menu_Code)
        {
            var MenuPrivilege = MenuPrivileges.Where(a => a.Menu_Code == Menu_Code).FirstOrDefault();
            if (MenuPrivilege != null && MenuPrivilege.CanView.GetValueOrDefault() == 1)
            {
                return true;
            }
            return false;
        }
    }
}