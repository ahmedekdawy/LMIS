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
    public partial class NewsItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetNewsByID(decimal newsId)
        {

            var result = BllFactory.Singleton.NewsManager.GetNewsByID(newsId, (int)Utils.GetLanguage());
                return result; 
    

        }
    }
}