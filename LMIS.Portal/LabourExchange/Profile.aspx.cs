using LMIS.Infrastructure.Data.Entities.Organization;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class Profile : System.Web.UI.Page
    {
        public const string PageCode = "F037";
        public const string LoginUrl = "login";

        private static readonly IOrganizationManager OrgMgr = BllFactory.Singleton.Organization;

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
            var mr = OrgMgr.GetProfile(Utils.LoggedUser, id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Approve(long reqKey, long id, bool approved, string reason)
        {
            var mr = OrgMgr.Approve(Utils.LoggedUser, reqKey, id, approved, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Update(OrganizationVM organizationObject, bool validateOnly)
        {
            try
            {
                organizationObject.PortalUsersID = Utils.LoggedUser.PortalUserId;

                var result = OrgMgr.Update(ref organizationObject, Utils.LoggedUser.UserId);
                return Utils.ServiceResponse(PageCode, result);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}