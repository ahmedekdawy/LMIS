using LMIS.Bll.Helpers;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;

namespace LMIS.Bll.Managers
{
    public class TestimonialsManager : ITestimonialsManager
    {
        private readonly ITestimonialsRepository _repo = new TestimonialsRepository();

        public List<TestimonialsVM> GetTestimonials(int languageID)
        {
            return _repo.GetTestimonials(languageID);
        }
        
        public int Insert(TestimonialsVM item)
        {
            return _repo.Insert(item);
        }

        public ModelResponse Review(UserInfo user, long id, int langId = 1)
        {
            Dictionary<string, object> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);

                ds = _repo.Review(id, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long testimonialId, bool approved, string reason)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                _repo.Approve(user.UserId, reqKey, testimonialId, approved, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
    }
}