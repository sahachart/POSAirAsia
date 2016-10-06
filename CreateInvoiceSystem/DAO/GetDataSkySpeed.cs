using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Data;


namespace CreateInvoiceSystem.DAO
{
    public class GetDataSkySpeed
    {
        public GetDataSkySpeed()
        { 
        
        }

        public com.airasia.acebooking.Booking GetData(string pnrno)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["GetFileBooking"] != null && System.Configuration.ConfigurationManager.AppSettings["GetFileBooking"].ToString() == "1")
                return GetDataXML(pnrno); // For Test

            com.airasia.acebooking.Booking booking = null;
            try
            {
                string URL_Session = System.Configuration.ConfigurationManager.AppSettings["com_airasia_acesession_SessionService"];
                string URL_Bookinging = System.Configuration.ConfigurationManager.AppSettings["com_airasia_acebooking_BookingService"];

                com.airasia.acesession.LogonRequest logonreq = new com.airasia.acesession.LogonRequest();
                logonreq.Username = System.Configuration.ConfigurationManager.AppSettings["usernamesky"];
                logonreq.Password = System.Configuration.ConfigurationManager.AppSettings["passwordsky"];
                com.airasia.acesession.basicHttpsSessionService svc = new com.airasia.acesession.basicHttpsSessionService();

                svc.Url = URL_Session;
                com.airasia.acesession.LogonResponse logonres = svc.Logon(logonreq);
                string ssid = logonres.SessionID;
                com.airasia.acebooking.GetByRecordLocator getbyrex = new com.airasia.acebooking.GetByRecordLocator();
                com.airasia.acebooking.GetBookingRequestData bookreq = new com.airasia.acebooking.GetBookingRequestData();
                com.airasia.acebooking.basicHttpBookingService booksvc = new com.airasia.acebooking.basicHttpBookingService();



                if (ssid != string.Empty)
                {
                    getbyrex = new com.airasia.acebooking.GetByRecordLocator();
                    getbyrex.RecordLocator = pnrno;
                    bookreq = new com.airasia.acebooking.GetBookingRequestData();
                    bookreq.GetByRecordLocator = getbyrex;
                    booksvc = new com.airasia.acebooking.basicHttpBookingService();

                    booksvc.Url = URL_Bookinging;
                    booking = booksvc.GetBooking(ssid, bookreq);

                }
            }
            catch (Exception ex)
            {
                LogException.Save(ex, "GetDataSkySpeed", CookiesMenager.EmpID);
                //throw ex;
            }

