using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
  public  interface IEmployersTrainingProvidersRepository
    {
      EmployersTrainingProvidersVM Post(EmployersTrainingProvidersVM vm, string id);
      int Delete(string UserId, long id);
      List<EmployersTrainingProvidersVM> Get(int languageId, int type, decimal? conceptId);
      List<EmployersTrainingProvidersVM> GetFrontBack(int languageId, int type, decimal? conceptId);
    }
}
