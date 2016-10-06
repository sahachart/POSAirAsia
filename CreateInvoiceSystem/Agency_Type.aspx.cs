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
    public partial class Agency_Type : System.Web.UI.Page
    {

        public static string CreateConfirmation(String action, String item)
        {
            return String.Format(@"return doConfirm('{0}', '{1}');", action, item);
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }
        private SqlCommand sqlcmd;
        private DataTable dt;
        private DataTable GetData()
        {
            sqlcmd = new SqlCommand();
            sqlcmd.CommandText = "SP_Load_M_AgencyType";
            sqlcmd.CommandType = CommandType.StoredProcedure;
            string getwhere = "";
            if (tcode.Text != "") getwhere += " and (Agent_type_code Like '%" + tcode.Text.Replace("'", "") + "%') ";
            if (tname.Text != "") getwhere += " and (Agent_type_Name Like '%" + tname.Text.Replace("'", "") + "%') ";
            sqlcmd.Parameters.Add("@getwhere", SqlDbType.NVarChar).Value = getwhere;
            dt = FunctionAndClass.IexcuteDataByDataTable(sqlcmd);
            return dt;
        }
        private void BindGrid()
        {
            GridView1.DataSource = GetData();//ViewState["data"]as List<M_Agency>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string index = e.Keys[1].ToString();
            DAO.MasterDA da = new DAO.MasterDA();
            var Output = da.DeleteAgenType(index.ToString());
            if (Output != string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + Output + "');", true);
            }
            else
            {
                BindGrid();
            }

        }
        protected void btndel_click(object sender, ImageClickEventArgs e)
        {
            ImageButton index = (ImageButton)sender;
            if (index != null)
            {
                DAO.MasterDA da = new DAO.MasterDA();
                var Output = da.DeleteAgenType(index.AlternateText.ToString());
                if (Output != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('" + Output + "');", true);
                }
                else
                {
                    BindGrid();
                }
            }
        }

        //protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string item = e.Row.Cells[1].Text;
        //        foreach (ImageButton button in e.Row.Cells[6].Controls.OfType<ImageButton>())
        //        {
        //            if (button.CommandName == "Delete")
        //            {
        //                button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
        //            }
        //        }
        //    }
        //}

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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}