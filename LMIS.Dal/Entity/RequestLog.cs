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
    
    public partial class RequestLog
    {
        public decimal ID { get; set; }
        public string RequestType { get; set; }
        public decimal RequestID { get; set; }
        public System.DateTime PostDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public byte Is_Approved { get; set; }
        public decimal PortalUserID { get; set; }
        public string AdminID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}