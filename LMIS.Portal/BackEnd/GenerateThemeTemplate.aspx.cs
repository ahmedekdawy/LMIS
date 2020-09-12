using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using Ninject;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.Style;
using LMIS.Portal.Helpers;
using Utils = LMIS.Infrastructure.Helpers.Utils;


namespace LMIS.Portal.BackEnd
{
    public partial class GenerateThemeTemplate : System.Web.UI.Page
    {
   
        private static readonly IDimThemesManager Mgr = BllFactory.Singleton.DimThemes;
        private static readonly IReportsManager MgrReports = BllFactory.Singleton.Reports;
        private static readonly ISubCodeManager MgrSubCode = BllFactory.Singleton.SubCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (LMIS.Portal.Helpers. Utils.LoggedUser == null)
                {
                    Response.Redirect("~/login");
                }
                if (LMIS.Portal.Helpers.Utils.CheckPermission(1, 9, LMIS.Portal.Helpers.Utils.LoggedUser.Roles) < 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                    return;
                }
                int lang = (int)LMIS.Portal.Helpers . Utils.GetLanguage();
                ddlThemeType.Items.Clear();
                ddlThemeType.Items.Add(new ListItem("", "", true));
                ddlThemeType.DataSource = Mgr.GetAllThemeType(lang);
                ddlThemeType.DataTextField = "Name";
                ddlThemeType.DataValueField = "SubID";
                ddlThemeType.DataBind();
              
            }
           
        }
        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {

            //chkRunnningVariables.DataSource = MgrReports.GetReportsRunningValues(int.Parse(ddlReports.SelectedValue), 1);
            //chkRunnningVariables.DataTextField = "Name";
            //chkRunnningVariables.DataValueField = "SubID";
            //chkRunnningVariables.DataBind();
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (LMIS.Portal.Helpers.Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (LMIS.Portal.Helpers.Utils.CheckPermission(2, 9, LMIS.Portal.Helpers.Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
            int lang = (int)LMIS.Portal.Helpers.Utils.GetLanguage();

            var dt = MgrReports.GetReportsChangingValues(int.Parse(ddlReports.SelectedValue), lang);
            var result = MgrReports.GetReportsRunningValues(int.Parse(ddlReports.SelectedValue), lang);
           // var runningVariable = result[0].GeneralID + "-" + result[0].GeneralName;
            System.Data.DataTable  dtResult = new  System.Data.DataTable ();
            if (dt.Count > 0)
            {
                dtResult.Columns.Add(dt[0].GeneralID + "-" + dt[0].GeneralName);
            }


            //for (int i = 0; i < chkRunnningVariables.Items.Count; i++)
            //{
            //    if (chkRunnningVariables.Items[i].Selected)
            //    {


            //        runnigvalue = chkRunnningVariables.Items[i].Value + "-" + chkRunnningVariables.Items[i].Text;
            //        break;
            //    }

            //}
            foreach (var item in dt)
            {
                   DataRow row = dtResult.NewRow();
                if (item.SubID.Substring(3)=="00001")
                {
                    continue;
                }
                    row[dt[0].GeneralID + "-" + dt[0].GeneralName] =item.SubID+"-"+ item.Name;
               
                   dtResult.Rows.Add(row);


            }
        

            DumpExcel(dtResult);

            #region old
            
            //      string runnigvaiable=""; 
           
      //      string fileName = Server.MapPath("/") + "Temp\\" + ddlReports.SelectedItem.Text.Trim() + ".xls";
            
      //      SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
      //     // Add a WorkbookPart to the document.
      //     WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
      //     workbookpart.Workbook = new Workbook();

      //     // Add a WorksheetPart to the WorkbookPart.
      //     WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
      //     worksheetPart.Worksheet = new Worksheet(new SheetData());

      //     // Add Sheets to the Workbook.
      //     Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
      //         AppendChild<Sheets>(new Sheets());

      //     // Append a new worksheet and associate it with the workbook.
      //     var index = 0;
      //      for (int i = 0; i < chkRunnningVariables.Items.Count;i++)
      //      {
      //             if (chkRunnningVariables.Items[i].Selected)
      //          {

                   
      //              runnigvaiable =chkRunnningVariables.Items[i].Value+"-"+ chkRunnningVariables.Items[i].Text ;
      //              break;
      //          }

      //      }
          
      //      Sheet sheet = new Sheet()
      //     {
      //         Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
      //         SheetId = 1,
      //         Name = runnigvaiable
      //     };

      

      //              var sheetData = new SheetData();
      //              var headerRow = new Row();
             
      //              List<String> columns = new List<string>();
      //                 Cell cell = new Cell();
      //                 cell.DataType = CellValues.String;
                      
      //                 columns.Add(dt[0].GeneralID+"-"+ dt[0].GeneralName );
      //                 cell.CellValue = new CellValue(dt[0].GeneralID + "-" + dt[0].GeneralName);
      //                 headerRow.AppendChild(cell);

      //                 cell = new Cell();
      //                  columns.Add(runningVariable);
      //                  cell.CellValue = new CellValue(runningVariable);
      //                  headerRow.AppendChild(cell);


      //      EpplusExcelDataProvider ep = new EpplusExcelDataProvider();
            


      //              sheetData.AppendChild(headerRow);
      //              Row newRow = new Row();
      //              foreach (var  dsrow in dt)
      //              {
                       
                       
      //                  foreach (String col in columns)
      //                  {
                            
                            
      //                      cell.CellValue = new CellValue(dsrow.Name.ToString()); //
      //                      newRow.AppendChild(cell.CloneNode(true));
      //                  }

      //                  sheetData.AppendChild(newRow.CloneNode(true));
      //              }
      ////      sheet.AppendChild(sheetData.CloneNode(true));

      //      sheets.Append(sheetData);

      //     workbookpart.Workbook.Save();

      //     // Close the document.
      //     spreadsheetDocument.Close();
      //      //try
      //      //{
      //      //    foreach (var dsrow in dt)
      //      //    {
      //      //        System.Data.OleDb.OleDbConnection MyConnection;
      //      //        System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
      //      //        string sql = null;
      //      //        MyConnection =
      //      //            new System.Data.OleDb.OleDbConnection(
      //      //                 "PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source='" + fileName + "';Extended Properties=Excel 8.0;");
      //      //        MyConnection.Open();
      //      //        myCommand.Connection = MyConnection;
      //      //        sql = "Insert into [" + runnigvaiable + "] (" + dt[0].GeneralName + "," + runningVariable + ") values('5','e')";
      //      //        myCommand.CommandText = sql;
      //      //        myCommand.ExecuteNonQuery();
      //      //        MyConnection.Close();
      //      //    }
      //      //}
      //      //catch (Exception ex)
      //      //{
      //      //    var x = ex.Message;
            //      //}
            #endregion
        }

        private void DumpExcel(System.Data.DataTable tbl)
        {
            int lang = (int)LMIS.Portal.Helpers.Utils.GetLanguage();

            var runningValues = MgrReports.GetReportsRunningValues(int.Parse(ddlReports.SelectedValue), lang);
             var theme = MgrReports.GetTheme(int.Parse(ddlReports.SelectedValue));
             var fixedVariables = MgrReports.GetReport(int.Parse(ddlReports.SelectedValue));
            System.Data.DataColumn columnUnit = new System.Data.DataColumn();
            columnUnit.ColumnName = "Measure Unit";
            columnUnit.DefaultValue = theme.UnitScale ;
            int counter = 0;
            string columns  = Excelcolumns(fixedVariables);
//            if (runningValues.Count < 1 || (runningValues.Count == 1 && runningValues[0].SubID.Substring(3)=="00001"))
//            {

//                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", @"  $(document).ready(function() {
//            return noty({
//                type: 'error',
//                text: '" + HttpContext.GetGlobalResourceObject("MessagesResource", "F11_ReportVariableNoData") + @"',)
//                layout: 'center', closeWith: ['click', 'backdrop'],
//                modal: true, killer: true
//            });
//
//            } );", true);

//                return;
//            }

            using (ExcelPackage pck = new ExcelPackage())
            {
               
                //Create the worksheet
                if (runningValues.Count>0)
                {

                    for (int i = 0; i < runningValues.Count; i++)
                    {
                        if (runningValues[i].SubID.Substring(3)=="00001")
                        {
                            continue;

                        }
                        counter++;
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Run" + counter);
                        System.Data.DataColumn column = new System.Data.DataColumn();
                        column.ColumnName = runningValues[i].GeneralID + "-" + runningValues[i].GeneralName;
                        column.DefaultValue = runningValues[i].SubID + "-" + runningValues[i].Name;
                        if (tbl.Columns.Contains(runningValues[i].GeneralID + "-" + runningValues[i].GeneralName))
                        {
                            tbl.Columns.Remove(runningValues[i].GeneralID + "-" + runningValues[i].GeneralName);
                            tbl.Columns.Remove("Value");
                            tbl.Columns.Remove("Measure Unit");

                        }
                        tbl.Columns.Add(column);
                        if (!tbl.Columns.Contains("Value"))
                        {
                            tbl.Columns.Add("Value");
                            tbl.Columns.Add(columnUnit);
                        }


                        //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1

                        ws.Cells["A1:H1"].Merge = true;
                        ws.Cells["A2:H2"].Merge = true;
                        ws.Cells["A3:H3"].Merge = true;
                        ws.Cells["A1:F1"].Value = theme.CodeNo + "-" + theme.Name;
                        ws.Cells["A2:F2"].Value = ddlReports.SelectedValue + "-" + ddlReports.SelectedItem.Text.Trim();

                        if (!string.IsNullOrEmpty(columns))
                        {
                            ws.Cells["A3:F3"].Value = columns.Substring(0, columns.Length - 1);
                        }
                        ws.Cells["A1:D3"].Style.Font.Bold = true;

                        ws.Cells["A4"].LoadFromDataTable(tbl, true);


                    }
                }
                else
                {
                   
                   
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Run0" );
                  
                    if (!tbl.Columns.Contains("Value"))
                    {
                        tbl.Columns.Add("Value");
                        tbl.Columns.Add(columnUnit);
                    }


                    //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1

                    ws.Cells["A1:H1"].Merge = true;
                    ws.Cells["A2:H2"].Merge = true;
                    ws.Cells["A3:H3"].Merge = true;
                    ws.Cells["A1:F1"].Value = theme.CodeNo + "-" + theme.Name;
                    ws.Cells["A2:F2"].Value = ddlReports.SelectedValue + "-" + ddlReports.SelectedItem.Text.Trim();

                    if (!string.IsNullOrEmpty(columns))
                    {
                        ws.Cells["A3:F3"].Value = columns.Substring(0, columns.Length - 1);
                    }
                    ws.Cells["A1:D3"].Style.Font.Bold = true;

                    ws.Cells["A4"].LoadFromDataTable(tbl, true);
                }

                ExcelPackage excelpack = new ExcelPackage();
                var fileName = ddlReports.SelectedValue + "-" + ddlReports.SelectedItem.Text.Trim() + ".xlsx";
                
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName);
                Response.StatusCode = 200;
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
                
            }
        }
        public string  Excelcolumns( ReportVM fixedVariables)
        {
            string  columns = "";

            columns += fixedVariables.YearID == null || (fixedVariables.YearTo != null && fixedVariables.YearID != null) ? "" : fixedVariables.YearID + "-" + Utils.VariableName("YearID") + ":" + MgrSubCode.GetSubCode(fixedVariables.YearID, 1) + ",";
            columns += fixedVariables.GenderID == null? "": fixedVariables.GenderID + "-" + Utils.VariableName("GenderID") + ":" +MgrSubCode.GetSubCode(fixedVariables.GenderID, 1) + ",";
            columns += fixedVariables.GovID == null ? "": fixedVariables.GovID + "-" + Utils.VariableName("GovID") + ":" +MgrSubCode.GetSubCode(fixedVariables.GovID, 1) + ",";
            columns += fixedVariables.MaritalStatusID == null ? "": fixedVariables.MaritalStatusID + "-" + Utils.VariableName("MaritalStatusID") + ":" +MgrSubCode.GetSubCode(fixedVariables.MaritalStatusID, 1) + ",";
            columns += fixedVariables.EducationLevelID == null ? "": fixedVariables.EducationLevelID + "-" + Utils.VariableName("EducationLevelID") + ":" +MgrSubCode.GetSubCode(fixedVariables.EducationLevelID, 1) + ",";
            columns += fixedVariables.AgeID == null ? "": fixedVariables.AgeID + "-" + Utils.VariableName("AgeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.AgeID, 1) + ",";
            columns += fixedVariables.SectorID == null? "": fixedVariables.SectorID + "-" + Utils.VariableName("SectorID") + ":" +MgrSubCode.GetSubCode(fixedVariables.SectorID, 1) + ",";
            columns += fixedVariables.CountryID == null? "": fixedVariables.CountryID + "-" + Utils.VariableName("CountryID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CountryID, 1) + ",";
            columns += fixedVariables.MonthID == null? "": fixedVariables.MonthID + "-" + Utils.VariableName("MonthID") + ":" +MgrSubCode.GetSubCode(fixedVariables.MonthID, 1) + ",";
            columns += fixedVariables.NationailtyID == null? "": fixedVariables.NationailtyID + "-" + Utils.VariableName("NationailtyID") + ":" +MgrSubCode.GetSubCode(fixedVariables.NationailtyID, 1) + ",";
            columns += fixedVariables.SchoolTypeID == null? "": fixedVariables.SchoolTypeID + "-" + Utils.VariableName("SchoolTypeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.SchoolTypeID, 1) + ",";
            columns += fixedVariables.EconomicActivityID == null? "": fixedVariables.EconomicActivityID + "-" + Utils.VariableName("EconomicActivityID") + ":" +MgrSubCode.GetSubCode(fixedVariables.EconomicActivityID, 1) + ",";
            columns += fixedVariables.GeographicalDistributionID == null? "": fixedVariables.GeographicalDistributionID + "-" + Utils.VariableName("GeographicalDistributionID") + ":" +MgrSubCode.GetSubCode(fixedVariables.GeographicalDistributionID, 1) + ",";
            columns += fixedVariables.GovernoratesGroupID == null? "": fixedVariables.GovernoratesGroupID + "-" + Utils.VariableName("GovernoratesGroupID") + ":" +MgrSubCode.GetSubCode(fixedVariables.GovernoratesGroupID, 1) + ",";
            columns += fixedVariables.GenderRatioID == null? "": fixedVariables.GenderRatioID + "-" + Utils.VariableName("GenderRatioID") + ":" +MgrSubCode.GetSubCode(fixedVariables.GenderRatioID, 1) + ",";
            columns += fixedVariables.EstablishmentID == null? "": fixedVariables.EstablishmentID + "-" + Utils.VariableName("EstablishmentID") + ":" +MgrSubCode.GetSubCode(fixedVariables.EstablishmentID, 1) + ",";
            columns += fixedVariables.UniversityID == null? "": fixedVariables.UniversityID + "-" + Utils.VariableName("UniversityID") + ":" +MgrSubCode.GetSubCode(fixedVariables.UniversityID, 1) + ",";
            columns += fixedVariables.FacultyID == null? "": fixedVariables.FacultyID + "-" + Utils.VariableName("FacultyID") + ":" +MgrSubCode.GetSubCode(fixedVariables.FacultyID, 1) + ",";
            columns += fixedVariables.InistitueID == null? "": fixedVariables.InistitueID + "-" + Utils.VariableName("InistitueID") + ":" +MgrSubCode.GetSubCode(fixedVariables.InistitueID, 1) + ",";
            columns += fixedVariables.DropOutID == null? "": fixedVariables.DropOutID + "-" + Utils.VariableName("DropOutID") + ":" +MgrSubCode.GetSubCode(fixedVariables.DropOutID, 1) + ",";
            columns += fixedVariables.TeahcingPositionsID == null? "": fixedVariables.TeahcingPositionsID + "-" + Utils.VariableName("TeahcingPositionsID") + ":" +MgrSubCode.GetSubCode(fixedVariables.TeahcingPositionsID, 1) + ",";
            columns += fixedVariables.WaterID == null? "": fixedVariables.WaterID + "-" + Utils.VariableName("WaterID") + ":" +MgrSubCode.GetSubCode(fixedVariables.WaterID, 1) + ",";
            columns += fixedVariables.WaterProducerID == null? "": fixedVariables.WaterProducerID + "-" + Utils.VariableName("WaterProducerID") + ":" +MgrSubCode.GetSubCode(fixedVariables.WaterProducerID, 1) + ",";
            columns += fixedVariables.RoadID == null? "": fixedVariables.RoadID + "-" + Utils.VariableName("RoadID") + ":" +MgrSubCode.GetSubCode(fixedVariables.RoadID, 1) + ",";
            columns += fixedVariables.VehicleID == null? "": fixedVariables.VehicleID + "-" + Utils.VariableName("VehicleID") + ":" +MgrSubCode.GetSubCode(fixedVariables.VehicleID, 1) + ",";
            columns += fixedVariables.TransportedItemID == null? "": fixedVariables.TransportedItemID + "-" + Utils.VariableName("TransportedItemID") + ":" +MgrSubCode.GetSubCode(fixedVariables.TransportedItemID, 1) + ",";
            columns += fixedVariables.CargoStatusTravelingID == null? "": fixedVariables.CargoStatusTravelingID + "-" + Utils.VariableName("CargoStatusTravelingID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CargoStatusTravelingID, 1) + ",";
            columns += fixedVariables.TravelStatusID == null? "": fixedVariables.TravelStatusID + "-" + Utils.VariableName("TravelStatusID") + ":" +MgrSubCode.GetSubCode(fixedVariables.TravelStatusID, 1) + ",";
            columns += fixedVariables.CommodityID == null? "": fixedVariables.CommodityID + "-" + Utils.VariableName("CommodityID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CommodityID, 1) + ",";
            columns += fixedVariables.CommodityGroupID == null? "": fixedVariables.CommodityGroupID + "-" + Utils.VariableName("CommodityGroupID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CommodityGroupID, 1) + ",";
            columns += fixedVariables.CaseTypeID == null? "": fixedVariables.CaseTypeID + "-" + Utils.VariableName("CaseTypeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CaseTypeID, 1) + ",";
            columns += fixedVariables.CaseStatusID == null? "": fixedVariables.CaseStatusID + "-" + Utils.VariableName("CaseStatusID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CaseStatusID, 1) + ",";
            columns += fixedVariables.AssociationsActivityID == null? "": fixedVariables.AssociationsActivityID + "-" + Utils.VariableName("AssociationsActivityID") + ":" +MgrSubCode.GetSubCode(fixedVariables.AssociationsActivityID, 1) + ",";
            columns += fixedVariables.CulturalServiceAssociationsID == null? "": fixedVariables.CulturalServiceAssociationsID + "-" + Utils.VariableName("CulturalServiceAssociationsID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CulturalServiceAssociationsID, 1) + ",";
            columns += fixedVariables.SocialServiceAssociationsID == null? "": fixedVariables.SocialServiceAssociationsID + "-" + Utils.VariableName("SocialServiceAssociationsID") + ":" +MgrSubCode.GetSubCode(fixedVariables.SocialServiceAssociationsID, 1) + ",";
            columns += fixedVariables.IssuedCapitalID == null? "": fixedVariables.IssuedCapitalID + "-" + Utils.VariableName("IssuedCapitalID") + ":" +MgrSubCode.GetSubCode(fixedVariables.IssuedCapitalID, 1) + ",";
            columns += fixedVariables.WaterPollutionIndicatorID == null? "": fixedVariables.WaterPollutionIndicatorID + "-" + Utils.VariableName("WaterPollutionIndicatorID") + ":" +MgrSubCode.GetSubCode(fixedVariables.WaterPollutionIndicatorID, 1) + ",";
            columns += fixedVariables.TransportingFacilityID == null? "": fixedVariables.TransportingFacilityID + "-" + Utils.VariableName("TransportingFacilityID") + ":" +MgrSubCode.GetSubCode(fixedVariables.TransportingFacilityID, 1) + ",";
            columns += fixedVariables.PortNameID == null? "": fixedVariables.PortNameID + "-" + Utils.VariableName("PortNameID") + ":" +MgrSubCode.GetSubCode(fixedVariables.PortNameID, 1) + ",";
            columns += fixedVariables.AirPollutionID == null? "": fixedVariables.AirPollutionID + "-" + Utils.VariableName("AirPollutionID") + ":" +MgrSubCode.GetSubCode(fixedVariables.AirPollutionID, 1) + ",";
            columns += fixedVariables.AreaDateID == null? "": fixedVariables.AreaDateID + "-" + Utils.VariableName("AreaDateID") + ":" +MgrSubCode.GetSubCode(fixedVariables.AreaDateID, 1) + ",";
            columns += fixedVariables.CropsSeasonID == null? "": fixedVariables.CropsSeasonID + "-" + Utils.VariableName("CropsSeasonID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CropsSeasonID, 1) + ",";
            columns += fixedVariables.liveStockID == null? "": fixedVariables.liveStockID + "-" + Utils.VariableName("liveStockID") + ":" +MgrSubCode.GetSubCode(fixedVariables.liveStockID, 1) + ",";
            columns += fixedVariables.OriginPlaceID == null? "": fixedVariables.OriginPlaceID + "-" + Utils.VariableName("OriginPlaceID") + ":" +MgrSubCode.GetSubCode(fixedVariables.OriginPlaceID, 1) + ",";
            columns += fixedVariables.BorrowingPurposeID == null? "": fixedVariables.BorrowingPurposeID + "-" + Utils.VariableName("BorrowingPurposeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.BorrowingPurposeID, 1) + ",";
            columns += fixedVariables.AuthoritySurveillanceID == null? "": fixedVariables.AuthoritySurveillanceID + "-" + Utils.VariableName("AuthoritySurveillanceID") + ":" +MgrSubCode.GetSubCode(fixedVariables.AuthoritySurveillanceID, 1) + ",";
            columns += fixedVariables.ServiceID == null? "": fixedVariables.ServiceID + "-" + Utils.VariableName("ServiceID") + ":" +MgrSubCode.GetSubCode(fixedVariables.ServiceID, 1) + ",";
            columns += fixedVariables.FisheryRegionID == null? "": fixedVariables.FisheryRegionID + "-" + Utils.VariableName("FisheryRegionID") + ":" +MgrSubCode.GetSubCode(fixedVariables.FisheryRegionID, 1) + ",";
            columns += fixedVariables.FoodProductsID == null? "": fixedVariables.FoodProductsID + "-" + Utils.VariableName("FoodProductsID") + ":" +MgrSubCode.GetSubCode(fixedVariables.FoodProductsID, 1) + ",";
            columns += fixedVariables.ChemicalProductID == null? "": fixedVariables.ChemicalProductID + "-" + Utils.VariableName("ChemicalProductID") + ":" +MgrSubCode.GetSubCode(fixedVariables.ChemicalProductID, 1) + ",";
            columns += fixedVariables.PaperPrintingProductID == null? "": fixedVariables.PaperPrintingProductID + "-" + Utils.VariableName("PaperPrintingProductID") + ":" +MgrSubCode.GetSubCode(fixedVariables.PaperPrintingProductID, 1) + ",";
            columns += fixedVariables.RubberPlasticProductID == null? "": fixedVariables.RubberPlasticProductID + "-" + Utils.VariableName("RubberPlasticProductID") + ":" +MgrSubCode.GetSubCode(fixedVariables.RubberPlasticProductID, 1) + ",";
            columns += fixedVariables.TextileProductID == null? "": fixedVariables.TextileProductID + "-" + Utils.VariableName("TextileProductID") + ":" +MgrSubCode.GetSubCode(fixedVariables.TextileProductID, 1) + ",";
            columns += fixedVariables.MetalicEngineeringElectricProductID == null? "": fixedVariables.MetalicEngineeringElectricProductID + "-" + Utils.VariableName("MetalicEngineeringElectricProductID") + ":" +MgrSubCode.GetSubCode(fixedVariables.MetalicEngineeringElectricProductID, 1) + ",";
            columns += fixedVariables.ConstrcutionsMaterialRefractoryProductID == null? "": fixedVariables.ConstrcutionsMaterialRefractoryProductID + "-" + Utils.VariableName("ConstrcutionsMaterialRefractoryProductID") + ":" +MgrSubCode.GetSubCode(fixedVariables.ConstrcutionsMaterialRefractoryProductID, 1) + ",";
            columns += fixedVariables.PetroluemNaturalGasProductID == null? "": fixedVariables.PetroluemNaturalGasProductID + "-" + Utils.VariableName("PetroluemNaturalGasProductID") + ":" +MgrSubCode.GetSubCode(fixedVariables.PetroluemNaturalGasProductID, 1) + ",";
            columns += fixedVariables.TradeID == null? "": fixedVariables.TradeID + "-" + Utils.VariableName("TradeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.TradeID, 1) + ",";
            columns += fixedVariables.CountriesGroupID == null? "": fixedVariables.CountriesGroupID + "-" + Utils.VariableName("CountriesGroupID") + ":" +MgrSubCode.GetSubCode(fixedVariables.CountriesGroupID, 1) + ",";
            columns += fixedVariables.ArrivalMethodID == null? "": fixedVariables.ArrivalMethodID + "-" + Utils.VariableName("ArrivalMethodID") + ":" +MgrSubCode.GetSubCode(fixedVariables.ArrivalMethodID, 1) + ",";
            columns += fixedVariables.HotelTypeID == null? "": fixedVariables.HotelTypeID + "-" + Utils.VariableName("HotelTypeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.HotelTypeID, 1) + ",";
            columns += fixedVariables.RadioStationID == null? "": fixedVariables.RadioStationID + "-" + Utils.VariableName("RadioStationID") + ":" +MgrSubCode.GetSubCode(fixedVariables.RadioStationID, 1) + ",";
            columns += fixedVariables.ProgramTypeID == null? "": fixedVariables.ProgramTypeID + "-" + Utils.VariableName("ProgramTypeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.ProgramTypeID, 1) + ",";
            columns += fixedVariables.SubjectTypeID == null? "": fixedVariables.SubjectTypeID + "-" + Utils.VariableName("SubjectTypeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.SubjectTypeID, 1) + ",";
            columns += fixedVariables.MuseumTypeID == null? "": fixedVariables.MuseumTypeID + "-" + Utils.VariableName("MuseumTypeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.MuseumTypeID, 1) + ",";
            columns += fixedVariables.IndustrySectionsID == null? "": fixedVariables.IndustrySectionsID + "-" + Utils.VariableName("IndustrySectionsID") + ":" +MgrSubCode.GetSubCode(fixedVariables.IndustrySectionsID, 1) + ",";
            columns += fixedVariables.EducationalTypeID == null? "": fixedVariables.EducationalTypeID + "-" + Utils.VariableName("EducationalTypeID") + ":" +MgrSubCode.GetSubCode(fixedVariables.EducationalTypeID, 1) + ",";
           
            return columns;
        }

        protected void ddlThemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LMIS.Portal.Helpers.Utils.LoggedUser == null)
            {
                Response.Redirect("~/login");
            }
            if (LMIS.Portal.Helpers.Utils.CheckPermission(1, 9, LMIS.Portal.Helpers.Utils.LoggedUser.Roles) < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", @" noty({
                    type: 'error',
                    text: '" + GetGlobalResourceObject("MessagesResource", "X_NotAuthorized") + @"',
                    layout: 'center', closeWith: ['click', 'backdrop'],
                    modal: true, killer: true
                });", true);
                return;
            }
            if (ddlThemeType.SelectedIndex < 1)
            {
                return;
            }
            int lang = (int)LMIS.Portal.Helpers.Utils.GetLanguage();
            var mr = Mgr.GetAThemesesByType(LMIS.Portal.Helpers.Utils.LoggedUser, ddlThemeType.SelectedValue, lang);
            ddlThemes.Items.Clear();
            ddlThemes.Items.Add(new ListItem("", "", true));
            ddlThemes.DataSource = mr.Data;
            ddlThemes.DataTextField = "Name";
            ddlThemes.DataValueField = "CodeNo";
            ddlThemes.DataBind();
        }
     

        protected void ddlThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
           
         var result=   MgrReports.GetReports(
            string.IsNullOrEmpty(ddlThemes.SelectedValue) ? 0 : int.Parse(ddlThemes.SelectedValue),
            ddlThemeType.SelectedValue, (int)LMIS.Portal.Helpers.Utils.GetLanguage());
            ddlReports.DataSource = result;
            var lang = LMIS.Portal.Helpers.Utils.GetLanguageStr();
            ddlReports.Items.Clear();
            ddlReports.DataTextField = "Report" + lang + "Name";
            ddlReports.DataValueField = "ReportID";
            ddlReports.DataBind();
        }
       
    }
}