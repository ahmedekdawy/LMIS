using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class ThemesVariablesRepository : IThemesVariablesRepository
    {
        private LMISEntities Context = new LMISEntities();
        public List<Infrastructure.Data.Entities.ThemesVariablesVM> GetThemesesVariable(int themeId, int languageID)
        {
            var result = (from themesVariables in Context.ThemesVariables
                          join gen in Context.GeneralCodes on themesVariables.VariableID equals gen.GeneralID
                          where themesVariables.ThemeID == themeId && gen.ParentGeneralcodeID == "1" && gen.LanguageID== languageID

                          select
                     new ThemesVariablesVM
                     {
                         ThemeVariableID = themesVariables.ThemeVariableID,
                         ThemeID = themesVariables.ThemeID,
                         VariableID = themesVariables.VariableID,
                         VariableName = gen.Name
                     }
                    );
            return result.ToList();
          
        }



        public int DeleteThemesesVariable(int themeId, string variableId)
        {
            int affectedRows = 0;
           
            var themVariable = (from themVar in Context.ThemesVariables
                                where themVar.ThemeID == themeId && themVar.VariableID == variableId
                                select themVar).FirstOrDefault();

            if (themVariable != null)
            {

                //Delete it from memory
                Context.ThemesVariables.Remove(themVariable);
                //Save to database
                affectedRows = Context.SaveChanges();
            }



            return affectedRows;
        }

        public int AddThemesesVariable(int themeId, string variableId)
        {
            int affectedRows = 0;

        var themVariable=new ThemesVariable(){ ThemeID=themeId,VariableID=variableId};
           

                //Delete it from memory
                Context.ThemesVariables.Add(themVariable);
                //Save to database
                affectedRows = Context.SaveChanges();
            



            return affectedRows;
        }
        public List<Infrastructure.Data.Entities.ThemesVariablesVM> GetVariables(string runningVariable, string changingVariable, int themeId,int languageID)
        {
            var result = (from themesVariables in Context.ThemesVariables
                          join gen in Context.GeneralCodes on themesVariables.VariableID equals gen.GeneralID
                          join varMapp in Context.VariablesMappings on gen.GeneralID equals varMapp.VariableID
                          where themesVariables.ThemeID == themeId 
                             && gen.ParentGeneralcodeID == "1" 
                             && gen.GeneralID != runningVariable
                             && gen.GeneralID != changingVariable
                             && gen.LanguageID == languageID

                          select
                     new ThemesVariablesVM
                     {
                         ThemeVariableID = themesVariables.ThemeVariableID,
                         ThemeID = themesVariables.ThemeID,
                         VariableID = themesVariables.VariableID,
                         VariableName = gen.Name,
                         VariableColumnName = varMapp.VariableColumnName,
                         GeneralID = gen.GeneralID,
                         GeneralName = gen.Name
                     }
                    );
            return result.ToList();

        }
    }
}
