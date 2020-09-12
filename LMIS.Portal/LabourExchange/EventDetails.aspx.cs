using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Http;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class EventDetails : System.Web.UI.Page
    {
        public const string PageCode = "F006";
        private static readonly IEventManager Mgr = BllFactory.Singleton.Event;

        [WebMethod]
        public static object Get(long id)
        {
            var mr = Mgr.Get(id,(int) Utils.GetLanguage());
            return Utils.ServiceResponse(PageCode, mr);
        }

       
    }
}