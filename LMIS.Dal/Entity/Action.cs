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
    
    public partial class Action
    {
        public Action()
        {
            this.PagesActions = new HashSet<PagesAction>();
        }
    
        public int id { get; set; }
        public string ActionName { get; set; }
    
        public virtual ICollection<PagesAction> PagesActions { get; set; }
    }
}
