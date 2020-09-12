using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Organization;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;

namespace LMIS.Bll.Managers
{
    public class OrganizationManager : IOrganizationManager
    {
        private static readonly IOrganizationRepository Repo = DalFactory.Singleton.Organization;

        public ModelResponse Post(ref OrganizationVM vm, string userId)
        {
            Repo.Post(ref vm, userId);
            return new ModelResponse(0, userId);
        }

        public ModelResponse Update(ref OrganizationVM vm, string userId)
        {
            Repo.Update(ref vm, userId);
            return new ModelResponse(0, userId);
        }

        public OrganizationVM GetOrganizationDetails(long portalUserId, Language languageId)
        {
            return Repo.GetOrganizationDetails(portalUserId, languageId);
        }

        public ModelResponse GetProfile(UserInfo user, long portalUserId, int langId = 1)
        {
            OrganizationProfileVM ret;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                if (portalUserId < 1) portalUserId = (long)user.PortalUserId;
                if (user.PortalUserId != portalUserId)
                    if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);

                ret = Repo.GetProfile(portalUserId, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ret);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long orgId, bool approved, string reason)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                Repo.Approve(user.UserId, reqKey, orgId, approved, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
    }
}