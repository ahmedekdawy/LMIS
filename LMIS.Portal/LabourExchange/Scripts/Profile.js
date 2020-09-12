function EmployerViewModel() {

    //Initialize parameters
    var self = this;
    var mode = lmis.queryString["m"], key = parseInt(lmis.queryString["k"]);

    if (!mode || isNaN(key)) mode = "p";
    else mode = mode.toLowerCase();

    if (!key || isNaN(key)) key = 0;

    //Initialize view model properties
    self.vmReview = window.vmReview;
    self.Mode = ko.observable(mode);
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.OrganizationLogoPath = ko.observable();
    self.IndustryType = ko.observable();
    self.OrganizationName = ko.observable();
    self.Address = ko.observable();
    self.OtherIndustryType = ko.observable();
    self.Country = ko.observable();
    self.City = ko.observable();
    self.EconomicActivity = ko.observable();
    self.IndustryType = ko.observable();
    self.OrganizationSize = ko.observable();
    self.YearsofExperience = ko.observable();
    self.ZipPostalCode = ko.observable();
    self.Telephone = ko.observable();
    self.OrganizationWebsite = ko.observable();
    self.OrganizationProfilePath = ko.observable();
    self.EstablishmentDate = ko.observable().extend({ required: true, date: { dateFormat: lmis.x.momentDateFormat } });
    self.ServerLogoName = ko.observable();
    self.Profile = ko.observable();
    self.ServerProfileName = ko.observable();
    self.GS = ko.observable();
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();

    //Portal user properties
    self.IDType = ko.observable();
    self.IDNumber = ko.observable();
    self.TrainingProvider = ko.observable();
    self.UserCategory = ko.observable();
    self.UserSubCategory = ko.observable();
    self.Employer = ko.observable();
    self.TrainingSeeker = ko.observable();
    self.JobSeeker = ko.observable();
    self.Researcher = ko.observable();
    self.Internal = ko.observable();
    self.IsSubscriper = ko.observable();
    self.RegistrationNumberWithITC = ko.observable('');
    self.IsDiscalaimerApproved = ko.observable(true);

    //----------------------------- Operations ---------------------------//
    self.Load = function () {
        lmis.ajax("../LabourExchange/Profile.aspx/GetProfile", { id: key, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.OrganizationName(data.d.OrganizationName);
                    self.Address(data.d.Address);
                    self.OtherIndustryType(data.d.OtherIndustryType);
                    (data.d.OrganizationLogoPath != null) ? self.ServerLogoName((data.d.OrganizationLogoPath.length > 0) ? config.employer.logoPath + data.d.OrganizationLogoPath : config.employer.defaultLogoPath) : self.PhotoPath(config.Employer.defaultLogoPath),
                    self.OrganizationSize(data.d.OrganizationSize),
                    self.Country(data.d.Country),
                    self.City(data.d.City),
                    self.ZipPostalCode(data.d.ZipPostalCode),
                    self.Telephone(data.d.Telephone),
                    self.OrganizationWebsite(data.d.OrganizationWebsite),
                    self.ServerProfileName(data.d.OrganizationProfilePath),
                    self.EconomicActivity(data.d.EconomicActivity),
                    self.IndustryType(data.d.IndustryType),
                    self.YearsofExperience(data.d.YearsofExperienceID),
                    self.EstablishmentDate(lmis.format.dateToString(data.d.EstablishmentDate));
                    self.RegistrationNumberWithITC(data.d.RegistrationNumberWithITC),
                    self.GS(data.d.GS);
                    self.Approval(data.d.Approval);
                    self.RejectReason(data.d.RejectReason);
                    self.IsDiscalaimerApproved(data.d.IsDiscalaimerApproved), //TODO: Activate property

                    //Portal user object
                    self.IDType(data.d.IDType),
                    self.IDNumber(data.d.IDNumber),
                    self.UserCategory(data.d.UserCategory),
                    self.UserSubCategory(data.d.UserSubCategory),
                    self.TrainingProvider(data.d.TrainingProvider),
                    self.Employer(data.d.Employer),
                    self.TrainingSeeker(data.d.TrainingSeeker),
                    self.JobSeeker(data.d.JobSeeker),
                    self.Researcher(data.d.Researcher),
                    self.Internal(data.d.Internal),
                    self.IsSubscriper(data.d.IsSubscriper);

                    //Admin Review?
                    if (window.vmReview) window.vmReview.init("orgProfile", key);
                }
            });
    }

    self.Load();
}

$(document).ready(function() {
    window.vm = new EmployerViewModel();
    ko.applyBindings(vm);
});