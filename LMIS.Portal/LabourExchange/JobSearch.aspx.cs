using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class JobSearch : System.Web.UI.Page
    {
        public const string PageCode = "F018";
        private static readonly IJobManager Mgr = BllFactory.Singleton.Job;

        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (Utils.LoggedUser != null && Utils.LoggedUser.IsIndividual)
        //        MasterPageFile = "~/MasterPages/LabourExchange.master";
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object Search(string keywords, string langCode)
        {
            var mr = Mgr.Search(keywords, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}