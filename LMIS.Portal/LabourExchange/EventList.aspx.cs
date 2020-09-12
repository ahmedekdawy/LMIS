using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class EventList : System.Web.UI.Page
    {
        public const string PageCode = "F006";
        private static readonly IEventManager Mgr = BllFactory.Singleton.Event;

        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null) return;

            if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId))
                MasterPageFile = "~/MasterPages/BackEnd.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List()
        {
            if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId) && Utils.CheckPermission(1, 93, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.ListByOrgContact(Utils.LoggedUser);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Delete(long id, string reason)
        {
            if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId) && Utils.CheckPermission(4, 93, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.Delete(Utils.LoggedUser, id, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}