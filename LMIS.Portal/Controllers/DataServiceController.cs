using LMIS.Portal.Helpers;
using LMIS.Infrastructure.Data.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNet.Identity;

namespace LMIS.Portal.Controllers
{
    [RoutePrefix("api/DataService")]
    public class DataServiceController : ApiController
    {
        [HttpPost]
        [Route("{fid:length(4)}/{langCode?}")]
        public HttpResponseMessage DataServiceQuery(string fid, [FromBody]object[] args, string langCode = "")
        {
            fid = fid.ToLower().Trim();

            if (fid.StartsWith("ow")) return QueryOwin(fid, args);
            if (fid == "cnfg") return QueryConfigCenter(args, langCode);

            return QueryDataService(fid, args, langCode);
        }

        [HttpPost]
        [Route("SubCodes/{generalId:length(3)}/{langCode?}")]
        public HttpResponseMessage ListSubCodes(string generalId, string langCode = "")
        {
            return QuerySubCodes(0, generalId, langCode);
        }
        [HttpPost]
        [Route("SubCodes/{generalId:length(3)}/{execludedIds}/{langCode?}")]
        public HttpResponseMessage ListSubCodes(string generalId, string execludedIds, string langCode = "")
        {
            return QuerySubCodes(0, generalId, execludedIds, langCode,"");
        }
        [HttpPost]
        [Route("SubCodes/group/{generalId:length(3)}/{langCode?}")]
        public HttpResponseMessage GroupSubCodes(string generalId, string langCode = "")
        {
            return QuerySubCodes(1, generalId, langCode);
        }

        [HttpPost]
        [Route("SubCodes/byparent/{generalId:length(3)}/{parentSubCodeId:length(8)}/{langCode?}")]
        public HttpResponseMessage FilterSubCodesByParentSubCodeId(string generalId, string parentSubCodeId, string langCode = "")
        {
            return QuerySubCodes(2, generalId, langCode, parentSubCodeId);
        }

        private HttpResponseMessage QueryConfigCenter(IEnumerable<object> args, string langCode)
        {
            try
            {
                var keys = args.Cast<string>().ToList();
                var langId = (int)Utils.GetLanguage(langCode);
                var mgr = BllFactory.Singleton.ConfigCenterManager;

                var ret = mgr.List(keys, langId);

                if (keys.Count == 1 && ret.Count == 1)
                    return Request.CreateResponse(HttpStatusCode.OK, ret.Values.First());

                return ret.Count < 1
                    ? Request.CreateResponse(HttpStatusCode.NoContent)
                    : Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private HttpResponseMessage QueryDataService(string fid, IReadOnlyList<object> args, string langCode)
        {
            try
            {
                object ret = new List<object>(); var retCount = 0;
                var langId = (int)Utils.GetLanguage(langCode);
                var mgr = BllFactory.Singleton.DataService;

                switch (fid)
                {
                    case "f001":
                        ret = mgr.ListIndustriesHavingSkills(langId);
                        break;

                    case "f002":
                        ret = mgr.GroupSkillsForIndustry((string)args[0], langId);
                        break;

                    case "f003":
                        ret = mgr.ListObsceneWords();
                        break;

                    case "f004":
                        ret = mgr.FillSkillsByIndustryAndLevel(args.Cast<string>().ToList());
                        retCount = ((Dictionary<string, List<CodeSet>>) ret).Count;
                        break;
                }

                if (fid != "f004") retCount = ((List<object>) ret).Count;

                return retCount < 1
                    ? Request.CreateResponse(HttpStatusCode.NoContent)
                    : Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        private HttpResponseMessage QuerySubCodes(int action, string filterId, string langCode, string p1 = "")
        {
            try
            {
                var lang = Utils.GetLanguage(langCode);
                var mgr = BllFactory.Singleton.SubCode;
                var ret = new List<object>();

                switch (action)
                {
                    case 0:
                        foreach (var r in mgr.List(filterId))
                            ret.Add(new { id = r.Id, desc = r.ToLocalString(lang, true).T });

                        break;

                    case 1:
                        foreach (var group in mgr.Group(filterId))
                        {
                            var ds = new List<object>();

                            foreach (var r in group.Subset)
                                ds.Add(new { id = r.Id, desc = r.ToLocalString(lang, true).T });

                            ret.Add(new { id = group.Id, desc = group.Desc, subset = ds });
                        }

                        break;

                    case 2:
                        foreach (var r in mgr.FilterByParentSubCodeId(filterId, p1))
                            ret.Add(new { id = r.Id, desc = r.ToLocalString(lang, true).T });

                        break;
                }

                if (ret.Count < 1) return Request.CreateResponse(HttpStatusCode.NoContent);

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        private HttpResponseMessage QuerySubCodes(int action, string filterId, string execludedIds, string langCode, string p1 = "")
        {
            try
            {
                var lang = Utils.GetLanguage(langCode);
                var mgr = BllFactory.Singleton.SubCode;
                var ret = new List<object>();

                switch (action)
                {
                    case 0:
                        foreach (var r in mgr.List(filterId, execludedIds))
                            ret.Add(new { id = r.Id, desc = r.ToLocalString(lang, true).T });

                        break;

                    case 1:
                        foreach (var group in mgr.Group(filterId))
                        {
                            var ds = new List<object>();

                            foreach (var r in group.Subset)
                                ds.Add(new { id = r.Id, desc = r.ToLocalString(lang, true).T });

                            ret.Add(new { id = group.Id, desc = group.Desc, subset = ds });
                        }

                        break;

                    case 2:
                        foreach (var r in mgr.FilterByParentSubCodeId(filterId, p1))
                            ret.Add(new { id = r.Id, desc = r.ToLocalString(lang, true).T });

                        break;
                }

                if (ret.Count < 1) return Request.CreateResponse(HttpStatusCode.NoContent);

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        private HttpResponseMessage QueryOwin(string fid, IReadOnlyList<object> args)
        {
            try
            {
                object ret = null;
                var mgr = BllFactory.Singleton.User;

                switch (fid)
                {
                    case "ow01": //Is UserName available?
                        ret = mgr.FindByName((string) args[0]) == null;
                        break;
                }

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}