using System.Collections.Generic;
using LMIS.Infrastructure.Data.Entities.Individual;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IIndividualRepository
    {
        void Post(ref IndividualVM vm, string userId);
        IndividualVM UpdatePersonalInfo(IndividualVM vm);
        IndividualProfileVM GetProfile(long portalUserId, int langId = 1);
        void Approve(string adminId, long reqKey, long indId, bool approved, string reason, Dictionary<string, object> newValues);
        decimal JobSeekers(bool unemployed);
    }
}