using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class PastAchievements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetNews()
        {
            var result = BllFactory.Singleton.NewsManager.GetNews("IsAchievement", (int)Utils.GetLanguage());
            return result;

        }
    }
}