using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.BackEnd
{
    public partial class DescriptiveJobBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
            }
        }
        [WebMethod]
        public static object Get()
        {

            if (Utils.CheckPermission(1, 20, Utils.LoggedUser.Roles) < 1)
            {

                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            var path = HttpContext.Current.Server.MapPath(@"~\Uploads\DescriptiveJob");
            string[] filePaths = Directory.GetFiles(path);
            return Utils.ServiceResponse(null, filePaths);

        }
      
    }
}