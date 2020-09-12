using LMIS.Dal.Entity;
using LMIS.Dal.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Helpers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace LMIS.Dal.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        public void Delete(long id, string userId, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.Offices
                    .Where(r => r.ID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteReason = reason.Limit(500);
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }

        public OfficeVm Get(long id)
        {
            List<Office> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Offices
                    .Include(r => r.OfficeDetails)
                    .Where(r => r.IsDeleted == null && r.ID == id)
                    .ToList();
            }

            return ds.Select(r => new OfficeVm()
            {
                OfficeId = (long)r.ID,
                Title = r.OfficeDetails.Select(d => new LocalString(d.LangID, d.Title)).ToList(),
                Address = r.OfficeDetails.Select(d => new LocalString(d.LangID, d.Address)).ToList(),
                District = r.OfficeDetails.Select(d => new LocalString(d.LangID, d.District)).ToList(),
                Telephone = r.Telephone,
                Mobile = r.Mobile,
                Fax = r.Fax,
                Hotline = r.Hotline
            })
            .SingleOrDefault();
        }

        public List<OfficeVm> List()
        {
            List<Office> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Offices
                    .Include(r => r.OfficeDetails)
                    .Where(r => r.IsDeleted == null)
                    .ToList();
            }

            return ds.Select(r => new OfficeVm()
            {
                OfficeId = (long)r.ID,
                Title = r.OfficeDetails.Select(d => new LocalString(d.LangID, d.Title)).ToList(),
                Address = r.OfficeDetails.Select(d => new LocalString(d.LangID, d.Address)).ToList(),
                District = r.OfficeDetails.Select(d => new LocalString(d.LangID, d.District)).ToList(),
                Telephone = r.Telephone,
                Mobile = r.Mobile,
                Fax = r.Fax,
                Hotline = r.Hotline
            })
            .ToList();
        }

        public long Post(ref OfficeVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.OfficeId;

                    if (id > 0)     //Update
                    {
                        var tr = db.Offices
                            .Where(r => r.IsDeleted == null && r.ID == id)
                            .ToList().Single();

                        tr.Telephone = vm.Telephone;
                        tr.Mobile = vm.Mobile;
                        tr.Fax = vm.Fax;
                        tr.Hotline = vm.Hotline;
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;

                        //Delete detail records
                        var dr = db.OfficeDetails
                            .Where(r => r.OfficeID == id)
                            .ToList();

                        db.OfficeDetails.RemoveRange(dr);
                    }
                    else            //Insert
                    {
                        var tr = new Office
                        {
                            Telephone = vm.Telephone,
                            Mobile = vm.Mobile,
                            Fax = vm.Fax,
                            Hotline = vm.Hotline,
                            PostUserID = userId,
                            PostDate = DateTime.UtcNow
                        };

                        db.Offices.Add(tr);
                        db.SaveChanges();

                        vm.OfficeId = (long)tr.ID;
                    }

                    //Insert detail records
                    var ds = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.Title},
                            {"c2", vm.Address},
                            {"c3", vm.District}
                        });

                    foreach (var r in ds)
                        db.OfficeDetails.Add(new OfficeDetail()
                        {
                            OfficeID = vm.OfficeId,
                            LangID = r["c1"].L,
                            Title = r["c1"].T,
                            Address = r["c2"].T,
                            District = r["c3"].T
                        });

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.OfficeId;
        }
    }
}