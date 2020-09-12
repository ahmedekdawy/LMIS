using LMIS.Dal.Entity;
using LMIS.Dal.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Individual;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.ExceptionServices;
using EntityState = System.Data.Entity.EntityState;

namespace LMIS.Dal.Repositories
{
    public class IndividualRepository : IIndividualRepository
    {
        public void Post(ref IndividualVM vm, string userId)
        {
            var dbContext = new LMISEntities();
            try
            {
                #region 1st: Add portal user

                PortalUser portalUser = new PortalUser
                {
                    PortalUsersID = vm.PortalUser.PortalUsersID,
                    IDType = vm.PortalUser.IDType,
                    IDNumber = vm.PortalUser.IDNumber,
                    UserCategory = vm.PortalUser.UserCategory,
                    UserSubCategory = vm.PortalUser.UserSubCategory,
                    TrainingProvider = vm.PortalUser.TrainingProvider,
                    Employer = vm.PortalUser.Employer,
                    JobSeeker = vm.PortalUser.JobSeeker,
                    TrainingSeeker = vm.PortalUser.TrainingSeeker,
                    Researcher = vm.PortalUser.Researcher,
                    Internal = vm.PortalUser.Internal,
                    IsSubscriper = vm.PortalUser.IsSubscriper
                };
                dbContext.PortalUsers.Add(portalUser);
                dbContext.SaveChanges();

                #endregion


                using (var transaction = dbContext.Database.BeginTransaction())
                {

                    #region 2nd: Populate Individual object

                    IndividualDetail individualObject = PopulateRecord(vm, portalUser.PortalUsersID, userId);
                    individualObject.PostDate = DateTime.UtcNow;

                    #endregion

                    #region trd: Add translations

                    foreach (
                        IndividualTranslationVM translation in
                            vm.Translation.Where(t => t.FirstName != null || t.LastName != null || t.Address != null))
                    {
                        IndividualDetailsDet translationObject = new IndividualDetailsDet
                        {
                            PortalUsersID = portalUser.PortalUsersID,
                            LanguageID = translation.LanguageID,
                            FirstName = translation.FirstName,
                            LastName = translation.LastName,
                            Address = translation.Address
                        };
                        individualObject.IndividualDetailsDets.Add(translationObject);
                    }

                    #endregion

                    #region 4th: Save Individual object

                    dbContext.IndividualDetails.Add(individualObject);
                    dbContext.SaveChanges();
                    transaction.Commit();

                    #endregion
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                }
                throw;
            }
            catch
            {
                throw;
            }
        }

