using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class AvailableVacancies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetAvailableVacancies()
        {

            var AvailableVacancies =
                BllFactory.Singleton.Job.JobsPerYear(true, true, (int)Utils.GetLanguage()).Where(w => w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now).GroupBy(g => g.Title).Select(s => new { Title = s.Key, Count = s.Count() }).ToList();
            return AvailableVacancies;


        }
    }
}