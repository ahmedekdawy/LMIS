using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class PagesActionsRepository : IPagesActionsRepository
    {
        private LMISEntities Context = new LMISEntities();

        public int checkPermission(int actionid, int pageId, List<string> roleId)
        {
            var count = Context.PagesActions
                .Where(o => o.Actionid == actionid && o.PageId == pageId && roleId.Contains(o.RoleId))
                .Count();
            return count;
        }
        public int checkPageActionRole(int actionid, int pageId, string roleId)
        {
            var count = Context.PagesActions
                .Where(o => o.Actionid == actionid && o.PageId == pageId && o.RoleId == roleId)
                .Count();
            return count;
        }
        public List<PagesActionsVM> GetPagesActions(int pageId, string roleId)
        {
            var result = (from pagAction in Context.PagesActions
                          where pagAction.PageId == pageId && pagAction.RoleId == roleId
                          select new PagesActionsVM()
                          {
                              id = pagAction.id,
                              Actionid = pagAction.Actionid,
                              PageId = pagAction.PageId,
                              RoleId = pagAction.RoleId
                          });
            return result.ToList();
        }
        public int Insert(int pageId,int actionId, string roleId)
        {
            int affectedRows = 0;
            Context.PagesActions.Add(new PagesAction { PageId = pageId, Actionid = actionId, RoleId = roleId });

              //Save to database
            affectedRows = Context.SaveChanges();
            return affectedRows;
        }
        public int Delete(int pageId, int actionId, string roleId)
        {
            int affectedRows = 0;
            var pAction = (from pagAction in Context.PagesActions
                           where pagAction.PageId == pageId && pagAction.Actionid == actionId && pagAction.RoleId== roleId
                           select pagAction).FirstOrDefault();

            if (pAction != null)
            {

                //Delete it from memory
                Context.PagesActions.Remove(pAction);
                //Save to database
                affectedRows = Context.SaveChanges();
            }



            return affectedRows;
        }
    }
}
