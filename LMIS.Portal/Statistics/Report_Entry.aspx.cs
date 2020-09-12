using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using Statistics;

namespace LMIS.Portal
{
    public partial class Report_Entry : System.Web.UI.Page
    {
        public string ConnectionString12 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private static readonly IReportsManager MgrReports = BllFactory.Singleton.Reports;
        private static readonly IDimThemesManager Mgr = BllFactory.Singleton.DimThemes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               
                int lang = (int) Utils.GetLanguage();
                var resultReport = MgrReports.GetReports();
                var resultThemeType = Mgr.GetAllThemeType(lang);

                new ReportsManagers().divReportsHtml( lang,resultReport, resultThemeType, ref divReports, ref ulThemeType);
            }
          
            //divReports;

        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            //Session["ReportID"] = ReportDrp.SelectedValue.ToString();
            Response.Redirect("View_Report.aspx");
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            LinkButton link= (LinkButton)sender;
            Session["ReportID"] = link.ID;
            Response.Redirect("View_Report.aspx");
        }

    }
}