using CreateInvoiceSystem.DAO;
using CreateInvoiceSystem.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreateInvoiceSystem.DAO;
namespace CreateInvoiceSystem
{
    public partial class billinvoice_form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.Request["id"] != null)
                {
                    preview_form();
                }
            }
        }

        private void preview_form()
        {
            NumberToEnglish num = new NumberToEnglish();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            
            var res = GetData();
            form_invoicemodel model = new form_invoicemodel();
            List<InvoiceHeaderModel> header = new List<InvoiceHeaderModel>();
            InvoiceHeaderModel obj_h = new InvoiceHeaderModel();
            DeepCopyUtility.CopyObjectData(res.info, obj_h);
            obj_h.MoneyToWord = num.changeCurrencyToWords(Convert.ToDouble(obj_h.Net_balance));
            obj_h.Route = obj_h.Route.Replace("|", "<br/>");
            header.Add(obj_h);
            List<InvoiceDetailModel> detail = new List<InvoiceDetailModel>();
            InvoiceDetailModel obj_d = new InvoiceDetailModel();
            //DeepCopyUtility.CopyObjectData(res.detail, detail);
            if (res.detail != null)
            {
                foreach (var item in res.detail)
                {
                    obj_d = new InvoiceDetailModel();
                    DeepCopyUtility.CopyObjectData(item, obj_d);
                    detail.Add(obj_d);
                }
            }
            if (res.info.Receipt_type.Trim() =="R")
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_Receipt_Tax_Invoice.rdlc");
            }
            else
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/form_Receipt_Tax_Invoice_Copy.rdlc");
            }
            var p = new ReportParameter[] { 
                    //new ReportParameter("REP",str.objAbb1.REP == null ? "0.00" : str.objAbb1.REP),
                };
            //ReportViewer1.LocalReport.SetParameters(p);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource1 = new ReportDataSource();
            ReportDataSource reportDataSource2 = new ReportDataSource();

            if (header != null)
            {
                reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "DS_RT_INV_HEADER";
                reportDataSource1.Value = header;
            }
            if (detail != null)
            {
                reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "DS_RT_INV_DETAIL";
                reportDataSource2.Value = detail;
            }
            
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            ReportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            ReportViewer1.LocalReport.Refresh();
        }

        protected InvoiceModel GetData()
        {
            //InvoiceModel val = new InvoiceModel();
            string key = this.Request["id"].ToString();
            InvoiceModel model = new InvoiceModel();
            model.info = MasterDA.GetInvoiceManual(key);
            model.detail = MasterDA.GetInvoiceDetailList(key);
            return model;
        }
    }
}