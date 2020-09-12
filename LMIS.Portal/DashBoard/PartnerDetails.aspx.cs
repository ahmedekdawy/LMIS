using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.DashBoard
{
    public partial class PartnerDetails : System.Web.UI.Page
    {
        private static readonly IPartnerManager Mgr = BllFactory.Singleton.PartnersManager;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetPartners()
        {
            var result = Mgr.GetActivePartners((int)Utils.GetLanguage(), decimal.Parse(HttpContext.Current.Request.QueryString["PartnerID"]));
            return result;

        }
    }
}