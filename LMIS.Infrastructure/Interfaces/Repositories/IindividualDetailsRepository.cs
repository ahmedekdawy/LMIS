

using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace LMIS.Infrastructure.Interfaces.Repositories
{
    public interface IindividualDetailsRepository
    {
        UserInfo GetUserInfo(string UserId);
        long PostNewSkills(UserInfo user,ref SkillsInformationVm vm);
        List<ExperienceInformationVm> ExperienceList(UserInfo user);
        List<TrainingandCertificationVm> TrainingList(UserInfo user);
        List<TrainingandCertificationVm> CertificationList(UserInfo user);
        List<SkillsInformationVm.TrainingSkill> SkillsList(UserInfo user, int langId = 1);
        List<EducationalInformationVm> EducationList(UserInfo user);
        List<IndividualRegisterationVm> GetIndividualsList();
        long PostPersonalInformation(ref IndividualRegisterationVm r, UserInfo user);
        long PostNewEducationInformation(ref EducationalInformationVm r,UserInfo user);
        void EducationDelete(long id, UserInfo user, string reason);
        EducationalInformationVm GetEducationInformation(long id, UserInfo user);
        IndividualRegisterationVm GetPersonalInformation(UserInfo user);
       long NewExperienceInformation(UserInfo user ,ref ExperienceInformationVm r);
       ExperienceInformationVm GetExperienceInformation(long id, UserInfo user);
       TrainingandCertificationVm GetTrainingInformation(long id, UserInfo user);
       TrainingandCertificationVm GetCertificateInformation(long id, UserInfo user);
       void ExperienceDelete(long id, UserInfo user, string reason);
       void TrainingeDelete(long id, UserInfo user, string reason);
       void CertificateDelete(long id, UserInfo user, string reason);
       void SkillDelete(UserInfo user, long id, string reason);
       long PostNewTrainingInformation(UserInfo user, ref TrainingandCertificationVm r);
       long PostNewCertificationInformation(UserInfo user, ref TrainingandCertificationVm r);
        
        
        
    }
}
