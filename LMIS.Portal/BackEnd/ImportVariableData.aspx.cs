using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Bll.Managers;
using System.IO;
using LMIS.Infrastructure;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Portal.Helpers;

namespace LMIS.Portal.BackEnd
{
    public partial class ImportVariableData : System.Web.UI.Page
    {

        private static readonly ISubCodeManager MgrSubCode = BllFactory.Singleton.SubCode;
        private static readonly IGeneralCodeManager MgrGeneralCode = BllFactory.Singleton.GeneralCode;
        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (fUpload.HasFiles)
            {
                string path =
                    Server.MapPath(Request.ApplicationPath + "//Temp//" + fUpload.FileName);
                fUpload.PostedFile.SaveAs(path);
                int themeId = 0;
                int reportId = 0;
                DataSet ds = GetDataTableFromExcel(path, ref themeId, ref reportId);
                string value;


                if (ds.Tables.Count > 0)
                {
                    string generalID, generalName, SubiD, SubName;
                    int lang = 1;
                    
                    foreach (DataTable dt in ds.Tables)
                    {
                        for (int x = 0; x < dt.Columns.Count; x++)
                        {
                            lang=int.Parse(  dt.Rows[0][x].ToString().Split(new[] { '-' })[0]);
                          
                            generalID= dt.Columns[x].ColumnName.Split(new[] {'-'})[0];
                           generalName= dt.Columns[x].ColumnName.Split(new[] {'-'})[1];
                            if (generalID.Substring(0, 1) != "0")
                            {
                                generalID = dt.Columns[x].ColumnName.Split(new[] { '-' })[1];
                                generalName = dt.Columns[x].ColumnName.Split(new[] { '-' })[0];
                            }
                            MgrGeneralCode.Save(new GeneralCodeVM() { GeneralID = generalID, Name = generalName, LanguageID = lang, ParentGeneralcodeID = "1" });
                            switch (lang)
                            {
                                case 1: MgrSubCode.Save(new SubCodeVm() { GeneralID = generalID, LanguageID = lang, Name = "ALL", SubID = generalID + "00001" });
                                    break;
                                case 3: MgrSubCode.Save(new SubCodeVm() { GeneralID = generalID, LanguageID = lang, Name = "الكل", SubID = generalID + "00001" });
                                    break;
                                case 2: MgrSubCode.Save(new SubCodeVm() { GeneralID = generalID, LanguageID = lang, Name = "TOUS", SubID = generalID + "00001" });
                                    break;
                            }

                             
                          
                            for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            SubiD=generalID+ (i+1).ToString("00000");
                            SubName = dt.Rows[i][x].ToString();
                            if (!string.IsNullOrEmpty(SubName))
                            {
                                MgrSubCode.Save(new SubCodeVm()
                                {
                                    GeneralID = generalID,
                                    LanguageID = lang,
                                    Name = SubName,
                                    SubID = SubiD
                                });
                            }

                        }

                        }
                        
                     
                    }
                    ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.success('" + GetGlobalResourceObject("MessagesResource", "X_Success") + @"');", true);
                    
                }
                else
                {

                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "lmis.notification.error('" + GetGlobalResourceObject("MessagesResource", "X_SelectFile") + @"');", true);
               

            }
        }

        public DataSet GetDataTableFromExcel(string path, ref int themeID, ref int reportId)
        {
            string fixedVariables;
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
                    bool hasHeader = true ; // adjust it accordingly( i've mentioned that this is a simple approach)
                    
                  

                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader
                            ? firstRowCell.Text
                            : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        var row = tbl.NewRow();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }

                        tbl.Rows.Add(row);

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