        public IndividualProfileVM GetProfile(long portalUserId, int langId = 1)
        {
            IndividualProfileVM indObj;

            using (var db = new LMISEntities())
            {
                var portalObj = db.PortalUsers.Where(pu => pu.PortalUsersID == portalUserId);

                indObj = portalObj.Select(user => new IndividualProfileVM()
                {
                    #region Load main object with translation
                    PortalUsersID = (long) user.PortalUsersID,
                    FullName =
                        user.IndividualDetail.IndividualDetailsDets.Where(t => t.LanguageID == langId)
                            .Select(d => d.FirstName + " " + d.LastName)
                            .FirstOrDefault(),
                    AddressLocalized =
                        user.IndividualDetail.IndividualDetailsDets.Where(t => t.LanguageID == langId)
                            .Select(d => d.Address)
                            .FirstOrDefault(),

                    Email = user.IndividualDetail.Email,
                    MobileNo = user.IndividualDetail.MobileNo,
                    TelephoneNo = user.IndividualDetail.TelephoneNo,
                    Gender = SqlUdf.SubCodeName(user.IndividualDetail.GenderId, langId),
                    DateOfBirth = user.IndividualDetail.DateOfBirth,
                    MaritalStatus = SqlUdf.SubCodeName(user.IndividualDetail.MaritalStatusId, langId),
                    MilitaryStatus = SqlUdf.SubCodeName(user.IndividualDetail.MilitaryStatus_Id, langId),
                    Nationality = SqlUdf.SubCodeName(user.IndividualDetail.NationalityId, langId),
                    Country = SqlUdf.SubCodeName(user.IndividualDetail.CountryID, langId),
                    City = SqlUdf.SubCodeName(user.IndividualDetail.CityID, langId),
                    IndividualMedical = SqlUdf.SubCodeName(user.IndividualDetail.IndividualMedicalID, langId),
                    AllowtoViewMyInfo = (user.IndividualDetail.AllowtoViewMyInfo == 1) ? true : false,
                    GenderId = user.IndividualDetail.GenderId,
                    MaritalStatusId = user.IndividualDetail.MaritalStatusId,
                    MilitaryStatus_Id = user.IndividualDetail.MilitaryStatus_Id,
                    NationalityId = user.IndividualDetail.NationalityId,
                    IndividualMedicalID = user.IndividualDetail.IndividualMedicalID,
                    CountryID = user.IndividualDetail.CountryID,
                    CityID = user.IndividualDetail.CityID,
                    IDType = user.IDType,
                    IDNumber = user.IDNumber,
                    PhotoPath = user.IndividualDetail.PhotoPath,
                    Approval = (Approval) user.IndividualDetail.Is_Approved,
                    RejectReason = user.IndividualDetail.RejectReason,

                    #endregion

                    #region Educationzl Info
                    Educations =
                        user.IndividualDetail.IndividualEducationlevels.Where(w => w.IsDeleted == null)
                            .Select(r => new IndividualProfileEducationVM()
                            {
                                IndividualEducationlevelID = (long) r.IndividualEducationlevelID,
                                EducationName =
                                    r.IndividualEducationlevelDets.Where(rDet => rDet.LanguageID == langId)
                                        .FirstOrDefault()
                                        .InstitutionName,
                                FacultyID =
                                    r.IndividualEducationlevelDets.Where(rDet => rDet.LanguageID == langId)
                                        .FirstOrDefault()
                                        .FacultyName,
                                LevelOfEducation = r.LevelOfEducation.Trim(),
                                InstitutionType =
                                    r.IndividualEducationlevelDets.Where(rDet => rDet.LanguageID == langId)
                                        .FirstOrDefault()
                                        .InstitutionType,
                                LevelOfEducationName = SqlUdf.SubCodeName(r.LevelOfEducation.Trim(), 1),
                                GraduationYear = r.GraduationYear,
                                Grade =
                                    r.IndividualEducationlevelDets.Where(rDet => rDet.LanguageID == langId)
                                        .FirstOrDefault()
                                        .Grade,
                                Degree = r.Degree,
                                GradeGPA = r.GradeGPA

                            }).ToList(),

                    #endregion

                    #region Experience Info
                    Experiences =
                        user.IndividualDetail.IndividualExperienceDetails.Where(w => w.IsDeleted == null)
                            .Select(x => new IndividualProfileExperienceVM()
                            {
                                IndividualExperienceID = (long) x.IndividualExperienceID,
                                Name =
                                    x.IndividualExperienceDetails_Det.FirstOrDefault(l => l.LanguageID == langId)
                                        .EmployerName,
                                JobDescription =
                                    x.IndividualExperienceDetails_Det.FirstOrDefault(l => l.LanguageID == langId)
                                        .EmploymentJobDescription,
                                EmploymentStartDate = x.EmploymentStartDate,
                                EmploymentEndDate = x.EmploymentEndDate,
                                ExpYears = x.ExpYears,
                                EmploymentJobTitle = SqlUdf.SubCodeName(x.EmploymentJobTitle, langId),
                                ExpMonths = x.ExpMonths
                            }).OrderByDescending(o => o.EmploymentStartDate).ToList(),

                    #endregion

                    #region Skill Info
                    Skills = user.IndividualDetail.IndividualSkillsDetails.Select(s => new IndividualProfileSkillVM()
                    {
                        IndividualSkillsDetailsID = (long) s.IndividualSkillsDetailsID,
                        Skill = new CodeSet {id = s.SkillID, desc = SqlUdf.SubCodeName(s.SkillID, langId)},
                        Industry = new CodeSet {id = s.IndustryID, desc = SqlUdf.SubCodeName(s.IndustryID, langId)},
                        SkillLevel =
                            new CodeSet {id = s.SkillLevelID, desc = SqlUdf.SubCodeName(s.SkillLevelID, langId)},
                        SkillType = new CodeSet {id = s.SkillTypeID, desc = SqlUdf.SubCodeName(s.SkillTypeID, langId)},
                        YearsOf_Experience = s.YearsOf_Experience,
                        IsOtherSkill = false
                    }).ToList(),

                    #endregion

                    #region Training Info
                    Trainings =
                        user.IndividualDetail.IndividualTrainingDetails.Where(w => w.IsDeleted == null)
                            .Select(t => new IndividualProfileTrainingVM()
                            {
                                IndividualTrainingID = (long) t.IndividualTrainingID,
                                TrainingStartDate = t.TrainingStartDate,
                                TrainingEndDate = t.TrainingEndDate,
                                TrainingName =
                                    t.IndividualTrainingDetails_Det.FirstOrDefault(
                                        l => l.LanguageID == langId && l.IndividualTrainingID == t.IndividualTrainingID)
                                        .TrainingName,
                                TrainingProviderName =
                                    t.IndividualTrainingDetails_Det.FirstOrDefault(l => l.LanguageID == langId)
                                        .TrainingProviderName
                            }).ToList(),

                    #endregion

                    #region Certificates Info
                    Certificates =
                        user.IndividualDetail.IndividualCertificationDetails.Where(w => w.IsDeleted == null)
                            .Select(c => new IndividualProfileCertificateVM()
                            {
                                IndividualCertificationID = (long) c.IndividualCertificationID,
                                CertificateName =
                                    c.IndividualCertificationDetails_Det.FirstOrDefault(
                                        l =>
                                            l.LanguageID == langId &&
                                            l.IndividualCertificationID == c.IndividualCertificationID).CertificateName,
                                CertificateIssueDate = c.CertificateIssueDate,
                                CertificateValidUntil = c.CertificateValidUntil
                            }).ToList(),

                    #endregion

                }).Single();

                #region load localized values

                indObj.FirstName =
                    portalObj.First()
                        .IndividualDetail.IndividualDetailsDets.Select(d => new LocalString(d.LanguageID, d.FirstName))
                        .ToList();
                indObj.LastName =
                    portalObj.First()
                        .IndividualDetail.IndividualDetailsDets.Select(d => new LocalString(d.LanguageID, d.LastName))
                        .ToList();
                indObj.Address =
                    portalObj.First()
                        .IndividualDetail.IndividualDetailsDets.Select(d => new LocalString(d.LanguageID, d.Address))
                        .ToList();

                #endregion

                #region Other skills

                indObj.Skills =
                    indObj.Skills.Concat(
                        db.IndividualOtherSkills.Where(pu => pu.PortalUsersID == portalUserId)
                            .Select(o => new IndividualProfileSkillVM()
                            {
                                Skill = new CodeSet {id = null, desc = o.OtherSkill},
                                Industry =
                                    new CodeSet {id = o.IndustryId, desc = SqlUdf.SubCodeName(o.IndustryId, langId)},
                                SkillLevel =
                                    new CodeSet {id = o.SkillLevelId, desc = SqlUdf.SubCodeName(o.SkillLevelId, langId)},
                                SkillType = new CodeSet {id = null, desc = null},
                                YearsOf_Experience = "-",
                                IsOtherSkill = true
                            })).ToList();

                #endregion

                #region Applied Jobs

                indObj.Jobs = db.JobApplieds.Where(j => j.IndPortalUserID == portalUserId).Select(
                    job => new IndividualProfileAppliedJobsVM()
                    {
                        OrganizationName =
                            job.JobOffer.OrganizationDetail.OrganizationDetails_Det.FirstOrDefault(
                                l => l.LanguageID == langId && l.PortalUsersID == job.JobOffer.PortalUsersID)
                                .OrganizationName,
                        ApplyDate = job.ApplyDate,
                        Title = SqlUdf.SubCodeName(job.JobOffer.JobTiltleID, langId),
                        JobDescription =
                            job.JobOffer.JobOfferDetails.FirstOrDefault(
                                l => l.LanguageID == langId && l.JobOfferID == job.JobOfferID).JobDescription,
                        ViewStatus = job.ViewStatus,
                        ExpiryDate = job.JobOffer.EndDate,
                        JobAppliedID = job.JobAppliedID,
                        JobOfferID = job.JobOfferID
                    }).OrderByDescending(o => o.ApplyDate).ToList();

                #endregion

                #region Applied Trainings

                indObj.AppliedTrainings = db.TrainingApplies.Where(j => j.IndPortalUserID == portalUserId).Select(
                    training => new IndividualProfileAppliedTrainingVM()
                    {
                        OrganizationName =
                            training.TrainingOffer.OrganizationDetail.OrganizationDetails_Det.FirstOrDefault(
                                l => l.LanguageID == langId && l.PortalUsersID == training.TrainingOffer.PortalUsersID)
                                .OrganizationName,
                        ApplyDate = training.ApplyDate,
                        CourseName = SqlUdf.SubCodeName(training.TrainingOffer.CourseNameID, langId),
                        TrainingDescription =
                            training.TrainingOffer.TrainingOfferDetails.FirstOrDefault(
                                l => l.LanguageID == langId && l.TrainingOfferID == training.TrainingAppliedID)
                                .CourseDescription,
                        ViewStatus = training.ViewStatus,
                        ExpiryDate = training.TrainingOffer.EndDate,
                        TrainingAppliedID = training.TrainingAppliedID,
                        TrainingOfferID = training.TrainingOfferID
                    }).OrderByDescending(o => o.ApplyDate).ToList();

                #endregion

                indObj.Reviews = new Dictionary<string, object>()
                {
                    {
                        "FirstName",
                        new GlobalString(
                            portalObj.First()
                                .IndividualDetail.IndividualDetailsDets.Select(
                                    x => new LocalString(x.LanguageID, x.FirstName))
                                .ToList(), "")
                    },
                    {
                        "LastName",
                        new GlobalString(
                            portalObj.First()
                                .IndividualDetail.IndividualDetailsDets.Select(
                                    x => new LocalString(x.LanguageID, x.LastName))
                                .ToList(), "")
                    },
                    {
                        "Address",
                        new GlobalString(
                            portalObj.First()
                                .IndividualDetail.IndividualDetailsDets.Select(
                                    x => new LocalString(x.LanguageID, x.Address))
                                .ToList(), "")
                    },
                    {
                        "Certificates",
                        portalObj.First()
                            .IndividualDetail.IndividualCertificationDetails.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualCertificationDetails_Det.Select(
                                            y => new LocalString(y.LanguageID, y.CertificateName)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "EduInstNames",
                        portalObj.First()
                            .IndividualDetail.IndividualEducationlevels.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualEducationlevelDets.Select(
                                            y => new LocalString(y.LanguageID, y.InstitutionName)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "EduInstTypes",
                        portalObj.First()
                            .IndividualDetail.IndividualEducationlevels.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualEducationlevelDets.Select(
                                            y => new LocalString(y.LanguageID, y.InstitutionType)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "EduCertTypes",
                        portalObj.First()
                            .IndividualDetail.IndividualEducationlevels.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualEducationlevelDets.Select(
                                            y => new LocalString(y.LanguageID, y.CertificationType)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "EduFaculties",
                        portalObj.First()
                            .IndividualDetail.IndividualEducationlevels.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualEducationlevelDets.Select(
                                            y => new LocalString(y.LanguageID, y.FacultyName)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "EduGrades",
                        portalObj.First()
                            .IndividualDetail.IndividualEducationlevels.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualEducationlevelDets.Select(
                                            y => new LocalString(y.LanguageID, y.Grade)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "ExpEmployers",
                        portalObj.First()
                            .IndividualDetail.IndividualExperienceDetails.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualExperienceDetails_Det.Select(
                                            y => new LocalString(y.LanguageID, y.EmployerName)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "ExpJobs",
                        portalObj.First()
                            .IndividualDetail.IndividualExperienceDetails.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualExperienceDetails_Det.Select(
                                            y => new LocalString(y.LanguageID, y.EmploymentJobDescription)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "TrProviders",
                        portalObj.First()
                            .IndividualDetail.IndividualTrainingDetails.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualTrainingDetails_Det.Select(
                                            y => new LocalString(y.LanguageID, y.TrainingProviderName)).ToList(), ""))
                            .ToList()
                    },
                    {
                        "TrNames",
                        portalObj.First()
                            .IndividualDetail.IndividualTrainingDetails.Where(x => x.IsDeleted == null)
                            .Select(
                                x =>
                                    new GlobalString(
                                        x.IndividualTrainingDetails_Det.Select(
                                            y => new LocalString(y.LanguageID, y.TrainingName)).ToList(), ""))
                            .ToList()
                    },
                };

            }
            return indObj;
        }

        public void Approve(string adminId, long reqKey, long indId, bool approved, string reason,
            Dictionary<string, object> newValues)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == indId
                                    && r.RequestType == "02900002" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte) Approval.Approved : (byte) Approval.Rejected;

                    var tr = db.IndividualDetails
                        .Where(r => r.PortalUsersID == indId)
                        .ToList().Single();

                    if (approved)
                        tr.Is_Approved = (byte) Approval.Approved;
                    else
                    {
                        tr.Is_Approved = (byte) Approval.Rejected;
                        tr.RejectReason = reason.Limit(500);
                    }

                    if (approved)
                    {
                        var drOtherSkills = db.IndividualOtherSkills.Where(r => r.PortalUsersID == indId).ToList();

                        if (drOtherSkills.Any())
                        {
                            db.IndividualOtherSkills.RemoveRange(drOtherSkills);

                            var newSkills = ((object[]) newValues["skills"]).Cast<Dictionary<string, object>>().ToList();
                            var drSkills = db.IndividualSkillsDetails.Where(r => r.PortalUsersID == indId).ToList();

                            newSkills =
                                newSkills.GroupBy(
                                    s => (string) s["industry"] + (string) s["skill"] + (string) s["level"])
                                    .Select(g => g.First()).ToList();

                            newSkills.RemoveAll(s => drSkills.Any(r =>
                                r.IndustryID == (string) s["industry"] &&
                                r.SkillID == (string) s["skill"] &&
                                r.SkillLevelID == (string) s["level"]));

                            foreach (var s in newSkills)
                                db.IndividualSkillsDetails.Add(new IndividualSkillsDetail
                                {
                                    PortalUsersID = indId,
                                    IndustryID = (string) s["industry"],
                                    SkillID = (string) s["skill"],
                                    SkillLevelID = (string) s["level"],
                                    PostDate = DateTime.UtcNow
                                });
                        }
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

        public IndividualVM UpdatePersonalInfo(IndividualVM vm)
        {
            var dbContext = new LMISEntities();
            try
            {
                var individualObject = dbContext.IndividualDetails.Single(i => i.PortalUsersID == vm.PortalUsersID);

                #region 1st: Update portal user

                individualObject.PortalUser.IDNumber = vm.PortalUser.IDNumber;
                individualObject.PortalUser.IDType = vm.PortalUser.IDType;
                if (individualObject.Email != vm.Email)
                {
                    var user = dbContext.AspNetUsers.Where(u => u.Id == vm.UserID).SingleOrDefault();
                    user.UserName = vm.Email;
                    user.Email = vm.Email;
                    dbContext.Entry(user).State = EntityState.Modified;
                }

                #endregion

                #region 2nd: Populate Individual object

                individualObject.MobileNo = vm.MobileNo;
                individualObject.TelephoneNo = vm.TelephoneNo;
                individualObject.DateOfBirth = vm.DateOfBirth;
                individualObject.Email = vm.Email;
                individualObject.MaritalStatusId = vm.MaritalStatusId;
                individualObject.GenderId = vm.GenderId;
                individualObject.MilitaryStatus_Id = vm.GenderId == "00200002" ? vm.MilitaryStatus_Id : string.Empty;
                individualObject.NationalityId = string.IsNullOrEmpty(vm.NationalityId)
                    ? individualObject.NationalityId
                    : vm.NationalityId;
                individualObject.CountryID = vm.CountryID;
                individualObject.CityID = vm.CityID;
                individualObject.IndividualMedicalID = vm.IndividualMedicalID;
                individualObject.AllowtoViewMyInfo = vm.AllowtoViewMyInfo;
                individualObject.UpdateDate = System.DateTime.Now;
                individualObject.UpdateUserID = individualObject.AspNetUser.Id;
                individualObject.PhotoPath = string.IsNullOrEmpty(vm.PhotoPath)
                    ? individualObject.PhotoPath
                    : vm.PhotoPath;
                individualObject.Is_Approved = (int) Approval.Pending;

                #endregion

                #region 3rd: Delete details

                var dr = individualObject.IndividualDetailsDets
                    .Where(r => r.PortalUsersID == vm.PortalUsersID)
                    .ToList();

                dbContext.IndividualDetailsDets.RemoveRange(dr);

                #endregion

                #region 4th: Add translations

                foreach (
                    IndividualTranslationVM translation in
                        vm.Translation.Where(t => t.FirstName != null || t.LastName != null || t.Address != null))
                {
                    IndividualDetailsDet translationObject = new IndividualDetailsDet
                    {
                        PortalUsersID = individualObject.PortalUsersID,
                        LanguageID = translation.LanguageID,
                        FirstName = translation.FirstName,
                        LastName = translation.LastName,
                        Address = translation.Address
                    };
                    individualObject.IndividualDetailsDets.Add(translationObject);
                }

                #endregion

                #region 5th: Save Individual object

                dbContext.Entry(individualObject).State = EntityState.Modified;

                dbContext.SaveChanges();

                #endregion

                return vm;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:");
                }
                throw;
            }
            catch
            {
                throw;
            }
        }

        private IndividualDetail PopulateRecord(IndividualVM vm, decimal portalUsersId, string userId)
        {
            return new IndividualDetail
            {
                PortalUsersID = portalUsersId,
                Email = vm.Email,
                MobileNo = vm.MobileNo,
                TelephoneNo = vm.TelephoneNo,
                GenderId = vm.GenderId,
                DateOfBirth = vm.DateOfBirth,
                MaritalStatusId = vm.MaritalStatusId,
                MilitaryStatus_Id = vm.MilitaryStatus_Id,
                NationalityId = vm.NationalityId,
                PhotoPath = vm.PhotoPath,
                Is_Approved = 1,
                AllowtoViewMyInfo = vm.AllowtoViewMyInfo,
                RejectReason = vm.RejectReason,
                PostDate = DateTime.Now,
                PostUserID = userId,
                IndividualMedicalID = vm.IndividualMedicalID,
                UserID = userId,
                CountryID = vm.CountryID,
                CityID = vm.CityID
            };
        }

        public decimal JobSeekers(bool unemployed)
        {
            using (var db = new LMISEntities())
            {
                if (unemployed)
                {

                    return db.IndividualDetails.Count(r => r.Is_Approved == 2) - db.IndividualExperienceDetails.Where(e => e.CurrentEmploymentStatus == 1 && e.IsDeleted == null).Select(s => s.PortalUsersID).Distinct().Count();


                }
                else
                {
                    return db.IndividualDetails.Count(r => r.Is_Approved == 2);
                }
                
            }
        }

    }
}
