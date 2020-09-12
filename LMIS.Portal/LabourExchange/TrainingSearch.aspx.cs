using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class TrainingSearch : System.Web.UI.Page
    {
        public const string PageCode = "F020";
        private static readonly ITrainingManager Mgr = BllFactory.Singleton.Training;

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