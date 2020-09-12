using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class FeedbackVM
    {
      public   int PortalUserID { get; set; }
      public string  FeedbackTypeId { get; set; }
      public int FeedbackLang { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
    
      public string PostUserID { get; set; }

    }
}
