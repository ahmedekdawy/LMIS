using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IJobManager
    {
        ModelResponse ListByOrgContact(UserInfo user, long? id = null, int langId = 1, bool brief = false);
        ModelResponse DetailApplicants(UserInfo user, long id, int langId = 1);
        ModelResponse ChangeApplicantStatus(long id, int status);
        ModelResponse Post(UserInfo user, ref JobVm vm, string fileFolder, bool validateOnly);
        ModelResponse Delete(UserInfo user, long id, string reason);
        ModelResponse Search(string keywords, int langId = 1);
        ModelResponse View(UserInfo user, long id, int langId = 1);
        ModelResponse ApplicationRequirements(long id, int langId = 1);
        ModelResponse Apply(UserInfo user, long id, string fileFolder, string appForm, List<CodeSet> attachments);
        ModelResponse Approve(UserInfo user, long reqKey, long jobId, bool approved, string reason, Dictionary<string, object> newValues);
        List<JobsCountVm> JobsPerYear(bool started, bool jobStatus, int langId);
        decimal JobApplied();
    }
}