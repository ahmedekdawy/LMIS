using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
   public interface IObsceneWordsRepository
    {
       ObsceneWordsVm Post(ObsceneWordsVm vm, string Userid);
        int Delete(string userId, long id);
        List<ObsceneWordsVm> Get( decimal? obsceneWordId);
    }
}
