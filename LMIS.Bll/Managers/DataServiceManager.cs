using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System.Collections.Generic;

namespace LMIS.Bll.Managers
{
    public class DataServiceManager : IDataServiceManager
    {
        private static readonly IDataServiceRepository Repo = DalFactory.Singleton.DataService;

        public List<object> ListIndustriesHavingSkills(int langId = 1)
        {
            return Repo.ListIndustriesHavingSkills(langId);
        }

        public List<object> GroupSkillsForIndustry(string industryId, int langId = 1)
        {
            return Repo.GroupSkillsForIndustry(industryId, langId);
        }

        public Dictionary<string, List<CodeSet>> FillSkillsByIndustryAndLevel(List<string> filters, int langId = 1)
        {
            return Repo.FillSkillsByIndustryAndLevel(filters, langId);
        }

        public List<object> ListObsceneWords()
        {
            return Repo.ListObsceneWords();
        }

        public List<Dictionary<string, object>> ListAdminViews(string adminId = "")
        {
            return Repo.ListAdminViews(adminId);
        }

        public List<object> ListAdmins(bool availableOnly = false, bool excludeSuper = false)
        {
            return Repo.ListAdmins(availableOnly, excludeSuper);
        }

        public List<object> ListSuperAdmins(bool availableOnly = false)
        {
            return Repo.ListSuperAdmins(availableOnly);
        }

        public bool IsAdmin(string userId, bool availableOnly = false)
        {
            return Repo.IsAdmin(userId, availableOnly);
        }

        public bool IsSuperAdmin(string userId, bool availableOnly = false)
        {
            return Repo.IsSuperAdmin(userId, availableOnly);
        }
    }
}