using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ninject;
using LMIS.Infrastructure.Interfaces;
using LMIS.Bll.Managers;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.BackEnd
{
    public partial class GeneralCode : System.Web.UI.Page
    {

        [Inject]
        public  IGeneralCodeManager _GeneralCodeManager { get; set; }
        public static IGeneralCodeManager _GeneralCodeManager1 { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
            }
            _GeneralCodeManager.GetGeneralCode(1);
        }
        [WebMethod]
        public static List<GeneralCodeVM> GetGeneralCodes()
        {
          return  _GeneralCodeManager1.GetGeneralCode(1);
       
        }
    }
}