using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Individual;
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

namespace LMIS.Portal.Individual
{
    public partial class Registeration : System.Web.UI.Page
    {
        public const string pageCode = "F009";
        public const string loginURL = "login";

        private static readonly IAspNetUsersManager userManager = BllFactory.Singleton.AspNetUsersManager;
        private static readonly IIndividualManager individualManager = BllFactory.Singleton.IndividualManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(pageCode);
        }

        [WebMethod]
        public static object Post(IndividualVM individualObject)
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
               
                    RequireUppercase = false
                };
                IdentityResult userResult;
                var user = new ApplicationUser() { UserName = individualObject.User.UserName, Email = individualObject.User.Email, PhoneNumber = individualObject.User.PhoneNumber };
                userResult = manager.Create(user, individualObject.User.Password);
                ModelResponse mr = null;
                if (userResult.Succeeded)
                {
                    mr = individualManager.Post(ref individualObject, user.Id);
                    return Utils.ServiceResponse(pageCode, mr);
                }
                else
                {
                    if (userResult.Errors.FirstOrDefault().Contains("is already taken"))
                    {
                       
                        mr = new ModelResponse(-4, null);
                    }
                    else
                    {
                        mr = new ModelResponse(new Exception(userResult.Errors.FirstOrDefault())); 
                    }
                  
                   
                    return Utils.ServiceResponse(pageCode, mr);
                }
            }
            catch (Exception ex)
            {
                ModelResponse mr = new ModelResponse(new Exception(ex.Message));
                 return Utils.ServiceResponse(pageCode,mr ); 
            }
        }

    }
}