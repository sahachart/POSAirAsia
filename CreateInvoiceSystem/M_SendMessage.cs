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
    public partial class M_SendMessage
    {
        public int ID { get; set; }
        public string DescMessage { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public string IsActive { get; set; }
        public string Create_By { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string Update_By { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
    }
}
