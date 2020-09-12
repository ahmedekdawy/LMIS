using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Helpers;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;

namespace LMIS.Bll.Managers
{
    public class IndividualRegistrationManager : IIndividualRegistrationManager
    {
        private readonly IndividualRegistrationManager _reg = new IndividualRegistrationManager();

        public string UserId { get; set; }

        public ModelResponse GetOpportunityList(out List<OpportunityVm> ds)
        {
            try
            {
                ds = _reg.GetOpportunityList();
            }
            catch (Exception ex)
            {
                ds = null;
                return Factory.ModelResponse(ex);
            }

            return Factory.ModelResponse(0);
        }

        public ModelResponse PostAnOpportunity(ref OpportunityVm r)
        {
            try
            {
                if (r.Title.IsNullOrWhiteSpace()) return Factory.ModelResponse(1);

                if (r.StartDate < DateTime.Today || r.EndDate < r.StartDate) return Factory.ModelResponse(2);

                if (_repo.IsDuplicate(r)) return Factory.ModelResponse(3);

                if (!string.IsNullOrWhiteSpace(r.FilePath))
                {
                    r.Approval = Approval.Pending;
                    _repo.PostAnOpportunity(ref r, UserId);
                }
            }
            catch (Exception ex)
            {
                return Factory.ModelResponse(ex);
            }

            return Factory.ModelResponse(0);
        }
    }
}
