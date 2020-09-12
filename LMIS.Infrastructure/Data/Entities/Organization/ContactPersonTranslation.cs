using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMIS.Infrastructure.Data.Entities.Organization
{
  public  class ContactPersonTranslationVM
    {
        public int LanguageID { get; set; }
        public string ContactFullName { get; set; }
        public string Department { get; set; }
    }
}
