

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
    public partial class frmSlipMessage : System.Web.UI.Page
    {
        SqlCommand sql;
        DataTable dt;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               BindGrid();
                RHeader.Attributes.Add("onClick", "javascript:CheckRadio('1')");
                RFooter.Attributes.Add("onClick", "javascript:CheckRadio('2')");
            }
        }
     
        private DataTable GetData()
        {
            sql = new SqlCommand();
            sql.CommandText = "SP_Load_M_SlipMessage";
            sql.CommandType = CommandType.StoredProcedure;
            string getwhere = "";
            if (tcode.Text != "") getwhere += " and (SlipMessage_Code Like '%" + tcode.Text.Replace("'", "") + "%') ";
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

        protected void btnload_Click(object sender, EventArgs e)  
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
            BindGrid();
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
                DAO.MasterDA da = new DAO.MasterDA();
                var Output = da.DeleteMessageSlip(Convert.ToInt32(bl.AlternateText));
                if (Output != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + Output + "');", true);
                }
                else
                {
                    BindGrid();
                }
                //sql = new SqlCommand();
                //sql.CommandText = "SP_del_M_SlipMessage";
                //sql.CommandType = CommandType.StoredProcedure;
                //sql.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(bl.AlternateText);
                //if (FunctionAndClass.IexcuteData(sql))
                //{
                //    BindGrid();
                //}

            }
        }

    }
}