using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace CreateInvoiceSystem
{
    public partial class Agency : System.Web.UI.Page
    {
        SqlCommand sql;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // ViewState["data"] = GetData();
                 LoadAgenType();
                BindGrid();

            }
        }
        private void LoadAgenType() {
            sql = new SqlCommand();
            dt = new DataTable();
            sql.CommandText = "SP_Load_M_AgencyType";
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.Add("@getwhere", SqlDbType.NVarChar).Value = "";
            dt = FunctionAndClass.IexcuteDataByDataTable(sql);
            cmbList.Items.Add(new ListItem("--Choose--", "0"));
            DropDownList2.Items.Add(new ListItem("--Choose--", "0"));
            if (dt.Rows.Count > 0) {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbList.Items.Add(new ListItem(dt.Rows[i]["Agent_type_Name"].ToString(), dt.Rows[i]["Agent_type_code"].ToString()));
                    DropDownList2.Items.Add(new ListItem(dt.Rows[i]["Agent_type_Name"].ToString(), dt.Rows[i]["Agent_type_code"].ToString()));
                }
            }
        }
        //protected List<M_Agency> GetData()
        //{
        //    using (var context = new POSINVEntities())
        //    {
        //        var items = context.M_Agency.Where(x => (x.IsActive == "Y" && x.Agency_Code.Contains(this.tcode.Text) && x.Agency_Name.Contains(this.tname.Text))).ToList();
        //        return items;
        //    }
        //}
        private DataTable GetData()
        {
            sql = new SqlCommand();
            sql.CommandText = "SP_Load_M_Agency";
            sql.CommandType = CommandType.StoredProcedure;
            string getwhere = "";
            if (tcode.Text != "") getwhere += " and (M_Agency.Agency_Code Like '%" + tcode.Text.Replace("'", "") + "%') ";
            if (tname.Text != "") getwhere += " and (M_Agency.Agency_Name Like '%" + tname.Text.Replace("'", "") + "%') ";
            if (DropDownList2.SelectedIndex.ToString() != "0") getwhere += " and (M_Agency.Agency_Type_Code = '" + DropDownList2.SelectedValue + "') ";
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

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.Keys[0]);
            DAO.MasterDA da = new DAO.MasterDA();
            da.DeleteAgency(index, Context.Request.Cookies["authenuser"].Values["Emp_ID"]);
        //    ViewState["data"] = GetData();
            BindGrid();
        }

        //protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        M_Agency dataitem = (M_Agency)e.Row.DataItem;
        //        if (dataitem.GenInvoice.ToString() == "Y")
        //        {
        //            CheckBox chk = (CheckBox)e.Row.Cells[3].FindControl("chkRow");
        //            chk.Checked = true;
        //        }
        //        string item = e.Row.Cells[1].Text;
        //        foreach (ImageButton button in e.Row.Cells[4].Controls.OfType<ImageButton>())
        //        {
        //            if (button.CommandName == "Delete")
        //            {
        //                button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
        //            }
        //        }
        //    }
        //    }
        //    catch { }
        //}
        public static bool CheckINV(string INV) {
            if (INV.ToUpper() == "Y") return true;
            return false;

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
           // ViewState["data"] = GetData();
            BindGrid();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
        //    ViewState["data"] = GetData();
            BindGrid();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.DropDownList2.SelectedIndex = 0;
            this.tcode.Text = "";
            this.tname.Text = "";
        //    ViewState["data"] = GetData();
            BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
          //  ViewState["data"] = GetData();
            BindGrid();
        }

        public static string CreateConfirmation(String action, String item)
        {
            return String.Format(@"return confirm('{0}');", action + item);
        }

         protected void btndel_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton bl = (ImageButton)sender;
            if (bl != null) {
                sql = new SqlCommand();
                sql.CommandText = "SP_Del_M_Agency";
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(bl.AlternateText);
                if (FunctionAndClass.IexcuteData(sql)) {
                    BindGrid();
                }

            }
        }


    }
}