//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMIS.Dal.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class IndividualSkillsDetail
    {
        public decimal IndividualSkillsDetailsID { get; set; }
        public decimal PortalUsersID { get; set; }
        public string SkillID { get; set; }
        public string SkillLevelID { get; set; }
        public string YearsOf_Experience { get; set; }
        public string SkillTypeID { get; set; }
        public string IndustryID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string DeleteReason { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUserID { get; set; }
        public System.DateTime PostDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
    
        public virtual IndividualDetail IndividualDetail { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
