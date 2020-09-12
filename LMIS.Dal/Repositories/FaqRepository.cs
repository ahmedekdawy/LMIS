using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class FaqRepository : IFaqRepository
    {
        public Infrastructure.Data.Entities.FaqVm Post(Infrastructure.Data.Entities.FaqVm vm, string Userid)
        {
            using (var db = new LMISEntities())
            {
                try
                {
                    var id = vm.FAQID;
                    var checkExist = db.FAQs.Count(c => c.Question == vm.Question && c.FAQLanguage == vm.FAQLanguage && c.FAQID != vm.FAQID && !c.IsDeleted );
                    if (checkExist > 0)
                    {return null;}
                    if (id > 0) //Update
                    {
                        var tr = db.FAQs
                            .Where(r =>  r.FAQID == id)
                            .ToList().Single();
                        tr.FAQCategoryID = vm.FAQCategoryID;
                        tr.Question = vm.Question;
                        tr.Answer = vm.Answer;
                        tr.FAQLanguage = vm.FAQLanguage;
                        tr.UpdateUserID = Userid;
                        tr.UpdateDate = DateTime.UtcNow;
                    }
                    else //Insert
                    {
                        var tr = new FAQ() 
                        {
                            Question = vm.Question,
                            Answer = vm.Answer,
                            FAQLanguage = vm.FAQLanguage,
                            FAQCategoryID = vm.FAQCategoryID,
                            PostUserID = Userid,
                            PostDate = DateTime.UtcNow

                        };

                        db.FAQs.Add(tr);
                        db.SaveChanges();

                        vm.FAQID = tr.FAQID;
                    }

                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }


                return vm;
            }
        }

        public int Delete(string userId, long id)
        {
            int result;
            using (var db = new LMISEntities())
            {
                var tr = db.FAQs.Single(r => r.FAQID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteDate = DateTime.UtcNow;

                result = db.SaveChanges();
            }
            return result;
        }

        public List<Infrastructure.Data.Entities.FaqVm> Get( int languageId,decimal? Id)
        {
            List<FaqVm> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.FAQs.Where(w => !w.IsDeleted && w.FAQID == (Id > 0 ? Id : w.FAQID) && w.FAQLanguage == (languageId > 0 ? languageId : w.FAQLanguage))
                  
                        .Select(s => new FaqVm
                        {
                            FAQID = s.FAQID,
                            FAQCategoryID = s.FAQCategoryID,
                            Question = s.Question,
                            Answer = s.Answer,
                            FAQLanguage = s.FAQLanguage,
                            GroupName = SqlUdf.SubCodeName(s.FAQCategoryID, languageId)

                        }).ToList();
            }

            return result.OrderBy(o=>o.GroupName).ToList();
        }
    }
}
