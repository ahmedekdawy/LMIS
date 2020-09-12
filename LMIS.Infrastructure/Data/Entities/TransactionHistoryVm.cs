using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
   public  class TransactionHistoryVm
    {
        public string  TransactionType { get; set; }
        public string TransactionTitle { get; set; }
        public string PostUserName { get; set; }
        public DateTime  PostDate { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime UpdateDate { get; set; }
      
     
    
    }
}
