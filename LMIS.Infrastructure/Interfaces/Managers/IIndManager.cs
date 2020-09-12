using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IIndManager
    {
        ModelResponse Search(UserInfo user, long jobOfferId, int langId = 1);
    }
}