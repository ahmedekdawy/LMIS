using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Helpers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class FactStatisticalDataRepository : IFactStatisticalDataRepository
    {
        private LMISEntities _context = new LMISEntities();
        public int Insert(ReportVM fixedVariables, int themeId, string changingVariable, string runningVariable, string changingValue, string runningValue, decimal value)
        {
            string changingVariableColumn="";
            string runningVariableColumn = "";
          
            changingVariableColumn = Utils.VariableName(changingVariable.Substring(0,3));
            if (!string.IsNullOrEmpty(runningVariable))
            {
                runningVariableColumn = Utils.VariableName(runningVariable.Substring(0, 3));  
            }
            
          
            FactStatisticalData item = new FactStatisticalData();
            if (fixedVariables != null)
            {
                item.YearID = fixedVariables.YearID;
                foreach (var column in fixedVariables.GetType().GetProperties())
                {
                    if (
                        !"ReportID,ThemeNameAr,ThemeNameFr,UnitScaleAr,UnitScaleFr,ReportEnName,ReportArName,ReportFrName,ThemeID,ThemeName,ThemeType,UnitScale,RunningVariableID,changingVariableID,RunningVariableName,changingVariableName,FixedVariable,YearTo,Source,SourceAr,SourceFr,PublishYear"
                            .Contains(column.Name))
                    {
                        item.GetType().GetProperty(column.Name).SetValue(item, column.GetValue(fixedVariables, null));
                    }
                }

            }



            // item.SectorID = fixedVariables.SectorID;
          //  item.AgeID = fixedVariables.AgeID;
            item.GetType().GetProperty(changingVariableColumn).SetValue(item, changingValue.Substring(0, 8));
            if (!string.IsNullOrEmpty(runningVariable))
            {
                item.GetType().GetProperty(runningVariableColumn).SetValue(item, runningValue.Substring(0, 8));
            }
            item.ThemeID = themeId;
            item.Value = value;
            
            if (_context.FactStatisticalDatas.Where(r =>
                (item.AgeID == null || r.AgeID == item.AgeID)
                && (item.EducationLevelID == null || r.EducationLevelID == item.EducationLevelID)
                && (item.AgeID == null || r.AgeID == item.AgeID)
                && (item.GenderID == null || r.GenderID == item.GenderID)
                && (item.GovID == null || r.GovID == item.GovID)
                && (item.MaritalStatusID == null || r.MaritalStatusID == item.MaritalStatusID)
                && (item.YearID == null || r.YearID == item.YearID)
                && (item.FactDataID == null || r.FactDataID == item.FactDataID)
                && (item.ThemeID == null || r.ThemeID == item.ThemeID)

                ).Any() == false)
            {
                _context.FactStatisticalDatas.Add(item);
            }
            else
            {
                _context.FactStatisticalDatas.Attach(item);
                _context.Entry(item ).State = EntityState.Modified;
            }
            return _context.SaveChanges();
         
        }
    }
}
