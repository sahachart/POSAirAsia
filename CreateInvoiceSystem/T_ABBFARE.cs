//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreateInvoiceSystem
{
    using System;
    using System.Collections.Generic;
    
    [Serializable]
    public partial class T_ABBFARE
    {
        public System.Guid ABBFAREId { get; set; }
        public System.Guid ABBTid { get; set; }
        public string PNR_No { get; set; }
        public int SEQ { get; set; }
        public string CarrierCode { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public System.DateTime STD { get; set; }
        public System.DateTime STA { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount_Ori { get; set; }
        public decimal ExchangeRateTH { get; set; }
    }
}
