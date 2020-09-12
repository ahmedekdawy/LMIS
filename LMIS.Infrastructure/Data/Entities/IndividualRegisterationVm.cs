using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Infrastructure.Data.Entities
{
    public class IndividualRegisterationVm
    {
        public long RegisterationId { get; set; }
        public GlobalString FirstName { get; set; }
        public GlobalString LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Militarystatus { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Maritalstatus { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public GlobalString Address { get; set; }
        public string MobileNumber { get; set; }
        public string MobileNumber2 { get; set; }
        public string TelephoneNo { get; set; }
        public string Nationality { get; set; }
        public string IdType { get; set; }
        public string NationailtyIDorPassportID { get; set; }
        public string Medicalconditions { get; set; }
        public byte AllowtoViewMyInfo { get; set; }
        public string IndividualMedicalId { get; set; }
        public int LanguageId { get; set; }
        public Approval Approval { get; set; }
        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>
        public long ContactId { get; set; }
        public GlobalString ContactName { get; set; }
        public GlobalString OrganizationName { get; set; }
        public bool IsInformal { get; set; }
    }
}
