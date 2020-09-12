using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Managers;
using LMIS.Utlities.Helpers;
using Microsoft.Reporting.WebForms;
using Statistics.Helpers;
using ReportParameter = Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution.ReportParameter;

namespace Statistics
{
    public class ReportsManagers
    {

        public string ConnectionString12 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private static readonly IReportsManager MgrReports = BllFactory.Singleton.Reports;
        private static readonly ISubCodeManager MgrSubCode = BllFactory.Singleton.SubCode;
        private static readonly IConfigCenterManager MgrConfigCenter = BllFactory.Singleton.ConfigCenterManager;
        public static int i;
        int ReportID;
        string ChangingVaribleID;
        string RunningVariable;
        string YearFrom;
        string ThemeName;
        private string UnitScale;
        string YearTo;
        Nullable<int> ThemeID;
        static int z;
        string[,] FixedVariables = new string[10, 2];
        public DataTable ds1 = new DataTable();

        public int CreateReport(string reportId, string nameEn, string nameAr, string nameFr, string source,
            string sourceAr, string sourceFr, string publishYear, string themes, string runningVariable,
            string changingVariable
            , string yearFrom, string yearTo, HtmlGenericControl mydiv)
        {
            SqlConnection con = new SqlConnection(ConnectionString12);

            SqlCommand cmd1 = new SqlCommand();
            string insertsql = string.Empty;
            if (string.IsNullOrEmpty(reportId))
            {
                insertsql =
                    "insert into Reports (ReportEnName,ReportArName, ReportFrName,Source,SourceAr,SourceFr,PublishYear, ThemeID, RunningVariableID, ChangingVariableID";
            }
            else
            {
                insertsql =
                    "update  Reports set ReportEnName=@ReportEnName,ReportArName=@ReportArName, ReportFrName=@ReportFrName,Source=@Source,SourceAr=@SourceAr,SourceFr=@SourceFr,PublishYear=@PublishYear, ThemeID=@ThemeID, RunningVariableID=@RunningVariableID, ChangingVariableID=@ChangingVariableID";

            }
            ArabicPrepocessor strp = new ArabicPrepocessor();
            cmd1.Parameters.Clear();
            cmd1.Parameters.Add("ReportEnName", SqlDbType.NVarChar);
            cmd1.Parameters["ReportEnName"].Value = strp.StripArabicWords(nameEn);
            cmd1.Parameters.Add("ReportArName", SqlDbType.NVarChar);
            cmd1.Parameters["ReportArName"].Value = strp.StripArabicWords(nameAr);
            cmd1.Parameters.Add("ReportFrName", SqlDbType.NVarChar);
            cmd1.Parameters["ReportFrName"].Value = strp.StripArabicWords(nameFr);
            cmd1.Parameters.Add("Source", SqlDbType.NVarChar);
            cmd1.Parameters["Source"].Value = strp.StripArabicWords(source);
            cmd1.Parameters.Add("SourceAr", SqlDbType.NVarChar);
            cmd1.Parameters["SourceAr"].Value = strp.StripArabicWords(sourceAr);
            cmd1.Parameters.Add("SourceFr", SqlDbType.NVarChar);
            cmd1.Parameters["SourceFr"].Value = strp.StripArabicWords(sourceFr);
            cmd1.Parameters.Add("PublishYear", SqlDbType.Int);
            if (String.IsNullOrEmpty(publishYear))
            {
                cmd1.Parameters["PublishYear"].Value = DBNull.Value;
            }
            else
            {
                cmd1.Parameters["PublishYear"].Value = publishYear;
            }

            cmd1.Parameters.Add("ThemeID", SqlDbType.NVarChar);
            cmd1.Parameters["ThemeID"].Value = themes;
            cmd1.Parameters.Add("RunningVariableID", SqlDbType.NVarChar);
            if (runningVariable == "selected")
            {
                cmd1.Parameters["RunningVariableID"].Value = DBNull.Value;
            }
            else
            {
                cmd1.Parameters["RunningVariableID"].Value = runningVariable;
            }

            cmd1.Parameters.Add("ChangingVariableID", SqlDbType.NVarChar);
            cmd1.Parameters["ChangingVariableID"].Value = changingVariable;

            if (runningVariable == "004" || changingVariable == "004")
            {
                if (string.IsNullOrEmpty(reportId))
                {
                    insertsql += ",YearID,YearTo";
                }
                else
                {
                    insertsql += ",YearID=@YearID,YearTo=@YearTo";
                }
                cmd1.Parameters.Add("YearTo", SqlDbType.NVarChar);
                cmd1.Parameters.Add("YearID", SqlDbType.NVarChar);

                cmd1.Parameters["YearID"].Value = yearFrom;
                cmd1.Parameters["YearTo"].Value = yearTo;


            }





            for (int y = 0; y < i; y++)
            {
                RadioButtonList VariablesRadio = (RadioButtonList) (mydiv.FindControl("VariablesRadio" + y));
                RadioButtonList VariableValuehck = (RadioButtonList) (mydiv.FindControl("VariablesValueChck" + y));
                //if (VariableValuehck.SelectedIndex < 0 || VariablesRadio.SelectedIndex < 0)
                //{
                //    VariableValuehck.SelectedIndex = 0;
                //    VariablesRadio.SelectedIndex = 0;
                //}
                if (VariablesRadio.SelectedIndex >= 0 && VariableValuehck.SelectedIndex >= 0)
                {
                    if (VariableValuehck.SelectedValue.Substring(3) != "00001" &&
                        VariablesRadio.SelectedValue != "unselect")
                    {
                        if (string.IsNullOrEmpty(reportId))
                        {
                            insertsql = insertsql + ",  " + VariablesRadio.SelectedValue.ToString();
                        }
                        else
                        {
                            insertsql = insertsql + ",  " + VariablesRadio.SelectedValue.ToString() + "='" +
                                        VariableValuehck.SelectedValue.ToString() + "'";
                        }
                    }
                }

            }
            if (string.IsNullOrEmpty(reportId))
            {
                insertsql = insertsql +
                            " ) values (@ReportEnName,@ReportArName, @ReportFrName,@Source,@SourceAr,@SourceFr,@PublishYear, @ThemeID, @RunningVariableID, @ChangingVariableID";
            }
            if (runningVariable == "004" || changingVariable == "004")
            {
                if (string.IsNullOrEmpty(reportId))
                {
                    insertsql += ",@YearID,  @YearTo";
                }


            }
            for (int y = 0; y < i; y++)
            {
                RadioButtonList VariablesRadio = (RadioButtonList) (mydiv.FindControl("VariablesRadio" + y));
                RadioButtonList VariableValuehck = (RadioButtonList) (mydiv.FindControl("VariablesValueChck" + y));
                if (VariablesRadio.SelectedIndex >= 0 && VariableValuehck.SelectedIndex >= 0)
                {

                    if (VariableValuehck.SelectedValue.Substring(3) != "00001" &&
                        VariablesRadio.SelectedValue != "unselect")
                    {
                        //if (VariableValuehck.SelectedIndex < 0 || VariablesRadio.SelectedIndex < 0)
                        //{
                        //    VariableValuehck.SelectedIndex = 0;
                        //    VariablesRadio.SelectedIndex = 0;
                        //}
                        if (VariablesRadio.SelectedIndex >= 0 && VariableValuehck.SelectedIndex >= 0)
                        {
                            if (string.IsNullOrEmpty(reportId))
                            {
                                insertsql = insertsql + ",'" + VariableValuehck.SelectedValue.ToString() + "'";
                            }

                        }
                    }
                }

            }
            if (string.IsNullOrEmpty(reportId))
            {
                insertsql = insertsql + " )";
            }

            if (!string.IsNullOrEmpty(reportId))
            {
                string column =
                    "YearID, YearTo, GenderID, GovID, MaritalStatusID,EducationLevelID,AgeID, SectorID, CountryID, MonthID, NationailtyID, SchoolTypeID, " +
                    "EconomicActivityID, CostID, GeographicalDistributionID, GovernoratesGroupID, GenderRatioID, EstablishmentID, AgeID3, UniversityID, FacultyID, " +
                    "InistitueID, DropOutID, TeahcingPositionsID, WaterID, WaterProducerID, RoadID, VehicleID, TransportedItemID, CargoStatusTravelingID, TravelStatusID, " +
                    "CommodityID, CommodityGroupID, CaseTypeID, CaseStatusID, AssociationsActivityID, CulturalServiceAssociationsID, SocialServiceAssociationsID, IssuedCapitalID, " +
                    "WaterPollutionIndicatorID, TransportingFacilityID, PortNameID, AirPollutionID, AreaDateID, CropsSeasonID, liveStockID, OriginPlaceID, BorrowingPurposeID, AuthoritySurveillanceID, " +
                    "ServiceID, FisheryRegionID, FoodProductsID, ChemicalProductID, PaperPrintingProductID, RubberPlasticProductID, TextileProductID, MetalicEngineeringElectricProductID, " +
                    "ConstrcutionsMaterialRefractoryProductID, PetroluemNaturalGasProductID, TradeID, CountriesGroupID, ArrivalMethodID, HotelTypeID, RadioStationID, ProgramTypeID, " +
                    "SubjectTypeID, MuseumTypeID, IndustrySectionsID, EducationalTypeID, ExtraVariableID, ExtraVariableValue";
                foreach (string itm in column.Split(new[] {','}))
                {
                    if (!insertsql.Contains(itm.Trim()))
                    {
                        insertsql += " , " + itm + " = null ";
                    }
                }
                insertsql += " where ReportID=" + reportId;
            }
            cmd1.Connection = con;
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = insertsql;
            con.Open();
            int result = cmd1.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public void  divReportsHtml(int lang,List<ReportVM> resultReport, List<SubCodeVm> resultThemeType, ref HtmlGenericControl divReports, ref HtmlGenericControl ulThemeType)
        {
            string strHtml = string.Empty;
            for (int i = 0; i < resultThemeType.Count; i++)
            {
                if (i == 0)
                {
                    ulThemeType.InnerHtml += " <li class='active'><a href='#tab1default" + i + "' data-toggle='tab'>" + resultThemeType[i].Name + "  </a></li>";
                }
                else
                {
                    ulThemeType.InnerHtml += " <li ><a href='#tab1default" + i + "' data-toggle='tab'>" + resultThemeType[i].Name + "  </a></li>";
                }
                var reportsTheme = resultReport.Where(k => k.ThemeType == resultThemeType[i].SubID).ToList();
                if (i == 0)
                {
                    divReports.InnerHtml += " <div class='tab-pane fade in active' id='tab1default" + i + "'>";
                }
                else
                {
                    divReports.InnerHtml += " <div class='tab-pane fade' id='tab1default" + i + "'>";
                }
                for (int j = 0; j < reportsTheme.Count; j++)
                {
                    if (resultThemeType[i].SubID == reportsTheme[j].ThemeType)
                    {
                        divReports.InnerHtml += "<a href='../HarmonizedData/View?repId=" + reportsTheme[j].ReportID.ToString() + @"' target='_blank' class='report-title-link' >
                                             <i class='fa fa-angle-double-right'></i>
                                               " + GetReportName(lang, reportsTheme[j]) + "</a> ";
                    }
                }

                divReports.InnerHtml += " </div> ";


            }
        }
        public  void LoadReport(int lang,ref ReportVM result,ref DataTable dtOtherData, string reportType , ref  DataTable rptds, ref ReportViewer Migration_Rport, ref Label NotificationLbl)
        {
            string[,] FixedVariables = new string[10, 2];
            int ReportID=0;
            if (HttpContext.Current.Request.QueryString["repId"] != null)
            {
                ReportID = Convert.ToInt16(HttpContext.Current.Request.QueryString["repId"]);
            }
           List<ConfigCenterVm> yearWindow=  MgrConfigCenter.GetValue("", 1, -1, 8);
             result = MgrReports.GetReportsByID(ReportID, lang);
            if (result != null)
            {
                ChangingVaribleID = result.changingVariableID;
                RunningVariable = result.RunningVariableID;
                YearFrom = result.YearID;
                YearTo = result.YearTo;
                ThemeID = result.ThemeID;
                ThemeName = result.ThemeName;
                UnitScale = result.UnitScale;
                

                SubCodeVm sub;
                foreach (PropertyInfo col in result.GetType().GetProperties())
                {
                    if (("YearID" == col.Name && RunningVariable == "004"))
                    {
                        continue;
                    }
                    if (col.GetValue(result, null) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(col.GetValue(result, null).ToString()))
                        {


                            if (
                                !"ReportID,ReportEnName,ReportArName,ReportFrName,ThemeID,ThemeType,UnitScale,UnitScaleAr,UnitScaleFr,ThemeName,ThemeNameAr,ThemeNameFr,RunningVariableID,YearTo,changingVariableID,RunningVariableName,changingVariableName"
                                    .Contains(col.Name))
                            {


                                sub = MgrSubCode.GetSubCodeModel(col.GetValue(result, null).ToString(), lang);
                                if (sub == null)
                                {
                                    switch (lang)
                                    {
                                        case 1:
                                            lang = 3;
                                            break;
                                        case 2:
                                            lang = 1;
                                            break;
                                        case 3:
                                            lang = 1;
                                            break;
                                    }
                                    sub = MgrSubCode.GetSubCodeModel(col.GetValue(result, null).ToString(), lang);
                                    if (sub == null)
                                    {
                                        switch (lang)
                                        {
                                            case 1:
                                                lang = 2;
                                                break;
                                            case 2:
                                                lang = 3;
                                                break;
                                            case 3:
                                                lang = 2;
                                                break;
                                        }
                                        sub = MgrSubCode.GetSubCodeModel(col.GetValue(result, null).ToString(), lang);
                                    }

                                }
                                if (sub != null)
                                {


                                    if (!dtOtherData.Columns.Contains("KeyName"))
                                    {
                                        dtOtherData.Columns.Add("KeyName");
                                        dtOtherData.Columns.Add("KeyValue");
                                    }
                                    DataRow dtRow = dtOtherData.NewRow();
                                    if ((result.changingVariableID == "004" || result.RunningVariableID == "004") &&
                                        col.Name.ToLower() == "yearid")
                                    {

                                    }
                                    else
                                    {
                                        dtRow[0] = sub.GeneralName + " : ";
                                        dtRow[1] = sub.Name;
                                        dtOtherData.Rows.Add(dtRow);
                                        FixedVariables[z, 0] = col.Name;
                                        FixedVariables[z, 1] = col.GetValue(result, null).ToString();
                                        z++;
                                    }

                                }
                                //   string Y = asd.Tables[0].Rows[0][i].ToString();
                                //  string x = asd.Tables[0].Columns[i].ColumnName;
                            }
                        }

                    }
                }
                z = 0;


            }
            HttpContext.Current.Session["dtOtherData"] = dtOtherData;
            SqlConnection con = new SqlConnection(ConnectionString12);
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            cmd.Connection = con;
            cmd1.Connection = con;
            if (ChangingVaribleID != "")
            {
                string Query = "select Queryfrom,VariableID,VariableColumnName from VariablesMapping where VariableID= '" + ChangingVaribleID + "' or VariableID= '" + RunningVariable + "'";

                string sqlstrg;
                //= "select * FROM   dbo.FactStatisticalData INNER JOIN dbo.Reports ON dbo.FactStatisticalData.ReportID = dbo.Reports.ReportID left outer JOIN dbo.SubCode AS subEducation ON dbo.FactStatisticalData.EducationLevelID = subEducation.SubID and subEducation.GeneralID = '" + ChangingVaribleID + "' left outer JOIN dbo.SubCode AS subGender ON dbo.FactStatisticalData.GenderID = subGender.SubID and subGender.GeneralID = '002' WHERE        (dbo.FactStatisticalData.ReportID = " + ReportID + ") order by subEducation.SubID Asc, subGender.SubID Asc";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = Query;
                DataSet ds = new DataSet();

                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);
                StringBuilder fixedColumn = new StringBuilder(
                    "YearID, GenderID, GovID, MaritalStatusID, EducationLevelID, AgeID, SectorID, CountryID, MonthID, NationailtyID, SchoolTypeID, " +
                    "EconomicActivityID, CostID, GeographicalDistributionID, GovernoratesGroupID, GenderRatioID, EstablishmentID, UniversityID, FacultyID, InistitueID, DropOutID, " +
                    "TeahcingPositionsID, WaterID, WaterProducerID, RoadID, VehicleID, TransportedItemID, CargoStatusTravelingID, TravelStatusID, CommodityID, CommodityGroupID, CaseTypeID, " +
                    "CaseStatusID, AssociationsActivityID, CulturalServiceAssociationsID, SocialServiceAssociationsID, IssuedCapitalID, WaterPollutionIndicatorID, TransportingFacilityID, " +
                    "PortNameID, AirPollutionID, AreaDateID, CropsSeasonID, liveStockID, OriginPlaceID, BorrowingPurposeID, AuthoritySurveillanceID, ServiceID, FisheryRegionID, FoodProductsID, " +
                    "ChemicalProductID, PaperPrintingProductID, RubberPlasticProductID, TextileProductID, MetalicEngineeringElectricProductID, ConstrcutionsMaterialRefractoryProductID, " +
                    "PetroluemNaturalGasProductID, TradeID, CountriesGroupID, ArrivalMethodID, HotelTypeID, RadioStationID, ProgramTypeID, SubjectTypeID, MuseumTypeID, IndustrySectionsID, " +
                    "EducationalTypeID");
               
                string queryYearWindow = !string.IsNullOrEmpty(yearWindow[0].Value) ? "   convert(int, subcode.name)>=" + yearWindow[0].Value : "  ";
                queryYearWindow += !string.IsNullOrEmpty(queryYearWindow) ? " and " : "";
                queryYearWindow += !string.IsNullOrEmpty(yearWindow[1].Value) ? "   convert(int, subcode.name )<=" + yearWindow[1].Value + " and " : "  ";

                if (ds.Tables[0].Rows.Count == 2)
                {
                    fixedColumn.Replace(ds.Tables[0].Rows[0][2] + ",", "");
                    fixedColumn.Replace(ds.Tables[0].Rows[1][2] + ",", "");
                    sqlstrg = " select distinct * " +
                              "FROM   dbo.FactStatisticalData " +
                              "INNER JOIN dbo.Reports ON dbo.Reports.ReportID=" + ReportID + " and dbo.FactStatisticalData.ThemeID = dbo.Reports.ThemeID "
                              + ds.Tables[0].Select("VariableID=" + ChangingVaribleID)[0][0] + lang + " "
                              + ds.Tables[0].Select("VariableID=" + RunningVariable)[0][0] + lang +
                              " left outer join dbo.subcode on FactStatisticalData.yearid=subcode.subid and subcode.generalid='004' and subcode.LanguageID=" + lang +
                              "WHERE    " + queryYearWindow + "    (dbo.FactStatisticalData.ThemeID = " + ThemeID + ") and FactStatisticalData." + ds.Tables[0].Rows[0][2] + " is not null and FactStatisticalData." + ds.Tables[0].Rows[1][2] + " is not null ";
                }
                else
                {

                    fixedColumn.Replace(ds.Tables[0].Rows[0][2] + ",", "");
                    sqlstrg = " select distinct * FROM   dbo.FactStatisticalData " +
                              "INNER JOIN dbo.Reports  ON dbo.Reports.ReportID=" + ReportID + " and dbo.FactStatisticalData.ThemeID = dbo.Reports.ThemeID " + ds.Tables[0].Rows[0][0].ToString() + lang +
                              " left outer join dbo.subcode on FactStatisticalData.yearid=subcode.subid and subcode.generalid='004' and subcode.LanguageID= " + lang +
                              " WHERE   " + queryYearWindow + "    (dbo.FactStatisticalData.ThemeID = " + ThemeID + ") and FactStatisticalData." + ds.Tables[0].Rows[0][2] + " is not null ";
                }
                con.Close();
                cmd.Dispose();
                ds.Dispose();
                adpt.Dispose();
                for (int x = 0; x < 10; x++)
                {
                    if (FixedVariables[x, 1] != null)
                    {
                        if (!FixedVariables[x, 1].Contains("00001"))
                        {
                            if (!string.IsNullOrEmpty(YearTo) && FixedVariables[x, 1].Substring(0, 3) == "004")
                            {
                            }
                            else
                            {
                                sqlstrg = sqlstrg + "and  FactStatisticalData." + FixedVariables[x, 0] + " = '" +
                                              FixedVariables[x, 1] + "'";
                                fixedColumn.Replace(FixedVariables[x, 0] + ",", "");
                            }
                        }
                    }
                }
                fixedColumn.Replace(",", " is null AND FactStatisticalData.");
                sqlstrg += "AND FactStatisticalData." + fixedColumn + " IS null";
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = sqlstrg;
                DataSet ds1 = new DataSet();

                con.Open();
                cmd1.ExecuteNonQuery();
                SqlDataAdapter adpt1 = new SqlDataAdapter(cmd1);
                adpt1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                   
                    if (ds.Tables[0].Rows.Count != 2)
                    {
                        System.Data.DataColumn newColumn = new System.Data.DataColumn("Name1", typeof(System.String));
                        newColumn.DefaultValue =HttpContext. GetGlobalResourceObject("CommonControls", "F22_Value");
                        if (!ds1.Tables[0].Columns.Contains("Name1"))
                            ds1.Tables[0].Columns.Add(newColumn);
                        ;
                    }


                    HttpContext.Current.Session["result"] = result;
                    HttpContext.Current.Session["ds1"] = ds1.Tables[0];
                  
                    HttpContext.Current.Session["ThemeName"] = ThemeName;
                    HttpContext.Current.Session["UnitScale"] = UnitScale;
                    rptds = ds1.Tables[0];
              
                }
                else
                {
                    NotificationLbl.Text = "Sorry, there's no available data";
                }
            }
            else
            {
                NotificationLbl.Text = "Sorry, there's no available data";
            }
        }
      
        public  string GetReportName(int lang, ReportVM result)
        {
            var value = "";
            switch (lang)
            {
                case 1: value = result.ReportEnName;
                    break;
                case 2: value = result.ReportFrName;
                    break;
                case 3: value = result.ReportArName;
                    break;
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 2: value = result.ReportEnName;
                        break;
                    case 3: value = result.ReportFrName;
                        break;
                    case 1: value = result.ReportArName;
                        break;
                }
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 3: value = result.ReportEnName;
                        break;
                    case 1: value = result.ReportFrName;
                        break;
                    case 2: value = result.ReportArName;
                        break;
                }
            }
            return value;
        }
        public  string GetReportSource(int lang, ReportVM result)
        {
            var value = "";
            switch (lang)
            {
                case 1: value = result.Source;
                    break;
                case 2: value = result.SourceFr;
                    break;
                case 3: value = result.SourceAr;
                    break;
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 2: value = result.Source;
                        break;
                    case 3: value = result.SourceFr;
                        break;
                    case 1: value = result.SourceAr;
                        break;
                }
            }
            if (string.IsNullOrEmpty(value))
            {
                switch (lang)
                {
                    case 3: value = result.Source;
                        break;
                    case 1: value = result.SourceFr;
                        break;
                    case 2: value = result.SourceAr;
                        break;
                }
            }
            return value;
        }
    
    }
}
