using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualProfileVM
    {
        public decimal PortalUsersID { get; set; }
        public string FullName { get; set; }
        public string AddressLocalized { get; set; }
        public GlobalString FirstName { get; set; }
        public GlobalString LastName { get; set; }
        public GlobalString Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhotoPath { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string MilitaryStatus { get; set; }
        public string Nationality { get; set; }
        public string IndividualMedical { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string GenderId { get; set; }
        public string MaritalStatusId { get; set; }
        public string MilitaryStatus_Id { get; set; }
        public string NationalityId { get; set; }
        public string IndividualMedicalID { get; set; }
        public bool AllowtoViewMyInfo { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }
        public List<IndividualProfileEducationVM> Educations { get; set; }
        public List<IndividualProfileExperienceVM> Experiences { get; set; }
        public List<IndividualProfileSkillVM> Skills { get; set; }
        public List<IndividualProfileTrainingVM> Trainings { get; set; }
        public List<IndividualProfileCertificateVM> Certificates { get; set; }
        public List<IndividualProfileAppliedJobsVM> AppliedJobs { get; set; }
        public List<IndividualProfileAppliedJobsVM> Jobs { get; set; }
        public List<IndividualProfileAppliedTrainingVM> AppliedTrainings { get; set; }
        public Approval Approval { get; set; }
        public string RejectReason { get; set; } //500
        public Dictionary<string, object> Reviews { get; set; }
    }
}