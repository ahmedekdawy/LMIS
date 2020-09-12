using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IIndRepository
    {
        List<Dictionary<string, object>> Search(long jobOfferId, int langId = 1);
    }
}