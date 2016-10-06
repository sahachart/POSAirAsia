using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem.DAO
{
    public class MessageResponse
    {
        public int ID { get; set; }
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }
    }
}