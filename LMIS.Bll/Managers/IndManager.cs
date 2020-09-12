using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;

namespace LMIS.Bll.Managers
{
    public class IndManager : IIndManager
    {
        private static readonly IIndRepository Repo = DalFactory.Singleton.Ind;

        public ModelResponse Search(UserInfo user, long jobOfferId, int langId = 1)
        {
            List<Dictionary<string, object>> ds;

            try
            {
                //Authorization
                if (user == null || user.PortalUserId < 1) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                ds = Repo.Search(jobOfferId, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);

        }
    }
}