
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;

namespace LMIS.Infrastructure.Interfaces.Managers
{
    public interface IindividualDetailsManager
    {
       // UserInfo GetUserInfo(string UserId);
        ModelResponse EducationList(UserInfo user);
        ModelResponse GetIndividualsList();
        ModelResponse PostInformationPerson(UserInfo user, ref IndividualRegisterationVm r, string fileFolder, bool validateOnly);
        ModelResponse NewEducationInformation(UserInfo user, ref EducationalInformationVm r, bool validateOnly);
        ModelResponse EducationDelete(UserInfo user, long id, string reason);
        ModelResponse SkillDelete(UserInfo user, long id, string reason);
        ModelResponse GetEducationInformation(UserInfo user, long id, int languageid);
        ModelResponse GetPersonalInformation(UserInfo user);
        ModelResponse NewExperienceInformation(UserInfo user, ref ExperienceInformationVm r, bool validateOnly);
        ModelResponse ExperienceList(UserInfo user);
        ModelResponse GetExperienceInformation(UserInfo user, long id, int languageid);
        ModelResponse GetTrainingInformation(UserInfo user, long id, int languageid);
        ModelResponse GetCertificateInformation(UserInfo user, long id, int languageid);
        ModelResponse ExperienceDelete(UserInfo user, long id, string reason);
        ModelResponse TrainingeDelete(UserInfo user, long id, string reason);
        ModelResponse CertificateDelete(UserInfo user, long id, string reason);
        ModelResponse PostNewTrainingInformation(UserInfo user, ref TrainingandCertificationVm r, bool validateOnly);
        ModelResponse PostNewCertificationInformation(UserInfo user, ref TrainingandCertificationVm r, bool validateOnly);
        ModelResponse TrainingList(UserInfo user);
        ModelResponse CertificationList(UserInfo user);
        ModelResponse PostNewSkills(UserInfo user, ref SkillsInformationVm vm, bool validateOnly);
        ModelResponse SkillsList(UserInfo user, int langId = 1);
    }   
}
