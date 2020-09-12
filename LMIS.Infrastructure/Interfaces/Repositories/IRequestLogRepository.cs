using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IRequestLogRepository
    {
        List<RequestLogVm> ListPendingByAdminId(string adminId = "", int langId = 1);
        List<RequestLogVm> ListRequestLog();
        bool ReassignAdmin(long id, string newAdminId);
        string AssignedTo(long id);
    }
}