using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Individual;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;

namespace LMIS.Bll.Managers
{
    public class IndividualManager : IIndividualManager
    {
        private static readonly IIndividualRepository Repo = DalFactory.Singleton.Individual;

        public ModelResponse Post(ref IndividualVM vm, string userId)
        {
            Repo.Post(ref vm, userId);
            return new ModelResponse(0, userId);
        }

        public ModelResponse UpdatePersonalInfo(IndividualVM vm)
        {
            var result = Repo.UpdatePersonalInfo(vm);
            return new ModelResponse(0,result);
        }

        public ModelResponse GetProfile(UserInfo user, long portalUserId, int langId = 1)
        {
            IndividualProfileVM ret;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (portalUserId < 1) portalUserId = (long)user.PortalUserId;
                if (user.PortalUserId != portalUserId &&!user.IsEmployer)
                    if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);

                ret = Repo.GetProfile(portalUserId, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ret);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long indId, bool approved, string reason, Dictionary<string, object> newValues)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                Repo.Approve(user.UserId, reqKey, indId, approved, reason, newValues);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public decimal JobSeekers(bool unemployed)
        {
          return   Repo.JobSeekers( unemployed);

        }
    }
}