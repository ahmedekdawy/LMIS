using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class JobList : System.Web.UI.Page
    {
        public const string PageCode = "F004";
        private static readonly IJobManager Mgr = BllFactory.Singleton.Job;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List(string langCode)
        {
            var mr = Mgr.ListByOrgContact(Utils.LoggedUser, null, (int)Utils.GetLanguage(langCode), true);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Delete(long id, string reason)
        {
            var mr = Mgr.Delete(Utils.LoggedUser, id, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object DetailApplicants(long id, string langCode)
        {
            var mr = Mgr.DetailApplicants(Utils.LoggedUser, id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }
        [WebMethod]
        public static object ChangeApplicantStatus(long id, int status)
        {
            var mr = Mgr.ChangeApplicantStatus(id, status);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}