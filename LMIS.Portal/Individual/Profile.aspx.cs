using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Services;

namespace LMIS.Portal.Individual
{
    public partial class Profile : System.Web.UI.Page
    {
        public const string PageCode = "F038";
        public const string LoginUrl = "login";

        private static readonly IIndividualManager IndMgr = BllFactory.Singleton.IndividualManager;

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
        public static object GetProfile(long id, string langCode)
        {
            var mr = IndMgr.GetProfile(Utils.LoggedUser, id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
       
      public static object Approve(long reqKey, long id, bool approved, string reason, Dictionary<string, object> newValues)
        {
            var mr = IndMgr.Approve(Utils.LoggedUser, reqKey, id, approved, reason, newValues);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}