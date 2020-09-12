using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace LMIS.Portal.Registration
{
    public partial class Profile : System.Web.UI.Page
    {
        public const string PageCode = "R002";

        private static readonly IPortalUsersManager PUserMgr = BllFactory.Singleton.PortalUsersManager;
        private static readonly IIndManager IndMgr = BllFactory.Singleton.Ind;
        private static readonly IOrgManager OrgMgr = BllFactory.Singleton.Org;

        private bool _reviewMode;
        private long _portalUserId;

        void Page_PreInit(Object sender, EventArgs e)
        {
            var key = Request.QueryString["k"];
            if (!long.TryParse(key, out _portalUserId)) return;

            var mode = Request.QueryString["m"];
            if (string.IsNullOrWhiteSpace(mode) || mode.ToLower().Trim() != "r") return;

            _reviewMode = true;
            MasterPageFile = "~/MasterPages/Review.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string profileType, viewOnly;

            if (_reviewMode)
            {
                var userCat = PUserMgr.GetUserCat(_portalUserId);
                if (userCat == UserCat.Unknown) Response.Redirect("/home");

                profileType = userCat == UserCat.Individual ? "ind" : "org";
                viewOnly = "yes";
            }
            else
            {
                var userClass = PUserMgr.GetUserClass(Utils.LoggedUser);
                if (userClass == UserClass.Unknown) Response.Redirect("/home");

                profileType = userClass == UserClass.Individual ? "ind" : "org";
                viewOnly = userClass == UserClass.OrgContact ? "yes" : "";
            }

            Response.Cookies.Add(new HttpCookie("ProfileType", profileType));
            Response.Cookies.Add(new HttpCookie("ViewOnly", viewOnly));
            Title = Utils.GetPageTitle(PageCode + "_" + profileType);
        }

        [WebMethod]
        public static object OrgLoad(long? portalUserId)
        {
            var puid = portalUserId == null ? (long)Utils.LoggedUser.PortalUserId : portalUserId.GetValueOrDefault();
            var mr = OrgMgr.LoadProfile(Utils.LoggedUser, puid);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object OrgUpdate(OrgVm data)
        {
            var mr = OrgMgr.UpdateProfile(Utils.LoggedUser, ref data, Utils.UploadFolder);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object OrgApprove(long reqKey, long id, bool approved, string reason, Dictionary<string, object> newValues)
        {
            var mr = OrgMgr.Approve(Utils.LoggedUser, reqKey, id, approved, reason, newValues);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}