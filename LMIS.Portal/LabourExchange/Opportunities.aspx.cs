using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;
using Microsoft.Ajax.Utilities;

namespace LMIS.Portal.LabourExchange
{
    public partial class Opportunities : System.Web.UI.Page
    {
        public const string PageCode = "F005";
        private static readonly IOpportunityManager Mgr = BllFactory.Singleton.Opportunity;

        protected void Page_Load(object sender, EventArgs e)
        {
            var mode = Request.QueryString["m"];
            if (!mode.IsNullOrWhiteSpace()) mode = mode.ToLower().Trim();

            switch (mode)
            {
                case "i":
                    Title = Utils.GetPageTitle("F005_InformalOpps");
                    break;
                case "f":
                    Title = Utils.GetPageTitle("F005_FormalOpps");
                    break;
                default:
                    Title = Utils.GetPageTitle("F005_AllOpp");
                    break;
            }
        }

        [WebMethod]
        public static object GetOpportunityList(bool? informal)
        {
            var mr = Mgr.List(informal);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}