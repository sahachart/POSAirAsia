using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem.Model
{
    public class CriteriaModel
    {
    }

    public class CriteriaMachinePOSModel
    {
        public string station_from { get; set; }
        public string station_to { get; set; }
        public Nullable<DateTime> date_from { get; set; }
        public Nullable<DateTime> date_to { get; set; }
        public string Flight_Type_Code { get; set; }
    }
    public class InvoiceModel
    {
        public T_Invoice_Manual info { get; set; }
        public List<T_Invoice_Detail> detail { get; set; }
    }

    public class InvoiceCNModel
    {
        public string CN_ID { get; set; }
        public string TransactionId { get; set; }
        public string INV_No { get; set; }
        public string PNR_No { get; set; }
        public string CN_NO { get; set; }
        public int CNReason_Id { get; set; }
        public string CNReason_Code { get; set; }
        public string Create_By { get; set; }

        public string mode { get; set; }

    }

    public class CriteriaInvoiceModel
    {
        public string booking_no { get; set; }
        public DateTime? date_from { get; set; }
        public DateTime? date_to { get; set; }
        public string passenger { get; set; }


    }
}