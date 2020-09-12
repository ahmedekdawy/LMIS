using LMIS.Infrastructure.Data.Entities;

using System;
using System.Collections.Generic;


namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IIndividualRegistrationRepository
    {
       // List<OpportunityVm> GetOpportunityList();
        long PostAnOpportunity(ref OpportunityVm r, string userId);
        //bool IsDuplicate(OpportunityVm r);
    }
}
