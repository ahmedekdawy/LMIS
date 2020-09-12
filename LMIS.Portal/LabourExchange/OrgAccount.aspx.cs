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
namespace LMIS.Portal.LabourExchange
{
    public partial class OrgAccount : System.Web.UI.Page
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
        public static object GetDetails()
        {
            try
            {
                if (Utils.LoggedUser == null)
                {
                    ModelResponse mr = new ModelResponse(new Exception("Not authorized!"));
                    return Utils.ServiceResponse(pageCode, mr);
                }

                var result = organizationManager.GetOrganizationDetails((long)Utils.LoggedUser.PortalUserId, Utils.GetLanguage());
                return Utils.ServiceResponse(pageCode, result);
            }
            catch (Exception ex)
            {
                ModelResponse mr = new ModelResponse(new Exception(ex.Message));
                return Utils.ServiceResponse(pageCode, mr);
            }
        }

        [WebMethod]
        public static object Update(OrganizationVM organizationObject, bool validateOnly)
        {
            try
            {
                organizationObject.PortalUsersID = Utils.LoggedUser.PortalUserId;

                var result = organizationManager.Update(ref organizationObject, Utils.LoggedUser.UserId);
                return Utils.ServiceResponse(pageCode, result);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}