using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem
{
    public partial class T_SegmentCurrentUpdate
    {
        public int RowNo { get; set; }
        public int RowState { get; set; }

        public string STD_Date { get; set; }
        public string STA_Date { get; set; }

        public string STD_Time { get; set; }
        public string STA_Time { get; set; }
    }
}