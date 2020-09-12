using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class FaqVm
    {

        public decimal FAQID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int FAQLanguage { get; set; }
        public string FAQCategoryID { get; set; }
        public string GroupName { get; set; }
    }
}
