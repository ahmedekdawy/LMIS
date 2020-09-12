using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
   public interface IListOfEmailsManager
    {
       ModelResponse Post(UserInfo user,ListOfEmailsVm vm);
       ModelResponse Delete(UserInfo user, long id);
       ModelResponse Get(decimal? EmailID);
    }
}
