using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Repositories;

using System.Collections.Generic;
using System.Linq;

namespace LMIS.Dal.Repositories
{
    public class DataServiceRepository : IDataServiceRepository
    {
        public List<object> ListIndustriesHavingSkills(int langId = 1)
        {
            List<object> ds;

            using (var db = new LMISEntities())
            {
                ds = db.SubCodes
                    .Where(r => r.GeneralID == "096" && db.Skills.Select(s => s.IndustryId).Distinct().Contains(r.SubID))
                    .Select(a => new
                    {
                        id = a.SubID,
                        desc = SqlUdf.SubCodeName(a.SubID, langId)
                    })
                    .ToList()
                    .Cast<object>().ToList();
            }

            return ds;
        }

        public List<object> GroupSkillsForIndustry(string industryId, int langId = 1)
        {
            List<object> ds;

            using (var db = new LMISEntities())
            {
                ds = db.Skills.Where(a => a.IndustryId == industryId)
                    .Select(a => a.SkillID)
                    .Distinct()
                    .GroupJoin(
                        db.Skills.Where(a => a.IndustryId == industryId),
                        a => a, b => b.SkillID, (k, g) => new
                    {
                        SkillId = k,
                        SkillDesc = SqlUdf.SubCodeName(k, langId),
                        Levels = g.Select(a => new
                        {
                            id = a.SkillLevel_ID,
                            desc = SqlUdf.SubCodeName(a.SkillLevel_ID, langId)
                        }),
                        TypeIsRequired = (SqlUdf.SubCodeParent(k) == "02000003") //For Lingual Skills Only
                    })
                    .ToList()
                    .GroupJoin(
                        db.SubCodes.Where(a => a.GeneralID == "022") //Skill Types
                        .Select(a => new
                        {
                            id = a.SubID,
                            desc = SqlUdf.SubCodeName(a.SubID, langId)
                        })
                        .Distinct(),
                        a => true, b => true, (k, g) => new
                    {
                        id = k.SkillId,
                        desc = k.SkillDesc,
                        Types = k.TypeIsRequired ? g.ToList() : g.ToList().TakeWhile(a => false),
                        k.Levels
                    })
                    .SelectMany(a => a.Types.DefaultIfEmpty(new { id = "", desc = "" }), (p, c) => new
                    {
                        id = industryId + "|" + p.id + "|" + c.id,
                        desc = p.desc + (c.id == "" ? "" : " [" + c.desc + "]"),
                        options = p.Levels.Select(a => new
                        {
                            id = industryId + "|" + p.id + "|" + c.id + "|" + a.id,
                            desc = a.desc + ": " + p.desc + (c.id == "" ? "" : " [" + c.desc + "]"),
                            Skill = new { p.id, p.desc },
                            Type = (c.id == "" ? null : c),
                            Level = a
                        })
                    })
                    .ToList()
                    .Cast<object>().ToList();
            }

            return ds;
        }

        public Dictionary<string, List<CodeSet>> FillSkillsByIndustryAndLevel(List<string> filters, int langId = 1)
        {
            Dictionary<string, List<CodeSet>> ds;
            
            using (var db = new LMISEntities())
            {
                ds = db.Skills.Where(a => filters.Contains(a.IndustryId + "|" + a.SkillLevel_ID))
                    .Select(a => new
                    {
                        Filter = a.IndustryId + "|" + a.SkillLevel_ID,
                        SkillId = a.SkillID,
                        SkillDesc = SqlUdf.SubCodeName(a.SkillID, langId)
                    })
                    .ToList()
                    .GroupBy(a => a.Filter)
                    .ToDictionary(g => g.Key, g => g.Select(a => new CodeSet {id = a.SkillId, desc = a.SkillDesc}).ToList());
            }

            return ds;
        }

        public List<object> ListObsceneWords()
        {
            using (var db = new LMISEntities())
            {
                return db.ObsceneWords
                    .Where(r => r.IsDeleted == null)
                    .Select(r => r.Description)
                    .ToList()
                    .Cast<object>().ToList();
            }
        }

        public List<Dictionary<string, object>> ListAdminViews(string adminId = "")
        {
            using (var db = new LMISEntities())
            {
                return db.Admins
                    .AsNoTracking()
                    .Where(r => r.AdminId == adminId || adminId == "")
                    .ToList()
                    .Select(a => new Dictionary<string, object>
                    {
                        { "Id", a.AdminId },
                        { "Name", a.AdminName },
                        { "Super", a.Super },
                        { "Available", a.Available },
                        { "PendingRequests", a.PendingRequests }
                    })
                    .ToList();
            }
        }

        public List<object> ListAdmins(bool availableOnly = false, bool excludeSuper = false)
        {
            using (var db = new LMISEntities())
            {
                return db.Admins
                    .AsNoTracking()
                    .Where(r => (!availableOnly || r.Available == true) && (!excludeSuper || r.Super != true))
                    .ToList()
                    .Select(a => new
                    {
                        id = a.AdminId,
                        desc = a.AdminName
                    })
                    .Distinct()
                    .ToList()
                    .Cast<object>().ToList();
            }
        }

        public List<object> ListSuperAdmins(bool availableOnly = false)
        {
            using (var db = new LMISEntities())
            {
                return db.Admins
                    .AsNoTracking()
                    .Where(r => (!availableOnly || r.Available == true) && (r.Super == true))
                    .ToList()
                    .Select(a => new
                    {
                        id = a.AdminId,
                        desc = a.AdminName
                    })
                    .Distinct()
                    .ToList()
                    .Cast<object>().ToList();
            }
        }

        public bool IsAdmin(string userId, bool availableOnly = false)
        {
            using (var db = new LMISEntities())
            {
                return db.Admins
                    .AsNoTracking()
                    .Where(r => (r.AdminId == userId) && (!availableOnly || r.Available == true))
                    .ToList()
                    .Count > 0;
            }
        }

        public bool IsSuperAdmin(string userId, bool availableOnly = false)
        {
            using (var db = new LMISEntities())
            {
                return db.Admins
                    .AsNoTracking()
                    .Where(r => (r.AdminId == userId && r.Super == true) && (!availableOnly || r.Available == true))
                    .ToList()
                    .Count > 0;
            }
        }
    }
}