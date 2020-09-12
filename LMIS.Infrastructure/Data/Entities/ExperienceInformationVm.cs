using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Infrastructure.Data.Entities
{
    public class ExperienceInformationVm
    {
        public GlobalString Name { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ? EndDate { get; set; }
        public GlobalString JobDescription { get; set; }
        public string TypeofEmployment  { get; set; }
        public long IndividualExperienceID { get; set; }
        public byte CurrentEmploymentStatus { get; set; }
        public int ExpYears { get; set; }
        public int ExpMonths { get; set; }
     
    }
}
