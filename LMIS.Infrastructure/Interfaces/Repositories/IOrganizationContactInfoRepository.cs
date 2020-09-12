using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
   public  interface IOrganizationContactInfoRepository
    {
        UserInfo GetOrgContactInfo(string UserId);
       int Insert(UserInfo contact);
       int Delete(string UserId);
    }
}
