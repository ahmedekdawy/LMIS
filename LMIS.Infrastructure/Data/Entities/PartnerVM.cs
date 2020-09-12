using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public  class PartnerVM
    {
        public decimal PartnerID { get; set; }
        public decimal PortalUserID { get; set; }
        public string CEOEmail { get; set; }
        public int Is_Approved { get; set; }
        public int YearFounded { get; set; }
        public string RejectReason { get; set; }
        public string ZipPostalCode { get; set; }
        public string Telephone { get; set; }
        public string OrganizationWebsite { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string OrganizationLogoPath { get; set; }
        public Nullable< DateTime>  PostDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<bool>   IsDeleted { get; set; }
        public string DeleteReason { get; set; }
        public Nullable<DateTime>  DeleteDate { get; set; }
        public decimal  OrganizationContactID { get; set; }
        public string PostUserID { get; set; }
        public string UpdateUserID { get; set; }
        public string DeleteUserID { get; set; }
        public int LanguageID { get; set; }
        public string CEOFirstName { get; set; }
        public string CEOLastName { get; set; }
        public string GeneralDescriptionCoreBusiness { get; set; }
        public string PossibleAreaOfCooperation { get; set; }
    }
}
