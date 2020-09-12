using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IOpportunityManager
    {
        ModelResponse List(bool? informal = null);
        ModelResponse ListByOrgContact(UserInfo user);
        ModelResponse Post(UserInfo user, ref OpportunityVm vm, string fileFolder, bool validateOnly);
        ModelResponse Delete(UserInfo user, long id, string reason);
        ModelResponse Get(UserInfo user, long id);
        ModelResponse Get(long id);
        ModelResponse Approve(UserInfo user, long reqKey, long oppId, bool approved, string reason);
    }
}