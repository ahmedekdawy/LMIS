using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Organization
{
    public class OrganizationVM
    {
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
        public string YearsofExperienceID { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public string RegistrationNumberWithITC { get; set; }
        public byte Is_Approved { get; set; }
        public bool IsDiscalaimerApproved { get; set; }
        public DateTime PostDate { get; set; }


        public GlobalString OrganizationNameLocalized { get; set; }
        public GlobalString AddressLocalized { get; set; }
        public GlobalString OtherIndustryTypeLocalized { get; set; }

        public AspNetUserVM User { get; set; }
        public PortalUserVM PortalUser { get; set; }
        public List<OrganizationTranslationVM> Translation { get; set; }
        public List<OrganizationContactPersonVM> ContactPersons { get; set; }

    }
}
