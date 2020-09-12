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
    public class UnionRepository : IUnionRepository
    {
        public void Delete(long id, string userId, string reason)
        {
            using (var db = new LMISEntities())
            {
                var tr = db.Unions
                    .Where(r => r.ID == id)
                    .ToList().Single();

                tr.IsDeleted = true;
                tr.DeleteUserID = userId;
                tr.DeleteReason = reason.Limit(500);
                tr.DeleteDate = DateTime.UtcNow;

                db.SaveChanges();
            }
        }

        public List<UnionVm> List(int langId = 1)
        {
            List<Union> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Unions
                    .Include(r => r.UnionDetails)
                    .Include(r => r.UnionProfessions)
                    .Include(r => r.UnionCommittees.Select(d => d.UnionCommitteeDetails))
                    .Where(r => r.IsDeleted == null)
                    .ToList();
            }

            return ds.Select(r => new UnionVm()
            {
                UnionId = (long)r.ID,
                Name = r.UnionDetails.Select(d => new LocalString(d.LangID, d.Name)).ToList(),
                Address = r.UnionDetails.Select(d => new LocalString(d.LangID, d.Address)).ToList(),
                Professions = r.UnionProfessions.GroupBy(d => d.ProfID, d => new LocalString(d.LangID, d.Name), (k, g) => new GlobalString(g.ToList())).ToList(),
                Committees = r.UnionCommittees.Select(d => new UnionVm.UnionCommittee
                {
                    Gov = new CodeSet { id = d.Gov },
                    Name = d.UnionCommitteeDetails.Select(dd => new LocalString(dd.LangID, dd.Name)).ToList()
                }).ToList(),
                Telephone = r.Telephone,
                Fax = r.Fax,
                Email = r.Email,
                Website = r.Website,
                Logo = r.Logo
            })
            .ToList();

        }

        public long Post(ref UnionVm vm, string userId)
        {
            using (var db = new LMISEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var id = vm.UnionId;

                    if (id > 0)     //Update
                    {
                        var tr = db.Unions
                            .Where(r => r.IsDeleted == null && r.ID == id)
                            .ToList().Single();

                        tr.Telephone = vm.Telephone;
                        tr.Fax = vm.Fax;
                        tr.Website = vm.Website;
                        tr.Email = vm.Email;
                        tr.Logo = vm.Logo;
                        tr.UpdateUserID = userId;
                        tr.UpdateDate = DateTime.UtcNow;

                        //Delete detail records
                        var dr1 = db.UnionDetails
                            .Where(r => r.UnionID == id)
                            .ToList();

                        db.UnionDetails.RemoveRange(dr1);

                        var dr2 = db.UnionProfessions
                            .Where(r => r.UnionID == id)
                            .ToList();

                        db.UnionProfessions.RemoveRange(dr2);

                        var dr3 = db.UnionCommitteeDetails
                            .Where(r => r.UnionID == id)
                            .ToList();

                        db.UnionCommitteeDetails.RemoveRange(dr3);

                        var dr4 = db.UnionCommittees
                            .Where(r => r.UnionID == id)
                            .ToList();

                        db.UnionCommittees.RemoveRange(dr4);
                    }
                    else            //Insert
                    {
                        var tr = new Union
                        {
                            Telephone = vm.Telephone,
                            Fax = vm.Fax,
                            Website = vm.Website,
                            Email = vm.Email,
                            Logo = vm.Logo,
                            PostUserID = userId,
                            PostDate = DateTime.UtcNow
                        };

                        db.Unions.Add(tr);
                        db.SaveChanges();

                        vm.UnionId = (long)tr.ID;
                    }

                    //Insert detail records
                    var ds1 = Utils.MultilingualDataSet(
                        new Dictionary<string, GlobalString>
                        {
                            {"c1", vm.Name},
                            {"c2", vm.Address}
                        });

                    foreach (var r in ds1)
                        db.UnionDetails.Add(new UnionDetail()
                        {
                            UnionID = vm.UnionId,
                            LangID = r["c1"].L,
                            Name = r["c1"].T,
                            Address = r["c2"].T
                        });

                    var sid = 0;

                    foreach(var p in vm.Professions)
                    {
                        sid++;

                        var ds2 = Utils.MultilingualDataSet(
                            new Dictionary<string, GlobalString>
                            {
                                {"c1", p}
                            });

                        foreach (var r in ds2)
                            db.UnionProfessions.Add(new UnionProfession()
                            {
                                UnionID = vm.UnionId,
                                ProfID = sid,
                                LangID = r["c1"].L,
                                Name = r["c1"].T
                            });
                    }

                    sid = 0;

                    foreach (var c in vm.Committees)
                    {
                        sid++;

                        var dr = db.UnionCommittees.Add(new UnionCommittee()
                        {
                            UnionID = vm.UnionId,
                            ComID = sid,
                            Gov = c.Gov.id
                        });

                        var ds3 = Utils.MultilingualDataSet(
                            new Dictionary<string, GlobalString>
                            {
                                {"c1", c.Name}
                            });

                        foreach (var r in ds3)
                            dr.UnionCommitteeDetails.Add(new UnionCommitteeDetail()
                            {
                                UnionID = dr.UnionID,
                                ComID = dr.ComID,
                                LangID = r["c1"].L,
                                Name = r["c1"].T
                            });
                    }

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }

            return vm.UnionId;
        }
    }
}