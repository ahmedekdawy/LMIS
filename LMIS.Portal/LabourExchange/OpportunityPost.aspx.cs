using LMIS.Bll.Helpers;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class OpportunityPost : System.Web.UI.Page
    {
        public const string PageCode = "F005";
        private static readonly IOpportunityManager Mgr = BllFactory.Singleton.Opportunity;

        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Utils.LoggedUser == null) return;

            var mode = Request.QueryString["m"];

            if (!string.IsNullOrWhiteSpace(mode) && mode.ToLower().Trim() == "r")
                MasterPageFile = "~/MasterPages/Review.master";
            else if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId))
                    MasterPageFile = "~/MasterPages/BackEnd.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object PostAnOpportunity(OpportunityVm data, bool validateOnly)
        {
            if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId))
            {
                if (data.OpportunityId > 0)
                {
                    if (Utils.CheckPermission(3, 92, Utils.LoggedUser.Roles) < 1)
                        return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);
                }
                else
                {
                    if (Utils.CheckPermission(2, 92, Utils.LoggedUser.Roles) < 1)
                        return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);
                }
            }

            var mr = Mgr.Post(Utils.LoggedUser, ref data, Utils.UploadFolder, validateOnly);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object GetOrgContactOpportunityById(long id)
        {
            if (DalFactory.Singleton.DataService.IsAdmin(Utils.LoggedUser.UserId))
            {
                if (Utils.CheckPermission(1, 92, Utils.LoggedUser.Roles) < 1)
                    return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);
            }

            var mr = Mgr.Get(Utils.LoggedUser, id);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Approve(long reqKey, long id, bool approved, string reason)
        {
            var mr = Mgr.Approve(Utils.LoggedUser, reqKey, id, approved, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}