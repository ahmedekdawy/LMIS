using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;
using LMIS.Infrastructure.Data.DTOs;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IConfigCenterManager
    {
        List<ConfigCenterVm> GetValue(string key, int textType, int sexCode, int categoryType);
        ConfigCenterVm GetConfigCenter(string key);
        Dictionary<string, string> List(List<string> keys, int langId = 0);
        string Get(string key, int langId = 0);
        ModelResponse Post(Dictionary<string, string> keys);
    }
}