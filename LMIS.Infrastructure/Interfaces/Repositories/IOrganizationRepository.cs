using LMIS.Infrastructure.Data.Entities.Organization;
using LMIS.Infrastructure.Enums;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IOrganizationRepository
    {
        void Post(ref OrganizationVM vm, string userId);
        void Update(ref OrganizationVM vm, string userId);
        OrganizationVM GetOrganizationDetails(long portalUserId, Language languageId);
        OrganizationProfileVM GetProfile(long portalUserId, int langId = 1);
        void Approve(string adminId, long reqKey, long orgId, bool approved, string reason);
    }
}