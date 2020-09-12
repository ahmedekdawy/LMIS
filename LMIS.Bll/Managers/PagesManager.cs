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
    public class PagesManager : IPagesManager
    {
        private readonly IPagesRepository _repo = new PagesRepository();
        public List<PagesVM> GetPages()
        {
            return _repo.GetPages();
        }
    }

    public class CopyOfPagesManager : IPagesManager
    {
        private readonly IPagesRepository _repo = new PagesRepository();
        public List<PagesVM> GetPages()
        {
            return _repo.GetPages();
        }
    }
}
