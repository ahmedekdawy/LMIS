using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.FrontEnd
{
    public partial class Unions : System.Web.UI.Page
    {
        public const string PageCode = "B002";
        private static readonly IUnionManager Mgr = BllFactory.Singleton.Union;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List()
        {
            var mr = Mgr.List();
            var govLookup = BllFactory.Singleton.SubCode.List("003");
            return Utils.ServiceResponse(PageCode, mr, govLookup);
        }

        
    }
}