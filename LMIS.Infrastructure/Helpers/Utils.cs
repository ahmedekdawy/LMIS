using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LMIS.Infrastructure.Helpers
{
    public static class Utils
    {
        public static List<Dictionary<string, LocalString>> MultilingualDataSet(Dictionary<string, GlobalString> columns)
        {
            var ds = new List<Dictionary<string, LocalString>>();

            foreach (Language lang in Enum.GetValues(typeof(Language)))
            {
                var tr = columns.ToDictionary(c => c.Key, c => c.Value.ToLocalString(lang));
                if (tr.Values.Any(v => !string.IsNullOrWhiteSpace(v.T))) ds.Add(tr);
            }

            return ds;

        }
        public static Language GetLanguage(string langCode)
        {
            var ret = Language.English;

            switch (langCode.Trim().ToLower())
            {
                case "ar":
                    ret = Language.Arabic;
                    break;
                case "fr":
                    ret = Language.French;
                    break;
            }

            return ret;
        }

        public static string VariableName(string variable)
        {
            switch (variable)
            {
                case "002": variable = "GenderID"; break;
                case "003": variable = "GovID"; break;
                case "004": variable = "YearID"; break;
                case "005": variable = "MaritalStatusID"; break;
                case "006": variable = "EducationLevelID"; break;
                case "007": variable = "AgeID"; break;
                case "008": variable = "SectorID"; break;
                case "009": variable = "CountryID"; break;
                case "010": variable = "MonthID"; break;
                case "011": variable = "NationailtyID"; break;
                case "016": variable = "SchoolTypeID"; break;
                case "032": variable = "EconomicActivityID"; break;
                case "033": variable = "CostID"; break;
                case "034": variable = "GeographicalDistributionID"; break;
                case "035": variable = "GovernoratesGroupID"; break;
                case "036": variable = "GenderRatioID"; break;
                case "037": variable = "EstablishmentID"; break;
                case "038": variable = "UniversityID"; break;
                case "039": variable = "FacultyID"; break;
                case "040": variable = "InistitueID"; break;
                case "041": variable = "DropOutID"; break;
                case "042": variable = "TeahcingPositionsID"; break;
                case "043": variable = "WaterID"; break;
                case "044": variable = "WaterProducerID"; break;
                case "045": variable = "RoadID"; break;
                case "046": variable = "VehicleID"; break;
                case "047": variable = "TransportedItemID"; break;
                case "048": variable = "CargoStatusTravelingID"; break;
                case "049": variable = "TravelStatusID"; break;
                case "050": variable = "CommodityID"; break;
                case "051": variable = "CommodityGroupID"; break;
                case "052": variable = "CaseTypeID"; break;
                case "053": variable = "CaseStatusID"; break;
                case "054": variable = "AssociationsActivityID"; break;
                case "055": variable = "CulturalServiceAssociationsID"; break;
                case "056": variable = "SocialServiceAssociationsID"; break;
                case "057": variable = "IssuedCapitalID"; break;
                case "058": variable = "WaterPollutionIndicatorID"; break;
                case "059": variable = "TransportingFacilityID"; break;
                case "060": variable = "PortNameID"; break;
                case "061": variable = "AirPollutionID"; break;
                case "062": variable = "AreaDateID"; break;
                case "063": variable = "CropsSeasonID"; break;
                case "064": variable = "liveStockID"; break;
                case "065": variable = "OriginPlaceID"; break;
                case "066": variable = "BorrowingPurposeID"; break;
                case "067": variable = "AuthoritySurveillanceID"; break;
                case "068": variable = "ServiceID"; break;
                case "069": variable = "FisheryRegionID"; break;
                case "070": variable = "FoodProductsID"; break;
                case "071": variable = "ChemicalProductID"; break;
                case "072": variable = "PaperPrintingProductID"; break;
                case "073": variable = "RubberPlasticProductID"; break;
                case "074": variable = "TextileProductID"; break;
                case "075": variable = "MetalicEngineeringElectricProductID"; break;
                case "076": variable = "ConstrcutionsMaterialRefractoryProductID"; break;
                case "077": variable = "PetroluemNaturalGasProductID"; break;
                case "078": variable = "TradeID"; break;
                case "079": variable = "CountriesGroupID"; break;
                case "080": variable = "ArrivalMethodID"; break;
                case "081": variable = "HotelTypeID"; break;
                case "082": variable = "RadioStationID"; break;
                case "083": variable = "ProgramTypeID"; break;
                case "084": variable = "SubjectTypeID"; break;
                case "085": variable = "MuseumTypeID"; break;
                case "086": variable = "IndustrySectionsID"; break;
                case "087": variable = "EducationalTypeID"; break;

            }
            return variable;
        }

    }
}