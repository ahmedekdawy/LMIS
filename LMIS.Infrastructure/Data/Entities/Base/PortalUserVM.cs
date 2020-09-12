using LMIS.Infrastructure.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Organization
{
   public class PortalUserVM
    {
        public decimal PortalUsersID { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string UserCategory { get; set; }
        public string UserSubCategory { get; set; }
        public bool TrainingProvider { get; set; }
        public bool Employer { get; set; }
        public bool JobSeeker { get; set; }
        public bool TrainingSeeker { get; set; }
        public bool Researcher { get; set; }
        public bool Internal { get; set; }
        public bool IsSubscriper { get; set; }
    }
}
