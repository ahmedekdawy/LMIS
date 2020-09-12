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
    public class ChatLogRepository : IChatLogRepository
    {
        public int Insert(List<MessageInfoVM> msgList )
        {
            
            using (var context = new LMISEntities())
            {
                var messages =
                    msgList.Select(
                        s => new ChatLog {
                            AdminUserId = s.UserId, 
                            ChatMessage = s.Message, 
                            MsgDate = s.MsgDate,
                            PortalUserID = (int)s.PortalUserID
                        });
                context.ChatLogs.AddRange(messages);
              return   context.SaveChanges();
            }
        }
    }
}
