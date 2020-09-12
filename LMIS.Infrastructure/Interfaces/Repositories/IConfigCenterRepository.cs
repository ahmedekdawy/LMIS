using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IConfigCenterRepository
    {
        List<ConfigCenterVm> GetValue(string key, int textType, int sexCode, int categoryType);
        ConfigCenterVm GetConfigCenter(string key);
        Dictionary<string, string> List(List<string> keys, int langId = 0);
        string Get(string key, int langId = 0);
        int Post(Dictionary<string, string> keys);
    }
}