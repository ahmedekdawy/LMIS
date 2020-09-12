using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IFeedbackManager
    {
        int Insert(FeedbackVM item);
        ModelResponse Review(UserInfo user, long id, int langId = 1);
        ModelResponse Approve(UserInfo user, long reqKey, long feedbackId, bool approved, string reason);
    }
}