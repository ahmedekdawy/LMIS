using LMIS.Dal.Entity;
using LMIS.Dal.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        public List<OpportunityVm> List(bool? informal = null)
        {
            using (var db = new LMISEntities())
            {
                var ds = db.Opportunities
                    .Include(r => r.OpportunitiesDetails)
                    .Where(r => r.IsDeleted == null && r.Is_Approved == (byte)Approval.Approved
                        && r.EndDate >= DateTime.UtcNow && (informal == null || r.IsInformal == informal))
                    .ToList();

                return ds.Select(r => new OpportunityVm()
                {
                    OpportunityId = (long)r.OpportunityID,
                    ContactId = (long)r.OrganizationContactID,
                    ContactName = r.OrganizationContact_Info.OrganizationContactInfoDetails.Select(d => new LocalString(d.LanguageID, d.ContactFullName)).ToList(),
                    OrganizationName = r.OrganizationContact_Info.OrganizationDetail.OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList(),
                    Title = r.OpportunitiesDetails.Select(d => new LocalString(d.LanguageID, d.OpportunityTitle)).ToList(),
                    FilePath = r.OpportunityFilePath,
                    StartDate = r.StartDate.AsUtc(),
                    EndDate = r.EndDate.AsUtc(),
                    Approval = (Approval)r.Is_Approved,
                    IsInformal = r.IsInformal,
                    IsInternal = r.IsInternal == true
                })
                .ToList();
            }
        }

        public List<OpportunityVm> ListInternal()
        {
            using (var db = new LMISEntities())
            {
                db.Database.Log = message => System.Diagnostics.Debug.Write(message);

                var ds = db.Opportunities
                    .Include(r => r.OpportunitiesDetails)
                    .Where(r => r.IsDeleted == null && r.IsInternal == true)
                    .ToList();

                return ds.Select(r => new OpportunityVm()
                {
                    OpportunityId = (long)r.OpportunityID,
                    ContactId = (long)r.OrganizationContactID,
                    ContactName = r.OrganizationContact_Info.OrganizationContactInfoDetails.Select(d => new LocalString(d.LanguageID, d.ContactFullName)).ToList(),
                    OrganizationName = r.OrganizationContact_Info.OrganizationDetail.OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList(),
                    Title = r.OpportunitiesDetails.Select(d => new LocalString(d.LanguageID, d.OpportunityTitle)).ToList(),
                    FilePath = r.OpportunityFilePath,
                    StartDate = r.StartDate.AsUtc(),
                    EndDate = r.EndDate.AsUtc(),
                    Approval = (Approval)r.Is_Approved,
                    IsInformal = r.IsInformal,
                    IsInternal = r.IsInternal == true
                })
                .ToList();
            }
        }

        public List<OpportunityVm> ListByOrgContact(long contactId)
        {
            List<Opportunity> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Opportunities
                    .Include(r => r.OpportunitiesDetails)
                    .Where(r => r.IsDeleted == null && r.OrganizationContactID == contactId)
                    .ToList();
            }

            return ds.Select(r => new OpportunityVm()
            {
                OpportunityId = (long)r.OpportunityID,
                ContactId = (long)r.OrganizationContactID,
                Title = r.OpportunitiesDetails.Select(d => new LocalString(d.LanguageID, d.OpportunityTitle)).ToList(),
                FilePath = r.OpportunityFilePath,
                StartDate = r.StartDate.AsUtc(),
                EndDate = r.EndDate.AsUtc(),
                Approval = (Approval)r.Is_Approved,
                IsInformal = r.IsInformal,
                IsInternal = r.IsInternal == true
            })
            .ToList();
        }

        public long Post(ref OpportunityVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.OpportunityId;

                    if (id > 0)     //Update
                    {
                        var tr = db.Opportunities
                            .Where(r => r.IsDeleted == null && r.OpportunityID == id)
                            .ToList().Single();

                        tr.OrganizationContactID = vm.ContactId;
                        tr.OpportunityFilePath = vm.FilePath;
                        tr.StartDate = vm.StartDate.AsUtc();
                        tr.EndDate = vm.EndDate.AsUtc();
                        tr.Is_Approved = (byte)vm.Approval;
                        tr.IsInformal = vm.IsInformal;
                        tr.IsInternal = vm.IsInternal;
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;

                        //Delete detail records
                        var dr = db.OpportunitiesDetails
                            .Where(r => r.OpportunityID == id)
                            .ToList();

                        db.OpportunitiesDetails.RemoveRange(dr);
                    }
                    else            //Insert
                    {
                        var tr = new Opportunity
                        {
                            OrganizationContactID = vm.ContactId,
                            OpportunityFilePath = vm.FilePath,
                            StartDate = vm.StartDate.AsUtc(),
                            EndDate = vm.EndDate.AsUtc(),
                            Is_Approved = (byte)vm.Approval,
                            IsInformal = vm.IsInformal,
                            IsInternal = vm.IsInternal,
                            PostUserID = userId,
                            PostDate = DateTime.UtcNow
                        };

                        db.Opportunities.Add(tr);
                        db.SaveChanges();

                        vm.OpportunityId = (long)tr.OpportunityID;
                    }

                    //Insert detail records
                    foreach (var r in vm.Title.ToLocalStrings())
                    {
                        var dr = new OpportunitiesDetail()
                        {
                            OpportunityID = vm.OpportunityId,
                            LanguageID = r.L,
                            OpportunityTitle = r.T
                        };
                        db.OpportunitiesDetails.Add(dr);
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

            return vm.OpportunityId;
        }

        public void Delete(long id, string userId, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.Opportunities
                    .Where(r => r.OpportunityID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteReason = reason.Limit(500);
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }

        public bool IsDuplicate(ref OpportunityVm vm)
        {
            var id = vm.OpportunityId;
            var startDate = vm.StartDate;
            var titles = vm.Title.ToLowerTrimmedStrings();

            using (var db = new LMISEntities())
            {
                return db.Opportunities
                    .Include(r => r.OpportunitiesDetails)
                    .Any(r => r.IsDeleted == null && r.OpportunityID != id && r.StartDate == startDate &&
                         r.OpportunitiesDetails
                         .Any(d => titles.Contains(d.OpportunityTitle.Trim().ToLower())));
            }
        }

        public OpportunityVm Get(long id)
        {
            List<Opportunity> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Opportunities
                    .Include(r => r.OpportunitiesDetails)
                    .Where(r => r.IsDeleted == null && r.OpportunityID == id)
                    .ToList();
            }

            return ds.Select(r => new OpportunityVm()
            {
                OpportunityId = (long)r.OpportunityID,
                ContactId = (long)r.OrganizationContactID,
                Title = r.OpportunitiesDetails.Select(d => new LocalString(d.LanguageID, d.OpportunityTitle)).ToList(),
                FilePath = r.OpportunityFilePath,
                StartDate = r.StartDate.AsUtc(),
                EndDate = r.EndDate.AsUtc(),
                Approval = (Approval)r.Is_Approved,
                RejectReason = r.RejectReason,
                IsInformal = r.IsInformal,
                IsInternal = r.IsInternal == true
            })
            .SingleOrDefault();
        }

        public bool IsInternal(long id)
        {
            using (var db = new LMISEntities())
            {
                return db.Opportunities
                    .Any(r => r.IsDeleted == null && r.OpportunityID == id && r.IsInternal == true);
            }
        }

        public bool IsByOrgContact(long id, long contactId)
        {
            using (var db = new LMISEntities())
            {
                return db.Opportunities
                    .Any(r => r.IsDeleted == null && r.OpportunityID == id && r.OrganizationContactID == contactId);
            }
        }

        public void Approve(string adminId, long reqKey, long oppId, bool approved, string reason)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == oppId
                            && r.RequestType == "02900004" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                    var tr = db.Opportunities
                        .Where(r => r.OpportunityID == oppId)
                        .ToList().Single();

                    if (approved)
                        tr.Is_Approved = (byte)Approval.Approved;
                    else
                    {
                        tr.Is_Approved = (byte)Approval.Rejected;
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