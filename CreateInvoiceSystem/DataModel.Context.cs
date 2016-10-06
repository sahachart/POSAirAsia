﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class POSINVEntities : DbContext
    {
        public POSINVEntities()
            : base("name=POSINVEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<M_Country> M_Country { get; set; }
        public DbSet<M_Province> M_Province { get; set; }
        public DbSet<M_FlightType> M_FlightType { get; set; }
        public DbSet<M_Agency> M_Agency { get; set; }
        public DbSet<M_Station> M_Station { get; set; }
        public DbSet<M_CNReason> M_CNReason { get; set; }
        public DbSet<M_PaymentType> M_PaymentType { get; set; }
        public DbSet<M_FlightFee> M_FlightFee { get; set; }
        public DbSet<M_Currency> M_Currency { get; set; }
        public DbSet<T_ABBFARE> T_ABBFARE { get; set; }
        public DbSet<T_ABBPAX> T_ABBPAX { get; set; }
        public DbSet<T_ABBPAYMENTMETHOD> T_ABBPAYMENTMETHOD { get; set; }
        public DbSet<T_LogsGenABB> T_LogsGenABB { get; set; }
        public DbSet<M_POS_Machine> M_POS_Machine { get; set; }
        public DbSet<T_BookingCurrentUpdate> T_BookingCurrentUpdate { get; set; }
        public DbSet<T_BookingServiceChargeCurrentUpdate> T_BookingServiceChargeCurrentUpdate { get; set; }
        public DbSet<T_PassengerFeeCurrentUpdate> T_PassengerFeeCurrentUpdate { get; set; }
        public DbSet<T_PassengerFeeServiceChargeCurrentUpdate> T_PassengerFeeServiceChargeCurrentUpdate { get; set; }
        public DbSet<T_PaxFareCurrentUpdate> T_PaxFareCurrentUpdate { get; set; }
        public DbSet<T_PaymentCurrentUpdate> T_PaymentCurrentUpdate { get; set; }
        public DbSet<T_SegmentCurrentUpdate> T_SegmentCurrentUpdate { get; set; }
        public DbSet<T_Passenger> T_Passenger { get; set; }
        public DbSet<T_Payment> T_Payment { get; set; }
        public DbSet<M_SlipMessageDetails> M_SlipMessageDetails { get; set; }
        public DbSet<rpt> rpts { get; set; }
        public DbSet<T_Booking> T_Booking { get; set; }
        public DbSet<T_BookingServiceCharge> T_BookingServiceCharge { get; set; }
        public DbSet<T_BookingServiceChargeNewItem> T_BookingServiceChargeNewItem { get; set; }
        public DbSet<T_PassengerFee> T_PassengerFee { get; set; }
        public DbSet<T_PassengerFeeNewItem> T_PassengerFeeNewItem { get; set; }
        public DbSet<T_PassengerFeeServiceCharge> T_PassengerFeeServiceCharge { get; set; }
        public DbSet<T_PassengerFeeServiceChargeNewItem> T_PassengerFeeServiceChargeNewItem { get; set; }
        public DbSet<T_PassengerNewItem> T_PassengerNewItem { get; set; }
        public DbSet<T_PaxFare> T_PaxFare { get; set; }
        public DbSet<T_PaxFareNewItem> T_PaxFareNewItem { get; set; }
        public DbSet<T_PaymentNewItem> T_PaymentNewItem { get; set; }
        public DbSet<T_Segment> T_Segment { get; set; }
        public DbSet<T_SegmentNewItem> T_SegmentNewItem { get; set; }
        public DbSet<T_WaitingForManualGenABB> T_WaitingForManualGenABB { get; set; }
        public DbSet<T_PassengerCurrentUpdate> T_PassengerCurrentUpdate { get; set; }
        public DbSet<M_EmpPOS> M_EmpPOS { get; set; }
        public DbSet<M_SlipMessage> M_SlipMessage { get; set; }
        public DbSet<M_SendMessage> M_SendMessage { get; set; }
        public DbSet<M_AgencyType> M_AgencyType { get; set; }
        public DbSet<M_VAT> M_VAT { get; set; }
        public DbSet<T_Invoice_Manual> T_Invoice_Manual { get; set; }
        public DbSet<T_Invoice_Seq> T_Invoice_Seq { get; set; }
        public DbSet<M_Menu> M_Menu { get; set; }
        public DbSet<T_LogAccess> T_LogAccess { get; set; }
        public DbSet<M_AppSystem> M_AppSystem { get; set; }
        public DbSet<M_UserGroupMenu> M_UserGroupMenu { get; set; }
        public DbSet<M_UserPermission> M_UserPermission { get; set; }
        public DbSet<T_Invoice_CN> T_Invoice_CN { get; set; }
        public DbSet<T_LogBookingCurrentUpdate> T_LogBookingCurrentUpdate { get; set; }
        public DbSet<M_Employee> M_Employee { get; set; }
        public DbSet<T_Invoice_Flight> T_Invoice_Flight { get; set; }
        public DbSet<T_Invoice_Detail> T_Invoice_Detail { get; set; }
        public DbSet<T_ABB> T_ABB { get; set; }
        public DbSet<T_Invoice_Fare> T_Invoice_Fare { get; set; }
        public DbSet<T_Invoice_Fee> T_Invoice_Fee { get; set; }
        public DbSet<T_Invoice_Payment> T_Invoice_Payment { get; set; }
        public DbSet<M_FlightFeeGroup> M_FlightFeeGroup { get; set; }
        public DbSet<M_Customer> M_Customer { get; set; }
        public DbSet<T_Invoice_Info> T_Invoice_Info { get; set; }
        public DbSet<M_UserGroupMappingAD> M_UserGroupMappingAD { get; set; }
        public DbSet<M_UserGroup> M_UserGroup { get; set; }
    
        public virtual int GenerateInvoiceABBOnDaily()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerateInvoiceABBOnDaily");
        }
    
        public virtual int Insert_T_ABB(Nullable<System.Guid> transactionId, string pnrno, string createby, string inv, Nullable<int> paxcountadt, Nullable<int> paxcountchd, Nullable<decimal> adminfeeth, Nullable<decimal> fuelth, Nullable<decimal> serviceth, Nullable<decimal> vatth, Nullable<decimal> airporttaxth, Nullable<decimal> insurth, Nullable<decimal> farepriceth, Nullable<decimal> otherth, Nullable<decimal> repth, Nullable<decimal> paymentamtth, Nullable<decimal> exchangeRateth)
        {
            var transactionIdParameter = transactionId.HasValue ?
                new ObjectParameter("TransactionId", transactionId) :
                new ObjectParameter("TransactionId", typeof(System.Guid));
    
            var pnrnoParameter = pnrno != null ?
                new ObjectParameter("pnrno", pnrno) :
                new ObjectParameter("pnrno", typeof(string));
    
            var createbyParameter = createby != null ?
                new ObjectParameter("createby", createby) :
                new ObjectParameter("createby", typeof(string));
    
            var invParameter = inv != null ?
                new ObjectParameter("inv", inv) :
                new ObjectParameter("inv", typeof(string));
    
            var paxcountadtParameter = paxcountadt.HasValue ?
                new ObjectParameter("paxcountadt", paxcountadt) :
                new ObjectParameter("paxcountadt", typeof(int));
    
            var paxcountchdParameter = paxcountchd.HasValue ?
                new ObjectParameter("paxcountchd", paxcountchd) :
                new ObjectParameter("paxcountchd", typeof(int));
    
            var adminfeethParameter = adminfeeth.HasValue ?
                new ObjectParameter("adminfeeth", adminfeeth) :
                new ObjectParameter("adminfeeth", typeof(decimal));
    
            var fuelthParameter = fuelth.HasValue ?
                new ObjectParameter("fuelth", fuelth) :
                new ObjectParameter("fuelth", typeof(decimal));
    
            var servicethParameter = serviceth.HasValue ?
                new ObjectParameter("Serviceth", serviceth) :
                new ObjectParameter("Serviceth", typeof(decimal));
    
            var vatthParameter = vatth.HasValue ?
                new ObjectParameter("vatth", vatth) :
                new ObjectParameter("vatth", typeof(decimal));
    
            var airporttaxthParameter = airporttaxth.HasValue ?
                new ObjectParameter("airporttaxth", airporttaxth) :
                new ObjectParameter("airporttaxth", typeof(decimal));
    
            var insurthParameter = insurth.HasValue ?
                new ObjectParameter("insurth", insurth) :
                new ObjectParameter("insurth", typeof(decimal));
    
            var farepricethParameter = farepriceth.HasValue ?
                new ObjectParameter("farepriceth", farepriceth) :
                new ObjectParameter("farepriceth", typeof(decimal));
    
            var otherthParameter = otherth.HasValue ?
                new ObjectParameter("otherth", otherth) :
                new ObjectParameter("otherth", typeof(decimal));
    
            var repthParameter = repth.HasValue ?
                new ObjectParameter("repth", repth) :
                new ObjectParameter("repth", typeof(decimal));
    
            var paymentamtthParameter = paymentamtth.HasValue ?
                new ObjectParameter("paymentamtth", paymentamtth) :
                new ObjectParameter("paymentamtth", typeof(decimal));
    
            var exchangeRatethParameter = exchangeRateth.HasValue ?
                new ObjectParameter("ExchangeRateth", exchangeRateth) :
                new ObjectParameter("ExchangeRateth", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_T_ABB", transactionIdParameter, pnrnoParameter, createbyParameter, invParameter, paxcountadtParameter, paxcountchdParameter, adminfeethParameter, fuelthParameter, servicethParameter, vatthParameter, airporttaxthParameter, insurthParameter, farepricethParameter, otherthParameter, repthParameter, paymentamtthParameter, exchangeRatethParameter);
        }
    
        public virtual int Insert_T_ABB_NewItem(Nullable<System.Guid> transactionId, string pnrno, string createby, string inv, Nullable<int> paxcountadt, Nullable<int> paxcountchd, Nullable<decimal> adminfeeth, Nullable<decimal> fuelth, Nullable<decimal> serviceth, Nullable<decimal> vatth, Nullable<decimal> airporttaxth, Nullable<decimal> insurth, Nullable<decimal> farepriceth, Nullable<decimal> otherth, Nullable<decimal> repth, Nullable<decimal> paymentamtth, Nullable<decimal> exchangeRateth, Nullable<System.Guid> tranExistId)
        {
            var transactionIdParameter = transactionId.HasValue ?
                new ObjectParameter("TransactionId", transactionId) :
                new ObjectParameter("TransactionId", typeof(System.Guid));
    
            var pnrnoParameter = pnrno != null ?
                new ObjectParameter("pnrno", pnrno) :
                new ObjectParameter("pnrno", typeof(string));
    
            var createbyParameter = createby != null ?
                new ObjectParameter("createby", createby) :
                new ObjectParameter("createby", typeof(string));
    
            var invParameter = inv != null ?
                new ObjectParameter("inv", inv) :
                new ObjectParameter("inv", typeof(string));
    
            var paxcountadtParameter = paxcountadt.HasValue ?
                new ObjectParameter("paxcountadt", paxcountadt) :
                new ObjectParameter("paxcountadt", typeof(int));
    
            var paxcountchdParameter = paxcountchd.HasValue ?
                new ObjectParameter("paxcountchd", paxcountchd) :
                new ObjectParameter("paxcountchd", typeof(int));
    
            var adminfeethParameter = adminfeeth.HasValue ?
                new ObjectParameter("adminfeeth", adminfeeth) :
                new ObjectParameter("adminfeeth", typeof(decimal));
    
            var fuelthParameter = fuelth.HasValue ?
                new ObjectParameter("fuelth", fuelth) :
                new ObjectParameter("fuelth", typeof(decimal));
    
            var servicethParameter = serviceth.HasValue ?
                new ObjectParameter("Serviceth", serviceth) :
                new ObjectParameter("Serviceth", typeof(decimal));
    
            var vatthParameter = vatth.HasValue ?
                new ObjectParameter("vatth", vatth) :
                new ObjectParameter("vatth", typeof(decimal));
    
            var airporttaxthParameter = airporttaxth.HasValue ?
                new ObjectParameter("airporttaxth", airporttaxth) :
                new ObjectParameter("airporttaxth", typeof(decimal));
    
            var insurthParameter = insurth.HasValue ?
                new ObjectParameter("insurth", insurth) :
                new ObjectParameter("insurth", typeof(decimal));
    
            var farepricethParameter = farepriceth.HasValue ?
                new ObjectParameter("farepriceth", farepriceth) :
                new ObjectParameter("farepriceth", typeof(decimal));
    
            var otherthParameter = otherth.HasValue ?
                new ObjectParameter("otherth", otherth) :
                new ObjectParameter("otherth", typeof(decimal));
    
            var repthParameter = repth.HasValue ?
                new ObjectParameter("repth", repth) :
                new ObjectParameter("repth", typeof(decimal));
    
            var paymentamtthParameter = paymentamtth.HasValue ?
                new ObjectParameter("paymentamtth", paymentamtth) :
                new ObjectParameter("paymentamtth", typeof(decimal));
    
            var exchangeRatethParameter = exchangeRateth.HasValue ?
                new ObjectParameter("ExchangeRateth", exchangeRateth) :
                new ObjectParameter("ExchangeRateth", typeof(decimal));
    
            var tranExistIdParameter = tranExistId.HasValue ?
                new ObjectParameter("TranExistId", tranExistId) :
                new ObjectParameter("TranExistId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_T_ABB_NewItem", transactionIdParameter, pnrnoParameter, createbyParameter, invParameter, paxcountadtParameter, paxcountchdParameter, adminfeethParameter, fuelthParameter, servicethParameter, vatthParameter, airporttaxthParameter, insurthParameter, farepricethParameter, otherthParameter, repthParameter, paymentamtthParameter, exchangeRatethParameter, tranExistIdParameter);
        }
    
        public virtual int InsertLogABBGenerate(string pnrno, Nullable<System.Guid> transId, string msg, string createby)
        {
            var pnrnoParameter = pnrno != null ?
                new ObjectParameter("pnrno", pnrno) :
                new ObjectParameter("pnrno", typeof(string));
    
            var transIdParameter = transId.HasValue ?
                new ObjectParameter("TransId", transId) :
                new ObjectParameter("TransId", typeof(System.Guid));
    
            var msgParameter = msg != null ?
                new ObjectParameter("msg", msg) :
                new ObjectParameter("msg", typeof(string));
    
            var createbyParameter = createby != null ?
                new ObjectParameter("createby", createby) :
                new ObjectParameter("createby", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertLogABBGenerate", pnrnoParameter, transIdParameter, msgParameter, createbyParameter);
        }
    
        public virtual int InsertManualABBGenerate(string pnrno, string msg, Nullable<System.Guid> transId, string createby)
        {
            var pnrnoParameter = pnrno != null ?
                new ObjectParameter("pnrno", pnrno) :
                new ObjectParameter("pnrno", typeof(string));
    
            var msgParameter = msg != null ?
                new ObjectParameter("Msg", msg) :
                new ObjectParameter("Msg", typeof(string));
    
            var transIdParameter = transId.HasValue ?
                new ObjectParameter("TransId", transId) :
                new ObjectParameter("TransId", typeof(System.Guid));
    
            var createbyParameter = createby != null ?
                new ObjectParameter("createby", createby) :
                new ObjectParameter("createby", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertManualABBGenerate", pnrnoParameter, msgParameter, transIdParameter, createbyParameter);
        }
    
        public virtual int PrepareTableForBatchJob()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PrepareTableForBatchJob");
        }
    
        public virtual ObjectResult<string> GenerateInvoiceABBOnline(string pnrno, Nullable<int> userid, string posCode, string stationcode)
        {
            var pnrnoParameter = pnrno != null ?
                new ObjectParameter("pnrno", pnrno) :
                new ObjectParameter("pnrno", typeof(string));
    
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var posCodeParameter = posCode != null ?
                new ObjectParameter("posCode", posCode) :
                new ObjectParameter("posCode", typeof(string));
    
            var stationcodeParameter = stationcode != null ?
                new ObjectParameter("stationcode", stationcode) :
                new ObjectParameter("stationcode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GenerateInvoiceABBOnline", pnrnoParameter, useridParameter, posCodeParameter, stationcodeParameter);
        }
    
        public virtual int Insert_T_ABB_Online(Nullable<System.Guid> transactionId, string pnrno, string createby, string inv, Nullable<int> paxcountadt, Nullable<int> paxcountchd, Nullable<decimal> adminfeeth, Nullable<decimal> fuelth, Nullable<decimal> serviceth, Nullable<decimal> vatth, Nullable<decimal> airporttaxth, Nullable<decimal> insurth, Nullable<decimal> farepriceth, Nullable<decimal> otherth, Nullable<decimal> repth, Nullable<decimal> paymentamtth, Nullable<decimal> exchangeRateth)
        {
            var transactionIdParameter = transactionId.HasValue ?
                new ObjectParameter("TransactionId", transactionId) :
                new ObjectParameter("TransactionId", typeof(System.Guid));
    
            var pnrnoParameter = pnrno != null ?
                new ObjectParameter("pnrno", pnrno) :
                new ObjectParameter("pnrno", typeof(string));
    
            var createbyParameter = createby != null ?
                new ObjectParameter("createby", createby) :
                new ObjectParameter("createby", typeof(string));
    
            var invParameter = inv != null ?
                new ObjectParameter("inv", inv) :
                new ObjectParameter("inv", typeof(string));
    
            var paxcountadtParameter = paxcountadt.HasValue ?
                new ObjectParameter("paxcountadt", paxcountadt) :
                new ObjectParameter("paxcountadt", typeof(int));
    
            var paxcountchdParameter = paxcountchd.HasValue ?
                new ObjectParameter("paxcountchd", paxcountchd) :
                new ObjectParameter("paxcountchd", typeof(int));
    
            var adminfeethParameter = adminfeeth.HasValue ?
                new ObjectParameter("adminfeeth", adminfeeth) :
                new ObjectParameter("adminfeeth", typeof(decimal));
    
            var fuelthParameter = fuelth.HasValue ?
                new ObjectParameter("fuelth", fuelth) :
                new ObjectParameter("fuelth", typeof(decimal));
    
            var servicethParameter = serviceth.HasValue ?
                new ObjectParameter("Serviceth", serviceth) :
                new ObjectParameter("Serviceth", typeof(decimal));
    
            var vatthParameter = vatth.HasValue ?
                new ObjectParameter("vatth", vatth) :
                new ObjectParameter("vatth", typeof(decimal));
    
            var airporttaxthParameter = airporttaxth.HasValue ?
                new ObjectParameter("airporttaxth", airporttaxth) :
                new ObjectParameter("airporttaxth", typeof(decimal));
    
            var insurthParameter = insurth.HasValue ?
                new ObjectParameter("insurth", insurth) :
                new ObjectParameter("insurth", typeof(decimal));
    
            var farepricethParameter = farepriceth.HasValue ?
                new ObjectParameter("farepriceth", farepriceth) :
                new ObjectParameter("farepriceth", typeof(decimal));
    
            var otherthParameter = otherth.HasValue ?
                new ObjectParameter("otherth", otherth) :
                new ObjectParameter("otherth", typeof(decimal));
    
            var repthParameter = repth.HasValue ?
                new ObjectParameter("repth", repth) :
                new ObjectParameter("repth", typeof(decimal));
    
            var paymentamtthParameter = paymentamtth.HasValue ?
                new ObjectParameter("paymentamtth", paymentamtth) :
                new ObjectParameter("paymentamtth", typeof(decimal));
    
            var exchangeRatethParameter = exchangeRateth.HasValue ?
                new ObjectParameter("ExchangeRateth", exchangeRateth) :
                new ObjectParameter("ExchangeRateth", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_T_ABB_Online", transactionIdParameter, pnrnoParameter, createbyParameter, invParameter, paxcountadtParameter, paxcountchdParameter, adminfeethParameter, fuelthParameter, servicethParameter, vatthParameter, airporttaxthParameter, insurthParameter, farepricethParameter, otherthParameter, repthParameter, paymentamtthParameter, exchangeRatethParameter);
        }
    }
}