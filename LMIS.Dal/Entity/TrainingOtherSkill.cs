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
    
    public partial class TrainingOtherSkill
    {
        public decimal Id { get; set; }
        public decimal TrainingOfferId { get; set; }
        public string OtherSkill { get; set; }
        public bool IsReviewed { get; set; }
        public string IndustryId { get; set; }
        public string SkillLevelId { get; set; }
    
        public virtual TrainingOffer TrainingOffer { get; set; }
    }
}
