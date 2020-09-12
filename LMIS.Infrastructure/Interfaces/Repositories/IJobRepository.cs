using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IJobRepository
    {
        List<JobVm> BriefByOrgContact(long contactId, long? id = null, int langId = 1);
        List<JobVm> ListByOrgContact(long contactId, long? id = null, int langId = 1);
        List<Dictionary<string, object>> DetailApplicants(long contactId, long id, int langId = 1);
        void ChangeApplicantStatus(long id, int status);
        long Post(ref JobVm vm, string userId);
        void Delete(long id, string userId, string reason);
        bool IsByOrgContact(long id, long contactId);
        List<JobVm> Search(string keywords, int langId = 1);
        JobVm View(long id, long? portalUserId, int langId = 1);
        Dictionary<string, object> ApplicationRequirements(long id, int langId = 1);
        bool Apply(long id, long portalUserId, string appForm, List<CodeSet> attachments);
        void Approve(string adminId, long reqKey, long jobId, bool approved, string reason, Dictionary<string, object> newValues);
        List<JobsCountVm> JobsPerYear(bool started, bool jobStatus, int langId);
        decimal JobApplied();
    }
}