using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities.Organization
{
    public class OrganizationContactPersonVM
    {
        public decimal OrganizationContactID { get; set; }
        public decimal PortalUsersID { get; set; }
        public string JobTitleID { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string AuthorizationletterPath { get; set; }
        public byte IsApproved { get; set; }
        public string RejectReason { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleteReason { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string UpdateUSerID { get; set; }
        public string DeleteUserID { get; set; }
        public string UserID { get; set; }

        public List<ContactPersonTranslationVM> Translation { get; set; }


    }
}
