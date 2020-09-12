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
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private static readonly Func<TrainingOffer, IEnumerable<TrainingVm.TrainingSkill>, IEnumerable<TrainingVm.TrainingSkill>, TrainingVm> MapTrainingOffers = (a, b, c) =>
            new TrainingVm
            {
                Id = (long)a.TrainingOfferID,
                ContactId = (long)a.OrganizationContactID,
                PortalUserId = (long)a.PortalUsersID,
                Title = a.CourseNameID,
                NewTitle = a.TrainingOfferDetails.Select(d => new LocalString(d.LanguageID, d.OtherCours_Name)).ToList(),
                Description = a.TrainingOfferDetails.Select(d => new LocalString(d.LanguageID, d.CourseDescription)).ToList(),
                FileName = a.OutlineFile,
                Country = a.CountryID,
                City = a.CityID,
                Address = a.TrainingOfferDetails.Select(d => new LocalString(d.LanguageID, d.Address)).ToList(),
                Duration = a.CourseDurationPerDay,
                StartDate = a.StartDate.AsUtc(),
                EndDate = a.EndDate.AsUtc(),
                Seats = a.NoOfSeats,
                Cost = a.CourseCost,
                Occurrence = a.TrainingOfferOccurrences.Select(d => d.OccurrenceID).ToList(),
                TimeFrom = a.CourseTimeFrom.AsUtc(),
                TimeTo = a.CourseTimeTo.AsUtc(),
                TimeZone = a.TimeZone,
                Status = a.trainingStatus,
                Skills = b.ToList().Union(c.ToList()).ToList(),
                Approval = (Approval)a.Is_Approved,
                RejectReason = a.RejectReason
            };

        private static void PopulateRecord(ref TrainingOffer tr, ref TrainingVm vm)
        {
            tr.PortalUsersID = vm.PortalUserId;
            tr.OrganizationContactID = vm.ContactId;
            tr.CourseNameID = vm.Title;
            tr.OutlineFile = vm.FileName;
            tr.CourseDurationPerDay = vm.Duration;
            tr.CourseTimeFrom = vm.TimeFrom.AsUtc();
            tr.CourseTimeTo = vm.TimeTo.AsUtc();
            tr.TimeZone = vm.TimeZone;
            tr.CountryID = vm.Country;
            tr.CityID = vm.City;
            tr.NoOfSeats = vm.Seats;
            tr.CourseCost = vm.Cost;
            tr.StartDate = vm.StartDate.AsUtc();
            tr.EndDate = vm.EndDate.AsUtc();
            tr.trainingStatus = vm.Status;
            tr.Is_Approved = (byte)vm.Approval;
        }

        public List<TrainingVm> BriefByOrgContact(long contactId, long? id = null, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.TrainingOffers
                    .AsNoTracking()
                    .Where(r => r.IsDeleted == null
                        && r.OrganizationContactID == contactId
                        && (id == null || r.TrainingOfferID == id))
                    .Select(a => new
                    {
                        Offer = a,
                        TitleDesc = SqlUdf.SubCodeName(a.CourseNameID, langId),
                        Details = a.TrainingOfferDetails
                    })
                    .ToList()
                    .Select(a => new TrainingVm()
                    {
                        Id = (long)a.Offer.TrainingOfferID,
                        ContactId = (long)a.Offer.OrganizationContactID,
                        PortalUserId = (long)a.Offer.PortalUsersID,
                        Title = a.TitleDesc,
                        Status = a.Offer.trainingStatus,
                        Approval = (Approval)a.Offer.Is_Approved,
                        RejectReason = a.Offer.RejectReason,
                        NewTitle = a.Details.Select(d => new LocalString(d.LanguageID, d.OtherCours_Name)).ToList(),
                    })
                    .ToList();
            }
        }

        public List<TrainingVm> ListByOrgContact(long contactId, long? id = null, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                var q = db.TrainingOffers
                    .AsNoTracking()
                    .Where(r => r.IsDeleted == null
                        && (contactId == -1 || r.OrganizationContactID == contactId)
                        && (id == null || r.TrainingOfferID == id))
                    .Select(r => new
                        {
                            TrainingOffer = r,
                            Skills = r.TrainingSkillDetails.Select(a => new TrainingVm.TrainingSkill
                            {
                                IsNew = false,
                                Industry = new CodeSet {id = a.IndustryID, desc = SqlUdf.SubCodeName(a.IndustryID, langId)},
                                Level = new CodeSet {id = a.SkillLevelID, desc = SqlUdf.SubCodeName(a.SkillLevelID, langId)},
                                Skill = new CodeSet {id = a.SkillID, desc = SqlUdf.SubCodeName(a.SkillID, langId)},
                                Type = new CodeSet {id = a.SkillTypeID, desc = SqlUdf.SubCodeName(a.SkillTypeID, langId)}
                            }),
                            OtherSkills = r.TrainingOtherSkills.Select(a => new TrainingVm.TrainingSkill
                            {
                                IsNew = true,
                                Industry = new CodeSet {id = a.IndustryId, desc = SqlUdf.SubCodeName(a.IndustryId, langId)},
                                Level = new CodeSet {id = a.SkillLevelId, desc = SqlUdf.SubCodeName(a.SkillLevelId, langId)},
                                Skill = new CodeSet {id = null, desc = a.OtherSkill},
                                Type = new CodeSet {id = null, desc = null}
                            })
                        });

                return q.ToList().Select(r => MapTrainingOffers(r.TrainingOffer, r.Skills, r.OtherSkills)).ToList();
            }
        }

        public List<CodeSet> DetailApplicants(long contactId, long id, int langId = 1)
        {
            List<TrainingApply> ds;

            using (var db = new LMISEntities())
            {
                ds = db.TrainingApplies
                    .AsNoTracking()
                    .Include(r => r.TrainingOffer)
                    .Include(r => r.IndividualDetail.IndividualDetailsDets)
                    .Where(r => r.TrainingOffer.IsDeleted == null
                        && r.TrainingOffer.OrganizationContactID == contactId
                        && r.TrainingOffer.TrainingOfferID == (int)id)
                    .ToList();
            }

            return ds.Select(a => new CodeSet
            {
                id = a.IndPortalUserID.ToString(CultureInfo.InvariantCulture),
                desc = new GlobalString(a.IndividualDetail.IndividualDetailsDets
                    .Select(d => new LocalString(d.LanguageID, d.FirstName + " " + d.LastName))
                    .ToList()).ToLocalString((Language)langId, true).T
            }).ToList();
        }

        public long Post(ref TrainingVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.Id;

                    if (id > 0) //Update
                    {
                        var tr = db.TrainingOffers
                            .Where(r => r.IsDeleted == null && r.TrainingOfferID == id)
                            .ToList().Single();

                        PopulateRecord(ref tr, ref vm);
                        tr.UpdateUSerID = userId;
                        tr.UpdateDate = DateTime.UtcNow;

                        //Delete detail records
                        var dr1 = db.TrainingOfferDetails
                            .Where(r => r.TrainingOfferID == id)
                            .ToList();

                        db.TrainingOfferDetails.RemoveRange(dr1);

                        var dr2 = db.TrainingOfferOccurrences
                            .Where(r => r.TrainingOfferID == id)
                            .ToList();

                        db.TrainingOfferOccurrences.RemoveRange(dr2);

                        var dr3 = db.TrainingSkillDetails
                            .Where(r => r.TrainingOfferID == id)
                            .ToList();

                        db.TrainingSkillDetails.RemoveRange(dr3);

                        var dr4 = db.TrainingOtherSkills
                            .Where(r => r.TrainingOfferId == id)
                            .ToList();

                        db.TrainingOtherSkills.RemoveRange(dr4);
                    }
                    else //Insert
                    {
                        var tr = new TrainingOffer();

                        PopulateRecord(ref tr, ref vm);
                        tr.PostUSerID = userId;
                        tr.PostDate = DateTime.UtcNow;

                        db.TrainingOffers.Add(tr);
                        db.SaveChanges();

                        vm.Id = (long)tr.TrainingOfferID;
                    }

                    //Insert detail records
                    var ds = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.NewTitle},
                            {"c2", vm.Description},
                            {"c3", vm.Address}
                        });

                    foreach (var r in ds)
                        db.TrainingOfferDetails.Add(new TrainingOfferDetail()
                        {
                            TrainingOfferID = vm.Id,
                            LanguageID = r["c1"].L,
                            OtherCours_Name = r["c1"].T,
                            CourseDescription = r["c2"].T,
                            Address = r["c3"].T
                        });

                    foreach (var r in vm.Occurrence)
                        db.TrainingOfferOccurrences.Add(new TrainingOfferOccurrence()
                        {
                            TrainingOfferID = vm.Id,
                            OccurrenceID = r
                        });

                    foreach (var r in vm.Skills.Where(a => !a.IsNew))
                        db.TrainingSkillDetails.Add(new TrainingSkillDetail()
                        {
                            TrainingOfferID = vm.Id,
                            IndustryID = r.Industry.id,
                            SkillID = r.Skill.id,
                            SkillLevelID = r.Level.id,
                            SkillTypeID = r.Type == null || string.IsNullOrWhiteSpace(r.Type.id) ? null : r.Type.id
                        });

                    foreach (var r in vm.Skills.Where(a => a.IsNew))
                        db.TrainingOtherSkills.Add(new TrainingOtherSkill()
                        {
                            TrainingOfferId = vm.Id,
                            IndustryId = r.Industry.id,
                            OtherSkill = r.Skill.desc,
                            SkillLevelId = r.Level.id,
                            IsReviewed = false
                        });

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.Id;
        }

        public void Delete(long id, string userId, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.TrainingOffers
                    .Where(r => r.TrainingOfferID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteReason = reason.Limit(500);
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }

        public bool IsByOrgContact(long id, long contactId)
        {
            using (var db = new LMISEntities())
            {
                return db.TrainingOffers
                    .Any(r => r.IsDeleted == null && r.TrainingOfferID == id && r.OrganizationContactID == contactId);
            }
        }

        public void SetTrainingList(long portalUserId, string serverFilePath)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.OrganizationDetails
                    .Where(r => r.PortalUsersID == portalUserId)
                    .ToList().Single();

                tr.TrainingListPath = serverFilePath;

                db.SaveChanges();
            }
        }

        public string GetTrainingList(long portalUserId)
        {
            using (var db = new LMISEntities())
            {
                return db.OrganizationDetails
                    .Where(r => r.PortalUsersID == portalUserId)
                    .Select(r => r.TrainingListPath)
                    .Single();
            }
        }

        public List<TrainingVm> Search(string keywords, int langId = 1)
        {
            var lookup = keywords.ToLower().Split(' ');

            using (var db = new LMISEntities())
            {
                return db.TrainingOffers
                    .AsNoTracking()
                    .Where(r => r.IsDeleted == null && r.trainingStatus
                        && r.Is_Approved == (byte)Approval.Approved)
                    .Select(a => new
                    {
                        Offer = a,
                        TitleDesc = SqlUdf.SubCodeName(a.CourseNameID, langId),
                        TitleSearchable = SqlUdf.SubCodeSearchString(a.CourseNameID, langId),
                        Details = a.TrainingOfferDetails,
                        OrgNames = a.OrganizationDetail.OrganizationDetails_Det
                            .Select(r => new
                            {
                                r.LanguageID,
                                r.OrganizationName
                            }).ToList()
                    })
                    .Where(r => lookup.Any(q => r.TitleSearchable.ToLower().Contains(q)))
                    .ToList()
                    .Select(a => new TrainingVm
                    {
                        Id = (long)a.Offer.TrainingOfferID,
                        Title = a.TitleDesc,
                        Description = (new GlobalString(a.Details.Select(d => new LocalString(d.LanguageID, d.CourseDescription)).ToList())).Reduce(langId),
                        PortalUserId = (long)a.Offer.PortalUsersID,
                        Country = a.Offer.CountryID,
                        City = a.Offer.CityID,
                        Extras = new Dictionary<string, object> 
                        {
                            { "OrgName", new GlobalString(a.OrgNames.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList())
                                    .ToLocalString((Language)langId, true).T }
                        }
                    })
                    .ToList();
            }
        }

        public TrainingVm View(long id, long? portalUserId, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.TrainingOffers
                    .AsNoTracking()
                    .Where(r => r.TrainingOfferID == id
                                && r.IsDeleted == null && r.trainingStatus
                                && r.Is_Approved == (byte)Approval.Approved)
                    .Select(r => new
                    {
                        Offer = r,
                        Details = r.TrainingOfferDetails,
                        Title = SqlUdf.SubCodeName(r.CourseNameID, langId),
                        Country = SqlUdf.SubCodeName(r.CountryID, langId),
                        City = SqlUdf.SubCodeName(r.CityID, langId),
                        Occurrence = r.TrainingOfferOccurrences.Select(d => SqlUdf.SubCodeName(d.OccurrenceID, langId)).ToList(),
                        TimeZone = SqlUdf.SubCodeName(r.TimeZone, langId),
                        Skills = r.TrainingSkillDetails.Select(a => new TrainingVm.TrainingSkill
                        {
                            Industry = new CodeSet { id = a.IndustryID, desc = SqlUdf.SubCodeName(a.IndustryID, langId) },
                            Level = new CodeSet { id = a.SkillLevelID, desc = SqlUdf.SubCodeName(a.SkillLevelID, langId) },
                            Skill = new CodeSet { id = a.SkillID, desc = SqlUdf.SubCodeName(a.SkillID, langId) },
                            Type = new CodeSet { id = a.SkillTypeID, desc = SqlUdf.SubCodeName(a.SkillTypeID, langId) }
                        }).ToList(),
                        OrgNames = r.OrganizationDetail.OrganizationDetails_Det
                            .Select(a => new
                            {
                                a.LanguageID,
                                a.OrganizationName
                            }).ToList(),
                        OrgUrl = r.OrganizationDetail.OrganizationWebsite,
                        Applied = r.TrainingApplies.Any(a => a.IndPortalUserID == portalUserId),
                    })
                    .ToList()
                    .Select(r => new TrainingVm
                    {
                        Id = (long)r.Offer.TrainingOfferID,
                        FileName = r.Offer.OutlineFile,
                        Title = r.Title,
                        Description = (new GlobalString(r.Details.Select(d => new LocalString(d.LanguageID, d.CourseDescription)).ToList())).Reduce(langId),
                        Country = r.Country,
                        City = r.City,
                        Address = (new GlobalString(r.Details.Select(d => new LocalString(d.LanguageID, d.Address)).ToList())).Reduce(langId),
                        Duration = r.Offer.CourseDurationPerDay,
                        StartDate = r.Offer.StartDate.AsUtc(),
                        EndDate = r.Offer.EndDate.AsUtc(),
                        Seats = r.Offer.NoOfSeats,
                        Cost = r.Offer.CourseCost,
                        Occurrence = r.Occurrence,
                        TimeFrom = r.Offer.CourseTimeFrom.AsUtc(),
                        TimeTo = r.Offer.CourseTimeTo.AsUtc(),
                        TimeZone = r.TimeZone,
                        Skills = r.Skills,
                        Extras = new Dictionary<string, object>
                        {
                            { "OrgName", new GlobalString(r.OrgNames.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList())
                                    .ToLocalString((Language)langId, true).T },
                            { "OrgUrl", r.OrgUrl },
                            { "Applied", r.Applied }
                        }
                    })
                    .SingleOrDefault();
            }
        }

        public bool Apply(long id, long portalUserId)
        {
            using (var db = new LMISEntities())
            {
                if (db.TrainingApplies.Any(r => r.TrainingOfferID == id && r.IndPortalUserID == portalUserId))
                    return false;

                db.TrainingApplies.Add(new TrainingApply()
                {
                    TrainingOfferID = id,
                    IndPortalUserID = portalUserId,
                    ApplyDate = DateTime.UtcNow,
                    ViewStatus = 1
                });

                db.SaveChanges();

                return true;
            }
        }

        public void Approve(string adminId, long reqKey, long trainingId, bool approved, string reason, Dictionary<string, object> newValues)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == trainingId
                            && r.RequestType == "02900007" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                    var tr = db.TrainingOffers
                        .Where(r => r.TrainingOfferID == trainingId)
                        .ToList().Single();

                    if (approved)
                        tr.Is_Approved = (byte)Approval.Approved;
                    else
                    {
                        tr.Is_Approved = (byte)Approval.Rejected;
                        tr.RejectReason = reason.Limit(500);
                    }

                    if (string.IsNullOrWhiteSpace(tr.CourseNameID))
                    {
                        tr.CourseNameID = (string)newValues["title"];
                        if (string.IsNullOrWhiteSpace(tr.CourseNameID)) throw new Exception("Title is mandatory.");

                        var dr = db.TrainingOfferDetails
                            .Where(r => r.TrainingOfferID == trainingId && r.OtherCours_Name != null && r.OtherCours_Name.Trim() != "")
                            .ToList();

                        foreach (var r in dr)
                            r.OtherCours_Name = null;

                        db.TrainingOfferDetails.RemoveRange(
                            dr.Where(r =>
                                string.IsNullOrWhiteSpace(r.CourseDescription) &&
                                string.IsNullOrWhiteSpace(r.Address)));
                    }

                    var drOtherSkills = db.TrainingOtherSkills.Where(r => r.TrainingOfferId == trainingId).ToList();

                    if (drOtherSkills.Any())
                    {
                        db.TrainingOtherSkills.RemoveRange(drOtherSkills);

                        var newSkills = ((object[])newValues["skills"]).Cast<Dictionary<string, object>>().ToList();
                        var drSkills = db.TrainingSkillDetails.Where(r => r.TrainingOfferID == trainingId).ToList();

                        newSkills = newSkills.GroupBy(s => (string)s["industry"] + (string)s["skill"] + (string)s["level"])
                            .Select(g => g.First()).ToList();

                        newSkills.RemoveAll(s => drSkills.Any(r =>
                            r.IndustryID == (string)s["industry"] &&
                            r.SkillID == (string)s["skill"] &&
                            r.SkillLevelID == (string)s["level"]));

                        foreach (var s in newSkills)
                            db.TrainingSkillDetails.Add(new TrainingSkillDetail
                            {
                                TrainingOfferID = trainingId,
                                IndustryID = (string)s["industry"],
                                SkillID = (string)s["skill"],
                                SkillLevelID = (string)s["level"]
                            });

                        if (!drSkills.Any() && !newSkills.Any()) throw new Exception("Skills are mandatory.");
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