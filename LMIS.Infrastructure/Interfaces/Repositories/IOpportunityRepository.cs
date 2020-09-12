using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IOpportunityRepository
    {
        List<OpportunityVm> List(bool? informal = null);
        List<OpportunityVm> ListInternal();
        List<OpportunityVm> ListByOrgContact(long contactId);
        long Post(ref OpportunityVm vm, string userId);
        void Delete(long id, string userId, string reason);
        bool IsDuplicate(ref OpportunityVm vm);
        OpportunityVm Get(long id);
        bool IsInternal(long id);
        bool IsByOrgContact(long id, long contactId);
        void Approve(string adminId, long reqKey, long oppId, bool approved, string reason);
    }
}