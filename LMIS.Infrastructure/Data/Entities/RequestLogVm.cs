using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Infrastructure.Data.Entities
{
    public class RequestLogVm
    {
        public long Id { get; set; }
        public CodeSet Admin { get; set; }
        public long PortalUserId { get; set; }
        public string PortalUserName { get; set; }
        public CodeSet RequestType { get; set; }
        public long RequestId { get; set; }
        public string Title { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Approval Approval { get; set; }
    }
}