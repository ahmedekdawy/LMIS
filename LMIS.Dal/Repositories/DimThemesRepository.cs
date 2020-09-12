using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
     
   public  class DimThemesRepository : IDimThemesRepository
    {
      private LMISEntities Context=new LMISEntities();
      public List<ThemeVm> GetAllThemese(int languageID)
      {
          var result = (from theme in Context.DimThemes
              join subCode in Context.SubCodes on theme.ThemeType equals subCode.SubID
             
              where subCode.GeneralID == "001" && subCode.LanguageID == languageID
              select new ThemeVm
              {
                  CodeNo = theme.CodeNo,
                  Name = theme.Name,
                  NameAr = theme.NameAr,
                  NameFr = theme.NameFr,
                  ThemeType = theme.ThemeType,
                  UnitScale = theme.UnitScale,
                  UnitScaleAr = theme.UnitScaleAr,
                  UnitScaleFr = theme.UnitScaleFr,
                  ThemeTypeName = subCode.Name


              });
          return result.ToList();
        }

        public List<ThemeVm> GetAThemesesByType(string themeType, int languageID)
        {
            var result=   (from theme in Context.DimThemes
                    from subCode in Context.SubCodes
                           where theme.ThemeType == subCode.SubID && subCode.GeneralID == "001" && theme.ThemeType == themeType && subCode.LanguageID == languageID
                    orderby theme.Name 
                    select new ThemeVm
                    {
                        CodeNo = theme.CodeNo,
                        Name = theme.Name,
                        NameAr = theme.NameAr,
                        NameFr = theme.NameFr,
                        ThemeType = theme.ThemeType,
                        UnitScale = theme.UnitScale,
                        UnitScaleAr = theme.UnitScaleAr,
                        UnitScaleFr = theme.UnitScaleFr,
                        ThemeTypeName = subCode.Name


                    });
            return result.ToList();
        }

        public List<ThemeVm> GetAThemesesByName(string name)
        {
            return (from theme in Context.DimThemes
                    from subCode in Context.SubCodes
                    where theme.ThemeType == subCode.SubID && subCode.GeneralID == "001" && theme.Name.Contains(name)

                    select new ThemeVm
                    {
                        CodeNo = theme.CodeNo,
                        Name = theme.Name,
                        ThemeType = theme.ThemeType,
                        UnitScale = theme.UnitScale,
                        UnitScaleAr = theme.UnitScaleAr,
                        UnitScaleFr = theme.UnitScaleFr,
                        ThemeTypeName = theme.ThemeType


                    }).ToList();
        }
        public ThemeVm GetAThemesesByCode(string themeType, int codeNo)
        {
            return (from theme in Context.DimThemes
                    where theme.ThemeType == themeType && theme.CodeNo == codeNo

                    select new ThemeVm
                    {
                        CodeNo = theme.CodeNo,
                        Name = theme.Name,
                        NameAr = theme.NameAr,
                        NameFr = theme.NameFr,
                        ThemeType = theme.ThemeType,
                        UnitScale = theme.UnitScale,
                        UnitScaleAr = theme.UnitScaleAr,
                        UnitScaleFr = theme.UnitScaleFr



                    }).FirstOrDefault();
        }

        public int Save(ThemeVm theme)

        {

            theme.Name = !string.IsNullOrEmpty(theme.Name)
                ? theme.Name
                : string.IsNullOrEmpty(theme.NameAr) ? theme.NameAr : theme.NameFr;
            theme.NameAr = !string.IsNullOrEmpty(theme.NameAr)
                ? theme.NameAr
                : !string.IsNullOrEmpty(theme.Name) ? theme.Name : theme.NameFr;
            theme.NameFr = !string.IsNullOrEmpty(theme.NameFr)
                ? theme.NameFr
                : !string.IsNullOrEmpty(theme.Name) ? theme.Name : theme.NameAr;
            theme.ThemeType = theme.ThemeType;
            theme.UnitScale = !string.IsNullOrEmpty(theme.UnitScale)
                ? theme.UnitScale
                : !string.IsNullOrEmpty(theme.UnitScaleAr) ? theme.UnitScaleAr : theme.UnitScaleFr;
            theme.UnitScaleAr = !string.IsNullOrEmpty(theme.UnitScaleAr)
                ? theme.UnitScaleAr
                : !string.IsNullOrEmpty(theme.UnitScale) ? theme.UnitScale : theme.UnitScaleFr;
            theme.UnitScaleFr = !string.IsNullOrEmpty(theme.UnitScaleFr)
                ? theme.UnitScaleFr
                : !string.IsNullOrEmpty(theme.UnitScale) ? theme.UnitScale : theme.UnitScaleAr;


            DimTheme them = new DimTheme
            {
                CodeNo = theme.CodeNo,
                Name = theme.Name,
                NameAr = theme.NameAr,
                NameFr = theme.NameFr,
                ThemeType = theme.ThemeType,
                UnitScale = theme.UnitScale,
                UnitScaleAr = theme.UnitScaleAr,
                UnitScaleFr = theme.UnitScaleFr
               
                
            };
            var _them = (from th in Context.DimThemes where th.Name == theme.Name && th.NameAr == theme.NameAr && th.NameFr == theme.NameFr && (th.CodeNo != theme.CodeNo || theme.CodeNo==0) select th).FirstOrDefault();

            if (_them != null){return -4;}
            if ( theme.CodeNo>0)
            {
                _them = (from th in Context.DimThemes where  th.CodeNo == theme.CodeNo select th).FirstOrDefault();
                _them.Name = theme.Name;
                _them.NameAr = theme.NameAr;
                _them.NameFr = theme.NameFr;
                _them.UnitScale = theme.UnitScale;
                _them.UnitScaleAr = theme.UnitScaleAr;
                _them.UnitScaleFr = theme.UnitScaleFr;
                _them.ThemeType = theme.ThemeType;
                Context.DimThemes.Attach(_them);
                Context.Entry(_them).State = EntityState.Modified;
            }
            else
            {Context.DimThemes.Add(them);}
            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }


        public int Delete(int codeNo)
       {
           int affectedRows = 0;
           var them = (from theme in Context.DimThemes
               where theme.CodeNo == codeNo
               select theme).FirstOrDefault();
           var themVariable = (from themVar in Context.ThemesVariables
               where themVar.ThemeID == codeNo
               select themVar).Count();
           if (themVariable > 0)
           {
               return -2;
           }
           if (them != null)
           {
          
           //Delete it from memory
           Context.DimThemes.Remove(them);
           //Save to database
            affectedRows = Context.SaveChanges(); 
           }
       
    

              return affectedRows;
        }
    }
}
