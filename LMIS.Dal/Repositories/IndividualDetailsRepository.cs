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
using Microsoft.SqlServer.Server;
using System.Data.Entity.Validation;

namespace LMIS.Dal.Repositories
{
    public class IndividualDetailsRepository : IindividualDetailsRepository
    {
        private LMISEntities _ctx = new LMISEntities();
        private static readonly SubCodeRepository Mgrgeneral = new SubCodeRepository();
        public UserInfo GetUserInfo(string UserId)
        {

            var userinfo = (from us in _ctx.IndividualDetails
                            join puser in _ctx.PortalUsers on us.PortalUsersID equals puser.PortalUsersID
                            where us.UserID == UserId 
                            select new UserInfo
                            {
                                UserId = us.UserID,
                                PortalUserId = us.PortalUsersID,
                                IsApproved = us.Is_Approved == 2 ? true : false,
                                IsInternal = puser.Internal,
                                IsJobSeeker = puser.JobSeeker,
                                IsTrainingProvider = puser.TrainingProvider,
                                IsTrainingSeeker = puser.TrainingSeeker,
                                IsEmployer = puser.Employer,
                                IsResearcher = puser.Researcher,
                                IsIndividual = true,
                                Email = us.Email

                            }).ToList().FirstOrDefault();

            return userinfo;
        }

        public long PostNewSkills(UserInfo user, ref SkillsInformationVm vm)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //var id = vm.Id;

                    //if (id > 0) //Update
                    //{
                    //    var dr5 = db.IndividualSkillsDetails
                    //        .Where(r => r.IndividualSkillsDetailsID == id)
                    //        .ToList();

                    //    db.IndividualSkillsDetails.RemoveRange(dr5);

                    //    var dr6 = db.IndividualOtherSkills
                    //        .Where(r => r.IndividualOtherSkillsId == id)
                    //        .ToList();

                    //    db.IndividualOtherSkills.RemoveRange(dr6);
                    //}
                    //else //Insert
                    //{
                    var dr5 = db.IndividualSkillsDetails.Where(r => r.PortalUsersID == user.PortalUserId).ToList();

                    db.IndividualSkillsDetails.RemoveRange(dr5);

                    var dr6 = db.IndividualOtherSkills
                        .Where(r => r.PortalUsersID == user.PortalUserId)
                        .ToList();

                    db.IndividualOtherSkills.RemoveRange(dr6);

                    foreach (var r in vm.Skills.Where(a => !a.IsNew))
                        db.IndividualSkillsDetails.Add(new IndividualSkillsDetail()
                        {
                            IndustryID = r.Industry.id,
                            SkillID = r.Skill.id,
                            SkillLevelID = r.Level.id,
                            SkillTypeID = r.Type == null || string.IsNullOrWhiteSpace(r.Type.id) ? null : r.Type.id,
                            YearsOf_Experience = r.YOfExperience,
                            PortalUsersID = user.PortalUserId,
                            PostDate=DateTime.Today

                        });

