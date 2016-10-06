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
    public partial class SendMasege : System.Web.UI.Page
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
            sql.CommandText = "SP_Load_M_SendMessage";
            sql.CommandType = CommandType.StoredProcedure;
            string getwhere = "";
            if (tname.Text != "") getwhere += " and (DescMessage Like '%" + tname.Text.Replace("'", "") + "%') ";
            string _sDate, _Edate = "";
            if (lbStartDate.Value != "" && lbExpireDate.Value != "")
            {
                _sDate = (lbStartDate.Value.Substring(6, 4) + "-" + lbStartDate.Value.Substring(3, 2) + "-" + lbStartDate.Value.Substring(0, 2));
                _Edate = (lbExpireDate.Value.Substring(6, 4) + "-" + lbExpireDate.Value.Substring(3, 2) + "-" + lbExpireDate.Value.Substring(0, 2));
                getwhere += " and (CAST(StartDate as Date) >= '" + _sDate + "') and   (CAST(ExpireDate as Date) >= '" + _Edate + "') ";
            }
            else if (lbStartDate.Value != "")
            {
                _sDate = (lbStartDate.Value.Substring(6, 4) + "-" + lbStartDate.Value.Substring(3, 2) + "-" + lbStartDate.Value.Substring(0, 2));
                getwhere += "and (CAST('" + _sDate + "' as Date) between CAST(StartDate as Date) and CAST(ExpireDate as Date)) ";
            }
            else if (lbExpireDate.Value != "")
            {
                _Edate = (lbExpireDate.Value.Substring(6, 4) + "-" + lbExpireDate.Value.Substring(3, 2) + "-" + lbExpireDate.Value.Substring(0, 2));
                getwhere += "and (CAST('" + _Edate + "' as Date) between CAST(StartDate as Date) and CAST(ExpireDate as Date)) ";
            }
            if (txtMessage.Text != "") getwhere += "and (DescMessage like '%" + txtMessage.Text.Replace("'", "") + "%') ";
            lbExpireDate.Value = "";
            lbStartDate.Value = "";

            getwhere += " order By Create_Date desc ";
            sql.Parameters.Add("@getwhere", SqlDbType.NVarChar).Value = getwhere;
            DataTable dst = FunctionAndClass.IexcuteDataByDataTable(sql);
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
            this.txtMessage.Text = "";
            this.startDate.Value = "";
            this.ExpireDate.Value = "";
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
                sql = new SqlCommand();
                sql.CommandText = "SP_Del_M_SendMessage";
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