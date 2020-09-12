using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using Microsoft.AspNet.Identity.Owin;
using Ninject;



namespace LMIS.Portal.Administration
{
    public partial class login : System.Web.UI.Page
    {
        public const string PageCode = "F007";
        private static readonly IAspNetUsersManager Mgr = BllFactory.Singleton.AspNetUsersManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserInfo"] = null;
        }
   
        [WebMethod]
        public static object chklogin(string email, string password)
        {
            var signinManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            var result = signinManager.PasswordSignIn(email, password, true, shouldLockout: false);
        
            switch (result)
            {
                case SignInStatus.Success:
                    var userinfo = Mgr.GetUserInfo(email);
               // var roles = signinManager.CreateUserIdentity().GetRolesForUser(email);
                     
                HttpContext.Current.Session["UserInfo"] = userinfo;
                    return new {status=1,Message=HttpContext.GetGlobalResourceObject("MessagesResource", "X_Success")};
                    break;
                case SignInStatus.LockedOut:
                    return new { status = 2, Message = HttpContext.GetGlobalResourceObject("MessagesResource", "F007_LockedOut") };
                   
                    break;
                case SignInStatus.RequiresVerification:
                    return new { status = 3, Message = HttpContext.GetGlobalResourceObject("MessagesResource", "F007_RequiresVerification") };
                    break;
                case SignInStatus.Failure:
                default:
                    return new { status = 4, Message = HttpContext.GetGlobalResourceObject("MessagesResource", "F007_InvalidLogin") };
                   
                

                    break;
            }


        }

       

        
    }
}