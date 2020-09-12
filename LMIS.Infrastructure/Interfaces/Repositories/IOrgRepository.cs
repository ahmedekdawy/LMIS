using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IOrgRepository
    {
        long SignUp(ref OrgVm vm);
        OrgVm LoadProfile(long portalUserId);
        void UpdateProfile(ref OrgVm vm, string userId);
        void Approve(string adminId, long reqKey, long orgId, bool approved, string reason, Dictionary<string, object> newValues);
        long UpdateOrgContact(ref OrgContactVm vm, string userId);
        List<OrgContactVm> ListOrgContacts(long portalUserId);
        OrgContactVm GetOrgContact(long contactId, long portalUserId = 0);
        bool DeleteOrgContact(long contactId, long portalUserId, string userId);
    }
}