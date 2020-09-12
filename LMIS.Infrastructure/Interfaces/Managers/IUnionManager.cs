using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IUnionManager
    {
        ModelResponse List();
        ModelResponse Post(UserInfo user, ref UnionVm vm, string fileFolder, bool validateOnly);
        ModelResponse Delete(UserInfo user, long id, string reason);
    }
}