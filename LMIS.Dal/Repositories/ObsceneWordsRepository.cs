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
    public class ObsceneWordsRepository : IObsceneWordsRepository
    {
        public Infrastructure.Data.Entities.ObsceneWordsVm Post(Infrastructure.Data.Entities.ObsceneWordsVm vm, string Userid)
        {
            using (var db = new LMISEntities())
            {
                try
                {
                    var id = vm.ObsceneWordID;
                    var checkExist = db.ObsceneWords.Count(c => c.Description == vm.Description && c.ObsceneWordID != vm.ObsceneWordID && c.IsDeleted == null);
                    if (checkExist > 0)
                    {return null;}
                    if (id > 0) //Update
                    {
                        var tr = db.ObsceneWords
                            .Where(r => r.IsDeleted == null && r.ObsceneWordID == id)
                            .ToList().Single();

                        tr.Description = vm.Description;

                        tr.UpdateUserID = Userid;
                        tr.UpdateDate = DateTime.UtcNow;
                    }
                    else //Insert
                    {
                        var tr = new ObsceneWord()
                        {
                            Description = vm.Description,
                            PostUserID = Userid,
                            PostDate = DateTime.UtcNow

                        };

                        db.ObsceneWords.Add(tr);
                        db.SaveChanges();

                        vm.ObsceneWordID = tr.ObsceneWordID;
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
                var tr = db.ObsceneWords.Single(r => r.ObsceneWordID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteDate = DateTime.UtcNow;

                result = db.SaveChanges();
            }
            return result;
        }

        public List<Infrastructure.Data.Entities.ObsceneWordsVm> Get( decimal? obsceneWordId)
        {
            List<ObsceneWordsVm> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.ObsceneWords.Where(w => w.IsDeleted == null && w.ObsceneWordID == (obsceneWordId > 0 ? obsceneWordId : w.ObsceneWordID) )
                        .Select(s => new ObsceneWordsVm
                        {
                            ObsceneWordID = s.ObsceneWordID,
                            Description = s.Description

                        }).ToList();
            }

            return result;
        }
    }
}
