using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Interfaces;

using LMIS.Dal.Entity;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Repositories;


namespace LMIS.Dal.Repositories
{
    public class AspNetUsersRepository : IAspNetUsersRepository
    {
       private LMISEntities Context=new LMISEntities();
    

        public UserInfo GetUserInfo(string email)
        {
            var userinfo = (from us in Context.AspNetUsers
                            where us.UserName == email 
                            
                            select new UserInfo
                            {
                                UserId = us.Id,
                                UserName = us.UserName,
                                Roles=us.AspNetRoles.Select(o=> o.Id ).ToList()
                            }).ToList().SingleOrDefault();
            return userinfo;
       
        }
        public List<UserInfo> GetUsersAdmin( )
        {
            var indvidualusers = (from f in Context.IndividualDetails select f.UserID).ToList().Distinct();
            var organizationContactInfousers = (from f in Context.OrganizationContact_Info.Where(w=>w.PortalUsersID>1) select f.UserID).ToList().Distinct();
            var userinfo = (from us in Context.AspNetUsers
                            where !indvidualusers.Contains(us.Id) && !organizationContactInfousers.Contains(us.Id)
                            select new UserInfo
                            {
                                UserId = us.Id,
                                UserName = us.UserName,
                            });
            return userinfo.ToList();

        }
    }
}
