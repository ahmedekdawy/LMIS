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
    
    public partial class jobOfferMedicalDetail
    {
        public decimal JobOfferID { get; set; }
        public string MedicalID { get; set; }
    
        public virtual JobOffer JobOffer { get; set; }
    }
}