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
    public partial class T_PassengerFeeCurrentUpdate
    {
        public System.Guid PassengerFeeTId { get; set; }
        public System.Guid PassengerTId { get; set; }
        public Nullable<long> PassengerId { get; set; }
        public Nullable<long> PassengerFeeNo { get; set; }
        public Nullable<System.DateTime> FeeCreateDate { get; set; }
        public string ABBNo { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
        public int Action { get; set; }
    }
}
