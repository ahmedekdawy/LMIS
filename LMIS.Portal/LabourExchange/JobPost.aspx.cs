using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class JobPost : System.Web.UI.Page
    {
        public const string PageCode = "F004";
        private static readonly IJobManager Mgr = BllFactory.Singleton.Job;

        void Page_PreInit(Object sender, EventArgs e)
        {
            var mode = Request.QueryString["m"];

            if (!string.IsNullOrWhiteSpace(mode) && mode.ToLower().Trim() == "r")
                MasterPageFile = "~/MasterPages/Review.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object Post(JobVm data, bool validateOnly)
        {
            var mr = Mgr.Post(Utils.LoggedUser, ref data, Utils.UploadFolder, validateOnly);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Get(long id, string langCode)
        {
            var mr = Mgr.ListByOrgContact(Utils.LoggedUser, id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
       public static object Approve(long reqKey, long id, bool approved, string reason, Dictionary<string, object> newValues)
        {
            var mr = Mgr.Approve(Utils.LoggedUser, reqKey, id, approved, reason, newValues);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}