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
    public class JobManager : IJobManager
    {
        private static readonly IJobRepository Repo = DalFactory.Singleton.Job;

        private static bool IsAuthorized(UserInfo user, bool asAnApplicant = false)
        {
            if (user == null) return false;
            if (user.PortalUserId < 1) return false;
            if (string.IsNullOrWhiteSpace(user.UserId)) return false;
            if (!user.IsApproved) return false;

            if (asAnApplicant)
            {
                if (!user.IsIndividual) return false;
            }
            else
            {
                if (user.IsIndividual || user.OrgContactId == null) return false;
                if (!user.IsEmployer && !user.IsTrainingProvider) return false;
            }

            return true;
        }

        public ModelResponse ListByOrgContact(UserInfo user, long? id = null, int langId = 1, bool brief = false)
        {
            List<JobVm> ds;
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
            List<Dictionary<string, object>> ds;

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

        public ModelResponse ChangeApplicantStatus(long id, int status)
        {
            try
            {
                if (status < 0 || status > 5) status = 1;

                Repo.ChangeApplicantStatus(id, status);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public ModelResponse Post(UserInfo user, ref JobVm vm, string fileFolder, bool validateOnly)
        {
            try
            {
                //Authorization
                if (!IsAuthorized(user)) return new ModelResponse(101);
                Debug.Assert(user.OrgContactId != null);
                if (vm.JobId > 0 && !Repo.IsByOrgContact(vm.JobId, (long)user.OrgContactId)) return new ModelResponse(101);

                //Validations
                if ((vm.Title.IsNotASubCode() && vm.NewTitle.HasNoValue())
                    || vm.Description.HasNoValue()
                    || vm.ExpFrom < 0 || vm.ExpTo < 0 || vm.Vacancies < 1
                    || vm.EmploymentType.IsNotASubCode()
                    || vm.EdLevel.IsNotASubCode()
                    || vm.EdCert.HasNoValue()
                    || vm.Gender.IsNotASubCode()
                    || vm.Country.IsNotASubCode()
                    || vm.City.IsNotASubCode()
                    || vm.Skills.Count < 1
                    || vm.Skills.Any(s => s.Industry.id.IsNotASubCode() || s.Level.id.IsNotASubCode())
                    || vm.Skills.Any(s => !s.IsNew && s.Skill.id.IsNotASubCode())
                    || (vm.PerMonth <= 0 && vm.PerHour <= 0)
                    || vm.Currency.IsNotASubCode()
                    || vm.MedConditions.Count < 1 || vm.MedConditions.Any(x => x.IsNotASubCode())) return new ModelResponse(1);

                if ((vm.JobId < 1 && vm.StartDate < DateTime.Today) || vm.EndDate < vm.StartDate) return new ModelResponse(2);

                if (!validateOnly)
                {
                    //Verify File Path
                    if (fileFolder.HasNoValue()) return new ModelResponse(102);
                    if (!vm.FileName.HasNoValue() && !File.Exists(Path.Combine(fileFolder, vm.FileName))) return new ModelResponse(102);

                    //Save to DB
                    if (vm.Title.IsNotASubCode()) vm.Title = "";
                    else vm.NewTitle = new GlobalString();

                    if (vm.ExpTo < vm.ExpFrom)
                    {
                        var tmp = vm.ExpTo;
                        vm.ExpTo = vm.ExpFrom;
                        vm.ExpFrom = tmp;
                    }
                    if (vm.PerMonth < 0) vm.PerMonth = 0;
                    if (vm.PerHour < 0) vm.PerHour = 0;

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

            return new ModelResponse(0, vm.JobId);
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

        public ModelResponse Search(string keywords, int langId = 1)
        {
            List<JobVm> ds;

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
            JobVm ret;

            try
            {
                //No Authorization (Public)
                ret = user == null ? Repo.View(id, null, langId) : Repo.View(id, (long)user.PortalUserId, langId);

                if (ret != null)
                {
                    ret.PerMonth = 0;
                    ret.PerHour = 0;
                    ret.Currency = "";
                    ret.Extras["CanApply"] = user != null && user.IsIndividual && !(bool)ret.Extras["Applied"];
                }
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ret);
        }

        public ModelResponse ApplicationRequirements(long id, int langId = 1)
        {
            Dictionary<string, object> ds;

            try
            {
                //No Authorization (Public)
                ds = Repo.ApplicationRequirements(id, langId);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, ds);
        }

        public ModelResponse Apply(UserInfo user, long id, string fileFolder, string appForm, List<CodeSet> attachments)
        {
            try
            {
                if (attachments == null) attachments = new List<CodeSet>();

                //Authorization
                if (!IsAuthorized(user, true)) return new ModelResponse(101);
                
                //Verify File Paths
                if (fileFolder.HasNoValue()) return new ModelResponse(102);
                if (!appForm.HasNoValue() && !File.Exists(Path.Combine(fileFolder, appForm))) return new ModelResponse(102);
                if (attachments.Any(f => !f.desc.HasNoValue() && !File.Exists(Path.Combine(fileFolder, f.desc)))) return new ModelResponse(102);

                //Validations
                var req = Repo.ApplicationRequirements(id);
                if(req == null) return new ModelResponse(101);
                if (!((String)req["AppTemplate"]).HasNoValue() && appForm.HasNoValue()) return new ModelResponse(1);
                if (((List<CodeSet>)req["AdditionalDocs"]).Any(f => attachments.All(a => a.id != f.id || a.desc.HasNoValue()))) return new ModelResponse(1);

                //Record Application
                var ret = Repo.Apply(id, (long)user.PortalUserId, appForm, attachments);
                if (!ret) return new ModelResponse(2);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0, true);
        }

        public ModelResponse Approve(UserInfo user, long reqKey, long jobId, bool approved, string reason, Dictionary<string, object> newValues)
        {
            try
            {
                //Authorization
                if (user == null) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsAdmin(user.UserId)) return new ModelResponse(101);
                if (!DalFactory.Singleton.DataService.IsSuperAdmin(user.UserId))
                    if (DalFactory.Singleton.RequestLog.AssignedTo(reqKey) != user.UserId) return new ModelResponse(101);

                Repo.Approve(user.UserId, reqKey, jobId, approved, reason, newValues);
            }
            catch (Exception ex)
            {
                return new ModelResponse(ex);
            }

            return new ModelResponse(0);
        }

        public List<JobsCountVm> JobsPerYear(bool started, bool jobStatus, int langId)
        {
            return Repo.JobsPerYear(  started,  jobStatus,  langId );
        }

        public decimal JobApplied()
        {
            return Repo.JobApplied();
        }
    }
}