using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

using System.Web.UI.WebControls;
using CreateInvoiceSystem.DAO;

namespace CreateInvoiceSystem
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            { 
                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoStore();
                Response.AppendHeader("Cache-Control", "no-store");
              //  HttpCookie _cookie = new HttpCookie("authenuser");
                if (_cookie != null)
                {
                    _cookie.Expires = DateTime.Now.AddDays(-1);
                }
                HttpContext.Current.Request.Cookies.Clear();
                Session["pass"] = null;
                HttpContext.Current.Session.Abandon();
                string[] vers = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Label1.Text = vers[0] + "." + vers[1];
                GetStation();
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.UserName.Text = "";
            this.Password.Text = "";
            tr1.Visible = false;
            try
            {
                cmbDropdawnList.Items.Clear();
            }
            catch(Exception ex)
            {
                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
            }
        }

        protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
        {

            var IpAddress =  HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() : HttpContext.Current.Request.UserHostAddress ;
            if (IpAddress == "::1")
            {
                var Address = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                IpAddress = Address != null ? Address.ToString() : string.Empty;
            }

            this.FailureText.Text = "";
            try
            {
                cmbDropdawnList.Items.Clear();
            }
            catch (Exception ex) {
                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
            }
            try
            {
                DAO.MasterDA dal = new DAO.MasterDA();
                M_AppSystem appsystem = dal.GetLockSystem();
                if (appsystem != null)
                {
                    if (appsystem.LockSystem == "Y")
                    {
                        this.FailureText.Text = "System lock , Please login later";
                        this.UserName.Text = "";
                        this.Password.Text = "";
                        return;
                    }
                    
                }

                SqlCommand sqlcmd = new SqlCommand();
                string mode = System.Configuration.ConfigurationManager.AppSettings["UseLDAP"];
                if (mode == "YES")
                {
                    string _path = @"LDAP://" + System.Configuration.ConfigurationManager.AppSettings["AdServerAndPort"];
                    String domainAndUsername = System.Configuration.ConfigurationManager.AppSettings["Domain"] + @"\" + this.UserName.Text;
                    SearchResult result = null;
                    DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, this.Password.Text);

                    bool isfind = true;
                    try
                    {
                        DirectorySearcher search = new DirectorySearcher(entry);
                        search.Filter = "(SAMAccountName=" + this.UserName.Text + ")";
                        result = search.FindOne();

                        if (result != null)
                        {
                            
                        }
                        else
                        {
                            isfind = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        isfind = false;
                        LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
                    }

                    if (isfind)
                    {
                        if (result == null)
                            return;
                        M_Employee _user = null;
                        #region Add User AD TO DB
                        
                        
                        string username = this.UserName.Text;
                        M_Employee emp = new M_Employee();
                        emp.UserCode = username;
                        emp.Username = username;
                        emp.FirstName = username;
                        emp.LastName = username;
                        emp.DefaultMenu = "001";
                        emp.Active = "Y";
                        emp.IsActive = "Y";
                        emp.IsUserAD = true;

                    
                        foreach (Object obj in result.Properties["distinguishedname"])
                        {
                          
                            emp.UserGroup = FindADUserGroup(obj.ToString());
                            if (emp.UserGroup != string.Empty)
                                break;
                        }
                        //string t = result.Properties["distinguishedname"].ToString();
                        //emp.UserGroup = FindADUserGroup(t);
                        if (emp.UserGroup == string.Empty)
                        {
                            this.FailureText.Text = "Your Username invalid User Group";
                            this.UserName.Text = "";
                            this.Password.Text = "";
                            return;
                        }
                        MasterDA da = new MasterDA();
                        _user = da.AddUsersAD(emp);
                        #endregion

                         //_user = da.GetUsers("pos_admin", "pos_admin");
                        if (_user != null)
                        {
                            sqlcmd.CommandText = "SP_CheckPOS";
                            sqlcmd.CommandType = CommandType.StoredProcedure;
                            sqlcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = _user.UserID;
                            DataTable dt = FunctionAndClass.IexcuteDataByDataTable(sqlcmd);
                            _cookie.Values.Add("userid", _user.UserID.ToString());
                            _cookie.Values.Add("Emp_ID", _user.UserCode);
                            _cookie.Values.Add("fname", _user.FirstName);
                            _cookie.Values.Add("lastname", _user.LastName);
                            Session["Emp_ID"] = _user.UserCode;
                            Session["fname"] = _user.FirstName;
                            Session["lastname"] = _user.LastName;
                            Session["Ucode"] = _user.UserCode;
                            Session["menu"] = 1;
                            Session["DefaultMenu"] = _user.DefaultMenu;
                            Session["uid"] = _user.UserID;
                            if (dt != null)
                            {
                                M_Station stn = null;
                            
                                if (string.IsNullOrEmpty(dt.Rows[0]["Station_Code"].ToString()) && string.IsNullOrEmpty(hidfStationCode.Value.ToString()))
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ChooseStation", "ChooseStation();", true);
                                    return;
                                }
                                else if (!string.IsNullOrEmpty(dt.Rows[0]["Station_Code"].ToString()))
                                {
                                    stn = da.GetStation(dt.Rows[0]["Station_Code"].ToString());
                                }
                                else if (!string.IsNullOrEmpty(hidfStationCode.Value.ToString()))
                                {
                                    stn = da.GetStation(hidfStationCode.Value.ToString());
                                }

                                if (stn != null)
                                {
                                    this.FailureText.Text = "Your Username Staion not found";
                                    this.UserName.Text = "";
                                    this.Password.Text = "";

                                }

                                try
                                {
                                   // stn = da.GetStation(dt.Rows[0]["Station_Code"].ToString());
                                    _cookie.Values.Add("Station_Code", dt.Rows[0]["Station_Code"].ToString());
                                    _cookie.Values.Add("Station_Name", dt.Rows[0]["Station_Name"].ToString());
                                    _cookie.Values.Add("Station_Id", stn.Station_Id.ToString());
                                    _cookie.Values.Add("grp", dt.Rows[0]["UserGroupCode"].ToString());
                                    _cookie.Values.Add("grpName", dt.Rows[0]["UserGroupName"].ToString());
                                    Session["grp"] = dt.Rows[0]["UserGroupCode"].ToString();
                                    Session["grpName"] = dt.Rows[0]["UserGroupName"].ToString();
                                    Session["Station_Code"] = dt.Rows[0]["Station_Code"].ToString();
                                    Session["Station_Name"] = dt.Rows[0]["Station_Name"].ToString();
                                    Session["Station_Id"] = stn.Station_Id.ToString();
                                }
                                catch (Exception ex)
                                {
                                    _cookie.Values.Add("grp", "-");
                                    Session["grp"] = "-";
                                    Session["grpName"] = "-";
                                    Session["Station_Code"] = "-";
                                    Session["Station_Name"] = "-";
                                    Session["Station_Id"] = "-";
                                    _cookie.Values.Add("Station_Id", "-1");
                                    _cookie.Values.Add("Station_Name", "-");
                                    _cookie.Values.Add("Station_Code", "-");
                                    LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
                                }

                                if (dt.Rows.Count > 1)
                                {
                                    tr1.Visible = true;
                                    cmbDropdawnList.Items.Add(new ListItem("--Choose--", "0"));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        cmbDropdawnList.Items.Add(new ListItem(dt.Rows[i]["POS_MacNo"].ToString() + " - " + dt.Rows[i]["POS_Code"].ToString(), dt.Rows[i]["POS_Code"].ToString()));
                                    }
                                    return;
                                }
                                else 
                                {
                                    try
                                    {
                                        //_cookie.Values.Add("grp", dt.Rows[0]["UserGroupCode"].ToString());
                                        //_cookie.Values.Add("grpName", dt.Rows[0]["UserGroupName"].ToString());
                                        //Session["grp"] = dt.Rows[0]["UserGroupCode"].ToString();
                                        //Session["grpName"] = dt.Rows[0]["UserGroupName"].ToString();

                                        DAO.MasterDA mda = new DAO.MasterDA();
                                        M_POS_Machine pos = mda.GetPOS(dt.Rows[0]["POS_Code"].ToString());
                                        _cookie.Values.Add("POS_Id", pos.POS_Id.ToString());
                                        _cookie.Values.Add("POS_CODE", dt.Rows[0]["POS_Code"].ToString());
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        //_cookie.Values.Add("grp", "-");
                                        //Session["grp"] = "-";
                                        //Session["grpName"] = "-";
                                        //_cookie.Values.Add("grpName", "-");

                                        
                                        _cookie.Values.Add("POS_Id", "-1");
                                        _cookie.Values.Add("POS_CODE", "-");
                                        LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
                                        
                                    }
                                    FormsAuthentication.SetAuthCookie(_user.UserCode, false);
                                }
                            }
                            Del_Session();
                            this.Response.AppendCookie(_cookie);

                            M_Menu menu = da.GetMenu(_user.DefaultMenu);
                            if (menu != null)
                            {
                                this.Response.Redirect(menu.Menu_Url);
                            }
                            else
                            {
                                this.Response.Redirect("index.aspx");
                            }
                        }
                        else
                        {
                            this.FailureText.Text = "Your Username or Password incorrect";
                            this.UserName.Text = "";
                            this.Password.Text = "";
                        }
                    }
                    else
                    {
                        DoLoginFromDB();
                        //this.FailureText.Text = "Your Username or Password incorrect";
                        //this.UserName.Text = "";
                        //this.Password.Text = "";
                    }
                    
                }
                else
                {
                    DoLoginFromDB();
                }

            }
            catch (Exception ex)
            {
                this.FailureText.Text = ex.Message;
                this.UserName.Text = "";
                this.Password.Text = "";
                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
            }
        }

        private void Del_Session() {

            try
            {
                Session["uid"] = null;
                Session["Emp_ID"] = null;
                Session["fname"] = null;
                Session["lastname"] = null;
                Session["grp"] = null;
                Session["grpName"] = null;
                Session["DefaultMenu"] = null;
                Session["Station_Code"] = null;
                Session["Station_Name"] = null;
                Session["Station_Id"] = null;

                Session.Remove("uid");
                Session.Remove("Emp_ID");
                Session.Remove("fname");
                Session.Remove("lastname");
                Session.Remove("grp");
                Session.Remove("grpName");
                Session.Remove("Station_Code");
                Session.Remove("Station_Name");
                Session.Remove("Station_Id");

                Session.Clear();

            }
            catch (Exception ex) {
                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);

            }
        }
        HttpCookie _cookie = new HttpCookie("authenuser");
        protected void cmbDropdawnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDropdawnList.SelectedIndex.ToString() != "0") 
            {
                var UserOnline = DAO.MasterDA.CheckPOSOnline(int.Parse(Session["uid"].ToString()));
                if (UserOnline != null)
                {
                    this.FailureText.Text = string.Format("This POS is use by: ", UserOnline.FirstName);
                    return;
                }
                _cookie.Values.Add("userid", Session["uid"].ToString());
                _cookie.Values.Add("Emp_ID", Session["Emp_ID"].ToString());
                _cookie.Values.Add("fname", Session["fname"].ToString());
                _cookie.Values.Add("lastname", Session["lastname"].ToString());
                _cookie.Values.Add("grp", Session["grp"].ToString());
                _cookie.Values.Add("grpName", Session["grpName"].ToString());
                _cookie.Values.Add("Station_Code", Session["Station_Code"].ToString());
                _cookie.Values.Add("Station_Name", Session["Station_Name"].ToString());
                _cookie.Values.Add("Station_Id", Session["Station_Id"].ToString());
                // _cookie.Values.Add("POSNAME", cmbDropdawnList.SelectedItem.Text);
                _cookie.Values.Add("POS_CODE", cmbDropdawnList.SelectedItem.Value);
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.CommandText = "SP_GetStationCode";
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@PosCode", SqlDbType.NVarChar).Value = cmbDropdawnList.SelectedItem.Value;
                SqlDataReader dr =    FunctionAndClass.IexcuteDataBySqlReader(sqlcmd);
                try
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DAO.MasterDA mda = new DAO.MasterDA();;
                            M_POS_Machine pos = mda.GetPOS(cmbDropdawnList.SelectedItem.Value.ToString());
                            _cookie.Values.Add("POS_Id", pos.POS_Id.ToString());
                        }
                    }
                    else {
                        _cookie.Values.Add("POS_Id", "-1");
                    }
                    dr.Close();
                    
                    Session["menu"] = 1;
                    
                    this.Response.AppendCookie(_cookie);

                    DAO.MasterDA da = new DAO.MasterDA();
                    M_Menu menu = da.GetMenu(Session["DefaultMenu"].ToString());
                    Del_Session();
                    if (menu != null)
                    {
                        this.Response.Redirect(menu.Menu_Url);
                    }
                    else
                    {
                        this.Response.Redirect("index.aspx");
                    }
                }
                catch(Exception ex) {
                    LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
                }
            }
        }

        private string FindADUserGroup(string str)
        {
            //str = "CN=pos_admin,OU=Admin,OU=POCPos,OU=ICT,OU=FIN,OU=TAA,OU=AirAsia,DC=aagroup,DC=redicons,DC=local";
            MasterDA da = new MasterDA();
            string[] sp = str.Split(',');
            foreach (string s in sp)
            {
               string[] sp2 = s.Split('=');

               if (sp2.Length == 2)
               {
                   if (sp2[0] == "OU")
                   {
                       M_UserGroup g = da.GetUserGroup(sp2[1]);
                       if (g != null)
                       {
                           return g.UserGroupCode;
                       }
                   }
               }
            }

            return "";
        }

        private void DoLoginFromDB()
        {
            try
            {
                SqlCommand sqlcmd = new SqlCommand();
                string IpAddress = GetIPAddress();
                DAO.MasterDA da = new DAO.MasterDA();
                M_Employee _user = da.GetUsers(this.UserName.Text, this.Password.Text);
                if (_user != null)
                {
                    var UserOnline = DAO.MasterDA.CheckUserOnline(int.Parse(_user.UserID.ToString()), IpAddress);
                    if (false)//if (UserOnline != null)
                    {
                        var POS = DAO.MasterDA.GetPOSbyId(UserOnline.Last_POS_ID.Value);
                        var Station = DAO.MasterDA.GetStationByCode(POS.Station_Code);
                        this.FailureText.Text = string.Format("This User is online on <br />IP: {4} <br />Station: {0}-{1} <br />POS: {2}-{3}", Station.Station_Code, Station.Station_Name, POS.POS_Code, POS.POS_MacNo, UserOnline.IP_Address);
                        return;
                    }
                    sqlcmd.CommandText = "SP_CheckPOS";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = _user.UserID;
                    DataTable dt = FunctionAndClass.IexcuteDataByDataTable(sqlcmd);
                    _cookie.Values.Add("userid", _user.UserID.ToString());
                    _cookie.Values.Add("Emp_ID", _user.UserCode);
                    _cookie.Values.Add("fname", _user.FirstName);
                    _cookie.Values.Add("lastname", _user.LastName);
                    Session["Emp_ID"] = _user.UserCode;
                    Session["fname"] = _user.FirstName;
                    Session["lastname"] = _user.LastName;
                    Session["Ucode"] = _user.UserCode;
                    Session["menu"] = 1;
                    Session["DefaultMenu"] = _user.DefaultMenu;
                    Session["uid"] = _user.UserID;
                    if (dt != null)
                    {
                        try
                        {
                            M_Station stn = da.GetStation(dt.Rows[0]["Station_Code"].ToString());
                            _cookie.Values.Add("Station_Code", dt.Rows[0]["Station_Code"].ToString());
                            _cookie.Values.Add("Station_Name", dt.Rows[0]["Station_Name"].ToString());
                            _cookie.Values.Add("Station_Id", stn.Station_Id.ToString());
                            _cookie.Values.Add("grp", dt.Rows[0]["UserGroupCode"].ToString());
                            _cookie.Values.Add("grpName", dt.Rows[0]["UserGroupName"].ToString());
                            Session["grp"] = dt.Rows[0]["UserGroupCode"].ToString();
                            Session["grpName"] = dt.Rows[0]["UserGroupName"].ToString();
                            Session["Station_Code"] = dt.Rows[0]["Station_Code"].ToString();
                            Session["Station_Name"] = dt.Rows[0]["Station_Name"].ToString();
                            Session["Station_Id"] = stn.Station_Id.ToString();
                        }
                        catch (Exception ex)
                        {
                            _cookie.Values.Add("grp", "-");
                            Session["grp"] = "-";
                            Session["grpName"] = "-";
                            Session["Station_Code"] = "-";
                            Session["Station_Name"] = "-";
                            Session["Station_Id"] = "-";
                            _cookie.Values.Add("Station_Id", "-1");
                            _cookie.Values.Add("Station_Name", "-");
                            _cookie.Values.Add("Station_Code", "-");
                            LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
                        }
                        if (dt.Rows.Count > 1)
                        {

                            tr1.Visible = true;
                            cmbDropdawnList.Items.Add(new ListItem("--Choose--", "0"));
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                cmbDropdawnList.Items.Add(new ListItem(dt.Rows[i]["POS_MacNo"].ToString() + " - " + dt.Rows[i]["POS_Code"].ToString(), dt.Rows[i]["POS_Code"].ToString()));
                            }
                            this.Response.AppendCookie(_cookie);
                            return;
                        }
                        else
                        {
                            try
                            {
                                DAO.MasterDA mda = new DAO.MasterDA();
                                M_POS_Machine pos = mda.GetPOS(dt.Rows[0]["POS_Code"].ToString());
                                _cookie.Values.Add("POS_Id", pos.POS_Id.ToString());
                                _cookie.Values.Add("POS_CODE", dt.Rows[0]["POS_Code"].ToString());

                            }
                            catch (Exception ex)
                            {
                                _cookie.Values.Add("POS_Id", "-1");
                                _cookie.Values.Add("POS_CODE", "-");
                                LogException.Save(ex, HttpContext.Current.Request.Url.AbsoluteUri, UserName.Text);
                            }
                            FormsAuthentication.SetAuthCookie(_user.UserCode, false);
                        }
                    }
                    Del_Session();
                    this.Response.AppendCookie(_cookie);

                    M_Menu menu = da.GetMenu(_user.DefaultMenu);
                    if (menu != null)
                    {
                        this.Response.Redirect(menu.Menu_Url);
                    }
                    else
                    {
                        this.Response.Redirect("index.aspx");
                    }
                }
                else
                {
                    this.FailureText.Text = "Your Username or Password incorrect";
                    this.UserName.Text = "";
                    this.Password.Text = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GetIPAddress()
        {
            var IpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() : HttpContext.Current.Request.UserHostAddress;
            if (IpAddress == "::1")
            {
                var Address = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                IpAddress = Address != null ? Address.ToString() : string.Empty;
            }
            return IpAddress;
        }

        private void GetStation()
        {
            using (var context = new POSINVEntities())
            {
                List<M_Station> stations = context.M_Station.Where(x => (x.IsActive == "Y" && x.Station_Code != "System")).ToList();
                Session["All_Station"] = stations;
            }
        }

        public string TableStation()
        {

            string htmlStr = "";
            if (Session["All_Station"] != null)
            {
                List<M_Station> stations = (List<M_Station>)Session["All_Station"];
                int count = 0;
                foreach (var item in stations)
                {
                    htmlStr +=
                        @"<tr><td><img src='Images/Text-Edit-icon.png' onclick=""ChooseItem('" + item.Station_Code + "');\" Style='cursor: pointer; width: 18px;' /></td><td>" + item.Station_Name + @"</td></tr>";
                    count++;
                }
            }
            return htmlStr;
        }
    }
}