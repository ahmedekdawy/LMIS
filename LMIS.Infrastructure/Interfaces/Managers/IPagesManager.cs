﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
  public   interface IPagesManager
    {
      List<PagesVM> GetPages();
    }
}
