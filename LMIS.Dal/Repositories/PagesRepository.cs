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
    public class PagesRepository : IPagesRepository
    {
        private LMISEntities Context = new LMISEntities();
        public List<PagesVM> GetPages()
        {
            var result = (from pag in Context.Pages select new PagesVM
            {
                id = pag.id,
                Page=pag.Page1,
                Path = pag.Path,
                PageCode=pag.PageCode 
            });
            return result.ToList();
        }
      
    }
}
