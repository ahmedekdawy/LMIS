using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class Faq : System.Web.UI.Page
    {
        private static readonly IFaqManager Mgr = BllFactory.Singleton.Faq;
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        [WebMethod]
        public static object Get(decimal id=0)
        {
            var mr = Mgr.Get((int)Utils.GetLanguage(), id);
            return Utils.ServiceResponse(mr, null);

        }
    }
}