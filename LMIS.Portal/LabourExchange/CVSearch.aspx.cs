using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class CVSearch : System.Web.UI.Page
    {
        public const string PageCode = "F003";
        private static readonly IIndManager Mgr = BllFactory.Singleton.Ind;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object Search(long jobOfferId, string langCode)
        {
            var mr = Mgr.Search(Utils.LoggedUser, jobOfferId, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object List(string langCode)
        {
            var mr = BllFactory.Singleton.Job.ListByOrgContact(Utils.LoggedUser, null, (int)Utils.GetLanguage(langCode), true);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}