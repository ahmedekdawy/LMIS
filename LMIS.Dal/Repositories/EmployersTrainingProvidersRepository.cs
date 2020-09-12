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
    public class EmployersTrainingProvidersRepository : IEmployersTrainingProvidersRepository
    {


        public EmployersTrainingProvidersVM Post(EmployersTrainingProvidersVM vm, string userId)
        {

            using (var db = new LMISEntities())

            {
                try
                {
                    var id = vm.ID;
                    var checkExist = db.EmployersTrainingProviders.Count(c => c.Name == vm.Name && c.ID != vm.ID && c.IsDeleted == null);
                    if (checkExist > 0)
                    { return null; }

                    if (id > 0) //Update
                    {
                        var tr = db.EmployersTrainingProviders
                            .Where(r => r.IsDeleted == null && r.ID == id)
                            .ToList().Single();

                        tr.Description = vm.Description;
                        tr.Name = vm.Name;
                        tr.Website = vm.Website;
                        tr.LogoPath =string.IsNullOrEmpty(vm.LogoPath)?tr.LogoPath: vm.LogoPath;
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;
                        tr.LanguageID = vm.LanguageID;
                        tr.Type = (vm.Type != 0);

                    }
                    else //Insert
                    {
                        var tr = new EmployersTrainingProvider()
                        {
                            Description = vm.Description,
                            Name = vm.Name,
                            Website = vm.Website,
                            LogoPath = vm.LogoPath,
                            PostUserID = userId,
                            PostDate = DateTime.UtcNow,
                            LanguageID=vm.LanguageID,
                            Type = (vm.Type != 0)  

                        };

                        db.EmployersTrainingProviders.Add(tr);
                        db.SaveChanges();

                        vm.ID = tr.ID;
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
                var tr = db.EmployersTrainingProviders.Single(r => r.ID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = UserId;
                tr.DeleteDate = DateTime.UtcNow;

               result= db.SaveChanges();
            }
            return result;
        }

        public List<EmployersTrainingProvidersVM> Get(int languageId, int type, decimal? ID)
        {
            List<EmployersTrainingProvidersVM> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.EmployersTrainingProviders.Where(w => w.IsDeleted == null && w.Type == (type > -1 ?(type != 0) : w.Type) && w.ID == (ID > 0 ? ID : w.ID) && w.LanguageID == (languageId > 0 ? languageId : w.LanguageID))
                        .Select( s=>new EmployersTrainingProvidersVM
                        {
                            ID=s.ID,
                            Description=s.Description,
                            Name = s.Name,
                            Website = s.Website,
                            LogoPath = s.LogoPath,
                            LanguageID = s.LanguageID,
                            Type = (s.Type ? 1 : 0) 

                        }).ToList();
            }

            return result;
        }
        public List<EmployersTrainingProvidersVM> GetFrontBack(int languageId, int type, decimal? ID)
        {
            List<EmployersTrainingProvidersVM> result;
            using (var db = new LMISEntities())
            {
                 result = (from w in db.EmployersTrainingProviders
                                where  w.IsDeleted == null && w.Type == (type != 0)  &&
                        w.ID == (ID > 0 ? ID : w.ID) && w.LanguageID ==  languageId
                        select 
                               new EmployersTrainingProvidersVM
                    {
                        Name = w.Name,
                        Website = string.IsNullOrEmpty(w.Website) ? "" : w.Website,
                        LogoPath = w.LogoPath,
                        Description =w.Description
                       
                    }).ToList() ;
          
             var ResultFront =(from pu in db.PortalUsers
                              join org in db.OrganizationDetails on  pu.PortalUsersID equals org.PortalUsersID 
                              join orgDt in db.OrganizationDetails_Det on pu.PortalUsersID equals orgDt.PortalUsersID
                              where (pu.TrainingProvider == (type == 0) || type == 1) && (pu.Employer == (type == 1) || type == 0) && orgDt.LanguageID == languageId
                           
                        select new EmployersTrainingProvidersVM
                        {

                            Name = orgDt.OrganizationName,
                            Website = string.IsNullOrEmpty(org.OrganizationWebsite) ? "" :org.OrganizationWebsite,
                            LogoPath = org.OrganizationLogoPath,
                            Description=""

                        }).ToList();

             result.AddRange(ResultFront);


            }

            return result;
        }
    }
}
