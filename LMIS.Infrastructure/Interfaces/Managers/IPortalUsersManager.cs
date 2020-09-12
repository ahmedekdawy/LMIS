using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IPortalUsersManager
    {
        ModelResponse SubscribeNewsLetter(UserInfo user);
        UserClass GetUserClass(UserInfo user);
        UserCat GetUserCat(long portalUserId);
        List<UserInfo> SubscribeNewsLetterUsers();
    }
}