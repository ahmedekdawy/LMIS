using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IOfficeManager
    {
        ModelResponse List();
        ModelResponse Get(long id);
        ModelResponse Post(UserInfo user, ref OfficeVm vm);
        ModelResponse Delete(UserInfo user, long id, string reason);
    }
}