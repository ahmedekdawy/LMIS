using System.Collections.Generic;
using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
    public class PortalUsersManager : IPortalUsersManager
    {
        private readonly IPortalUsersRepository PortalUsersRepository = DalFactory.Singleton.PortalUsers;
        public ModelResponse SubscribeNewsLetter(UserInfo user)
        {
            if (user == null) return new ModelResponse(101);
            var sresult= PortalUsersRepository.SubscribeNewsLetter(decimal.Parse(user.PortalUserId.ToString()));
            return new ModelResponse(0,null);
        }

        public UserClass GetUserClass(UserInfo user)
        {
            if (user == null || user.PortalUserId == 0 || string.IsNullOrWhiteSpace(user.UserId)) return UserClass.Unknown;
            return PortalUsersRepository.GetUserClass(user);
        }

        public UserCat GetUserCat(long portalUserId)
        {
            return PortalUsersRepository.GetUserCat(portalUserId);
        }

        public  List<UserInfo> SubscribeNewsLetterUsers()
        {
            return PortalUsersRepository.SubscribeNewsLetterUsers();
        }
    }
}
