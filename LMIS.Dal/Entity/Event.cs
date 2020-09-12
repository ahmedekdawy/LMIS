//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMIS.Dal.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public Event()
        {
            this.EventsDetails = new HashSet<EventsDetail>();
        }
    
        public decimal EventId { get; set; }
        public decimal OrganizationContactID { get; set; }
        public string EventTypeID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double Price { get; set; }
        public string EventContactTelephone { get; set; }
        public string EventContactWebsite { get; set; }
        public string UploadPath { get; set; }
        public byte IsApproved { get; set; }
        public string RejectReason { get; set; }
        public System.DateTime PostDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteReason { get; set; }
        public string PostUserID { get; set; }
        public string UpdateUserID { get; set; }
        public string DeleteUserID { get; set; }
        public Nullable<bool> IsInternal { get; set; }
        public Nullable<bool> IsInformal { get; set; }
    
        public virtual OrganizationContact_Info OrganizationContact_Info { get; set; }
        public virtual ICollection<EventsDetail> EventsDetails { get; set; }
    }
}