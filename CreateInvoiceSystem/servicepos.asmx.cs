using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using CreateInvoiceSystem.DAO;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using CreateInvoiceSystem.Model;
using Newtonsoft.Json;

namespace CreateInvoiceSystem
{
    /// <summary>
    /// Summary description for servicepos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class servicepos : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Json)]
        public string SaveSlipMassege(string Code,List<string> _Detail,string _type) 
        {
            string str = "";
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_SlipMessage.Where(x => x.SlipMessage_Code == Code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    
                    if (_type == "new")
                    {
                        SqlConnection conn = new SqlConnection(FunctionAndClass.Strconn);
                        conn.Open();
                        SqlTransaction transql = conn.BeginTransaction();

                        try
                        {
                            cmd = new SqlCommand();
                            cmd.CommandText = "SP_ins_M_SlipMessage";
                            cmd.CommandType = CommandType.StoredProcedure;
                            string GetID = "";
                            try
                            {
                                if (Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString() != "")
                                {
                                    GetID = Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString();
                                }
                            }
                            catch
                            {
                            }
                            var HeaderP = new object[] {
                                new SqlParameter("@SlipMessage_Code",Code),
                                new SqlParameter("@Create_By",GetID),
                            };
                            cmd.Parameters.AddRange(HeaderP);

                            cmd.Connection = conn;
                            cmd.Transaction = transql;
                            int J = Convert.ToInt32(cmd.ExecuteScalar());

                            char[] Cr = { '|' };

                            int SID = 0;

                            for (int ff = 0; ff < _Detail.Count; ff++)
                            {
                                SqlCommand q1 = new SqlCommand();
                                q1.CommandText = "SP_ins_M_SlipMessageDetails";
                                q1.CommandType = CommandType.StoredProcedure;
                                q1.Connection = conn;
                                q1.Transaction = transql;

                                string[] ct = _Detail[ff].Split(Cr);
                                SID += 1;
                                string mag = ct[1].ToString();
                                var p = new object[] {
                                new SqlParameter("@SlipMessage_Id",J),
                                new SqlParameter("@Descriptions",ct[0].ToString() != "" ? ct[0].ToString() : ""),
                                new SqlParameter("@SortID",Convert.ToInt32(SID)),
                                new SqlParameter("@SlipStat",mag != "Footer" ? 1:0),
                                new SqlParameter("@Active",ct[2].ToString())
                            };
                                q1.Parameters.AddRange(p);
                                q1.ExecuteNonQuery();

                            }
                            transql.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            transql.Rollback();
                            conn.Close();
                            str = Code + " already exists";
                        }
                    }
                }
                else
                {
                    str = Code + " already exists";
                }
            }
            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Json)]
        public string EditSlipMassege(string ID, string Code, List<string> _Detail)
        {
            string str = "";
            using (var context = new POSINVEntities())
            {
                int _Id = int.Parse(ID);
                var _itemexist = context.M_SlipMessage.Where(x => x.SlipMessage_Code == Code && x.SlipMessage_Id != _Id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    SqlConnection conn = new SqlConnection(FunctionAndClass.Strconn);
                    conn.Open();
                    SqlTransaction transql = conn.BeginTransaction();
                    try
                    {
                        cmd = new SqlCommand();
                        cmd.CommandText = "SP_upd_M_SlipMessage";
                        cmd.CommandType = CommandType.StoredProcedure;
                        string GetID = "";
                        try
                        {
                            if (Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString() != "")
                            {
                                GetID = Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString();
                            }
                        }
                        catch
                        {
                        }
                        var HeaderP = new object[] {
                            new SqlParameter("@ID",ID),
                            new SqlParameter("@SlipMessage_Code",Code),
                            new SqlParameter("@Update_By",GetID),
                        };
                        cmd.Parameters.AddRange(HeaderP);

                        cmd.Connection = conn;
                        cmd.Transaction = transql;
                        cmd.ExecuteNonQuery();

                        char[] Cr = { '|' };

                        int SID = 0;

                        for (int ff = 0; ff < _Detail.Count; ff++)
                        {
                            SqlCommand q1 = new SqlCommand();
                            q1.CommandText = "SP_ins_M_SlipMessageDetails";
                            q1.CommandType = CommandType.StoredProcedure;
                            q1.Connection = conn;
                            q1.Transaction = transql;

                            string[] ct = _Detail[ff].Split(Cr);
                            SID += 1;
                            string mag = ct[1].ToString();
                            var p = new object[] {
                            new SqlParameter("@SlipMessage_Id",ID),
                            new SqlParameter("@Descriptions",ct[0].ToString() != "" ? ct[0].ToString() : ""),
                            new SqlParameter("@SortID",Convert.ToInt32(SID)),
                            new SqlParameter("@SlipStat",mag != "Footer" ? 1:0),
                            new SqlParameter("@Active",ct[2].ToString())
                        };
                        q1.Parameters.AddRange(p);
                        q1.ExecuteNonQuery();

                        }
                        transql.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        transql.Rollback();
                        conn.Close();
                        str = Code + " already exists";
                    }
                }
                else
                {
                    str = Code + " already exists";
                }
            }

            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public List<string> GetSlipMassege(string ID) {
            List<string> str = new List<string>();
            cmd = new SqlCommand();
            cmd.CommandText = "SP_LoadEdit_M_SlipMessage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SlipMessage_Id", SqlDbType.Int).Value = Convert.ToInt32(ID);
            DataTable dst = FunctionAndClass.IexcuteDataByDataTable(cmd);
            if (dst != null) {

                if (dst.Rows.Count > 0)
                {
                   
                    for (int i = 0; i < dst.Rows.Count; i++) {
                        str.Add(dst.Rows[i]["SlipMessage_Code"].ToString() + "|" + dst.Rows[i]["Descriptions"].ToString() + "|" + dst.Rows[i]["SlipStat"].ToString() + "|" + dst.Rows[i]["Active"].ToString());
                    }
                }
            }
            return str;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveSendMessage(string DescMessage, string s,string e, string stat)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_ins_M_SendMessage";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@DescMessage", SqlDbType.NVarChar).Value = DescMessage;

            DateTime ss = Convert.ToDateTime((s.Substring(3, 2) + "-" + s.Substring(0, 2) + "-" + s.Substring(6, 4)));
            DateTime ee = Convert.ToDateTime((e.Substring(3, 2) + "-" + e.Substring(0, 2) + "-" + e.Substring(6, 4)));


            if (ss > ee) return "Please Check Data : Start Date > Expire Date!";
            try
            {

                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = ss;

            }
            catch {

                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = "";

            }

            try
            {

                cmd.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = ee;

            }
            catch
            {

                cmd.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = "";

            }
            if (stat.ToUpper() == "TRUE")
            {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "Y";
            }
            else {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "N";
            }
            try
            {
                if (Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString() != "") cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            }
            catch {
                cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = "-";

            }
           
            if (FunctionAndClass.IexcuteData(cmd))
            {
                return "";
            }
            else {
                return DescMessage + " already exists";
            };
        }
        private bool Check_UserGroupCode(string code) {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_Check_M_UserGroupByCode";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = code;
            if (FunctionAndClass.IexcuteDataByScalar(cmd) > 0)
            {
                return false;
            }
            else {
                return true;
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveUserGroup(string code, string name, string Isactive)
        {
            if (Check_UserGroupCode(code) == false)  return code + " already exists";

            cmd = new SqlCommand();
            cmd.CommandText = "SP_ins_M_UserGroup";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserGroupCode", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("@UserGroupName", SqlDbType.NVarChar).Value = name;

            if (Isactive.ToUpper() == "TRUE")
            {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "Y";
            }
            else {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "N";
            }
            try
            {
                if (Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString() != "") cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            }
            catch
            {
                cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = "-";

            }

            if (FunctionAndClass.IexcuteData(cmd))
            {
                return "";
            }
            else {
                return code + " already exists";
            };
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetUserGroup(string ID)
        {
            var serializer = new JavaScriptSerializer();
            var ss = "";

            using (var context = new POSINVEntities())
            {
                var Id = int.Parse(ID);
                if (context.M_UserGroup.Count(x => x.UserGroupId == Id) > 0)
                {
                    var UserGroup = context.M_UserGroup.Where(x => x.UserGroupId == Id).ToList().Select(s => new M_UserGroup()
                    {
                        UserGroupId = s.UserGroupId,
                        UserGroupCode = s.UserGroupCode,
                        UserGroupName = s.UserGroupName,
                        IsActive = s.IsActive,
                        M_UserGroupMappingAD = s.M_UserGroupMappingAD.OrderBy(d => d.AD_Department).Select(c => new M_UserGroupMappingAD() { AD_Department = c.AD_Department }).ToList()
                    }).ToList();

                    ss = serializer.Serialize(UserGroup);
                }
            }

            return ss;
        }


        private bool Check_AgencyCode(string code) {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_CheckCode_M_Agency";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = code;
            if (FunctionAndClass.IexcuteDataByScalar(cmd) > 0)
            {
                return false;
            }
            else {
                return true;
            }
        }
        SqlCommand cmd;
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveAgencyType(string code, string name)
        {
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_AgencyType.Where(x => x.Agent_type_code == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "SP_ins_AgencyType";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Agent_type_code", SqlDbType.NVarChar).Value = code;
                    cmd.Parameters.Add("@Agent_type_Name", SqlDbType.NVarChar).Value = name;
                    try
                    {
                        if (Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString() != "") cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
                    }
                    catch
                    {

                        cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = "";
                    }

                    if (FunctionAndClass.IexcuteData(cmd))
                    {
                        return "";
                    }
                    else
                    {
                        return code + " already exists";
                    };
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetM_SendMessage(string ID)
        {
            cmd = new SqlCommand();
            string ss = "";
          cmd.CommandText = "SP_Load_M_SendMessage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@getwhere", SqlDbType.NVarChar).Value = " and (ID = " + ID + ") ";
            DataTable dt = FunctionAndClass.IexcuteDataByDataTable(cmd);
            if (dt != null) 
            {
                if (dt.Rows.Count > 0) 
                { 
                    List<M_SendMessage> _list = new List<M_SendMessage>();
                    M_SendMessage sitem = new M_SendMessage();
                    sitem.ID = Convert.ToInt32(ID);
                    sitem.DescMessage = dt.Rows[0]["DescMessage"].ToString() ;
                    DateTime StartDate = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
                    DateTime _e = Convert.ToDateTime(dt.Rows[0]["ExpireDate"].ToString());
                    sitem.Create_By = StartDate.ToString("dd/MM/yyyy"); ;// StartDate.Day.ToString("00") + "-" + StartDate.Month.ToString("00") + "-" + StartDate.Year.ToString();
                    sitem.Update_By = _e.ToString("dd/MM/yyyy"); //_e.Day.ToString("00") + "-" + _e.Month.ToString("00") + "-" + _e.Year.ToString();
                    //sitem.StartDate = StartDate.ToString("dd/MM/yyyy");
                    //sitem.ExpireDate = _e.ToString("dd/MM/yyyy");
                    //sitem.IsActive = stat;
                    //sitem.Create_By = StartDate.Day.ToString("00") + "-" + StartDate.Month.ToString("00") + "-" + StartDate.Year.ToString();
                    //sitem.Update_By = _e.Day.ToString("00") + "-" + _e.Month.ToString("00") + "-" + _e.Year.ToString();
                    sitem.IsActive = dt.Rows[0]["IsActive"].ToString();
                    _list.Add(sitem);
                    var serializer = new JavaScriptSerializer();
                    ss=  serializer.Serialize(_list.ToArray());
                }
            }
            return ss;
        }
       
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditUserGroup(string ID, string code, string name, string stat, string _viewstat, string oldcode, string AD_Department)
        {
            //ปกรณีแก้ไข ตรวจสอบ Usergroup_Code
            if (_viewstat.ToUpper() == "TRUE") if (oldcode != code) if (Check_UserGroupCode(code) == false) return code + " already exists";

            var Id = Convert.ToInt32(ID);
            cmd = new SqlCommand();
            cmd.CommandText = "SP_upd_M_UserGroup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserGroupId", SqlDbType.Int).Value = Id;
            cmd.Parameters.Add("@UserGroupCode", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("@UserGroupName", SqlDbType.NVarChar).Value = name;
            if (stat == "true")
            {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "Y";
            }
            else {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "N";
            }
            try
            {
                if (Context.Request.Cookies["authenuser"].Values["Emp_ID"] != "") cmd.Parameters.Add("@Update_By", SqlDbType.NVarChar).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            }
            catch
            {
                cmd.Parameters.Add("@Update_By", SqlDbType.NVarChar).Value = "0";

            }
            if (FunctionAndClass.IexcuteData(cmd))
            {
                using (var context = new POSINVEntities())
                {
                    if (context.M_UserGroupMappingAD.Count(x => x.UserGroupId == Id) > 0)
                    {
                        var UserGroupMappingADs = context.M_UserGroupMappingAD.Where(x => x.UserGroupId == Id).ToList();
                        foreach (var item in UserGroupMappingADs)
                        {
                            context.M_UserGroupMappingAD.Remove(item);
                        }
                    }

                    
                    if (AD_Department.Length > 0)
                    {
                        var AD_DepartmentList = AD_Department.Split('|');
                        foreach (var item in AD_DepartmentList)
                        {
                            context.M_UserGroupMappingAD.Add(new M_UserGroupMappingAD()
                            {
                                RowId = Guid.NewGuid(),
                                UserGroupId = Id,
                                AD_Department = item
                            });
                        }
                    }

                    context.SaveChanges();
                    context.Dispose();

                }
                return "";
            }
            else {
                return code + " already exists";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditM_SendMessage(string ID, string name, string s, string e, string stat)
        {
            DateTime ss = Convert.ToDateTime((s.Substring(3, 2) + "-" + s.Substring(0, 2) + "-" + s.Substring(6, 4)));
            DateTime ee = Convert.ToDateTime((e.Substring(3, 2) + "-" + e.Substring(0, 2) + "-" + e.Substring(6, 4)));
            if (ss > ee) return "Please Check Data : Start Date > Expire Date!";
            cmd = new SqlCommand();
            cmd.CommandText = "SP_upd_M_SendMessage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            cmd.Parameters.Add("@DescMessage", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = ss;
            cmd.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = ee;
            if (stat == "true")
            {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "Y";
            }
            else {
                cmd.Parameters.Add("@IsActive", SqlDbType.NVarChar).Value = "N";
            }
            try
            {
                if (Context.Request.Cookies["authenuser"].Values["Emp_ID"] != "")  cmd.Parameters.Add("@Update_By", SqlDbType.NVarChar).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            }
            catch {
                cmd.Parameters.Add("@Update_By", SqlDbType.NVarChar).Value = "0";

            }


            if (FunctionAndClass.IexcuteData(cmd))
            {
                return "";
            }
            else {
                return name + " already exists";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetAgencyType(string code, string name)
        {
            List<M_AgencyType> _list = new List<M_AgencyType>();
            M_AgencyType _item = new M_AgencyType();
            _item.Agent_type_code = code;
            _item.Agent_type_Name = name;
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditAgencyType(string ID,string code, string name)
        {
            using (var context = new POSINVEntities())
            {
                int _Id = int.Parse(ID);
                var _itemexist = context.M_AgencyType.Where(x => x.Agent_type_code == code && x.AgencyTypeId != _Id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "SP_upd_AgencyType";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    cmd.Parameters.Add("@Agent_type_code", SqlDbType.NVarChar).Value = code;
                    cmd.Parameters.Add("@Agent_type_Name", SqlDbType.NVarChar).Value = name;
                    try
                    {
                        if (Context.Request.Cookies["authenuser"].Values["Emp_ID"] != "") cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
                    }
                    catch
                    {
                        cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = "0";

                    }



                    if (FunctionAndClass.IexcuteData(cmd))
                    {
                        return "";
                    }
                    else
                    {
                        return code + " already exists";
                    }
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveCountry(string code, string name)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_Country.Where(x => x.CountryCode == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.SaveCountry(code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetCountry(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_Country _item = da.GetCountry(int.Parse(id));
            List<M_Country> _list = new List<M_Country>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditCountry(string code, string name, string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id  = int.Parse(id);
                var _itemexist = context.M_Country.Where(x => x.CountryCode == code && x.CountryId != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.EditCountry(int.Parse(id), code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveProvince(string code, string name)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_Province.Where(x => x.ProvinceCode == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.SaveProvince(code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetProvince(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_Province _item = da.GetProvince(int.Parse(id));
            List<M_Province> _list = new List<M_Province>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditProvince(string code, string name, string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _itemexist = context.M_Province.Where(x => x.ProvinceCode == code && x.ProvinceId != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.EditProvince(int.Parse(id), code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveFlightType(string code, string name)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_FlightType.Where(x => x.Flight_Type_Code == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.SaveFlightType(code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetFlightType(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_FlightType _item = da.GetFlightType(int.Parse(id));
            List<M_FlightType> _list = new List<M_FlightType>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditFlightType(string code, string name, string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _itemexist = context.M_FlightType.Where(x => x.Flight_Type_Code == code && x.Flight_Type_Id != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.EditFlightType(int.Parse(id), code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveFlightFee(string code, string name, string displayabb, string abbgrp, string strFlightFeeGroupID)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_FlightFee.Where(x => x.Flight_Fee_Code == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    Guid? FlightFeeGroupID = null;
                    Guid parseGuid;
                    if (Guid.TryParse(strFlightFeeGroupID, out parseGuid))
                    {
                        FlightFeeGroupID = parseGuid;
                    }

                    da.SaveFlightFee(code, name, (displayabb.ToUpper() == "TRUE" ? "Y" : "N"), abbgrp, Context.Request.Cookies["authenuser"].Values["Emp_ID"], FlightFeeGroupID);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetFlightFee(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_FlightFee _item = da.GetFlightFee(int.Parse(id));
            List<M_FlightFee> _list = new List<M_FlightFee>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditFlightFee(string code, string name, string displayabb, string abbgrp, string id, string strFlightFeeGroupID)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _itemexist = context.M_FlightFee.Where(x => x.Flight_Fee_Code == code && x.Flight_Fee_Id != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    Guid? FlightFeeGroupID = null;
                    Guid parseGuid;
                    if (Guid.TryParse(strFlightFeeGroupID, out parseGuid))
                    {
                        FlightFeeGroupID = parseGuid;
                    }

                    da.EditFlightFee(int.Parse(id), code, name, (displayabb.ToUpper() == "TRUE" ? "Y" : "N"), abbgrp, Context.Request.Cookies["authenuser"].Values["Emp_ID"], FlightFeeGroupID);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveAgency(string code, string name, string geninv,string AgencyType)
        {

            if (Check_AgencyCode(code) == false) return code + " already exists";
            cmd = new SqlCommand();
            cmd.CommandText = "SP_ins_Agency";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@Agency_Code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("@Agency_Name", SqlDbType.NVarChar).Value = name;
            if (geninv.ToUpper() == "TRUE")
            {
                cmd.Parameters.Add("@GenInvoice", SqlDbType.NVarChar).Value = "Y";
            }
            else {
                cmd.Parameters.Add("@GenInvoice", SqlDbType.NVarChar).Value = "N";
            }  
            cmd.Parameters.Add("@Agency_Type_Code", SqlDbType.NVarChar).Value = AgencyType;
            try
            {
                if (Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString() != "") cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];

            }
            catch {

                cmd.Parameters.Add("@Create_By", SqlDbType.NVarChar).Value = "0";

            }
            if (FunctionAndClass.IexcuteData(cmd))
            {
                return "";
            }
            else {
                return code + " already exists";
            };
        }
      
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetAgency(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_Agency _item = da.GetAgency(int.Parse(id));
            List<M_Agency> _list = new List<M_Agency>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditAgency(string code, string name, string geninv, string id,string AgenType,string CheckCode)
        {
            if (CheckCode != code) {
                if (Check_AgencyCode(code) == false) return code + " already exists";
            }

            cmd = new SqlCommand();
            cmd.CommandText = "SP_upd_M_Agency";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Agency_Code", SqlDbType.NVarChar).Value = code;
            cmd.Parameters.Add("@Agency_Name", SqlDbType.NVarChar).Value = name;
            if (geninv.ToUpper() == "TRUE" || geninv.ToUpper() == "Y")
            {
                cmd.Parameters.Add("@GenInvoice", SqlDbType.NVarChar).Value = "Y";
            }
            else {
                cmd.Parameters.Add("@GenInvoice", SqlDbType.NVarChar).Value = "N";
            }
            cmd.Parameters.Add("@Update_By", SqlDbType.Int).Value = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            cmd.Parameters.Add("@Agency_Type_Code", SqlDbType.NVarChar).Value = AgenType;
            cmd.Parameters.Add("@Agency_Id", SqlDbType.Int).Value = id;
            if (FunctionAndClass.IexcuteData(cmd))
            {
                return "";
            }
            else {
                return code + " already exists";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveCnReason(string code, string name)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_CNReason.Where(x => x.CNReason_Code == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.SaveCNReason(code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetCnReason(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_CNReason _item = da.GetCNReason(int.Parse(id));
            List<object> _list = new List<object>();
            _list.Add(new
            {
                CNReason_Id = _item.CNReason_Id,
                CNReason_Code = _item.CNReason_Code,
                CNReason_Name = _item.CNReason_Name,
                IsActive = _item.IsActive,
                Create_By = _item.Create_By,
                Create_Date = _item.Create_Date,
                Update_By = _item.Update_By,
                Update_Date = _item.Update_Date
            });
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditCnReason(string code, string name, string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _itemexist = context.M_CNReason.Where(x => x.CNReason_Code == code && x.CNReason_Id != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.EditCNReason(int.Parse(id), code, name, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SavePaymentType(string code, string name, string geninvoice)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_PaymentType.Where(x => x.PaymentType_Code == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.SavePaymentType(code, name, (geninvoice.ToUpper() == "TRUE" ? "Y" : "N"), Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetPaymentType(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_PaymentType _item = da.GetPaymentType(int.Parse(id));
            List<M_PaymentType> _list = new List<M_PaymentType>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditPaymentType(string code, string name, string geninvoice, string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _itemexist = context.M_PaymentType.Where(x => x.PaymentType_Code == code && x.PaymentType_Id != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.EditPaymentType(int.Parse(id), code, name, (geninvoice.ToUpper() == "TRUE" ? "Y" : "N"), Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveStation(string code, string name, string location, string active, bool ABB_Print_A4, bool INV_Print_A4, bool CN_Print_A4, int ABB_SlipMessage_Id, int INV_SlipMessage_Id, int CN_SlipMessage_Id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_Station.Where(x => x.Station_Code == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.SaveStation(
                                    code, 
                                    name, 
                                    location, 
                                    (active.ToUpper() == "TRUE" ? "Y" : "N"), 
                                    Context.Request.Cookies["authenuser"].Values["Emp_ID"],
                                    ABB_Print_A4,
                                    INV_Print_A4,
                                    CN_Print_A4,
                                    ABB_SlipMessage_Id,
                                    INV_SlipMessage_Id,
                                    CN_SlipMessage_Id);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetStation(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_Station _item = da.GetStation(int.Parse(id));
            List<M_Station> _list = new List<M_Station>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditStation(string code, string name, string location, string active, string id, bool ABB_Print_A4, bool INV_Print_A4, bool CN_Print_A4, int ABB_SlipMessage_Id, int INV_SlipMessage_Id, int CN_SlipMessage_Id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _Active = active.ToUpper() == "TRUE" ? "Y" : "N";

                var original = context.M_Station.Where(x => x.Station_Id == _id).FirstOrDefault();
                if (original != null)
                {
                    if (original.Active == "Y" && _Active == "N")
                    {
                        if (context.M_POS_Machine.Count(x => x.Station_Code == original.Station_Code) > 0 ||
                        context.M_Employee.Count(x => x.Station_Code == original.Station_Code) > 0 ||
                        context.T_Invoice_Info.Count(x => x.BranchNo == original.Station_Code) > 0 ||
                        context.T_Invoice_Manual.Count(x => x.Station_Code == original.Station_Code) > 0
                        )
                        {
                            return "Can't set INACTIVE because this data is already used.";
                        }
                    }
                    
                }
                else
                {
                    return "Not Found Data!!!";
                }

                var _itemexist = context.M_Station.Where(x => x.Station_Code == code && x.Station_Id != _id && x.IsActive == "Y").FirstOrDefault();
                if (_itemexist == null)
                {
                    da.EditStation(
                                    int.Parse(id),
                                    code, 
                                    name, 
                                    location,
                                    _Active, 
                                    Context.Request.Cookies["authenuser"].Values["Emp_ID"],
                                    ABB_Print_A4,
                                    INV_Print_A4,
                                    CN_Print_A4,
                                    ABB_SlipMessage_Id,
                                    INV_SlipMessage_Id,
                                    CN_SlipMessage_Id
                                   );
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SavePOS(string code, string POS_MacNo, string Station_Code, string SlipMessage_Code
            , string SetEJ, string CusDisplay, string CashDrawer, string FormatPrint, string Active)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var _itemexist = context.M_POS_Machine.Where(x => x.POS_Code == code && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null) 
                {
                    da.SavePOS(code, POS_MacNo, Station_Code, SlipMessage_Code
            , (SetEJ.ToUpper() == "TRUE" ? "Y" : "N"), (CusDisplay.ToUpper() == "TRUE" ? "Y" : "N"), (CashDrawer.ToUpper() == "TRUE" ? "Y" : "N")
            , (FormatPrint.ToUpper() == "TRUE" ? "Y" : "N"), (Active.ToUpper() == "TRUE" ? "Y" : "N"), Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetPOS(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_POS_Machine _item = da.GetPOS(int.Parse(id));
            List<M_POS_Machine> _list = new List<M_POS_Machine>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list);
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditPOS(string code, string POS_MacNo, string Station_Code, string SlipMessage_Code
            , string SetEJ, string CusDisplay, string CashDrawer, string FormatPrint, string Active, string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _SetEJ = (SetEJ.ToUpper() == "TRUE" ? "Y" : "N");
                var _CusDisplay = (CusDisplay.ToUpper() == "TRUE" ? "Y" : "N");
                var _CashDrawer = (CashDrawer.ToUpper() == "TRUE" ? "Y" : "N");
                var _FormatPrint = (FormatPrint.ToUpper() == "TRUE" ? "Y" : "N");
                var _Active = (Active.ToUpper() == "TRUE" ? "Y" : "N");

                var original = context.M_POS_Machine.Where(x => x.POS_Id == _id).FirstOrDefault();
                if (original != null)
                {
                    if (original.Active == "Y" && _Active == "N")
                    {
                        if (context.T_LogAccess.Count(x => x.POS_Id == original.POS_Id) > 0 ||
                       context.T_Invoice_Manual.Count(x => x.POS_Code == original.POS_Code) > 0 ||
                       context.T_Invoice_Info.Count(x => x.POS_Code == original.POS_Code) > 0 ||
                       context.T_ABB.Count(x => x.POS_Id == original.POS_Id) > 0
                       )
                        {
                            return "Can't set INACTIVE because this data is already used.";
                        }
                    }
                }
                else
                {
                    return "Not Found Data!!!";
                }

                var _itemexist = context.M_POS_Machine.Where(x => x.POS_Code == code && x.POS_Id != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.EditPOS(int.Parse(id),
                        code, POS_MacNo, Station_Code, SlipMessage_Code, _SetEJ, _CusDisplay, _CashDrawer, 
                        _FormatPrint, _Active, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return code + " already exists";
                }
            }
        }

        //Customer Master
        #region Customer Master...
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MessageResponse SaveCustomer(M_Customer obj, string mode)
        {
            /*string cust_code,string first_name,
                                            string last_name,string prfx_name,                                            
                                            string mode*/
            // obj = new M_Customer();
            MessageResponse msg = new MessageResponse();
            DAO.MasterDA da = new DAO.MasterDA();
            var customer = new M_Customer();
            obj.last_upd_by = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            obj.last_upd_dt = DateTime.Now;
            obj.IsActive = "Y";
            DeepCopyUtility.CopyObjectData(obj, customer);
            if (mode == "insert"){
                obj.CustomerID = Guid.NewGuid();
                msg = da.InsertCustomer(obj);
            }
            else if (mode == "edit"){
                msg = da.EditCustomer(obj);
            }            
            else if (mode == "delete") {
                msg = da.DeleteCustomeer(obj.CustomerID);
            }
            return msg;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public MessageResponse SaveCustomerXml(string key, string mode)
        {
            M_Customer obj = new M_Customer();
            MessageResponse msg = new MessageResponse();
            DAO.MasterDA da = new DAO.MasterDA();
            var customer = new M_Customer();
            obj.last_upd_by = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            obj.last_upd_dt = DateTime.Now;
            obj.IsActive = "Y";
            DeepCopyUtility.CopyObjectData(obj, customer);
            if (mode == "insert")
            {
                //msg = da.InsertCustomer(obj);
            }
            else if (mode == "edit")
            {
                //msg = da.EditCustomer(obj);
            }
            else if (mode == "delete")
            {
                //msg = da.DeleteCustomeer(obj.cust_code);
            }
            return msg;
        }

        public List<M_Customer> GetCustomerList()
        { 
            DAO.MasterDA da = new DAO.MasterDA();
            var res = da.GetCustomerList();
            return res;
        }

        public List<M_Customer> GetCustomerListByCode(string key, string name)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            var res = da.GetCustomerListByCode(key, name);
            return res;
        }
 
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public M_Customer GetCustomerByCode(Guid key, string mode)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            var customer = new M_Customer();
            var res = da.GetCustomerById(key);            
            return res; 
        }
        #endregion       

        //Currency Master
        #region Currency Master...
        public List<M_Currency> getMstCurrencyList()
        {
            DAO.MasterDA da = new DAO.MasterDA();
            var res = da.GetCurrencyList();
            return res;
        }
        public List<M_Currency> getMstcurrencyListByCode(string key, DateTime? CurrencyDate)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            var res = da.GetCurrencyListByCode(key, CurrencyDate);
            return res;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public M_Currency GetMstCurrencyByCode(string key, string mode)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            var res = da.GetCurrencyByCode(key);
            return res;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MessageResponse SaveMstCurrency(M_Currency obj, string mode)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            MessageResponse msg = new MessageResponse();
            obj.Update_Date = DateTime.Now;
            obj.Update_By = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            obj.Create_By = Context.Request.Cookies["authenuser"].Values["Emp_ID"];
            obj.Create_Date = DateTime.Now;
            obj.IsActive = "Y";
            if (mode =="add")
            {
                msg = da.AddCurrency(obj); 
            }
            else if (mode == "edit")
            {
                msg = da.EditCurrency(obj);
            }
            else if (mode == "delete")
            {
                obj.IsActive = "N";
                msg = da.DeleteCurrency(obj.Currency_Id);
            }
            return msg;
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public T_BookingCurrentUpdate GetBooking(string key)
        {
            MasterDA da = new MasterDA();
            var result = da.GetBooking(key);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<M_SlipMessageDetails> GetMstSlipMessageDetail(string key)
        {
            var res = MasterDA.GetMstSlipMessageDetailList(Convert.ToInt32(key));
            return res;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MessageResponse SaveInvoice(T_Invoice_Manual oinfo, List<T_Invoice_Detail> olist, string mode, string route)
        {
            var posno = Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToString();
            MessageResponse msg = new MessageResponse();
            if (mode == "add")
            {
                //
                //oinfo.Booking_date = DateTime.Parse(oinfo.Booking_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                oinfo.Create_date = DateTime.Now;//DateTime.Parse(oinfo.Create_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                //oinfo.Receipt_date = DateTime.Parse(oinfo.Receipt_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                oinfo.Update_date = DateTime.Now;//DateTime.Parse(oinfo.Update_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                //oinfo. = null;
                oinfo.TransactionId = Guid.NewGuid();
                msg = MasterDA.Invoice_Info_insert(oinfo, olist, posno, route);
            }
            else if (mode == "edit")
            {
                //oinfo.Booking_date = DateTime.Parse(oinfo.Booking_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                //oinfo.Create_date = DateTime.Now;//DateTime.Parse(oinfo.Create_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                //oinfo.Receipt_date = DateTime.Parse(oinfo.Receipt_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                oinfo.Update_date = DateTime.Now;//DateTime.Parse(oinfo.Update_date.ToString(), new System.Globalization.CultureInfo("en-GB"));
                msg = MasterDA.Invoice_Info_update(oinfo, olist, posno);
            }
            return msg;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MessageResponse UpdateIsPrintInvoiceManual(string Receipt_no)
        {
            var msg = MasterDA.UpdateIsPrint_Invoice_Manual(Receipt_no);
           
            return msg;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public MessageResponse SaveInvoiceCN(InvoiceCNModel oinfo)
        {
            MessageResponse msg = new MessageResponse();

            var posno = Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToString();
            T_Invoice_CN invCN = new T_Invoice_CN();


            invCN.TransactionId = Guid.Parse(oinfo.TransactionId);
            invCN.INV_No = oinfo.INV_No;
            invCN.PNR_No = oinfo.PNR_No;
            
            invCN.CNReason_Id = Convert.ToInt32(oinfo.CNReason_Id);

            invCN.Create_By = CookiesMenager.EmpID;
            DataTable dtReason = InvoiceCNDA.Get_M_CNReason("AND IsActive ='Y' ");
            if (dtReason.Rows.Count > 0)
            {
                DataRow[] dr = dtReason.Select("CNReason_Id = " + invCN.CNReason_Id);
                if (dr.Length > 0)
                {
                    invCN.CNReason_Code = dr[0]["CNReason_Code"].ToString();
                }
            }
            if (oinfo.mode == "add")
            {
                invCN.CN_ID = Guid.NewGuid();
                invCN.CN_NO = InvoiceDA.GenerateRunning(InvoiceDA.RunningType.CN, posno);

                InvoiceCNDA.InsertInvoiceCN(invCN);
                InvoiceDA.UpdateInvoice_M_ByCN(invCN);
                InvoiceDA.UpdateRunningInvoiceNo("CN", invCN.CN_NO, CookiesMenager.POS_Code, CookiesMenager.EmpID);
                //msg = MasterDA.Invoice_Info_insert(oinfo, olist, posno);
            }
            else if (oinfo.mode == "edit")
            {
                invCN.CN_ID = Guid.Parse(oinfo.CN_ID);
                invCN.CN_NO = oinfo.CN_NO;
                InvoiceCNDA.UpdateInvoiceCN(invCN);
                //msg = MasterDA.Invoice_Info_update(oinfo, olist, posno);
            }
            msg.IsSuccessfull = true;
            msg.Message = "success";
            return msg;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetInvoiceCN(string cn_id)
        {
            string cn_no = string.Empty;
            if (cn_id != string.Empty)
            {
                DataTable dt = InvoiceCNDA.GetInvoiceCN_ByCNID(cn_id);
                cn_no = dt.Rows[0]["CN_NO"].ToString();
                //string cc = JsonConvert.SerializeObject(dt);
            }
            return cn_no;
        }




        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public InvoiceModel GetInvoiceBycode(string key)
        {
            InvoiceModel model = new InvoiceModel();
            model.info = MasterDA.GetInvoiceManual(key);
            model.detail = MasterDA.GetInvoiceDetailList(key);
            return model;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetTime()
        {
            try
            {
                FileInfo f = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]);

                if (Directory.Exists(f.DirectoryName) == false)
                {
                    Directory.CreateDirectory(f.DirectoryName);
                }
                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]) == false)
                {
                    StreamWriter sw = File.CreateText(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]);
                    sw.Write("");
                    sw.Flush();
                    string _item = "";
                    List<string> _list = new List<string>();
                    _list.Add(_item);
                    var serializer = new JavaScriptSerializer();
                    string ss = serializer.Serialize(_list.ToArray());
                    return ss;
                }
                else
                {
                    StreamReader sr = new StreamReader(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]);

                    string _item = sr.ReadToEnd();
                    sr.Close();
                    List<string> _list = new List<string>();
                    _list.Add(_item);
                    var serializer = new JavaScriptSerializer();
                    string ss = serializer.Serialize(_list.ToArray());
                    return ss;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string setimporttime(string time)
        {
            try
            {
                FileInfo f = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]);

                if (Directory.Exists(f.DirectoryName) == false)
                {
                    Directory.CreateDirectory(f.DirectoryName);
                }
                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]) == false)
                {
                    StreamWriter sw = File.CreateText(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]);
                    sw.Write(time);
                    sw.Flush();
                    return "";
                }
                else
                {
                    using (System.IO.FileStream fs = System.IO.File.OpenWrite(System.Configuration.ConfigurationManager.AppSettings["pathtimeschedule"]))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(time);
                        fs.Write(info, 0, info.Length);
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string uploadFile()
        {
            //HttpPostedFileBase myFile = Request.Files[0];
            return "";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<MenuPrivilege> GetMenuPrivilege(string UserGroupCode)
        {
            MasterDA da = new MasterDA();
            var result = da.GetMenuPrivilege(UserGroupCode);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveUserGroupRole(List<MenuPrivilege> menuprivilege, string code)
        {
            try
            {
                MasterDA da = new MasterDA();
                return da.UpdateUserGroupMenu(menuprivilege, code);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<MenuPrivilege> GetMenuPrivilegeUser(string UserGroupCode, string usercode )
        {
            MasterDA da = new MasterDA();
            var result = da.GetMenuPrivilegeUser(UserGroupCode, usercode);
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveUserGroupRoleUser(List<MenuPrivilege> menuprivilege, string code, string usercode)
        {
            try
            {
                MasterDA da = new MasterDA();
                return da.UpdateUserGroupMenuUser(menuprivilege, code, usercode);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPOSByStn(string stn)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            List < M_POS_Machine > _list = da.GetPOSByStn(stn);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMenuPrivilegeAddUser(string UserGroupCode)
        {
            MasterDA da = new MasterDA();
            var result = da.GetMenuPrivilege(UserGroupCode).Where(x=>x.CanView == 1).ToList();
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(result.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveUser(string usercode, string username, string password, string password1, string firstname, string lastname
            ,string group, string station, string pos, string menu, string active)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var Validation_Str = string.Empty;

                var M_Employees = context.M_Employee.Where(x => x.IsActive == "Y");
                if (context.M_Employee.Count(x => x.UserCode == usercode) > 0)
                {
                    Validation_Str += "UserCode: " + usercode + " is already exists\n";
                }
                if (context.M_Employee.Count(x => x.Username == username) > 0)
                {
                    Validation_Str += "Username: " + username + " is already exists";
                }

                var _itemexist = context.M_Employee.Where(x => x.UserCode == usercode && x.IsActive == "Y").FirstOrDefault();

                if (Validation_Str == string.Empty)
                {
                    da.SaveUser(usercode, username, password, password1, firstname, lastname, 
                                group, station, pos, menu, (active.ToUpper() == "TRUE" ? "Y" : "N"), 
                                Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                }

                return Validation_Str;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetUser(string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            M_Employee _item = da.GetUser(int.Parse(id));
            List<M_Employee> _list = new List<M_Employee>();
            _list.Add(_item);
            var serializer = new JavaScriptSerializer();
            string ss = serializer.Serialize(_list.ToArray());
            return ss;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditUser(string usercode, string username, string password, string password1, string firstname, string lastname
            , string group, string station, string pos, string menu, string active, string id)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                int _id = int.Parse(id);
                var _itemexist = context.M_Employee.Where(x => x.UserCode == usercode && x.UserID != _id && x.IsActive == "Y").FirstOrDefault();

                if (_itemexist == null)
                {
                    da.EditUser(int.Parse(id), usercode, username, password, password1, firstname, lastname
            , group, station, pos, menu, (active.ToUpper() == "TRUE" ? "Y" : "N"), Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
                    return "";
                }
                else
                {
                    return usercode + " already exists";
                }
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string EditFlight(string index, string code, string flnum, string from, string to, string fromdate
            , string todate, string timefrom, string timeto)
        {
            List<T_SegmentCurrentUpdate> res_flight = (List<T_SegmentCurrentUpdate>)Session["res_flight"];
            //DAO.MasterDA da = new DAO.MasterDA();
            //using (var context = new POSINVEntities())
            //{
            //    int _id = int.Parse(id);
            //    var _itemexist = context.M_Employee.Where(x => x.UserCode == usercode && x.UserID != _id && x.IsActive == "Y").FirstOrDefault();

            //    if (_itemexist == null)
            //    {
            //        da.EditUser(int.Parse(id), usercode, username, password, password1, firstname, lastname
            //, group, station, pos, menu, (active.ToUpper() == "TRUE" ? "Y" : "N"), Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
            return "";
            //    }
            //    else
            //    {
            //        return usercode + " already exists";
            //    }
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string ORABB(string ABBNo)
        {
            return AbbDA.OR_ABBbyABBNo(ABBNo);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetFlightFeeGroupByID(string strID)
        {
            using (var context = new POSINVEntities())
            {
                var ID = Guid.Parse(strID);
                if (context.M_FlightFeeGroup.Count(x => x.ID == ID) > 0)
                {
                    var serializer = new JavaScriptSerializer();
                    var FlightFeeGroup = context.M_FlightFeeGroup.First(x => x.ID == ID);

                    return serializer.Serialize(new M_FlightFeeGroup()
                    {
                        ID = FlightFeeGroup.ID,
                        NameTH = FlightFeeGroup.NameTH,
                        NameEN = FlightFeeGroup.NameEN,
                        DescriptionTH = FlightFeeGroup.DescriptionTH,
                        DescriptionEN = FlightFeeGroup.DescriptionEN
                    });
                }
            }
            return string.Empty;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string SaveFlightFeeGroup(string NameTH, string NameEN, string DescriptionTH, string DescriptionEN)
        {
            return MasterDA.SaveFlightFeeGroup(null, NameTH.Trim(), NameEN.Trim(), DescriptionTH.Trim(), DescriptionEN.Trim());
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true, ResponseFormat = ResponseFormat.Xml)]
        public string UpdateFlightFeeGroup(string strID, string NameTH, string NameEN, string DescriptionTH, string DescriptionEN)
        {
            DAO.MasterDA da = new DAO.MasterDA();
            using (var context = new POSINVEntities())
            {
                var ID = Guid.Parse(strID);
                return MasterDA.SaveFlightFeeGroup(ID, NameTH.Trim(), NameEN.Trim(), DescriptionTH.Trim(), DescriptionEN.Trim());
            }
        }
    }
}
