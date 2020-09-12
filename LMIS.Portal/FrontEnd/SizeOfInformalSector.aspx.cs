using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.FrontEnd
{
    public partial class SizeOfInformalSector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {

                var resultReport = BllFactory.Singleton.Reports.GetReports(ConfigurationManager.AppSettings["SizeOfInformalSector"]);
                int lang = (int) Utils.GetLanguage();
                for (int j = 0; j < resultReport.Count; j++)

                {
                    divReports.InnerHtml += "<a href='../HarmonizedData/View?repId=" +
                                            resultReport[j].ReportID.ToString() +
                                            @"' target='_blank' class='report-title-link' >
                                             <i class='fa fa-angle-double-right'></i>
                                               " + Utils.GetReportName(lang, resultReport[j]) + "</a> ";
                }
            }
        }
    }
}