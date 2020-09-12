using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities.Organization
{
    public class OrganizationProfileVM
    {
        public decimal PortalUsersID { get; set; }
        public string OrganizationLogoPath { get; set; }
        public string OrganizationSize { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipPostalCode { get; set; }
        public string Telephone { get; set; }
        public string OrganizationWebsite { get; set; }
        public string OrganizationProfilePath { get; set; }
        public string EconomicActivity { get; set; }
        public string IndustryType { get; set; }
        public string YearsofExperience { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public string RegistrationNumberWithITC { get; set; }
        public byte Is_Approved { get; set; }
        public bool IsDiscalaimerApproved { get; set; }
        public DateTime PostDate { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string OtherIndustryType { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string UserCategory { get; set; }
        public string UserSubCategory { get; set; }
        public bool TrainingProvider { get; set; }
        public bool Employer { get; set; }
        public bool TrainingSeeker { get; set; }
        public Approval Approval { get; set; }
        public string RejectReason { get; set; } //500
        public Dictionary<string, GlobalString> GS { get; set; } 
    }
}