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
    
    public partial class PublicationReport
    {
        public decimal ReportID { get; set; }
        public string ReportType { get; set; }
        public string ReportTitle { get; set; }
        public string ReportPath { get; set; }
        public string PostUserID { get; set; }
        public System.DateTime PostDate { get; set; }
        public string UpdateUserID { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string DeleteUserID { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string DeleteReason { get; set; }
    }
}