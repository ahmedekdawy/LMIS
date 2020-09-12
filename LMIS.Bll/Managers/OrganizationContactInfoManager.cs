using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
  public   class OrganizationContactInfoManager : IOrganizationContactInfoManager
    {
        private readonly IOrganizationContactInfoRepository _OrganizationContactInfoRepository = new OrganizationContactInfoRepository();


        public Infrastructure.Data.DTOs.UserInfo GetOrgContactInfo(string UserId)
        {

            var _userInfo = _OrganizationContactInfoRepository.GetOrgContactInfo(UserId);


            return _userInfo;

        }


        public int Insert(Infrastructure.Data.DTOs.UserInfo contact)
        {
            var _userInfo = _OrganizationContactInfoRepository.Insert(contact);


            return _userInfo;
        }

      public int Delete(string UserId)
      {
          
       return    _OrganizationContactInfoRepository.Delete( UserId);
      }
    }
}
