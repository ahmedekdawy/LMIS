using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.BackEnd
{
    public partial class Unions : System.Web.UI.Page
    {
        public const string PageCode = "B002";
        private static readonly IUnionManager Mgr = BllFactory.Singleton.Union;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List()
        {
            if (Utils.CheckPermission(1, 90, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.List();
            var govLookup = BllFactory.Singleton.SubCode.List("003");
            return Utils.ServiceResponse(PageCode, mr, govLookup);
        }

        [WebMethod]
        public static object Delete(long id, string reason)
        {
            if (Utils.CheckPermission(4, 90, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.Delete(Utils.LoggedUser, id, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Post(UnionVm data, bool validateOnly)
        {
            if (data.UnionId > 0)
            {
                if (Utils.CheckPermission(3, 90, Utils.LoggedUser.Roles) < 1)
                    return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);
            }
            else
            {
                if (Utils.CheckPermission(2, 90, Utils.LoggedUser.Roles) < 1)
                    return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);
            }

            var mr = Mgr.Post(Utils.LoggedUser, ref data, Utils.UploadFolder, validateOnly);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}