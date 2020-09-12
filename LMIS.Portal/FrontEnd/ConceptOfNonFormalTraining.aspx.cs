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
    public partial class ConceptNonFormalTraining : System.Web.UI.Page
    {
        private static readonly IConceptNonFormalTrainingManager Mgr = BllFactory.Singleton.ConceptNonFormalTraining;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object Get()
        {


            var mr = Mgr.Get((int)Utils.GetLanguage(),0);
            return Utils.ServiceResponse(mr,null );

        }
    }
}