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
    
    public partial class Union
    {
        public Union()
        {
            this.UnionCommittees = new HashSet<UnionCommittee>();
            this.UnionDetails = new HashSet<UnionDetail>();
            this.UnionProfessions = new HashSet<UnionProfession>();
        }
    
        public decimal ID { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public System.DateTime PostDate { get; set; }
        public string PostUserID { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string DeleteUserID { get; set; }
        public string DeleteReason { get; set; }
    
        public virtual ICollection<UnionCommittee> UnionCommittees { get; set; }
        public virtual ICollection<UnionDetail> UnionDetails { get; set; }
        public virtual ICollection<UnionProfession> UnionProfessions { get; set; }
    }
}
