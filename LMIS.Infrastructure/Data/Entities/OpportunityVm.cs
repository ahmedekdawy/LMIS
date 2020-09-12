using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities
{
    public class OpportunityVm
    {
        public long OpportunityId { get; set; }
        public long ContactId { get; set; }
        public GlobalString ContactName { get; set; }
        public GlobalString OrganizationName { get; set; }
        public GlobalString Title { get; set; }
        public string FilePath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Approval Approval { get; set; }
        public string RejectReason { get; set; } //500
        public bool IsInformal { get; set; }
        public bool IsInternal { get; set; }
        public Dictionary<string, object> Extras { get; set; } 
    }
}