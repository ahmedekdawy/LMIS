using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IPagesActionsRepository
    {
        int checkPermission(int Actionid, int PageId, List<string> RoleId);
        int checkPageActionRole(int actionid, int pageId, string roleId);
        List<PagesActionsVM> GetPagesActions(int pageId, string roleId);
        int Insert(int pageId, int actionId, string roleId);
        int Delete(int pageId, int actionId, string roleId);
    }
}
