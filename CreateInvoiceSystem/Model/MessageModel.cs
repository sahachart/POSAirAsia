using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem.Model
{
    public class MessageModel
    {
        public int POS_Id { get; set; }
        public string POS_Code { get; set; }
        public string POS_MacNo { get; set; }
        public string Station_Code { get; set; }
        public string SlipMessage_Code { get; set; }
        public string Descriptions { get; set; }
        public int SortID { get; set; }
        public Boolean SlipStat { get; set; }
    }

    public class FormPrintModel
    {
        public FormPrintModel()
        {
            this.MessageModels = new List<MessageModel>();
        }
        public bool IsA4 { get; set; }

        public List<MessageModel> MessageModels { get; set; }
    }
}