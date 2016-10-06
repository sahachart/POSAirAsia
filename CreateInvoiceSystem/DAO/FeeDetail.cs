using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateInvoiceSystem.DAO
{
    public class FeeDetail
    {
        public string PaxType { get; set; }
        public string ChargeCode { get; set; }
        public string ChargeType { get; set; }
        public string ChargeDetail { get; set; }

        public long? PassengerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string TicketCode { get; set; }
        public string CollectType { get; set; }
        public string ForeignCurrencyCode { get; set; }
        public decimal ForeignAmount { get; set; }

        
        public string PaxFareTId { get; set; }
        public string SegmentTId { get; set; }
        public string SeqmentNo { get; set; }
        public string SeqmentPaxNo { get; set; }
        public string ServiceChargeNo { get; set; }
        public string PassengerFeeNo { get; set; }

        public DateTime? DepartureDate { get; set; }
        
        public DateTime FeeCreateDate { get; set; }
        public DateTime FareCreateDate { get; set; }

        public DateTime Booking_Date { get; set; }

        public int qty { get; set; }
        public decimal? price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? amt { get; set; }
        public string AbbGroup { get; set; }
        public string TaxInvoiceNo { get; set; }
        public string Header { get; set; }

        public string BaseCurrencyCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? BaseAmount { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DiscountOri { get; set; }
        public decimal? Exc { get; set; }


        public decimal AmountTHB { get; set; }
        public decimal TotalAmountTHB { get; set; }



        public System.Guid RowID { get; set; }
        public int RowNo { get; set; }
        public int RowState { get; set; }
        public string TableName { get; set; }

        public string index { get; set; }
        public string Create_By { get; set; }
        public string Update_By { get; set; }



    }
}
