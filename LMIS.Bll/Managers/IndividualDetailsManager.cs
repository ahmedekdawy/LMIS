using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace LMIS.Bll.Managers
{

    public class IndividualDetailsManager : IindividualDetailsManager
    {
        private static readonly IindividualDetailsRepository Repo = DalFactory.Singleton.IndividualDetails;
        private static bool IsAuthorized(UserInfo user, bool asAnApplicant = false)
        {
            if (user == null) return false;
            if (user.PortalUserId < 1) return false;
            if (string.IsNullOrWhiteSpace(user.UserId)) return false;
           // if (!user.IsApproved) return false;

            if (asAnApplicant)
            {
                if (!user.IsIndividual) return false;
            }
            else
            {
                if (!user.IsIndividual) return false;
                // if (!user.IsTrainingProvider) return false;
            }

            return true;
        }
        public ModelResponse SkillsList(UserInfo user, int langId = 1)
        {
            List<SkillsInformationVm.TrainingSkill> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual) return new ModelResponse(101);

                }
                 ds = Repo.SkillsList(user, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse PostNewSkills(UserInfo user, ref SkillsInformationVm vm, bool validateOnly)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                //Validations
                if (vm.Skills.Count < 1
                    || vm.Skills.Any(s => s.Industry.id.IsNotASubCode() || s.Level.id.IsNotASubCode())
                    || vm.Skills.Any(s => !s.IsNew && s.Skill.id.IsNotASubCode())) return new ModelResponse(1);

                if (!validateOnly)
                {
                    vm.PortalUsersID = (long)user.PortalUserId;
                    Repo.PostNewSkills(user,ref vm);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.Id);
        }
        public ModelResponse CertificationList(UserInfo user)
        {
            List<TrainingandCertificationVm> ds;

            try
            {
                //Authorization
                if (user == null ) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual ) return new ModelResponse(101);

                }
                ds = Repo.CertificationList(user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse TrainingList(UserInfo user)
        {
            List<TrainingandCertificationVm> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual) return new ModelResponse(101);

                }
                ds = Repo.TrainingList(user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse ExperienceList(UserInfo user)
        {
            List<ExperienceInformationVm> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual) return new ModelResponse(101);

                }
                ds = Repo.ExperienceList(user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse EducationList(UserInfo user)
        {
            List<EducationalInformationVm> ds;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual) return new ModelResponse(101);

                }
                ds = Repo.EducationList(user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse GetIndividualsList()
        {
            //List<IndividualRegisterationVm> ds;

            //try
            //{
            //    //Authorization
            //    //if (user == null) return new ModelResponse(101);
            //    //if ( user.IsIndividual || user.OrgContactId == null) return new ModelResponse(101);

            //    ds = Repo.GetIndividualsList();
            //}
            //catch (Exception ex)
            //{
            //    return new ModelResponse(ex);
            //}

            return new ModelResponse(0, null);
        }
        public ModelResponse NewExperienceInformation(UserInfo user, ref ExperienceInformationVm vm, bool validateOnly)
        {
            try
            {
                //Authorization

                //Authorization
                if (user == null) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual) return new ModelResponse(101);

                }

                //Validations
                // if (vm.graduationyear != null) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.JobTitle)) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.TypeofEmployment)) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.StartDate.ToShortDateString())) return new ModelResponse(1);
                if (vm.Name.IsNullOrWhiteSpace()) return new ModelResponse(1);
                if (vm.JobDescription.IsNullOrWhiteSpace()) return new ModelResponse(1);

                if (!validateOnly)
                {
                    Repo.NewExperienceInformation(user, ref vm);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.IndividualExperienceID);
        }
        public ModelResponse PostNewCertificationInformation(UserInfo user, ref TrainingandCertificationVm vm, bool validateOnly)
        {
            try
            {
                //Authorization

                //Authorization
                if (user == null) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual) return new ModelResponse(101);

                }

                //Validations
                // if (vm.graduationyear != null) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.CertificationValidUntil.ToShortDateString())) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.CertificationIssueDate.ToShortDateString())) return new ModelResponse(1);

                if (vm.CertificationName.IsNullOrWhiteSpace()) return new ModelResponse(1);

                if (!validateOnly)
                {
                    Repo.PostNewCertificationInformation(user, ref vm);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.IndividualCertificationID);
        }
        public ModelResponse PostNewTrainingInformation(UserInfo user, ref TrainingandCertificationVm vm, bool validateOnly)
        {
            try
            {
                //Authorization

                //Authorization
                if (user == null) return new ModelResponse(101);
                if (user != null)
                {
                    if ( !user.IsIndividual) return new ModelResponse(101);

                }

                //Validations
                // if (vm.graduationyear != null) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.StartDate.ToShortDateString())) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.EndDate.ToShortDateString())) return new ModelResponse(1);

                if (vm.trainingname.IsNullOrWhiteSpace()) return new ModelResponse(1);
                if (vm.TrainingProvider.IsNullOrWhiteSpace()) return new ModelResponse(1);

                if (!validateOnly)
                {
                    Repo.PostNewTrainingInformation(user, ref vm);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.IndividualTrainingID);
        }
        public ModelResponse NewEducationInformation(UserInfo user, ref EducationalInformationVm vm, bool validateOnly)
        {
            try
            {
                //Authorization

                //Authorization
                if (user == null) return new ModelResponse(101);
                //if (user != null)
                //{
                //    if ( !user.IsIndividual) return new ModelResponse(101);

                //}


                //Validations
                // if (vm.graduationyear != null) return new ModelResponse(1);
                //if (string.IsNullOrWhiteSpace(vm.Degree) ) return new ModelResponse(1);

               // if (string.IsNullOrWhiteSpace(vm.Grade)) return new ModelResponse(1);
               // if (vm.Name.IsNullOrWhiteSpace()) return new ModelResponse(1);

                if (!validateOnly)
                {
                    Repo.PostNewEducationInformation(ref vm, user);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.IndividualEducationlevelID);
        }
        public ModelResponse GetCertificateInformation(UserInfo user, long id, int languageid)
        {
            TrainingandCertificationVm ds;
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                ds = Repo.GetCertificateInformation(id, user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse GetTrainingInformation(UserInfo user, long id, int languageid)
        {
            TrainingandCertificationVm ds;
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                ds = Repo.GetTrainingInformation(id, user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse GetExperienceInformation(UserInfo user, long id, int languageid)
        {
            ExperienceInformationVm ds;
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                ds = Repo.GetExperienceInformation(id, user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse GetPersonalInformation(UserInfo user)
        {
            IndividualRegisterationVm ds;
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                ds = Repo.GetPersonalInformation(user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse GetEducationInformation(UserInfo user, long id, int languageid)
        {
            EducationalInformationVm ds;
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                ds = Repo.GetEducationInformation(id, user);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }
        public ModelResponse CertificateDelete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                Repo.CertificateDelete(id, user, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
        public ModelResponse TrainingeDelete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                Repo.TrainingeDelete(id, user, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
        public ModelResponse ExperienceDelete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                Repo.ExperienceDelete(id, user, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
        public ModelResponse EducationDelete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                Repo.EducationDelete(id, user, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
        public ModelResponse SkillDelete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);

                Repo.SkillDelete(user,id , reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
        public ModelResponse PostInformationPerson(UserInfo user, ref IndividualRegisterationVm vm, string fileFolder, bool validateOnly)
        {
            try
            {
                //Authorization
                if (user != null)
                {
                    if (string.IsNullOrWhiteSpace(user.UserId)) return new ModelResponse(101);
                    //if ( !user.IsIndividual) return new ModelResponse(101);
                }

                //Validations
                if (vm.FirstName.IsNullOrWhiteSpace()) return new ModelResponse(1);
                if (vm.LastName.IsNullOrWhiteSpace()) return new ModelResponse(1);

                if (string.IsNullOrWhiteSpace(vm.Email)) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.Gender)) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.BirthDate.ToShortDateString())) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.Militarystatus)) return new ModelResponse(1);
                if (string.IsNullOrWhiteSpace(vm.Nationality)) return new ModelResponse(1);
                if (!validateOnly)
                {
                    //Verify File Path
                  //  if (string.IsNullOrWhiteSpace(fileFolder)) return new ModelResponse(102);
                    // if (string.IsNullOrWhiteSpace(vm.FilePath)) return new ModelResponse(102);
                    //    if (!File.Exists(Path.Combine(fileFolder, vm.FilePath))) return new ModelResponse(102);

                    //Save to DB

                    //vm.Approval = Approval.Pending;
                    // Repo.PostPersonalInformation(ref vm, (user != null) ? user.UserId : userid);
                    Repo.PostPersonalInformation(ref vm, user);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.RegisterationId);
        }
    }
}
