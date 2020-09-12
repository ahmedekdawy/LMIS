using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class EmployersTrainingProvidersVM
    {
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string LogoPath { get; set; }
        public int LanguageID { get; set; }
        public int  Type { get; set; }

    }
}
