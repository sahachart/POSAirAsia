using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem
{
    public partial class T_BookingServiceChargeCurrentUpdate
    {
        public decimal Discount { get; set; }
        public decimal AmountTHB { get; set; }
        public decimal Exc { get; set; }
        public DateTime Booking_Date { get; set; }
        public string AbbGroup { get; set; }
        public int RowNo { get; set; }
    }
}