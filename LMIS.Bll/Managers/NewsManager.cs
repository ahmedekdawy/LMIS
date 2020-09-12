using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
    public class NewsManager : INewsManager
    {
        private readonly INewsRepository NewsRepository = DalFactory.Singleton.News;
        public List<NewsVm> GetNews(string NewsType, int LanguageId)
        {
            return NewsRepository.GetNews(NewsType, LanguageId);
        }

        public List<NewsVm> GetNewsByID(decimal newsID, int LanguageId)
        {
            return NewsRepository.GetNewsByID(newsID, LanguageId); 
        }

        public int Insert(NewsVm News)
        {
            return NewsRepository.Insert(News);
        }

        public int Update(NewsVm News)
        {
            return NewsRepository.Update(News);
        }

        public int Delete(decimal newsId, string deleteReason)
        {
            return NewsRepository.Delete(newsId, deleteReason);
        }
    }
}
