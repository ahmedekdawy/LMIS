using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class LmisReportsRepository : ILmisReportsRepository
    {
        public List<TransactionHistoryVm> TransactionHistory(string transactionType, string userName, DateTime postDate, int actionType)
        {
            List<TransactionHistoryVm> result;

            using (var db = new LMISEntities())

            {
                result = db.CareerMapAlternatives.Where(w => w.PostUserID == userName).Select(s => new TransactionHistoryVm() { PostUserName = db.AspNetUsers.Where(u => u.Id == s.PostUserID).Select(ss=>ss.Email ).FirstOrDefault() }).ToList();
            }
            

            return result;

        }
    }
}
