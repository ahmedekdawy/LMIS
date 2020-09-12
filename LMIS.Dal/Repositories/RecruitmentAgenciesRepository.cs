using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class RecruitmentAgenciesRepository : IRecruitmentAgenciesRepository
    {


        public RecruitmentAgenciesVM Post(RecruitmentAgenciesVM vm, string userId)
        {

            using (var db = new LMISEntities())

            {
                try
                {
                    var id = vm.RecruitmentAgencieID;
                    var checkExist = db.RecruitmentAgencies.Count(c => c.Name == vm.Name && c.RecruitmentAgencieID != vm.RecruitmentAgencieID && c.IsDeleted == null);
                    if (checkExist > 0)
                    { return null; }

                    if (id > 0) //Update
                    {
                        var tr = db.RecruitmentAgencies
                            .Where(r => r.IsDeleted == null && r.RecruitmentAgencieID == id)
                            .ToList().Single();

                        tr.Background = vm.Background;
                        tr.Name = vm.Name;
                        tr.LogoPath =string.IsNullOrEmpty(vm.LogoPath)?tr.LogoPath: vm.LogoPath;
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;
                        tr.LanguageID = vm.LanguageID;
                      

                    }
                    else //Insert
                    {
                     
                        var tr = new RecruitmentAgency()
                        {
                    
                            Background = vm.Background,
                            Name = vm.Name,
                            LogoPath = vm.LogoPath,
                            PostUserID = userId,
                            PostDate = DateTime.UtcNow,
                            LanguageID=vm.LanguageID

                        };

                        db.RecruitmentAgencies.Add(tr);
                        db.SaveChanges();

                        vm.RecruitmentAgencieID = tr.RecruitmentAgencieID;
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
                var tr = db.RecruitmentAgencies.Single(r => r.RecruitmentAgencieID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = UserId;
                tr.DeleteDate = DateTime.UtcNow;

               result= db.SaveChanges();
            }
            return result;
        }

        public List<RecruitmentAgenciesVM> Get(int languageId,decimal? ID)
        {
            List<RecruitmentAgenciesVM> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.RecruitmentAgencies.Where(w => w.IsDeleted == null  && w.LanguageID == (languageId > 0 ? languageId : w.LanguageID))
                        .Select( s=>new RecruitmentAgenciesVM
                        {
                            RecruitmentAgencieID = s.RecruitmentAgencieID,
                            Background=s.Background,
                            Name = s.Name,
                            LogoPath = s.LogoPath,
                            LanguageID = s.LanguageID

                        }).ToList();
            }

            return result;
        }


    
    }
}
