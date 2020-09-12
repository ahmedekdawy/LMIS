using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class TestimonialsVM
    {
      public   int PortalUserID    { get; set; }
      public int SiteRating { get; set; }
      public int LanguageID { get; set; }
      public string Comments { get; set; }
   
      public string Name { get; set; }

    }
}
