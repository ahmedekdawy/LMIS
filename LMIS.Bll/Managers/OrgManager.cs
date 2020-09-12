using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.IO;

namespace LMIS.Bll.Managers
{
    public class OrgManager : IOrgManager
    {
        private static readonly IOrgRepository Repo = DalFactory.Singleton.Org;
        private static readonly IPortalUsersRepository PUserRepo = DalFactory.Singleton.PortalUsers;

        private static ModelResponse ValidateOrgProfile(OrgVm vm, string fileFolder)
        {
            //Validations
            if (vm.OrgType.IsNotASubCode()
                || vm.OrgName.IsNullOrWhiteSpace()
                || vm.OrgSize.IsNotASubCode()
                || vm.IDType.IsNotASubCode()
                || vm.ID.HasNoValue()
                || vm.DateEstablished.Year < 1000
                || vm.Activity.IsNotASubCode()
                || vm.ContactInfo == null
                || vm.ContactInfo.Country.IsNotASubCode()
                || vm.ContactInfo.City.IsNotASubCode()
                || vm.ContactInfo.PostalCode.HasNoValue()) return new ModelResponse(1);
            if (vm.OrgType == "10000003" && vm.YOE.IsNotASubCode()) return new ModelResponse(1);
            if (vm.Activity == "03200004" && vm.Industry.IsNotASubCode() && vm.OtherIndustry.IsNullOrWhiteSpace()) return new ModelResponse(1);

            //Verify File Paths
            if (fileFolder.HasNoValue()) return new ModelResponse(102);
            if (!vm.LogoFileName.HasNoValue())
                if (!File.Exists(Path.Combine(fileFolder, vm.LogoFileName))) return new ModelResponse(102);
            if (!vm.ProfileFileName.HasNoValue())
                if (!File.Exists(Path.Combine(fileFolder, vm.ProfileFileName))) return new ModelResponse(102);

            return null;
        }

        public ModelResponse SignUp(ref OrgVm vm, string password, string fileFolder)
        {
            try
            {
                //Validations
                var ret = ValidateOrgProfile(vm, fileFolder);
                if (ret != null) return ret;

                //Verify Auth Letter File Path
                if (!File.Exists(Path.Combine(fileFolder, vm.AuthLetterFileName))) return new ModelResponse(102);

                //Save to DB
                vm.Approval = Approval.Pending;
                Repo.SignUp(ref vm);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.PortalUserId);
        }

        public ModelResponse LoadProfile(UserInfo user, long portalUserId)
        {
            OrgVm r;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (user.PortalUserId != portalUserId)
                {
                    if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                }
                else
                {
                    var userClass = PUserRepo.GetUserClass(user);
                    if (userClass != UserClass.OrgAdmin && userClass != UserClass.OrgContact) return new ModelResponse(101);
                }

                //Load from DB
                r = Repo.LoadProfile(portalUserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, r);
        }

        public ModelResponse UpdateProfile(UserInfo user, ref OrgVm vm, string fileFolder)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (PUserRepo.GetUserClass(user) != UserClass.OrgAdmin) return new ModelResponse(101);

                //Validations
                var ret = ValidateOrgProfile(vm, fileFolder);
                if (ret != null) return ret;

                //Update DB
                vm.PortalUserId = (long)user.PortalUserId;
                vm.Approval = Approval.Pending;
                Repo.UpdateProfile(ref vm, user.UserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.PortalUserId);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long orgId, bool approved, string reason, Dictionary<string, object> newValues)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                Repo.Approve(user.UserId, reqKey, orgId, approved, reason, newValues);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse UpdateOrgContact(UserInfo user, ref OrgContactVm vm)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (PUserRepo.GetUserClass(user) != UserClass.OrgAdmin) return new ModelResponse(101);

                //Validations
                if (vm.UserName.HasNoValue()
                    || vm.FullName.IsNullOrWhiteSpace()
                    || vm.Department.IsNullOrWhiteSpace()
                    || vm.JobTitle.IsNotASubCode()) return new ModelResponse(1);

                //Save to DB
                vm.PortalUserId = (long)user.PortalUserId;
                Repo.UpdateOrgContact(ref vm, user.UserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.ContactId);
        }

        public ModelResponse ListOrgContacts(UserInfo user)
        {
            List<OrgContactVm> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (user.PortalUserId < 1) return new ModelResponse(101);

                //Load from DB
                ds = Repo.ListOrgContacts((long)user.PortalUserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse GetOrgContact(UserInfo user, long contactId)
        {
            OrgContactVm r;
            
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (user.PortalUserId < 1) return new ModelResponse(101);

                //Load from DB
                if (user.OrgContactId == contactId || DalFactory.Singleton.DataService.IsAdmin(user.UserId))
                    r = Repo.GetOrgContact(contactId);
                else
                    r = Repo.GetOrgContact(contactId, (long)user.PortalUserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, r);
        }

        public ModelResponse DeleteOrgContact(UserInfo user, long contactId)
        {
            bool ret;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (PUserRepo.GetUserClass(user) != UserClass.OrgAdmin) return new ModelResponse(101);

                //Delete
                ret = Repo.DeleteOrgContact(contactId, (long)user.PortalUserId, user.UserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ret);
        }
    }
}