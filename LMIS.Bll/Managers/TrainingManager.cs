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
    public class TrainingManager : ITrainingManager
    {
        private static readonly ITrainingRepository Repo = DalFactory.Singleton.Training;

        private static bool IsAuthorized(UserInfo user, bool asAnApplicant = false)
        {
            if (user == null) return false;
            if (user.PortalUserId < 1) return false;
            if (string.IsNullOrWhiteSpace(user.UserId)) return false;
            if (!user.IsApproved) return false;

            if (asAnApplicant)
            {
                if(!user.IsIndividual) return false;
            }
            else
            {
                if (user.IsIndividual || user.OrgContactId == null) return false;
                if (!user.IsTrainingProvider) return false;
            }

            return true;
        }

        public ModelResponse ListByOrgContact(UserInfo user, long? id = null, int langId = 1, bool brief = false)
        {
            List<TrainingVm> ds;
            long contactId = -1;

            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (brief || !DalFactory.Singleton.DataService.IsAdmin(user.UserId))
                {
                    if (!IsAuthorized(user)) return new ModelResponse(101);
                    contactId = (long)user.OrgContactId.GetValueOrDefault();
                }

                if (brief)
                    ds = Repo.BriefByOrgContact(contactId, id, langId);
                else
                    ds = Repo.ListByOrgContact(contactId, id, langId);

                if (id > 0 && ds.Count != 1) return new ModelResponse(101);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return id > 0 ? new ModelResponse(0, ds[0]) : new ModelResponse(0, ds);
        }

        public ModelResponse DetailApplicants(UserInfo user, long id, int langId = 1)
        {
            List<CodeSet> ds;

            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);
                Debug.Assert(user.OrgContactId != null);

                ds = Repo.DetailApplicants((long)user.OrgContactId, id, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse Post(UserInfo user, ref TrainingVm vm, string fileFolder, bool validateOnly)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);
                Debug.Assert(user.OrgContactId != null);
                if (vm.Id > 0 && !Repo.IsByOrgContact(vm.Id, (long)user.OrgContactId)) return new ModelResponse(101);

                //Validations
                if ((vm.Title.IsNotASubCode() && vm.NewTitle.HasNoValue())
                    || vm.Country.IsNotASubCode() || vm.City.IsNotASubCode()
                    || vm.Address.HasNoValue() || vm.Duration <= 0
                    || vm.Occurrence.Count < 1 || vm.Occurrence.Any(o => o.IsNotASubCode())
                    || vm.TimeZone.IsNotASubCode() || vm.Skills.Count < 1
                    || vm.Skills.Any(s => s.Industry.id.IsNotASubCode() || s.Level.id.IsNotASubCode())
                    || vm.Skills.Any(s => !s.IsNew && s.Skill.id.IsNotASubCode())) return new ModelResponse(1);
                if ((vm.Id < 1 && vm.StartDate < DateTime.Today) || vm.EndDate < vm.StartDate) return new ModelResponse(2);
                if (vm.TimeTo < vm.TimeFrom) return new ModelResponse(3);

                if (!validateOnly)
                {
                    //Validation after optional file upload
                    if (vm.Description.HasNoValue() && vm.FileName.HasNoValue()) return new ModelResponse(1);

                    //Verify File Path
                    if (fileFolder.HasNoValue()) return new ModelResponse(102);
                    if (!vm.FileName.HasNoValue() && !File.Exists(Path.Combine(fileFolder, vm.FileName))) return new ModelResponse(102);

                    //Save to DB
                    if (vm.Title.IsNotASubCode()) vm.Title = "";
                    else vm.NewTitle = new GlobalString();

                    if (vm.Seats <= 0) vm.Seats = 0;
                    if (vm.Cost <= 0) vm.Cost = 0;

                    vm.Approval = Approval.Pending;
                    vm.PortalUserId = (long)user.PortalUserId;
                    vm.ContactId = (long)user.OrgContactId;
                    Repo.Post(ref vm, user.UserId);
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, vm.Id);
        }

        public ModelResponse Delete(UserInfo user, long id, string reason)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);
                Debug.Assert(user.OrgContactId != null);
                if (!Repo.IsByOrgContact(id, (long)user.OrgContactId)) return new ModelResponse(101);

                Repo.Delete(id, user.UserId, reason);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse SetTrainingList(UserInfo user, string fileFolder, string serverFilePath)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);
                Debug.Assert(user.OrgContactId != null);

                //Verify File Path
                if (!serverFilePath.HasNoValue() && !File.Exists(Path.Combine(fileFolder, serverFilePath))) return new ModelResponse(102);

                if (serverFilePath.HasNoValue()) serverFilePath = null;
                Repo.SetTrainingList((long)user.PortalUserId, serverFilePath);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse GetTrainingList(UserInfo user)
        {
            string filePath;

            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);
                Debug.Assert(user.OrgContactId != null);

                filePath = Repo.GetTrainingList((long)user.PortalUserId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, filePath);
        }

        public ModelResponse Search(string keywords, int langId = 1)
        {
            List<TrainingVm> ds;

            try
            {
                //No Authorization (Public)
                ds = Repo.Search(keywords, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse View(UserInfo user, long id, int langId = 1)
        {
            TrainingVm ret;

            try
            {
                //No Authorization (Public)
                ret = user == null ? Repo.View(id, null, langId) : Repo.View(id, (long)user.PortalUserId, langId);

                if (ret != null)
                {
                    ret.Extras["CanApply"] = user != null && user.IsIndividual && !(bool)ret.Extras["Applied"];
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ret);
        }

        public ModelResponse Apply(UserInfo user, long id)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user, true)) return new ModelResponse(101);

                //Record Application
                var ret = Repo.Apply(id, (long)user.PortalUserId);
                if (!ret) return new ModelResponse(1);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long trainingId, bool approved, string reason, Dictionary<string, object> newValues)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                Repo.Approve(user.UserId, reqKey, trainingId, approved, reason, newValues);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }
    }
}