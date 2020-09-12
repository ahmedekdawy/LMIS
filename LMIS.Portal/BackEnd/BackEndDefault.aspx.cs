using LMIS.Portal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMIS.Portal.BackEnd
{
    public partial class BackEndDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.LoggedUser==null)
            {
                Response.Redirect("~/login");
            }
            if ( Utils.LoggedUser.Roles.Count<1)
            {
                Response.Redirect("~/login");
            }
        }
    }
}