using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.BackEnd
{
    public partial class News : System.Web.UI.Page
    {
        private static readonly INewsManager Mgr = BllFactory.Singleton.NewsManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LMIS.Portal.Helpers.Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
        }
        [WebMethod]
        public static object GetNews()
        {
            if (Utils.CheckPermission(1, 27, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            var result = Mgr.GetNews("", (int)Utils.GetLanguage());
            return result;

        }
        [WebMethod]
        public static object Delete(decimal newsId, string deleteReason)
        {
            if (Utils.CheckPermission(4, 27, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            var result = Mgr.Delete(newsId, deleteReason);
            return result;

        }
        [WebMethod]
        public static object Update(NewsVm News)
        {
            if (Utils.CheckPermission(3, 27, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            var result = Mgr.Update(News);
            return result;

        }
        [WebMethod]
        public static object Insert(NewsVm News)
        {
            int result = 0;
            if (Utils.LoggedUser == null)
            {
                return Utils.ServiceResponse("F036", new ModelResponse(101), null);
              
            }
            if (News.NewsID > 0)
            {
                if (Utils.CheckPermission(3, 27, Utils.LoggedUser.Roles) < 1)
                {
                    return Utils.ServiceResponse("", new ModelResponse(101), null);
                }
            }
            else
            {
                  if (Utils.CheckPermission(2, 27, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            }
          
           
                News.PostUserID = Utils.LoggedUser.PortalUserId.ToString();
               result= Mgr.Insert(News);
                if (result > 0)
                {
                    return new ModelResponse(0, result);
                }

             
            

            return result;
        }
    }
}