using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LMIS.Bll.Managers
{
    public class UnionManager : IUnionManager
    {
        private static readonly IUnionRepository Repo = DalFactory.Singleton.Union;

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

        public ModelResponse List()
        {
            List<UnionVm> ds;

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

        public ModelResponse Post(UserInfo user, ref UnionVm vm, string fileFolder, bool validateOnly)
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
                if (vm.Professions == null) vm.Professions = new List<GlobalString>();
                if (vm.Committees == null) vm.Committees = new List<UnionVm.UnionCommittee>();
                if (vm.Professions.Count < 1 || vm.Committees.Count < 1 || vm.Name.IsNullOrWhiteSpace() || vm.Address.IsNullOrWhiteSpace() || vm.Professions.Any(p => p.IsNullOrWhiteSpace())
                    || vm.Committees.Any(c => c.Gov.id.IsNotASubCode() || c.Name.IsNullOrWhiteSpace())) return new ModelResponse(1);

                if (!validateOnly)
                {
                    //Verify File Path
                    if (fileFolder.HasNoValue()) return new ModelResponse(102);
                    if (!vm.Logo.HasNoValue() && !File.Exists(Path.Combine(fileFolder, vm.Logo))) return new ModelResponse(102);

                    //Save to DB
                    Repo.Post(ref vm, user.UserId);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.UnionId);
        }
    }
}