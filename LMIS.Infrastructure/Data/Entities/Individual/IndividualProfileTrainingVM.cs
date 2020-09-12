using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileTrainingVM
    {
        public decimal IndividualTrainingID { get; set; }
        public decimal PortalUsersID { get; set; }
        public DateTime TrainingStartDate { get; set; }
        public DateTime TrainingEndDate { get; set; }
        public string TrainingName { get; set; }
        public string TrainingProviderName { get; set; }

    }
}
