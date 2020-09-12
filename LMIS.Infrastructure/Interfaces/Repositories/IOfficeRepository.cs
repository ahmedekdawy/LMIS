using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IOfficeRepository
    {
        List<OfficeVm> List();
        OfficeVm Get(long id);
        long Post(ref OfficeVm vm, string userId);
        void Delete(long id, string userId, string reason);
    }
}