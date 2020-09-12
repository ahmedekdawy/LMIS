using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface INewsRepository
    {
        List<NewsVm> GetNews(string NewsType, int LanguageId);
        List<NewsVm> GetNewsByID(decimal NewsID, int LanguageId);
        int Insert(NewsVm news);
        int Update(NewsVm News);
        int Delete(decimal newsId, string deleteReason);
    }
}
