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
    
    public partial class EmployerGuidancekitDetail
    {
        public decimal EmployerguidancekitID { get; set; }
        public int LanguageID { get; set; }
        public string InterviewQuestion { get; set; }
        public string InterviewAnswer { get; set; }
    
        public virtual EmployerGuidancekit EmployerGuidancekit { get; set; }
    }
}
