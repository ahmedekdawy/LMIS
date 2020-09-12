using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities
{
    public class JobVm
    {
        public class JobSkill
        {
            public bool IsNew { get; set; }
            public CodeSet Industry { get; set; }
            public CodeSet Skill { get; set; } //50 if New Skill
            public CodeSet Type { get; set; }
            public CodeSet Level { get; set; }
        }
        public long JobId { get; set; }
        public long ContactId { get; set; }
        public long PortalUserId { get; set; }
        public string Title { get; set; } //Mandatory if NewTitle is null
        public GlobalString NewTitle { get; set; } //100, Nullable
        public GlobalString Description { get; set; } //1000, Nullable
        public string FileName { get; set; }
        public int ExpFrom { get; set; }
        public int ExpTo { get; set; }
        public int Vacancies { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmploymentType { get; set; }
        public string EdLevel { get; set; }
        public GlobalString EdCert { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool JobStatus { get; set; }
        public List<JobSkill> Skills { get; set; }
        public double PerMonth { get; set; }
        public double PerHour { get; set; }
        public string Currency { get; set; }
        public List<string> MedConditions { get; set; }
        public List<string> DocTypes { get; set; } //Optional
        public Approval Approval { get; set; }
        public string RejectReason { get; set; } //500
        public DateTime PostDate { get; set; }
        public Dictionary<string, object> Extras { get; set; } 
    }

    public class JobsCountVm
    {

        public decimal id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}