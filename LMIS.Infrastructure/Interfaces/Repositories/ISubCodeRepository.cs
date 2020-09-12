using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public  interface ISubCodeRepository
    {
        List<SubCodeVm> GetAllSubCode(string generalId, int languageId);
        List<SubCodeVm> GetSubCode(string subId);
        List<SubCodeVm> GetAllSubCode(string generalId, int languageId,string yearFrom,string  YearTo);
        List<GlobalString> List(string generalId);
        List<GlobalString> List(string generalId, string execludedIds);
        List<CodeSuperset> Group(string generalId);
        List<GlobalString> FilterByParentSubCodeId(string generalId, string parentSubCodeId);
        string GetSubCode(string subId, int languageId);
        SubCodeVm GetSubCodeModel(string subId, int languageId);
        int Save(SubCodeVm subCode);
        int Save(List<SubCodeVm> subCode);
        int Update(SubCodeVm subCode);
        int Delete(string subId);
        string GetMaxSubCode(string generalId, int languageId);
    }
}