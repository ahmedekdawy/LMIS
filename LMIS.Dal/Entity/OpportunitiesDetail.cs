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
    
    public partial class OpportunitiesDetail
    {
        public decimal OpportunityID { get; set; }
        public int LanguageID { get; set; }
        public string OpportunityTitle { get; set; }
    
        public virtual Opportunity Opportunity { get; set; }
    }
}
