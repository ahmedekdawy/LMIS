using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualTranslationVM
    {
        public int LanguageID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
