using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IFeedbackRepository
    {
        int Insert(FeedbackVM item);
        Dictionary<string, object> Review(long id, int langId = 1);
        void Approve(string adminId, long reqKey, long feedbackId, bool approved, string reason);
    }
}