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
    public partial class T_PaymentNewItem
    {
        public System.Guid PaymentTId { get; set; }
        public System.Guid TransactionId { get; set; }
        public long PaymentID { get; set; }
        public string ReferenceType { get; set; }
        public long ReferenceID { get; set; }
        public string PaymentMethodType { get; set; }
        public string PaymentMethodCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal PaymentAmount { get; set; }
        public string CollectedCurrencyCode { get; set; }
        public decimal CollectedAmount { get; set; }
        public string QuotedCurrencyCode { get; set; }
        public decimal QuotedAmount { get; set; }
        public string Status { get; set; }
        public long ParentPaymentID { get; set; }
        public string AgentCode { get; set; }
        public string OrganizationCode { get; set; }
        public string DomainCode { get; set; }
        public string LocationCode { get; set; }
        public System.DateTime ApprovalDate { get; set; }
        public string Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public string Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
    }
}
