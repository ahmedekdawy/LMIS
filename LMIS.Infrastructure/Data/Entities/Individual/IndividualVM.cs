using LMIS.Infrastructure.Data.Entities.Base;
using LMIS.Infrastructure.Data.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Individual
{
    public class IndividualVM
    {
        public decimal PortalUsersID { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public string GenderId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatusId { get; set; }
        public string MilitaryStatus_Id { get; set; }
        public string NationalityId { get; set; }
        public string PhotoPath { get; set; }
        public byte Is_Approved { get; set; }
        public byte AllowtoViewMyInfo { get; set; }
        public string RejectReason { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string PostUserID { get; set; }
        public string UpdateUserID { get; set; }
        public string IndividualMedicalID { get; set; }
        public string UserID { get; set; }
        public string CountryID { get; set; }
        public string CityID { get; set; }

        public AspNetUserVM User { get; set; }
        public PortalUserVM PortalUser { get; set; }
        public List<IndividualTranslationVM> Translation { get; set; }

    }
}
