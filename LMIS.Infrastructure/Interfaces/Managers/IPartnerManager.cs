
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IPartnerManager
    {
        List<PartnerVM> GetActivePartners(int lang, decimal? partnerID);
         ModelResponse Insert(PartnerVM partener);
         ModelResponse Review(UserInfo user, long id, int langId = 1);
         ModelResponse Approve(UserInfo user, long reqKey, long partnerId, bool approved, string reason);
    }
}
