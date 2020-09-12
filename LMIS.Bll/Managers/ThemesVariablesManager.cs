using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
    public class ThemesVariablesManager : IThemesVariablesManager
    {
        private readonly IThemesVariablesRepository _repo = new ThemesVariablesRepository();

        public List<ThemesVariablesVM> GetThemesesVariable(int themeId, int languageID)
        {
            return _repo.GetThemesesVariable(themeId, languageID);
        }

        public int DeleteThemesesVariable(int themeId, string variableId)
        {
            return _repo.DeleteThemesesVariable(themeId, variableId);
            
        }
        public int AddThemesesVariable(int themeId, string variableId)
        {
            return _repo.AddThemesesVariable(themeId, variableId);
        }

        public List<Infrastructure.Data.Entities.ThemesVariablesVM> GetVariables(string runningVariable, string changingVariable, int themeId, int languageID)
        {
            return _repo.GetVariables(runningVariable, changingVariable, themeId, languageID);
        }
    }
}
