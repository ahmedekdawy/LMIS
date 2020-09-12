using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.FrontEnd
{
    public partial class Offices : System.Web.UI.Page
    {
        public const string PageCode = "B001";
        private static readonly IOfficeManager Mgr = BllFactory.Singleton.Office;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List()
        {
            var mr = Mgr.List();
            return Utils.ServiceResponse(PageCode, mr);
        }

      
    }
}