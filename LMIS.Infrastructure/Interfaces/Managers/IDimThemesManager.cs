using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;


namespace LMIS.Infrastructure.Interfaces.Managers
{
  public   interface IDimThemesManager
    {
      ModelResponse GetAllThemese(UserInfo user, int languageID);
        ModelResponse GetAThemesesByType(UserInfo user, string themeType, int languageID);
        List<ThemeVm> GetAThemesesByName(string name);
        ThemeVm GetAThemesesByCode(string themeType, int codeNo);
        List<SubCodeVm> GetAllThemeType(int languageID);
        ModelResponse Save(UserInfo user, ThemeVm them, int languageID);
        ModelResponse Delete(UserInfo user, int codeNo, string themeType, int languageID);

    }
}
