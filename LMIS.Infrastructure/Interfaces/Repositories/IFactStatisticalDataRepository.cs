using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
   public  interface IFactStatisticalDataRepository
    {
       int Insert(ReportVM fixedVariables, int themeId, string changingVariable, string runningVariable, string changingValue, string runningValue, decimal value);
    }
}
