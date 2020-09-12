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
    public class ConceptsDefinitionsRepository : IConceptsDefinitionsRepository
    {


        public ConceptsDefinitionsVM Post(ConceptsDefinitionsVM vm, string userId)
        {

            using (var db = new LMISEntities())

            {
                try
                {
                    var id = vm.ConceptDefID;
                    var checkExist = db.ConceptDefs.Count(c => c.ConceptDefTitle == vm.ConceptDefTitle && c.ConceptDefID != vm.ConceptDefID && !c.IsDeleted);
                    if (checkExist > 0)
                    { return null; }

                    if (id > 0) //Update
                    {
                        var tr = db.ConceptDefs
                            .Where(r => !r.IsDeleted  && r.ConceptDefID == id)
                            .ToList().Single();

                        tr.ConceptDefDesc = vm.ConceptDefDesc;
                        tr.ConceptDefTitle = vm.ConceptDefTitle;
                       tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;
                        tr.LanguageID = vm.LanguageID;

                    }
                    else //Insert
                    {
                        var tr = new ConceptDef()
                        {
                            ConceptDefDesc = vm.ConceptDefDesc,
                            ConceptDefTitle = vm.ConceptDefTitle,
                            PostUserId= userId,
                            PostDate = DateTime.UtcNow,
                            LanguageID=vm.LanguageID

                        };

                        db.ConceptDefs.Add(tr);
                        db.SaveChanges();

                        vm.ConceptDefID = tr.ConceptDefID;
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




        public int Delete(String UserId, long id)
        {
            int result;
            using (var db = new LMISEntities())
            {
                var tr = db.ConceptDefs.Single(r => r.ConceptDefID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = UserId;
                tr.DeleteDate = DateTime.UtcNow;

               result= db.SaveChanges();
            }
            return result;
        }

        public List<ConceptsDefinitionsVM> Get(int languageId,decimal? ConceptDefID)
        {
            List<ConceptsDefinitionsVM> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.ConceptDefs.Where(w => !w.IsDeleted  && w.ConceptDefID == (ConceptDefID > 0 ? ConceptDefID : w.ConceptDefID) && w.LanguageID == (languageId > 0 ? languageId : w.LanguageID))
                        .Select( s=>new ConceptsDefinitionsVM
                        {
                            ConceptDefID=s.ConceptDefID,
                            ConceptDefDesc=s.ConceptDefDesc,
                            ConceptDefTitle = s.ConceptDefTitle,
                            LanguageID = s.LanguageID

                        }).ToList();
            }

            return result;
        }
    }
}
