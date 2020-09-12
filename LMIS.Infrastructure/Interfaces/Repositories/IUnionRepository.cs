using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IUnionRepository
    {
        List<UnionVm> List(int langId = 1);
        long Post(ref UnionVm vm, string userId);
        void Delete(long id, string userId, string reason);
    }
}