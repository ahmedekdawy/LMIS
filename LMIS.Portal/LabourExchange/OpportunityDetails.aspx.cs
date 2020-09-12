using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class OpportunityDetails : System.Web.UI.Page
    {
        public const string PageCode = "F005";
        private static readonly IOpportunityManager Mgr = BllFactory.Singleton.Opportunity;

        [WebMethod]
        public static object GetOrgContactOpportunityById(long id)
        {
            var mr = Mgr.Get(id);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}