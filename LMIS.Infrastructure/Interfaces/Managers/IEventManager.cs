using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IEventManager
    {
        ModelResponse ListByOrgContact(UserInfo user);
        ModelResponse List(int langId);
        ModelResponse Post(UserInfo user, ref EventVm vm, string fileFolder, bool validateOnly);
        ModelResponse Delete(UserInfo user, long id, string reason);
        ModelResponse Get(UserInfo user, long id);
        ModelResponse Get(long id, int langId);
        ModelResponse Approve(UserInfo user, long reqKey, long eventId, bool approved, string reason);
    }
}