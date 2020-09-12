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
    public partial class JobsOffers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static object GetJobsOffers()
        {

            var JobsOffers =
                BllFactory.Singleton.Job.JobsPerYear(true, false, (int)Utils.GetLanguage()).GroupBy(g => g.Title).Select(s => new { Title = s.Key, Count = s.Count() }).ToList();
            return JobsOffers;


        }
    }
}