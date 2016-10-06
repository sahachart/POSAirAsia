using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreateInvoiceSystem.Model;
using System.Globalization;

namespace CreateInvoiceSystem
{
    public partial class batchincomplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["data"] = GetData();
                BindGrid();
            }
        }

        protected List<Model.LogABB> GetData()
        {
            DAO.MasterDA da = new DAO.MasterDA();
            var LogABB = da.GetLogABB(this.tcode.Text);

            DateTime? BatchDate = null;
            if (dtp_BatchDate.Text != string.Empty)
            {
                try
                {
                    BatchDate = DateTime.ParseExact(dtp_BatchDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { dtp_BatchDate.Text = string.Empty; }

            }

            if (BatchDate != null)
            {
                LogABB = LogABB.Where(x => x.Create_Date.Date == BatchDate.Value.Date).ToList();
            }
            return LogABB;
        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["data"] as List<LogABB>;
            GridView1.DataBind();
            this.Label1.Visible = !(this.GridView1.Rows.Count > 0);
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.tcode.Text = "";
            dtp_BatchDate.Text = string.Empty;
            ViewState["data"] = GetData();
            BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ViewState["data"] = GetData();
            BindGrid();
        }
    }
}