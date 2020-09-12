using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileAppliedJobsVM
    {
        public decimal JobAppliedID { get; set; }
        public DateTime ApplyDate { get; set; }
        public decimal JobOfferID { get; set; }
        public int ViewStatus { get; set; }
        public string Title { get; set; }
        public string JobDescription { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string OrganizationName { get; set; }
    }
}
