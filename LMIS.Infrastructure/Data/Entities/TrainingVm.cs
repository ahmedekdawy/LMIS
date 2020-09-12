using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities
{
    public class TrainingVm
    {
        public class TrainingSkill
        {
            public bool IsNew { get; set; }
            public CodeSet Industry { get; set; }
            public CodeSet Skill { get; set; } //50 if New Skill
            public CodeSet Type { get; set; }
            public CodeSet Level { get; set; }
        }
        public long Id { get; set; }
        public long ContactId { get; set; }
        public long PortalUserId { get; set; }
        public string Title { get; set; } //Mandatory if NewTitle is null
        public GlobalString NewTitle { get; set; } //100, Nullable
        public GlobalString Description { get; set; } //1000, Nullable
        public string FileName { get; set; } //Mandatory if Description is null
        public string Country { get; set; }
        public string City { get; set; }
        public GlobalString Address { get; set; } //500
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Seats { get; set; }
        public double? Cost { get; set; }
        public List<string> Occurrence { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string TimeZone { get; set; }
        public bool Status { get; set; }
        public List<TrainingSkill> Skills { get; set; }
        public Approval Approval { get; set; }
        public string RejectReason { get; set; } //500
        public Dictionary<string, object> Extras { get; set; } 
    }
}