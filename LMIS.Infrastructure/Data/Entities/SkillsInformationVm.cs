using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities
{
    public class SkillsInformationVm
    {
        public class TrainingSkill
        {
            public bool IsNew { get; set; }
            public CodeSet Industry { get; set; }
            public CodeSet Skill { get; set; } //50 if New Skill
            public CodeSet Type { get; set; }
            public CodeSet Level { get; set; }
            public string YOfExperience { get; set; }
        }
        public List<TrainingSkill> Skills { get; set; }
        public long Id { get; set; }

        public long PortalUsersID { get; set; }
    }
}