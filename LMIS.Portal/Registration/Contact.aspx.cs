using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using LMIS.Portal.Models;

using System;
using System.Collections.Generic;
using System.Web.Services;

using Microsoft.AspNet.Identity;

namespace LMIS.Portal.Registration
{
    public partial class Contact : System.Web.UI.Page
    {
        public const string PageCode = "R003";

        private static readonly UserManager<ApplicationUser> UserMgr = BllFactory.Singleton.User;
        private static readonly IOrgManager OrgMgr = BllFactory.Singleton.Org;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object Get(long contactId)
        {
            var mr = OrgMgr.GetOrgContact(Utils.LoggedUser, contactId);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Update(OrgContactVm data, string password)
        {
            ApplicationUser user = null;

            try
            {
                if (data.ContactId == 0) //New User?
                {
                    user = new ApplicationUser() { UserName = data.UserName, Email = data.UserName };

                    var ir = UserMgr.Create(user, password);
                    if (!ir.Succeeded) return IdentityErrors(ir.Errors);
                }

                var mr = OrgMgr.UpdateOrgContact(Utils.LoggedUser, ref data);
                if (mr.Status == ResponseStatus.Success) return Utils.ServiceResponse(PageCode, mr);

                if (user != null) UserMgr.Delete(user);
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