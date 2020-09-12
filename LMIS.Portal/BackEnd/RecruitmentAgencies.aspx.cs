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
using LMIS.Portal.Helpers;

namespace LMIS.Portal.BackEnd
{
    public partial class RecruitmentAgencies : System.Web.UI.Page
    {
        private static readonly IRecruitmentAgenciesManager Mgr = BllFactory.Singleton.RecruitmentAgencies;
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
        public static object Get()
        {
          
            if (Utils.CheckPermission(1, 29, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }

            var mr = Mgr.Get(0,0);
            return Utils.ServiceResponse(mr, null);

        }
        [WebMethod]
        public static object Post(RecruitmentAgenciesVM vm)
        {
            if (vm.RecruitmentAgencieID > 0)
            {
                if (Utils.CheckPermission(3, 29, Utils.LoggedUser.Roles) < 1)
                {
                    return Utils.ServiceResponse("", new ModelResponse(101), null);
                }
            }
            else
            {
                if (Utils.CheckPermission(2, 29, Utils.LoggedUser.Roles) < 1)
                {
                    return Utils.ServiceResponse("", new ModelResponse(101), null);
                }
            }

            
            var mr = Mgr.Post(Utils.LoggedUser , vm);
            return Utils.ServiceResponse(mr, null);

        }
         [WebMethod]
        public static object Delete(long ID)
        {

            if (Utils.CheckPermission(4, 29, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            var mr = Mgr.Delete(Utils.LoggedUser, ID);
            return Utils.ServiceResponse(mr, null);

        }
    }
}