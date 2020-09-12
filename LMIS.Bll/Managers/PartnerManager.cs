using LMIS.Dal;

using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Bll.Managers
{
 
    public class PartnerManager : IPartnerManager
    {
        private readonly IPartnerRepository _repo = new PartnerRepository();
        public List<Infrastructure.Data.Entities.PartnerVM> GetActivePartners(int lang, decimal? partnerID)
        {
            
            return _repo.GetActivePartners( lang,partnerID);
        }

        public ModelResponse Insert(PartnerVM partener)
        {
            try
            {
                _repo.Insert(partener);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }
           return new ModelResponse(0,null );
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

        public ModelResponse Approve(UserInfo user, long reqKey, long partnerId, bool approved, string reason)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                _repo.Approve(user.UserId, reqKey, partnerId, approved, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

    }
}
