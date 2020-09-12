using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class TrainingProviders : System.Web.UI.Page
    {
        private static readonly IEmployersTrainingProvidersManager Mgr = BllFactory.Singleton.EmployersTrainingProviders;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object Get(int type)
        {


            var mr = Mgr.GetFrontBack((int)Utils.GetLanguage(), type, 0);
            return Utils.ServiceResponse(mr,null );

        }
    }
}