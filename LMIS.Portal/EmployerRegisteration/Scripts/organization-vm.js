var OrganizationViewModel = function () {

    //Initialize parameters
    var self = this;
    var validLogoExtensions = ".png,.jpg,.gif";
    var validProfileExtensions = ".pdf,.doc,.docx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes

    //Constants Codes
    var countryCode = "009";
    var otherIndustryTypeCode = '09600004';

    //Initialize view model properties
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.OrganizationLogoPath = ko.observable().extend({ required: true });
    self.IndustryType = ko.observable();
    self.OrganizationName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.OtherIndustryType = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });

    self.Country = ko.observable().extend({ required: true });
    self.CountryOptions = ko.observableArray([]);
    self.City = ko.observable().extend({ required: true });
    self.CityOptions = ko.observableArray([]);
    self.EconomicActivity = ko.observable().extend({ required: true });
    self.EconomicActivityOptions = ko.observableArray([]);
    self.IndustryType = ko.observable();
    self.IndustryTypeOptions = ko.observableArray([]);
    self.OrganizationSize = ko.observable().extend({ required: true });
    self.OrganizationSizeOptions = ko.observableArray([]);
    self.YearsofExperienceID = ko.observable().extend({ required: true });
    self.YearsofExperienceIDOptions = ko.observableArray([]);

    self.ZipPostalCode = ko.observable().extend({ required: true });
    self.Telephone = ko.observable().extend({ required: true });
    self.OrganizationWebsite = ko.observable().extend({ pattern: '(https?:\/\/(?:www\.|(?!www))[^\s\.]+\.[^\s]{2,}|www\.[^\s]+\.[^\s]{2,})' });
    self.OrganizationProfilePath = ko.observable();
    self.EstablishmentDate = ko.observable().extend({ required: true, date: { dateFormat: 'dd-mm-yy' } });

    self.AcceptedLogoFiles = validLogoExtensions;
    self.Logo = ko.observable().extend({ required: true });
    self.ServerLogoName = ko.observable("empty");
    self.AcceptedProfileFiles = validProfileExtensions;
    self.Profile = ko.observable();
    self.ServerProfileName = ko.observable();

    //Portal user properties
    self.IDType = ko.observable().extend({ required: true });
    self.IDTypeOptions = ko.observableArray([]);
    self.IDNumber = ko.observable().extend({ required: true });
    self.TrainingProvider = ko.observable();
    self.Employer = ko.observable();
    self.TrainingSeeker = ko.observable();
    self.RegistrationNumberWithITC = ko.observable('');

    //Bind lookups
    lmis.api.SubCodes(self.IDTypeOptions, "013", function () {
        self.IDType(self.AsyncItems.Country);
    });
    lmis.api.SubCodes(self.CountryOptions, "009", function () {
        self.Country(self.AsyncItems.Country);
    });
    self.Country.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal);
    });
    lmis.api.SubCodes(self.EconomicActivityOptions, "032", function () {
        self.EconomicActivity(self.AsyncItems.EconomicActivity);
    });
    //lmis.api.SubCodes(self.IndustryTypeOptions, "096", function () {
    //    self.IndustryType(self.AsyncItems.IndustryType);
    //});
    self.EconomicActivity.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.IndustryTypeOptions, self.IndustryType, "096", newVal);
    });
    self.IndustryType.subscribe(function (newVal) {
        //$('#divOtherIndustry').hide();
        $('#divOtherIndustry').css('visibility', 'visible');
        if (newVal == otherIndustryTypeCode)
            //$('#divOtherIndustry').show();
            $('#divOtherIndustry').css('visibility', 'visible');
    });
    lmis.api.SubCodes(self.OrganizationSizeOptions, "098", function () {
        self.OrganizationSize(self.AsyncItems.OrganizationSize);
    });

    //TODO: add code with YearsOfExperince
    //DONE
    lmis.api.SubCodes(self.YearsofExperienceIDOptions, "099", function () {
        self.YearsofExperienceID(self.AsyncItems.YearsofExperienceID);
    });

    //----------------------------- Logo upload functions ---------------------------//
    self.ValidateLogo = function (item, e) {

        var selectedLogo = e.target.files[0];

        if (selectedLogo != null) {
            if (selectedLogo.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedLogo.name, validLogoExtensions)) {
                    self.Logo(selectedLogo);
                    $("#txtLogoName").val(selectedLogo.name);
                } else self.ClearLogo();
            } else self.ClearLogo();
        } else self.ClearLogo();

    }
    self.ClearLogo = function () {

        self.Logo(null);
        self.ServerLogoName(null);
        $("#txtLogoName").val("");
        lmis.fileInput.clear($("#hdnLogoBrowser"));

    }
    self.UploadLogo = function (onSuccess, onError) {

        if (!self.Logo()) {
            self.ClearLogo();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" + config.employer.logoFolder, self.Logo(), 0, "show/close",
            function (data) {
                self.ServerLogoName(data);
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearLogo();   //Validation Error
                onError();
            });
    }
    // Profile upload functions
    self.ValidateProfile = function (item, e) {

        var selectedProfile = e.target.files[0];

        if (selectedProfile != null) {
            if (selectedProfile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedProfile.name, validProfileExtensions)) {
                    self.Profile(selectedProfile);
                    $("#txtProfileName").val(selectedProfile.name);
                } else self.ClearProfile();
            } else self.ClearProfile();
        } else self.ClearProfile();

    }
    self.ClearProfile = function () {

        self.Profile(null);
        self.ServerProfileName(null);
        $("#txtProfileName").val("");
        lmis.fileInput.clear($("#hdnProfileBrowser"));

    }
    self.UploadProfile = function () {

        if (!self.Profile()) {
            self.ClearProfile();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/UploadDocWithPath?path=" + config.employer.profileFolder, self.Profile(), 0, "show/close",
            function (data) {
                self.ServerProfileName(data);
                //onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearProfile();   //Validation Error
                //onError();
            });
    }

    //Display Controls
    //$('#divOtherIndustry').hide();
    self.isOtherIndustryTypeVisiable = ko.dependentObservable(function () {
        return (self.IndustryType() == otherIndustryTypeCode)
        //if (newVal == otherIndustryTypeCode)
        //    $('#divOtherIndustry').show();
    });

    //ko.bindingHandlers.stopBinding = {
    //    init: function () {
    //        return { controlsDescendantBindings: true };
    //    }
    //};
    //ko.virtualElements.allowedBindings.stopBinding = true;

    self.firstStepValidation = [
        //TODO: validate manual
   self.OrganizationName,
   self.Address,
   self.ServerLogoName,
   self.OrganizationSize,
   self.Country,
   self.City,
   self.ZipPostalCode,
   self.Telephone,
   self.EconomicActivity,
   self.IndustryType,
   self.EstablishmentDate,
   self.YearsofExperienceID,
   self.IDType,
   self.IDNumber,
   self.OrganizationWebsite
    ];

    //self.errors = ko.validation.group(StepValidation);
    //self.errors = ko.validation.group(self);
    //self.errors.showAllMessages(false);
    return self;
}

var hasValue = function (v, val) {
    if (val == 'undefined') return true;
    else return (val != '')
    //return (val.isNullOrWhiteSpace())
}