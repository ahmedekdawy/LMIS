using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IPortalUsersRepository
    {
        int SubscribeNewsLetter(decimal portalUsersID);
        UserClass GetUserClass(UserInfo user);
        UserCat GetUserCat(long portalUserId);
        List<UserInfo> SubscribeNewsLetterUsers();
    }
}