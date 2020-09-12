using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Interfaces.Repositories;

namespace LMIS.Dal.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private LMISEntities Context = new LMISEntities();
        public List<Infrastructure.Data.Entities.ReportVM> GetReports()
        {
            var result = (from report in Context.Reports

                          join them in Context.DimThemes on report.ThemeID equals them.CodeNo
                     select
                     new ReportVM
                     {
                         ReportID = report.ReportID,
                         ReportArName = string.IsNullOrEmpty(report.ReportArName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportFrName : report.ReportEnName : report.ReportArName,
                         ReportEnName = string.IsNullOrEmpty(report.ReportEnName) ? string.IsNullOrEmpty(report.ReportArName) ? report.ReportFrName : report.ReportArName : report.ReportEnName,
                         ReportFrName = string.IsNullOrEmpty(report.ReportFrName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportArName : report.ReportEnName : report.ReportFrName,
                         
                         SourceAr = string.IsNullOrEmpty(report.SourceAr) ? string.IsNullOrEmpty(report.Source) ? report.SourceFr : report.Source : report.SourceAr,
                         Source = string.IsNullOrEmpty(report.Source) ? string.IsNullOrEmpty(report.SourceAr) ? report.SourceFr : report.SourceAr : report.Source,
                         SourceFr = string.IsNullOrEmpty(report.SourceFr) ? string.IsNullOrEmpty(report.Source) ? report.SourceAr : report.Source : report.SourceFr,
                         PublishYear = report.PublishYear,
                         ThemeID = report.ThemeID,
                         RunningVariableID = report.RunningVariableID,
                         changingVariableID = report.changingVariableID,
                         ThemeType=them.ThemeType
                         
                     }
                      );
            return result.ToList();
        }
        public List<Infrastructure.Data.Entities.ReportVM> GetReports(string themeType)
        {
            var result = (from report in Context.Reports

                          join them in Context.DimThemes on report.ThemeID equals them.CodeNo
                          where them.ThemeType == themeType
                          select
                          new ReportVM
                          {
                              ReportID = report.ReportID,
                              ReportArName = string.IsNullOrEmpty(report.ReportArName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportFrName : report.ReportEnName : report.ReportArName,
                              ReportEnName = string.IsNullOrEmpty(report.ReportEnName) ? string.IsNullOrEmpty(report.ReportArName) ? report.ReportFrName : report.ReportArName : report.ReportEnName,
                              ReportFrName = string.IsNullOrEmpty(report.ReportFrName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportArName : report.ReportEnName : report.ReportFrName,
                              SourceAr = string.IsNullOrEmpty(report.SourceAr) ? string.IsNullOrEmpty(report.Source) ? report.SourceFr : report.Source : report.SourceAr,
                              Source = string.IsNullOrEmpty(report.Source) ? string.IsNullOrEmpty(report.SourceAr) ? report.SourceFr : report.SourceAr : report.Source,
                              SourceFr = string.IsNullOrEmpty(report.SourceFr) ? string.IsNullOrEmpty(report.Source) ? report.SourceAr : report.Source : report.SourceFr,
                              PublishYear=report.PublishYear ,
                              ThemeID = report.ThemeID,
                              RunningVariableID = report.RunningVariableID,
                              changingVariableID = report.changingVariableID,
                              ThemeType = them.ThemeType

                          }
                      );
            return result.ToList();
        }
        public List<ReportVM> GetReports(int themeId, string themeTypeId, int languageId)
        {
            var result = (from report in Context.Reports
                          join them  in Context.DimThemes   on report.ThemeID equals  them.CodeNo
                          join genChange in Context.GeneralCodes on report.changingVariableID equals genChange.GeneralID into joinTable from z in joinTable.DefaultIfEmpty()
                          join genRunning in Context.GeneralCodes on report.RunningVariableID equals genRunning.GeneralID into joinTable1 from z1 in joinTable1.Where(table1 => table1.LanguageID == languageId).DefaultIfEmpty()
                          where (themeId==0||report.ThemeID == themeId )
                          && them.ThemeType == themeTypeId && z.LanguageID == languageId 
                          select
                             new ReportVM
                             {
                                 ReportID = report.ReportID,
                                 ReportArName = string.IsNullOrEmpty(report.ReportArName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportFrName : report.ReportEnName : report.ReportArName,
                                 ReportEnName = string.IsNullOrEmpty(report.ReportEnName) ? string.IsNullOrEmpty(report.ReportArName) ? report.ReportFrName : report.ReportArName : report.ReportEnName,
                                 ReportFrName = string.IsNullOrEmpty(report.ReportFrName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportArName : report.ReportEnName : report.ReportFrName,
                                 SourceAr = string.IsNullOrEmpty(report.SourceAr) ? string.IsNullOrEmpty(report.Source) ? report.SourceFr : report.Source : report.SourceAr,
                                 Source = string.IsNullOrEmpty(report.Source) ? string.IsNullOrEmpty(report.SourceAr) ? report.SourceFr : report.SourceAr : report.Source,
                                 SourceFr = string.IsNullOrEmpty(report.SourceFr) ? string.IsNullOrEmpty(report.Source) ? report.SourceAr : report.Source : report.SourceFr,
                                 PublishYear=report.PublishYear ,
                                 ThemeID = report.ThemeID,
                                 RunningVariableID = report.RunningVariableID,
                                 changingVariableID = report.changingVariableID,
                                 RunningVariableName = z1.Name,
                                 changingVariableName = z.Name
                                 
                             }
                              );
            return result.ToList();
        }

        public Infrastructure.Data.Entities.ReportVM GetReportsByID(int reportId, int languageId)
        {
            var result = (from report in Context.Reports
                join them in Context.DimThemes on report.ThemeID equals them.CodeNo
                join genRunning in Context.GeneralCodes on   report.RunningVariableID equals genRunning.GeneralID into joinTable1
                          from z1 in joinTable1.Where(table1 => table1.LanguageID == languageId).DefaultIfEmpty()  

                join genChange in Context.GeneralCodes on report.changingVariableID equals genChange.GeneralID   into joinTable
                from z in joinTable.DefaultIfEmpty()

                 where report.ReportID == reportId && z.LanguageID == languageId 
                select

                    new ReportVM
                    {

                        ThemeType = them.ThemeType,
                        
                        RunningVariableName = z1.Name,
                        changingVariableName = z.Name,
                        ReportID = report.ReportID,
                        ReportArName = string.IsNullOrEmpty(report.ReportArName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportFrName : report.ReportEnName : report.ReportArName,
                        ReportEnName = string.IsNullOrEmpty(report.ReportEnName) ? string.IsNullOrEmpty(report.ReportArName) ? report.ReportFrName : report.ReportArName : report.ReportEnName,
                        ReportFrName = string.IsNullOrEmpty(report.ReportFrName) ? string.IsNullOrEmpty(report.ReportEnName) ? report.ReportArName : report.ReportEnName : report.ReportFrName,
                        SourceAr = string.IsNullOrEmpty(report.SourceAr) ? string.IsNullOrEmpty(report.Source) ? report.SourceFr : report.Source : report.SourceAr,
                        Source = string.IsNullOrEmpty(report.Source) ? string.IsNullOrEmpty(report.SourceAr) ? report.SourceFr : report.SourceAr : report.Source,
                        SourceFr = string.IsNullOrEmpty(report.SourceFr) ? string.IsNullOrEmpty(report.Source) ? report.SourceAr : report.Source : report.SourceFr,
                        PublishYear = report.PublishYear,

                        ThemeID = report.ThemeID,
                        
                        ThemeNameAr = string.IsNullOrEmpty(them.NameAr) ? string.IsNullOrEmpty(them.Name) ? them.NameFr : them.Name : them.NameAr,
                        ThemeName = string.IsNullOrEmpty(them.Name) ? string.IsNullOrEmpty(them.NameAr) ? them.NameFr : them.NameAr : them.Name,
                        ThemeNameFr = string.IsNullOrEmpty(them.NameFr) ? string.IsNullOrEmpty(them.Name) ? them.NameAr : them.Name : them.NameFr,
                       
                        UnitScaleAr = string.IsNullOrEmpty(them.UnitScaleAr) ? string.IsNullOrEmpty(them.UnitScale) ? them.UnitScaleFr : them.UnitScale : them.UnitScaleAr,
                        UnitScale = string.IsNullOrEmpty(them.UnitScale) ? string.IsNullOrEmpty(them.UnitScaleAr) ? them.UnitScaleFr : them.UnitScaleAr : them.UnitScale,
                        UnitScaleFr = string.IsNullOrEmpty(them.UnitScaleFr) ? string.IsNullOrEmpty(them.UnitScale) ? them.UnitScaleAr : them.UnitScale : them.UnitScaleFr,
                       
                        RunningVariableID = report.RunningVariableID,
                        YearID = report.YearID,
                        YearTo = report.YearTo,
                        GenderID = report.GenderID,
                        GovID = report.GovID,
                        MaritalStatusID = report.MaritalStatusID,
                        EducationLevelID = report.EducationLevelID,
                        AgeID = report.AgeID,
                        SectorID = report.SectorID,
                        CountryID = report.CountryID,
                        MonthID = report.MonthID,
                        NationailtyID = report.NationailtyID,
                        SchoolTypeID = report.SchoolTypeID,
                        EconomicActivityID = report.EconomicActivityID,
                        CostID = report.CostID,
                        GeographicalDistributionID = report.GeographicalDistributionID,
                        GovernoratesGroupID = report.GovernoratesGroupID,
                        GenderRatioID = report.GenderRatioID,
                        EstablishmentID = report.EstablishmentID,
                        UniversityID = report.UniversityID,
                        FacultyID = report.FacultyID,
                        InistitueID = report.InistitueID,
                        DropOutID = report.DropOutID,
                        TeahcingPositionsID = report.TeahcingPositionsID,
                        WaterID = report.WaterID,
                        WaterProducerID = report.WaterProducerID,
                        RoadID = report.RoadID,
                        VehicleID = report.VehicleID,
                        TransportedItemID = report.TransportedItemID,
                        CargoStatusTravelingID = report.CargoStatusTravelingID,
                        TravelStatusID = report.TravelStatusID,
                        CommodityID = report.CommodityID,
                        CommodityGroupID = report.CommodityGroupID,
                        CaseTypeID = report.CaseTypeID,
                        CaseStatusID = report.CaseStatusID,
                        AssociationsActivityID = report.AssociationsActivityID,
                        CulturalServiceAssociationsID = report.CulturalServiceAssociationsID,
                        SocialServiceAssociationsID = report.SocialServiceAssociationsID,
                        IssuedCapitalID = report.IssuedCapitalID,
                        WaterPollutionIndicatorID = report.WaterPollutionIndicatorID,
                        TransportingFacilityID = report.TransportingFacilityID,
                        PortNameID = report.PortNameID,
                        AirPollutionID = report.AirPollutionID,
                        AreaDateID = report.AreaDateID,
                        CropsSeasonID = report.CropsSeasonID,
                        liveStockID = report.liveStockID,
                        OriginPlaceID = report.OriginPlaceID,
                        BorrowingPurposeID = report.BorrowingPurposeID,
                        AuthoritySurveillanceID = report.AuthoritySurveillanceID,
                        ServiceID = report.ServiceID,
                        FisheryRegionID = report.FisheryRegionID,
                        FoodProductsID = report.FoodProductsID,
                        ChemicalProductID = report.ChemicalProductID,
                        PaperPrintingProductID = report.PaperPrintingProductID,
                        RubberPlasticProductID = report.RubberPlasticProductID,
                        TextileProductID = report.TextileProductID,
                        MetalicEngineeringElectricProductID = report.MetalicEngineeringElectricProductID,
                        ConstrcutionsMaterialRefractoryProductID = report.ConstrcutionsMaterialRefractoryProductID,
                        PetroluemNaturalGasProductID = report.PetroluemNaturalGasProductID,
                        TradeID = report.TradeID,
                        CountriesGroupID = report.CountriesGroupID,
                        ArrivalMethodID = report.ArrivalMethodID,
                        HotelTypeID = report.HotelTypeID,
                        RadioStationID = report.RadioStationID,
                        ProgramTypeID = report.ProgramTypeID,
                        SubjectTypeID = report.SubjectTypeID,
                        MuseumTypeID = report.MuseumTypeID,
                        IndustrySectionsID = report.IndustrySectionsID,
                        EducationalTypeID = report.EducationalTypeID,
                        changingVariableID = report.changingVariableID,
                    }
                );
            return result.FirstOrDefault();
        }


        public List<SubCodeVm> GetReportsRunningValues(int reportId, int languageId)
        {
            throw new NotImplementedException();
        }


        public List<SubCodeVm> GetReportsChangingValues(int reportId, int languageId)
        {
            throw new NotImplementedException();
        }


        public ThemeVm GetTheme(int reportId)
        {
            var result = (from report in Context.Reports
                          join theme in  Context.DimThemes  on report.ThemeID equals theme.CodeNo 
                          where report.ReportID == reportId
                          select new ThemeVm
                          {
                             Name=theme.Name,
                             CodeNo=theme.CodeNo,
                             UnitScale=theme.UnitScale 

                          }

                           
                    );
            return result.FirstOrDefault();
        }
        public ReportVM GetReport(int reportId)
        {
            var result = (from report in Context.Reports

                          where report.ReportID == reportId
                          select new ReportVM
                          {
                              ReportID = report.ReportID,
                              ThemeID = report.ThemeID,
                              RunningVariableID = report.RunningVariableID,
                              YearID = report.YearID,
                              YearTo = report.YearTo,
                              GenderID = report.GenderID,
                              GovID = report.GovID,
                              MaritalStatusID = report.MaritalStatusID,
                              EducationLevelID = report.EducationLevelID,
                              AgeID = report.AgeID,
                              SectorID = report.SectorID,
                              CountryID = report.CountryID,
                              MonthID = report.MonthID,
                              NationailtyID = report.NationailtyID,
                              SchoolTypeID = report.SchoolTypeID,
                              EconomicActivityID = report.EconomicActivityID,
                              CostID = report.CostID,
                              GeographicalDistributionID = report.GeographicalDistributionID,
                              GovernoratesGroupID = report.GovernoratesGroupID,
                              GenderRatioID = report.GenderRatioID,
                              EstablishmentID = report.EstablishmentID,
                              UniversityID = report.UniversityID,
                              FacultyID = report.FacultyID,
                              InistitueID = report.InistitueID,
                              DropOutID = report.DropOutID,
                              TeahcingPositionsID = report.TeahcingPositionsID,
                              WaterID = report.WaterID,
                              WaterProducerID = report.WaterProducerID,
                              RoadID = report.RoadID,
                              VehicleID = report.VehicleID,
                              TransportedItemID = report.TransportedItemID,
                              CargoStatusTravelingID = report.CargoStatusTravelingID,
                              TravelStatusID = report.TravelStatusID,
                              CommodityID = report.CommodityID,
                              CommodityGroupID = report.CommodityGroupID,
                              CaseTypeID = report.CaseTypeID,
                              CaseStatusID = report.CaseStatusID,
                              AssociationsActivityID = report.AssociationsActivityID,
                              CulturalServiceAssociationsID = report.CulturalServiceAssociationsID,
                              SocialServiceAssociationsID = report.SocialServiceAssociationsID,
                              IssuedCapitalID = report.IssuedCapitalID,
                              WaterPollutionIndicatorID = report.WaterPollutionIndicatorID,
                              TransportingFacilityID = report.TransportingFacilityID,
                              PortNameID = report.PortNameID,
                              AirPollutionID = report.AirPollutionID,
                              AreaDateID = report.AreaDateID,
                              CropsSeasonID = report.CropsSeasonID,
                              liveStockID = report.liveStockID,
                              OriginPlaceID = report.OriginPlaceID,
                              BorrowingPurposeID = report.BorrowingPurposeID,
                              AuthoritySurveillanceID = report.AuthoritySurveillanceID,
                              ServiceID = report.ServiceID,
                              FisheryRegionID = report.FisheryRegionID,
                              FoodProductsID = report.FoodProductsID,
                              ChemicalProductID = report.ChemicalProductID,
                              PaperPrintingProductID = report.PaperPrintingProductID,
                              RubberPlasticProductID = report.RubberPlasticProductID,
                              TextileProductID = report.TextileProductID,
                              MetalicEngineeringElectricProductID = report.MetalicEngineeringElectricProductID,
                              ConstrcutionsMaterialRefractoryProductID = report.ConstrcutionsMaterialRefractoryProductID,
                              PetroluemNaturalGasProductID = report.PetroluemNaturalGasProductID,
                              TradeID = report.TradeID,
                              CountriesGroupID = report.CountriesGroupID,
                              ArrivalMethodID = report.ArrivalMethodID,
                              HotelTypeID = report.HotelTypeID,
                              RadioStationID = report.RadioStationID,
                              ProgramTypeID = report.ProgramTypeID,
                              SubjectTypeID = report.SubjectTypeID,
                              MuseumTypeID = report.MuseumTypeID,
                              IndustrySectionsID = report.IndustrySectionsID,
                              EducationalTypeID = report.EducationalTypeID,
                              changingVariableID = report.changingVariableID
                             
                          }


                    );

            return result.FirstOrDefault();
        }
        public int Save(ReportVM report)
        {

            var _rep = (from rep in Context.Reports
                        where rep.ThemeID == report.ThemeID
                        && string.IsNullOrEmpty(report.ReportEnName) || rep.ReportEnName == report.ReportEnName
                        && string.IsNullOrEmpty(report.ReportArName) || rep.ReportArName == report.ReportArName
                        && string.IsNullOrEmpty(report.ReportFrName) || rep.ReportFrName == report.ReportFrName
                        && rep.ReportID != report.ReportID

                        select rep).FirstOrDefault();

            if (_rep != null)
            {
                return 0;
            }
            if (report.ReportID > 0)
            {
                _rep = (from th in Context.Reports
                        where th.ThemeID == report.ThemeID && th.ReportID == report.ReportID

                        select th).FirstOrDefault();
                _rep.ReportEnName = report.ReportEnName;
                _rep.ReportArName = report.ReportArName;
                _rep.ReportFrName = report.ReportFrName;
                _rep.Source = report.Source;
                _rep.SourceAr = report.SourceAr;
                _rep.SourceFr = report.SourceFr;
                _rep.PublishYear = report.PublishYear;
                _rep.ThemeID = report.ThemeID;
                _rep.YearTo = report.YearTo;
                Context.Reports.Attach(_rep);

                Context.Entry(_rep).State = EntityState.Modified;
            }
            else
            {
                Report repor = new Report
                {
                    ReportID = report.ReportID,
                    ReportEnName = report.ReportEnName,
                    ReportArName = report.ReportArName,
                    ReportFrName = report.ReportFrName,
                     Source  = report.Source,
                    SourceAr  = report.SourceAr,
                    SourceFr = report.SourceFr,

                    ThemeID = report.ThemeID,
                    RunningVariableID = report.RunningVariableID,
                    changingVariableID = report.changingVariableID,
                    YearTo = report.YearTo
                };
                Context.Reports.Add(repor);
            }

            int affectedRows = Context.SaveChanges();
            return affectedRows;
        }
        public int Delete(int reportId)
        {
            int affectedRows = 0;
            var report = (from rep in Context.Reports
                          where rep.ReportID == reportId
                          select rep).FirstOrDefault();


            if (report != null)
            {

                //Delete it from memory
                Context.Reports.Remove(report);
                //Save to database
                affectedRows = Context.SaveChanges();
            }



            return affectedRows;
        }
    }
}