                    foreach (var r in vm.Skills.Where(a => a.IsNew))
                        db.IndividualOtherSkills.Add(new IndividualOtherSkill()
                        {
                            IndustryId = r.Industry.id,
                            OtherSkill = r.Skill.desc,
                            SkillLevelId = r.Level.id,
                            IsReviewed = false,
                            PortalUsersID = user.PortalUserId
                        });
                    //}
                    var indvidual = db.IndividualDetails.Single(s => s.PortalUsersID == user.PortalUserId);
                    indvidual.Is_Approved = (int)Approval.Pending;
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
        public List<IndividualRegisterationVm> GetIndividualsList()
        {
            //Get all active records from the table
            var query = _ctx.IndividualDetailsDets.ToList();

            //Map results to a View Model list
            return query.Select(m => new IndividualRegisterationVm()
            {
                LanguageId = m.LanguageID,
                //   Address = m.Address,
                // FirstName = m.FirstName,
                // LastName = m.LastName,
                Email = m.IndividualDetail.Email,
                BirthDate = m.IndividualDetail.DateOfBirth,
                Gender = m.IndividualDetail.GenderId,
                Militarystatus = m.IndividualDetail.MilitaryStatus_Id,
                Maritalstatus = m.IndividualDetail.MaritalStatusId,
                // Militarystatus = ,
                MobileNumber = m.IndividualDetail.MobileNo,
                //TelephoneNumber = m.IndividualDetail.TelephoneNo,
                Nationality = m.IndividualDetail.NationalityId,
                // MobileNumber = m.IndividualDetail.idtype,
                // IdNumber = _ctx.PortalUsers.Select(p=>p.IDNumber),
                //  PhotoPath = m.IndividualDetail.PhotoPath,
                AllowtoViewMyInfo = m.IndividualDetail.AllowtoViewMyInfo,
                IndividualMedicalId = m.IndividualDetail.IndividualMedicalID,
                Approval = Approval.Approved
                /// PhotoPath = m.IndividualDetail.Is_Approved,
                // PhotoPath = m.IndividualDetail.PhotoPath,
                //  PhotoPath = m.IndividualDetail.PhotoPath,
                //ContactName = m.IndividualDetail..OrganizationContactInfoDetails.Select(d => new LocalString(d.LanguageID, d.ContactFullName)).ToList(),
                //  OrganizationName = m.OrganizationContact_Info.OrganizationDetail.OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList(),
                //// Title = m.OpportunitiesDetails.Select(d => new LocalString(d.LanguageID, d.OpportunityTitle)).ToList(),
                // FilePath = m.OpportunityFilePath,
                // StartDate = m.StartDate,
                // EndDate = m.EndDate,

                // IsInformal = m.IsInformal
            }).ToList();
        }
        public void CertificateDelete(long id, UserInfo user, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.IndividualCertificationDetails
                    .Where(r => r.IndividualCertificationID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = user.UserId;
                tr.DeleteReason = reason;
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }
        public void TrainingeDelete(long id, UserInfo user, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.IndividualTrainingDetails
                    .Where(r => r.IndividualTrainingID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = user.UserId;
                tr.DeleteReason = reason;
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }
        public void ExperienceDelete(long id, UserInfo user, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.IndividualExperienceDetails
                    .Where(r => r.IndividualExperienceID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = user.UserId;
                tr.DeleteReason = reason;
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }
        public void EducationDelete(long id, UserInfo user, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.IndividualEducationlevels
                    .Where(r => r.IndividualEducationlevelID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = user.UserId;
                tr.DeleteReason = reason;
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }
        public void SkillDelete(UserInfo user, long id, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.IndividualSkillsDetails
                    .Where(r => r.IndividualSkillsDetailsID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = user.UserId;
                tr.DeleteReason = reason;
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }

        public long NewExperienceInformation(UserInfo user, ref ExperienceInformationVm vm)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.IndividualExperienceID;

                    if (id > 0)     //Update
                    {
                        var tr = db.IndividualExperienceDetails
                        .Where(r => r.IsDeleted == null && r.IndividualExperienceID == id)
                        .ToList().Single();

                        tr.EmploymentStartDate = vm.StartDate;
                        tr.EmploymentEndDate = vm.EndDate;
                        tr.EmploymentJobTitle = vm.JobTitle;
                        tr.TypeOfEmployment = vm.TypeofEmployment;
                        tr.CurrentEmploymentStatus = vm.CurrentEmploymentStatus;
                        tr.UpdateUserID = user.UserId;
                        tr.UpdateDate = DateTime.Now;
                        tr.ExpYears = (vm.EndDate.HasValue) ? Math.Abs(vm.EndDate.Value.Year - vm.StartDate.Year) : Math.Abs(DateTime.Now.Year - vm.StartDate.Year);
                        tr.ExpMonths = (vm.EndDate.HasValue) ? Math.Abs(vm.EndDate.Value.Month - vm.StartDate.Month) : Math.Abs(DateTime.Now.Month - vm.StartDate.Month);
                        //  tr.ExpYears = (!vm.EndDate.HasValue) ? ((DateTime.Now.Year - vm.StartDate.Year) > 0) ? (DateTime.Now.Year - vm.StartDate.Year) : 0 : ((vm.EndDate.Value.Year - vm.StartDate.Year) > 0) ? (vm.EndDate.Value.Year - vm.StartDate.Year) : 0;
                        // tr.ExpMonths = (!vm.EndDate.HasValue) ? ((DateTime.Now.Month - vm.StartDate.Month) > 0) ? (DateTime.Now.Month - vm.StartDate.Month) : 0 : ((vm.EndDate.Value.Month - vm.StartDate.Month) > 0) ? (vm.EndDate.Value.Month - vm.StartDate.Month) : 0;
                        //Delete detail records
                        var dr = db.IndividualExperienceDetails_Det
                            .Where(r => r.IndividualExperienceID == id)
                            .ToList();

                        db.IndividualExperienceDetails_Det.RemoveRange(dr);
                    }
                    else            //Insert
                    {
                        var tr = new IndividualExperienceDetail
                        {
                            PortalUsersID = user.PortalUserId,
                            EmploymentStartDate = vm.StartDate,
                            EmploymentEndDate = vm.EndDate,
                            EmploymentJobTitle = vm.JobTitle,
                            TypeOfEmployment = vm.TypeofEmployment,
                            CurrentEmploymentStatus = vm.CurrentEmploymentStatus,
                            PostDate = DateTime.UtcNow,
                            PostUserID = user.UserId,
                            ExpYears = (vm.EndDate.HasValue) ? Math.Abs(vm.EndDate.Value.Year - vm.StartDate.Year) : Math.Abs(DateTime.Now.Year - vm.StartDate.Year),
                            ExpMonths = (vm.EndDate.HasValue) ? Math.Abs(vm.EndDate.Value.Month - vm.StartDate.Month) : Math.Abs(DateTime.Now.Month - vm.StartDate.Month)
                            //  tr.ExpYears = (!vm.EndDate.HasValue) ? ((DateTime.Now.Year - vm.StartDate.Year) > 0) ? (DateTime.Now.Year - vm.StartDate.Year) : 0 : ((vm.EndDate.Value.Year - vm.StartDate.Year) > 0) ? (vm.EndDate.Value.Year - vm.StartDate.Year) : 0;
                            // tr.ExpMonths = (!vm.EndDate.HasValue) ? ((DateTime.Now.Month - vm.StartDate.Month) > 0) ? (DateTime.Now.Month - vm.StartDate.Month) : 0 : ((vm.EndDate.Value.Month - vm.StartDate.Month) > 0) ? (vm.EndDate.Value.Month - vm.StartDate.Month) : 0;
                            //Delete detail records
                        };

                        db.IndividualExperienceDetails.Add(tr);
                        db.SaveChanges();
                        vm.IndividualExperienceID = (long)tr.IndividualExperienceID;

                    }
                    //Insert detail records
                    var mdr = new IndividualExperienceDetails_Det();

                    var ds = Utils.MultilingualDataSet(
                       new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.Name},
                            {"c2", vm.JobDescription},
                        });

                    foreach (var r in ds)
                        db.IndividualExperienceDetails_Det.Add(new IndividualExperienceDetails_Det()
                        {
                            IndividualExperienceID = vm.IndividualExperienceID,
                            LanguageID = r["c1"].L,
                            EmployerName = r["c1"].T,
                            EmploymentJobDescription = r["c2"].T
                        });
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:");
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.IndividualExperienceID;
        }
        public long PostNewCertificationInformation(UserInfo user, ref TrainingandCertificationVm vm)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.IndividualCertificationID;

                    if (id > 0)     //Update
                    {
                        var tr = db.IndividualCertificationDetails
                        .Where(r => r.IsDeleted == null && r.IndividualCertificationID == id)
                        .ToList().Single();

                        tr.CertificateIssueDate = vm.CertificationIssueDate;
                        tr.CertificateValidUntil = vm.CertificationValidUntil;
                        tr.PostDate = DateTime.Now;
                        tr.UpdateDate = DateTime.UtcNow;
                        tr.UpdateUserID = user.UserId;
                        //Delete detail records
                        var dr = db.IndividualCertificationDetails_Det
                            .Where(r => r.IndividualCertificationID == id)
                            .ToList();

                        db.IndividualCertificationDetails_Det.RemoveRange(dr);
                    }
                    else            //Insert
                    {
                        var tr = new IndividualCertificationDetail
                        {
                            CertificateValidUntil = vm.CertificationValidUntil,
                            CertificateIssueDate = vm.CertificationIssueDate,
                            PortalUsersID = user.PortalUserId,
                            PostDate = System.DateTime.Now,
                            PostUserID = user.UserId

                        };

                        db.IndividualCertificationDetails.Add(tr);
                        db.SaveChanges();
                        vm.IndividualCertificationID = (long)tr.IndividualCertificationID;

                    }
                    var mdr = new IndividualCertificationDetails_Det();

                    var ds = Utils.MultilingualDataSet(
                       new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.CertificationName}
                        });

