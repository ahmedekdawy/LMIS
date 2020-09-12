using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
   public  class ChatLogManager : IChatLogManager
    {
        private static readonly IChatLogRepository Repo = DalFactory.Singleton.ChatLog;
        public int Insert(List<MessageInfoVM> msgList)
        {
            return Repo.Insert(msgList);
        }
    }
}
