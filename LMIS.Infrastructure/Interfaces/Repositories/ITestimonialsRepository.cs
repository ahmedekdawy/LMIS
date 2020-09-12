using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface   ITestimonialsRepository
    {
        List<TestimonialsVM> GetTestimonials(int languageID);
        int Insert(TestimonialsVM item);
        Dictionary<string, object> Review(long id, int langId = 1);
        void Approve(string adminId, long reqKey, long testimonialId, bool approved, string reason);
    }
}