                    foreach (var r in ds)
                        db.IndividualCertificationDetails_Det.Add(new IndividualCertificationDetails_Det()
                        {
                            IndividualCertificationID = vm.IndividualCertificationID,
                            LanguageID = r["c1"].L,
                            CertificateName = r["c1"].T
                        });
                    var indvidual = db.IndividualDetails.Single(s => s.PortalUsersID == user.PortalUserId);
                    indvidual.Is_Approved = (int) Approval.Pending;
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.IndividualCertificationID;
        }
        public long PostNewTrainingInformation(UserInfo user, ref TrainingandCertificationVm vm)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.IndividualTrainingID;

                    if (id > 0)     //Update
                    {
                        var tr = db.IndividualTrainingDetails
                        .Where(r => r.IsDeleted == null && r.IndividualTrainingID == id)
                        .ToList().Single();

                        tr.TrainingStartDate = vm.StartDate;
                        tr.TrainingEndDate = vm.EndDate;
                        tr.PostDate = DateTime.Now;
                        tr.UpdateDate = DateTime.UtcNow;
                        tr.UpdateUserID = user.UserId;
                        //Delete detail records
                        var dr = db.IndividualTrainingDetails_Det
                            .Where(r => r.IndividualTrainingID == id)
                            .ToList();

                        db.IndividualTrainingDetails_Det.RemoveRange(dr);
                    }
                    else            //Insert
                    {
                        var tr = new IndividualTrainingDetail
                        {
                            TrainingStartDate = vm.StartDate,
                            TrainingEndDate = vm.EndDate,
                            PortalUsersID = user.PortalUserId,
                            PostDate = System.DateTime.Now,
                            PostUserID = user.UserId

                        };
                        db.IndividualTrainingDetails.Add(tr);
                        db.SaveChanges();
                        vm.IndividualTrainingID = (long)tr.IndividualTrainingID;

                    }
                    //Insert detail records
                    var mdr = new IndividualTrainingDetails_Det();

