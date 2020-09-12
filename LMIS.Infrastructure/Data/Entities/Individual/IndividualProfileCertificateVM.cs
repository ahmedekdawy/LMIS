using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileCertificateVM
    {
        public decimal IndividualCertificationID { get; set; }
        public decimal PortalUsersID { get; set; }
        public DateTime CertificateIssueDate { get; set; }
        public DateTime CertificateValidUntil { get; set; }
        public string CertificateName { get; set; }
    }
}
