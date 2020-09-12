﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IHelpfulLinkManager
    {
        ModelResponse Post(UserInfo user, HelpfulLinkVm vm);
       ModelResponse Delete(UserInfo user, long id);
       ModelResponse Get(int languageId,decimal? HelpfulLinkId);
    }
}
