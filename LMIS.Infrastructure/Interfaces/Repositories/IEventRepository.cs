using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IEventRepository
    {
        List<EventVm> ListInternal();
        List<EventVm> ListByOrgContact(long contactId);
        List<CalendarVM> List(int langId);
        long Post(ref EventVm vm, string userId);
        void Delete(long id, string userId, string reason);
        bool IsDuplicate(ref EventVm vm);
        EventVm Get(long id, int langId = 1);
        bool IsInternal(long id);
        bool IsByOrgContact(long id, long contactId);
        void Approve(string adminId, long reqKey, long eventId, bool approved, string reason);
    }
}