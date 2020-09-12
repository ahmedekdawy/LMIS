using LMIS.Dal.Entity;
using LMIS.Dal.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Organization;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        #region Selection

        public OrganizationVM GetOrganizationDetails(long portalUserId, Language languageId = Language.English)
        {
            OrganizationVM organizationObject;

            using (var db = new LMISEntities())
            {
                var orgObj = db.OrganizationDetails.Where(org => org.PortalUsersID == portalUserId);
                organizationObject = orgObj.Select(org => new OrganizationVM()
                {
                    #region Load main object with translation
                    PortalUsersID = (long)org.PortalUsersID,
                    OrganizationLogoPath = org.OrganizationLogoPath,
                    OrganizationSize = org.OrganizationSize,
                    CountryID = org.CountryID,
                    CityID = org.CityID,
                    ZipPostalCode = org.ZipPostalCode,
                    Telephone = org.Telephone,
                    OrganizationWebsite = org.OrganizationWebsite,
                    OrganizationProfilePath = org.OrganizationProfilePath,
                    EconomicActivity = org.EconomicActivity,
                    IndustryType = org.IndustryType,
                    YearsofExperienceID = org.YearsofExperienceID,
                    EstablishmentDate = org.EstablishmentDate,
                    RegistrationNumberWithITC = org.RegistrationNumberWithITC,
                    Is_Approved = org.Is_Approved,
                    IsDiscalaimerApproved = org.IsDiscalaimerApproved,
                    PostDate = org.PostDate,
                    #endregion

                    #region Translation
                    Translation = org.OrganizationDetails_Det.Select(t => new OrganizationTranslationVM()
                    {
                        LanguageID = t.LanguageID,
                        OrganizationName = t.OrganizationName,
                        Address = t.Address,
                        OtherIndustryType = t.OtherIndustryType
                    }).ToList(),
                    #endregion

                    #region Certificates Info
                    PortalUser = new PortalUserVM()
                    {
                        PortalUsersID = org.PortalUser.PortalUsersID,
                        IDType = org.PortalUser.IDType,
                        IDNumber = org.PortalUser.IDNumber,
                        UserCategory = org.PortalUser.UserCategory,
                        UserSubCategory = org.PortalUser.UserSubCategory,
                        TrainingProvider = org.PortalUser.TrainingProvider,
                        Employer = org.PortalUser.Employer,
                        JobSeeker = org.PortalUser.JobSeeker,
                        TrainingSeeker = org.PortalUser.TrainingSeeker,
                        Researcher = org.PortalUser.Researcher,
                        Internal = org.PortalUser.Internal,
                        IsSubscriper = org.PortalUser.IsSubscriper,
                    },
                    #endregion

                }).Single();

                #region load localized values
                organizationObject.OrganizationNameLocalized = orgObj.First().OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList();
                organizationObject.AddressLocalized = orgObj.First().OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.Address)).ToList();
                organizationObject.OtherIndustryTypeLocalized = orgObj.First().OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.OtherIndustryType)).ToList();
                #endregion

            }
            return organizationObject;
        }

        public OrganizationProfileVM GetProfile(long portalUserId, int langId = 1)
        {
            OrganizationProfileVM ret;

            using (var db = new LMISEntities())
            {
                var orgObj = db.OrganizationDetails.Where(org => org.PortalUsersID == portalUserId);

                ret = orgObj.Select(org => new OrganizationProfileVM()
                {
                    #region Load main object with translation

                    PortalUsersID = (long)org.PortalUsersID,
                    OrganizationLogoPath = org.OrganizationLogoPath,
                    OrganizationSize = org.OrganizationSize,
                    Country = SqlUdf.SubCodeName(org.CountryID, langId),
                    City = SqlUdf.SubCodeName(org.CityID, langId),
                    ZipPostalCode = org.ZipPostalCode,
                    Telephone = org.Telephone,
                    OrganizationWebsite = org.OrganizationWebsite,
                    OrganizationProfilePath = org.OrganizationProfilePath,
                    EconomicActivity = SqlUdf.SubCodeName(org.EconomicActivity, langId),
                    IndustryType = SqlUdf.SubCodeName(org.IndustryType, langId),
                    YearsofExperience = SqlUdf.SubCodeName(org.YearsofExperienceID, langId),
                    EstablishmentDate = org.EstablishmentDate,
                    RegistrationNumberWithITC = org.RegistrationNumberWithITC,
                    Is_Approved = org.Is_Approved,
                    IsDiscalaimerApproved = org.IsDiscalaimerApproved,
                    PostDate = org.PostDate,
                    Approval = (Approval)org.Is_Approved,
                    RejectReason = org.RejectReason,

                    #endregion

                    #region Portal user data

                    IDType = SqlUdf.SubCodeName(org.PortalUser.IDType, langId),
                    IDNumber = org.PortalUser.IDNumber,
                    UserCategory = SqlUdf.SubCodeName(org.PortalUser.UserCategory, langId),
                    UserSubCategory = SqlUdf.SubCodeName(org.PortalUser.UserSubCategory, langId),
                    TrainingProvider = org.PortalUser.TrainingProvider,
                    Employer = org.PortalUser.Employer,
                    TrainingSeeker = org.PortalUser.TrainingSeeker

                    #endregion

                }).Single();

                #region load localized values

                var det = orgObj.Single()
                    .OrganizationDetails_Det.Where(d => d.PortalUsersID == portalUserId).ToList();

                ret.GS = new Dictionary<string, GlobalString>
                {
                    {"OrgName", new GlobalString(det.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList())},
                    {"Address", new GlobalString(det.Select(d => new LocalString(d.LanguageID, d.Address)).ToList())},
                    {"OtherIndustry", new GlobalString(det.Select(d => new LocalString(d.LanguageID, d.OtherIndustryType)).ToList())}
                };

                ret.OrganizationName = ret.GS["OrgName"].ToLocalString((Language) langId, true).T;
                ret.Address = ret.GS["Address"].ToLocalString((Language)langId, true).T;
                ret.OtherIndustryType = ret.GS["OtherIndustry"].ToLocalString((Language)langId, true).T;

                #endregion

            }

            return ret;
        }

        public void Approve(string adminId, long reqKey, long orgId, bool approved, string reason)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var log = db.RequestLogs
                        .Where(r => r.ID == reqKey && r.RequestID == orgId
                            && r.RequestType == "02900001" && r.Is_Approved == 1)
                        .ToList().Single();

                    log.AdminID = adminId;
                    log.Is_Approved = approved ? (byte)Approval.Approved : (byte)Approval.Rejected;

                    var tr = db.OrganizationDetails
                        .Where(r => r.PortalUsersID == orgId)
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

        #endregion

        #region Modify Data

        public void Post(ref OrganizationVM vm, string userId)
        {
            var dbContext = new LMISEntities();

            #region 1st: Add portal user

            var portalUser = new PortalUser
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

            #region 2nd: Add AspNetUser
            //    var user = organizationVM.ContactPersons.FirstOrDefault().User;
            //    AspNetUser aspNetUser = new AspNetUser
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Email = user.Email,
            //        EmailConfirmed = false,
            //        PasswordHash = user.PasswordHash,
            //        SecurityStamp = user.SecurityStamp,
            //        PhoneNumber = user.PhoneNumber,
            //        PhoneNumberConfirmed = false,
            //        TwoFactorEnabled = user.TwoFactorEnabled,
            //        LockoutEnabled = false,
            //        AccessFailedCount = 0,
            //        UserName = user.Email
            //    };
            //    dbContext.AspNetUsers.Add(aspNetUser);
            //    dbContext.SaveChanges();
            #endregion

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                #region 3rd: Populate Organization object
                var organizationObject = new OrganizationDetail();

                PopulateRecord(ref organizationObject, vm, portalUser.PortalUsersID);
                organizationObject.PostDate = DateTime.UtcNow;
                organizationObject.PostUserID = userId;
                #endregion

                #region 4th: Add translations
                foreach (var translation in vm.Translation.Where(t => t.OrganizationName != null || t.Address != null || t.OtherIndustryType != null))
                {
                    OrganizationDetails_Det translationObject = new OrganizationDetails_Det
                    {
                        PortalUsersID = portalUser.PortalUsersID,
                        LanguageID = translation.LanguageID,
                        OrganizationName = translation.OrganizationName,
                        Address = translation.Address,
                        OtherIndustryType = translation.OtherIndustryType
                    };
                    organizationObject.OrganizationDetails_Det.Add(translationObject);
                }
                #endregion

                #region 5th: Add Contact persons
                foreach (var person in vm.ContactPersons)
                {
                    var contactPerson = new OrganizationContact_Info
                    {
                        OrganizationContactID = person.OrganizationContactID,
                        PortalUsersID = portalUser.PortalUsersID,
                        JobTitleID = person.JobTitleID,
                        Telephone = person.Telephone,
                        Mobile = person.Mobile,
                        Fax = person.Fax,
                        Email = person.Email,
                        AuthorizationletterPath = person.AuthorizationletterPath,
                        IsApproved = person.IsApproved,
                        RejectReason = person.RejectReason,
                        IsDeleted = person.IsDeleted,
                        DeleteReason = person.DeleteReason,
                        PostDate = DateTime.Now,
                        PostUSerID = userId,
                        UserID = userId,
                    };
                    organizationObject.OrganizationContact_Info.Add(contactPerson);

                    #region 6th: Add translations to contact person
                    foreach (var translation in person.Translation.Where(t => t.ContactFullName != null && t.Department != null))
                    {
                        var translationObject = new OrganizationContactInfoDetail
                        {
                            OrganizationContactID = contactPerson.OrganizationContactID,
                            LanguageID = translation.LanguageID,
                            ContactFullName = translation.ContactFullName,
                            Department = translation.Department
                        };
                        contactPerson.OrganizationContactInfoDetails.Add(translationObject);
                    }
                    #endregion
                }
                #endregion

                #region 6th: Save Organization object
                dbContext.OrganizationDetails.Add(organizationObject);
                dbContext.SaveChanges();
                transaction.Commit();
                #endregion
            }
        }

        public void Update(ref OrganizationVM vm, string userId)
        {
            var dbContext = new LMISEntities();

            var portalUserId = vm.PortalUsersID;

            #region 1st: Update portal user

            var portalUser = dbContext.PortalUsers.Single(pu => pu.PortalUsersID == portalUserId);

            // portalUser.PortalUsersID = organizationVM.PortalUser.PortalUsersID;
            portalUser.IDType = vm.PortalUser.IDType;
            portalUser.IDNumber = vm.PortalUser.IDNumber;
            portalUser.UserCategory = vm.PortalUser.UserCategory;
            portalUser.UserSubCategory = vm.PortalUser.UserSubCategory;
            portalUser.TrainingProvider = vm.PortalUser.TrainingProvider;
            portalUser.Employer = vm.PortalUser.Employer;
            portalUser.JobSeeker = vm.PortalUser.JobSeeker;
            portalUser.TrainingSeeker = vm.PortalUser.TrainingSeeker;
            portalUser.Researcher = vm.PortalUser.Researcher;
            portalUser.Internal = vm.PortalUser.Internal;
            portalUser.IsSubscriper = vm.PortalUser.IsSubscriper;

            dbContext.Entry(portalUser).State = EntityState.Modified;
            dbContext.SaveChanges();

            #endregion

            using (var transaction = dbContext.Database.BeginTransaction())
            {

                #region 3rd: Populate Organization object

                var organizationObject = dbContext.OrganizationDetails.Single(org => org.PortalUsersID == portalUserId);
                PopulateRecord(ref organizationObject, vm, vm.PortalUsersID);
                organizationObject.UpdateDate = DateTime.UtcNow;
                organizationObject.UpdateUserID = userId;

                #endregion

                #region remove old translations
                var details = dbContext.OrganizationDetails_Det
                    .Where(r => r.PortalUsersID == portalUserId)
                    .ToList();
                foreach (var row in details)
                    organizationObject.OrganizationDetails_Det.Remove(row);

                dbContext.OrganizationDetails_Det.RemoveRange(details);
                //dbContext.SaveChanges();
                #endregion

                #region 4th: Add translations
                foreach (var translation in vm.Translation.Where(t => t.OrganizationName != null || t.Address != null || t.OtherIndustryType != null))
                {
                    var translationObject = new OrganizationDetails_Det
                    {
                        PortalUsersID = portalUser.PortalUsersID,
                        LanguageID = translation.LanguageID,
                        OrganizationName = translation.OrganizationName,
                        Address = translation.Address,
                        OtherIndustryType = translation.OtherIndustryType
                    };
                    organizationObject.OrganizationDetails_Det.Add(translationObject);
                }
                #endregion

                #region 6th: Save Organization object
                organizationObject.UpdateUserID = userId;
                organizationObject.UpdateDate = DateTime.UtcNow;

                dbContext.Entry(organizationObject).State = EntityState.Modified;
                dbContext.SaveChanges();
                transaction.Commit();
                #endregion
            }
        }

        private static void PopulateRecord(ref OrganizationDetail orgDet, OrganizationVM vm, decimal? portalUsersId)
        {
            if (portalUsersId.HasValue)
                orgDet.PortalUsersID = portalUsersId.Value;
            orgDet.OrganizationLogoPath = vm.OrganizationLogoPath;
            orgDet.OrganizationSize = vm.OrganizationSize;
            orgDet.CountryID = vm.CountryID;
            orgDet.CityID = vm.CityID;
            orgDet.ZipPostalCode = vm.ZipPostalCode;
            orgDet.Telephone = vm.Telephone;
            orgDet.OrganizationWebsite = vm.OrganizationWebsite;
            orgDet.OrganizationProfilePath = vm.OrganizationProfilePath;
            orgDet.EconomicActivity = vm.EconomicActivity;
            orgDet.IndustryType = vm.IndustryType;
            orgDet.YearsofExperienceID = vm.YearsofExperienceID;
            orgDet.EstablishmentDate = vm.EstablishmentDate;
            orgDet.RegistrationNumberWithITC = vm.RegistrationNumberWithITC;
            orgDet.Is_Approved = vm.Is_Approved;
            orgDet.IsDiscalaimerApproved = vm.IsDiscalaimerApproved;
        }
        
        #endregion
    }
}