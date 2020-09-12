using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
   public  interface INewsManager
    {
       List<NewsVm> GetNews(string NewsType, int LanguageId);
       List<NewsVm> GetNewsByID(decimal newsID, int LanguageId);
        int Insert(NewsVm News);
        int Update(NewsVm News);
        int Delete(decimal newsId, string deleteReason);
    }
}
