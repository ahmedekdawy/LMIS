using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class OpportunityList : System.Web.UI.Page
    {
        public const string PageCode = "F005";
        private static readonly IOpportunityManager Mgr = BllFactory.Singleton.Opportunity;

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
        public static object GetOpportunityList()
        {
            if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId) && Utils.CheckPermission(1, 92, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.ListByOrgContact(Utils.LoggedUser);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object DeleteOpportunityById(long id, string reason)
        {
            if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId) && Utils.CheckPermission(4, 92, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.Delete(Utils.LoggedUser, id, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }

}