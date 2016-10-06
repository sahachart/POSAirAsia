using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateInvoiceSystem.Model
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserGroup { get; set; }
        public int POS_Id { get; set; }
        public string POS_Code { get; set; }
        public string POS_MacNo { get; set; }
        public string SlipMessage_Code { get; set; }
        public string Station_ID { get; set; }
        public string Station_Code { get; set; }

    }

    [Serializable]
    public class EmpModel
    {
        public int UserID { get; set; }
        public string UserCode { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string UserGroupCode { get; set; }
        public string UserGroupName { get; set; }
        public string Station_Code { get; set; }
        public string Station_Name { get; set; }

    }

    [Serializable]
    public class Monitoruser
    {
        public string Username { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Station_Name { get; set; }
        public string UserGroupName { get; set; }
        public string POS_MacNo { get; set; }
        public string Menu_Name { get; set; }

    }

    [Serializable]
    public class MenuPrivilege
    {
        public string Menu_Code { get; set; }
        public string Menu_Name { get; set; }
        public int CanView { get; set; }
        public int CanInsert { get; set; }
        public int CanDelete { get; set; }
        public int CanUpdate { get; set; }
        public int CanProcess { get; set; }
        public int CanPrint { get; set; }
    }

    [Serializable]
    public class LogABB
    {
        public string PNR_No { get; set; }
        public string Msg { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
    }
}