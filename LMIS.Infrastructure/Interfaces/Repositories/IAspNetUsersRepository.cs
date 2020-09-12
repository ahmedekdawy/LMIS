

using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IAspNetUsersRepository
    {
        UserInfo GetUserInfo(string email);
       List<UserInfo> GetUsersAdmin();
    }
}
