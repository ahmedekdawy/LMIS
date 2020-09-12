using LMIS.Infrastructure.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileSkillVM
    {
        public decimal IndividualSkillsDetailsID { get; set; }
        public decimal PortalUsersID { get; set; }
        public CodeSet Skill { get; set; }
        public CodeSet SkillLevel { get; set; }
        /// <summary>
        /// this property determins the skill percentage that draws the skill bar
        /// </summary>
        public int SkillLevelPercentage
        {
            get
            {
                try
                {
                    if (SkillLevel != null)
                        return int.Parse(SkillLevel.id.Substring(SkillLevel.id.Length - 1, 1)) * 25;
                    else
                        return 50;
                }
                catch { return 50; }
            }
        }

        public string YearsOf_Experience { get; set; }
        public CodeSet SkillType { get; set; }
        public CodeSet Industry { get; set; }
        public bool IsOtherSkill { get; set; }

    }
}
