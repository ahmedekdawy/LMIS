using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
  public   class ConceptNonFormalTrainingVM
    {
      public decimal ConceptID { get; set; }
      public string ConceptTitle { get; set; }
      public string ConceptDescription { get; set; }
      public string ImagePath { get; set; }
      public int LanguageID { get; set; }

    }
}
