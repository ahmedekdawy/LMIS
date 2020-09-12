using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{

    public class AspNetUserRolesManager : IAspNetUserRolesManager
    {
        private readonly IAspNetUserRolesRepository _aspnet_UsersRepository = new AspNetUserRolesRepository();

        private readonly IindividualDetailsRepository _individualDetailsRepository = new IndividualDetailsRepository();
        public List<string> GetUserRoles(string email)
     {
         //var info = _aspnet_UsersRepository.GetUserInfo(email);
         //var userid = info.UserId;
         //var UserName = info.UserName;
         //var userInfo = _individualDetailsRepository.GetUserInfo(userid);
         //if (userInfo == null)
         //{
         //    userInfo = new UserInfo();

         //    userInfo.UserId = userid;
         //    userInfo.IsIndividual = false;
         //}

         //userInfo.UserName = UserName;

         //return userInfo;
            return null; 

     }
    }
}
