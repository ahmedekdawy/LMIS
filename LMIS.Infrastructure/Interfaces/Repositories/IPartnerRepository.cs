
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
   public interface IPartnerRepository
    {
        List<PartnerVM> GetActivePartners(int lang, decimal? partnerID);
        int Insert(PartnerVM partener);
        Dictionary<string, object> Review(long id, int langId = 1);
        void Approve(string adminId, long reqKey, long partnerId, bool approved, string reason);
    }
}
