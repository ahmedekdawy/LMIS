using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface ITrainingManager
    {
        ModelResponse ListByOrgContact(UserInfo user, long? id = null, int langId = 1, bool brief = false);
        ModelResponse DetailApplicants(UserInfo user, long id, int langId = 1);
        ModelResponse Post(UserInfo user, ref TrainingVm vm, string fileFolder, bool validateOnly);
        ModelResponse Delete(UserInfo user, long id, string reason);
        ModelResponse SetTrainingList(UserInfo user, string fileFolder, string serverFilePath);
        ModelResponse GetTrainingList(UserInfo user);
        ModelResponse Search(string keywords, int langId = 1);
        ModelResponse View(UserInfo user, long id, int langId = 1);
        ModelResponse Apply(UserInfo user, long id);
        ModelResponse Approve(UserInfo user, long reqKey, long trainingId, bool approved, string reason, Dictionary<string, object> newValues);
    }
}