using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Infrastructure.Data.Entities
{
    public class OrgVm
    {
        public class OrgContactInfo
        {
            public string Country { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public GlobalString Address { get; set; }
            public string Telephone { get; set; }
            public string Website { get; set; }
        }
        public long PortalUserId { get; set; }
        public string UserName { get; set; }
        public string AuthLetterFileName { get; set; }
        public string OrgType { get; set; }
        public string LogoFileName { get; set; }
        public GlobalString OrgName { get; set; }
        public string ProfileFileName { get; set; }
        public string OrgSize { get; set; }
        public string IDType { get; set; }
        public string ID { get; set; }
        public DateTime DateEstablished { get; set; }
        public string YOE { get; set; }
        public string Activity { get; set; }
        public string Industry { get; set; }
        public GlobalString OtherIndustry { get; set; }
        public OrgContactInfo ContactInfo { get; set; }
        public bool ReceiveTraining { get; set; }
        public bool OfferJobs { get; set; }
        public bool OfferTraining { get; set; }
        public string ItcRegNo { get; set; }
        public Approval Approval { get; set; }
        public string RejectReason { get; set; }
    }
}