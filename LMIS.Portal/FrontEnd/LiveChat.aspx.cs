using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class LiveChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object   GetChatTime()
        {
            var result = new { 
                  LiveChatDays = BllFactory.Singleton.ConfigCenterManager.GetValue("LiveChatDays", 1,-1, 5)[0].Value
                , LiveChatFrom = BllFactory.Singleton.ConfigCenterManager.GetValue("LiveChatFrom", 1, -1, 5)[0].Value
                , LiveChatTo = BllFactory.Singleton.ConfigCenterManager.GetValue("LiveChatTo", 1, -1, 5)[0].Value
            };
            return result; 
        }
    }
}