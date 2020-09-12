using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal;
using LMIS.Infrastructure.Interfaces;

using LMIS.Dal.Repositories;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;
using LMIS.Utlities.Helpers;

namespace LMIS.Bll.Managers
{

    public class GeneraCodeManager :IGeneralCodeManager 
    {
     private readonly    IGeneralCodeRepository _generalCodeRepository =new GeneralCodeRepository();
     //GeneraCodeManager(IGeneralCodeRepository reneralCodeRepository)
     //{
     //    _generalCodeRepository = reneralCodeRepository;
     //}
     List<GeneralCodeVM> IGeneralCodeManager.GetGeneralCode(int languageID)
        {
          return   _generalCodeRepository.GetGeneralCode( languageID);
        }


     public List<GeneralCodeVM> GetGeneralCode(string parentGeneralcodeId, int languageID)
         {
             return _generalCodeRepository.GetGeneralCode(parentGeneralcodeId,  languageID);
         }

        public int Save(GeneralCodeVM generalCode)

        {
            generalCode.Name = new ArabicPrepocessor().StripArabicWords(generalCode.Name);
            return _generalCodeRepository.Save(generalCode);
        }
    }
}
