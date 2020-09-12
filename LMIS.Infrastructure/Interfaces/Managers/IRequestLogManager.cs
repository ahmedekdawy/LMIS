using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IRequestLogManager
    {
        ModelResponse ListPendingByAdminId(UserInfo admin, int langId = 1, bool forAllAdmins = false);
        List<RequestLogVm> ListRequestLog();
        ModelResponse ReassignAdmin(UserInfo admin, long requestId, string newAdminId);
    }
}