using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IOrgManager
    {
        ModelResponse SignUp(ref OrgVm vm, string password, string fileFolder);
        ModelResponse LoadProfile(UserInfo user, long portalUserId);
        ModelResponse UpdateProfile(UserInfo user, ref OrgVm vm, string fileFolder);
        ModelResponse Approve(UserInfo user, long reqKey, long orgId, bool approved, string reason, Dictionary<string, object> newValues);
        ModelResponse UpdateOrgContact(UserInfo user, ref OrgContactVm vm);
        ModelResponse ListOrgContacts(UserInfo user);
        ModelResponse GetOrgContact(UserInfo user, long contactId);
        ModelResponse DeleteOrgContact(UserInfo user, long contactId);
    }
}