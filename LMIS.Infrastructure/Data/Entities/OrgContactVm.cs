using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Data.Entities
{
    public class OrgContactVm
    {
        public long PortalUserId { get; set; }
        public long ContactId { get; set; }
        public string UserName { get; set; }
        public GlobalString FullName { get; set; }
        public GlobalString Department { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
    }
}