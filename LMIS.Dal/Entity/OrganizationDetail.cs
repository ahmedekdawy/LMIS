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
    
    public partial class OrganizationDetail
    {
        public OrganizationDetail()
        {
            this.JobOffers = new HashSet<JobOffer>();
            this.NewHires = new HashSet<NewHire>();
            this.NewTrainees = new HashSet<NewTrainee>();
            this.OrganizationContact_Info = new HashSet<OrganizationContact_Info>();
            this.OrganizationDetails_Det = new HashSet<OrganizationDetails_Det>();
            this.Partners = new HashSet<Partner>();
            this.TrainingOffers = new HashSet<TrainingOffer>();
        }
    
        public decimal PortalUsersID { get; set; }
        public string OrganizationLogoPath { get; set; }
        public string OrganizationSize { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
        public string ZipPostalCode { get; set; }
        public string Telephone { get; set; }
        public string OrganizationWebsite { get; set; }
        public string OrganizationProfilePath { get; set; }
        public string EconomicActivity { get; set; }
        public string IndustryType { get; set; }
        public System.DateTime EstablishmentDate { get; set; }
        public string RegistrationNumberWithITC { get; set; }
        public byte Is_Approved { get; set; }
        public System.DateTime PostDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string RejectReason { get; set; }
        public bool IsDiscalaimerApproved { get; set; }
        public string YearsofExperienceID { get; set; }
        public string PostUserID { get; set; }
        public string UpdateUserID { get; set; }
        public string DeleteUserID { get; set; }
        public string TrainingListPath { get; set; }
    
        public virtual ICollection<JobOffer> JobOffers { get; set; }
        public virtual ICollection<NewHire> NewHires { get; set; }
        public virtual ICollection<NewTrainee> NewTrainees { get; set; }
        public virtual ICollection<OrganizationContact_Info> OrganizationContact_Info { get; set; }
        public virtual ICollection<OrganizationDetails_Det> OrganizationDetails_Det { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<TrainingOffer> TrainingOffers { get; set; }
        public virtual PortalUser PortalUser { get; set; }
    }
}