            return booking;
        }

        public void LoadToDB(com.airasia.acebooking.Booking booking, string userid)
        { 
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    int SEQ_BookSer = 0;
                    int SEQ_PassSer = 0;
                    using (var context = new POSINVEntities())
                    {
                        Guid tid = Guid.NewGuid();

                        #region booking
                        string paxcount = booking.PaxCount.ToString();
                        string status = booking.BookingInfo.BookingStatus.ToString();
                        string Total = booking.BookingSum.TotalCost.ToString();
                        var dataitem = new T_BookingCurrentUpdate
                        {
                            TransactionId = tid,
                            BookingId = booking.BookingID,
                            Load_Date = DateTime.Now,
                            PNR_No = booking.RecordLocator,
                            Booking_Date = booking.BookingInfo.BookingDate,
                            CurrencyCode = booking.CurrencyCode,
                            PaxCount = paxcount,
                            BookingParentID = booking.BookingParentID,
                            ParentRecordLocator = booking.ParentRecordLocator,
                            GroupName = booking.GroupName,
                            BookingStatus = status,
                            TotalCost = Total,
                            ABBNo = null,
                            Create_By = userid,
                            Create_Date = DateTime.Now,
                            Update_By = userid,
                            Update_Date = DateTime.Now
                        };
                        context.T_BookingCurrentUpdate.Add(dataitem);

                        #endregion

                        foreach (com.airasia.acebooking.Payment payment in booking.Payments)
                        {
                            #region add payment

                            string ReferenceType = payment.ReferenceType.ToString();
                            string PaymentMethodType = payment.PaymentMethodType.ToString();
                            string PaymentStatus = payment.Status.ToString();
                            var paymentitem = new T_PaymentCurrentUpdate
                            {
                                PaymentTId = Guid.NewGuid(),
                                TransactionId = tid,
                                PaymentID = payment.PaymentID,
                                ReferenceType = ReferenceType,
                                ReferenceID = payment.ReferenceID,
                                PaymentMethodType = PaymentMethodType,
                                PaymentMethodCode = payment.PaymentMethodCode,
                                CurrencyCode = payment.CurrencyCode,
                                PaymentAmount = payment.PaymentAmount,
                                CollectedCurrencyCode = payment.CollectedCurrencyCode,
                                CollectedAmount = payment.CollectedAmount,
                                QuotedCurrencyCode = payment.QuotedCurrencyCode,
                                QuotedAmount = payment.QuotedAmount,
                                Status = PaymentStatus,
                                ParentPaymentID = payment.ParentPaymentID,
                                AgentCode = payment.PointOfSale.AgentCode,
                                OrganizationCode = payment.PointOfSale.OrganizationCode,
                                DomainCode = payment.PointOfSale.DomainCode,
                                LocationCode = payment.PointOfSale.LocationCode,
                                ApprovalDate = payment.ApprovalDate,
                                ABBNo = null,
                                Create_By = userid,
                                Create_Date = DateTime.Now,
                                Update_By = userid,
                                Update_Date = DateTime.Now
                            };


                            if (payment.PaymentFields != null && payment.PaymentFields.Length > 0)
                            {
                                foreach (var item in payment.PaymentFields)
                                {
                                    switch (item.FieldName.ToUpper())
                                    {
                                        case "ISSNAME":
                                            paymentitem.BankName = item.FieldValue;
                                            break;
                                        case "ISSCTRY":
                                            paymentitem.BranchNo = item.FieldValue;
                                            break;
                                    }
                                }
                            }

                            paymentitem.AccountNo = payment.AccountNumber;
                            paymentitem.AccountNoID = payment.AccountNumberID.ToString();


                            context.T_PaymentCurrentUpdate.Add(paymentitem);

                            #endregion
                        }

                        foreach (com.airasia.acebooking.Passenger pax in booking.Passengers)
                        {
                            #region add Passenger
                            Guid paxid = Guid.NewGuid();
                            var paxitem = new T_PassengerCurrentUpdate
                            {
                                PassengerTId = paxid,
                                TransactionId = tid,
                                PassengerId = pax.PassengerID,
                                FirstName = pax.Names[0].FirstName,
                                LastName = pax.Names[0].LastName,
                                Title = pax.Names[0].Title,
                                PaxType = pax.PassengerTypeInfo.PaxType,
                                ABBNo = null,
                                Create_By = userid,
                                Create_Date = DateTime.Now,
                                Update_By = userid,
                                Update_Date = DateTime.Now
                            };
                            context.T_PassengerCurrentUpdate.Add(paxitem);

                            if (pax.PassengerFees != null)
                            {
                                foreach (com.airasia.acebooking.PassengerFee paxfee in pax.PassengerFees)
                                {
                                    long feeno = long.Parse(pax.PassengerID.ToString() + paxfee.FeeNumber.ToString());
                                    Guid paxfeeid = Guid.NewGuid();
                                    var paxfeeitem = new T_PassengerFeeCurrentUpdate
                                    {
                                        PassengerFeeTId = paxfeeid,
                                        PassengerTId = paxid,
                                        PassengerId = pax.PassengerID,
                                        PassengerFeeNo = feeno,
                                        FeeCreateDate = paxfee.CreatedDate,
                                        ABBNo = null,
                                        Create_By = userid,
                                        Create_Date = DateTime.Now,
                                        Update_By = userid,
                                        Update_Date = DateTime.Now
                                    };
                                    context.T_PassengerFeeCurrentUpdate.Add(paxfeeitem);

                                    foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfee.ServiceCharges)
                                    {
                                        string servno = pax.PassengerID.ToString() + paxfee.FeeNumber.ToString() + servchgr.ChargeCode.Trim() + servchgr.TicketCode.Trim() + servchgr.Amount.ToString().Trim();
                                        string chgrtype = servchgr.ChargeType.ToString();
                                        string coltype = servchgr.CollectType.ToString();
                                        var paxfeesvritem = new T_PassengerFeeServiceChargeCurrentUpdate
                                        {
                                            PassengerFeeServiceChargeTId = Guid.NewGuid(),
                                            PassengerFeeTId = paxfeeid,
                                            PassengerId = pax.PassengerID,
                                            PassengerFeeNo = feeno,
                                            FeeCreateDate = paxfee.CreatedDate,
                                            ServiceChargeNo = servno,
                                            ChargeType = chgrtype,
                                            CollectType = coltype,
                                            ChargeCode = servchgr.ChargeCode,
                                            TicketCode = servchgr.TicketCode,
                                            BaseCurrencyCode = (servchgr.BaseCurrencyCode == null) ? "" : servchgr.BaseCurrencyCode,
                                            CurrencyCode = servchgr.CurrencyCode,
                                            Amount = servchgr.Amount,
                                            BaseAmount = servchgr.BaseAmount,
                                            ChargeDetail = servchgr.ChargeDetail,
                                            ForeignCurrencyCode = servchgr.ForeignCurrencyCode,
                                            ForeignAmount = servchgr.ForeignAmount,
                                            ABBNo = null,
                                            Create_By = userid,
                                            Create_Date = DateTime.Now,
                                            Update_By = userid,
                                            Update_Date = DateTime.Now,
                                            SEQ = SEQ_PassSer++
                                        };
                                        context.T_PassengerFeeServiceChargeCurrentUpdate.Add(paxfeesvritem);
                                    }
                                }

                            }


                            #endregion

                            
                        }

                        foreach (com.airasia.acebooking.Journey jou in booking.Journeys)
                        {

                            foreach (com.airasia.acebooking.Segment seq in jou.Segments)
                            {
                                #region add Segment
                                Guid seqid = Guid.NewGuid();
                                string seqmentNo = seq.DepartureStation.Trim() + seq.ArrivalStation.Trim() + seq.FlightDesignator.CarrierCode.Trim() + seq.FlightDesignator.FlightNumber.Trim();
                                var segitem = new T_SegmentCurrentUpdate
                                {
                                    SegmentTId = seqid,
                                    TransactionId = tid,
                                    SeqmentNo = seqmentNo,
                                    DepartureStation = seq.DepartureStation,
                                    ArrivalStation = seq.ArrivalStation,
                                    STD = seq.STD,
                                    STA = seq.STA,
                                    CarrierCode = seq.FlightDesignator.CarrierCode,
                                    FlightNumber = seq.FlightDesignator.FlightNumber,
                                    ABBNo = null,
                                    Create_By = userid,
                                    Create_Date = DateTime.Now,
                                    Update_By = userid,
                                    Update_Date = DateTime.Now
                                };
                                context.T_SegmentCurrentUpdate.Add(segitem);

                                foreach (com.airasia.acebooking.Fare fare in seq.Fares)
                                {
                                    foreach (com.airasia.acebooking.PaxFare paxfare in fare.PaxFares)
                                    {
                                        Guid _paxfareid = Guid.NewGuid();
                                        string seqmentpaxNo = seqmentNo + paxfare.PaxType.Trim();
                                        var paxfareitem = new T_PaxFareCurrentUpdate
                                        {
                                            PaxFareTId = _paxfareid,
                                            SegmentTId = seqid,
                                            SeqmentNo = seqmentNo,
                                            SeqmentPaxNo = seqmentpaxNo,
                                            PaxType = paxfare.PaxType,
                                            ABBNo = null,
                                            Create_By = userid,
                                            Create_Date = DateTime.Now,
                                            Update_By = userid,
                                            Update_Date = DateTime.Now
                                        };
                                        context.T_PaxFareCurrentUpdate.Add(paxfareitem);

                                        foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfare.ServiceCharges)
                                        {
                                            var pax = booking.Passengers.Where(x => x.PassengerTypeInfo.PaxType == paxfare.PaxType).ToList();

                                            //foreach (com.airasia.acebooking.Passenger paxitm in pax)
                                            //{
                                                string serviceChargeNo = seqmentpaxNo + servchgr.ChargeCode.Trim() + servchgr.TicketCode.Trim() + servchgr.Amount;
                                                string chgrtype = servchgr.ChargeType.ToString();
                                                string coltype = servchgr.CollectType.ToString();
                                                servchgr.BaseCurrencyCode = "";
                                                var bookingsvritem = new T_BookingServiceChargeCurrentUpdate
                                                {
                                                    BookingServiceChargeTId = Guid.NewGuid(),
                                                    PaxFareTId = _paxfareid,
                                                    SeqmentNo = seqmentNo,
                                                    SeqmentPaxNo = seqmentpaxNo,
                                                    ServiceChargeNo = serviceChargeNo,
                                                    //PassengerId = paxitm.PassengerID,
                                                    ChargeType = chgrtype,
                                                    CollectType = coltype,
                                                    ChargeCode = servchgr.ChargeCode,
                                                    TicketCode = servchgr.TicketCode,
                                                    CurrencyCode = servchgr.CurrencyCode,
                                                    Amount = servchgr.Amount,
                                                    BaseAmount = servchgr.BaseAmount,
                                                    ChargeDetail = servchgr.ChargeDetail,
                                                    ForeignCurrencyCode = servchgr.ForeignCurrencyCode,
                                                    FareCreateDate = seq.SalesDate,
                                                    ForeignAmount = servchgr.ForeignAmount,
                                                    ABBNo = null,
                                                    Create_By = userid,
                                                    Create_Date = DateTime.Now,
                                                    Update_By = userid,
                                                    Update_Date = DateTime.Now,
                                                    SEQ = SEQ_BookSer++,
                                                    QTY = pax.Count

                                                };
                                                context.T_BookingServiceChargeCurrentUpdate.Add(bookingsvritem);
                                            //}
                                            
                                        }
                                    }
                                }

                                #endregion
                            }
                        }

                        context.SaveChanges();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                    //if (ex is Exception)
                    //{
                    //}
                    //if (ex is DbEntityValidationException)
                    //{

                    //    foreach (var eve in (ex as DbEntityValidationException).EntityValidationErrors)
                    //    {
                    //        string msgh = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    //        foreach (var ve in eve.ValidationErrors)
                    //        {
                    //            string msgd = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                    //            ve.PropertyName, ve.ErrorMessage);

                    //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                    //                ve.PropertyName, ve.ErrorMessage);
                    //        }
                    //    }
                    //}
                }
            }
        }

        public bool LoadToDBNoneExist(Guid existsId, com.airasia.acebooking.Booking booking, string userid)
        {
            bool hasnew = false;
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    using (var context = new POSINVEntities())
                    {
                        Guid tid = existsId;

                        #region booking
                        string paxcount = booking.PaxCount.ToString();
                        string status = booking.BookingInfo.BookingStatus.ToString();
                        string Total = booking.BookingSum.TotalCost.ToString();
                        
                        var original = context.T_BookingCurrentUpdate.Where(x => x.TransactionId == tid).FirstOrDefault();
                        if (original.PaxCount != paxcount || original.TotalCost != Total)
                        {
                            hasnew = true;
                            original.PaxCount = paxcount;
                            original.BookingStatus = status;
                            original.TotalCost = Total;
                            original.Update_By = userid;
                            original.Update_Date = DateTime.Now;
                            var entry = context.Entry(original);
                            entry.State = EntityState.Modified;
                            entry.Property(e => e.PaxCount).IsModified = true;
                            entry.Property(e => e.BookingStatus).IsModified = true;
                            entry.Property(e => e.TotalCost).IsModified = true;
                            entry.Property(e => e.Update_By).IsModified = true;
                            entry.Property(e => e.Update_Date).IsModified = true;
                        }
                        #endregion

                        foreach (com.airasia.acebooking.Payment payment in booking.Payments)
                        {
                            var originalpay = context.T_PaymentCurrentUpdate.Where(x => x.PaymentID == payment.PaymentID).FirstOrDefault();
                            if (originalpay == null)
                            {
                                hasnew = true;
                                #region add payment

                                string ReferenceType = payment.ReferenceType.ToString();
                                string PaymentMethodType = payment.PaymentMethodType.ToString();
                                string PaymentStatus = payment.Status.ToString();
                                var paymentitem = new T_PaymentCurrentUpdate
                                {
                                    PaymentTId = Guid.NewGuid(),
                                    TransactionId = tid,
                                    PaymentID = payment.PaymentID,
                                    ReferenceType = ReferenceType,
                                    ReferenceID = payment.ReferenceID,
                                    PaymentMethodType = PaymentMethodType,
                                    PaymentMethodCode = payment.PaymentMethodCode,
                                    CurrencyCode = payment.CurrencyCode,
                                    PaymentAmount = payment.PaymentAmount,
                                    CollectedCurrencyCode = payment.CollectedCurrencyCode,
                                    CollectedAmount = payment.CollectedAmount,
                                    QuotedCurrencyCode = payment.QuotedCurrencyCode,
                                    QuotedAmount = payment.QuotedAmount,
                                    Status = PaymentStatus,
                                    ParentPaymentID = payment.ParentPaymentID,
                                    AgentCode = payment.PointOfSale.AgentCode,
                                    OrganizationCode = payment.PointOfSale.OrganizationCode,
                                    DomainCode = payment.PointOfSale.DomainCode,
                                    LocationCode = payment.PointOfSale.LocationCode,
                                    ApprovalDate = payment.ApprovalDate,
                                    ABBNo = null,
                                    Create_By = userid,
                                    Create_Date = DateTime.Now,
                                    Update_By = userid,
                                    Update_Date = DateTime.Now
                                };
                                context.T_PaymentCurrentUpdate.Add(paymentitem);

                                #endregion
                            }
                        }

                        foreach (com.airasia.acebooking.Passenger pax in booking.Passengers)
                        {
                            #region add Passenger
                            Guid paxid = Guid.NewGuid();
                            var originalpas = context.T_PassengerCurrentUpdate.Where(x => x.PassengerId == pax.PassengerID).ToList();
                            if (originalpas.Count() == 0)
                            {
                                hasnew = true;
                                var paxitem = new T_PassengerCurrentUpdate
                                {
                                    PassengerTId = paxid,
                                    TransactionId = tid,
                                    PassengerId = pax.PassengerID,
                                    FirstName = pax.Names[0].FirstName,
                                    LastName = pax.Names[0].LastName,
                                    Title = pax.Names[0].Title,
                                    PaxType = pax.PassengerTypeInfo.PaxType,
                                    ABBNo = null,
                                    Create_By = userid,
                                    Create_Date = DateTime.Now,
                                    Update_By = userid,
                                    Update_Date = DateTime.Now
                                };
                                context.T_PassengerCurrentUpdate.Add(paxitem);
                            }
                            else
                            {
                                paxid = originalpas[0].PassengerTId;
                            }

                            if (pax.PassengerFees != null)
                            {
                                foreach (com.airasia.acebooking.PassengerFee paxfee in pax.PassengerFees)
                                {
                                    long feeno = long.Parse(pax.PassengerID.ToString() + paxfee.FeeNumber.ToString());
                                    Guid paxfeeid = Guid.NewGuid();

                                    var originalpaxfee = context.T_PassengerFeeCurrentUpdate.Where(x => x.PassengerFeeNo == feeno).ToList();
                                    if (originalpaxfee.Count() == 0)
                                    {
                                        hasnew = true;
                                        var paxfeeitem = new T_PassengerFeeCurrentUpdate
                                        {
                                            PassengerFeeTId = paxfeeid,
                                            PassengerTId = paxid,
                                            PassengerId = pax.PassengerID,
                                            PassengerFeeNo = feeno,
                                            FeeCreateDate = paxfee.CreatedDate,
                                            ABBNo = null,
                                            Create_By = userid,
                                            Create_Date = DateTime.Now,
                                            Update_By = userid,
                                            Update_Date = DateTime.Now
                                        };
                                        context.T_PassengerFeeCurrentUpdate.Add(paxfeeitem);
                                    }
                                    else
                                    {
                                        paxfeeid = originalpaxfee[0].PassengerFeeTId;
                                    }

                                    foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfee.ServiceCharges)
                                    {
                                        string servno = pax.PassengerID.ToString() + paxfee.FeeNumber.ToString() + servchgr.ChargeCode.Trim() + servchgr.TicketCode.Trim() + servchgr.Amount.ToString().Trim();
                                        string chgrtype = servchgr.ChargeType.ToString();
                                        string coltype = servchgr.CollectType.ToString();

                                        var originalserpaxfee = context.T_PassengerFeeServiceChargeCurrentUpdate.Where(x => x.ServiceChargeNo == servno).ToList();
                                        if (originalserpaxfee.Count() == 0)
                                        {
                                            hasnew = true;
                                            var paxfeesvritem = new T_PassengerFeeServiceChargeCurrentUpdate
                                            {
                                                PassengerFeeServiceChargeTId = Guid.NewGuid(),
                                                PassengerFeeTId = paxfeeid,
                                                PassengerId = pax.PassengerID,
                                                PassengerFeeNo = feeno,
                                                FeeCreateDate = paxfee.CreatedDate,
                                                ServiceChargeNo = servno,
                                                ChargeType = chgrtype,
                                                CollectType = coltype,
                                                ChargeCode = servchgr.ChargeCode,
                                                TicketCode = servchgr.TicketCode,
                                                BaseCurrencyCode = (servchgr.BaseCurrencyCode == null) ? "" : servchgr.BaseCurrencyCode,
                                                CurrencyCode = servchgr.CurrencyCode,
                                                Amount = servchgr.Amount,
                                                BaseAmount = servchgr.BaseAmount,
                                                ChargeDetail = servchgr.ChargeDetail,
                                                ForeignCurrencyCode = servchgr.ForeignCurrencyCode,
                                                ForeignAmount = servchgr.ForeignAmount,
                                                ABBNo = null,
                                                Create_By = userid,
                                                Create_Date = DateTime.Now,
                                                Update_By = userid,
                                                Update_Date = DateTime.Now
                                            };
                                            context.T_PassengerFeeServiceChargeCurrentUpdate.Add(paxfeesvritem);
                                        }
                                    }
                                }

                            }


                            #endregion

                            
                        }

                        foreach (com.airasia.acebooking.Journey jou in booking.Journeys)
                        {

                            foreach (com.airasia.acebooking.Segment seq in jou.Segments)
                            {
                                #region add Segment
                                Guid seqid = Guid.NewGuid();
                                string seqmentNo = seq.DepartureStation.Trim() + seq.ArrivalStation.Trim() + seq.FlightDesignator.CarrierCode.Trim() + seq.FlightDesignator.FlightNumber.Trim();
                                var originalseq = context.T_SegmentCurrentUpdate.Where(x => x.SeqmentNo == seqmentNo).ToList();
                                if (originalseq.Count() == 0)
                                {
                                    hasnew = true;
                                    var segitem = new T_SegmentCurrentUpdate
                                    {
                                        SegmentTId = seqid,
                                        TransactionId = tid,
                                        SeqmentNo = seqmentNo,
                                        DepartureStation = seq.DepartureStation,
                                        ArrivalStation = seq.ArrivalStation,
                                        STD = seq.STD,
                                        STA = seq.STA,
                                        CarrierCode = seq.FlightDesignator.CarrierCode,
                                        FlightNumber = seq.FlightDesignator.FlightNumber,
                                        ABBNo = null,
                                        Create_By = userid,
                                        Create_Date = DateTime.Now,
                                        Update_By = userid,
                                        Update_Date = DateTime.Now
                                    };
                                    context.T_SegmentCurrentUpdate.Add(segitem);
                                }
                                else
                                {
                                    seqid = originalseq[0].SegmentTId;
                                }

                                foreach (com.airasia.acebooking.Fare fare in seq.Fares)
                                {
                                    foreach (com.airasia.acebooking.PaxFare paxfare in fare.PaxFares)
                                    {
                                        Guid _paxfareid = Guid.NewGuid();
                                        string seqmentpaxNo = seqmentNo + paxfare.PaxType.Trim();

                                        var originalseqfare = context.T_PaxFareCurrentUpdate.Where(x => x.SeqmentPaxNo == seqmentpaxNo).ToList();
                                        if (originalseqfare.Count() == 0)
                                        {
                                            hasnew = true;
                                            var paxfareitem = new T_PaxFareCurrentUpdate
                                            {
                                                PaxFareTId = _paxfareid,
                                                SegmentTId = seqid,
                                                SeqmentNo = seqmentNo,
                                                SeqmentPaxNo = seqmentpaxNo,
                                                PaxType = paxfare.PaxType,
                                                ABBNo = null,
                                                Create_By = userid,
                                                Create_Date = DateTime.Now,
                                                Update_By = userid,
                                                Update_Date = DateTime.Now
                                            };
                                            context.T_PaxFareCurrentUpdate.Add(paxfareitem);
                                        }
                                        else
                                        {
                                            _paxfareid = originalseqfare[0].PaxFareTId;
                                        }

                                        foreach (com.airasia.acebooking.BookingServiceCharge servchgr in paxfare.ServiceCharges)
                                        {
                                            var pax = booking.Passengers.Where(x => x.PassengerTypeInfo.PaxType == paxfare.PaxType).ToList();

                                            foreach (com.airasia.acebooking.Passenger paxitm in pax)
                                            {
                                                string serviceChargeNo = seqmentpaxNo + servchgr.ChargeCode.Trim() + servchgr.TicketCode.Trim() + servchgr.Amount;
                                                string chgrtype = servchgr.ChargeType.ToString();
                                                string coltype = servchgr.CollectType.ToString();
                                                servchgr.BaseCurrencyCode = "";

                                                var originalserseqfare = context.T_BookingServiceChargeCurrentUpdate.Where(x => x.ServiceChargeNo == serviceChargeNo).ToList();
                                                if (originalserseqfare.Count() == 0)
                                                {
                                                    hasnew = true;
                                                    var bookingsvritem = new T_BookingServiceChargeCurrentUpdate
                                                    {
                                                        BookingServiceChargeTId = Guid.NewGuid(),
                                                        PaxFareTId = _paxfareid,
                                                        SeqmentNo = seqmentNo,
                                                        SeqmentPaxNo = seqmentpaxNo,
                                                        ServiceChargeNo = serviceChargeNo,
                                                        PassengerId = paxitm.PassengerID,
                                                        ChargeType = chgrtype,
                                                        CollectType = coltype,
                                                        ChargeCode = servchgr.ChargeCode,
                                                        TicketCode = servchgr.TicketCode,
                                                        CurrencyCode = servchgr.CurrencyCode,
                                                        Amount = servchgr.Amount,
                                                        BaseAmount = servchgr.BaseAmount,
                                                        ChargeDetail = servchgr.ChargeDetail,
                                                        ForeignCurrencyCode = servchgr.ForeignCurrencyCode,
                                                        ForeignAmount = servchgr.ForeignAmount,
                                                        ABBNo = null,
                                                        Create_By = userid,
                                                        Create_Date = DateTime.Now,
                                                        Update_By = userid,
                                                        Update_Date = DateTime.Now
                                                    };
                                                    context.T_BookingServiceChargeCurrentUpdate.Add(bookingsvritem);
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion
                            }
                        }

                        context.SaveChanges();
                    }

                    scope.Complete();

                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return hasnew;
        }


        public com.airasia.acebooking.Booking GetDataXML(string pnrno)
        {
            com.airasia.acebooking.Booking booking = null;
            XmlSerializer serializer = new XmlSerializer(typeof(com.airasia.acebooking.Booking));
            string xmlpath = @"C:\XML\" + pnrno + ".xml";
            try
            {
                XmlReader reader = XmlReader.Create(xmlpath);
                booking = (com.airasia.acebooking.Booking)serializer.Deserialize(reader);
                reader.Close();
                reader.Dispose();
            }
            catch (Exception)
            {
                try
                {
                    XmlReader reader = XmlReader.Create(xmlpath);
                    reader.MoveToContent();
                    string xml = reader.ReadOuterXml();
                    reader.Close();
                    reader.Dispose();
                    string processedValue = "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">";
                    xml = xml.Replace(processedValue, "");
                    processedValue = "<s:Body>";
                    xml = xml.Replace(processedValue, "");
                    processedValue = "<GetBookingResponse xmlns=\"http://tempuri.org/\">";
                    xml = xml.Replace(processedValue, "");


                    processedValue = "</GetBookingResponse>";
                    xml = xml.Replace(processedValue, "");
                    processedValue = "</s:Body>";
                    xml = xml.Replace(processedValue, "");
                    processedValue = "</s:Envelope>";
                    xml = xml.Replace(processedValue, "");

                    processedValue = "GetBookingResult";
                    xml = xml.Replace(processedValue, "Booking");

                    MemoryStream ms = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(xml));
                    XmlReader myReader = XmlReader.Create(ms);
                    //XmlReaderSettings settings = new XmlReaderSettings();
                    myReader.MoveToContent();

                    booking = (com.airasia.acebooking.Booking)serializer.Deserialize(myReader);
                    myReader.Close();
                    myReader.Dispose();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return booking;
        }


        public XmlReader FixUpReader(XmlReader reader)
        {
            reader.MoveToContent();

            string xml = reader.ReadOuterXml();

            string processedValue = "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">";
            xml = xml.Replace(processedValue, "");
            processedValue = "<s:Body>";
            xml = xml.Replace(processedValue, "");
            processedValue = "<GetBookingResponse xmlns=\"http://tempuri.org/\">";
            xml = xml.Replace(processedValue, "");


            processedValue = "</GetBookingResponse>";
            xml = xml.Replace(processedValue, "");
            processedValue = "</s:Body>";
            xml = xml.Replace(processedValue, "");
            processedValue = "</s:Envelope>";
            xml = xml.Replace(processedValue, "");

            processedValue = "GetBookingResult";
            xml = xml.Replace(processedValue, "Booking");

            MemoryStream ms = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(xml));
            //XmlReaderSettings settings = new XmlReaderSettings();

            XmlReader myReader = XmlReader.Create(ms);
            myReader.MoveToContent();
            return myReader;
        }

    }
}
