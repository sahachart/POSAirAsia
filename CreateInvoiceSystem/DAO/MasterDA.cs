using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using CreateInvoiceSystem.Model;
using System.Configuration;
using CreateInvoiceSystem.Reports;
using System.Transactions;
using System.Data.Entity; 
namespace CreateInvoiceSystem.DAO
{
   
    public class MasterDA
    {
        public string strErrorDataUsed = "Can not delete because this data is already used.";
        public string DeleteAgenType(string ID) {
            using (var context = new POSINVEntities())
            {
                var id = int.Parse(ID);
                var original = context.M_AgencyType.Where(x => x.AgencyTypeId == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.M_Agency.Count(x => x.Agency_Type_Code == original.Agent_type_code) > 0)
                    {
                        return strErrorDataUsed;
                    }

                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;

           // SqlCommand sqlcmd = new SqlCommand();
           // sqlcmd.CommandText = "SP_Del_M_AgencyType";
           // sqlcmd.CommandType = CommandType.StoredProcedure;
           // sqlcmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = ID;
           //return FunctionAndClass.IexcuteData(sqlcmd);
        }

        public M_Employee GetUsers(string username, string password)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var found = context.M_Employee.FirstOrDefault(f => f.Username == username
                        && f.Password == password && f.Active == "Y");
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public M_Employee AddUsersAD(M_Employee emp)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    M_Employee emp_db = context.M_Employee.Where(it => it.Username == emp.Username && it.IsUserAD).FirstOrDefault();
                    if (emp_db == null)
                    {
                        context.M_Employee.Add(emp);
                        context.SaveChanges();
                    }

                    emp_db = context.M_Employee.Where(it => it.Username == emp.Username && it.IsUserAD).FirstOrDefault();


                    return emp_db;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public M_UserGroup GetUserGroup(string GroupName)
        {
            using (var context = new POSINVEntities())
            {
                M_UserGroupMappingAD g_AD = context.M_UserGroupMappingAD.Where(x => x.AD_Department == GroupName).FirstOrDefault();
                if (g_AD != null)
                {
                    M_UserGroup g = context.M_UserGroup.Where(x => x.UserGroupId == g_AD.UserGroupId).FirstOrDefault();
                    if (g != null)
                    {
                        return g;
                    }
                }

               
                
            }
            return null;
        }

        public M_Menu GetMenu(string menu)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var found = context.M_Menu.FirstOrDefault(f => f.Menu_Code == menu);
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public void SaveCountry(string code, string name, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_Country { CountryCode=code, CountryName = name, CountryId = -1, Create_By = Create_By, IsActive = "Y", Create_Date = DateTime.Now
                , Update_By = Create_By, Update_Date = DateTime.Now}; 
                context.M_Country.Add(contry); 
                context.SaveChanges();
            }
        }

        public string DeleteCountry(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                
                var original = context.M_Country.Where(x => x.CountryId == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.M_Customer.Count(x => x.Cntr_Code == original.CountryCode) > 0)
                    {
                        return strErrorDataUsed;
                    }

                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public M_Country GetCountry(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Country.Where(x => x.CountryId == id).FirstOrDefault();
                return _item;
            }
        }

