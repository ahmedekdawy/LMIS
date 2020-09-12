using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace LMIS.Dal.Repositories
{
    public class NewsRepository : INewsRepository
    {

        public List<Infrastructure.Data.Entities.NewsVm> GetNews(string NewsType, int LanguageId)
        {

            using (var db = new LMISEntities())
            {
                var result = (from _news in db.News
                    where
                        (_news.IsDeleted == false || _news.IsDeleted == null) && _news.NewsLangauage == LanguageId &&
                        EntityFunctions.TruncateTime(_news.NewsExpiryDate) >= EntityFunctions.TruncateTime(DateTime.Now )
                        &&
                        (NewsType == "IsAchievement"
                            ? _news.IsAchievement == true
                            : NewsType == "IsInformal" ? _news.IsInformal == true : 1 == 1)


                    select new NewsVm
                    {
                        NewsID = _news.NewsID,
                        NewsTitle = _news.NewsTitle,
                        NewsDescription = _news.NewsDescription,
                        NewsDate = _news.NewsDate,
                        NewsExpiryDate = _news.NewsExpiryDate,
                        NewsBannerPath = _news.NewsBannerPath,
                        NewsIconPath = _news.NewsIconPath,
                        NewsVideoPath = _news.NewsVideoPath,
                        NewsLangauage = _news.NewsLangauage,
                        PostUserID = _news.PostUserID,
                        PostDate = _news.PostDate,
                        IsInformal = _news.IsInformal,
                        IsAchievement = _news.IsAchievement

                    });
                return result.ToList();

            }

        }

        public List<Infrastructure.Data.Entities.NewsVm> GetNewsByID(decimal newsID, int LanguageId)
        {
            using (var db = new LMISEntities())
            {
                var result = (from _news in db.News
                    where
                        (_news.IsDeleted == false || _news.IsDeleted == null) && _news.NewsLangauage == LanguageId &&
                         EntityFunctions.TruncateTime(_news.NewsExpiryDate) >= EntityFunctions.TruncateTime(DateTime.Now)
                        && _news.NewsID == newsID


                    select new NewsVm
                    {
                        NewsID = _news.NewsID,
                        NewsTitle = _news.NewsTitle,
                        NewsDescription = _news.NewsDescription,
                        NewsDate = _news.NewsDate,
                        NewsExpiryDate = _news.NewsExpiryDate,
                        NewsBannerPath = _news.NewsBannerPath,
                        NewsIconPath = _news.NewsIconPath,
                        NewsVideoPath = _news.NewsVideoPath,
                        NewsLangauage = _news.NewsLangauage,
                        PostUserID = _news.PostUserID,
                        PostDate = _news.PostDate,
                        IsInformal = _news.IsInformal,
                        IsAchievement = _news.IsAchievement

                    });
                return result.ToList();

            }
        }

        public int Insert(Infrastructure.Data.Entities.NewsVm news)
        {
            try
            {


                News _news = new News
                {
                    NewsTitle = news.NewsTitle,
                    NewsDescription = news.NewsDescription,
                    NewsDate = news.NewsDate,
                    NewsExpiryDate = news.NewsExpiryDate,
                    NewsBannerPath = news.NewsBannerPath,
                    NewsIconPath = news.NewsIconPath,
                    NewsVideoPath = news.NewsVideoPath,
                    NewsLangauage = news.NewsLangauage,
                    PostUserID = news.PostUserID,
                    PostDate = DateTime.Now,
                    IsInformal = news.IsInformal,
                    IsAchievement = news.IsAchievement


                };
                int affectedRows = 0;
                using (var db = new LMISEntities())
                {
                    if (news.NewsID < 1)
                    {
                        db.News.Add(_news);
                    }
                    else
                    {
                     var    _News = (from n in db.News where n.NewsID == news.NewsID select n).FirstOrDefault();
                     _News.NewsTitle = news.NewsTitle;
                     _News.NewsDescription = news.NewsDescription;
                     _News.NewsDate = news.NewsDate;
                     _News.NewsExpiryDate = news.NewsExpiryDate;
                     _News.IsInformal = news.IsInformal;
                     _News.IsAchievement = news.IsAchievement;
                     if (!string.IsNullOrEmpty(news.NewsBannerPath)) { _News.NewsBannerPath = news.NewsBannerPath; }
                     if (!string.IsNullOrEmpty(news.NewsIconPath)) { _News.NewsIconPath = news.NewsIconPath; }
                     if (!string.IsNullOrEmpty(news.NewsVideoPath)) { _News.NewsVideoPath = news.NewsVideoPath; }
                     db.News.Attach(_News);
                     db.Entry(_News).State = EntityState.Modified;
                        
                    }
                   
                   db.SaveChanges();

                    affectedRows = int.Parse(_news.NewsID.ToString());
                }
                return affectedRows;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (DbUnexpectedValidationException  ex)
            {
                Console.WriteLine(ex.StackTrace+":"+ ex.InnerException );
                throw;
            }
        }

        public int Update(Infrastructure.Data.Entities.NewsVm News)
        {
            throw new NotImplementedException();
        }

        public int Delete(decimal newsId, string deleteReason)
        {
            int affectedRows = 0;
            using (var db = new LMISEntities())
            {

                var _news = (from news in db.News where news.NewsID == newsId select news).FirstOrDefault();

                if (_news !=null )
                {
                    _news.IsDeleted = true;
                    _news.DeleteReason = deleteReason;
                    _news.DeleteDate = DateTime.Now;
                    affectedRows = db.SaveChanges();
                }
                return affectedRows;

            }
        }
    }
}
