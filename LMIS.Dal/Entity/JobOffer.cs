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
    
    public partial class JobOffer
    {
        public JobOffer()
        {
            this.JobApplieds = new HashSet<JobApplied>();
            this.JobOfferAdditionalDocs = new HashSet<JobOfferAdditionalDoc>();
            this.JobOfferDetails = new HashSet<JobOfferDetail>();
            this.JobOfferEducationLevels = new HashSet<JobOfferEducationLevel>();
            this.jobOfferMedicalDetails = new HashSet<jobOfferMedicalDetail>();
            this.jobOfferSkillsDetails = new HashSet<jobOfferSkillsDetail>();
            this.JobOtherSkills = new HashSet<JobOtherSkill>();
        }
    
        public decimal JobOfferID { get; set; }
        public decimal PortalUsersID { get; set; }
        public string JobTiltleID { get; set; }
        public string GenderID { get; set; }
        public string EmploymentTypeID { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
        public int NumberOfVacanciesPosition { get; set; }
        public double SalaryRangePerMonth { get; set; }
        public Nullable<double> SalaryRangePerHour { get; set; }
        public string SalaryCurrencyID { get; set; }
        public byte IsApproved { get; set; }
        public Nullable<decimal> OrganizationContactID { get; set; }
        public string RejectReason { get; set; }
        public System.DateTime PostDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteReason { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool JobStatus { get; set; }
        public int ExpYearFrom { get; set; }
        public int ExpYearTo { get; set; }
        public string PostUserID { get; set; }
        public string UpdateUserID { get; set; }
        public string DeleteUserID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<JobApplied> JobApplieds { get; set; }
        public virtual OrganizationDetail OrganizationDetail { get; set; }
        public virtual ICollection<JobOfferAdditionalDoc> JobOfferAdditionalDocs { get; set; }
        public virtual ICollection<JobOfferDetail> JobOfferDetails { get; set; }
        public virtual ICollection<JobOfferEducationLevel> JobOfferEducationLevels { get; set; }
        public virtual ICollection<jobOfferMedicalDetail> jobOfferMedicalDetails { get; set; }
        public virtual ICollection<jobOfferSkillsDetail> jobOfferSkillsDetails { get; set; }
        public virtual ICollection<JobOtherSkill> JobOtherSkills { get; set; }
    }
}