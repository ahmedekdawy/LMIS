using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace LMIS.Portal.IndividualRegistration
{
    public partial class AddNewTraining : System.Web.UI.Page
    {
        public const string PageCode = "F009";
        private static readonly IindividualDetailsManager Mgr = BllFactory.Singleton.IndividualDetails;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object PostNewTrainingInformation(TrainingandCertificationVm data, bool validateOnly)
        {
            var mr = Mgr.PostNewTrainingInformation(Utils.LoggedUser, ref data, validateOnly);
            return Utils.ServiceResponse(PageCode, mr);
        }
        [WebMethod]
        public static object GetTrainingInformation(long id, string langCode)
        {
            var mr = Mgr.GetTrainingInformation(Utils.LoggedUser, id, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}