using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class ConceptsDefinitionsVM
    {
        public decimal ConceptDefID { get; set; }
        public string ConceptDefTitle { get; set; }
        public string ConceptDefDesc { get; set; }
    
      public int LanguageID { get; set; }

    }
}
