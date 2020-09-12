using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using LMIS.Portal.Models;

using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;

using Microsoft.AspNet.Identity;

namespace LMIS.Portal.Registration
{
    public partial class SignUp : Page
    {
        public const string PageCode = "R001";

        private static readonly UserManager<ApplicationUser> UserMgr = BllFactory.Singleton.User; 
        private static readonly IIndManager IndMgr = BllFactory.Singleton.Ind;
        private static readonly IOrgManager OrgMgr = BllFactory.Singleton.Org;

        protected void Page_Load(object sender, EventArgs e)
        {
            var qs = Request.QueryString["as"];
            var suffix = (qs != null && qs.Trim().ToLower() == "org") ? "_Org" : "_Ind";
            Title = Utils.GetPageTitle(PageCode + suffix);
        }

        [WebMethod]
        public static object OrgSignUp(OrgVm data, string password)
        {
            ApplicationUser user = null;

            try
            {
             
               
                user = new ApplicationUser() { UserName = data.UserName, Email = data.UserName };
                UserMgr.PasswordValidator = new PasswordValidator()
                {

                    RequireUppercase = false
                };
            
                var ir = UserMgr.Create(user, password);
                if (!ir.Succeeded) return IdentityErrors(ir.Errors);

                var mr = OrgMgr.SignUp(ref data, password, Utils.UploadFolder);
                if (mr.Status == ResponseStatus.Success) return Utils.ServiceResponse(PageCode, mr);

                UserMgr.Delete(user);
                return Utils.ServiceResponse(PageCode, mr);
            }
            catch (Exception ex)
            {
                if (user != null) UserMgr.Delete(user);
                return Utils.ServiceResponse(new ModelResponse(ex));
            }
        }

        private static object IdentityErrors(IEnumerable<string> errors)
        {
            return Utils.ServiceResponse(PageCode,
                new ModelResponse(new Exception(string.Join("\n", errors))));
        }
    }
}