using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Infrastructure.Data.Entities
{
    public class ReportVM
    {
        public int ReportID { get; set; }
        public string ReportEnName { get; set; }
        public string ReportArName { get; set; }
        public string ReportFrName { get; set; }
        public Nullable<int >  ThemeID { get; set; }
        public string ThemeType { get; set; }
        public string ThemeName { get; set; }
        public string ThemeNameAr { get; set; }
        public string ThemeNameFr { get; set; }
        public string Source { get; set; }
        public string SourceAr { get; set; }
        public string SourceFr { get; set; }
        public Nullable<int> PublishYear { get; set; }
        public string UnitScale { get; set; }
        public string UnitScaleAr { get; set; }
        public string UnitScaleFr { get; set; }
        public string RunningVariableID { get; set; }
        public string changingVariableID { get; set; }
        public string RunningVariableName { get; set; }
        public string changingVariableName { get; set; }
        public string FixedVariable { get; set; }
        public string YearID { get; set; }
        public string YearTo { get; set; }
        public string GenderID { get; set; }
        public string GovID { get; set; }
        public string MaritalStatusID { get; set; }
        public string EducationLevelID { get; set; }
        public string AgeID { get; set; }
        public string SectorID { get; set; }
        public string CountryID { get; set; }
        public string MonthID { get; set; }
        public string NationailtyID { get; set; }
        public string SchoolTypeID { get; set; }
        public string EconomicActivityID { get; set; }
        public string CostID { get; set; }
        public string GeographicalDistributionID { get; set; }
        public string GovernoratesGroupID { get; set; }
        public string GenderRatioID { get; set; }
        public string EstablishmentID { get; set; }
        public string UniversityID { get; set; }
        public string FacultyID { get; set; }
        public string InistitueID { get; set; }
        public string DropOutID { get; set; }
        public string TeahcingPositionsID { get; set; }
        public string WaterID { get; set; }
        public string WaterProducerID { get; set; }
        public string RoadID { get; set; }
        public string VehicleID { get; set; }
        public string TransportedItemID { get; set; }
        public string CargoStatusTravelingID { get; set; }
        public string TravelStatusID { get; set; }
        public string CommodityID { get; set; }
        public string CommodityGroupID { get; set; }
        public string CaseTypeID { get; set; }
        public string CaseStatusID { get; set; }
        public string AssociationsActivityID { get; set; }
        public string CulturalServiceAssociationsID { get; set; }
        public string SocialServiceAssociationsID { get; set; }
        public string IssuedCapitalID { get; set; }
        public string WaterPollutionIndicatorID { get; set; }
        public string TransportingFacilityID { get; set; }
        public string PortNameID { get; set; }
        public string AirPollutionID { get; set; }
        public string AreaDateID { get; set; }
        public string CropsSeasonID { get; set; }
        public string liveStockID { get; set; }
        public string OriginPlaceID { get; set; }
        public string BorrowingPurposeID { get; set; }
        public string AuthoritySurveillanceID { get; set; }
        public string ServiceID { get; set; }
        public string FisheryRegionID { get; set; }
        public string FoodProductsID { get; set; }
        public string ChemicalProductID { get; set; }
        public string PaperPrintingProductID { get; set; }
        public string RubberPlasticProductID { get; set; }
        public string TextileProductID { get; set; }
        public string MetalicEngineeringElectricProductID { get; set; }
        public string ConstrcutionsMaterialRefractoryProductID { get; set; }
        public string PetroluemNaturalGasProductID { get; set; }
        public string TradeID { get; set; }
        public string CountriesGroupID { get; set; }
        public string ArrivalMethodID { get; set; }
        public string HotelTypeID { get; set; }
        public string RadioStationID { get; set; }
        public string ProgramTypeID { get; set; }
        public string SubjectTypeID { get; set; }
        public string MuseumTypeID { get; set; }
        public string IndustrySectionsID { get; set; }
        public string EducationalTypeID { get; set; }
    }
}
