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
    public class OpportunityManager : IOpportunityManager
    {
        private static readonly IOpportunityRepository Repo = DalFactory.Singleton.Opportunity;

        public ModelResponse List(bool? informal = null)
        {
            List<OpportunityVm> ds;

            try
            {
                ds = Repo.List(informal);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse ListByOrgContact(UserInfo user)
        {
            List<OpportunityVm> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                ds = DalFactory.Singleton.DataService.IsAdmin(user.UserId) ? Repo.ListInternal()
                    : Repo.ListByOrgContact((long) user.OrgContactId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse Post(UserInfo user, ref OpportunityVm vm, string fileFolder, bool validateOnly)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                var isAdmin = DalFactory.Singleton.DataService.IsAdmin(user.UserId);
                if (isAdmin && vm.OpportunityId > 0 && !Repo.IsInternal(vm.OpportunityId)) return new ModelResponse(101);
                if (!isAdmin && vm.OpportunityId > 0 && !Repo.IsByOrgContact(vm.OpportunityId, (long)user.OrgContactId)) return new ModelResponse(101);

                //Validations
                if (vm.Title.IsNullOrWhiteSpace()) return new ModelResponse(1);
                if ((vm.OpportunityId < 1 && vm.StartDate < DateTime.Today) || vm.EndDate < vm.StartDate) return new ModelResponse(2);
                if (Repo.IsDuplicate(ref vm)) return new ModelResponse(3);

                if (!validateOnly)
                {
                    //Verify File Path
                    if (string.IsNullOrWhiteSpace(fileFolder)) return new ModelResponse(102);
                    if (string.IsNullOrWhiteSpace(vm.FilePath)) return new ModelResponse(102);
                    if (!File.Exists(Path.Combine(fileFolder, vm.FilePath))) return new ModelResponse(102);

                    //Save to DB
                    vm.Approval = isAdmin ? Approval.Approved : Approval.Pending;
                    if (!isAdmin) vm.IsInformal = false;
                    vm.IsInternal = isAdmin;
                    vm.ContactId = (long)user.OrgContactId;
                    Repo.Post(ref vm, user.UserId);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.OpportunityId);
        }

        public ModelResponse Delete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                var isAdmin = DalFactory.Singleton.DataService.IsAdmin(user.UserId);
                if (isAdmin && !Repo.IsInternal(id)) return new ModelResponse(101);
                if (!isAdmin && !Repo.IsByOrgContact(id, (long)user.OrgContactId)) return new ModelResponse(101);

                Repo.Delete(id, user.UserId, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse Get(UserInfo user, long id)
        {
            OpportunityVm r;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId))
                {
                    if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);
                    if (!Repo.IsByOrgContact(id, (long)user.OrgContactId)) return new ModelResponse(101);
                }

                r = Repo.Get(id);
                if (r == null) return new ModelResponse(101);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, r);
        }

        public ModelResponse Get(long id)
        {
            OpportunityVm r;

            try
            {
                r = Repo.Get(id);
                if (r == null) return new ModelResponse(101);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, r);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long oppId, bool approved, string reason)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                Repo.Approve(user.UserId, reqKey, oppId, approved, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
    }
}