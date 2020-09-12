using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

using System;
using System.Web.Services;

namespace LMIS.Portal.LabourExchange
{
    public partial class TrainingList : System.Web.UI.Page
    {
        public const string PageCode = "F015";
        private static readonly ITrainingManager Mgr = BllFactory.Singleton.Training;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Utils.GetPageTitle(PageCode);
        }

        [WebMethod]
        public static object List(string langCode)
        {
            ModelResponse mr;

            var mr1 = Mgr.ListByOrgContact(Utils.LoggedUser, null, (int)Utils.GetLanguage(langCode), true);

            if (mr1.Status == ResponseStatus.Success)
            {
                var mr2 = Mgr.GetTrainingList(Utils.LoggedUser);
                mr = mr2.Status == ResponseStatus.Success
                    ? new ModelResponse(0, new [] { mr1.Data, mr2.Data })
                    : mr2;
            }
            else
            {
                mr = mr1;
            }

            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object Delete(long id, string reason)
        {
            var mr = Mgr.Delete(Utils.LoggedUser, id, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object DetailApplicants(long id, string langCode)
        {
            var mr = Mgr.DetailApplicants(Utils.LoggedUser, id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object SetTrainingList(string file)
        {
            var mr = Mgr.SetTrainingList(Utils.LoggedUser, Utils.UploadFolder, file);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}