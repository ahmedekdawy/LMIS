using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class DescriptiveJob : System.Web.UI.Page
    {
        [WebMethod]
        public static object Get()
        {

           
            var path = HttpContext.Current.Server.MapPath(@"~\Uploads\DescriptiveJob");
            string[] filePaths = Directory.GetFiles(path);
            return Utils.ServiceResponse(null, filePaths);

        }
    }
}