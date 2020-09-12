using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
   public  class PagesActionsManager:IPagesActionsManager
    {
       private readonly IPagesActionsRepository _repo = new PagesActionsRepository();
       public int checkPermission(int Actionid, int PageId, List<string> RoleId)
        {
          return _repo.checkPermission(Actionid,  PageId,  RoleId);
        }

       public int checkPageActionRole(int actionid, int pageId, string roleId)
       {
           return _repo.checkPageActionRole(actionid, pageId, roleId);

       }

       public List<PagesActionsVM> GetPagesActions(int pageId, string roleId)
       {
           return _repo.GetPagesActions(pageId, roleId); 
       }

       public int Insert(int pageId, int actionId, string roleId)
       {
           return _repo.Insert( pageId,  actionId,  roleId); 
       }

       public int Delete(int pageId, int actionId, string roleId)
       {
           return _repo.Delete(pageId, actionId, roleId);
           ;
       }
    }
}
