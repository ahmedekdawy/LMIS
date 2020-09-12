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
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class OrgRepository : IOrgRepository
    {
        public long SignUp(ref OrgVm vm)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var userName = vm.UserName;
                    var userId = db.AspNetUsers.Where(u => u.UserName == userName).Select(u => u.Id).Single();
                    var isSelfEmployed = vm.OrgType == "10000003";
                    var isIndustrial = vm.Activity == "03200004";
                    var otherIndustry = vm.OtherIndustry;
                    var telephoneNo = vm.ContactInfo.Telephone;

                    if (string.IsNullOrWhiteSpace(vm.Industry)) vm.Industry = "";
                    else if (vm.Industry.Length != 8) vm.Industry = "";
                    if (!isIndustrial || vm.Industry.Length == 8) otherIndustry = new GlobalString();
                    if (telephoneNo == null) telephoneNo = "";

                    var portalUser = db.PortalUsers.Add(new PortalUser()
                    {
                        IDType = vm.IDType,
                        IDNumber = vm.ID,
                        UserCategory = "ORG",
                        UserSubCategory = vm.OrgType,
                        TrainingSeeker = vm.ReceiveTraining,
                        TrainingProvider = vm.OfferTraining,
                        Employer = vm.OfferJobs,
                        JobSeeker = false,
                        Researcher = true,
                        Internal = false,
                        IsSubscriper = false
                    });

                    db.SaveChanges();
                    var portalUserId = portalUser.PortalUsersID;
                    vm.PortalUserId = (long) portalUserId;

                    var org = db.OrganizationDetails.Add(new OrganizationDetail()
                    {
                        PortalUsersID = portalUserId,
                        OrganizationLogoPath = string.IsNullOrWhiteSpace(vm.LogoFileName) ? "" : vm.LogoFileName,
                        OrganizationProfilePath = vm.ProfileFileName,
                        OrganizationSize = vm.OrgSize,
                        CountryID = vm.ContactInfo.Country,
                        CityID = vm.ContactInfo.City,
                        ZipPostalCode = vm.ContactInfo.PostalCode,
                        Telephone = telephoneNo,
                        OrganizationWebsite = vm.ContactInfo.Website,
                        EconomicActivity = vm.Activity,
                        IndustryType = isIndustrial ? vm.Industry : null,
                        EstablishmentDate = vm.DateEstablished,
                        YearsofExperienceID = isSelfEmployed ? vm.YOE : "",
                        RegistrationNumberWithITC = vm.OfferTraining ? vm.ItcRegNo : null,
                        IsDiscalaimerApproved = true,
                        Is_Approved = (byte) vm.Approval,
                        PostDate = DateTime.UtcNow
                    });

                    var orgDets = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.OrgName},
                            {"c2", vm.ContactInfo.Address},
                            {"c3", otherIndustry}
                        });

                    foreach (var r in orgDets)
                        org.OrganizationDetails_Det.Add(new OrganizationDetails_Det()
                        {
                            LanguageID = r["c1"].L,
                            OrganizationName = r["c1"].T,
                            Address = r["c2"].T,
                            OtherIndustryType = r["c3"].T
                        });

                    org.OrganizationContact_Info.Add(new OrganizationContact_Info()
                    {
                        UserID = userId,
                        JobTitleID = "admin",
                        Telephone = telephoneNo,
                        Mobile = "",
                        Email = vm.UserName,
                        AuthorizationletterPath = vm.AuthLetterFileName,
                        IsApproved = (byte) vm.Approval,
                        PostDate = DateTime.UtcNow,
                        PostUSerID = userId
                    });

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.PortalUserId;
        }

        public OrgVm LoadProfile(long portalUserId)
        {
            OrganizationContact_Info admin;
            List<OrganizationDetail> ds;

            using (var db = new LMISEntities())
            {
                admin = db.OrganizationContact_Info
                    .First(r => r.IsDeleted == null && r.PortalUsersID == portalUserId && r.JobTitleID.Trim().ToLower() == "admin");

                ds = db.OrganizationDetails
                    .Include(r => r.PortalUser)
                    .Include(r => r.OrganizationDetails_Det)
                    .Where(r => r.DeleteUserID == null && r.PortalUsersID == portalUserId)
                    .ToList();
            }

            var ret = ds.Select(r => new OrgVm()
            {
                PortalUserId = portalUserId,
                OrgType = r.PortalUser.UserSubCategory,
                LogoFileName = r.OrganizationLogoPath,
                OrgName = r.OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.OrganizationName)).ToList(),
                ProfileFileName = r.OrganizationProfilePath,
                OrgSize = r.OrganizationSize,
                IDType = r.PortalUser.IDType,
                ID = r.PortalUser.IDNumber,
                DateEstablished = r.EstablishmentDate.AsUtc(),
                YOE = r.YearsofExperienceID,
                Activity = r.EconomicActivity,
                Industry = r.IndustryType,
                OtherIndustry = r.OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.OtherIndustryType)).ToList(),
                ContactInfo = new OrgVm.OrgContactInfo()
                {
                    Country = r.CountryID,
                    City = r.CityID,
                    PostalCode = r.ZipPostalCode,
                    Address = r.OrganizationDetails_Det.Select(d => new LocalString(d.LanguageID, d.Address)).ToList(),
                    Telephone = r.Telephone,
                    Website = r.OrganizationWebsite
                },
                ReceiveTraining = r.PortalUser.TrainingSeeker,
                OfferJobs = r.PortalUser.Employer,
                OfferTraining = r.PortalUser.TrainingProvider,
                ItcRegNo = r.RegistrationNumberWithITC,
                Approval = (Approval)r.Is_Approved,
                RejectReason = r.RejectReason
            })
            .SingleOrDefault();

            if (ret == null) return null;

            ret.UserName = admin.Email;
            ret.AuthLetterFileName = admin.AuthorizationletterPath;

            return ret;
        }

        public void UpdateProfile(ref OrgVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var portalUserId = vm.PortalUserId;
                    var isSelfEmployed = vm.OrgType == "10000003";
                    var isIndustrial = vm.Activity == "03200004";
                    var otherIndustry = vm.OtherIndustry;
                    var telephoneNo = vm.ContactInfo.Telephone;

                    if (string.IsNullOrWhiteSpace(vm.Industry)) vm.Industry = "";
                    else if (vm.Industry.Length != 8) vm.Industry = "";
                    if (!isIndustrial || vm.Industry.Length == 8) otherIndustry = new GlobalString();
                    if (telephoneNo == null) telephoneNo = "";

                    var portalUser = db.PortalUsers.Single(r => r.PortalUsersID == portalUserId);

                    portalUser.IDType = vm.IDType;
                    portalUser.IDNumber = vm.ID;
                    portalUser.UserSubCategory = vm.OrgType;
                    portalUser.TrainingSeeker = vm.ReceiveTraining;
                    portalUser.TrainingProvider = vm.OfferTraining;
                    portalUser.Employer = vm.OfferJobs;

                    var org = db.OrganizationDetails.Single(r => r.PortalUsersID == portalUserId);

                    org.OrganizationLogoPath = string.IsNullOrWhiteSpace(vm.LogoFileName) ? "" : vm.LogoFileName;
                    org.OrganizationProfilePath = vm.ProfileFileName;
                    org.OrganizationSize = vm.OrgSize;
                    org.CountryID = vm.ContactInfo.Country;
                    org.CityID = vm.ContactInfo.City;
                    org.ZipPostalCode = vm.ContactInfo.PostalCode;
                    org.Telephone = telephoneNo;
                    org.OrganizationWebsite = vm.ContactInfo.Website;
                    org.EconomicActivity = vm.Activity;
                    org.IndustryType = isIndustrial ? vm.Industry : null;
                    org.EstablishmentDate = vm.DateEstablished;
                    org.YearsofExperienceID = isSelfEmployed ? vm.YOE : "";
                    org.RegistrationNumberWithITC = vm.OfferTraining ? vm.ItcRegNo : null;
                    org.Is_Approved = (byte)vm.Approval;
                    org.UpdateDate = DateTime.UtcNow;
                    org.UpdateUserID = userId;

                    db.OrganizationDetails_Det.RemoveRange(org.OrganizationDetails_Det);

                    var orgDets = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.OrgName},
                            {"c2", vm.ContactInfo.Address},
                            {"c3", otherIndustry}
                        });

                    foreach (var r in orgDets)
                        org.OrganizationDetails_Det.Add(new OrganizationDetails_Det()
                        {
                            LanguageID = r["c1"].L,
                            OrganizationName = r["c1"].T,
                            Address = r["c2"].T,
                            OtherIndustryType = r["c3"].T
                        });

                    var orgAdmin = org.OrganizationContact_Info.Single( r =>
                        r.PortalUsersID == portalUserId && r.UserID == userId &&
                        r.JobTitleID.Trim().ToLower() == "admin");

                    orgAdmin.Telephone = telephoneNo;
                    orgAdmin.UpdateDate = DateTime.UtcNow;
                    orgAdmin.UpdateUSerID = userId;

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }
        }

        public void Approve(string adminId, long reqKey, long orgId, bool approved, string reason, Dictionary<string, object> newValues)
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
                    {
                        tr.Is_Approved = (byte)Approval.Approved;

                        var orgAdmins = tr.OrganizationContact_Info
                            .Where(r => r.IsDeleted == null && r.JobTitleID.Trim().ToLower() == "admin");

                        foreach (var r in orgAdmins)
                            r.IsApproved = (byte) Approval.Approved;
                    }
                    else
                    {
                        tr.Is_Approved = (byte)Approval.Rejected;
                        tr.RejectReason = reason.Limit(500);
                    }

                    if (newValues.ContainsKey("industry"))
                        if (!string.IsNullOrWhiteSpace((string)newValues["industry"]))
                        {
                            tr.IndustryType = (string)newValues["industry"];

                            var dr = tr.OrganizationDetails_Det
                                .Where(r => r.PortalUsersID == orgId && r.OtherIndustryType != null && r.OtherIndustryType.Trim() != "")
                                .ToList();

                            foreach (var r in dr)
                                r.OtherIndustryType = null;

                            db.OrganizationDetails_Det.RemoveRange(dr.Where(r => string.IsNullOrWhiteSpace(r.OrganizationName) && string.IsNullOrWhiteSpace(r.Address)));
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

        public long UpdateOrgContact(ref OrgContactVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var contactId = vm.ContactId;
                    var contactUserName = vm.UserName;
                    var contactUserId = db.AspNetUsers.Where(u => u.UserName == contactUserName).Select(u => u.Id).Single();

                    if (vm.Mobile == null) vm.Mobile = "";
                    if (vm.Telephone == null) vm.Telephone = "";

                    OrganizationContact_Info orgContact;

                    if (contactId < 1) //Insert
                    {
                        orgContact = db.OrganizationContact_Info.Add(new OrganizationContact_Info()
                        {
                            PortalUsersID = vm.PortalUserId,
                            UserID = contactUserId,
                            JobTitleID = vm.JobTitle,
                            Mobile = vm.Mobile,
                            Telephone = vm.Telephone,
                            Fax = vm.Fax,
                            Email = contactUserName,
                            IsApproved = (byte)Approval.Approved,
                            PostDate = DateTime.UtcNow,
                            PostUSerID = userId
                        });
                    }
                    else //Update
                    {
                        orgContact = db.OrganizationContact_Info.Single(r => r.IsDeleted == null && r.OrganizationContactID == contactId);
                        orgContact.JobTitleID = vm.JobTitle;
                        orgContact.Mobile = vm.Mobile;
                        orgContact.Telephone = vm.Telephone;
                        orgContact.Fax = vm.Fax;
                        orgContact.UpdateDate = DateTime.UtcNow;
                        orgContact.UpdateUSerID = userId;

                        db.OrganizationContactInfoDetails.RemoveRange(orgContact.OrganizationContactInfoDetails);
                    }

                    var orgContactDets = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.FullName},
                            {"c2", vm.Department}
                        });

                    foreach (var r in orgContactDets)
                        orgContact.OrganizationContactInfoDetails.Add(new OrganizationContactInfoDetail()
                        {
                            LanguageID = r["c1"].L,
                            ContactFullName = r["c1"].T,
                            Department = r["c2"].T,
                        });

                    db.SaveChanges();
                    transaction.Commit();

                    vm.ContactId = (long)orgContact.OrganizationContactID;
                }
                catch (DbEntityValidationException ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.ContactId;
        }

        public List<OrgContactVm> ListOrgContacts(long portalUserId)
        {
            List<OrganizationContact_Info> ds;

            using (var db = new LMISEntities())
            {
                ds = db.OrganizationContact_Info
                    .Include(r => r.OrganizationContactInfoDetails)
                    .Where(r => r.IsDeleted == null && r.PortalUsersID == portalUserId && r.JobTitleID.Trim().ToLower() != "admin")
                    .ToList();
            }

            var ret = ds.Select(r => new OrgContactVm()
            {
                PortalUserId = portalUserId,
                ContactId = (long)r.OrganizationContactID,
                UserName = r.Email,
                FullName = r.OrganizationContactInfoDetails.Select(d => new LocalString(d.LanguageID, d.ContactFullName)).ToList(),
                Department = r.OrganizationContactInfoDetails.Select(d => new LocalString(d.LanguageID, d.Department)).ToList(),
                JobTitle = r.JobTitleID,
                Mobile = r.Mobile,
                Telephone = r.Telephone,
                Fax = r.Fax
            })
            .ToList();

            return ret;
        }

        public OrgContactVm GetOrgContact(long contactId, long portalUserId = 0)
        {
            OrganizationContact_Info tr;

            using (var db = new LMISEntities())
            {
                tr = db.OrganizationContact_Info
                    .Include(r => r.OrganizationContactInfoDetails)
                    .SingleOrDefault(r => r.IsDeleted == null && r.OrganizationContactID == contactId && (portalUserId == 0 || r.PortalUsersID == portalUserId));
            }

            if (tr == null) return null;

            return new OrgContactVm()
            {
                PortalUserId = portalUserId,
                ContactId = (long)tr.OrganizationContactID,
                UserName = tr.Email,
                FullName = tr.OrganizationContactInfoDetails.Select(d => new LocalString(d.LanguageID, d.ContactFullName)).ToList(),
                Department = tr.OrganizationContactInfoDetails.Select(d => new LocalString(d.LanguageID, d.Department)).ToList(),
                JobTitle = tr.JobTitleID,
                Mobile = tr.Mobile,
                Telephone = tr.Telephone,
                Fax = tr.Fax
            };
        }

        public bool DeleteOrgContact(long contactId, long portalUserId, string userId)
        {
            using (var db = new LMISEntities())
            {
                var orgContact = db.OrganizationContact_Info.Single(r => r.IsDeleted == null && r.OrganizationContactID == contactId && r.PortalUsersID == portalUserId && r.JobTitleID.Trim().ToLower() != "admin");
                
                orgContact.IsDeleted = true;
                orgContact.DeleteDate = DateTime.UtcNow;
                orgContact.DeleteUserID = userId;

                db.SaveChanges();
            }

            return true;
        }
    }
}