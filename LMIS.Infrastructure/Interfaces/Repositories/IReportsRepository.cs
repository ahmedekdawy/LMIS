using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IReportsRepository
    {
        List<ReportVM> GetReports();
        List<Infrastructure.Data.Entities.ReportVM> GetReports(string  themeType);
        ReportVM GetReportsByID(int reportId, int languageId);
        List<ReportVM> GetReports(int themeId, string themeTypeId, int languageId);
        List<SubCodeVm> GetReportsRunningValues(int reportId, int languageId);
        List<SubCodeVm> GetReportsChangingValues(int reportId, int languageId);
        ThemeVm GetTheme(int reportId);
         ReportVM GetReport(int reportId);
         int Save(ReportVM report);
         int Delete(int reportId);
    }
}
