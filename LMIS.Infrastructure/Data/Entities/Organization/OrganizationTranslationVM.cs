using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Organization
{
   public class OrganizationTranslationVM
    {
        public int LanguageID { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string OtherIndustryType { get; set; }
    }
}
