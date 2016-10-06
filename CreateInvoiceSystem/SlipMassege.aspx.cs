

using CreateInvoiceSystem.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreateInvoiceSystem
{
    public partial class SlipMassege : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //GetMstPaymentList();
                InitialData();
                RHeader.Attributes.Add("onClick", "javascript:CheckRadio('1')");
                RFooter.Attributes.Add("onClick", "javascript:CheckRadio('2')");
            }
        }
        private void InitialData()
        {
            //this.tbx_date_booking.Text = "xx";
       //     //this.tbx_date_receipt.Text = "xx";
         //   this.tbx_create_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
          //  this.tbx_update_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        //protected void GetMstPaymentList()
        //{
        //    //MasterDA da = new MasterDA();
        //    var res = MasterDA.GetMstPaymentTypeList();
        //    M_PaymentType addobj = new M_PaymentType();
        //    addobj.PaymentType_Code = "";
        //    addobj.PaymentType_Name = "Please selected...";
        //    res.Add(addobj);
        //    res.Sort((x, y) => x.PaymentType_Name.CompareTo(y.PaymentType_Name));
        //    this.ddl_patment_type.DataSource = res;
        //    this.ddl_patment_type.DataTextField = "PaymentType_Name";
        //    this.ddl_patment_type.DataValueField = "PaymentType_Code";
        //    this.ddl_patment_type.DataBind();
        //    this.ddl_patment_type.SelectedValue = "";
        //}
   
    }
}