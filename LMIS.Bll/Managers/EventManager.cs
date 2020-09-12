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
    public class EventManager : IEventManager
    {
        private static readonly IEventRepository Repo = DalFactory.Singleton.Event;

        public ModelResponse ListByOrgContact(UserInfo user)
        {
            List<EventVm> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                ds = DalFactory.Singleton.DataService.IsAdmin(user.UserId) ? Repo.ListInternal()
                    : Repo.ListByOrgContact((long)user.OrgContactId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse List(int langId)
        {
            List<CalendarVM> ds;

            try
            {
                ds = Repo.List( langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse Post(UserInfo user, ref EventVm vm, string fileFolder, bool validateOnly)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                var isAdmin = DalFactory.Singleton.DataService.IsAdmin(user.UserId);
                if (isAdmin && vm.EventId > 0 && !Repo.IsInternal(vm.EventId)) return new ModelResponse(101);
                if (!isAdmin && vm.EventId > 0 && !Repo.IsByOrgContact(vm.EventId, (long)user.OrgContactId)) return new ModelResponse(101);

                //Validations
                if (vm.Title.IsNullOrWhiteSpace()
                    || vm.Address.IsNullOrWhiteSpace()
                    || string.IsNullOrWhiteSpace(vm.Type)
                    || vm.Price < 0
                    || vm.ContactAddress.IsNullOrWhiteSpace()
                    || string.IsNullOrWhiteSpace(vm.ContactTelephone)
                    || string.IsNullOrWhiteSpace(vm.ContactWebsite)) return new ModelResponse(1);
                if ((vm.EventId < 1 && vm.StartDate < DateTime.Today) || vm.EndDate < vm.StartDate) return new ModelResponse(2);
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

            return new ModelResponse(0, vm.EventId);
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
            EventVm r;

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

        public ModelResponse Get(long id, int langId)
        {
            EventVm r;

            try
            {
                r = Repo.Get(id, langId);
                if (r == null) return new ModelResponse(101);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, r);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long eventId, bool approved, string reason)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                Repo.Approve(user.UserId, reqKey, eventId, approved, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
    }
}