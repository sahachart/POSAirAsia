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
    public partial class T_Invoice_CN
    {
        public T_Invoice_CN()
        {
            this.T_Invoice_Info = new HashSet<T_Invoice_Info>();
        }
    
        public System.Guid CN_ID { get; set; }
        public System.Guid TransactionId { get; set; }
        public string INV_No { get; set; }
        public string PNR_No { get; set; }
        public string CN_NO { get; set; }
        public int CNReason_Id { get; set; }
        public string CNReason_Code { get; set; }
        public string Create_By { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
    
        public virtual M_CNReason M_CNReason { get; set; }
        public virtual ICollection<T_Invoice_Info> T_Invoice_Info { get; set; }
    }
}
