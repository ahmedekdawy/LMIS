using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
    public class ConceptNonFormalTrainingManager : IConceptNonFormalTrainingManager
    {
        private static readonly IConceptNonFormalTrainingRepository Repo = DalFactory.Singleton.ConceptNonFormalTraining;
        public  ModelResponse Post(UserInfo user, ConceptNonFormalTrainingVM vm)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);


             var ds=   Repo.Post(vm, user.UserId);
             return new ModelResponse(0,ds);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            
        }

       
        public ModelResponse Delete(Infrastructure.Data.DTOs.UserInfo user, long id)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);


                Repo.Delete( user.UserId,id);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse Get(int languageId,decimal? conceptId)
        {
            var ds = Repo.Get( languageId,conceptId);
            return new ModelResponse(0, ds); 
        }
    }
}
