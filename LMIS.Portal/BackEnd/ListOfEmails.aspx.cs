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

namespace LMIS.Portal.BackEnd
{
    public partial class ListOfEmails : System.Web.UI.Page
    {
        private static readonly IListOfEmailsManager Mgr = BllFactory.Singleton.ListOfEmails;
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

            if (Utils.CheckPermission(1, 18, Utils.LoggedUser.Roles) < 1)
            {
                
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }

            var mr = Mgr.Get( 0);
            return Utils.ServiceResponse(mr, null);

        }
        [WebMethod]
        public static object Post(ListOfEmailsVm vm)
        {

            if (vm.EmailID > 0)
            {
                if (Utils.CheckPermission(3, 18, Utils.LoggedUser.Roles) < 1)
                {
                    return Utils.ServiceResponse("", new ModelResponse(101), null);
                }
            }
            else
            {
                if (Utils.CheckPermission(2, 18, Utils.LoggedUser.Roles) < 1)
                {
                    return Utils.ServiceResponse("", new ModelResponse(101), null);
                }
            }
           
            var mr = Mgr.Post(Utils.LoggedUser, vm);
            return Utils.ServiceResponse(mr, null);

        }
        [WebMethod]
        public static object Delete(long EmailID)
        {

            if (Utils.CheckPermission(4, 18, Utils.LoggedUser.Roles) < 1)
            {
                return Utils.ServiceResponse("", new ModelResponse(101), null);
            }
            var mr = Mgr.Delete(Utils.LoggedUser, EmailID);
            return Utils.ServiceResponse(mr, null);

        }
    }
}