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
    public partial class T_Booking
    {
        public System.Guid TransactionId { get; set; }
        public long BookingId { get; set; }
        public System.DateTime Load_Date { get; set; }
        public string PNR_No { get; set; }
        public System.DateTime Booking_Date { get; set; }
        public string CurrencyCode { get; set; }
        public string PaxCount { get; set; }
        public long BookingParentID { get; set; }
        public string ParentRecordLocator { get; set; }
        public string GroupName { get; set; }
        public string BookingStatus { get; set; }
        public string TotalCost { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
    }
}
