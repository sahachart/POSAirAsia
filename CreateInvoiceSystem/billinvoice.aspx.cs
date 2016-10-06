using CreateInvoiceSystem.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class billinvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getStation();
                GetMstPaymentList();
                InitialData();
            }
        }
        private void InitialData()
        {
            //this.tbx_date_booking.Text = "xx";
            //this.tbx_date_receipt.Text = "xx";
            this.tbx_create_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.tbx_update_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.tbx_pos_no.Text = Context.Request.Cookies["authenuser"].Values["POS_CODE"].ToString();
            this.tbx_cashier_no.Text = Context.Request.Cookies["authenuser"].Values["Emp_ID"].ToString();
            this.tbx_create_by.Text = Context.Request.Cookies["authenuser"].Values["fname"].ToUpper() + " " + Context.Request.Cookies["authenuser"].Values["lastname"].ToUpper();
            this.tbx_update_by.Text = Context.Request.Cookies["authenuser"].Values["fname"].ToUpper() + " " + Context.Request.Cookies["authenuser"].Values["lastname"].ToUpper();

        }
        protected void GetMstPaymentList()
        {
            //MasterDA da = new MasterDA();
            //var res = MasterDA.GetMstPaymentTypeList();
            ////GetFlightFee
            //M_PaymentType addobj = new M_PaymentType();
            //addobj.PaymentType_Code = "";
            //addobj.PaymentType_Name = "Please selected...";
            //res.Add(addobj);
            //res.Sort((x, y) => x.PaymentType_Name.CompareTo(y.PaymentType_Name));
            //this.ddl_patment_type.DataSource = res;
            //this.ddl_patment_type.DataTextField = "PaymentType_Name";
            //this.ddl_patment_type.DataValueField = "PaymentType_Code";
            //this.ddl_patment_type.DataBind();
            //this.ddl_patment_type.SelectedValue = "";
            var res = MasterDA.GetFlightFee();
            
            res.Sort((x, y) => x.Flight_Fee_Name.CompareTo(y.Flight_Fee_Name));
            M_FlightFee addobj = new M_FlightFee();
            addobj.Flight_Fee_Code = "";
            addobj.Flight_Fee_Name = "Please selected...";
            res.Insert(0, addobj);

            this.ddl_patment_type.DataSource = res;
            this.ddl_patment_type.DataTextField = "Flight_Fee_Name";
            this.ddl_patment_type.DataValueField = "Flight_Fee_Code";
            this.ddl_patment_type.DataBind();
            this.ddl_patment_type.SelectedValue = "";
        }
        protected void getStation()
        {
            //MasterDA da = new MasterDA();
            var res = MasterDA.GetStationList();
            M_Station addobj = new M_Station();
            addobj.Station_Code = "";
            addobj.Station_Name = "Select the station...";
            res.Add(addobj);
            res.Sort((x, y) => x.Station_Code.CompareTo(y.Station_Code));
            this.ddl_station.DataSource = res;
            this.ddl_station.DataTextField = "Station_Name";
            this.ddl_station.DataValueField = "Station_Code";
            this.ddl_station.DataBind();
            this.ddl_station.SelectedValue = "";

            var msg = MasterDA.GetMstSlipMessageList();
            M_SlipMessage obj_msg = new M_SlipMessage();
            obj_msg.SlipMessage_Code = "Select the slip message...";
            obj_msg.SlipMessage_Id = 0;
            msg.Add(obj_msg);
            msg.Sort((x, y) => x.SlipMessage_Id.CompareTo(y.SlipMessage_Id));
            this.ddl_slip.DataSource = msg;
            this.ddl_slip.DataTextField = "SlipMessage_Code";
            this.ddl_slip.DataValueField = "SlipMessage_Id";
            this.ddl_slip.DataBind();
            this.ddl_slip.SelectedValue = "0";

            var vat = MasterDA.GetMstVatList();
            M_VAT obj_vat = new M_VAT();
            obj_vat.VAT_NAME = "Select the slip message...";
            obj_vat.VAT_CODE = "";
            vat.Add(obj_vat);
            vat.Sort((x, y) => x.VAT_CODE.CompareTo(y.VAT_CODE));
            this.ddl_vat.DataSource = vat;
            this.ddl_vat.DataTextField = "VAT_VALUE";
            this.ddl_vat.DataValueField = "VAT_CODE";
            this.ddl_vat.DataBind();
            this.ddl_vat.SelectedValue = "EXTVAT";
        }
    }
}