using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;
using LMIS.Utlities;
using LMIS.Utlities.Helpers;

namespace LMIS.Bll.Managers
{
   public  class DimThemesManager : IDimThemesManager
    {
        private readonly IDimThemesRepository _repo = new DimThemesRepository();
        public ModelResponse GetAllThemese(UserInfo user, int languageID)
        {
            //return _repo.GetAllThemese( languageID);
            List<Infrastructure.Data.Entities.ThemeVm> ds;
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
              //  if (!user.IsApproved || user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

                ds = _repo.GetAllThemese( languageID);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse GetAThemesesByType(UserInfo user, string themeType, int languageID)
        {
            List<Infrastructure.Data.Entities.ThemeVm> ds;

            try
            {
                //Authorization
               if (user == null) return new ModelResponse(101);
              //  if (!user.IsApproved || user.IsIndividual || user.OrgContactId != null) return new ModelResponse(101);

                ds = _repo.GetAThemesesByType( themeType,  languageID); 
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public List<Infrastructure.Data.Entities.ThemeVm> GetAThemesesByName(string name)
        {
          name=  new  ArabicPrepocessor().StripArabicWords( name);
          return  _repo.GetAThemesesByName(name);
        }

       public ThemeVm GetAThemesesByCode(string themeType, int codeNo)
       {
           return _repo.GetAThemesesByCode(themeType,  codeNo);
       }
        public ModelResponse Save(UserInfo user, Infrastructure.Data.Entities.ThemeVm them, int languageID)
        {
           int  affectedRows;
           them.Name = new ArabicPrepocessor().StripArabicWords(them.Name);
            List<Infrastructure.Data.Entities.ThemeVm> ds;
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
             //   if (!user.IsApproved || user.IsIndividual || user.OrgContactId != null) return new ModelResponse(101);

                affectedRows = _repo.Save(them);
                ds = _repo.GetAThemesesByType(them.ThemeType, languageID); 
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(affectedRows > 0 ? 0 : affectedRows, ds);
            
        }

        public ModelResponse Delete(UserInfo user, int codeNo, string themeType, int languageID)
        {
           
            int affectedRows;
            List<Infrastructure.Data.Entities.ThemeVm> ds;
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
              //  if (!user.IsApproved || user.IsIndividual || user.OrgContactId != null) return new ModelResponse(101);

                affectedRows = _repo.Delete(codeNo);

                ds = _repo.GetAThemesesByType(themeType, languageID); 
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(affectedRows > 0 ? 0 : affectedRows, ds);
            
        }

        public List<SubCodeVm> GetAllThemeType(int  languageID)
       {
           return  new SubCodeRepository().GetAllSubCode("001", languageID);
           
       }
    }
}
