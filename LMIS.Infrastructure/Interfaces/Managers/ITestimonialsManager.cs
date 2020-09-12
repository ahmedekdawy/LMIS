using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface ITestimonialsManager
    {
        List<TestimonialsVM> GetTestimonials(int languageID);
        int Insert(TestimonialsVM item);
        ModelResponse Review(UserInfo user, long id, int langId = 1);
        ModelResponse Approve(UserInfo user, long reqKey, long testimonialId, bool approved, string reason);
    }
}