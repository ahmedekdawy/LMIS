using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.BackEnd
{
    public partial class ReviewRequests : System.Web.UI.Page
    {
        public const string PageCode = "F033";
        private static readonly IRequestLogManager Mgr = BllFactory.Singleton.RequestLog;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object ListRequests(string langCode, bool forAllAdmins)
        {
            var ui = Utils.LoggedUser;
            var mr = Mgr.ListPendingByAdminId(ui, (int) Utils.GetLanguage(langCode), forAllAdmins);

            object res = null;

            if (mr.Status == ResponseStatus.Success)
                res = new
                {
                    New = Utils.GetResource(false, "F033_New"),
                    Updated = Utils.GetResource(false, "F033_Updated"),
                    Deleted = Utils.GetResource(false, "F033_Deleted"),
                    IsSuperAdmin = (BllFactory.Singleton.DataService.IsSuperAdmin(ui.UserId)),
                    ObsceneWords = (BllFactory.Singleton.DataService.ListObsceneWords())
                };

            return Utils.ServiceResponse(PageCode, mr, res);
        }

        [WebMethod]
        public static object ListAdmins(bool availableOnly, bool excludeSuper)
        {
            object data = null;

            if (Utils.LoggedUser != null)
                if (BllFactory.Singleton.DataService.IsSuperAdmin(Utils.TestUser(2).UserId))
                    data = BllFactory.Singleton.DataService.ListAdmins(availableOnly, excludeSuper);

            return Utils.ServiceResponse(data);
        }

        [WebMethod]
        public static object Reassign(long id, string newAdminId)
        {
            var mr = Mgr.ReassignAdmin(Utils.LoggedUser, id, newAdminId);
            return Utils.ServiceResponse(mr);
        }

    }
}