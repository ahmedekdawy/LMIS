using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities.Individual;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMIS.Portal.Individual
{
    public partial class EditPersonalInfo : System.Web.UI.Page
    {
        public const string pageCode = "F009";
        public const string loginURL = "login";

        private static readonly IAspNetUsersManager userManager = BllFactory.Singleton.AspNetUsersManager;
        private static readonly IIndividualManager individualManager = BllFactory.Singleton.IndividualManager;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetDetails(string mode)
        {
            try
            {
               
                    if (Utils.LoggedUser == null && (mode == "e" || mode == "v"))
                    {

                        var mr = new ModelResponse(new Exception("Not authorized!"));
                        return Utils.ServiceResponse(pageCode, mr);

                    }

                    var result = individualManager.GetProfile(Utils.LoggedUser, (long) Utils.LoggedUser.PortalUserId,
                        (int) Utils.GetLanguage());
                    return Utils.ServiceResponse(pageCode, result);
              
            }
            catch (Exception ex)
            {
                var mr = new ModelResponse(new Exception(ex.Message));
                return Utils.ServiceResponse(pageCode, mr);
            }
        }
        [WebMethod]
        public static object PostUpdates(IndividualVM individualObject)
        {
            try
            {
                individualObject.UserID = Utils.LoggedUser.UserId;
                var mr = individualManager.UpdatePersonalInfo(individualObject);
                return Utils.ServiceResponse(pageCode, mr);
            }
            catch (Exception ex)
            {
                ModelResponse mr = new ModelResponse(new Exception(ex.Message));
                return Utils.ServiceResponse(pageCode, mr);
            }
        }

    }
}