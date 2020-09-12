﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Bll.Managers
{
    public class HelpfulLinkManager : IHelpfulLinkManager
    {
        private static readonly IHelpfulLinkRepository Repo = DalFactory.Singleton.HelpfulLink;
        public Infrastructure.Data.DTOs.ModelResponse Post(Infrastructure.Data.DTOs.UserInfo user, Infrastructure.Data.Entities.HelpfulLinkVm vm)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);


                var ds = Repo.Post(vm, user.UserId);
              //  if(ds==null)
               // { return new ModelResponse(102, ds); }
                return new ModelResponse(0, ds);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }
        }

        public Infrastructure.Data.DTOs.ModelResponse Delete(Infrastructure.Data.DTOs.UserInfo user, long id)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);


                Repo.Delete(user.UserId, id);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public Infrastructure.Data.DTOs.ModelResponse Get(int languageId, decimal? HelpfulLinkId)
        {
            var ds = Repo.Get(languageId, HelpfulLinkId);
            return new ModelResponse(0, ds); 
        }

       
    }
}