﻿using LMIS.Infrastructure.Enums;
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
    public partial class EducationalInformation : System.Web.UI.Page
    {
        public const string PageCode = "F009";
        private static readonly IindividualDetailsManager Mgr = BllFactory.Singleton.IndividualDetails;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetEducationList()
        {
            
           var mr = Mgr.EducationList(Utils.LoggedUser);
           return Utils.ServiceResponse(PageCode, mr);
        }
        [WebMethod]
        public static object Delete(long id, string reason)
        {
            var mr = Mgr.EducationDelete(Utils.LoggedUser, id, reason);
            return Utils.ServiceResponse(PageCode, mr);
        }

    }
}