using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
   public  class LmisReportsManager : ILmisReportsManager
    {
       private static readonly ILmisReportsRepository Repo = DalFactory.Singleton.LmisReports;
        public List<Infrastructure.Data.Entities.TransactionHistoryVm> TransactionHistory(string transactionType, string userName, DateTime postDate, int actionType)
        {

            return Repo.TransactionHistory(transactionType, userName, postDate, actionType);
        }
    }
}
