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
using Microsoft.Owin.Security.Provider;

namespace LMIS.Dal.Repositories
{
    public class JobRepository : IJobRepository
    {
        private static readonly Func<JobOffer, IEnumerable<JobVm.JobSkill>, IEnumerable<JobVm.JobSkill>, JobVm> MapJobOffers = (a, b, c) =>
            new JobVm
            {
                JobId = (long)a.JobOfferID,
                ContactId = (long)a.OrganizationContactID.GetValueOrDefault(),
                PortalUserId = (long)a.PortalUsersID,
                Title = a.JobTiltleID,
                NewTitle = a.JobOfferDetails.Select(d => new LocalString(d.LanguageID, d.OtherJobTitle)).ToList(),
                Description = a.JobOfferDetails.Select(d => new LocalString(d.LanguageID, d.JobDescription)).ToList(),
                FileName = a.JobOfferAdditionalDocs
                    .Where(d => d.AdditionalDocTypeID == "99999999")
                    .Select(d => d.AdditionalDocTemplatePath).SingleOrDefault(),
                ExpFrom = a.ExpYearFrom,
                ExpTo = a.ExpYearTo,
                Vacancies = a.NumberOfVacanciesPosition,
                StartDate = a.StartDate.AsUtc(),
                EndDate = a.EndDate.AsUtc(),
                EmploymentType = a.EmploymentTypeID,
                EdLevel = a.JobOfferEducationLevels.Select(d => d.EducationLevelID).SingleOrDefault(),
                EdCert = a.JobOfferEducationLevels.First().JobOfferEducationLevelDetails
                    .Select(d => new LocalString(d.LanguageID, d.CertificationType)).ToList(),
                Gender = a.GenderID,
                Country = a.CountryID,
                City = a.CityID,
                JobStatus = a.JobStatus,
                Skills = b.ToList().Union(c.ToList()).ToList(),
                PerMonth = a.SalaryRangePerMonth,
                PerHour = a.SalaryRangePerHour.GetValueOrDefault(),
                Currency = a.SalaryCurrencyID,
                MedConditions = a.jobOfferMedicalDetails.Select(d => d.MedicalID).ToList(),
                DocTypes = a.JobOfferAdditionalDocs
                    .Where(d => d.AdditionalDocTypeID != "99999999")
                    .Select(d => d.AdditionalDocTypeID).ToList(),
                Approval = (Approval)a.IsApproved,
                RejectReason = a.RejectReason
            };

        private static void PopulateRecord(ref JobOffer tr, ref JobVm vm)
        {
            tr.OrganizationContactID = vm.ContactId;
            tr.PortalUsersID = vm.PortalUserId;
            tr.JobTiltleID = string.IsNullOrWhiteSpace(vm.Title) ? "" : vm.Title;
            tr.ExpYearFrom = vm.ExpFrom;
            tr.ExpYearTo = vm.ExpTo;
            tr.NumberOfVacanciesPosition = vm.Vacancies;
            tr.StartDate = vm.StartDate.AsUtc();
            tr.EndDate = vm.EndDate.AsUtc();
            tr.EmploymentTypeID = vm.EmploymentType;
            tr.GenderID = vm.Gender;
            tr.CountryID = vm.Country;
            tr.CityID = vm.City;
            tr.JobStatus = vm.JobStatus;
            tr.SalaryRangePerMonth = vm.PerMonth;
            tr.SalaryRangePerHour = vm.PerHour;
            tr.SalaryCurrencyID = vm.Currency;
            tr.IsApproved = (byte)vm.Approval;
        }

