using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class HelpfulLinkVm
    {

        public decimal HelpfulLinkID { get; set; }
        public string HelpfulLinkURL { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string HelpfulLinkName { get; set; }
        public int HelpfulLinkLanguage { get; set; }
    }
}
