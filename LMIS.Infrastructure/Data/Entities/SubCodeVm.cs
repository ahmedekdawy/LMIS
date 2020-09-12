using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
   public  class SubCodeVm
    {
        public string SubID { get; set; }
        public string GeneralID { get; set; }
        public int LanguageID { get; set; }
        public string Name { get; set; }
        public string ParentSubCodeID { get; set; }
        public string ParentSubCodeName { get; set; }
        public string GeneralName { get; set; }

    }
}
