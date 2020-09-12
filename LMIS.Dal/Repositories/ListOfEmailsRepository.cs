using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class ListOfEmailsRepository : IListOfEmailsRepository
    {
        public Infrastructure.Data.Entities.ListOfEmailsVm Post(Infrastructure.Data.Entities.ListOfEmailsVm vm, string Userid)
        {
            using (var db = new LMISEntities())
            {
                try
                {
                    var id = vm.EmailID;
                    var checkExist = db.ListOfEmails.Count(c => c.EmailAddress == vm.EmailAddress && c.EmailID != vm.EmailID && c.IsDeleted == null);
                    if (checkExist > 0)
                    {return null;}
                    if (id > 0) //Update
                    {
                        var tr = db.ListOfEmails
                            .Where(r => r.IsDeleted == null && r.EmailID == id)
                            .ToList().Single();

                        tr.Title = vm.Title;
                        tr.EmailAddress = vm.EmailAddress;
                        tr.UpdateUserID = Userid;
                        tr.UpdateDate = DateTime.UtcNow;
                    }
                    else //Insert
                    {
                        var tr = new ListOfEmail()
                        {
                            Title = vm.Title,
                            EmailAddress = vm.EmailAddress,
                            PostUserID = Userid,
                            PostDate = DateTime.UtcNow
                        
                        };

                  

                        db.ListOfEmails.Add(tr);
                        db.SaveChanges();

                        vm.EmailID = tr.EmailID;
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
                var tr = db.ListOfEmails.Single(r => r.EmailID == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteDate = DateTime.UtcNow;

                result = db.SaveChanges();
            }
            return result;
        }

        public List<Infrastructure.Data.Entities.ListOfEmailsVm> Get( decimal? obsceneWordId)
        {
            List<ListOfEmailsVm> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.ListOfEmails.Where(w => w.IsDeleted == null && w.EmailID == (obsceneWordId > 0 ? obsceneWordId : w.EmailID) )
                        .Select(s => new ListOfEmailsVm
                        {
                            EmailID = s.EmailID,
                            Title  = s.Title,
                            EmailAddress = s.EmailAddress

                        }).ToList();
            }

            return result;
        }
    }
}
