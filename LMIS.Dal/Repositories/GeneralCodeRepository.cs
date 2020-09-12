using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Interfaces;

using LMIS.Dal.Entity;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Interfaces.Repositories;


namespace LMIS.Dal.Repositories
{
  public   class GeneralCodeRepository : IGeneralCodeRepository
    {
        private LMISEntities Context=new LMISEntities();

        public List<GeneralCodeVM> GetGeneralCode(int languageID)
        {
            return (from generalCode in Context.GeneralCodes
                    where generalCode.LanguageID == languageID
                    select new GeneralCodeVM
                        {
                            GeneralID = generalCode.GeneralID,
                            Name = generalCode.Name,
         
                        }).ToList();
        
        }


        public List<GeneralCodeVM> GetGeneralCode(string parentGeneralcodeId, int languageID)
        {
            return (from generalCode in Context.GeneralCodes
                    where generalCode.ParentGeneralcodeID == parentGeneralcodeId && generalCode.LanguageID == languageID
                    orderby generalCode.Name 
                    select new GeneralCodeVM
                    {
                        GeneralID = generalCode.GeneralID,
                        Name = generalCode.Name,

                    }).ToList();
        }
        public int Save(GeneralCodeVM generalCode)
        {
            GeneralCode item=new GeneralCode(){
                GeneralID = generalCode.GeneralID,
                LanguageID = generalCode.LanguageID,
                Name = generalCode.Name ,
                ParentGeneralcodeID = generalCode.ParentGeneralcodeID
                
            };
           
            //if ( Context.GeneralCodes.Where(i=>i.GeneralID==item.GeneralID ).FirstOrDefault()!=null )
            //{
            //    Context.GeneralCodes.Remove(Context.GeneralCodes.Where(i => i.GeneralID == item.GeneralID).FirstOrDefault());
            //}
           
            Context.GeneralCodes.Add(item);
            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
    }
}
