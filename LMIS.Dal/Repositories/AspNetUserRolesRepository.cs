using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Interfaces;

using LMIS.Dal.Entity;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Repositories;


namespace LMIS.Dal.Repositories
{
    public class AspNetUserRolesRepository : IAspNetUserRolesRepository
    {
       private LMISEntities _Context2=new LMISEntities();


       public List<string> GetUserRoles(string userid)
       {

           var userRoles = _Context2.AspNetRoles
               .Where(x => x.AspNetUsers.Any(r => r.Id == userid)).Select(s=>s.Id ).ToList();
     

           return userRoles;

       }
    }
}
