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
    public class ConceptNonFormalTrainingRepository : IConceptNonFormalTrainingRepository
    {


        public ConceptNonFormalTrainingVM Post(ConceptNonFormalTrainingVM vm, string userId)
        {

            using (var db = new LMISEntities())

            {
                try
                {
                    var id = vm.ConceptID;
                    var checkExist = db.ConceptOfNonFormalTrainings.Count(c => c.ConceptTitle == vm.ConceptTitle && c.ConceptID != vm.ConceptID && c.IsDeleted == null);
                    if (checkExist > 0)
                    { return null; }

                    if (id > 0) //Update
                    {
                        var tr = db.ConceptOfNonFormalTrainings
                            .Where(r => r.IsDeleted == null && r.ConceptID == id)
                            .ToList().Single();

                        tr.ConceptDescription = vm.ConceptDescription;
                        tr.ConceptTitle = vm.ConceptTitle;
                        tr.ImagePath = vm.ImagePath;
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;
                        tr.LanguageID = vm.LanguageID;

                    }
                    else //Insert
                    {
                        var tr = new ConceptOfNonFormalTraining()
                        {
                            ConceptDescription = vm.ConceptDescription,
                            ConceptTitle = vm.ConceptTitle,
                            ImagePath = vm.ImagePath,
                            PostUserID = userId,
                            PostDate = DateTime.UtcNow,
                            LanguageID=vm.LanguageID

                        };

                        db.ConceptOfNonFormalTrainings.Add(tr);
                        db.SaveChanges();

                        vm.ConceptID = tr.ConceptID;
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
                var tr = db.ConceptOfNonFormalTrainings.Single(r => r.ConceptID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = UserId;
                tr.DeleteDate = DateTime.UtcNow;

               result= db.SaveChanges();
            }
            return result;
        }

        public List<ConceptNonFormalTrainingVM> Get(int languageId,decimal? conceptId)
        {
            List<ConceptNonFormalTrainingVM> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.ConceptOfNonFormalTrainings.Where(w => w.IsDeleted == null && w.ConceptID == (conceptId > 0 ? conceptId : w.ConceptID) && w.LanguageID == (languageId > 0 ? languageId : w.LanguageID))
                        .Select( s=>new ConceptNonFormalTrainingVM
                        {
                            ConceptID=s.ConceptID,
                            ConceptDescription=s.ConceptDescription,
                            ConceptTitle = s.ConceptTitle,
                            ImagePath = s.ImagePath,
                            LanguageID = s.LanguageID

                        }).ToList();
            }

            return result;
        }
    }
}
