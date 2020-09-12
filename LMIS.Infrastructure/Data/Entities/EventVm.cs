using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities
{
    public class EventVm
    {
        public long EventId { get; set; }
        public long ContactId { get; set; }
        public GlobalString Title { get; set; } //100
        public GlobalString Address { get; set; } //200
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public string TypeStr { get; set; }
        public double Price { get; set; }
        public GlobalString ContactAddress { get; set; } //200
        public string ContactTelephone { get; set; } //20
        public string ContactWebsite { get; set; } //200
        public string FilePath { get; set; } //200
        public Approval Approval { get; set; }
        public string RejectReason { get; set; } //500
        public bool IsInternal { get; set; }
        public bool IsInformal { get; set; }
        public Dictionary<string, object> Extras { get; set; } 
    }
}