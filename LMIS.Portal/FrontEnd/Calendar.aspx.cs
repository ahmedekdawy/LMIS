using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class Calendar : System.Web.UI.Page
    {
        public const string PageCode = "F006";
        private static readonly IEventManager Mgr = BllFactory.Singleton.Event;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object List()
        {
            
            var mr = Mgr.List((int )Utils.GetLanguage());
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}