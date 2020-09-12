using LMIS.Portal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;

namespace LMIS.Portal.DashBoard
{
    public partial class BecomePartner : System.Web.UI.Page
    {
        private static readonly IPartnerManager Mgr = BllFactory.Singleton.PartnersManager;
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
        public static object Insert(PartnerVM partner)
        {
            if (!Utils.LoggedUser.IsEmployer && !Utils.LoggedUser.IsTrainingProvider)
            {
                 return Utils.ServiceResponse("xxx", new ModelResponse(101), null);
            }
            partner.PortalUserID = Utils.LoggedUser.PortalUserId;
            partner.PostUserID = Utils.LoggedUser.UserId;
            partner.LanguageID = (int) Utils.GetLanguage();
            var result = Mgr.Insert(partner);
            return result;

        }

    }
}