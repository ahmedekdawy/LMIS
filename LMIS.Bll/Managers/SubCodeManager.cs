using System.Collections.Generic;
using LMIS.Dal.Repositories;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;
using LMIS.Utlities.Helpers;

namespace LMIS.Bll.Managers
{
    public class SubCodeManager : ISubCodeManager
    {
        private readonly ISubCodeRepository _repo = new SubCodeRepository();

        public List<Infrastructure.Data.Entities.SubCodeVm> GetAllSubCode(string generalId, int languageId)
        {
            return _repo.GetAllSubCode(generalId, languageId);
        }

        public List<SubCodeVm> GetSubCode(string subId)
        {
            return _repo.GetSubCode( subId);
        }

        public List<GlobalString> List(string generalId)
        {
            return _repo.List(generalId);
        }
        public List<GlobalString> List(string generalId, string execludedIds)
        {
            return _repo.List(generalId,  execludedIds);
        }

        public List<CodeSuperset> Group(string generalId)
        {
            return _repo.Group(generalId);
        }

        public List<GlobalString> FilterByParentSubCodeId(string generalId, string parentSubCodeId)
        {
            return _repo.FilterByParentSubCodeId(generalId, parentSubCodeId);
        }

        public string GetSubCode(string subId, int languageId)
        {
            return _repo.GetSubCode(subId, languageId);
        }

        public SubCodeVm GetSubCodeModel(string subId, int languageId)
        {
            return _repo.GetSubCodeModel(subId, languageId);
        }

        public int Save(SubCodeVm subCode)
        {
            subCode.Name = new ArabicPrepocessor().StripArabicWords(subCode.Name);
            return _repo.Save(subCode);
        }
        public int Save(List<SubCodeVm> subCode)
        {
            subCode[0].Name = new ArabicPrepocessor().StripArabicWords(subCode[0].Name);
            subCode[1].Name = new ArabicPrepocessor().StripArabicWords(subCode[1].Name);
            subCode[2].Name = new ArabicPrepocessor().StripArabicWords(subCode[2].Name);
            return _repo.Save(subCode);
        }

        public int Update(SubCodeVm subCode)
        {
            subCode.Name = new ArabicPrepocessor().StripArabicWords(subCode.Name);
            return _repo.Update(subCode);
        }

        public int Delete(string subId)
        {
            return _repo.Delete(subId);
        }

        public string GetMaxSubCode(string generalId, int languageId)
        {
            return _repo.GetMaxSubCode(generalId, languageId);
        }


      
    }
}