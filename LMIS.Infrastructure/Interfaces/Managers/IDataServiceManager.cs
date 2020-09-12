using LMIS.Infrastructure.Data.DTOs;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IDataServiceManager
    {
        List<object> ListIndustriesHavingSkills(int langId = 1);
        List<object> GroupSkillsForIndustry(string industryId, int langId = 1);
        Dictionary<string, List<CodeSet>> FillSkillsByIndustryAndLevel(List<string> filters, int langId = 1);
        List<object> ListObsceneWords();
        List<Dictionary<string, object>> ListAdminViews(string adminId = "");
        List<object> ListAdmins(bool availableOnly = false, bool excludeSuper = false);
        List<object> ListSuperAdmins(bool availableOnly = false);
        bool IsAdmin(string userId, bool availableOnly = false);
        bool IsSuperAdmin(string userId, bool availableOnly = false);
    }
}