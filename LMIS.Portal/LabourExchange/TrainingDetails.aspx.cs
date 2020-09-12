using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class TrainingDetails : System.Web.UI.Page
    {
        public const string PageCode = "F021";
        private static readonly ITrainingManager Mgr = BllFactory.Singleton.Training;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object View(long id, string langCode)
        {
            var mr = Mgr.View(Utils.LoggedUser, id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Apply(long id)
        {
            var mr = Mgr.Apply(Utils.LoggedUser, id);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}