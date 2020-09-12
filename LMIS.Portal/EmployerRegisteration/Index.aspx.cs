using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Data.Entities.Organization;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using LMIS.Portal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMIS.Portal.EmployerRegisteration
{
    public partial class Index : System.Web.UI.Page
    {
        public const string pageCode = "F037";
        public const string loginURL = "login";

        private static readonly IAspNetUsersManager userManager = BllFactory.Singleton.AspNetUsersManager;
        private static readonly IOrganizationManager organizationManager = BllFactory.Singleton.Organization;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(pageCode);
        }

        [WebMethod]
        public static object Post(OrganizationVM organizationObject, bool validateOnly)
        {
            try
            {
                var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                manager.PasswordValidator = new PasswordValidator()
                {
                    RequiredLength = 3,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false
                };
                IdentityResult userResult;
                var user = new ApplicationUser() { UserName = organizationObject.User.UserName, Email = organizationObject.User.Email, PhoneNumber = organizationObject.User.PhoneNumber };
                userResult = manager.Create(user, organizationObject.User.Password);
                ModelResponse mr = null;
                if (userResult.Succeeded)
                {
                    var result = userManager.GetUsersAdmin();
                    mr = organizationManager.Post(ref organizationObject, user.Id);
                }
                return Utils.ServiceResponse(pageCode, mr);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}