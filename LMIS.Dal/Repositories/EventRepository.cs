using LMIS.Dal.Entity;
using LMIS.Dal.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Helpers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class EventRepository : IEventRepository
    {
        public List<EventVm> ListInternal()
        {
            List<Event> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Events
                    .Include(r => r.EventsDetails)
                    .Where(r => r.IsDeleted == null && r.IsInternal == true)
                    .ToList();
            }

            return ds.Select(r => new EventVm()
            {
                EventId = (long)r.EventId,
                ContactId = (long)r.OrganizationContactID,
                Title = r.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventTitle)).ToList(),
                Address = r.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventAddress)).ToList(),
                StartDate = r.StartDate.AsUtc(),
                EndDate = r.EndDate.AsUtc(),
                Type = r.EventTypeID,
                Price = r.Price,
                ContactAddress = r.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventContactAddress)).ToList(),
                ContactTelephone = r.EventContactTelephone,
                ContactWebsite = r.EventContactWebsite,
                FilePath = r.UploadPath,
                IsInternal = r.IsInternal == true,
                IsInformal = r.IsInformal == true,
                Approval = (Approval)r.IsApproved
            })
            .ToList();
        }

        public List<EventVm> ListByOrgContact(long contactId)
        {
            List<Event> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Events
                    .Include(r => r.EventsDetails)
                    .Where(r => r.IsDeleted == null && r.OrganizationContactID == contactId)
                    .ToList();
            }

            return ds.Select(r => new EventVm()
            {
                EventId = (long)r.EventId,
                ContactId = (long)r.OrganizationContactID,
                Title = r.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventTitle)).ToList(),
                Address = r.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventAddress)).ToList(),
                StartDate = r.StartDate.AsUtc(),
                EndDate = r.EndDate.AsUtc(),
                Type = r.EventTypeID,
                Price = r.Price,
                ContactAddress = r.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventContactAddress)).ToList(),
                ContactTelephone = r.EventContactTelephone,
                ContactWebsite = r.EventContactWebsite,
                FilePath = r.UploadPath,
                IsInternal = r.IsInternal == true,
                IsInformal = r.IsInformal == true,
                Approval = (Approval)r.IsApproved
            })
            .ToList();
        }

        public List<CalendarVM> List(int langId)
        {
            List<CalendarVM> result;
            List<Event> resultEvent;
            List<Opportunity> resultOpportunity;
     
           
            using (var db = new LMISEntities())
            {
                 resultEvent =db.Events.Include(r => r.EventsDetails).Where(r => r.IsDeleted == null && r.IsApproved == 2 && r.StartDate.Year >= DateTime.Now.Year).ToList();
                        
                 resultOpportunity=db.Opportunities.Include(r => r.OpportunitiesDetails).Where(r =>r.IsDeleted == null && r.Is_Approved == 2 &&r.StartDate.Year >= DateTime.Now.Year) .ToList();

                 var resultTraning = db.TrainingOffers.Where(r => r.IsDeleted == null && r.Is_Approved == 2 && r.StartDate.Year >= DateTime.Now.Year).Select(s => new { s.TrainingOfferID, Title = SqlUdf.SubCodeName(s.CourseNameID, langId), s.StartDate, s.EndDate }).ToList();
              result = resultEvent.Select(r => new CalendarVM { Id = r.EventId, Title = r.EventsDetails.Where(w => w.LanguageID == langId).Select(d => d.EventTitle).FirstOrDefault(), StartDate = r.StartDate.AsUtc(), EndDate = r.EndDate.AsUtc(), Type = 1 }).ToList();
              result.AddRange(resultOpportunity.Select(r => new CalendarVM { Id = r.OpportunityID, Title = r.OpportunitiesDetails.Where(w => w.LanguageID == langId).Select(d => d.OpportunityTitle).FirstOrDefault(), StartDate = r.StartDate.AsUtc(), EndDate = r.EndDate.AsUtc(), Type = 2 }).ToList());
              result.AddRange(resultTraning.Select(r => new CalendarVM { Id = r.TrainingOfferID, Title =r.Title , StartDate = r.StartDate.AsUtc(), EndDate = r.EndDate.AsUtc(), Type = 3 }).ToList());
          

            }
              return result;
        }

        public long Post(ref EventVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.EventId;

                    if (id > 0) //Update
                    {
                        var tr = db.Events
                            .Where(r => r.IsDeleted == null && r.EventId == id)
                            .ToList().Single();

                        tr.OrganizationContactID = vm.ContactId;
                        tr.StartDate = vm.StartDate.AsUtc();
                        tr.EndDate = vm.EndDate.AsUtc();
                        tr.EventTypeID = vm.Type;
                        tr.Price = vm.Price;
                        tr.EventContactTelephone = vm.ContactTelephone;
                        tr.EventContactWebsite = vm.ContactWebsite;
                        tr.UploadPath = vm.FilePath;
                        tr.IsInternal = vm.IsInternal;
                        tr.IsInformal = vm.IsInformal;
                        tr.IsApproved = (byte) vm.Approval;
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;

                        //Delete detail records
                        var dr = db.EventsDetails
                            .Where(r => r.EventId == id)
                            .ToList();

                        db.EventsDetails.RemoveRange(dr);
                    }
                    else //Insert
                    {
                        var tr = new Event
                        {
                            OrganizationContactID = vm.ContactId,
                            StartDate = vm.StartDate.AsUtc(),
                            EndDate = vm.EndDate.AsUtc(),
                            EventTypeID = vm.Type,
                            Price = vm.Price,
                            EventContactTelephone = vm.ContactTelephone,
                            EventContactWebsite = vm.ContactWebsite,
                            UploadPath = vm.FilePath,
                            IsInternal = vm.IsInternal,
                            IsInformal = vm.IsInformal,
                            IsApproved = (byte) vm.Approval,
                            PostUserID = userId,
                            PostDate = DateTime.UtcNow,
                            RejectReason = ""
                        };

                        db.Events.Add(tr);
                        db.SaveChanges();

                        vm.EventId = (long) tr.EventId;
                    }

                    //Insert detail records
                    var ds = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.Title},
                            {"c2", vm.Address},
                            {"c3", vm.ContactAddress}
                        });

                    foreach (var r in ds)
                    {
                        var dr = new EventsDetail()
                        {
                            EventId = vm.EventId,
                            LanguageID = r["c1"].L,
                            EventTitle = r["c1"].T,
                            EventAddress = r["c2"].T,
                            EventContactAddress = r["c3"].T
                        };
                        db.EventsDetails.Add(dr);
                    }

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.EventId;
        }

        public void Delete(long id, string userId, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.Events
                    .Where(r => r.EventId == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteReason = reason.Limit(500);
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }

        public bool IsDuplicate(ref EventVm vm)
        {
            var id = vm.EventId;
            var startDate = vm.StartDate;
            var titles = vm.Title.ToLowerTrimmedStrings();

            using (var db = new LMISEntities())
            {
                return db.Events
                    .Include(r => r.EventsDetails)
                    .Any(r => r.IsDeleted == null && r.EventId != id && r.StartDate == startDate &&
                         r.EventsDetails
                         .Any(d => titles.Contains(d.EventTitle.Trim().ToLower())));
            }
        }

        public EventVm Get(long id, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.Events
                    .Include(r => r.EventsDetails)
                    .Where(r => r.IsDeleted == null && r.EventId == id)
                    .Select(r => new
                    {
                        e = r,
                        TypeStr = SqlUdf.SubCodeName(r.EventTypeID, langId)
                    })
                    .ToList()
                    .Select(r => new EventVm()
                    {
                        EventId = (long)r.e.EventId,
                        ContactId = (long)r.e.OrganizationContactID,
                        Title = r.e.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventTitle)).ToList(),
                        Address = r.e.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventAddress)).ToList(),
                        StartDate = r.e.StartDate.AsUtc(),
                        EndDate = r.e.EndDate.AsUtc(),
                        Type = r.e.EventTypeID,
                        TypeStr = r.TypeStr,
                        Price = r.e.Price,
                        ContactAddress = r.e.EventsDetails.Select(d => new LocalString(d.LanguageID, d.EventContactAddress)).ToList(),
                        ContactTelephone = r.e.EventContactTelephone,
                        ContactWebsite = r.e.EventContactWebsite,
                        FilePath = r.e.UploadPath,
                        IsInternal = r.e.IsInternal == true,
                        IsInformal = r.e.IsInformal == true,
                        Approval = (Approval)r.e.IsApproved,
                        RejectReason = r.e.RejectReason
                    })
                    .SingleOrDefault();
            }
        }

        public bool IsInternal(long id)
        {
            using (var db = new LMISEntities())
            {
                return db.Events
                    .Any(r => r.IsDeleted == null && r.EventId == id && r.IsInternal == true);
            }
        }

        public bool IsByOrgContact(long id, long contactId)
        {
            using (var db = new LMISEntities())
            {
                return db.Events
                    .Any(r => r.IsDeleted == null && r.EventId == id && r.OrganizationContactID == contactId);
            }
        }

        public void Approve(string adminId, long reqKey, long eventId, bool approved, string reason)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == eventId
                            && r.RequestType == "02900005" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                    var tr = db.Events
                        .Where(r => r.EventId == eventId)
                        .ToList().Single();

                    if (approved)
                        tr.IsApproved = (byte)Approval.Approved;
                    else
                    {
                        tr.IsApproved = (byte)Approval.Rejected;
                        tr.RejectReason = reason.Limit(500);
                    }

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }
        }
       
    }
}