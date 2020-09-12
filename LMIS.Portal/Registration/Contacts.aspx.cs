using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.Registration
{
    public partial class Contacts : System.Web.UI.Page
    {
        public const string PageCode = "R003";

        private static readonly IOrgManager OrgMgr = BllFactory.Singleton.Org;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List()
        {
            var mr = OrgMgr.ListOrgContacts(Utils.LoggedUser);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Delete(long contactId)
        {
            var mr = OrgMgr.DeleteOrgContact(Utils.LoggedUser, contactId);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}