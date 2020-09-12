using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
  public   interface ILmisReportsRepository
    {
      List<TransactionHistoryVm> TransactionHistory(string transactionType, string userName, DateTime postDate, int actionType);
    }
}
