using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IRecruitmentAgenciesRepository
    {
        RecruitmentAgenciesVM Post(RecruitmentAgenciesVM vm, string id);
      int Delete(string UserId, long id);
      List<RecruitmentAgenciesVM> Get(int languageId,  decimal? Id);
    }
}
