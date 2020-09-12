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
    public class FactStatisticalDataManager : IFactStatisticalDataManager
    {
        private readonly IFactStatisticalDataRepository _repoSubCode = new FactStatisticalDataRepository();
        public int Insert(ReportVM fixedVariables, int themeId, string changingVariable, string runningVariable, string changingValue, string runningValue, decimal value)
        {
            return _repoSubCode.Insert(fixedVariables, themeId, changingVariable, runningVariable, changingValue, runningValue, value);
        }
    }
}
