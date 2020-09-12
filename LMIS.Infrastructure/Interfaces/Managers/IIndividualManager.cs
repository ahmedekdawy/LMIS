using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Individual;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IIndividualManager
    {
        ModelResponse Post(ref IndividualVM vm, string userId);
        ModelResponse UpdatePersonalInfo(IndividualVM vm);
        ModelResponse GetProfile(UserInfo user, long portalUserId, int langId = 1);
        ModelResponse Approve(UserInfo user, long reqKey, long indId, bool approved, string reason, Dictionary<string, object> newValues);
        decimal JobSeekers(bool unemployed);
    }
}