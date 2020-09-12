using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{

    public class AspNetUsersManager : IAspNetUsersManager
    {
        private readonly IAspNetUsersRepository _aspnet_UsersRepository = new AspNetUsersRepository();

        private readonly IindividualDetailsRepository _individualDetailsRepository = new IndividualDetailsRepository();

        private readonly IOrganizationContactInfoRepository _organizationContactInfoRepository =
            new OrganizationContactInfoRepository();

        private readonly IAspNetUserRolesRepository _AspNetUserRolesRepository = new AspNetUserRolesRepository();

        public Infrastructure.Data.DTOs.UserInfo GetUserInfo(string email)
        {
            var info = _aspnet_UsersRepository.GetUserInfo(email);
            var userid = info.UserId;
            var UserName = info.UserName;
            var userInfo = _individualDetailsRepository.GetUserInfo(userid);
            if (userInfo == null)
            {
                userInfo = _organizationContactInfoRepository.GetOrgContactInfo(userid);
                if (userInfo == null && info != null)
                {
                    return info;
                }
                else
                {
                    userInfo.UserId = userid;
                    userInfo.IsIndividual = false;
                }

            }
            else
            {
                userInfo.IsIndividual = true;
            }
            var roles = _AspNetUserRolesRepository.GetUserRoles(userid);
            userInfo.Roles = roles;
            userInfo.UserName = UserName;

            return userInfo;

        }

        public List<UserInfo> GetUsersAdmin()
        {
         return    _aspnet_UsersRepository.GetUsersAdmin();
        }


    }
}
