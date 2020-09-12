using LMIS.Portal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;

namespace LMIS.Portal.BackEnd
{
    public partial class ConfigCenter : System.Web.UI.Page
    {
        private static readonly IConfigCenterManager MgrConfigCenter = BllFactory.Singleton.ConfigCenterManager;
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
        public static  object  Get()
        {
            if (Utils.CheckPermission(1, 7, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            var mr = MgrConfigCenter.GetValue(null, -1, -1, -1);

            return Utils.ServiceResponse(mr, null);
        }
        [HttpPost]
        [WebMethod]
        public static object  Post(Dictionary<string, string> obj)
        {
            if (Utils.CheckPermission(2, 7, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
         return    MgrConfigCenter.Post(obj);
              
            
        }
    }

}