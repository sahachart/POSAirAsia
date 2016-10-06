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
    public partial class M_Customer
    {
        public M_Customer()
        {
            this.T_Invoice_Info = new HashSet<T_Invoice_Info>();
        }
    
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string prfx_name { get; set; }
        public string gndr_code { get; set; }
        public string Addr_1 { get; set; }
        public string Addr_2 { get; set; }
        public string Addr_3 { get; set; }
        public string Prvn_Code { get; set; }
        public string Cntr_Code { get; set; }
        public string Zip_Code { get; set; }
        public string Home_Phone { get; set; }
        public string Email { get; set; }
        public string remark { get; set; }
        public Nullable<System.DateTime> crtd_dt { get; set; }
        public string crtd_by { get; set; }
        public Nullable<System.DateTime> last_upd_dt { get; set; }
        public string last_upd_by { get; set; }
        public string Addr_4 { get; set; }
        public string IsActive { get; set; }
        public string TaxID { get; set; }
        public System.Guid CustomerID { get; set; }
        public string Addr_5 { get; set; }
        public string Branch_No { get; set; }
    
        public virtual ICollection<T_Invoice_Info> T_Invoice_Info { get; set; }
    }
}