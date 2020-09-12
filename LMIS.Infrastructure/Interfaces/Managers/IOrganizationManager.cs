using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Organization;
using LMIS.Infrastructure.Enums;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IOrganizationManager
    {
        ModelResponse Post(ref OrganizationVM vm, string userId);
        ModelResponse Update(ref OrganizationVM vm, string userId);
        OrganizationVM GetOrganizationDetails(long portalUserId, Language language);
        ModelResponse GetProfile(UserInfo user, long portalUserId, int langId = 1);
        ModelResponse Approve(UserInfo user, long reqKey, long orgId, bool approved, string reason);
    }
}