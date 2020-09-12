using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Infrastructure.Data.Entities
{
    public class TrainingandCertificationVm
    {
        public GlobalString trainingname { get; set; }
        public GlobalString TrainingProvider { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long IndividualTrainingID { get; set; }
        public GlobalString CertificationName { get; set; }
        public DateTime CertificationIssueDate { get; set; }
        public DateTime CertificationValidUntil { get; set; }
        public long IndividualCertificationID { get; set; }
    
    }
}
