using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.BackEnd
{
    public partial class Offices : System.Web.UI.Page
    {
        public const string PageCode = "B001";
        private static readonly IOfficeManager Mgr = BllFactory.Singleton.Office;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List()
        {
            if (Utils.CheckPermission(1, 91, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.List();
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Delete(long id, string reason)
        {
            if (Utils.CheckPermission(4, 91, Utils.LoggedUser.Roles) < 1)
                return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);

            var mr = Mgr.Delete(Utils.LoggedUser, id, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Post(OfficeVm data)
        {
            if (data.OfficeId > 0)
            {
                if (Utils.CheckPermission(3, 91, Utils.LoggedUser.Roles) < 1)
                    return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);
            }
            else
            {
                if (Utils.CheckPermission(2, 91, Utils.LoggedUser.Roles) < 1)
                    return Utils.ServiceResponse(PageCode, new ModelResponse(101), null);
            }

            var mr = Mgr.Post(Utils.LoggedUser, ref data);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}