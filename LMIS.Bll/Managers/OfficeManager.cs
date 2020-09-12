using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;

namespace LMIS.Bll.Managers
{
    public class OfficeManager : IOfficeManager
    {
        private static readonly IOfficeRepository Repo = DalFactory.Singleton.Office;

        public ModelResponse Delete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);
                
                var isAdmin = DalFactory.Singleton.DataService.IsAdmin(user.UserId);
                if (!isAdmin) return new ModelResponse(101);

                Repo.Delete(id, user.UserId, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse Get(long id)
        {
            OfficeVm r;

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

        public ModelResponse List()
        {
            List<OfficeVm> ds;

            try
            {
                ds = Repo.List();
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse Post(UserInfo user, ref OfficeVm vm)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                var isAdmin = DalFactory.Singleton.DataService.IsAdmin(user.UserId);
                if (!isAdmin) return new ModelResponse(101);

                //Validations
                if (vm.Title.IsNullOrWhiteSpace() || vm.Address.IsNullOrWhiteSpace() || vm.District.IsNullOrWhiteSpace()
                    || string.IsNullOrWhiteSpace(vm.Telephone) || string.IsNullOrWhiteSpace(vm.Hotline)) return new ModelResponse(1);

                //Save to DB
                if (string.IsNullOrWhiteSpace(vm.Mobile)) vm.Mobile = "";
                Repo.Post(ref vm, user.UserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.OfficeId);
        }
    }
}