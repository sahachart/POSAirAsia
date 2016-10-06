using CreateInvoiceSystem.DAO;
using CreateInvoiceSystem.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem.Reports.Views
{
    public partial class ReportByMachine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getStation();
                initial_control();
            }
        }
        private void initial_control()
        {
            string key = this.Request["id"].ToString();

            if (key.Equals("1"))
            {
                this.Label_Menu.Text = "รายงานยอดขาย ตามเครื่องจุดขาย";
            }
            else if (key.Equals("2"))
            {
                this.Label_Menu.Text = "รายงานยอดขาย จุดขาย ตามภาษีขาย";
            }
            else if (key.Equals("3"))
            {
                this.Label_Menu.Text = "รายงานสรุปใบกำกับภาษี";
            }
            else if (key.Equals("4"))
            {
                this.Label_Menu.Text = "รายงานใบกำกับภาษีอย่างย่อ";
            }
            else if (key.Equals("5"))
            {
                this.Label_Menu.Text = "รายงานใบกำกับภาษีอย่างย่อ/แก้ไข";
            }
            else if (key.Equals("6"))
            {
                this.Label_Menu.Text = "รายงานตรวจสอบใบกำกับภาษีอย่างย่อ";
            }
            else if (key.Equals("7"))
            {
                this.Label_Menu.Text = "รายงานยอดขาย ตามวันที่ - สาขา";
            }
            else if (key.Equals("8"))
            {
                this.Label_Menu.Text = "รายงานใบลดหนี้";
            }
            else if (key.Equals("9"))
            {
                this.Label_Menu.Text = "รายงานการแก้ไข Invoice";
            }
            if (key.Equals("1") || key.Equals("2") || key.Equals("3") || key.Equals("4"))
            {
                this.lbl_st_from.Visible = true;
                this.lbl_st_from.Text = "Station Code :";// "From Station Code :";
                this.ddl_station_from.Visible = true;

                this.lbl_st_to.Visible = false;
                this.ddl_station_to.Visible = false;

                this.lbl_date_from.Visible = true;
                this.txb_date_from.Visible = true;

                this.lbl_date_to.Visible = true;
                this.txb_date_to.Visible = true;

                this.lbl_flight_type.Visible = false;
                this.ddl_flight.Visible = false;

            }
            else if (key.Equals("5") || key.Equals("6") ||
                key.Equals("7") || key.Equals("8") || key.Equals("9"))
            {
                this.lbl_st_from.Visible = true;
                this.lbl_st_from.Text = "Station Code :";
                this.ddl_station_from.Visible = true;

                this.lbl_st_to.Visible = false;
                this.ddl_station_to.Visible = false;

                this.lbl_date_from.Visible = true;
                this.txb_date_from.Visible = true;

                this.lbl_date_to.Visible = true;
                this.txb_date_to.Visible = true;

                this.lbl_flight_type.Visible = false;
                this.ddl_flight.Visible = false;

            }
        }
        protected void btn_run_Click(object sender, EventArgs e)
        {
             string key = this.Request["id"].ToString();
             
             preview_report(key);

             //ScriptManager.RegisterStartupScript(this, this.GetType(), "xxx", "ShowButtonPrint();", true);
        }
        protected void getStation()
        {
            //MasterDA da = new MasterDA();
            var res = MasterDA.GetStationListForReport();
            res.Sort((x, y) => x.Station_Code.CompareTo(y.Station_Code));
            M_Station addobj = new M_Station();
            addobj.Station_Code = "ALL";
            addobj.Station_Name = "ALL";
            res.Insert(0, addobj);
            this.ddl_station_from.DataSource = res;
            this.ddl_station_from.DataTextField = "Station_Name";
            this.ddl_station_from.DataValueField = "Station_Code";
            this.ddl_station_from.DataBind();

            this.ddl_station_to.DataSource = res;
            this.ddl_station_to.DataTextField = "Station_Name";
            this.ddl_station_to.DataValueField = "Station_Code";
            this.ddl_station_to.DataBind();

            M_FlightType addobjfl = new M_FlightType();
            addobjfl.Flight_Type_Code = "";
            addobjfl.Flight_Type_Name = "Please selected...";
            var res_fl = MasterDA.GetFlightTypeList();
            res_fl.Add(addobjfl);
            res_fl.Sort((x, y) => x.Flight_Type_Code.CompareTo(y.Flight_Type_Code));
            this.ddl_flight.DataSource = res_fl;
            this.ddl_flight.DataTextField = "Flight_Type_Name";
            this.ddl_flight.DataValueField = "Flight_Type_Code";
            this.ddl_flight.DataBind();

        }
        private void preview_report(string id)
        {
            string dtf = Request.Form[this.txb_date_from.UniqueID];
            string dtt = Request.Form[this.txb_date_to.UniqueID];
            string _StationCodeFrom = this.ddl_station_from.SelectedValue.ToString();
            string _StationCodeTo = this.ddl_station_to.SelectedValue.ToString();
            if (dtf == "" || dtt == "" || _StationCodeFrom == "")//|| _StationCodeTo == ""
            {
                return;
            }

            MasterDA dt = new MasterDA();

            var _SqlCommand = new SqlCommand() { CommandType = CommandType.StoredProcedure };
            var DataSetName = string.Empty;
            var DataReportTableName = string.Empty;

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //ReportViewer1.AsyncRendering = false;
            //ReportViewer1.SizeToReportContent = true;

            if (id.Equals("1"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report1_Machine_POS.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_1_FullTax";
                DataSetName = "DS_MAC_POS";
                DataReportTableName = "DT_MachinePOS";
            }
            else if (id.Equals("2"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report2_SaleVat.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_2_SaleVat";
                DataSetName = "DS_SALEVAT";
                DataReportTableName = "DT_SaleVat";
            }
            else if (id.Equals("3"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report3_FullTax.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_3_FullTax";
                DataSetName = "DS_FullTax";
                DataReportTableName = "DT_FullTax";
            }
            if (id.Equals("4"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report4_TaxInvoiceABB.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_4_TaxInvoiceABB";
                DataSetName = "DS_ABB";
                DataReportTableName = "DT_TaxInvoiceABB";
            }
            else if (id.Equals("5"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report5_TaxInvoiceABB_Edit.rdlc");
                _SqlCommand.CommandText = "";
                DataSetName = "DS_ABB";
                DataReportTableName = "DT_TaxInvoiceABB";
            }
            else if (id.Equals("6"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report6_AuditABB.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_6_AuditABB";
                DataSetName = "DS_AUDIT_ABB";
                DataReportTableName = "DT_Audit_FullTax";
            }
            else if (id.Equals("7"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report7_SaleDaily.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_7_SaleDaily";
                DataSetName = "DS_SALE_DAILY";
                DataReportTableName = "DT_SaleDaily";
            }
            else if (id.Equals("8"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report8_CreditNote.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_8_Invoice_CN";
                DataSetName = "DS_CREDIT_NOTE";
                DataReportTableName = "DT_CreditNote";
            }
            else if (id.Equals("9"))
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report9_EditTaxInvoice.rdlc");
                _SqlCommand.CommandText = "SP_GetReportData_9_EditTaxInvoice";
                DataSetName = "DS_FullTaxEdit";
                DataReportTableName = "DT_FullTax";
            }
            
            CriteriaMachinePOSModel cri = new CriteriaMachinePOSModel();
            if (dtf != string.Empty)
            {
                cri.date_from = DateTime.Parse(dtf, new System.Globalization.CultureInfo("en-GB"));
            }
            if (dtt != string.Empty)
            {
                cri.date_to = DateTime.Parse(dtt, new System.Globalization.CultureInfo("en-GB"));
            }

            //var StationFrom = MasterDA.GetStationByCode(_StationCodeFrom);
            //if (StationFrom != null)
            //{
            //    cri.station_from = StationFrom.Station_Code + " - " + StationFrom.Station_Name;
            //}

            //var StationTo = MasterDA.GetStationByCode(_StationCodeTo);
            //if (StationTo != null)
            //{
            //    cri.station_to = StationTo.Station_Code + " - " + StationTo.Station_Name;
            //}

            _SqlCommand.Parameters.Add("@Station1", SqlDbType.VarChar).Value = _StationCodeFrom;
            _SqlCommand.Parameters.Add("@Station2", SqlDbType.VarChar).Value = _StationCodeTo;
            _SqlCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = cri.date_from;
            _SqlCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = cri.date_to.Value.AddDays(1).AddSeconds(-1);

            

            DataReports dsRpt = dt.GetRptMachinePosDs(_SqlCommand, DataReportTableName);
            if (dsRpt != null)
            {
                var p = new ReportParameter[] { 
                    new ReportParameter("date_from",dtf =="" ? "-" : dtf),
                    new ReportParameter("date_to",dtt =="" ? "-" : dtt),
                    new ReportParameter("station_from", "-"),
                    new ReportParameter("station_to", "-"),
                };
                ReportViewer1.LocalReport.SetParameters(p);
                ReportDataSource datasource = new ReportDataSource(DataSetName, dsRpt.Tables[DataReportTableName]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);

            }
        }

        private void preview_report_4_9(string id)
        {
            //string dtf = Request.Form[this.txb_date_from.UniqueID];
            //string dtt = Request.Form[this.txb_date_to.UniqueID];
            //string stf = this.ddl_station_from.SelectedValue.ToString();
            //string stt = this.ddl_station_to.SelectedValue.ToString();
            //string flight = this.ddl_flight.SelectedValue.ToString();
            //if (dtf == "" || dtt == "" || stf == "" || stt == "")
            //{
            //    return;
            //}

            //MasterDA dt = new MasterDA();

            //ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //if (id.Equals("4"))
            //{
            //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report4_TaxInvoiceABB.rdlc");
            //}
            //else if (id.Equals("5"))
            //{
            //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report5_TaxInvoiceABB_Edit.rdlc");
            //}
            //else if (id.Equals("6"))
            //{
            //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report6_AuditABB.rdlc");
            //}
            //else if (id.Equals("7"))
            //{
            //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report7_SaleDaily.rdlc");
            //}
            //else if (id.Equals("8"))
            //{
            //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report8_CreditNote.rdlc");
            //}
            //else if (id.Equals("9"))
            //{
            //    //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Forms/Report3_FullTax.rdlc");
            //}
            //var obj = new DataReports();
            //CriteriaMachinePOSModel cri = new CriteriaMachinePOSModel();
            //if (dtf != string.Empty)
            //{
            //    cri.date_from = DateTime.Parse(dtf, new System.Globalization.CultureInfo("en-GB"));
            //}
            //if (dtf != string.Empty)
            //{
            //    cri.date_to = DateTime.Parse(dtt, new System.Globalization.CultureInfo("en-GB"));
            //}
            //cri.station_from = stf;
            //cri.station_to = stt;

            //DataReports dsRpt = dt.GetRptMachinePosDs( cri);
            //if (dsRpt != null)
            //{
            //    var p = new ReportParameter[] { 
            //        new ReportParameter("date_from",dtf =="" ? "-" : dtf),
            //        new ReportParameter("date_to",dtt =="" ? "-" : dtt),
            //        new ReportParameter("station_from",stf  =="" ? "-" : stf),
            //        new ReportParameter("flight_type",flight  =="" ? "-" : flight),
            //    };
            //    ReportViewer1.LocalReport.SetParameters(p);
            //    ReportDataSource datasource = new ReportDataSource();
            //    if (id.Equals("4"))
            //    {
            //        datasource = new ReportDataSource("DS_MAC_POS", dsRpt.Tables["DT_MachinePOS"]);
            //    }
            //    else if (id.Equals("5"))
            //    {
            //        datasource = new ReportDataSource("DS_MAC_POS", dsRpt.Tables["DT_MachinePOS"]);
            //    }
            //    else if (id.Equals("6"))
            //    {
            //        datasource = new ReportDataSource("DS_MAC_POS", dsRpt.Tables["DT_MachinePOS"]);
            //    }
            //    else if (id.Equals("7"))
            //    {
            //        datasource = new ReportDataSource("DS_MAC_POS", dsRpt.Tables["DT_MachinePOS"]);
            //    }
            //    else if (id.Equals("8"))
            //    {
            //        datasource = new ReportDataSource("DS_MAC_POS", dsRpt.Tables["DT_MachinePOS"]);
            //    }
            //    else if (id.Equals("9"))
            //    {
            //        datasource = new ReportDataSource("DS_MAC_POS", dsRpt.Tables["DT_MachinePOS"]);
            //    }
            //    ReportViewer1.LocalReport.DataSources.Clear();
            //    ReportViewer1.LocalReport.DataSources.Add(datasource);
            //}
        }
    }
}