using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
   public  interface ILmisReportsManager
    {
       List<TransactionHistoryVm> TransactionHistory(string transactionType, string userName, DateTime postDate, int actionType);
    }
}
