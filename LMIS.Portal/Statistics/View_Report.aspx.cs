using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Reflection;
using System.Text;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using Microsoft.Reporting.WebForms;
using Statistics;
using DataTable = System.Data.DataTable;

namespace LMIS.Portal
{
    public partial class View_Report : System.Web.UI.Page
    {
     
       
        
        protected void Page_Load(object sender, EventArgs e)
        {


            

            if (!IsPostBack)
            {
               
               DataTable dtOtherData = new DataTable();
         DataTable ds1 = new DataTable();
         string ThemeName ;
         string UnitScale ;
                ReportVM result=new ReportVM();
         new ReportsManagers().LoadReport((int)Utils.GetLanguage(), ref result,ref  dtOtherData, chkreportType.SelectedValue, ref ds1, ref Migration_Rport,ref NotificationLbl);
                if (string.IsNullOrEmpty(NotificationLbl.Text))
                {
                    List<ReportParameter> paramList = new List<ReportParameter>();
                    int lang = (int)Utils.GetLanguage();
                    paramList.Add(new ReportParameter("ReportName", Utils.GetReportName(lang, result)));

                    paramList.Add(new ReportParameter("Theme", result.ThemeName));
                    paramList.Add(new ReportParameter("UnitScale", result.UnitScale));
                    paramList.Add(new ReportParameter("changingVariableName", result.changingVariableName));
                    paramList.Add(new ReportParameter("RunningVariableName", result.RunningVariableName));
                    paramList.Add(new ReportParameter("rpGraphType", chkreportType.SelectedValue));

                    ReportDataSource rptds = new ReportDataSource("DataSet1", ds1);
                    ReportDataSource rptds1 = new ReportDataSource("OtherDs", dtOtherData);

                    if ((int)Utils.GetLanguage() == 1)
                    {
                        Migration_Rport.LocalReport.ReportPath = "Statistics\\RDLC\\Report5.rdlc";
                    }
                    else
                    {
                        Migration_Rport.LocalReport.ReportPath = "Statistics\\RDLC\\Report5Ar.rdlc";
                    }

                    Migration_Rport.LocalReport.DataSources.Clear();
                    Migration_Rport.LocalReport.DataSources.Add(rptds);
                    Migration_Rport.LocalReport.DataSources.Add(rptds1);
                    Migration_Rport.LocalReport.SetParameters(paramList);
                    Migration_Rport.DataBind();
                    Migration_Rport.LocalReport.Refresh();
                }
              //  LoadReport();
            }
        }
  
        protected void refreshBtn_Click(object sender, EventArgs e)
        {
          
            //foreach (ListItem item in Dimension_Chk.Items)
            //{
            //    string x = "";
            //    if (item.Selected)
            //    {
                    
            //        x= x +" , "+ item.Value.ToString();                
            //    }
            //}

        }
     

        protected void chkreportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["result"] != null)
            {
                var result = (ReportVM) Session["result"];
                DataTable ds1 = (DataTable)Session["ds1"] ;
           DataTable      dtOtherData = (DataTable)Session["dtOtherData"];
           
                List<ReportParameter> paramList = new List<ReportParameter>();
                int lang = (int) Utils.GetLanguage();
                paramList.Add(new ReportParameter("ReportName", Utils.GetReportName(lang, result)));
                paramList.Add(new ReportParameter("Source", Utils.GetReportSource(lang, result)));
                paramList.Add(new ReportParameter("PublishYear",Convert.ToString( result.PublishYear)));
                paramList.Add(new ReportParameter("Theme",Session["ThemeName"].ToString() ));
                paramList.Add(new ReportParameter("UnitScale", Session["UnitScale"].ToString()));
                paramList.Add(new ReportParameter("changingVariableName", result.changingVariableName));
                paramList.Add(new ReportParameter("RunningVariableName", result.RunningVariableName));
                paramList.Add(new ReportParameter("rpGraphType", chkreportType.SelectedValue));

                ReportDataSource rptds = new ReportDataSource("DataSet1", ds1);
                ReportDataSource rptds1 = new ReportDataSource("OtherDs", dtOtherData);

                if ((int) Utils.GetLanguage() == 1)
                {
                    Migration_Rport.LocalReport.ReportPath = "Statistics\\RDLC\\Report5.rdlc";
                }
                else
                {
                    Migration_Rport.LocalReport.ReportPath = "Statistics\\RDLC\\Report5Ar.rdlc";
                }

                Migration_Rport.LocalReport.DataSources.Clear();
                Migration_Rport.LocalReport.DataSources.Add(rptds);
                Migration_Rport.LocalReport.DataSources.Add(rptds1);
                Migration_Rport.LocalReport.SetParameters(paramList);
                Migration_Rport.DataBind();
                Migration_Rport.LocalReport.Refresh();
            }
        }
    }
    
}