        public void EditCountry(int id, string code, string name, string Update_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Country.Where(x => x.CountryId == id).FirstOrDefault();
                original.CountryCode = code;
                original.CountryName = name;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.CountryCode).IsModified = true;
                entry.Property(e => e.CountryName).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
            }
        }

        public void SaveProvince(string code, string name, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_Province
                {
                    ProvinceCode = code,
                     ProvinceName = name,
                      ProvinceId = -1,
                    Create_By = Create_By,
                    IsActive = "Y",
                    Create_Date = DateTime.Now
                    ,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now
                };
                context.M_Province.Add(contry);
                context.SaveChanges();
            }
        }

        public string DeleteProvince(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Province.Where(x => x.ProvinceId == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.M_Customer.Count(x => x.Prvn_Code == original.ProvinceCode) > 0)
                    {
                        return strErrorDataUsed;
                    }

                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public M_Province GetProvince(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Province.Where(x => x.ProvinceId == id).FirstOrDefault();
                return _item;
            }
        }

        public void EditProvince(int id,  string code, string name, string Update_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Province.Where(x => x.ProvinceId == id).FirstOrDefault();
                original.ProvinceCode = code;
                original.ProvinceName = name;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.ProvinceCode).IsModified = true;
                entry.Property(e => e.ProvinceName).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
            }
        }

        public void SaveFlightType(string code, string name, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_FlightType
                {
                     Flight_Type_Code = code,
                     Flight_Type_Name = name,
                     Flight_Type_Id = -1,
                    Create_By = Create_By,
                    IsActive = "Y",
                    Create_Date = DateTime.Now
                    ,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now
                };
                context.M_FlightType.Add(contry);
                context.SaveChanges();
            }
        }

        public void DeleteFlightType(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_FlightType.Where(x => x.Flight_Type_Id == id).FirstOrDefault();
                original.IsActive = "N";
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.IsActive).IsModified = true;
                context.SaveChanges();
            }
        }

        public M_FlightType GetFlightType(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_FlightType.Where(x => x.Flight_Type_Id == id).FirstOrDefault();
                return _item;
            }
        }

        public void EditFlightType(int id, string code, string name, string Update_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_FlightType.Where(x => x.Flight_Type_Id == id).FirstOrDefault();
                original.Flight_Type_Code = code;
                original.Flight_Type_Name = name;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.Flight_Type_Code).IsModified = true;
                entry.Property(e => e.Flight_Type_Name).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
            }
        }

        public void SaveFlightFee(string code, string name, string displayabb, string addgroup, string Create_By, Guid? FlightFeeGroupID)
        {
            try
            {
                using (var context = new POSINVEntities())
                {
                    var contry = new M_FlightFee
                    {
                        Flight_Fee_Code = code,
                        Flight_Fee_Name = name,
                        Flight_Fee_Id = -1,
                        DisplayAbb = displayabb,
                        AbbGroup = addgroup,
                        Create_By = Create_By,
                        IsActive = "Y",
                        Create_Date = DateTime.Now,
                        Update_By = Create_By,
                        Update_Date = DateTime.Now,
                        FlightFeeGroup_ID = FlightFeeGroupID
                    };
                    context.M_FlightFee.Add(contry);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public string DeleteFlightFee(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_FlightFee.Where(x => x.Flight_Fee_Id == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.T_PassengerFeeServiceChargeCurrentUpdate.Count(x => x.ChargeCode == original.Flight_Fee_Code) > 0 ||
                        context.T_BookingServiceChargeCurrentUpdate.Count(x => x.ChargeCode == original.Flight_Fee_Code) > 0
                        )
                    {
                        return strErrorDataUsed;
                    }
                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public M_FlightFee GetFlightFee(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_FlightFee.Where(x => x.Flight_Fee_Id == id).FirstOrDefault();

                return new M_FlightFee()
                {
                    Flight_Fee_Id = _item.Flight_Fee_Id,
                    Flight_Fee_Code = _item.Flight_Fee_Code,
                    Flight_Fee_Name = _item.Flight_Fee_Name,
                    DisplayAbb = _item.DisplayAbb,
                    IsActive = _item.IsActive,
                    Create_By = _item.Create_By,
                    Create_Date = _item.Create_Date,
                    Update_By = _item.Update_By,
                    Update_Date = _item.Update_Date,
                    AbbGroup = _item.AbbGroup,
                    FlightFeeGroup_ID = _item.FlightFeeGroup_ID,
                };
            }
        }

        public void EditFlightFee(int id, string code, string name, string displayabb, string addgroup, string Update_By, Guid? FlightFeeGroupID)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_FlightFee.Where(x => x.Flight_Fee_Id == id).FirstOrDefault();
                original.Flight_Fee_Code = code;
                original.Flight_Fee_Name = name;
                original.AbbGroup = addgroup;
                original.DisplayAbb = displayabb;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                original.FlightFeeGroup_ID = FlightFeeGroupID;

                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.Flight_Fee_Code).IsModified = true;
                entry.Property(e => e.Flight_Fee_Name).IsModified = true;
                entry.Property(e => e.DisplayAbb).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                entry.Property(e => e.FlightFeeGroup_ID).IsModified = true;
                context.SaveChanges();
            }
        }

        public void SaveAgency(string code, string name, string geninv, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_Agency
                {
                    Agency_Code = code,
                    Agency_Name = name,
                    Agency_Id = -1,
                    GenInvoice = geninv,
                    Create_By = Create_By,
                    IsActive = "Y",
                    Create_Date = DateTime.Now,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now
                };
                context.M_Agency.Add(contry);
                context.SaveChanges();
            }
        }

        public void DeleteAgency(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Agency.Where(x => x.Agency_Id == id).FirstOrDefault();
                original.IsActive = "N";
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.IsActive).IsModified = true;
                context.SaveChanges();
            }
        }

        public M_Agency GetAgency(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Agency.Where(x => x.Agency_Id == id).FirstOrDefault();
                return _item;
            }
        }

        public void EditAgency(int id, string code, string name, string geninv, string Update_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Agency.Where(x => x.Agency_Id == id).FirstOrDefault();
                original.Agency_Code = code;
                original.Agency_Name = name;
                original.GenInvoice = geninv;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.Agency_Code).IsModified = true;
                entry.Property(e => e.Agency_Name).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
            }
        }

        public void SaveCNReason(string code, string name, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_CNReason
                {
                     CNReason_Code = code,
                     CNReason_Name = name,
                     CNReason_Id = -1,
                    Create_By = Create_By,
                    IsActive = "Y",
                    Create_Date = DateTime.Now,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now
                };
                context.M_CNReason.Add(contry);
                context.SaveChanges();
            }
        }

        public string DeleteCNReason(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_CNReason.Where(x => x.CNReason_Id == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.T_Invoice_CN.Count(x => x.CNReason_Code == original.CNReason_Code) > 0)
                    {
                        return strErrorDataUsed;
                    }
                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public M_CNReason GetCNReason(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_CNReason.Where(x => x.CNReason_Id == id).FirstOrDefault();
                return _item;
            }
        }

        public void EditCNReason(int id, string code, string name, string Update_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_CNReason.Where(x => x.CNReason_Id == id).FirstOrDefault();
                original.CNReason_Code = code;
                original.CNReason_Name = name;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.CNReason_Code).IsModified = true;
                entry.Property(e => e.CNReason_Name).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
            }
        }

        public void SavePaymentType(string code, string name, string geninvoice, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_PaymentType
                {
                    PaymentType_Code = code,
                    PaymentType_Name = name,
                    PaymentType_Id = -1,
                    GenInvoice = geninvoice,
                    Create_By = Create_By,
                    IsActive = "Y",
                    Create_Date = DateTime.Now,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now
                };
                context.M_PaymentType.Add(contry);
                context.SaveChanges();
            }
        }

        public string DeletePaymentType(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_PaymentType.Where(x => x.PaymentType_Id == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.T_PaymentCurrentUpdate.Count(x => x.PaymentMethodCode == original.PaymentType_Code) > 0 || context.T_ABBPAYMENTMETHOD.Count(x => x.PaymentMethodCode == original.PaymentType_Code) > 0)
                    {
                        return strErrorDataUsed;
                    }

                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public M_PaymentType GetPaymentType(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_PaymentType.Where(x => x.PaymentType_Id == id).FirstOrDefault();
                if (_item != null)
                {
                    return new M_PaymentType()
                    {
                        PaymentType_Id = _item.PaymentType_Id,
                        PaymentType_Code = _item.PaymentType_Code,
                        PaymentType_Name = _item.PaymentType_Name,
                        GenInvoice = _item.GenInvoice,
                        IsActive = _item.IsActive,
                        Create_By = _item.Create_By,
                        Create_Date = _item.Create_Date,
                        Update_By = _item.Update_By,
                        Update_Date = _item.Update_Date
                    };
                }
                return _item;
            }
        }

        public static List<M_FlightFee> GetFlightFee()
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_FlightFee.Where(x => x.IsActive == "Y").ToList();
                return _item;
            }
        }
        public void EditPaymentType(int id, string code, string name, string geninvoice, string Update_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_PaymentType.Where(x => x.PaymentType_Id == id).FirstOrDefault();
                original.PaymentType_Code = code;
                original.PaymentType_Name = name;
                original.GenInvoice = geninvoice;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.PaymentType_Code).IsModified = true;
                entry.Property(e => e.PaymentType_Name).IsModified = true;
                entry.Property(e => e.GenInvoice).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
            }
        }

        public void SaveStation(string code, string name, string location, string active, string Create_By, bool ABB_Print_A4, bool INV_Print_A4, bool CN_Print_A4, int ABB_SlipMessage_Id, int INV_SlipMessage_Id, int CN_SlipMessage_Id)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_Station
                {
                    Station_Code = code,
                    Station_Name = name,
                    Location_Name = location,
                    Active = active,
                    Station_Id = -1,
                    Create_By = Create_By,
                    IsActive = "Y",
                    Create_Date = DateTime.Now,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now,
                    ABB_Print_A4 = ABB_Print_A4,
                    INV_Print_A4 = INV_Print_A4,
                    CN_Print_A4 = CN_Print_A4,
                    ABB_SlipMessage_Id = ABB_SlipMessage_Id,
                    INV_SlipMessage_Id = INV_SlipMessage_Id,
                    CN_SlipMessage_Id = CN_SlipMessage_Id
                };
                context.M_Station.Add(contry);
                context.SaveChanges();
            }
        }

        public string DeleteStation(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Station.Where(x => x.Station_Id == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.M_POS_Machine.Count(x => x.Station_Code == original.Station_Code) > 0 || 
                        context.M_Employee.Count(x => x.Station_Code == original.Station_Code) > 0 ||
                        context.T_Invoice_Info.Count(x => x.BranchNo == original.Station_Code) > 0 ||
                        context.T_Invoice_Manual.Count(x => x.Station_Code == original.Station_Code) > 0
                        )
                    {
                        return strErrorDataUsed;
                    }

                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public M_Station GetStation(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Station.Where(x => x.Station_Id == id).FirstOrDefault();
                return _item;
            }
        }

        public void EditStation(int id, string code, string name, string location, string active, string Update_By, bool ABB_Print_A4, bool INV_Print_A4, bool CN_Print_A4, int ABB_SlipMessage_Id, int INV_SlipMessage_Id, int CN_SlipMessage_Id)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Station.Where(x => x.Station_Id == id).FirstOrDefault();
                context.M_Station.Attach(original);
                original.Station_Code = code;
                original.Station_Name = name;
                original.Location_Name = location;
                original.Active = active;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                original.ABB_Print_A4 = ABB_Print_A4;
                original.INV_Print_A4 = INV_Print_A4;
                original.CN_Print_A4 = CN_Print_A4;
                original.ABB_SlipMessage_Id = ABB_SlipMessage_Id;
                original.INV_SlipMessage_Id = INV_SlipMessage_Id;
                original.CN_SlipMessage_Id = CN_SlipMessage_Id;

                context.SaveChanges();
            }
        }

        public void SavePOS(string code, string POS_MacNo, string Station_Code, string SlipMessage_Code
            , string SetEJ, string CusDisplay, string CashDrawer, string FormatPrint, string Active
            , string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var contry = new M_POS_Machine
                {
                    POS_Code = code,
                    POS_MacNo = POS_MacNo,
                    Station_Code = Station_Code,
                    SlipMessage_Code = SlipMessage_Code,
                    POS_Id = -1,
                    SetEJ = SetEJ,
                    CusDisplay = CusDisplay,
                    CashDrawer = CashDrawer,
                    FormatPrint = FormatPrint,
                    Last_Inv_No = "",
                    Last_TAX_No = "",
                    Last_Refund_Inv_No = "",
                    Create_By = Create_By,
                    Active = Active,
                    IsActive = "Y",
                    Create_Date = DateTime.Now,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now
                };
                context.M_POS_Machine.Add(contry);
                context.SaveChanges();
            }
        }

        public string DeletePOS(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_POS_Machine.Where(x => x.POS_Id == id).FirstOrDefault();
                if (original != null)
                {
                    if (context.T_LogAccess.Count(x => x.POS_Id == original.POS_Id) > 0 ||
                        context.T_Invoice_Manual.Count(x => x.POS_Code == original.POS_Code) > 0 ||
                        context.T_Invoice_Info.Count(x => x.POS_Code == original.POS_Code) > 0 ||
                        context.T_ABB.Count(x => x.POS_Id == original.POS_Id) > 0
                        )
                    {
                        return strErrorDataUsed;
                    }
                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public M_POS_Machine GetPOS(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_POS_Machine.Where(x => x.POS_Id == id).FirstOrDefault();
                var _POS_Machine = new M_POS_Machine()
                    {
                        POS_Id = _item.POS_Id,
                        POS_Code = _item.POS_Code,
                        POS_MacNo = _item.POS_MacNo,
                        Station_Code = _item.Station_Code,
                        SlipMessage_Code = _item.SlipMessage_Code,
                        SetEJ = _item.SetEJ,
                        CusDisplay = _item.CusDisplay,
                        CashDrawer = _item.CashDrawer,
                        FormatPrint = _item.FormatPrint,
                        Last_Inv_No = _item.Last_Inv_No,
                        Last_TAX_No = _item.Last_TAX_No,
                        Last_Refund_Inv_No = _item.Last_Refund_Inv_No,
                        Active = _item.Active,
                        IsActive = _item.IsActive,
                        Create_By = _item.Create_By,
                        Create_Date = _item.Create_Date,
                        Update_By = _item.Update_By,
                        Update_Date = _item.Update_Date
                    };
                return _POS_Machine;
            }
        }

        public List<M_POS_Machine> GetPOSByStn(string stn)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_POS_Machine.Where(x => x.Station_Code == stn && x.Active == "Y" && x.IsActive == "Y").ToList()
                    .Select(s => new M_POS_Machine()
                    {
                        POS_Id = s.POS_Id,
                        POS_Code = s.POS_Code,
                        POS_MacNo = s.POS_MacNo,
                        Station_Code = s.Station_Code,
                        SlipMessage_Code = s.SlipMessage_Code,
                        SetEJ = s.SetEJ,
                        CusDisplay = s.CusDisplay,
                        CashDrawer = s.CashDrawer,
                        FormatPrint = s.FormatPrint,
                        Last_Inv_No = s.Last_Inv_No,
                        Last_TAX_No = s.Last_TAX_No,
                        Last_Refund_Inv_No = s.Last_Refund_Inv_No,
                        Active = s.Active,
                        IsActive = s.IsActive,
                        Create_By = s.Create_By,
                        Create_Date = s.Create_Date,
                        Update_By = s.Update_By,
                        Update_Date = s.Update_Date
                    }).ToList();
                return _item;
            }
        }

        public void EditPOS(int id, string code, string POS_MacNo, string Station_Code, string SlipMessage_Code
            , string SetEJ, string CusDisplay, string CashDrawer, string FormatPrint, string Active, string Update_By)
        {
            DateTime date = DateTime.Now;
            using (var context = new POSINVEntities())
            {
                var original = context.M_POS_Machine.Where(x => x.POS_Id == id).FirstOrDefault();
                context.M_POS_Machine.Attach(original);
                original.POS_Code = code;
                original.POS_MacNo = POS_MacNo;
                original.Station_Code = Station_Code;
                original.SlipMessage_Code = SlipMessage_Code;
                original.SetEJ = SetEJ;
                original.CusDisplay = CusDisplay;
                original.CashDrawer = CashDrawer;
                original.FormatPrint = FormatPrint;
                original.Active = Active;
                original.Update_By = Update_By;
                original.Update_Date = date;
               
                //var entry = context.Entry(original);
                //entry.State = EntityState.Modified;
                //entry.Property(e => e.POS_Code).IsModified = true;
                //entry.Property(e => e.POS_MacNo).IsModified = true;
                //entry.Property(e => e.Station_Code).IsModified = true;
                //entry.Property(e => e.SlipMessage_Code).IsModified = true;
                //entry.Property(e => e.SetEJ).IsModified = true;
                //entry.Property(e => e.CusDisplay).IsModified = true;
                //entry.Property(e => e.CashDrawer).IsModified = true;
                //entry.Property(e => e.FormatPrint).IsModified = true;
                //entry.Property(e => e.Active).IsModified = true;
                //entry.Property(e => e.Update_By).IsModified = true;
                //entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
            }
        }

        #region Customer Master...

        public List<M_Customer> GetCustomerList()
        {
            List<M_Customer> cuslist = new List<M_Customer>();
            using (var context = new POSINVEntities())
            {
                var res = context.M_Customer.Where(x=>x.IsActive=="Y").ToList<M_Customer>();
                cuslist = res;
            }
            return cuslist;
        }

        public M_Customer GetCustomerById(Guid key)
        {
            M_Customer result = new M_Customer();
            using (var context = new POSINVEntities())
            {
                var Cus = context.M_Customer.Where(m => m.CustomerID == key).FirstOrDefault();
                result.first_name = Cus.first_name;
                result.last_name = Cus.last_name;
                result.prfx_name = Cus.prfx_name;
                result.gndr_code = Cus.gndr_code;
                result.Addr_1 = Cus.Addr_1;
                result.Addr_2 = Cus.Addr_2;
                result.Addr_3 = Cus.Addr_3;
                result.Prvn_Code = Cus.Prvn_Code;
                result.Cntr_Code = Cus.Cntr_Code;
                result.Zip_Code = Cus.Zip_Code;
                result.Home_Phone = Cus.Home_Phone;
                result.Email = Cus.Email;
                result.remark = Cus.remark;
                result.crtd_dt = Cus.crtd_dt;
                result.crtd_by = Cus.crtd_by;
                result.last_upd_dt = Cus.last_upd_dt;
                result.last_upd_by = Cus.last_upd_by;
                result.Addr_4 = Cus.Addr_4;
                result.IsActive = Cus.IsActive;
                result.TaxID = Cus.TaxID;
                result.CustomerID = Cus.CustomerID;
                result.Addr_5 = Cus.Addr_5;
                result.Branch_No = Cus.Branch_No;
            }
            return result;
        }

        public List<M_Customer> GetCustomerListByCode(string key, string name)
        {
            List<M_Customer> result = new List<M_Customer>();
            using (var context = new POSINVEntities())
            {
                var res = context.M_Customer.Where(x => x.IsActive == "Y");

                //if (key != null && key != string.Empty)
                //{
                //    res = res.Where(m => m.cust_code.Contains(key));
                //}

                if (name != null && name != string.Empty)
                {
                    res = res.Where(m => m.first_name.Contains(name));
                }

                result = res.ToList();
                
            }
            return result;
        }

        public List<M_Customer> GetCustomerForSearch(string txt)
        {
            List<M_Customer> cuslist = new List<M_Customer>();
            using (var context = new POSINVEntities())
            {
                var res = context.M_Customer.Where(x => x.IsActive == "Y" && (x.first_name.Contains(txt) || x.TaxID.Contains(txt))).ToList<M_Customer>();
                cuslist = res;

                //var res2 = from cus in context.M_Customer
                //           join prov in context.M_Province on cus.Prvn_Code equals prov.ProvinceCode into _prov
                //           from l_prov in _prov.DefaultIfEmpty()
                //           where cus.IsActive == "Y" && l_prov.IsActive == "Y" && (cus.first_name.Contains(txt) || cus.TaxID.Contains(txt))
                //           select new { 
                //                    Customer = cus,
                //                    ProvinceName = l_prov.ProvinceName
                //           };

                //foreach (var item in res2)
                //{
                //    M_Customer cus = item.Customer;
                //    cus.ProvinceName = item.ProvinceName;
                //    cuslist.Add(cus);
                //}
            }



            return cuslist;
        }


        public MessageResponse InsertCustomer(M_Customer obj)
        {
            MessageResponse msg = new MessageResponse();
            try
            {
                var context = new POSINVEntities();
                var customer = new M_Customer();
                var res = context.M_Customer.Where(m => m.first_name == obj.first_name).FirstOrDefault();
                if (res == null)
                {
                    DeepCopyUtility.CopyObjectData(obj, customer);
                    context.M_Customer.Add(customer);
                    msg.ID = context.SaveChanges();
                    msg.IsSuccessfull = msg.ID > 0;
                    msg.Message = msg.ID > 0 ? "Save Data Successfull" : "Save Data Fail";
                }
                else
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = obj.first_name + " already exists";
                }                
            }
            catch (Exception ex)
            {
                msg.ID = 0;
                msg.IsSuccessfull = false;
                msg.Message = ex.Message;
            }
            return msg;
        }

        public MessageResponse EditCustomer(M_Customer obj)
        {
            MessageResponse msg = new MessageResponse();
            try
            {
                var context = new POSINVEntities();
                var customer = new M_Customer();
                var res = context.M_Customer.Where(m => m.CustomerID == obj.CustomerID).FirstOrDefault();
                if (res != null)
                {

                    DeepCopyUtility.CopyObjectData(obj, res);
                    var entry = context.Entry(res);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.Addr_1).IsModified = true;
                    entry.Property(e => e.Addr_2).IsModified = true;
                    entry.Property(e => e.Addr_3).IsModified = true;
                    entry.Property(e => e.Addr_4).IsModified = true;
                    entry.Property(e => e.Addr_5).IsModified = true;
                    entry.Property(e => e.Cntr_Code).IsModified = true;
                    entry.Property(e => e.Email).IsModified = true;
                    entry.Property(e => e.first_name).IsModified = true;
                    entry.Property(e => e.gndr_code).IsModified = true;
                    entry.Property(e => e.Zip_Code).IsModified = true;
                    entry.Property(e => e.Home_Phone).IsModified = true;
                    entry.Property(e => e.last_name).IsModified = true;
                    entry.Property(e => e.last_upd_by).IsModified = true;
                    entry.Property(e => e.last_upd_dt).IsModified = true;
                    entry.Property(e => e.prfx_name).IsModified = true;
                    entry.Property(e => e.Branch_No).IsModified = true;
                    entry.Property(e => e.Prvn_Code).IsModified = true;
                    entry.Property(e => e.remark).IsModified = true;
                    entry.Property(e => e.Zip_Code).IsModified = true;
                    msg.ID = context.SaveChanges();
                    msg.IsSuccessfull = msg.ID > 0;
                    msg.Message = msg.ID > 0 ? "Save Data Successfull." : "Save Data Fail.";
                }
                else
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = string.Format("ID : {0} data not found.", obj.CustomerID);
                }
            }
            catch (Exception ex)
            {
                msg.ID = 0;
                msg.IsSuccessfull = false;
                msg.Message = ex.Message;
            }
            return msg;
        }

        public MessageResponse DeleteCustomeer(Guid key)
        {
            MessageResponse msg = new MessageResponse();
            try
            {
                var context = new POSINVEntities();
                var customer = new M_Customer();
                var res = context.M_Customer.Where(m => m.CustomerID == key).FirstOrDefault();
                if (res != null)
                {
                    if (context.T_Invoice_Info.Count(x => x.CustomerID == res.CustomerID) > 0)
                    {
                        msg.IsSuccessfull = false;
                        msg.Message = strErrorDataUsed;
                        return msg;
                    }
                    res.IsActive = "N";
                    var entry = context.Entry(res);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    msg.ID = context.SaveChanges();
                    msg.IsSuccessfull = msg.ID > 0;
                    msg.Message = msg.ID > 0 ? "Inactive Data Successfull." : "Inactive Data Fail.";
                }
                else
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = string.Format("ID : {0} data not found.", key);
                }
            }
            catch (Exception ex)
            {
                msg.ID = 0;
                msg.IsSuccessfull = false;
                msg.Message = ex.Message;
            }
            return msg;
        }
        #endregion        

        #region Currency Master...
        public MessageResponse InsertCurrency(List<M_Currency> obj)
        {
            //throw new NotImplementedException();
            MessageResponse msg = new MessageResponse();
            if (obj != null)
            {
                if (obj.Count > 0)
                {
                    foreach (var item in obj)
                    {
                        using (var context = new POSINVEntities())
                        {
                            //var reslist = context.M_Currency.ToList();
                            //context.Entry(reslist).State = EntityState.Deleted;

                            try
                            {
                                M_Currency curr = new M_Currency();
                                DeepCopyUtility.CopyObjectData(item, curr);
                                context.M_Currency.Add(curr);
                                var res = context.SaveChanges();
                                msg.ID = res;
                                msg.IsSuccessfull = res > 0;
                                msg.Message = res > 0 ? "Insert Success." : "Insert Fail.";
                            }
                            catch (Exception ex)
                            {
                                msg.ID = 0;
                                msg.IsSuccessfull = false;
                                msg.Message = ex.Message;
                            }
                        }
                    }
                }
            }
            return msg;
        }
        public MessageResponse DeleteCurrency(int key)
        {
            //throw new NotImplementedException();
            MessageResponse msg = new MessageResponse();
            try
            {
                using (var context = new POSINVEntities())
                {
                    var reslist = context.M_Currency.Where(m => m.Currency_Id == key).FirstOrDefault();
                    reslist.IsActive = "N";
                    context.Entry(reslist).State = EntityState.Modified;
                    context.Entry(reslist).Property(e => e.IsActive).IsModified = true;
                    var res = context.SaveChanges();
                    msg.ID = res;
                    msg.IsSuccessfull = res > 0;
                    msg.Message = res > 0 ? "Initial Data Success." : "Initial Data Fail.";
                }
            }
            catch (Exception ex)
            {
                msg.ID = 0;
                msg.IsSuccessfull = false;
                msg.Message = ex.Message;
            }
            return msg;
        }
        public MessageResponse UpdateCurrency(M_Currency obj)
        {
            //throw new NotImplementedException();
            MessageResponse msg = new MessageResponse();
            try
            {
                using (var context = new POSINVEntities())
                {
                    var reslist = context.M_Currency.Where(m => m.Currency_Code == obj.Currency_Code).FirstOrDefault();
                    reslist.Currency_Name = obj.Currency_Name;
                    reslist.ExChangeRate = obj.ExChangeRate;
                    reslist.Update_By = obj.Update_By;
                    reslist.Update_Date = obj.Update_Date;
                    reslist.IsActive = obj.IsActive;
                    context.Entry(reslist).State = EntityState.Modified;
                    context.Entry(reslist).Property(m => m.Currency_Name).IsModified = true;
                    context.Entry(reslist).Property(m => m.ExChangeRate).IsModified = true;
                    context.Entry(reslist).Property(m => m.Update_By).IsModified = true;
                    context.Entry(reslist).Property(m => m.Update_Date).IsModified = true;
                    context.Entry(reslist).Property(m => m.IsActive).IsModified = true;
                    var res = context.SaveChanges();
                    msg.ID = res;
                    msg.IsSuccessfull = res > 0;
                    msg.Message = res > 0 ? "Initial Data Success." : "Initial Data Fail.";
                }
            }
            catch (Exception ex)
            {
                msg.ID = 0;
                msg.IsSuccessfull = false;
                msg.Message = ex.Message;
            }
            return msg;
        }
        public MessageResponse UpdateCurrencyList(List<M_Currency> obj)
        {
            //throw new NotImplementedException();
            MessageResponse msg = new MessageResponse();
            try
            {
                foreach (var item in obj)
                {
                    using (var context = new POSINVEntities())
                    {
                        var reslist = context.M_Currency.Where(m => m.Currency_Code == item.Currency_Code).FirstOrDefault();
                        reslist.Currency_Name = item.Currency_Name;
                        reslist.ExChangeRate = item.ExChangeRate;
                        reslist.Update_By = item.Update_By;
                        reslist.Update_Date = item.Update_Date;
                        reslist.IsActive = item.IsActive;
                        context.Entry(reslist).State = EntityState.Modified;
                        context.Entry(reslist).Property(m => m.Currency_Name).IsModified = true;
                        context.Entry(reslist).Property(m => m.ExChangeRate).IsModified = true;
                        context.Entry(reslist).Property(m => m.Update_By).IsModified = true;
                        context.Entry(reslist).Property(m => m.Update_Date).IsModified = true;
                        context.Entry(reslist).Property(m => m.IsActive).IsModified = true;
                        var res = context.SaveChanges();
                        msg.ID = res;
                        msg.IsSuccessfull = res > 0;
                        msg.Message = res > 0 ? "Initial Data Success." : "Initial Data Fail.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg.ID = 0;
                msg.IsSuccessfull = false;
                msg.Message = ex.Message;
            }
            return msg;
        }
        public MessageResponse SvaeCurrencyList(List<M_Currency> obj)
        {
            MessageResponse msg = new MessageResponse();
            if (obj.Count > 0)
            {
                try
                {
                    foreach (var item in obj)
                    {
                        using (var context = new POSINVEntities())
                        {
                            var reslist = context.M_Currency.Where(m => m.Currency_Code == item.Currency_Code).FirstOrDefault();
                            if (reslist != null)
                            {
                                if (reslist.Currency_Id > 0)
                                {
                                    msg = EditCurrency(item);
                                }
                                else
                                {
                                    msg = AddCurrency(item);
                                }
                            }
                            else
                            {
                                msg = AddCurrency(item);
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = ex.Message;
                }
            }
            return msg;
        }
        public MessageResponse AddCurrency(M_Currency obj)
        {
            MessageResponse msg = new MessageResponse();
            if (obj != null)
            {
                using (var context = new POSINVEntities())
                {
                    try
                    {
                        var reslist = context.M_Currency.Where(m => m.Currency_Code == obj.Currency_Code).FirstOrDefault();
                        //if (reslist == null)
                        //{
                            context.M_Currency.Add(obj);
                            var res = context.SaveChanges();
                            msg.ID = res;
                            msg.IsSuccessfull = res > 0;
                            msg.Message = res > 0 ? "Insert Success." : "Insert Fail.";
                        //}
                        //else
                        //{
                        //    msg.ID = 0;
                        //    msg.IsSuccessfull = false;
                        //    msg.Message = "Insert Fail. Data is existing.";
                        //}
                        
                    }
                    catch (Exception ex)
                    {
                        msg.ID = 0;
                        msg.IsSuccessfull = false;
                        msg.Message = ex.Message;
                    }
                }
            }
            return msg;
        }
        public MessageResponse EditCurrency(M_Currency obj)
        {
            MessageResponse msg = new MessageResponse();
            try
            {
                using (var context = new POSINVEntities())
                {
                    var reslist = context.M_Currency.Where(m => m.Currency_Id == obj.Currency_Id).FirstOrDefault();
                    reslist.Currency_Name = obj.Currency_Name;
                    reslist.ExChangeRate = obj.ExChangeRate;
                    reslist.Update_By = obj.Update_By;
                    reslist.Update_Date = obj.Update_Date;
                    reslist.IsActive = obj.IsActive;
                    context.Entry(reslist).State = EntityState.Modified;
                    context.Entry(reslist).Property(m => m.Currency_Name).IsModified = true;
                    context.Entry(reslist).Property(m => m.ExChangeRate).IsModified = true;
                    context.Entry(reslist).Property(m => m.Update_By).IsModified = true;
                    context.Entry(reslist).Property(m => m.Update_Date).IsModified = true;
                    context.Entry(reslist).Property(m => m.IsActive).IsModified = true;
                    var res = context.SaveChanges();
                    msg.ID = res;
                    msg.IsSuccessfull = res > 0;
                    msg.Message = res > 0 ? "Update Data Success." : "Update Data Fail.";
                }
            }
            catch (Exception ex)
            {
                msg.ID = 0;
                msg.IsSuccessfull = false;
                msg.Message = ex.Message;
            }
            return msg;
        }
        public List<M_Currency> GetCurrencyList()
        {
            List<M_Currency> objlist = new List<M_Currency>();
            try
            {
                using (var context = new POSINVEntities())
                {
                    var res = context.M_Currency.OrderByDescending(x=>x.Update_Date).Where(x=>x.IsActive=="Y").ToList<M_Currency>();
                    objlist = res;
                }
            }
            catch (Exception ex)
            {
                
            }
            return objlist;
        }
        public M_Currency GetCurrencyByCode(string key)
        {
            int _id = int.Parse(key);
            M_Currency result = new M_Currency();
            try
            {
                using (var context = new POSINVEntities())
                {
                    var res = context.M_Currency.OrderByDescending(x => x.Update_Date).Where(m => m.Currency_Id == _id).FirstOrDefault();
                    result = res;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public List<M_Currency> GetCurrencyListByCode(string key,DateTime? CurrencyDate)
        {
            List<M_Currency> objlist = new List<M_Currency>();
            try
            {
                using (var context = new POSINVEntities())
                {
                    var res = context.M_Currency.Where(m => m.Currency_Code.Contains(key) && m.IsActive == "Y");

                    if (CurrencyDate.HasValue)
                    {
                        var EndDate = CurrencyDate.Value.AddDays(1).AddSeconds(-1);
                        res = res.Where(x => x.Update_Date.Value >= CurrencyDate.Value && x.Update_Date.Value <= EndDate);
                    }
                    objlist = res.OrderByDescending(x => x.Update_Date).ToList<M_Currency>();
                }
            }
            catch (Exception ex)
            {

            }
            return objlist;
        }
        #endregion

        #region Report...
        private string connectionstring = ConfigurationManager.ConnectionStrings["POSINVConnectionString"].ToString();
        public IEnumerable<RptMacPosModel> GetRptMachinePos()
        {
            string sql =@"SELECT DISTINCT R.Paydate as PAYMENT_DATE, 
                M.POS_MacNo as MAC_NO, 
                count(M.POS_MacNo) as BILL_COUNT,
                sum(R.AmtBill) as AMT_NETBALANCE, 
                sum(R.BVat0) as AMOUNT_B0, 
                sum(R.BVat7) as AMOUNT_B7, 
                sum(R.AVat7) as VAT, 
                sum(R.AirPay) as FEE, 
                sum(R.PayPrice) as AMT_PAY_INSTEAD, 
                sum(R.Dec_Inc) as AMT_DISCOUNT
                FROM   M_Station S RIGHT OUTER JOIN
                M_POS_Machine M ON S.Station_Code = M.POS_Code RIGHT OUTER JOIN
                rpt R ON M.POS_Id = R.POS_Id
                GROUP BY R.Paydate,M.POS_MacNo";
            IEnumerable<RptMacPosModel> res = Enumerable.Empty<RptMacPosModel>();
            using (var con = new SqlConnection(connectionstring))
            {
                try
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //var res = dt.AsEnumerable().ToList();
                    res = dt != null ? dt.DataTableToList<RptMacPosModel>() : Enumerable.Empty<RptMacPosModel>();
                }
                catch (Exception ex)
                {                    
                    //throw;
                }
            }
            return res;
        }

        public DataReports GetRptMachinePosDs(SqlCommand _SqlCommand, string DataReportTableName)
        {
            DataReports res = new DataReports();
            using (var con = new SqlConnection(connectionstring))
            {
                try
                {
                    con.Open();
                    DataTable _DataTable = FunctionAndClass.IexcuteDataByDataTable(_SqlCommand);
                    //var command = new SqlCommand(sql, con);
                    var adapter = new SqlDataAdapter(_SqlCommand);
                    using (DataReports ds = new DataReports())
                    {
                        adapter.Fill(ds, DataReportTableName);
                        return res = ds;
                    }
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
            return res;
        }
        #endregion

        #region Station Master...
        public static List<M_Station> GetStationListForReport()
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Station.Where(x => x.Active == "Y" && x.IsActive == "Y").ToList();
                return _item;
            }
        }
        public static List<M_Station> GetStationList()
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Station.Where(x => x.Active=="Y" && x.IsActive == "Y" && x.Station_Code != "System").ToList();
                return _item;
            }
        }

        public static M_Station GetStationByCode(string StationCode)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Station.FirstOrDefault(x => x.Active == "Y" && x.IsActive == "Y" && x.Station_Code == StationCode);
                return _item;
            }
        }
        #endregion

//        public static List<FeeDetail> GetFeeDetail(string pnrno)
//        {
//            using (var context = new POSINVEntities())
//            {
//                var _item = context.Database.SqlQuery<FeeDetail>(@"select * from 
//                            (select 'T_BookingServiceChargeCurrentUpdate' as TableName, tb.Header, tb.ChargeCode, tb.qty,tb.RowID,tb.PassengerId,tb.FirstName,tb.LastName , tb.ChargeDetail,tb.ChargeType,tb.TicketCode,tb.CollectType, tb.CurrencyCode, tb.Amount, tb.ForeignCurrencyCode, tb.ForeignAmount , tb.DiscountOri, tb.Exc, tb.price, Discount, (tb.price * tb.qty) - (Discount * tb.qty) amt, AbbGroup, TaxInvoiceNo, sort from(
//                            select (select FirstName + ' ' + LastName from [dbo].T_PassengerCurrentUpdate where PassengerId = T_BookingServiceChargeCurrentUpdate.PassengerId and TransactionId = [T_BookingCurrentUpdate].TransactionId) as Header, e.TaxInvoiceNo, [M_FlightFee].[AbbGroup], T_BookingServiceChargeCurrentUpdate.[PaxFareTId], 
//
//                            case when T_BookingServiceChargeCurrentUpdate.ChargeType = 'FarePrice' then 'Base Fare'
//	                            when T_BookingServiceChargeCurrentUpdate.ChargeType != 'FarePrice' then T_BookingServiceChargeCurrentUpdate.ChargeCode
//	                            end ChargeCode, 
//                            1 qty
//	                            , T_BookingServiceChargeCurrentUpdate.Amount * case when T_BookingCurrentUpdate.CurrencyCode <> 'THB' then isnull(e.ExchangeRateTH,
//                                    (select top 1 isnull(mc.ExChangeRate,1) 
//	                            from M_Currency mc 
//	                            where mc.Currency_Code = T_BookingCurrentUpdate.CurrencyCode and mc.IsActive = 'Y' and CONVERT(varchar(8),T_BookingCurrentUpdate.Booking_Date,112) = CONVERT(varchar(8), mc.Create_Date,112)
//	                            order by mc.Create_Date desc)) else 1 end as price
//	                            , isnull((select sum(d.Amount) 
//							     from [dbo].[T_BookingCurrentUpdate] a
//                            join dbo.T_SegmentCurrentUpdate b on a.TransactionId = b.TransactionId
//                            join [dbo].T_PaxFareCurrentUpdate c on b.SegmentTId = c.SegmentTId 
//                            join [dbo].T_BookingServiceChargeCurrentUpdate d on c.PaxFareTId = d.PaxFareTId
//                            left join [dbo].[M_FlightFee] on d.ChargeCode = [M_FlightFee].Flight_Fee_Code and [M_FlightFee].IsActive = 'Y'
//                            and ([M_FlightFee].[AbbGroup] <> '' or [M_FlightFee].[AbbGroup] is not null)
//                            where a.PNR_No = '" + pnrno + @"'  and (d.ChargeType = 'Discount'  or d.ChargeType = 'PromotionDiscount')
//                            and d.ChargeCode = T_BookingServiceChargeCurrentUpdate.ChargeCode
//                            and d.PaxFareTId = T_BookingServiceChargeCurrentUpdate.PaxFareTId),0) * isnull(e.ExchangeRateTH,
//                                (select top 1 isnull(mc.ExChangeRate,1) 
//	                            from M_Currency mc 
//	                            where mc.Currency_Code = T_BookingCurrentUpdate.CurrencyCode and mc.IsActive = 'Y' and CONVERT(varchar(8),T_BookingCurrentUpdate.Booking_Date,112) = CONVERT(varchar(8), mc.Create_Date,112)
//	                            order by mc.Create_Date desc)) Discount
//                            , T_BookingServiceChargeCurrentUpdate.CurrencyCode, T_BookingServiceChargeCurrentUpdate.Amount
//	                            , isnull((select sum(d.Amount)  
//						    from [dbo].[T_BookingCurrentUpdate] a
//                            join T_SegmentCurrentUpdate b on a.TransactionId = b.TransactionId
//                            join T_PaxFareCurrentUpdate c on b.SegmentTId = c.SegmentTId 
//                            join T_BookingServiceChargeCurrentUpdate d on c.PaxFareTId = d.PaxFareTId
//                            left join [dbo].[M_FlightFee] on d.ChargeCode = [M_FlightFee].Flight_Fee_Code and [M_FlightFee].IsActive = 'Y'
//                            and ([M_FlightFee].[AbbGroup] <> '' or [M_FlightFee].[AbbGroup] is not null)
//                            where a.PNR_No = '" + pnrno + @"' and (d.ChargeType = 'Discount' or d.ChargeType = 'PromotionDiscount')
//                            and d.ChargeCode = T_BookingServiceChargeCurrentUpdate.ChargeCode
//                            and d.PaxFareTId = T_BookingServiceChargeCurrentUpdate.PaxFareTId),0) DiscountOri
//                            , isnull(e.ExchangeRateTH,
//                                    (select top 1 isnull(mc.ExChangeRate,1) 
//	                            from M_Currency mc 
//	                            where mc.Currency_Code = T_BookingCurrentUpdate.CurrencyCode and mc.IsActive = 'Y' and CONVERT(varchar(8),T_BookingCurrentUpdate.Booking_Date,112) = CONVERT(varchar(8), mc.Create_Date,112)
//	                            order by mc.Create_Date desc)) Exc
//                            , case when T_BookingServiceChargeCurrentUpdate.ChargeType = 'FarePrice' then 1 
//                              else 2 end sort , T_BookingServiceChargeCurrentUpdate.BookingServiceChargeTId As RowID , T_BookingServiceChargeCurrentUpdate.ChargeDetail , T_BookingServiceChargeCurrentUpdate.ChargeType, T_BookingServiceChargeCurrentUpdate.TicketCode,T_BookingServiceChargeCurrentUpdate.CollectType ,T_BookingServiceChargeCurrentUpdate.ForeignCurrencyCode , T_BookingServiceChargeCurrentUpdate.ForeignAmount , T_BookingServiceChargeCurrentUpdate.PassengerId ,T_PassengerCurrentUpdate.FirstName,T_PassengerCurrentUpdate.LastName
//                            from [dbo].[T_BookingCurrentUpdate] 
//                            join dbo.T_SegmentCurrentUpdate on [T_BookingCurrentUpdate].TransactionId = T_SegmentCurrentUpdate.TransactionId
//                            join [dbo].T_PaxFareCurrentUpdate on [dbo].T_SegmentCurrentUpdate.SegmentTId = [dbo].T_PaxFareCurrentUpdate.SegmentTId 
//                            join [dbo].T_BookingServiceChargeCurrentUpdate on T_PaxFareCurrentUpdate.PaxFareTId = T_BookingServiceChargeCurrentUpdate.PaxFareTId
//                            left join [dbo].[M_FlightFee] on T_BookingServiceChargeCurrentUpdate.ChargeCode = [M_FlightFee].Flight_Fee_Code and [M_FlightFee].IsActive = 'Y'  and ([M_FlightFee].[AbbGroup] <> '' or [M_FlightFee].[AbbGroup] is not null)
//                            left join dbo.T_ABB e on T_BookingServiceChargeCurrentUpdate.ABBNo = e.TaxInvoiceNo
//                            left Join T_PassengerCurrentUpdate ON T_PassengerCurrentUpdate.PassengerId = T_BookingServiceChargeCurrentUpdate.PassengerId
//                            where [T_BookingCurrentUpdate].PNR_No = '" + pnrno + @"'  and (T_BookingServiceChargeCurrentUpdate.ChargeType != 'Discount'  and T_BookingServiceChargeCurrentUpdate.ChargeType != 'PromotionDiscount')) tb
//						    
//                            union all
//                            select 'T_PassengerFeeServiceChargeCurrentUpdate' as TableName, Header,  ChargeCode, 1 qty, tb.RowID, tb.PassengerId,tb.FirstName, tb.LastName  , tb.ChargeDetail,tb.ChargeType , tb.TicketCode,tb.CollectType, tb.CurrencyCode, tb.Amount,tb.ForeignCurrencyCode,tb.ForeignAmount , tb.DiscountOri, tb.Exc, price, Discount, price - Discount amt, AbbGroup, TaxInvoiceNo, sort from (
//                            select b.FirstName + ' ' + b.LastName as Header,b.FirstName, b.LastName , e.TaxInvoiceNo, [M_FlightFee].[AbbGroup], PassengerFeeServiceChargeTId as RowID, d.PassengerId , c.PassengerFeeNo, d.ChargeDetail, d.ChargeType , d.TicketCode,d.CollectType ,  d.ChargeCode, d.ForeignCurrencyCode, d.ForeignAmount , d.CurrencyCode, d.Amount *  case when d.CurrencyCode <> 'THB' then isnull(e.ExchangeRateTH,
//                            (select top 1 isnull(mc.ExChangeRate,1) 
//	                            from M_Currency mc 
//	                            where mc.Currency_Code = a.CurrencyCode and mc.IsActive = 'Y'   and CONVERT(varchar(8),c.FeeCreateDate,112) = CONVERT(varchar(8), mc.Create_Date,112)
//	                            order by mc.Create_Date desc)) else 1 end price
//
//                            , isnull((select sum(Amount) 
//                            from [dbo].[T_BookingCurrentUpdate] 
//                            join T_PassengerCurrentUpdate  on [T_BookingCurrentUpdate].TransactionId = T_PassengerCurrentUpdate.TransactionId
//                            join [T_PassengerFeeCurrentUpdate]  on T_PassengerCurrentUpdate.PassengerTId = [T_PassengerFeeCurrentUpdate].PassengerTId
//                            join [T_PassengerFeeServiceChargeCurrentUpdate]  on [T_PassengerFeeCurrentUpdate].PassengerFeeTId = [T_PassengerFeeServiceChargeCurrentUpdate].PassengerFeeTId
//                            left join [dbo].[M_FlightFee]  on [T_PassengerFeeServiceChargeCurrentUpdate].ChargeCode = [M_FlightFee].Flight_Fee_Code and [M_FlightFee].IsActive = 'Y' and ([M_FlightFee].[AbbGroup] <> '' or [M_FlightFee].[AbbGroup] is not null)
//                            where [T_BookingCurrentUpdate].PNR_No = '" + pnrno + @"'  and ([T_PassengerFeeServiceChargeCurrentUpdate].ChargeType = 'Discount'  or [T_PassengerFeeServiceChargeCurrentUpdate].ChargeType = 'PromotionDiscount')
//                            and [T_PassengerFeeServiceChargeCurrentUpdate].ChargeCode = d.ChargeCode
//                            and [T_PassengerFeeServiceChargeCurrentUpdate].PassengerFeeNo = d.PassengerFeeNo),0) * isnull(e.ExchangeRateTH,
//                                (select top 1 isnull(mc.ExChangeRate,1) 
//	                            from M_Currency mc 
//	                            where mc.Currency_Code = a.CurrencyCode and mc.IsActive = 'Y'  and CONVERT(varchar(8),c.FeeCreateDate,112) = CONVERT(varchar(8), mc.Create_Date,112)
//	                            order by mc.Create_Date desc)) Discount
//                            , d.Amount
//                             , isnull((select sum(Amount) from [dbo].[T_BookingCurrentUpdate] 
//                            join [dbo].T_PassengerCurrentUpdate  on [T_BookingCurrentUpdate].TransactionId = T_PassengerCurrentUpdate.TransactionId
//                            join [dbo].[T_PassengerFeeCurrentUpdate]  on T_PassengerCurrentUpdate.PassengerTId = [T_PassengerFeeCurrentUpdate].PassengerTId
//                            join [dbo].[T_PassengerFeeServiceChargeCurrentUpdate]  on [T_PassengerFeeCurrentUpdate].PassengerFeeTId = [T_PassengerFeeServiceChargeCurrentUpdate].PassengerFeeTId
//                            left join [dbo].[M_FlightFee]  on [T_PassengerFeeServiceChargeCurrentUpdate].ChargeCode = [M_FlightFee].Flight_Fee_Code and [M_FlightFee].IsActive = 'Y' and ([M_FlightFee].[AbbGroup] <> '' or [M_FlightFee].[AbbGroup] is not null)
//                            where [T_BookingCurrentUpdate].PNR_No = '" + pnrno + @"'  and ([T_PassengerFeeServiceChargeCurrentUpdate].ChargeType = 'Discount'  or [T_PassengerFeeServiceChargeCurrentUpdate].ChargeType = 'PromotionDiscount')
//                            and [T_PassengerFeeServiceChargeCurrentUpdate].ChargeCode = d.ChargeCode
//                            and [T_PassengerFeeServiceChargeCurrentUpdate].PassengerFeeNo = d.PassengerFeeNo),0) DiscountOri
//                            ,isnull(e.ExchangeRateTH,
//                            (select top 1 isnull(mc.ExChangeRate,1) 
//	                            from M_Currency mc 
//	                            where mc.Currency_Code = a.CurrencyCode and mc.IsActive = 'Y'  and CONVERT(varchar(8),c.FeeCreateDate,112) = CONVERT(varchar(8), mc.Create_Date,112)
//	                            order by mc.Create_Date desc)) Exc
//                            , 3 sort
//                            from [dbo].[T_BookingCurrentUpdate] a
//                            join [dbo].T_PassengerCurrentUpdate b on a.TransactionId = b.TransactionId
//                            join [dbo].[T_PassengerFeeCurrentUpdate] c on b.PassengerTId = c.PassengerTId
//                            join [dbo].[T_PassengerFeeServiceChargeCurrentUpdate] d on c.PassengerFeeTId = d.PassengerFeeTId
//                            left join [dbo].[M_FlightFee] on d.ChargeCode = [M_FlightFee].Flight_Fee_Code and [M_FlightFee].IsActive = 'Y'
//                            and ([M_FlightFee].[AbbGroup] <> '' or [M_FlightFee].[AbbGroup] is not null)
//                            left join dbo.T_ABB e on d.ABBNo = e.TaxInvoiceNo
//                            where a.PNR_No = '" + pnrno + @"' 
//                            and (d.ChargeType != 'Discount' 
//                            and d.ChargeType != 'PromotionDiscount')) tb) h 
//                            order by h.ChargeDetail, h.ChargeCode
//                            ").ToList();
//                // order by h.sort, h.Header, h.ChargeCode
//                int i = 0;
//                _item.ForEach(it => it.RowNo = i++);
//                return _item;
//            }
//        }

        public static List<FeeDetail> GetFareDetail(string pnrno)
        {
            List<FeeDetail> data = new List<FeeDetail>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DECLARE @BookingNo varchar(50) = '" + pnrno + "' ");
            sb.AppendLine("DECLARE @ExchangeRateTH decimal(18, 8) = (SELECT TOP 1 ExchangeRateTH FROM T_ABB WHERE PNR_No = @BookingNo)");
            sb.AppendLine("SELECT FF.AbbGroup,pax.PaxType, @ExchangeRateTH AS Exc ");
            sb.AppendLine(",convert(varchar(38), seg.SegmentTId) AS  SegmentTId ");
            sb.AppendLine(",serv.SeqmentNo,serv.SeqmentPaxNo,serv.ServiceChargeNo,serv.PassengerId,serv.ChargeType,serv.CollectType,serv.ChargeCode,serv.TicketCode,serv.CurrencyCode ,serv.FareCreateDate ");
            sb.AppendLine(",serv.Amount,serv.BaseAmount,serv.ChargeDetail,serv.ForeignCurrencyCode,serv.ForeignAmount,serv.ABBNo AS TaxInvoiceNo,serv.FareCreateDate,serv.SEQ,serv.QTY ");
            sb.AppendLine(",book.Booking_Date,serv.BookingServiceChargeTId AS RowID,convert(varchar(38), pax.PaxFareTId) AS  PaxFareTId ");            
            //sb.Append(",(SELECT COUNT(*) FROM T_PassengerCurrentUpdate pass WHERE book.TransactionId = pass.TransactionId AND pax.PaxType = pass.PaxType) AS QTY ");
            sb.AppendLine("FROM T_BookingCurrentUpdate book ");
            sb.AppendLine("JOIN T_SegmentCurrentUpdate seg ON seg.TransactionId = book.TransactionId ");
            sb.AppendLine("JOIN T_PaxFareCurrentUpdate pax ON seg.SegmentTId = pax.SegmentTId ");
            sb.AppendLine("JOIN T_BookingServiceChargeCurrentUpdate serv ON serv.PaxFareTId = pax.PaxFareTId ");
            sb.AppendLine("LEFT JOIN M_FlightFee FF ON FF.Flight_Fee_Code = serv.ChargeCode   ");
            sb.AppendLine("WHERE PNR_No = @BookingNo AND serv.Action >= 0 ");
            sb.AppendLine("ORDER BY seg.STD,pax.PaxType,serv.ChargeCode ");

            using (var context = new POSINVEntities())
            {
                data = context.Database.SqlQuery<FeeDetail>(sb.ToString()).ToList();
                int i = 0;
                //data.ForEach(it => it.RowNo = i++);




                decimal ExchangeRateTH = 1;
                foreach (FeeDetail item in data)
                {
                    if (item.ChargeType.ToLower() == "discount" || item.ChargeType.ToLower() == "promotiondiscount")
                    {
                        item.Discount = item.Amount;
                        item.Amount = item.Amount * -1;
                    }
                    item.RowNo = i++;

                    if (item.CurrencyCode == "THB")
                    {
                        item.AmountTHB = item.Amount ?? 0;
                        item.Exc = 1;
                    }
                    else
                    {
                        if (item.Exc == null || string.IsNullOrEmpty(item.Exc.ToString()))
                        {
                            ExchangeRateTH = MasterDA.GetCurrencyActiveDate(item.CurrencyCode, item.Booking_Date);
                            item.AmountTHB = ExchangeRateTH * item.Amount ?? 0;
                            item.Exc = ExchangeRateTH;
                        }
                        else
                        {
                            item.AmountTHB = item.Exc * item.Amount ?? 0;
                        }
                    }

                    item.TotalAmountTHB = item.AmountTHB * item.qty;
                }

            }
            return data;
        }

        static public List<FeeDetail> GetFeeDetail(string BookingNo)
        {
            try
            {
                using (var context = new POSINVEntities())
                {

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("DECLARE @BookingNo varchar(50) = '" + BookingNo + "' ");
                    sb.AppendLine("DECLARE @ExchangeRateTH decimal(18, 8) = (SELECT TOP 1 ExchangeRateTH FROM T_ABB WHERE PNR_No = @BookingNo)");
                    sb.AppendLine("SELECT pass_fee_sv.PassengerFeeServiceChargeTId AS RowID,pass_fee_sv.ServiceChargeNo AS ServiceChargeNo ,	pass_fee_sv.ChargeType,pass_fee_sv.ChargeCode,pass_fee_sv.TicketCode, ");
                    sb.AppendLine("pass_fee_sv.CollectType,pass_fee_sv.ChargeDetail,pass_fee_sv.ABBNo AS TaxInvoiceNo ,  pass_fee_sv.CurrencyCode, pass_fee_sv.Amount, pass_fee_sv.FeeCreateDate, ");
                    sb.AppendLine("pass_fee_sv.ForeignCurrencyCode, pass_fee_sv.ForeignAmount, 1 AS Qty ,");
                    sb.AppendLine("book.Booking_Date, ");
                    sb.AppendLine("'' AS SegmentTId,'' AS SeqmentNo,");
                    sb.AppendLine("pass.PassengerId,pass.FirstName,pass.LastName,");
                    sb.AppendLine("@ExchangeRateTH as Exc, ");
                    sb.AppendLine("FF.AbbGroup, ");
                    sb.AppendLine("'T_PassengerFeeServiceChargeCurrentUpdate' AS TableName ");
                    sb.AppendLine("FROM T_PassengerFeeServiceChargeCurrentUpdate pass_fee_sv ");
                    sb.AppendLine("JOIN T_PassengerFeeCurrentUpdate pass_fee  ON pass_fee.PassengerFeeTId = pass_fee_sv.PassengerFeeTId   ");
                    sb.AppendLine("JOIN T_PassengerCurrentUpdate pass ON  pass.PassengerTId = pass_fee.PassengerTId  ");
                    sb.AppendLine("JOIN T_BookingCurrentUpdate book ON book.TransactionId = pass.TransactionId ");
                    sb.AppendLine("LEFT JOIN M_FlightFee FF ON FF.Flight_Fee_Code = pass_fee_sv.ChargeCode ");
                    sb.AppendLine("WHERE book.PNR_No =  @BookingNo AND pass_fee_sv.Action >= 0");
                    sb.AppendLine("ORDER BY FirstName,ChargeDetail ");
                    var _item = context.Database.SqlQuery<FeeDetail>(sb.ToString()).ToList();

                    int i = 0;
                    decimal ExchangeRateTH = 1;
                    foreach (FeeDetail item in _item)
                    {
                        if (item.ChargeType.ToLower() == "discount" || item.ChargeType.ToLower() == "promotiondiscount")
                        {
                            item.Discount = item.Amount;
                            item.Amount = item.Amount * -1;
                        }
                        item.RowNo = i++;

                        if (item.CurrencyCode == "THB")
                        {
                            item.AmountTHB = (item.Amount ?? 0);
                            item.Exc = 1;
                        }
                        else
                        {
                            if (item.Exc == null || string.IsNullOrEmpty(item.Exc.ToString()))
                            {
                                ExchangeRateTH = MasterDA.GetCurrencyActiveDate(item.CurrencyCode, item.Booking_Date);
                                item.AmountTHB = ExchangeRateTH * (item.Amount ?? 0);
                                item.Exc = ExchangeRateTH;
                            }
                            else
                            {
                                item.AmountTHB = (item.Exc ?? 1) * (item.Amount ?? 0);
                            }
                        }
                    }

                    return _item;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public List<T_PaxFareCurrentUpdate> GetPaxFareBySegmentId(Guid SegmentTId)
        {
            using (var context = new POSINVEntities())
            {
                return context.T_PaxFareCurrentUpdate.Where(x => x.SegmentTId == SegmentTId).OrderBy(c => c.PaxType).ToList();
            }
        }

        public static bool ValidNullABB(string pnrno)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.Database.SqlQuery<string>(@"
                                select ABBNo from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"' and ABBNo is null
                                union
                                select ABBNo from [dbo].[T_PassengerCurrentUpdate]
                                where ABBNo is null and TransactionId in (
                                select TransactionId from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"')
                                union
                                select ABBNo from [dbo].[T_PassengerFeeCurrentUpdate]
                                where ABBNo is null and PassengerTId in(
                                select PassengerTId from [dbo].[T_PassengerCurrentUpdate]
                                where TransactionId in (
                                select TransactionId from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"'))
                                union
                                select ABBNo from [dbo].[T_PassengerFeeServiceChargeCurrentUpdate]
                                where ABBNo is null and PassengerFeeTId in(
                                select PassengerFeeTId from [dbo].[T_PassengerFeeCurrentUpdate]
                                where PassengerTId in(
                                select PassengerTId from [dbo].[T_PassengerCurrentUpdate]
                                where TransactionId in (
                                select TransactionId from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"')))
                                union
                                select ABBNo from [dbo].T_SegmentCurrentUpdate
                                where ABBNo is null and TransactionId in (
                                select TransactionId from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"')
                                union
                                select ABBNo from [dbo].T_PaxFareCurrentUpdate
                                where ABBNo is null and SegmentTId in(
                                select SegmentTId from [dbo].T_SegmentCurrentUpdate
                                where TransactionId in (
                                select TransactionId from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"'))
                                union
                                select ABBNo from [dbo].T_BookingServiceChargeCurrentUpdate
                                where ABBNo is null and PaxFareTId in(
                                select PaxFareTId from [dbo].T_PaxFareCurrentUpdate
                                where SegmentTId in(
                                select SegmentTId from [dbo].T_SegmentCurrentUpdate
                                where TransactionId in (
                                select TransactionId from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"')))
                                union
                                select ABBNo from [dbo].T_PaymentCurrentUpdate
                                where ABBNo is null and TransactionId in (
                                select TransactionId from [dbo].[T_BookingCurrentUpdate] where PNR_No = '" + pnrno + @"')").ToList();
                if (_item.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<T_SegmentCurrentUpdate> GetFlightList(Guid key)
        {
            //var found = GetBooking(key);
            if (key != null)
            {
                using (var context = new POSINVEntities())
                {
                    var res = context.T_SegmentCurrentUpdate.OrderBy(s => s.STD).Where(m => m.TransactionId == key && m.Action == 0).ToList();
                    int i =0;
                    res.ForEach(it => it.RowNo = i++);

                    return res;
                }
            }
            return null;
        }

        static public List<T_PaxFareCurrentUpdate> GetPaxList(Guid key)
        {
            if (key != null)
            {
                using (var context = new POSINVEntities())
                {
                    var res = context.T_PaxFareCurrentUpdate.Where(s => s.SegmentTId == key).ToList();
                    return res;
                }
            }
            return null;
        }

        static public T_PaxFareCurrentUpdate PaxFareInsert(T_PaxFareCurrentUpdate data)
        {
            try
            {
                using (var context = new POSINVEntities())
                {

                    data.Create_Date = DateTime.Now;
                    data.Update_Date = DateTime.Now;
                    context.T_PaxFareCurrentUpdate.Add(data);
                    context.SaveChanges();
                    return data;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return null;
        }

        public List<T_PassengerCurrentUpdate> GetPassengerList(Guid key)
        {
            if (key != null)
            {
                using (var context = new POSINVEntities())
                {
                    var res = context.T_PassengerCurrentUpdate.OrderBy(s => s.PassengerId).Where(m => m.TransactionId == key  && m.Action == 0).ToList();
                    int i = 1;
                    res.ForEach(it => it.RowNo = i++);

                    return res;
                }
            }
            return null;            
        }

        public List<T_PaymentCurrentUpdate> GetPaymentList(Guid key, string pnrno)
        {
            if (key != null)
            {
                using (var context = new POSINVEntities())
                {
                    var res = context.T_PaymentCurrentUpdate.OrderBy(s => s.ApprovalDate).Where(m => m.TransactionId == key && m.Status.ToUpper() == "APPROVED").ToList();
                    foreach (T_PaymentCurrentUpdate itm in res)
                    {
                        if (itm.CurrencyCode != "THB")
                        {
                            var tabb = context.T_ABB.OrderByDescending(f => f.Create_Date).FirstOrDefault(f => f.PNR_No == pnrno && f.STATUS_INV != "CANCEL");

                            if (tabb != null)
                            {
                                itm.PaymentAmountTHB = itm.PaymentAmount * tabb.ExchangeRateTH;
                            }
                            else
                            {
                                T_BookingCurrentUpdate b = GetBooking(pnrno);
                                if (b != null)
                                {
                                    decimal ExchangeRateTH = MasterDA.GetCurrencyActiveDate(itm.CurrencyCode, b.Booking_Date);
                                    itm.PaymentAmountTHB = itm.PaymentAmount * ExchangeRateTH;
                                }
                            }
                        }
                        else
                        {
                            itm.PaymentAmountTHB = itm.PaymentAmount;
                        }
                    }
                    return res;
                }
            }
            return null;
        }

        public List<T_ABB> GetABBList(string key)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.T_ABB.OrderBy(s => s.Create_Date).ThenBy(x => x.TaxInvoiceNo).Where(f => f.PNR_No == key && f.STATUS_INV == "ACTIVE").ToList();
                if (res.Count() > 0)
                {
                    for (int i = 0; i < res.Count; i++)
                    {
                        if (res[i].T_Invoice_Info != null)
                        {
                            string dd = res[i].T_Invoice_Info.INV_No; // ห้ามลบ
                        }
                    }
                }
                return res;
            }
        }

        public string GetServiceFee()
        {
            return null;
        }

        public T_BookingCurrentUpdate GetBooking(string key)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var res = context.T_BookingCurrentUpdate.Where(m => m.PNR_No == key).FirstOrDefault();
                    if (res != null)
                    {
                        return res;
                    }
                    else
                    {
                        return new T_BookingCurrentUpdate();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static UserModel GetUserDetail(string key, string pos)
        {

            try
            {
                using (var context = new POSINVEntities())
                {
                    string sql = @"SELECT M_Employee.UserID, M_Employee.UserCode, M_Employee.FirstName, 
                        M_Employee.LastName, M_Employee.UserGroup, M_POS_Machine.POS_Id, 
                        M_POS_Machine.POS_Code, M_POS_Machine.POS_MacNo, M_POS_Machine.SlipMessage_Code, 
                        M_Employee.Station_Code AS Station_ID, 
                        M_POS_Machine.Station_Code AS Station_Code
                        FROM M_Employee
						join [dbo].[M_EmpPOS] on M_Employee.UserID = [M_EmpPOS].UserID
                        join M_POS_Machine ON [M_EmpPOS].PID = M_POS_Machine.POS_Id 
                        WHERE M_Employee.UserCode='" + key + @"' and M_POS_Machine.POS_Code = '" + pos + @"'
						and M_Employee.IsActive = 'Y' and M_Employee.Active = 'Y'
                        and M_POS_Machine.IsActive = 'Y' and M_POS_Machine.Active = 'Y'";
                    var res = context.Database.SqlQuery<UserModel>(sql).FirstOrDefault();
                    return res;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
            //return null;
        }

        public enum SlipType
        {
            ABB = 0,
            Invoice = 1,
            CN = 2
        }
        public static FormPrintModel GetMessageList(SlipType _SlipType, bool IsHeader, string pos)
        {
            var Output = new FormPrintModel(); //List<MessageModel>();
            using (var context = new POSINVEntities())
            {
                var Pos = context.M_POS_Machine.Where(x => x.POS_Code == pos).FirstOrDefault();
                if (Pos != null)
                {
                    var Station = context.M_Station.Where(x => x.Station_Code == Pos.Station_Code).FirstOrDefault();
                    if (Station != null)
                    {
                        IEnumerable<M_SlipMessageDetails> SlipMessageDetails = null;
                        
                        if (_SlipType == SlipType.ABB)
                        {
                            Output.IsA4 = Station.ABB_Print_A4 ?? false;
                            SlipMessageDetails = context.M_SlipMessageDetails.Where(x => x.SlipMessage_Id == Station.ABB_SlipMessage_Id);
                        }
                        else if (_SlipType == SlipType.Invoice)
                        {
                            Output.IsA4 = Station.INV_Print_A4 ?? true;
                            SlipMessageDetails = context.M_SlipMessageDetails.Where(x => x.SlipMessage_Id == Station.INV_SlipMessage_Id);
                        }
                        else
                        {
                            Output.IsA4 = Station.CN_Print_A4 ?? true;
                            SlipMessageDetails = context.M_SlipMessageDetails.Where(x => x.SlipMessage_Id == Station.CN_SlipMessage_Id);
                        }

                        Output.MessageModels = SlipMessageDetails.Where(x => x.SlipStat == IsHeader).OrderBy(c => c.SortID).Select(s => new MessageModel()
                        {
                            POS_Id = Pos.POS_Id,
                            POS_Code= Pos.POS_Code,
                            POS_MacNo = Pos.POS_MacNo,
                            Station_Code =Station.Station_Code,
                            SlipMessage_Code = "",
                            Descriptions = s.Descriptions,
                            SortID = s.SortID.GetValueOrDefault(),
                            SlipStat = s.SlipStat.GetValueOrDefault()
                        }).ToList();
                    }
                    
                }

                return Output;

//                string sql = @"SELECT M_POS_Machine.POS_Id, 
//                        M_POS_Machine.POS_Code, 
//                        M_POS_Machine.POS_MacNo, 
//                        M_POS_Machine.Station_Code, 
//                        M_SlipMessage.SlipMessage_Code, 
//                        M_SlipMessageDetails.Descriptions, 
//                        M_SlipMessageDetails.SortID, 
//                        M_SlipMessageDetails.SlipStat
//                        FROM M_SlipMessage INNER JOIN
//                        M_SlipMessageDetails ON M_SlipMessage.SlipMessage_Id = M_SlipMessageDetails.SlipMessage_Id INNER JOIN
//                        M_POS_Machine ON M_SlipMessage.SlipMessage_Code = M_POS_Machine.SlipMessage_Code
//                        WHERE  M_SlipMessageDetails.SlipStat='" + mode.ToString() + @"' and M_SlipMessage.IsActive = 'Y' and M_SlipMessage.Active = 'Y' and M_SlipMessageDetails.Active = 'Y'
//                            AND M_POS_Machine.POS_Code='" + pos + "' ORDER BY SortID";// AND M_POS_Machine.POS_Code='" + key + "'  
//                var res = context.Database.SqlQuery<MessageModel>(sql).ToList();
//                return res;
            }
            //return null;
        }

        public static T_BookingCurrentUpdate GetTBooking(string key)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.T_BookingCurrentUpdate.Where(m => m.PNR_No == key).FirstOrDefault();
                return res;
            }
        }

        public static MessageResponse printcount_update(Guid key)
        {
            MessageResponse msg = new MessageResponse();
            using (var context = new POSINVEntities())
            {
                try
                {
                    var res = context.T_ABB.Where(m => m.ABBTid == key).FirstOrDefault();
                    res.PrintCount = res.PrintCount + 1;
                    context.Entry(res).State = EntityState.Modified;
                    context.Entry(res).Property(m => m.PrintCount).IsModified = true;
                    var result = context.SaveChanges();
                    msg.ID = result;
                    msg.IsSuccessfull = result > 0;
                    msg.Message = result > 0 ? "Initial Data Success." : "Initial Data Fail.";
                }
                catch (Exception ex)
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = ex.Message;
                }
                return msg;
            }
        }

        public static string CreateABB(string pnrno, string userid, string posCode, string stationcode)
        {
            string msg = "";

            //using (var context = new POSINVEntities())
            //{
                //var ss = context.Database.SqlQuery("GenerateInvoiceABBOnline @P1, @P2, @P3, @P4", new SqlParameter("@P1", pnrno)
                //    , new SqlParameter("@P2", userid)
                //    , new SqlParameter("@P3", posCode), new SqlParameter("@P4", stationcode));
                //var ss = context.Database.SqlQuery<List<string>>("GenerateInvoiceABBOnline {0}, {1}, {2}, {3}", new object[] { pnrno, userid, posCode, stationcode }).ToList();
                //context.SaveChanges();
                
                SqlConnection conn = new SqlConnection(FunctionAndClass.Strconn);
                conn.Open();
                SqlTransaction transql = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "GenerateInvoiceABBOnline";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    var param = new object[] {
                            new SqlParameter("@pnrno",pnrno),
                            new SqlParameter("@userid",userid),
                            new SqlParameter("@posCode",posCode),
                            new SqlParameter("@stationcode", stationcode)
                        };
                    cmd.Parameters.AddRange(param);

                    cmd.Connection = conn;
                    cmd.Transaction = transql;
                    msg = (string)cmd.ExecuteScalar();

                    transql.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    transql.Rollback();
                    conn.Close();
                }

            //}

            return msg;
        }

        public static List<M_PaymentType> GetMstPaymentTypeList()
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_PaymentType.Where(x => x.IsActive == "Y").ToList();
                return _item;
            }
        }

        public static M_Currency GetCurrency(string code)
        {
            if (code != null)
            {
                using (var context = new POSINVEntities())
                {
                    var curr = context.M_Currency.OrderByDescending(s => s.Create_Date).Where(m => m.Currency_Code == code && m.IsActive == "Y").FirstOrDefault();
                    return curr;
                }
            }
            return null;
        }

        public static decimal GetCurrencyActiveDate(string Code, DateTime DateActive)
        {
            if (Code != null)
            {
                using (var context = new POSINVEntities())
                {
                    var curr = context.M_Currency.OrderByDescending(s => s.Create_Date).Where(m => m.Currency_Code == Code && m.IsActive == "Y" && ((DateTime)m.Create_Date) <= DateActive).FirstOrDefault();

                    if (curr != null)
                    {
                        return curr.ExChangeRate;
                    }
                    else
                    {
                        curr = context.M_Currency.OrderBy(s => s.Create_Date).Where(m => m.Currency_Code == Code && m.IsActive == "Y" && ((DateTime)m.Create_Date) >= DateActive).FirstOrDefault();
                        if (curr != null)
                            return curr.ExChangeRate;
                    }


                    return 1;
                }
            }
            return 1;
        }



        #region Manaul Invoice...
        public static MessageResponse Invoice_Info_insert(T_Invoice_Manual info, List<T_Invoice_Detail> detail, string posno, string route)
        {
            MessageResponse msg = new MessageResponse();
            using (var context = new POSINVEntities())
            {
                try
                {
                    //var res = context.T_Invoice_Seq.Where(m => m.Prefix == route).FirstOrDefault();
                    //var obj_res = setcurr_invoice(res);
                    //msg.Message = obj_res.Last_Sequence;
                    //msg.IsSuccessfull = true;

                    //res.Curr_Month = obj_res.Curr_Month;
                    //res.Curr_Year = obj_res.Curr_Year;
                    //res.Last_Modify_date = obj_res.Last_Modify_date;
                    //res.Last_POS_Code = info.POS_Code;
                    //res.Last_Sequence = obj_res.Last_Sequence;
                    //res.Last_Station_Code = info.Station_Code;
                    //res.Last_Update_by = info.Cashier_no;
                    //res.Next_Sequence = obj_res.Next_Sequence;
                    //res.Seq_Number = obj_res.Seq_Number;

                    //context.Entry(res).State = EntityState.Modified;
                    //context.Entry(res).Property(m => m.Curr_Month).IsModified = true;
                    //context.Entry(res).Property(m => m.Curr_Year).IsModified = true;
                    //context.Entry(res).Property(m => m.Last_Modify_date).IsModified = true;
                    //context.Entry(res).Property(m => m.Last_POS_Code).IsModified = true;
                    //context.Entry(res).Property(m => m.Last_Sequence).IsModified = true;
                    //context.Entry(res).Property(m => m.Last_Station_Code).IsModified = true;
                    //context.Entry(res).Property(m => m.Last_Update_by).IsModified = true;
                    //context.Entry(res).Property(m => m.Next_Sequence).IsModified = true;
                    //context.Entry(res).Property(m => m.Seq_Number).IsModified = true;

                    var RunningNo = string.Empty;
                    if (route == "AA")
                    {
                        RunningNo = InvoiceDA.GenerateRunning(InvoiceDA.RunningType.AK, info.POS_Code);
                    }
                    else
                    {
                        RunningNo = InvoiceDA.GenerateRunning(InvoiceDA.RunningType.QZ, info.POS_Code);
                    }
                    

                    //var result = context.SaveChanges();
                    if (RunningNo != string.Empty)
                    {
                        var dmsg = Invoice_Detail_insert(detail, RunningNo);

                        if (dmsg.IsSuccessfull)
                        {
                            info.Receipt_no = RunningNo;
                            context.T_Invoice_Manual.Add(info);
                            var res_info = context.SaveChanges();
                            msg.ID = res_info;
                            msg.IsSuccessfull = msg.ID > 0;
                            msg.Message = info.Receipt_no;//msg.ID > 0 ? "Save Data Successfull" : "Save Data Fail";
                        }
                        else
                        {
                            msg.ID = 0;
                            msg.IsSuccessfull = false;
                            msg.Message = "Save Data Fail";
                        }
                    }

                }
                catch (Exception ex)
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = ex.Message;
                }
                return msg;
            }
        }
        public static MessageResponse Invoice_Info_update(T_Invoice_Manual info, List<T_Invoice_Detail> detail, string posno)
        {
            MessageResponse msg = new MessageResponse();
            using (var context = new POSINVEntities())
            {
                try
                {
                    var res = context.T_Invoice_Manual.Where(m => m.Receipt_no == info.Receipt_no).FirstOrDefault();
                    if (res != null)
                    {
                        var dmsg = Invoice_Detail_insert(detail, res.Receipt_no);

                        if (dmsg.IsSuccessfull)
                        {
                            //context.T_Invoice_Manual.Add(info);
                            res.Amount = info.Amount;
                            res.Bank = info.Bank;
                            res.Booking_date = info.Booking_date;
                            res.Booking_name = info.Booking_name;
                            res.Booking_no = info.Booking_no;
                            res.Branch_address1 = info.Branch_address1;
                            res.Branch_address2 = info.Branch_address2;
                            res.Branch_address3 = info.Branch_address3;
                            res.Branch_name = info.Branch_name;
                            res.Branch_Tax_ID = info.Branch_Tax_ID;
                            res.Card_no = info.Card_no;
                            res.Cashier_no = info.Cashier_no;
                            //res.Create_by = info.Create_by;
                            //res.Create_date = info.Create_date;
                            res.Customer_address1 = info.Customer_address1;
                            res.Customer_address2 = info.Customer_address2;
                            res.Customer_address3 = info.Customer_address3;
                            res.Customer_Tax_ID = info.Customer_Tax_ID;
                            res.Document_State = info.Document_State;
                            res.IsActive = info.IsActive;
                            //res.IsPrint = info.IsPrint;
                            res.Net_balance = info.Net_balance;
                            res.Passenger_name = info.Passenger_name;
                            res.Payment_type = info.Payment_type;
                            res.POS_Code = info.POS_Code;
                            res.Receipt_date = info.Receipt_date;
                            //res.Receipt_no = info.Receipt_no;
                            res.Receipt_type = info.Receipt_type;
                            res.Remark = info.Remark;
                            res.Route = info.Route;
                            res.SlipMessage_Code = info.SlipMessage_Code;
                            res.Station_Code = info.Station_Code;
                            res.Update_by = info.Update_by;
                            res.Update_date = info.Update_date;
                            res.Vat = info.Vat;
                            res.VAT_CODE = info.VAT_CODE;
                            res.SlipHeader = info.SlipHeader;

                            context.Entry(res).State = EntityState.Modified;
                            context.Entry(res).Property(m => m.Amount).IsModified = true;
                            context.Entry(res).Property(m => m.Bank).IsModified = true;
                            context.Entry(res).Property(m => m.Booking_date).IsModified = true;
                            context.Entry(res).Property(m => m.Booking_name).IsModified = true;
                            context.Entry(res).Property(m => m.Booking_no).IsModified = true;
                            context.Entry(res).Property(m => m.Branch_address1).IsModified = true;
                            context.Entry(res).Property(m => m.Branch_address2).IsModified = true;
                            context.Entry(res).Property(m => m.Branch_address3).IsModified = true;
                            context.Entry(res).Property(m => m.Branch_name).IsModified = true;
                            context.Entry(res).Property(m => m.Branch_Tax_ID).IsModified = true;
                            context.Entry(res).Property(m => m.Card_no).IsModified = true;
                            context.Entry(res).Property(m => m.Cashier_no).IsModified = true;
                            context.Entry(res).Property(m => m.Customer_address1).IsModified = true;
                            context.Entry(res).Property(m => m.Customer_address2).IsModified = true;
                            context.Entry(res).Property(m => m.Customer_address3).IsModified = true;
                            context.Entry(res).Property(m => m.Customer_Tax_ID).IsModified = true;
                            context.Entry(res).Property(m => m.Document_State).IsModified = true;
                            context.Entry(res).Property(m => m.IsActive).IsModified = true;
                            //context.Entry(res).Property(m => m.IsPrint).IsModified = true;
                            context.Entry(res).Property(m => m.Net_balance).IsModified = true;
                            context.Entry(res).Property(m => m.Passenger_name).IsModified = true;
                            context.Entry(res).Property(m => m.Payment_type).IsModified = true;
                            context.Entry(res).Property(m => m.POS_Code).IsModified = true;
                            context.Entry(res).Property(m => m.Receipt_date).IsModified = true;
                            context.Entry(res).Property(m => m.Receipt_type).IsModified = true;
                            context.Entry(res).Property(m => m.Remark).IsModified = true;
                            context.Entry(res).Property(m => m.Route).IsModified = true;
                            context.Entry(res).Property(m => m.SlipMessage_Code).IsModified = true;
                            context.Entry(res).Property(m => m.Station_Code).IsModified = true;
                            context.Entry(res).Property(m => m.Update_by).IsModified = true;
                            context.Entry(res).Property(m => m.Update_date).IsModified = true;
                            context.Entry(res).Property(m => m.Vat).IsModified = true;
                            context.Entry(res).Property(m => m.VAT_CODE).IsModified = true;
                            context.Entry(res).Property(m => m.SlipHeader).IsModified = true;
                            var res_info = context.SaveChanges();
                            msg.ID = res_info;// context.SaveChanges();
                            msg.IsSuccessfull = msg.ID > 0;
                            msg.Message = msg.ID > 0 ? "Save Data Successfull" : "Save Data Fail";
                        }
                        else
                        {
                            msg.ID = 0;
                            msg.IsSuccessfull = false;
                            msg.Message = "Save Data Fail";
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = ex.Message;
                }
                return msg;
            }
        }

        public static MessageResponse UpdateIsPrint_Invoice_Manual(string _Receipt_no)
        {
            MessageResponse msg = new MessageResponse();
            using (var context = new POSINVEntities())
            {
                try
                {
                    var _Invoice_Manual = context.T_Invoice_Manual.Where(m => m.Receipt_no == _Receipt_no).FirstOrDefault();
                    if (_Invoice_Manual != null && _Invoice_Manual.IsPrint.GetValueOrDefault(false) == false)
                    {
                        context.T_Invoice_Manual.Attach(_Invoice_Manual);
                        _Invoice_Manual.IsPrint = true;
                        context.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    msg.ID = 0;
                    msg.IsSuccessfull = false;
                    msg.Message = ex.Message;
                }
            }
            return msg;
        }

        public static MessageResponse Invoice_Detail_insert(List<T_Invoice_Detail> detail, string invoice_no)
        {
            MessageResponse msg = new MessageResponse();
            msg.IsSuccessfull = false;
            using (var context = new POSINVEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        var res = context.T_Invoice_Detail.Where(m => m.Receipt_no == invoice_no);
                        foreach (var item in res)
                        {
                            context.T_Invoice_Detail.Remove(item);
                        }
                        var resdel = context.SaveChanges();
                        if (true)
                        {
                            foreach (var item in detail)
                            {
                                item.Receipt_no = invoice_no;
                                context.T_Invoice_Detail.Add(item);
                                msg.ID = context.SaveChanges();
                                msg.IsSuccessfull = msg.ID > 0;
                                msg.Message = msg.ID > 0 ? "Save Data Successfull" : "Save Data Fail";
                            }
                            transaction.Complete();
                        }
                        else
                        {
                            msg.ID = 0;
                            msg.IsSuccessfull = false;
                            msg.Message = "Save Data Fail";
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.ID = 0;
                        msg.IsSuccessfull = false;
                        msg.Message = ex.Message;
                    }
                }
                return msg;
            }
        }
        public static T_Invoice_Manual GetInvoiceManual(string key)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.T_Invoice_Manual.Where(m => m.Receipt_no == key).FirstOrDefault();
                return res;
            }
        }
        public static List<T_Invoice_Detail> GetInvoiceDetailList(string key)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.T_Invoice_Detail.Where(m => m.Receipt_no == key).ToList();
                return res;
            }
        }
        public static List<T_Invoice_Manual> GetInvoiceList(CriteriaInvoiceModel val)
        {
            using (var context = new POSINVEntities())
            {
                string sqlwhere = "";
                sqlwhere += val.booking_no != "" && val.booking_no != null ? " AND Booking_no='" + val.booking_no + "'" : "";
                sqlwhere += val.date_from != null && val.date_to != null ? " AND (Booking_date between '" + val.date_from.Value.ToString("yyyy-MM-dd") + "' and '" + val.date_to.Value.ToString("yyyy-MM-dd") + "')" : "";
                sqlwhere += val.passenger != "" && val.passenger != null ? " AND Passenger_name like '%" + val.passenger + "%'" : "";


                string sql = @"select * from T_Invoice_Manual where status is null " + sqlwhere;
                var res = context.Database.SqlQuery<T_Invoice_Manual>(sql).ToList();
                return res;
            }
        }
        #endregion

        public static T_Invoice_CN GetInvoiceCN_ByCN_ID(string cn_id)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.T_Invoice_CN.Where(m => m.CN_ID.ToString() == cn_id).FirstOrDefault();
                return res;
            }
        }

        public string DeleteMessageSlip(int SlipMessage_Id)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_SlipMessage.Where(x => x.SlipMessage_Id == SlipMessage_Id).FirstOrDefault();
                if (original != null)
                {
                    if (context.T_Invoice_Info.Count(x => x.SlipMessage_Code == original.SlipMessage_Id.ToString()) > 0 ||
                        context.T_Invoice_Manual.Count(x => x.SlipMessage_Code == original.SlipMessage_Id.ToString()) > 0 ||
                        context.M_Station.Count(x => x.ABB_SlipMessage_Id == original.SlipMessage_Id || x.INV_SlipMessage_Id == original.SlipMessage_Id || x.CN_SlipMessage_Id == original.SlipMessage_Id) > 0
                        )
                    {
                        return strErrorDataUsed;
                    }

                    original.IsActive = "N";
                    var entry = context.Entry(original);
                    entry.State = EntityState.Modified;
                    entry.Property(e => e.IsActive).IsModified = true;
                    context.SaveChanges();
                }
            }
            return string.Empty;
        }

        public static List<M_SlipMessage> GetMstSlipMessageList()
        {
            using (var context = new POSINVEntities())
            {
                var res = context.M_SlipMessage.Where(m => m.IsActive=="Y").ToList();
                return res;
            }
        }
        public static List<M_SlipMessageDetails> GetMstSlipMessageDetailList(int key)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.M_SlipMessageDetails.Where(m => m.SlipMessage_Id == key && m.SlipStat == true).OrderBy(m => m.SortID).ToList();
                return res;
            }
        }

        public static List<M_VAT> GetMstVatList()
        {
            using (var context = new POSINVEntities())
            {
                var res = context.M_VAT.Where(m => m.ISACTIVE == true).ToList();
                return res;
            }
        }

        private static T_Invoice_Seq setcurr_invoice(T_Invoice_Seq obj)
        {
            T_Invoice_Seq val = new T_Invoice_Seq();
            //ext running code = AA=prefix, MM=month, YY=year, xxxxx = running number 5 digit
            var cur_MM = DateTime.Now.ToString("MM");
            var cur_YY = DateTime.Now.ToString("yy");
            var cur_number = "0";
            var prefix = obj.Prefix.Trim();
            var old_number = obj.Seq_Number;
            var old_MM = obj.Curr_Month;
            var old_YY = obj.Curr_Year;
            var receipt_no = "";

            val.Curr_Month = cur_MM;
            val.Curr_Year = cur_YY;
            val.Last_Modify_date = DateTime.Now;
            val.Last_Update_by = "";
            val.Last_POS_Code = "";

            val.Last_Station_Code = "";
            val.Next_Sequence = "";

            if (prefix != string.Empty)
            {
                //old_number = old_invoice.Substring(4);
                if (Convert.ToInt32(cur_YY) == Convert.ToInt32(old_YY))
                {
                    if (Convert.ToInt32(cur_MM) == Convert.ToInt32(old_MM))
                    {
                        cur_number = (Convert.ToDouble(old_number == null ? 0 : old_number) + 1).ToString();
                        receipt_no = prefix + cur_MM + cur_YY + "-" + Convert.ToDouble(cur_number).ToString("00000");
                        val.Seq_Number = (old_number == null ? 0 : old_number) + 1;
                    }
                    else
                    {
                        cur_number = "1";
                        receipt_no = prefix + cur_MM + cur_YY + "-" + Convert.ToDouble(cur_number).ToString("00000");
                        val.Seq_Number = 1;
                    }
                }
                else if ((Convert.ToInt32(cur_YY) != Convert.ToInt32(old_YY)))
                {
                    cur_number = "1";
                    receipt_no = prefix + cur_MM + cur_YY + "-" + Convert.ToDouble(cur_number).ToString("00000");
                    val.Seq_Number = 1;
                }
            }
            else
            {
                cur_number = "1";
                receipt_no = prefix + cur_MM + cur_YY + "-" + Convert.ToDouble(cur_number).ToString("00000");
                val.Seq_Number = 1;

            }
            val.Last_Sequence = receipt_no;

            return val;
        }

        public List<EmpModel> GetEmplist(string code, string name ,string grp, string station)
        {

            try
            {
                using (var context = new POSINVEntities())
                {
                    string sql = @"select emp.UserID, emp.UserCode, emp.Username, emp.FirstName + ' ' + emp.LastName as Name
, grp.UserGroupCode, grp.UserGroupName, stn.Station_Code, stn.Station_Name
, emp.Active
from M_Employee emp 
left join M_UserGroup grp
on emp.UserGroup = grp.UserGroupCode and grp.IsActive = 'Y'
left join M_Station stn
on emp.Station_Code = stn.Station_Code and stn.Active = 'Y' and stn.IsActive = 'Y'
where (emp.UserCode like '%" + code + @"%') and (emp.FirstName + ' ' + emp.LastName like '%" + name + @"%')
and (grp.UserGroupCode = '" + grp + @"' or '' = '" + grp + @"') and (stn.Station_Code = '" + station + @"' or '' = '" + station + @"') and emp.IsActive = 'Y'
order by emp.FirstName + ' ' + emp.LastName asc";
                    List<EmpModel> res = context.Database.SqlQuery<EmpModel>(sql).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            //return null;
        }

        public void DeleteEmp(int id, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Employee.Where(x => x.UserID == id).FirstOrDefault();
                original.IsActive = "N";
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.IsActive).IsModified = true;
                context.SaveChanges();
            }
        }

        public M_Station GetStation(string code)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var found = context.M_Station.FirstOrDefault(f => f.Station_Code == code);
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public M_POS_Machine GetPOS(string code)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var found = context.M_POS_Machine.FirstOrDefault(f => f.POS_Code == code);
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static M_POS_Machine GetPOSbyId(int _Id)
        {
            using (var context = new POSINVEntities())
            {
                return context.M_POS_Machine.FirstOrDefault(f => f.POS_Id == _Id);
            }
        }

        public M_Menu GetMenuForId(string path)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    string[] spath = path.Split('/');
                    string filename = spath[spath.Length - 1];
                    var found = context.M_Menu.FirstOrDefault(f => f.Menu_Url == filename);
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public void AddLog(string userid, string stationid, string posid, int menuid)
        {
            using (var context = new POSINVEntities())
            {
                int uid = int.Parse(userid);
                int sid = int.Parse(stationid);
                int pid = int.Parse(posid);
                var log = new T_LogAccess
                {
                    UserID = uid,
                    TimeStamp = DateTime.Now,
                    Station_Id = sid,
                    POS_Id = pid,
                    Menu_Id = menuid
                };
                context.T_LogAccess.Add(log);
                context.SaveChanges();
            }
        }

        public List<Monitoruser> GetMonitoruserlist(string code, string grp, string station)
        {

            try
            {
                using (var context = new POSINVEntities())
                {
                    string sql = @"select emp.Username, T_LogAccess.TimeStamp , stn.Station_Name,  grp.UserGroupName
, pos.POS_MacNo, meu.Menu_Name 
from T_LogAccess
left join M_Employee emp 
on T_LogAccess.UserID = emp.UserID
left join M_UserGroup grp
on emp.UserGroup = grp.UserGroupCode
left join M_Station stn
on T_LogAccess.Station_Id = stn.Station_Id
left join M_POS_Machine pos
on T_LogAccess.POS_Id = pos.POS_Id
left join M_Menu meu
on T_LogAccess.Menu_Id = meu.Menu_Id
where (emp.Username like '%" + code + @"%') and (cast(stn.Station_Id as int) = '" + station + @"' or '' = '" + station + @"') 
and (cast(grp.UserGroupId as int) = '" + grp + @"' or '' = '" + grp + @"') and convert(varchar, T_LogAccess.TimeStamp, 112) = convert(varchar, GetDate(), 112)
order by T_LogAccess.TimeStamp desc";
                    List<Monitoruser> res = context.Database.SqlQuery<Monitoruser>(sql).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            //return null;
        }

        public List<LogABB> GetLogABB(string code)
        {

            try
            {
                using (var context = new POSINVEntities())
                {
                    string sql = @"select TOP 500 a.[PNR_No]
                                      ,a.[Msg]
	                                  ,case when a.Create_By = '9999999' then 'Batch'
	                                   else b.Username end Create_By
	                                  ,a.[Create_Date]
                                from [dbo].T_WaitingForManualGenABB a
                                left join [M_Employee] b on a.Create_By = b.UserCode
                                where PNR_No like '%" + code + @"%'
                                order by a.[Create_Date] desc";
                    List<LogABB> res = context.Database.SqlQuery<LogABB>(sql).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            //return null;
        }

        public void locksystem(bool locksystem, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                string locksys = (locksystem) ? "Y" : "N";
                var appsys = new M_AppSystem
                {
                    LockSystem = locksys,
                    Create_Date = DateTime.Now,
                    Create_By = Create_By
                };
                context.M_AppSystem.Add(appsys);
                context.SaveChanges();
            }
        }

        public M_AppSystem GetLockSystem()
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var found = context.M_AppSystem.OrderByDescending(x=>x.Create_Date).FirstOrDefault();
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<MenuPrivilege> GetMenuPrivilege(string code)
        {

            try
            {
                using (var context = new POSINVEntities())
                {
                    string sql = @"select a.Menu_Code, a.Menu_Name	
                    , case when b.CanView = 0 then (case when a.CanView = 0 then 2 else b.CanView end)
						else b.CanView end as CanView
					, case when b.CanInsert = 0 then (case when a.CanInsert = 0 then 2 else b.CanInsert end)
						else b.CanInsert end as CanInsert
					, case when b.CanDelete = 0 then (case when a.CanDelete = 0 then 2 else b.CanDelete end)
						else b.CanDelete end as CanDelete
					, case when b.CanUpdate = 0 then (case when a.CanUpdate = 0 then 2 else b.CanUpdate end)
						else b.CanUpdate end as CanUpdate
					, case when b.CanProcess = 0 then (case when a.CanProcess = 0 then 2 else b.CanProcess end)
						else b.CanProcess end as CanProcess
					, case when b.CanPrint = 0 then (case when a.CanPrint = 0 then 2 else b.CanPrint end)
						else b.CanPrint end as CanPrint
                    from M_Menu a
                    left join M_UserGroupMenu b
                    on a.Menu_Code = b.Menu_Code
                    where b.UserGroupCode = '" + code + @"'
                    order by Menu_Code";
                    List<MenuPrivilege> res = context.Database.SqlQuery<MenuPrivilege>(sql).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            //return null;
        }

        public string UpdateUserGroupMenu(List<MenuPrivilege> menuprivilege, string code)
        {
            using (var context = new POSINVEntities())
            {
                string msg = "";
                foreach (MenuPrivilege menu in menuprivilege)
                {
                    try
                    {
                        var original = context.M_UserGroupMenu.Where(x => x.UserGroupCode == code && x.Menu_Code == menu.Menu_Code).FirstOrDefault();
                        original.CanView = menu.CanView;
                        var entry = context.Entry(original);
                        entry.State = EntityState.Modified;
                        entry.Property(e => e.CanView).IsModified = true;
                        context.SaveChanges();

                        var listall = context.M_UserPermission.Where(x => x.UserGroupCode == code && x.Menu_Code == menu.Menu_Code).ToList();
                        listall.ForEach(a=>a.CanView = menu.CanView);
                        var oentry = context.Entry(original);
                        oentry.State = EntityState.Modified;
                        oentry.Property(e => e.CanView).IsModified = true;
                        context.SaveChanges();

                        
                    }
                    catch (Exception ex)
                    {
                        msg += ex.Message;
                    }
                    
                }

                return msg;
            }
        }

        public string UpdateUserGroupMenuUser(List<MenuPrivilege> menuprivilege, string code, string user)
        {
            using (var context = new POSINVEntities())
            {
                string msg = "";
                foreach (MenuPrivilege menu in menuprivilege)
                {
                    try
                    {

                        var original = context.M_UserPermission.Where(x => x.UserGroupCode == code && x.Menu_Code == menu.Menu_Code && x.UserCode == user).FirstOrDefault();
                        original.CanView = menu.CanView;
                        var oentry = context.Entry(original);
                        oentry.State = EntityState.Modified;
                        oentry.Property(e => e.CanView).IsModified = true;
                        context.SaveChanges();


                    }
                    catch (Exception ex)
                    {
                        msg += ex.Message;
                    }

                }

                return msg;
            }
        }

        public List<M_UserPermission> GetMenuPrivilege(string usercode, string usergrpcode)
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var found = context.M_UserPermission.Where(f => f.UserCode == usercode && f.UserGroupCode == usergrpcode).ToList();
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<M_Menu> GetMenu()
        {
            using (var context = new POSINVEntities())
            {
                try
                {
                    var found = context.M_Menu.ToList();
                    return found;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<MenuPrivilege> GetMenuPrivilegeUser(string code, string user)
        {

            try
            {
                using (var context = new POSINVEntities())
                {
                    string sql = @"select a.Menu_Code, a.Menu_Name	
                    , case when b.CanView = 0 then (case when a.CanView = 0 then 2 else b.CanView end)
						else b.CanView end as CanView
					, case when b.CanInsert = 0 then (case when a.CanInsert = 0 then 2 else b.CanInsert end)
						else b.CanInsert end as CanInsert
					, case when b.CanDelete = 0 then (case when a.CanDelete = 0 then 2 else b.CanDelete end)
						else b.CanDelete end as CanDelete
					, case when b.CanUpdate = 0 then (case when a.CanUpdate = 0 then 2 else b.CanUpdate end)
						else b.CanUpdate end as CanUpdate
					, case when b.CanProcess = 0 then (case when a.CanProcess = 0 then 2 else b.CanProcess end)
						else b.CanProcess end as CanProcess
					, case when b.CanPrint = 0 then (case when a.CanPrint = 0 then 2 else b.CanPrint end)
						else b.CanPrint end as CanPrint
                    from M_Menu a
                    left join [dbo].[M_UserPermission] b
                    on a.Menu_Code = b.Menu_Code
                    where b.UserGroupCode = '" + code + @"' and b.UserCode = '" + user + @"'
                    order by Menu_Code";
                    List<MenuPrivilege> res = context.Database.SqlQuery<MenuPrivilege>(sql).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            //return null;
        }

        public void SaveUser(string usercode, string username, string password, string password1, string firstname, string lastname
            , string group, string station, string pos, string menu, string active, string Create_By)
        {
            using (var context = new POSINVEntities())
            {
                var user = new M_Employee
                {
                    UserCode = usercode,
                    Username = username,
                    Password = password,
                    FirstName = firstname,
                    LastName = lastname,
                    UserGroup = group,
                    Station_Code = station,
                    PID = pos,
                    DefaultMenu = menu,
                    Active = active,
                    Create_By = Create_By,
                    IsActive = "Y",
                    Create_Date = DateTime.Now,
                    Update_By = Create_By,
                    Update_Date = DateTime.Now
                };
                context.M_Employee.Add(user);
                context.SaveChanges();

                if (pos == "")
                {
                    var iuser = context.M_Employee.Where(x => x.UserCode == usercode && x.Active == "Y" && x.IsActive == "Y").FirstOrDefault();
                    var opos = context.M_POS_Machine.Where(x => x.Station_Code == station && x.Active == "Y" && x.IsActive == "Y").ToList();

                    foreach (M_POS_Machine ipos in opos)
                    {
                        var ep = new M_EmpPOS
                        {
                            UserID = iuser.UserID,
                            PID = ipos.POS_Id
                        };
                        context.M_EmpPOS.Add(ep);
                        context.SaveChanges();
                    }
                }
                else
                {
                    var iuser = context.M_Employee.Where(x => x.UserCode == usercode && x.Active == "Y" && x.IsActive == "Y").FirstOrDefault();
                    var opos = context.M_POS_Machine.Where(x => x.POS_Code == pos && x.Active == "Y" && x.IsActive == "Y").FirstOrDefault();
                    var ep = new M_EmpPOS
                    {
                        UserID = iuser.UserID,
                        PID = opos.POS_Id
                    };
                    context.M_EmpPOS.Add(ep);
                    context.SaveChanges();
                }

                var usergrp = context.M_UserGroupMenu.Where(x => x.UserGroupCode == group).ToList();

                foreach (M_UserGroupMenu grpm in usergrp)
                {
                    var pm = new M_UserPermission
                    {
                        UserCode = usercode,
                        UserGroupCode = grpm.UserGroupCode,
                        Menu_Code = grpm.Menu_Code,
                        CanView = grpm.CanView,
                        CanDelete = grpm.CanDelete,
                        CanInsert = grpm.CanInsert,
                        CanPrint = grpm.CanPrint,
                        CanProcess = grpm.CanProcess,
                        CanUpdate = grpm.CanUpdate
                    };
                    context.M_UserPermission.Add(pm);
                    context.SaveChanges();
                }
            }
        }

        public M_Employee GetUser(int id)
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_Employee.Where(x => x.UserID == id).FirstOrDefault();
                return _item;
            }
        }

        public void EditUser(int id, string usercode, string username, string password, string password1, string firstname, string lastname
            , string group, string station, string pos, string menu, string active, string Update_By)
        {
            using (var context = new POSINVEntities())
            {
                var original = context.M_Employee.Where(x => x.UserID == id).FirstOrDefault();
                var _UserCode = original.UserCode;
                var _UserGroup = original.UserGroup;
                original.UserCode = usercode;
                original.Username = username;
                original.Password = password;
                original.FirstName = firstname;
                original.LastName = lastname;
                original.UserGroup = group;
                original.Station_Code = station;
                original.PID = pos;
                original.DefaultMenu = menu;
                original.Active = active;
                original.Update_By = Update_By;
                original.Update_Date = DateTime.Now;
                var entry = context.Entry(original);
                entry.State = EntityState.Modified;
                entry.Property(e => e.UserCode).IsModified = true;
                entry.Property(e => e.Username).IsModified = true;
                entry.Property(e => e.Password).IsModified = true;
                entry.Property(e => e.FirstName).IsModified = true;
                entry.Property(e => e.LastName).IsModified = true;
                entry.Property(e => e.UserGroup).IsModified = true;
                entry.Property(e => e.Station_Code).IsModified = true;
                entry.Property(e => e.PID).IsModified = true;
                entry.Property(e => e.DefaultMenu).IsModified = true;
                entry.Property(e => e.Active).IsModified = true;
                entry.Property(e => e.Update_By).IsModified = true;
                entry.Property(e => e.Update_Date).IsModified = true;
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("delete from M_EmpPOS where UserID = @P1", new SqlParameter("@P1", id));
                if (pos == "")
                {
                    var opos = context.M_POS_Machine.Where(x => x.Station_Code == station && x.Active == "Y" && x.IsActive == "Y").ToList();

                    foreach (M_POS_Machine ipos in opos)
                    {
                        var ep = new M_EmpPOS
                        {
                            UserID = id,
                            PID = ipos.POS_Id
                        };
                        context.M_EmpPOS.Add(ep);
                        context.SaveChanges();
                    }
                }
                else
                {
                    var opos = context.M_POS_Machine.Where(x => x.POS_Code == pos && x.Active == "Y" && x.IsActive == "Y").FirstOrDefault();
                    var ep = new M_EmpPOS
                    {
                        UserID = id,
                        PID = opos.POS_Id
                    };
                    context.M_EmpPOS.Add(ep);
                    context.SaveChanges();
                }

                var userPermissions = context.M_UserPermission.Where(x => x.UserCode == _UserCode  && x.UserGroupCode == _UserGroup).ToList();
                foreach (var userPermission in userPermissions)
                {
                    context.M_UserPermission.Remove(userPermission);
                    context.SaveChanges();
                }

                var usergrp = context.M_UserGroupMenu.Where(x => x.UserGroupCode == group).ToList();

                foreach (M_UserGroupMenu grpm in usergrp)
                {
                    var pm = new M_UserPermission
                    {
                        UserCode = usercode,
                        UserGroupCode = grpm.UserGroupCode,
                        Menu_Code = grpm.Menu_Code,
                        CanView = grpm.CanView,
                        CanDelete = grpm.CanDelete,
                        CanInsert = grpm.CanInsert,
                        CanPrint = grpm.CanPrint,
                        CanProcess = grpm.CanProcess,
                        CanUpdate = grpm.CanUpdate
                    };
                    context.M_UserPermission.Add(pm);
                    context.SaveChanges();
                }
            }
        }
        public static List<M_FlightType> GetFlightTypeList()
        {
            using (var context = new POSINVEntities())
            {
                var _item = context.M_FlightType.Where(x => x.IsActive == "Y").ToList();
                return _item;
            }
        }

        public static List<T_ABB> GetAllABBList(CriteriaInvoiceModel obj)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.T_ABB.OrderByDescending(it=>it.Create_Date).Take(500).OrderBy(s => s.Create_Date).Where(f =>
                    //(f.PNR_No == obj.booking_no || "ALL" == (obj.booking_no == "" & obj.booking_no == null ? "ALL" : obj.booking_no)) &&
                    //(f.b == obj.booking_no || f.PNR_No == "ALL") &&
                    f.STATUS_INV != "CANCEL" ).ToList();
                /*
                 SELECT A.PNR_No, A.TaxInvoiceNo, B.Booking_Date FROM T_ABB A INNER JOIN T_BookingCurrentUpdate B
                ON B.PNR_No = A.PNR_No 
                WHERE A.PNR_No ='IHINNA'
                 */
                return res;
            }
        }

        public static T_ABB GetABBByInvoice(string key)
        {
            using (var context = new POSINVEntities())
            {
                var res = context.T_ABB.Where(m => m.TaxInvoiceNo == key).FirstOrDefault();
                return res;
            }
        }

        public static void UpdateLogin(int _UserId, string _UserCode, string _IPAddress, int? POS_ID, bool isOnline)
        {
            using (var context = new POSINVEntities())
            {
                var _UserLogin = context.M_Employee.Where(x => x.UserID == _UserId).FirstOrDefault();

                if (_UserLogin != null)
                {
                    context.M_Employee.Attach(_UserLogin);

                    _UserLogin.IsOnline = isOnline;
                    if (isOnline)
                    {
                        _UserLogin.IP_Address = _IPAddress;
                        _UserLogin.LastLogIn_Date = DateTime.Now;
                        _UserLogin.Last_POS_ID = POS_ID;
                    }

                    context.SaveChanges();
                }

            }
        }

        public static M_Employee CheckUserOnline(int _UserId, string _IPAddress)
        {
            //M_Employee _Employee = null;
            using (var context = new POSINVEntities())
            {
                var _Employee = context.M_Employee.Where(x => x.UserID == _UserId && x.IP_Address != _IPAddress && x.IsOnline.HasValue && x.IsOnline.Value && x.LastLogIn_Date.HasValue)
                    .ToList().Where(q =>  (DateTime.Now - q.LastLogIn_Date.Value).TotalMinutes < 20).FirstOrDefault();

                return _Employee;
            }

            //return _Employee;
        }

        public static M_Employee CheckPOSOnline(int POS_Id)
        {
            //M_Employee _Employee = null;
            using (var context = new POSINVEntities())
            {
                var _Employee = context.M_Employee.Where(x => x.Last_POS_ID == POS_Id && x.IsOnline.HasValue && x.IsOnline.Value && x.LastLogIn_Date.HasValue)
                    .ToList().Where(q => (DateTime.Now - q.LastLogIn_Date.Value).TotalMinutes < 20).FirstOrDefault();

                return _Employee;
            }

        }

        public static bool IsSessionExpired(int _UserId)
        {
            using (var context = new POSINVEntities())
            {
                var _Employee = context.M_Employee.Where(x => x.UserID == _UserId).FirstOrDefault();

                if (_Employee.LastLogIn_Date.HasValue)
                {
                    if ((DateTime.Now - _Employee.LastLogIn_Date.Value).TotalMinutes < 20)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static List<string> GetABBNoByBookingNo(string BookingNo)
        {
            var Output = new List<string>();

            using (var context = new POSINVEntities())
            {
                Output = context.T_ABB.Where(x => x.PNR_No == BookingNo && x.STATUS_INV == "ACTIVE").Select(s => s.TaxInvoiceNo).ToList();
            }

            return Output;
        }

        public static string GetPaymentFromABBNo(Guid ABBTid)
        {
            var Output = string.Empty;
            using (var context = new POSINVEntities())
            {
                var ApprovalDates = context.T_ABBPAYMENTMETHOD.Where(x => x.ABBTid == ABBTid).OrderBy(v => v.ApprovalDate).ToList().Select(s => s.ApprovalDate.ToString("dd/MM/yyyy")).GroupBy(c => c).Select(v => v.Key).ToList();
                Output = String.Join(", ", ApprovalDates);
            }
            return Output;
        }

        public static List<M_FlightFeeGroup> GetFlightFeeGroup(string Name)
        {
            List<M_FlightFeeGroup> Output;
            using (var context = new POSINVEntities())
            {
                Output = context.M_FlightFeeGroup.Where(x => x.NameTH.Contains(Name)).OrderBy(v => v.NameTH).ToList();
            }
            return Output;
        }

        public static string SaveFlightFeeGroup(Guid? ID, string NameTH, string NameEN, string DescriptionTH, string DescriptionEN)
        {
            using (var context = new POSINVEntities())
            {
                var FlightFeeGroup = new M_FlightFeeGroup()
                {
                    ID = Guid.NewGuid()
                };

                if (ID.HasValue && ID.Value != Guid.Empty)
                {
                    if (context.M_FlightFeeGroup.Count(c => c.ID != ID && c.NameTH == NameTH) > 0)
                    {
                        return "Group Name " + NameTH + " already exists";
                    }
                    FlightFeeGroup = context.M_FlightFeeGroup.First(x => x.ID == ID);
                    context.M_FlightFeeGroup.Attach(FlightFeeGroup);
                }
                else
                {
                    if (context.M_FlightFeeGroup.Count(c => c.NameTH == NameTH) > 0)
                    {
                        return "Group Name " + NameTH + " already exists";
                    }
                    context.M_FlightFeeGroup.Add(FlightFeeGroup);
                }

                FlightFeeGroup.NameTH = NameTH;
                FlightFeeGroup.NameEN = NameEN;
                FlightFeeGroup.DescriptionTH = DescriptionTH;
                FlightFeeGroup.DescriptionEN = DescriptionEN;

                context.SaveChanges();
            }
            return string.Empty;
        }

        public string DeleteFlightFeeGroup(Guid ID)
        {
            using (var context = new POSINVEntities())
            {
                if (context.M_FlightFeeGroup.Count(x => x.ID == ID) > 0)
                {
                    if (context.M_FlightFee.Count(c =>  c.FlightFeeGroup_ID != null && c.FlightFeeGroup_ID ==  ID) > 0)
                    {
                        return strErrorDataUsed;
                    }
                   
                    var FlightFeeGroup = context.M_FlightFeeGroup.First(x => x.ID == ID);
                    context.M_FlightFeeGroup.Remove(FlightFeeGroup);
                    context.SaveChanges();
                }
                else
                {
                    return "Not Found Data.!!!!";
                }
            }
            return string.Empty;
        }


        static public List<M_SendMessage> GetM_SendMessage()
        {
            try
            {
                using (var context = new POSINVEntities())
                {

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Select * from M_SendMessage ");
                    sb.AppendLine("WHERE IsActive ='Y' AND ( CONVERT(varchar(8),StartDate,112) <= CONVERT(varchar(8),GETDATE(),112)  ");
                    sb.AppendLine("AND  CONVERT(varchar(8),ExpireDate,112) >= CONVERT(varchar(8),GETDATE(),112) )");

                    var _item = context.Database.SqlQuery<M_SendMessage>(sb.ToString()).ToList();

                    return _item;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
