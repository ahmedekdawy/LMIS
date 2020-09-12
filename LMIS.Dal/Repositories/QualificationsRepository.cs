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
    public class QualificationsRepository : IQualificationsRepository
    {
        public Infrastructure.Data.Entities.QualificationsVm Post(Infrastructure.Data.Entities.QualificationsVm vm, string Userid)
        {
            using (var db = new LMISEntities())
            {
                try
                {
                    var id = vm.Id;
                    var checkExist = db.Qualifications.Count(c => ((c.QualificationAr == vm.QualificationAr && !string.IsNullOrEmpty(vm.QualificationAr)) || (c.QualificationEn == vm.QualificationEn && !string.IsNullOrEmpty(vm.QualificationEn)) || (c.QualificationFr == vm.QualificationFr) && !string.IsNullOrEmpty(vm.QualificationFr)) && c.Id != vm.Id && c.IsDeleted == null);
                    if (checkExist > 0)
                    {return null;}
                    if (id > 0) //Update
                    {
                        var tr = db.Qualifications
                            .Where(r => r.IsDeleted == null && r.Id == id)
                            .ToList().Single();
                        tr.GroupID = vm.GroupID;
                        tr.QualificationAr = vm.QualificationAr;
                        tr.QualificationEn = vm.QualificationEn;
                        tr.QualificationFr = vm.QualificationFr;
                        tr.UpdateUserID = Userid;
                        tr.UpdateDate = DateTime.UtcNow;
                    }
                    else //Insert
                    {
                        var tr = new Qualification()
                        {
                            QualificationAr = vm.QualificationAr,
                            QualificationEn = vm.QualificationEn,
                            QualificationFr = vm.QualificationFr,
                            GroupID = vm.GroupID,
                            PostUserID = Userid,
                            PostDate = DateTime.UtcNow

                        };

                        db.Qualifications.Add(tr);
                        db.SaveChanges();

                        vm.Id = tr.Id;
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
                var tr = db.Qualifications.Single(r => r.Id == id);

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteDate = DateTime.UtcNow;

                result = db.SaveChanges();
            }
            return result;
        }

        public List<Infrastructure.Data.Entities.QualificationsVm> Get( int languageId,decimal? Id)
        {
            List<QualificationsVm> result;
            using (var db = new LMISEntities())
            {
                result =
                    db.Qualifications.Where(w => w.IsDeleted == null && w.Id == (Id > 0 ? Id : w.Id))
                  
                        .Select(s => new QualificationsVm
                        {
                            Id = s.Id,
                            GroupID=s.GroupID,
                            QualificationAr = s.QualificationAr,
                            QualificationEn = s.QualificationEn,
                            QualificationFr = s.QualificationFr,
                            GroupName = SqlUdf.SubCodeName(s.GroupID, languageId)

                        }).ToList();
            }

            return result.OrderBy(o=>o.GroupName).ToList();
        }
    }
}
