using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class MessageInfoVM
    {
        public string UserId { get; set; }
        public decimal  PortalUserID { get; set; }
        public string UserName { get; set; }

        public string Message { get; set; }

        public string UserGroup { get; set; }

        public DateTime MsgDate { get; set; }
    }
}
