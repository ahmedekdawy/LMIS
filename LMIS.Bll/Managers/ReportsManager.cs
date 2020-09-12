using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;
using LMIS.Utlities.Helpers;

namespace LMIS.Bll.Managers
{
    public class ReportsManager : IReportsManager
    {

        private readonly IReportsRepository _repo = new ReportsRepository();
        private readonly ISubCodeRepository _repoSubCode = new SubCodeRepository();
        public List<Infrastructure.Data.Entities.ReportVM> GetReports()
        {
         return    _repo.GetReports();
        }
        public List<Infrastructure.Data.Entities.ReportVM> GetReports(string  themeType)
        {
            return _repo.GetReports(themeType);
        }
        public List<ReportVM> GetReports(int themeId, string themeTypeId, int languageId)
        {
            return _repo.GetReports(themeId, themeTypeId, languageId);
        }

        public Infrastructure.Data.Entities.ReportVM GetReportsByID(int reportId, int languageId)
        {
            return _repo.GetReportsByID(reportId, languageId);
        }


        public List<Infrastructure.Data.Entities.SubCodeVm> GetReportsRunningValues(int reportId,int languageId)
        {
            var result = _repo.GetReportsByID(reportId, languageId);
            if (result == null)
            {
                return null;
            }
            var runningVariableId = result.RunningVariableID;
            if (runningVariableId == null)
            {
                return new List<Infrastructure.Data.Entities.SubCodeVm> ();
            }
            if (runningVariableId == "004" && !string.IsNullOrEmpty(result.YearTo.Trim()) && result.YearTo != "00400001")
            {
                return _repoSubCode.GetAllSubCode(runningVariableId, languageId, result.YearID,result.YearTo);
            }
            else
            {
                return _repoSubCode.GetAllSubCode(runningVariableId, languageId);
            }
            
        }


        public List<Infrastructure.Data.Entities.SubCodeVm> GetReportsChangingValues(int reportId, int languageId)
        {
            var result = _repo.GetReportsByID(reportId, languageId);
            if (result == null)
            {
                return null;
            }
            var changingVariableId = result.changingVariableID;
            if (changingVariableId == "004" && !string.IsNullOrEmpty(result.YearTo.Trim()) && result.YearTo != "00400001")
            {
                return _repoSubCode.GetAllSubCode(changingVariableId, languageId, result.YearID, result.YearTo);
            }
            else
            {
                return _repoSubCode.GetAllSubCode(changingVariableId, languageId);
            }
           
        }


        public ThemeVm GetTheme(int reportId)
        {
            return _repo.GetTheme(reportId);
        }

        public ReportVM GetReport(int reportId)
        {
            return _repo.GetReport(reportId);
        }

        public int Save(ReportVM report)
        {
            report.ReportArName = new ArabicPrepocessor().StripArabicWords(report.ReportArName);
            report.ReportEnName = new ArabicPrepocessor().StripArabicWords(report.ReportEnName);
            report.ReportFrName = new ArabicPrepocessor().StripArabicWords(report.ReportFrName);
            return _repo.Save(report);
        }

        public int Delete(int reportId)
        {
            return _repo.Delete(reportId);
        }
    }
}
