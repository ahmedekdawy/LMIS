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

namespace LMIS.Portal.Individual
{
    public partial class EditSkillInfo : System.Web.UI.Page
    {

        public const string PageCode = "F009";
        private static readonly IindividualDetailsManager Mgr = BllFactory.Singleton.IndividualDetails;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object PostNewSkills(SkillsInformationVm data, bool validateOnly)
        {
            var mr = Mgr.PostNewSkills(Utils.LoggedUser, ref data, validateOnly);
            return Utils.ServiceResponse(PageCode, mr);
        }

        [WebMethod]
        public static object GetNewSkills(string langCode)
        {
            var mr = Mgr.SkillsList(Utils.LoggedUser, (int)Utils.GetLanguage(langCode));
            return Utils.ServiceResponse(PageCode, mr);
        }
          [WebMethod]
        public static object SkillDelete(long id, string reason)
        {
            var mr = Mgr.SkillDelete(Utils.LoggedUser,  id,  reason);
            return Utils.ServiceResponse(PageCode, mr);
        }
    }
}