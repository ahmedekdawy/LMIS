using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IThemesVariablesManager
    {
        List<ThemesVariablesVM> GetThemesesVariable(int themeId, int languageID);
        int DeleteThemesesVariable(int themeId, string  variableId);
        int AddThemesesVariable(int themeId, string variableId);

        List<Infrastructure.Data.Entities.ThemesVariablesVM> GetVariables(string runningVariable, string changingVariable, int themeId, int languageID);
    }
}
