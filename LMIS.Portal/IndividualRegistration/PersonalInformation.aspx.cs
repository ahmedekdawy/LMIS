using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using LMIS.Portal.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using LMIS.Infrastructure.Data.DTOs;
namespace LMIS.Portal.IndividualRegistration
{
    public partial class PersonalInformation : System.Web.UI.Page
    {
        public const string PageCode = "F009";
        private static readonly IindividualDetailsManager Mgr = BllFactory.Singleton.IndividualDetails;
        private static readonly ISubCodeManager Mgrgeneral = BllFactory.Singleton.SubCode;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        [WebMethod]
        public static object PostPersonalInformation(IndividualRegisterationVm data,string mode,bool validateOnly)
        {
            PersonalInformation pi = new PersonalInformation();
            pi.CheckLogIn(mode);
            UserInfo userinfo = new UserInfo();
            if (Utils.LoggedUser == null && mode == "a")
            {
                //HttpContext.Current.Session.Remove("UserInfo");
                userinfo.UserId = pi.CreatNewUser(data);
                userinfo.IsIndividual = true;
                userinfo.IsApproved = true;
                HttpContext.Current.Session["UserInfo"] = userinfo;
           }
            var mr = Mgr.PostInformationPerson(Utils.LoggedUser, ref data, Utils.UploadFolder, validateOnly);

            if (userinfo != null) 
            {
                userinfo.PortalUserId = Convert.ToDecimal(mr.Data.ToString());
                HttpContext.Current.Session["UserInfo"] = userinfo;
            }
            return Utils.ServiceResponse(PageCode, mr);

        }
        [WebMethod]
        public static object GetPersonalInformation(string mode)
        {
            PersonalInformation pi = new PersonalInformation();
            pi.CheckLogIn(mode);

            var mr = Mgr.GetPersonalInformation(Utils.LoggedUser);
            return Utils.ServiceResponse(PageCode, mr);
        }
        public string CreatNewUser(IndividualRegisterationVm data)
        {
            
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = data.Email, Email = data.Email };
            IdentityResult result = manager.Create(user, data.Password);
            if (result.Succeeded)
            {
                // user.Id;
                return user.Id;
            }
            else
            {
                return null;
            }
        }

        public void CheckLogIn(string mode)
        {
            if (Utils.LoggedUser == null && mode == "e")
            {
                Response.Redirect("../login");
            }
        }
    }
}

