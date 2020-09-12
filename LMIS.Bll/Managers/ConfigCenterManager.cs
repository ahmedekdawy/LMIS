using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Bll.Managers
{
    public class ConfigCenterManager : IConfigCenterManager
    {
        private static readonly IConfigCenterRepository Repo = DalFactory.Singleton.ConfigCenter;

        public List<ConfigCenterVm> GetValue(string key, int textType, int sexCode, int categoryType)
        {
            return Repo.GetValue(key, textType, sexCode, categoryType);
        }

        public ConfigCenterVm GetConfigCenter(string key)
        {
            return Repo.GetConfigCenter(key);
        }

        public Dictionary<string, string> List(List<string> keys, int langId = 0)
        {
            return Repo.List(keys, langId);
        }

        public string Get(string key, int langId = 0)
        {
            return Repo.Get(key, langId);
        }

        public ModelResponse Post(Dictionary<string, string> keys)
        {
             Repo.Post(keys);
            return new ModelResponse(0, null); 
        }
    }
}