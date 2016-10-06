
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CreateInvoiceSystem.DAO;


namespace CreateInvoiceSystem
{
    public partial class UserGroup : System.Web.UI.Page
    {
        SqlCommand sql;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();

            }
        }
        private DataTable GetData()
        {
            sql = new SqlCommand();
            sql.CommandText = "SP_Load_M_UserGroup";
            sql.CommandType = CommandType.StoredProcedure;
            string getwhere = "";
            if (tcode.Text != "") getwhere += " and (UserGroupCode Like '%" + tcode.Text.Replace("'", "") + "%') ";
            if (tname.Text != "") getwhere += " and (UserGroupName Like '%" + tname.Text.Replace("'", "") + "%') ";
            sql.Parameters.Add("@getwhere", SqlDbType.NVarChar).Value = getwhere;
            dt = FunctionAndClass.IexcuteDataByDataTable(sql);
            return dt;
        }
        protected void BindGrid()
        {
            GridView1.DataSource = GetData();
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }


        public static bool CheckINV(string INV)
        {
            if (INV.ToUpper() == "Y") return true;
            return false;

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            BindGrid();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.tcode.Text = "";
            this.tname.Text = "";
            BindGrid();
        }
        public static bool IsVisible(string ID,string _type) {
            // 1 ถ้า ID > 0 ซ่อน (ปุ่มลบ)
            int i = Convert.ToInt32(ID);
            if (_type == "1") {
                if (i > 0)
                {
                    return false;
                }
                else {
                    return true;
                }
            }
            else
            {
                // 2 ถ้า ID > 0 แสดง (ปุ่มลอค)
                if (i > 0)
                {
                    return true;
                }
                else {
                    return false;
                }

            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        public static string CreateConfirmation(String action, String item)
        {
            return String.Format(@"return confirm('{0}');", action + item);
        }

        protected void btndel_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton bl = (ImageButton)sender;
            if (bl != null)
            {
                var GroupId = int.Parse(bl.AlternateText);
                using (var context = new POSINVEntities())
                {
                    var UserGroup = context.M_UserGroup.FirstOrDefault(x => x.UserGroupId == GroupId);
                    if (UserGroup != null)
                    {
                        if (context.M_Employee.Count(x => x.UserGroup == UserGroup.UserGroupCode) > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + new MasterDA().strErrorDataUsed + "');", true);
                        }
                        else
                        {
                            sql = new SqlCommand();
                            sql.CommandText = "SP_Del_M_UserGroup";
                            sql.CommandType = CommandType.StoredProcedure;
                            sql.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(bl.AlternateText);
                            if (FunctionAndClass.IexcuteData(sql))
                            {
                                BindGrid();
                            }
                        }
                    }
                }
            }
        }


    }
}