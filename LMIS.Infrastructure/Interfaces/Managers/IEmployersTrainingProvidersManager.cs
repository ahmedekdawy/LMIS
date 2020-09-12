using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IEmployersTrainingProvidersManager
    {
        ModelResponse Post(UserInfo user, EmployersTrainingProvidersVM vm);
        ModelResponse Delete(UserInfo user, long id);
        ModelResponse Get(int languageId, int type, decimal? conceptId);
        ModelResponse GetFrontBack(int languageId, int type, decimal? conceptId);
    }
}
