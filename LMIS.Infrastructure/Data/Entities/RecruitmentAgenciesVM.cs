using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class RecruitmentAgenciesVM
    {
        public decimal RecruitmentAgencieID { get; set; }
        public string Name { get; set; }
        public string Background { get; set; }
        public string LogoPath { get; set; }
        public int LanguageID { get; set; }

    }
}