                    var ds = Utils.MultilingualDataSet(
                       new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.trainingname},
                            {"c2", vm.TrainingProvider},
                        });

                    foreach (var r in ds)
                        db.IndividualTrainingDetails_Det.Add(new IndividualTrainingDetails_Det()
                        {
                            IndividualTrainingID = vm.IndividualTrainingID,
                            LanguageID = r["c1"].L,
                            TrainingName = r["c1"].T,
                            TrainingProviderName = r["c2"].T
                        });
                    var indvidual = db.IndividualDetails.Single(s => s.PortalUsersID == user.PortalUserId);
                    indvidual.Is_Approved = (int)Approval.Pending;
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.IndividualTrainingID;
        }
        public long PostNewEducationInformation(ref EducationalInformationVm vm, UserInfo user)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.IndividualEducationlevelID;

                    if (id > 0)     //Update
                    {
                        var tr = db.IndividualEducationlevels
                        .Where(r => r.IsDeleted == null && r.IndividualEducationlevelID == id)
                        .ToList().Single();

                        tr.GraduationYear = vm.graduationyear;
                        tr.Degree = vm.Degree;
                        tr.GradePrecentage = vm.Percentage;
                        tr.PortalUsersID = user.PortalUserId;
                        tr.GradeGPA = vm.GradeGPA;

                        // tr.Is_Approved = (byte)vm.Approval;
                        // tr.IsInformal = vm.IsInformal;
                        tr.UpdateUserID = user.UserId;
                        tr.UpdateDate = DateTime.UtcNow;
                        //Delete detail records
                        var dr = db.IndividualEducationlevelDets
                            .Where(r => r.IndividualEducationlevelID == id)
                            .ToList();

                        db.IndividualEducationlevelDets.RemoveRange(dr);
                    }
                    else            //Insert
                    {
                        var tr = new IndividualEducationlevel
                        {
                            GraduationYear = vm.graduationyear,
                            Degree = vm.Degree,
                            GradePrecentage = vm.Percentage,
                            PortalUsersID = user.PortalUserId,
                            LevelOfEducation = vm.EducationalLevelId,
                            PostDate = System.DateTime.Now,
                            GradeGPA = vm.GradeGPA
                          
                        };

                        db.IndividualEducationlevels.Add(tr);
                        db.SaveChanges();
                        vm.IndividualEducationlevelID = (long)tr.IndividualEducationlevelID;

                    }
                    //Insert detail records
                    foreach (var r in vm.Name.ToLocalStrings())
                    {
                        var dr = new IndividualEducationlevelDet()
                        {
                            IndividualEducationlevelID = vm.IndividualEducationlevelID,
                            LanguageID = r.L,
                            InstitutionName = r.T,
                            InstitutionType = r.L == 1 ? vm.InstitutionType.English : r.L == 3 ? vm.InstitutionType.Arabic : vm.InstitutionType.French,
                            CertificationType = r.L == 1 ? vm.CertificationType.English : r.L == 3 ? vm.CertificationType.Arabic : vm.CertificationType.French,
                            FacultyName = r.L == 1 ? vm.FacultyName.English : r.L == 3 ? vm.FacultyName.Arabic : vm.FacultyName.French,
                            Grade = r.L == 1 ? vm.Grade.English : r.L == 3 ? vm.Grade.Arabic : vm.Grade.French
                        };
                        db.IndividualEducationlevelDets.Add(dr);
                    }
                    var indvidual = db.IndividualDetails.Single(s => s.PortalUsersID == user.PortalUserId);
                    indvidual.Is_Approved = (int)Approval.Pending;
                    db.SaveChanges();


                    transaction.Commit();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:");
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.IndividualEducationlevelID;
        }
       
        public TrainingandCertificationVm GetCertificateInformation(long id, UserInfo user)
        {
            List<IndividualCertificationDetail> ds;

            using (var db = new LMISEntities())
            {
                ds = db.IndividualCertificationDetails
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId && r.IndividualCertificationID == id)
                    .ToList();

                return ds.Select(r => new TrainingandCertificationVm()
                {
                    IndividualCertificationID = (long)r.IndividualCertificationID,
                    CertificationName = r.IndividualCertificationDetails_Det.Select(d => new LocalString(d.LanguageID, d.CertificateName)).ToList(),
                    CertificationIssueDate = r.CertificateIssueDate,
                    CertificationValidUntil = r.CertificateValidUntil
                })
               .SingleOrDefault();
            }
        }
        public TrainingandCertificationVm GetTrainingInformation(long id, UserInfo user)
        {
            List<IndividualTrainingDetail> ds;

            using (var db = new LMISEntities())
            {
                ds = db.IndividualTrainingDetails
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId && r.IndividualTrainingID == id)
                    .ToList();

                return ds.Select(r => new TrainingandCertificationVm()
                {
                    IndividualTrainingID = (long)r.IndividualTrainingID,
                    trainingname = r.IndividualTrainingDetails_Det.Select(d => new LocalString(d.LanguageID, d.TrainingName)).ToList(),
                    TrainingProvider = r.IndividualTrainingDetails_Det.Select(d => new LocalString(d.LanguageID, d.TrainingProviderName)).ToList(),
                    EndDate = r.TrainingEndDate,
                    StartDate = r.TrainingStartDate
                })
               .SingleOrDefault();
            }
        }
        public ExperienceInformationVm GetExperienceInformation(long id, UserInfo user)
        {
            List<IndividualExperienceDetail> ds;

            using (var db = new LMISEntities())
            {
                ds = db.IndividualExperienceDetails
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId && r.IndividualExperienceID == id)
                    .ToList();

                return ds.Select(r => new ExperienceInformationVm()
                {
                    IndividualExperienceID = (long)r.IndividualExperienceID,
                    JobTitle = r.EmploymentJobTitle,
                    Name = r.IndividualExperienceDetails_Det.Select(d => new LocalString(d.LanguageID, d.EmployerName)).ToList(),
                    JobDescription = r.IndividualExperienceDetails_Det.Select(d => new LocalString(d.LanguageID, d.EmploymentJobDescription)).ToList(),
                    TypeofEmployment = r.TypeOfEmployment.Trim(),
                    CurrentEmploymentStatus = r.CurrentEmploymentStatus,
                    EndDate = (r.EmploymentEndDate.HasValue) ? DateTime.Parse(r.EmploymentEndDate.Value.Date.ToShortDateString()) : (DateTime?)null,
                    StartDate = DateTime.Parse(r.EmploymentStartDate.Date.ToShortDateString())
                })
               .SingleOrDefault();
            }

        }
        public EducationalInformationVm GetEducationInformation(long id, UserInfo user)
        {
            List<IndividualEducationlevel> ds;

            using (var db = new LMISEntities())
            {
                ds = db.IndividualEducationlevels
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId && r.IndividualEducationlevelID == id)
                    .ToList();
                GlobalString nullname = new GlobalString("", "", "");
                //  nullname. = null;
                return ds.Select(r => new EducationalInformationVm()
                {
                    IndividualEducationlevelID = (long)r.IndividualEducationlevelID,
                    Degree = (!string.IsNullOrWhiteSpace(r.Degree)) ? r.Degree.Trim() : null,
                    Name = r.IndividualEducationlevelDets.Select(d => new LocalString(d.LanguageID, d.InstitutionName)).ToList(),
                    EducationalLevelId = r.LevelOfEducation,
                    Grade = r.IndividualEducationlevelDets.Select(d => new LocalString(d.LanguageID, d.Grade)).ToList(),
                    graduationyear = r.GraduationYear,
                    Percentage = (r.GradePrecentage.HasValue) ? r.GradePrecentage.Value : 0,
                    InstitutionType = r.IndividualEducationlevelDets.Select(d => new LocalString(d.LanguageID, d.InstitutionType)).ToList(),
                    CertificationType = r.IndividualEducationlevelDets.Select(d => new LocalString(d.LanguageID, d.CertificationType)).ToList(),
                    FacultyName = r.IndividualEducationlevelDets.Select(d => new LocalString(d.LanguageID, d.FacultyName)).ToList(),
                    GradeGPA = (r.GradeGPA.HasValue) ? (float)r.GradeGPA.Value : 0
                })
               .SingleOrDefault();
            }

        }
        public List<TrainingandCertificationVm> CertificationList(UserInfo user)
        {
            List<IndividualCertificationDetail> ds;

            using (var db = new LMISEntities())
            {

                ds = db.IndividualCertificationDetails
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId)
                    .ToList();


                return ds.Select(r => new TrainingandCertificationVm()
                {
                    IndividualCertificationID = (long)r.IndividualCertificationID,
                    CertificationName = r.IndividualCertificationDetails_Det.Select(d => new LocalString(d.LanguageID, d.CertificateName)).ToList(),
                    CertificationIssueDate = r.CertificateIssueDate,
                    CertificationValidUntil = r.CertificateValidUntil
                })
                .ToList();
            }
        }
        public List<SkillsInformationVm.TrainingSkill> SkillsList(UserInfo user, int langId = 1)
        {
            using (var db = new LMISEntities())
            {
                var Skills = db.IndividualSkillsDetails.Where(r => r.PortalUsersID == user.PortalUserId).Select(a => new SkillsInformationVm.TrainingSkill
                        {
                            IsNew = false,
                            Industry = new CodeSet { id = a.IndustryID, desc = SqlUdf.SubCodeName(a.IndustryID, langId) },
                            Level = new CodeSet { id = a.SkillLevelID, desc = SqlUdf.SubCodeName(a.SkillLevelID, langId) },
                            Skill = new CodeSet { id = a.SkillID, desc = SqlUdf.SubCodeName(a.SkillID, langId) },
                            Type = new CodeSet { id = a.SkillTypeID, desc = SqlUdf.SubCodeName(a.SkillTypeID, langId) },
                            YOfExperience = a.YearsOf_Experience
                        });
                var OtherSkills = db.IndividualOtherSkills.Where(r => r.PortalUsersID == user.PortalUserId).Select(a => new SkillsInformationVm.TrainingSkill
                        {
                            IsNew = true,
                            Industry = new CodeSet { id = a.IndustryId, desc = SqlUdf.SubCodeName(a.IndustryId, langId) },
                            Level = new CodeSet { id = a.SkillLevelId, desc = SqlUdf.SubCodeName(a.SkillLevelId, langId) },
                            Skill = new CodeSet { id = null, desc = a.OtherSkill },
                            Type = new CodeSet { id = null, desc = null }
                            // YOfExperience = a.YearsOf_Experience
                        });
                var TrainingSkills = new List<SkillsInformationVm.TrainingSkill>();
                var allTrainingSkills = TrainingSkills.Concat(Skills)
                                    .Concat(OtherSkills)
                                    .ToList();
                // TrainingSkills.Add(Skills);

                return allTrainingSkills.ToList();
            }

        }
        public List<TrainingandCertificationVm> TrainingList(UserInfo user)
        {
            List<IndividualTrainingDetail> ds;

            using (var db = new LMISEntities())
            {

                ds = db.IndividualTrainingDetails
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId)
                    .ToList();


                return ds.Select(r => new TrainingandCertificationVm()
                {
                    IndividualTrainingID = (long)r.IndividualTrainingID,
                    trainingname = r.IndividualTrainingDetails_Det.Select(d => new LocalString(d.LanguageID, d.TrainingName)).ToList(),
                    TrainingProvider = r.IndividualTrainingDetails_Det.Select(d => new LocalString(d.LanguageID, d.TrainingProviderName)).ToList(),
                    StartDate = r.TrainingStartDate,
                    EndDate = r.TrainingEndDate
                })
                .ToList();
            }

        }
        public List<ExperienceInformationVm> ExperienceList(UserInfo user)
        {
            List<IndividualExperienceDetail> ds;

            using (var db = new LMISEntities())
            {

                ds = db.IndividualExperienceDetails
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId)
                    .ToList();


                return ds.Select(r => new ExperienceInformationVm()
                {
                    IndividualExperienceID = (long)r.IndividualExperienceID,
                    Name = r.IndividualExperienceDetails_Det.Select(d => new LocalString(d.LanguageID, d.EmployerName)).ToList(),
                    ExpYears = r.ExpYears,
                    JobTitle = r.EmploymentJobTitle,
                    ExpMonths = r.ExpMonths
                })
                .ToList();
            }
        }
        public List<EducationalInformationVm> EducationList(UserInfo user)
        {
            // List<IndividualEducationlevel> ds;

            using (var db = new LMISEntities())
            {

                var EducationList = db.IndividualEducationlevels.Where(r => r.IsDeleted == null && r.PortalUsersID == user.PortalUserId).Select(r => new EducationalInformationVm()
               {
                   IndividualEducationlevelID = (long)r.IndividualEducationlevelID,
                   EducationName = r.IndividualEducationlevelDets.FirstOrDefault().InstitutionName,
                   EducationalLevelId = r.LevelOfEducation.Trim(),
                   EducationType = r.IndividualEducationlevelDets.FirstOrDefault().InstitutionType,
                   EducationalLevelName = SqlUdf.SubCodeName("00" + r.LevelOfEducation.Trim(), 1)
               });
                var c = EducationList.ToList();
                return EducationList.ToList();

            }
        }
        public IndividualRegisterationVm GetPersonalInformation(UserInfo user)
        {
            List<PortalUser> ds;

            using (var db = new LMISEntities())
            {
                ds = db.PortalUsers
                    .Where(r => r.PortalUsersID == user.PortalUserId).ToList();
                return ds.Select(r => new IndividualRegisterationVm()
                {
                    IdType = r.IDType,
                    NationailtyIDorPassportID = r.IDNumber,
                    RegisterationId = (long)r.PortalUsersID,
                    Email = r.IndividualDetail.Email,
                    MobileNumber = r.IndividualDetail.MobileNo,
                    TelephoneNo = r.IndividualDetail.TelephoneNo,
                    Gender = r.IndividualDetail.GenderId,
                    BirthDate = r.IndividualDetail.DateOfBirth,
                    Maritalstatus = r.IndividualDetail.MaritalStatusId,
                    Militarystatus = r.IndividualDetail.MilitaryStatus_Id,
                    Nationality = r.IndividualDetail.NationalityId,
                    Country = r.IndividualDetail.CountryID,
                    City = r.IndividualDetail.CityID,
                    AllowtoViewMyInfo = r.IndividualDetail.AllowtoViewMyInfo,
                    IndividualMedicalId = r.IndividualDetail.IndividualMedicalID,
                    FirstName = r.IndividualDetail.IndividualDetailsDets.Select(d => new LocalString(d.LanguageID, d.FirstName)).ToList(),
                    LastName = r.IndividualDetail.IndividualDetailsDets.Select(d => new LocalString(d.LanguageID, d.LastName)).ToList(),
                    Address = r.IndividualDetail.IndividualDetailsDets.Select(d => new LocalString(d.LanguageID, d.Address)).ToList()

                })
               .SingleOrDefault();
            }

        }
        public long PostPersonalInformation(ref IndividualRegisterationVm vm, UserInfo user)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = user.PortalUserId;

                    if (id > 0)     //Update
                    {
                        var tp = db.PortalUsers.Where(r => r.PortalUsersID == user.PortalUserId)
                            .ToList().Single();
                        tp.IDType = vm.IdType;
                        tp.IDNumber = vm.NationailtyIDorPassportID;

                        var tr = db.IndividualDetails
                            .Where(r => r.PortalUsersID == user.PortalUserId)
                            .ToList().Single();
                        tr.Is_Approved = 1;
                        tr.Email = vm.Email;
                        tr.MobileNo = vm.MobileNumber;
                        tr.TelephoneNo = vm.TelephoneNo;
                        tr.GenderId = vm.Gender;
                        tr.DateOfBirth = vm.BirthDate;
                        tr.MaritalStatusId = vm.Maritalstatus;
                        tr.MilitaryStatus_Id = vm.Militarystatus;
                        tr.NationalityId = vm.Nationality;
                        tr.CountryID = vm.Country;
                        tr.CityID = vm.City;
                        tr.AllowtoViewMyInfo = vm.AllowtoViewMyInfo;
                        tr.UpdateUserID = user.UserId;
                        tr.IndividualMedicalID = vm.IndividualMedicalId;
                        tr.UpdateUserID = user.UserId;
                        tr.UpdateDate = DateTime.UtcNow;
                        vm.RegisterationId = (long)tr.PortalUsersID;
                        var dr = db.IndividualDetailsDets
                            .Where(r => r.PortalUsersID == user.PortalUserId)
                            .ToList();

                        db.IndividualDetailsDets.RemoveRange(dr);
                    }
                    else            //Insert
                    {
                        var tr = new PortalUser
                        {
                            IDType = vm.IdType,
                            IDNumber = vm.NationailtyIDorPassportID,
                            JobSeeker = true,
                            TrainingSeeker = true,
                            UserCategory = "IND",
                            UserSubCategory = "IND"

                        };

                        db.PortalUsers.Add(tr);
                        db.SaveChanges();
                        vm.RegisterationId = (long)tr.PortalUsersID;


                        var dr = new IndividualDetail()
                        {
                            PortalUsersID = vm.RegisterationId,
                            Email = vm.Email,
                            MobileNo = vm.MobileNumber,
                            TelephoneNo = vm.TelephoneNo,
                            GenderId = vm.Gender,
                            DateOfBirth = vm.BirthDate,
                            MaritalStatusId = vm.Maritalstatus,
                            MilitaryStatus_Id = vm.Militarystatus,
                            NationalityId = vm.Nationality,
                            CountryID = vm.Country,
                            CityID = vm.City,
                            // PhotoPath = "g",
                            Is_Approved = 1,//(byte)vm.Approval,
                            AllowtoViewMyInfo = vm.AllowtoViewMyInfo,
                            //RejectReason = "v",
                            PostDate = System.DateTime.Now,
                            UpdateDate = System.DateTime.Now,
                            PostUserID = user.UserId,
                            UpdateUserID = user.UserId,
                            IndividualMedicalID = vm.IndividualMedicalId,
                            UserID = user.UserId
                        };
                        db.IndividualDetails.Add(dr);
                        db.SaveChanges();
                    }
                    // Insert More Details
                    var mdr = new IndividualDetailsDet();
                    mdr.PortalUsersID = vm.RegisterationId;
                    foreach (var r in vm.FirstName.ToLocalStrings())
                    {
                        mdr.LanguageID = r.L;
                        mdr.FirstName = r.T;
                        db.IndividualDetailsDets.Add(mdr);
                    }
                    foreach (var r in vm.LastName.ToLocalStrings())
                    {
                        mdr.LanguageID = r.L;
                        mdr.LastName = r.T;
                        db.IndividualDetailsDets.Add(mdr);
                    }
                    foreach (var r in vm.Address.ToLocalStrings())
                    {
                        mdr.LanguageID = r.L;
                        mdr.Address = r.T;
                        db.IndividualDetailsDets.Add(mdr);
                    }
                    db.IndividualDetailsDets.Add(mdr);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.RegisterationId;
        }
    }
}
