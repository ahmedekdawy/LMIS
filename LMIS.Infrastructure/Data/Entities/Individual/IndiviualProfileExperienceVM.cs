using LMIS.Infrastructure.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileExperienceVM
    {
        public decimal IndividualExperienceID { get; set; }
        public decimal PortalUsersID { get; set; }        
        public DateTime EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }
        public string EmploymentJobTitle { get; set; }
        public string TypeOfEmployment { get; set; }
        public byte CurrentEmploymentStatus { get; set; }
        public int ExpYears { get; set; }
        public int ExpMonths { get; set; }

        public string Name { get; set; }
        public string JobDescription { get; set; }
    }
}
