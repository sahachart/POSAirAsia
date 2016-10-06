using CreateInvoiceSystem.Reports;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class InvoicePrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataReports ds = new DataReports();
            DataTable dt = new DataTable();
            ReportDataSource dsRpt = new ReportDataSource(ds.DT_FULL_TAX.TableName, ds);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_Full_Tax.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(dsRpt);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}