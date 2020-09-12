using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileAppliedTrainingVM
    {
        public decimal TrainingAppliedID { get; set; }
        public DateTime ApplyDate { get; set; }
        public decimal TrainingOfferID { get; set; }
        public int ViewStatus { get; set; }
        public string CourseName { get; set; }
        public string TrainingDescription { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OrganizationName { get; set; }
    }
}
