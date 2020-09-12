using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public  class PartnersDetailsVM
    {
        public decimal PartnerID { get; set; }
        public int LanguageID { get; set; }
        public string CEOFirstName { get; set; }
        public string CEOLastName { get; set; }
        public string GeneralDescriptionCoreBusiness { get; set; }
        public string PossibleAreaOfCooperation { get; set; }

    }
}
