using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
  public  interface IConceptNonFormalTrainingRepository
    {
      ConceptNonFormalTrainingVM Post(ConceptNonFormalTrainingVM vm, string id);
      int Delete(string UserId, long id);
      List<ConceptNonFormalTrainingVM> Get(int languageId,decimal? conceptId);
    }
}