        public List<JobVm> BriefByOrgContact(long contactId, long? id = null, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.JobOffers
                    .AsNoTracking()
                    .Where(r => r.IsDeleted == null
                        && r.OrganizationContactID == contactId
                        && (id == null || r.JobOfferID == id))
                    .Select(a => new
                    {
                        Offer = a,
                        TitleDesc = SqlUdf.SubCodeName(a.JobTiltleID, langId),
                        Details = a.JobOfferDetails
                    })
                    .ToList()
                    .Select(a => new JobVm()
                    {
                        JobId = (long)a.Offer.JobOfferID,
                        ContactId = (long)a.Offer.OrganizationContactID.GetValueOrDefault(),
                        PortalUserId = (long)a.Offer.PortalUsersID,
                        Title = a.TitleDesc,
                        JobStatus = a.Offer.JobStatus,
                        Approval = (Approval)a.Offer.IsApproved,
                        RejectReason = a.Offer.RejectReason,
                        NewTitle = a.Details.Select(d => new LocalString(d.LanguageID, d.OtherJobTitle)).ToList(),
                        PostDate = a.Offer.PostDate
                    })
                    .ToList();
            }
        }

        public List<JobVm> ListByOrgContact(long contactId, long? id = null, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                var q = db.JobOffers
                    .AsNoTracking()
                    .Where(r => r.IsDeleted == null
                        && (contactId == -1 || r.OrganizationContactID == contactId)
                        && (id == null || r.JobOfferID == id))
                    .Select(r => new
                    {
                        JobOffer = r,
                        Skills = r.jobOfferSkillsDetails.Select(a => new JobVm.JobSkill
                        {
                            IsNew = false,
                            Industry = new CodeSet { id = a.IndustryID, desc = SqlUdf.SubCodeName(a.IndustryID, langId) },
                            Level = new CodeSet { id = a.SkillLevelID, desc = SqlUdf.SubCodeName(a.SkillLevelID, langId) },
                            Skill = new CodeSet { id = a.SkillID, desc = SqlUdf.SubCodeName(a.SkillID, langId) },
                            Type = new CodeSet { id = a.SkillTypeID, desc = SqlUdf.SubCodeName(a.SkillTypeID, langId) }
                        }),
                        OtherSkills = r.JobOtherSkills.Select(a => new JobVm.JobSkill
                        {
                            IsNew = true,
                            Industry = new CodeSet { id = a.IndustryId, desc = SqlUdf.SubCodeName(a.IndustryId, langId) },
                            Level = new CodeSet { id = a.SkillLevelId, desc = SqlUdf.SubCodeName(a.SkillLevelId, langId) },
                            Skill = new CodeSet { id = null, desc = a.OtherSkill },
                            Type = new CodeSet { id = null, desc = null }
                        })
                    });

                return q.ToList().Select(r => MapJobOffers(r.JobOffer, r.Skills, r.OtherSkills)).ToList();
            }
        }

        public List<Dictionary<string, object>> DetailApplicants(long contactId, long id, int langId = 1)
        {
            List<JobApplied> ds;

            using (var db = new LMISEntities())
            {
                ds = db.JobApplieds
                    .AsNoTracking()
                    .Include(r => r.JobOffer)
                    .Include(r => r.IndividualDetail.IndividualDetailsDets)
                    .Where(r => r.JobOffer.IsDeleted == null
                        && r.JobOffer.OrganizationContactID == contactId
                        && r.JobOffer.JobOfferID == (int)id)
                    .OrderBy(r => r.ViewStatus).ThenBy(r => r.ApplyDate)
                    .ToList();
            }

            return ds.Select(a => new Dictionary<string, object>
            {
                {"id", a.JobAppliedID},
                {"userId", a.IndPortalUserID.ToString(CultureInfo.InvariantCulture)},
                {"userName", new GlobalString(a.IndividualDetail.IndividualDetailsDets
                    .Select(d => new LocalString(d.LanguageID, d.FirstName + " " + d.LastName))
                    .ToList()).ToLocalString((Language)langId, true).T},
                {"date", a.ApplyDate},
                {"status", a.ViewStatus}
            }).ToList();
        }

        public void ChangeApplicantStatus(long id, int status)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.JobApplieds
                    .Where(r => r.JobAppliedID == id)
                    .ToList().Single();

                tr.ViewStatus = status;
                db.SaveChanges();
            }
        }

        public long Post(ref JobVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.JobId;

                    if (id > 0) //Update
                    {
                        var tr = db.JobOffers
                            .Where(r => r.IsDeleted == null && r.JobOfferID == id)
                            .ToList().Single();

                        PopulateRecord(ref tr, ref vm);
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;

                        //Delete detail records
                        var dr0 = db.JobOfferDetails
                            .Where(r => r.JobOfferID == id)
                            .ToList();

                        db.JobOfferDetails.RemoveRange(dr0);

                        var dr1 = db.JobOfferAdditionalDocs
                            .Where(r => r.JobOfferID == id)
                            .ToList();

                        db.JobOfferAdditionalDocs.RemoveRange(dr1);

                        var dr2 = db.JobOfferEducationLevelDetails
                            .Where(r => r.JobOfferID == id)
                            .ToList();

                        db.JobOfferEducationLevelDetails.RemoveRange(dr2);

                        var dr3 = db.JobOfferEducationLevels
                            .Where(r => r.JobOfferID == id)
                            .ToList();

                        db.JobOfferEducationLevels.RemoveRange(dr3);

                        var dr4 = db.jobOfferMedicalDetails
                            .Where(r => r.JobOfferID == id)
                            .ToList();

                        db.jobOfferMedicalDetails.RemoveRange(dr4);

                        var dr5 = db.jobOfferSkillsDetails
                            .Where(r => r.JobOfferID == id)
                            .ToList();

                        db.jobOfferSkillsDetails.RemoveRange(dr5);

                        var dr6 = db.JobOtherSkills
                            .Where(r => r.JobOfferId == id)
                            .ToList();

                        db.JobOtherSkills.RemoveRange(dr6);                    
                    }
                    else //Insert
                    {
                        var tr = new JobOffer();

                        PopulateRecord(ref tr, ref vm);
                        tr.PostUserID = userId;
                        tr.PostDate = DateTime.UtcNow;

                        db.JobOffers.Add(tr);
                        db.SaveChanges();

                        vm.JobId = (long)tr.JobOfferID;
                    }

                    //Insert detail records
                    var ds = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.NewTitle},
                            {"c2", vm.Description},
                        });

                    foreach (var r in ds)
                        db.JobOfferDetails.Add(new JobOfferDetail()
                        {
                            JobOfferID = vm.JobId,
                            LanguageID = r["c1"].L,
                            OtherJobTitle = r["c1"].T,
                            JobDescription = r["c2"].T
                        });

                    //Insert education details
                    var joEd = db.JobOfferEducationLevels.Add(new JobOfferEducationLevel()
                    {
                        JobOfferID = vm.JobId,
                        EducationLevelID = vm.EdLevel
                    });

                    ds = Utils.MultilingualDataSet(new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.EdCert},
                        });

                    foreach (var r in ds)
                        joEd.JobOfferEducationLevelDetails.Add(new JobOfferEducationLevelDetail()
                        {
                            JobOfferID = vm.JobId,
                            EdLevelID = vm.EdLevel,
                            LanguageID = r["c1"].L,
                            CertificationType = r["c1"].T
                        });

                    //Insert skill details
                    foreach (var r in vm.Skills.Where(a => !a.IsNew))
                        db.jobOfferSkillsDetails.Add(new jobOfferSkillsDetail()
                        {
                            JobOfferID = vm.JobId,
                            IndustryID = r.Industry.id,
                            SkillID = r.Skill.id,
                            SkillLevelID = r.Level.id,
                            SkillTypeID = r.Type == null || string.IsNullOrWhiteSpace(r.Type.id) ? null : r.Type.id
                        });

                    foreach (var r in vm.Skills.Where(a => a.IsNew))
                        db.JobOtherSkills.Add(new JobOtherSkill()
                        {
                            JobOfferId = vm.JobId,
                            IndustryId = r.Industry.id,
                            OtherSkill = r.Skill.desc,
                            SkillLevelId = r.Level.id,
                            IsReviewed = false
                        });

                    //Insert medical details
                    foreach (var cond in vm.MedConditions)
                        db.jobOfferMedicalDetails.Add(new jobOfferMedicalDetail()
                        {
                            JobOfferID = vm.JobId,
                            MedicalID = cond
                        });

                    //Insert document details
                    if (!string.IsNullOrWhiteSpace(vm.FileName))
                        db.JobOfferAdditionalDocs.Add(new JobOfferAdditionalDoc()
                        {
                            JobOfferID = vm.JobId,
                            AdditionalDocTypeID = "99999999", //Application Form
                            AdditionalDocTemplatePath = vm.FileName
                        });
                    foreach (var docType in vm.DocTypes)
                        db.JobOfferAdditionalDocs.Add(new JobOfferAdditionalDoc()
                        {
                            JobOfferID = vm.JobId,
                            AdditionalDocTypeID = docType
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

            return vm.JobId;
        }

        public void Delete(long id, string userId, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.JobOffers
                    .Where(r => r.JobOfferID == id)
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
                return db.JobOffers
                    .Any(r => r.IsDeleted == null && r.JobOfferID == id && r.OrganizationContactID == contactId);
            }
        }

        public List<JobVm> Search(string keywords, int langId = 1)
        {
            var lookup = keywords.ToLower().Split(' ');

            using (var db = new LMISEntities())
            {
                return db.JobOffers
                    .AsNoTracking()
                    .Where(r => r.IsDeleted == null && r.JobStatus
                        && r.IsApproved == (byte)Approval.Approved)
                    .Select(a => new
                    {
                        Offer = a,
                        TitleDesc = SqlUdf.SubCodeName(a.JobTiltleID, langId),
                        TitleSearchable = SqlUdf.SubCodeSearchString(a.JobTiltleID, langId),
                        Details = a.JobOfferDetails
                    })
                    .Where(r => lookup.Any(q => r.TitleSearchable.ToLower().Contains(q)))
                    .ToList()
                    .Select(a => new JobVm
                    {
                        JobId = (long)a.Offer.JobOfferID,
                        Title = a.TitleDesc,
                        Description = (new GlobalString(a.Details.Select(d => new LocalString(d.LanguageID, d.JobDescription)).ToList())).Reduce(langId),
                        EmploymentType = a.Offer.EmploymentTypeID,
                        Country = a.Offer.CountryID,
                        City = a.Offer.CityID
                    })
                    .ToList();
            }
        }

        public JobVm View(long id, long? portalUserId, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.JobOffers
                    .AsNoTracking()
                    .Where(r => r.JobOfferID == id
                                && r.IsDeleted == null && r.JobStatus
                                && r.IsApproved == (byte)Approval.Approved)
                    .Select(r => new
                    {
                        Offer = r,
                        Details = r.JobOfferDetails,
                        Title = SqlUdf.SubCodeName(r.JobTiltleID, langId),
                        FileName = r.JobOfferAdditionalDocs
                            .Where(d => d.AdditionalDocTypeID == "99999999")
                            .Select(d => d.AdditionalDocTemplatePath).FirstOrDefault(),
                        EmploymentType = SqlUdf.SubCodeName(r.EmploymentTypeID, langId),
                        EdLevel = r.JobOfferEducationLevels
                            .Select(d => SqlUdf.SubCodeName(d.EducationLevelID, langId)).FirstOrDefault(),
                        EdCert = r.JobOfferEducationLevels.FirstOrDefault().JobOfferEducationLevelDetails,
                        Gender = SqlUdf.SubCodeName(r.GenderID, langId),
                        Country = SqlUdf.SubCodeName(r.CountryID, langId),
                        City = SqlUdf.SubCodeName(r.CityID, langId),
                        Skills = r.jobOfferSkillsDetails.Select(a => new JobVm.JobSkill
                        {
                            Industry = new CodeSet { id = a.IndustryID, desc = SqlUdf.SubCodeName(a.IndustryID, langId) },
                            Level = new CodeSet { id = a.SkillLevelID, desc = SqlUdf.SubCodeName(a.SkillLevelID, langId) },
                            Skill = new CodeSet { id = a.SkillID, desc = SqlUdf.SubCodeName(a.SkillID, langId) },
                            Type = new CodeSet { id = a.SkillTypeID, desc = SqlUdf.SubCodeName(a.SkillTypeID, langId) }
                        }).ToList(),
                        Currency = SqlUdf.SubCodeName(r.SalaryCurrencyID, langId),
                        MedConsitions = r.jobOfferMedicalDetails
                            .Select(d => SqlUdf.SubCodeName(d.MedicalID, langId)).ToList(),
                        DocTypes = r.JobOfferAdditionalDocs
                            .Where(d => d.AdditionalDocTypeID != "99999999")
                            .Select(d => SqlUdf.SubCodeName(d.AdditionalDocTypeID, langId)).ToList(),
                        OrgNames = r.OrganizationDetail.OrganizationDetails_Det
                            .Select(a => new
                            {
                                a.LanguageID,
                                a.OrganizationName
                            }).ToList(),
                        OrgUrl = r.OrganizationDetail.OrganizationWebsite,
                        Applied = r.JobApplieds.Any(a => a.IndPortalUserID == portalUserId),
                    })
                    .ToList()
                    .Select(r => new JobVm
                    {
                        JobId = (long)r.Offer.JobOfferID,
                        Title = r.Title,
                        Description = (new GlobalString(r.Details.Select(d => new LocalString(d.LanguageID, d.JobDescription)).ToList())).Reduce(langId),
                        FileName = r.FileName,
                        ExpFrom = r.Offer.ExpYearFrom,
                        ExpTo = r.Offer.ExpYearTo,
                        Vacancies = r.Offer.NumberOfVacanciesPosition,
                        StartDate = r.Offer.StartDate.AsUtc(),
                        EndDate = r.Offer.EndDate.AsUtc(),
                        EmploymentType = r.EmploymentType,
                        EdLevel = r.EdLevel,
                        EdCert = (new GlobalString(r.EdCert.Select(d => new LocalString(d.LanguageID, d.CertificationType)).ToList())).Reduce(langId),
                        Gender = r.Gender,
                        Country = r.Country,
                        City = r.City,
                        Skills = r.Skills,
                        PerMonth = r.Offer.SalaryRangePerMonth,
                        PerHour = r.Offer.SalaryRangePerHour.GetValueOrDefault(),
                        Currency = r.Currency,
                        MedConditions = r.MedConsitions,
                        DocTypes = r.DocTypes,
                        Extras = new Dictionary<string, object>
                        {
                            { "OrgName", new GlobalString(r.OrgNames.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList())
                                    .ToLocalString((Language)langId, true).T },
                            { "OrgUrl", r.OrgUrl },
                            { "Applied", r.Applied },
                            { "Express", string.IsNullOrWhiteSpace(r.FileName) && r.DocTypes.Count == 0 }
                        }
                    })
                    .SingleOrDefault();
            }
        }

        public Dictionary<string, object> ApplicationRequirements(long id, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                return db.JobOffers
                    .AsNoTracking()
                    .Where(r => r.JobOfferID == id
                                && r.IsDeleted == null && r.JobStatus
                                && r.IsApproved == (byte)Approval.Approved)
                    .Select(a => new
                    {
                        Title = SqlUdf.SubCodeName(a.JobTiltleID, langId),
                        AppTemplate = a.JobOfferAdditionalDocs
                            .Where(d => d.AdditionalDocTypeID == "99999999")
                            .Select(d => d.AdditionalDocTemplatePath)
                            .FirstOrDefault(),
                        AdditionalDocs = a.JobOfferAdditionalDocs
                            .Where(d => d.AdditionalDocTypeID != "99999999")
                            .Select(d => new CodeSet
                            {
                                id = d.AdditionalDocTypeID,
                                desc = SqlUdf.SubCodeName(d.AdditionalDocTypeID, langId)
                            }).ToList()
                    })
                    .ToList()
                    .Select(a => new Dictionary<string, object>
                    {
                        { "Id", id },
                        { "Title", a.Title },
                        { "AppTemplate", a.AppTemplate },
                        { "AdditionalDocs", a.AdditionalDocs }
                    })
                    .SingleOrDefault();
            }
        }

        public bool Apply(long id, long portalUserId, string appForm, List<CodeSet> attachments)
        {
            using (var db = new LMISEntities())
            {
                if (db.JobApplieds.Any(r => r.JobOfferID == id && r.IndPortalUserID == portalUserId))
                    return false;

                var app = new JobApplied
                {
                    JobOfferID = id,
                    IndPortalUserID = portalUserId,
                    ApplyDate = DateTime.UtcNow,
                    ViewStatus = 1
                };

                db.JobApplieds.Add(app);
                db.SaveChanges();

                if (!string.IsNullOrWhiteSpace(appForm))
                    attachments.Add(new CodeSet
                    {
                        id = "99999999",
                        desc = appForm
                    });

                foreach (var f in attachments)
                    db.JobAppliedAdditionalDocs.Add(new JobAppliedAdditionalDoc
                    {
                        JobAppliedID = app.JobAppliedID,
                        AdditionalDocTypeID = f.id,
                        AdditionalDocPath = f.desc
                    });

                db.SaveChanges();

                return true;
            }
        }

        public void Approve(string adminId, long reqKey, long jobId, bool approved, string reason, Dictionary<string, object> newValues)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == jobId
                            && r.RequestType == "02900006" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                    var tr = db.JobOffers
                        .Where(r => r.JobOfferID == jobId)
                        .ToList().Single();

                    if (approved)
                        tr.IsApproved = (byte)Approval.Approved;
                    else
                    {
                        tr.IsApproved = (byte)Approval.Rejected;
                        tr.RejectReason = reason.Limit(500);
                    }

                    if (string.IsNullOrWhiteSpace(tr.JobTiltleID))
                    {
                        tr.JobTiltleID = (string)newValues["title"];
                        if (string.IsNullOrWhiteSpace(tr.JobTiltleID)) throw new Exception("Title is mandatory.");

                        var dr = db.JobOfferDetails
                            .Where(r => r.JobOfferID == jobId && r.OtherJobTitle != null && r.OtherJobTitle.Trim() != "")
                            .ToList();

                        foreach (var r in dr)
                            r.OtherJobTitle = null;

                        db.JobOfferDetails.RemoveRange(dr.Where(r => string.IsNullOrWhiteSpace(r.JobDescription)));
                    }

                    var drOtherSkills = db.JobOtherSkills.Where(r => r.JobOfferId == jobId).ToList();

                    if (drOtherSkills.Any())
                    {
                        db.JobOtherSkills.RemoveRange(drOtherSkills);

                        var newSkills = ((object[])newValues["skills"]).Cast<Dictionary<string, object>>().ToList();
                        var drSkills = db.jobOfferSkillsDetails.Where(r => r.JobOfferID == jobId).ToList();

                        newSkills = newSkills.GroupBy(s => (string)s["industry"] + (string)s["skill"] + (string)s["level"])
                            .Select(g => g.First()).ToList();

                        newSkills.RemoveAll(s => drSkills.Any(r =>
                            r.IndustryID == (string)s["industry"] &&
                            r.SkillID == (string)s["skill"] &&
                            r.SkillLevelID == (string)s["level"]));

                        foreach (var s in newSkills)
                            db.jobOfferSkillsDetails.Add(new jobOfferSkillsDetail
                            {
                                JobOfferID = jobId,
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

        public List<JobsCountVm> JobsPerYear(bool started, bool jobStatus, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                var result= db.JobOffers

                    .Where(r => r.IsDeleted == null 
                                && (r.JobStatus || jobStatus==false)
                                && r.IsApproved == (byte) Approval.Approved
                                && (r.StartDate.Year == DateTime.Now.Year || r.EndDate.Year == DateTime.Now.Year || started == false))
                                

                    .Select(a => new JobsCountVm
                    {
                        id = a.JobOfferID,
                        Title = SqlUdf.SubCodeName(a.JobTiltleID, langId),
                        StartDate=a.StartDate ,
                        EndDate=a.EndDate 

                    }).ToList();

                return result;

            }
        }
     
        
        public decimal JobApplied()
        {
            using (var db = new LMISEntities())
            {
             return    db.JobApplieds
                         .Where(w=>w.ApplyDate.Year==DateTime.Now.Year)
                         .Count();
            }
        }
    
    }
}