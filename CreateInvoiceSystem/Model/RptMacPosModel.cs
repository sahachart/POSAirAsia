using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem.Model
{
    public class RptMacPosModel
    {
        public string PAYMENT_DATE { get; set; }
        public string MAC_NO { get; set; }
        public string REGISTER_NO { get; set; }
        public string BILL_COUNT { get; set; }
        public string AMOUNT_B0 { get; set; }
        public string AMOUNT_B7 { get; set; }
        public string VAT { get; set; }
        public string FEE { get; set; }
        public string AMT_PAY_INSTEAD { get; set; }
        public string AMT_DISCOUNT { get; set; }
        public string AMT_NETBALANCE { get; set; }

    }

    public class ABB_1_Model
    {
        public string desc_type { get; set; }
        public string desc_val { get; set; }
        public string PNR_No { get; set; }
        public string PAXCount { get; set; }
        public string TaxInvoiceNo { get; set; }
        public string passenger_name { get; set; }
        public string VAT { get; set; }
        public string AIRPORTTAX { get; set; }
        public string TOTAL { get; set; }
        public string booking_date { get; set; }
        public string print_no { get; set; }
        public string print_reason { get; set; }
        public string print_date { get; set; }
        public string print_time { get; set; }
        public string pid { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string pos { get; set; }
        public string REP { get; set; }
        public string header { get; set; }
        public string footer { get; set; }

    }
    public class ABB_5_Model
    {
        public string passenger_name { get; set; }
    }
    public class ABB_2_Model
    {
        public string pay_desc { get; set; }
        public string pay_amt { get; set; }
        public string flight_desc { get; set; }
    }

    public class ABB_3_Model
    {
        public string tax_type { get; set; }
        public string tax_amt { get; set; }
        public string pay_type { get; set; }
        public string pay_amt { get; set; }
    }
    public class ListValueModel
    {
        public List<ABB_2_Model> oList_2 { get; set; }
        public List<ABB_3_Model> oList_3 { get; set; }
        public ABB_1_Model objAbb1 { get; set; }
        public List<ABB_2_Model> servicelist { get; set; }
    }

    public class form_invoicemodel
    {
        public InvoiceHeaderModel objheader { get; set; }
        public List<InvoiceDetailModel> objdetail { get; set; }

    }

    public class InvoiceHeaderModel
    {
        public string Receipt_no { get; set; }
        public string Receipt_type { get; set; }
        public Nullable<System.DateTime> Receipt_date { get; set; }
        public string Station_Code { get; set; }
        public string SlipMessage_Code { get; set; }
        public string Branch_name { get; set; }
        public string Branch_address1 { get; set; }
        public string Branch_address2 { get; set; }
        public string Branch_address3 { get; set; }
        public string Branch_Tax_ID { get; set; }
        public string POS_Code { get; set; }
        public string Cashier_no { get; set; }
        public string Booking_name { get; set; }
        public string Booking_no { get; set; }
        public Nullable<System.DateTime> Booking_date { get; set; }
        public string Customer_address1 { get; set; }
        public string Customer_address2 { get; set; }
        public string Customer_address3 { get; set; }
        public string Customer_Tax_ID { get; set; }
        public string Passenger_name { get; set; }
        public string Route { get; set; }
        public string Remark { get; set; }
        public Nullable<System.Decimal> Amount { get; set; }
        public Nullable<System.Decimal> Vat { get; set; }
        public Nullable<System.Decimal> Net_balance { get; set; }
        public string Payment_type { get; set; }
        public string Card_no { get; set; }
        public string Bank { get; set; }
        public string Document_State { get; set; }
        public string MoneyToWord { get; set; }
        public string VAT_CODE { get; set; }

        public string SlipHeader { get; set; }
    }

    public class InvoiceDetailModel
    {
        public string Receipt_no { get; set; }
        public string Flight_Fee_Code { get; set; }
        public string Flight_Fee_Name { get; set; }
        public string Other { get; set; }
        public Nullable<System.Int32> Qty { get; set; }
        public Nullable<System.Decimal> UnitQTY { get; set; }
        public Nullable<System.Decimal> AmountTH { get; set; }

    }

    public class FullTaxModel
    {
        public string DESC { get; set; }
        public decimal AMT { get; set; }
        public string Customer { get; set; }
        public string InvoiceNo { get; set; }
        public string Ref { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Bill_Collector { get; set; }
        public decimal Amount { get; set; }
        public decimal Amount_Service { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount_Total { get; set; }
        public decimal AirportTax { get; set; }
        public decimal Amount_Rep { get; set; }
        public decimal Grand_Total { get; set; }
        public string toWord { get; set; }
        public int Qty { get; set; }
    }
}