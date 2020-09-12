using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Collections.Generic;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class JobApplication : System.Web.UI.Page
    {
        public const string PageCode = "F022";
        private static readonly IJobManager Mgr = BllFactory.Singleton.Job;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object ApplicationRequirements(long id, string langCode)
        {
            var mr = Mgr.ApplicationRequirements(id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Apply(long id, string appForm, List<CodeSet> attachments)
        {
            var mr = Mgr.Apply(Utils.LoggedUser, id, Utils.UploadFolder, appForm, attachments);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}