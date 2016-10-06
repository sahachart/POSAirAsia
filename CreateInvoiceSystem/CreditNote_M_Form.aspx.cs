using CreateInvoiceSystem.DAO;
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
    public partial class CreditNote_M_Form : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DoPreview();

            }
        }

        private void DoPreview()
        {

            //string CN_No = "CN0716-00001";

            if (Request.QueryString["inv_no"] != null)
            {
                string POS_CODE = Context.Request.Cookies["authenuser"].Values["POS_CODE"];

                string inv_no =  Request.QueryString["inv_no"].ToString();



                DataReports ds = InvoiceCNDA.GetInvoiceCN_M_Report(inv_no);

                DataReports dsDT = InvoiceCNDA.GetInvoiceCNDT_M_Report(inv_no);



                //DataReports.DT_CreditNoteDT_FormRow dr = ds.DT_CreditNoteDT_Form.NewDT_CreditNoteDT_FormRow();
                //dr.Seq = 1;
                //dr.Detail = flight;
                //dr.Qty = 1;
                //dr.Price = ds.DT_CreditNote_Form[0].SubTotal;
                //dr.Amount = ds.DT_CreditNote_Form[0].SubTotal;
                //ds.DT_CreditNoteDT_Form.AddDT_CreditNoteDT_FormRow(dr);

                ReportDataSource reportDataSource1 = new ReportDataSource();
                reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DT_CreditNote_Form";
                reportDataSource1.Value = ds.DT_CreditNote_Form;

                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "DT_CreditNoteDT_Form";
                reportDataSource2.Value = dsDT.DT_CreditNoteDT_Form;

                var Header = MasterDA.GetMessageList(MasterDA.SlipType.CN, true, POS_CODE);
                var HeaderDesc = String.Join("<br/><br/>", Header.MessageModels.Select(x => x.Descriptions));

                var Footer = MasterDA.GetMessageList(MasterDA.SlipType.CN, false, POS_CODE);
                var FooterDesc = String.Join("<br/><br/>", Footer.MessageModels.Select(x => x.Descriptions));

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_CreditNote_M.rdlc");
                ReportViewer1.AsyncRendering = false;
                ReportViewer1.SizeToReportContent = true;

                var p = new ReportParameter[] { 
                new ReportParameter("Header", HeaderDesc),
                new ReportParameter("Footer", FooterDesc)
                    //new ReportParameter("REP",str.objAbb1.REP == null ? "0.00" : str.objAbb1.REP),
                };
                ReportViewer1.LocalReport.SetParameters(p);
                ReportViewer1.LocalReport.DataSources.Clear();

                
                ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                ReportViewer1.LocalReport.DataSources.Add(reportDataSource2);
                ReportViewer1.LocalReport.Refresh();
            }
        }

    }
}