﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LMIS.Infrastructure.Interfaces.Managers
{
   public  interface IGeneralCodeManager
    {
       List<GeneralCodeVM> GetGeneralCode(int languageID);
       List<GeneralCodeVM> GetGeneralCode(string parentGeneralcodeId, int languageID);
       int Save(GeneralCodeVM generalCode);

    }
}
