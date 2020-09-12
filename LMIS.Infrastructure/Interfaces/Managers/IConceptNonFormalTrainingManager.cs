using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
  public  interface IConceptNonFormalTrainingManager
    {
      ModelResponse Post(UserInfo user, ConceptNonFormalTrainingVM vm);
        ModelResponse Delete(UserInfo user, long id);
        ModelResponse Get(int languageId,  decimal? conceptId);
    }
}
