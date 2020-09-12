using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
   public  interface IDimThemesRepository
    {
        List<ThemeVm> GetAllThemese(int languageID);
        List<ThemeVm> GetAThemesesByType(string themeType,int languageID);
        List<ThemeVm> GetAThemesesByName(string name);
        ThemeVm GetAThemesesByCode(string themeType, int codeNo);
        int Save(ThemeVm them);
        int Delete(int codeNo);
    }
}
