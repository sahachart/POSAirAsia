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
    public partial class M_FlightFee
    {
        public int Flight_Fee_Id { get; set; }
        public string Flight_Fee_Code { get; set; }
        public string Flight_Fee_Name { get; set; }
        public string DisplayAbb { get; set; }
        public string IsActive { get; set; }
        public string Create_By { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string Update_By { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public string AbbGroup { get; set; }
        public Nullable<System.Guid> FlightFeeGroup_ID { get; set; }
    
        public virtual M_FlightFeeGroup M_FlightFeeGroup { get; set; }
    }
}
