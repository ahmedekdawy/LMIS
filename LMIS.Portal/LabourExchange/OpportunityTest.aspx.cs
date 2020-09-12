using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class OpportunityTest : System.Web.UI.Page
    {
        public const string PageCode = "F000";
        private static readonly IOpportunityManager Mgr = BllFactory.Singleton.Opportunity;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object GetOpportunityList()
        {
            //var mr = Mgr.List();
            //return Utils.ServiceResponse(PageCode, mr);
            return null;
        }

        [WebMethod(true)]
        public static string GetSessionId(string clientMessage)
        {
            return clientMessage + HttpContext.Current.Session.SessionID;
        }
    }
}