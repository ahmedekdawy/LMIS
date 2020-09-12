using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface ITrainingRepository
    {
        List<TrainingVm> BriefByOrgContact(long contactId, long? id = null, int langId = 1);
        List<TrainingVm> ListByOrgContact(long contactId, long? id = null, int langId = 1);
        List<CodeSet> DetailApplicants(long contactId, long id, int langId = 1);
        long Post(ref TrainingVm vm, string userId);
        void Delete(long id, string userId, string reason);
        bool IsByOrgContact(long id, long contactId);
        void SetTrainingList(long portalUserId, string serverFilePath);
        string GetTrainingList(long portalUserId);
        List<TrainingVm> Search(string keywords, int langId = 1);
        TrainingVm View(long id, long? portalUserId, int langId = 1);
        bool Apply(long id, long portalUserId);
        void Approve(string adminId, long reqKey, long trainingId, bool approved, string reason, Dictionary<string, object> newValues);
    }
}