using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;
using LMIS.Portal.Statistics.DS;
using Ninject;
using OfficeOpenXml;

namespace LMIS.Portal.BackEnd
{
    public partial class ImportTemplate : System.Web.UI.Page
    {


        private static readonly IFactStatisticalDataManager MgrFactStatisticalData = BllFactory.Singleton.FactStatisticalDataManager;
        private static readonly IReportsManager MgrReports = BllFactory.Singleton.Reports;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LMIS.Portal.Helpers.Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (LMIS.Portal.Helpers.Utils.CheckPermission(1, 8, LMIS.Portal.Helpers.Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);

                return;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (LMIS.Portal.Helpers.Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (LMIS.Portal.Helpers.Utils.CheckPermission(2, 8, LMIS.Portal.Helpers.Utils.LoggedUser.Roles) < 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"');", true);
           
                return;
            }
            if (FileUpload1.HasFiles)
            {
                string path =
                    Server.MapPath(Request.ApplicationPath + "//Temp//" + FileUpload1.FileName);
                FileUpload1.PostedFile.SaveAs(path);
                int   themeId=0;
                int  reportId =0;
                DataSet ds = GetDataTableFromExcel(path, ref themeId, ref reportId);
                string value;

                
                if (ds.Tables.Count > 0)
                {
                    var fixedVariables = MgrReports.GetReportsByID(reportId, (int)LMIS.Portal.Helpers.Utils.GetLanguage());

                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            
                           
                                if (dt.Columns.Count == 4)
                                {
                                    value = row[2].ToString();
                                    value = Regex.Replace(value, @"\s+", "");
                                    MgrFactStatisticalData.Insert(fixedVariables, themeId, dt.Columns[0].ColumnName, dt.Columns[1].ColumnName, row[0].ToString(), row[1].ToString(), decimal.Parse(value));
                                }
                                else
                                {
                                    value = row[1].ToString();
                                    value = Regex.Replace(value, @"\s+", "");
                                    MgrFactStatisticalData.Insert(fixedVariables, themeId, dt.Columns[0].ColumnName, null , row[0].ToString(), null , decimal.Parse(value));
                                }
                               
                            
                        }
                    }
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);
             
                    ClientScript.RegisterStartupScript(GetType(), "Success", " lmis.ajaxSuccessHandler('Success');", true );
                   
                }
                else
                {

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "error", " lmis.ajaxSuccessHandler('error');", true);

            }
        }
        public DataSet GetDataTableFromExcel(string path, ref int themeID, ref int reportId)
        {
          // string fixedVariables;
            bool firstsheet = true;
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
              //  var ws = pck.Workbook.Worksheets.First();
                DataSet ds = new DataSet();
                foreach (var ws in pck.Workbook.Worksheets)
                {

                    DataTable tbl = new DataTable(ws.Name);
                bool hasHeader = true; // adjust it accordingly( i've mentioned that this is a simple approach)
                //Cells[rowsnumber,
                 themeID =int.Parse( ws.Cells[1, 1, 1, ws.Dimension.End.Column].ToArray()[0].Text.Substring(0, ws.Cells[1, 1, 1, ws.Dimension.End.Column].ToArray()[0].Text.IndexOf("-")));
                 reportId = int.Parse(ws.Cells[2, 1, 2, ws.Dimension.End.Column].ToArray()[0].Text.Substring(0, ws.Cells[2, 1, 2, ws.Dimension.End.Column].ToArray()[0].Text.IndexOf("-")));
                 var  fixedVariables = MgrReports.GetReportsByID(reportId, (int)LMIS.Portal.Helpers.Utils.GetLanguage());   
                    foreach (var firstRowCell in ws.Cells[4, 1, 4, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader
                            ? firstRowCell.Text
                            : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 5 : 1;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    ExcelRange wsRow;
                    if (fixedVariables==null || string.IsNullOrEmpty(fixedVariables.RunningVariableID.Trim()))
                    {
                         wsRow = ws.Cells[rowNum, 1, rowNum, 2];
                    }
                    else
                    {
                         wsRow = ws.Cells[rowNum, 1, rowNum, 3];
                    }
                   
                    var row = tbl.NewRow();
                    foreach (var cell in wsRow)
                    {
                        
                            row[cell.Start.Column - 1] = cell.Text;
                        
                    }
                    if (fixedVariables == null || string.IsNullOrEmpty(fixedVariables.RunningVariableID.Trim()))
                    {
                        if (!string.IsNullOrEmpty(row[1].ToString()))
                        {

                            tbl.Rows.Add(row);
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(row[2].ToString()))
                        {

                            tbl.Rows.Add(row);
                        }

                    }
                   

                }
                    if (tbl.Rows.Count > 0)
                    {

                        ds.Tables.Add(tbl);
                    }
                    
                }
                return ds;
            }
        }
    }
}