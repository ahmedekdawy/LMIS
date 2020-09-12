ko.validation.rules.pattern.message = 'Invalid.';

ko.validation.init({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null,
    decorateElement: true,
}, true);

function EmployerViewModel() {

    //Initialize parameters
    var self = this;
    var validLogoExtensions = ".png,.jpg,.gif";
    var validProfileExtensions = ".pdf,.doc,.docx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var cat = lmis.queryString["cat"];
    var subCat = lmis.queryString["subcat"];
    var n; //Noty Message Object

    //Constants Codes
    var governmentCode = '10000001';
    var privateCode = '10000002';
    var selfEmployedCode = '10000003';

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
    self.UserCategory = ko.observable();
    self.UserSubCategory = ko.observable();
    self.Employer = ko.observable();
    self.TrainingSeeker = ko.observable();
    self.JobSeeker = ko.observable();
    self.Researcher = ko.observable();
    self.Internal = ko.observable();
    self.IsSubscriper = ko.observable();
    self.RegistrationNumberWithITC = ko.observable('');
    self.Is_Approved = ko.observable(1);
    self.IsDiscalaimerApproved = ko.observable(true);

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

    self.orgValidation = [
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
    //----------------------------- Operations ---------------------------//
    self.Load = function () {

        lmis.ajax("../LabourExchange/OrgAccount.aspx/GetDetails", null, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.OrganizationName.Populate(data.d.res.OrganizationNameLocalized.English, data.d.res.OrganizationNameLocalized.French, data.d.res.OrganizationNameLocalized.Arabic);
                    self.Address.Populate(data.d.res.AddressLocalized.English, data.d.res.AddressLocalized.French, data.d.res.AddressLocalized.Arabic);
                    self.OtherIndustryType.Populate(data.d.res.OtherIndustryTypeLocalized.English, data.d.res.OtherIndustryTypeLocalized.French, data.d.res.OtherIndustryTypeLocalized.Arabic);

                    self.ServerLogoName(data.d.res.OrganizationLogoPath),
                    self.OrganizationSize(data.d.res.OrganizationSize),
                    self.Country(data.d.res.CountryID),
                    self.City(data.d.res.CityID),
                    self.ZipPostalCode(data.d.res.ZipPostalCode),
                    self.Telephone(data.d.res.Telephone),
                    self.OrganizationWebsite(data.d.res.OrganizationWebsite),
                    self.ServerProfileName(data.d.res.OrganizationProfilePath),
                    self.EconomicActivity(data.d.res.EconomicActivity),
                    self.IndustryType(data.d.res.IndustryType),
                    self.YearsofExperienceID(data.d.res.YearsofExperienceID),
                    self.EstablishmentDate(lmis.format.dateToString(data.d.res.EstablishmentDate));
                    self.RegistrationNumberWithITC(data.d.res.RegistrationNumberWithITC),
                    self.Is_Approved(data.d.res.Is_Approved),// organization status should be PENDING
                     self.IsDiscalaimerApproved(data.d.res.IsDiscalaimerApproved),//TODO: Activate property


                    //Portal user object
                        self.IDType(data.d.res.PortalUser.IDType),
                        self.IDNumber(data.d.res.PortalUser.IDNumber),
                       self.UserCategory(data.d.res.PortalUser.UserCategory),
                        self.UserSubCategory(data.d.res.PortalUser.UserSubCategory),
                        self.TrainingProvider(data.d.res.PortalUser.TrainingProvider),
                        self.Employer(data.d.res.PortalUser.Employer),
                        self.TrainingSeeker(data.d.res.PortalUser.TrainingSeeker),
                        self.JobSeeker(data.d.res.PortalUser.JobSeeker),
                        self.Researcher(data.d.res.PortalUser.Researcher),
                        self.Internal(data.d.res.PortalUser.Internal),
                        self.IsSubscriper(data.d.res.PortalUser.IsSubscriper)

                    //Localize Trilingual Text Views
                    self.OrganizationName.LocalizeView(false);
                    self.Address.LocalizeView(false);
                    self.OtherIndustryType.LocalizeView(false);
                }
            });
    }

    self.Validate = function () {
        var errors = ko.validation.group(orgValidation);

        if (!self.ValidateMultiLangControls() || errors().length > 0) {
            errors.showAllMessages();//!self.ValidateMultiLangControls() ||
            return false;
        }
        else
            return true;
    }
    self.ValidateMultiLangControls = function () {
        var isValid = true;
        if (self.OrganizationName.isNullOrWhiteSpace()) {
            $('#OrganizationNameValidation').show();
            isValid = false;
        }
        else
            $('#OrganizationNameValidation').hide();
        if (self.Address.isNullOrWhiteSpace()) {
            $('#AddressValidation').show();
            isValid = false;
        }
        else
            $('#AddressValidation').hide();

        return isValid;

    }
    self.StartWorkflow = function () {
        self.Step1();
    }
    self.WorkflowSuccess = function () {
        lmis.notification.success();
    }
    self.WorkflowError = function () {
        lmis.notification.error();
    }
    self.Step1 = function () {
        var isValid = self.Validate();
        if (!isValid) {
            lmis.notification.error();
            return;
        }
        //Submit ViewModel for Server-Side Validation of Business Rules
        n = lmis.notification.progress($("#Step1").html());

        if (!self.Logo() && !self.Profile())
            self.Step3();
        else
            self.Step2(true, self.Save, self.WorkflowError);

    }
    self.Step2 = function (validateOnly, onSuccess, onError) {

        if (!self.Logo()) return;


        if (self.Profile()) {
            var ajaxUploadProfileRequest = self.UploadProfile();
            if (!ajaxUploadProfileRequest) {
                self.WorkflowError();
                return;
            };
        };

        //Upload and Validate Selected File
        var ajaxUploadLogoRequest = self.UploadLogo();
        if (!ajaxUploadLogoRequest) {
            self.WorkflowError();
            return;
        };

        onSuccess();
    }
    self.Step3 = function () {

        //Save ViewModel to DB with reference to Uploaded File
        n.setText($("#Step3").html());
        self.Save(false, self.WorkflowSuccess, self.WorkflowError);
    }
    self.Save = function (validateOnly, onSuccess, onError) {

        var dto = {
            validateOnly: true,
            organizationObject: {

                //Organization object
                OrganizationLogoPath: self.ServerLogoName(),
                OrganizationSize: self.OrganizationSize(),
                CountryID: self.Country(),
                CityID: self.City(),
                ZipPostalCode: self.ZipPostalCode(),
                Telephone: self.Telephone(),
                OrganizationWebsite: self.OrganizationWebsite(),
                OrganizationProfilePath: self.ServerProfileName(),
                EconomicActivity: self.EconomicActivity(),
                IndustryType: self.IndustryType(),
                YearsofExperienceID: self.YearsofExperienceID(),
                EstablishmentDate: self.EstablishmentDate.Value,
                RegistrationNumberWithITC: self.RegistrationNumberWithITC(),
                Is_Approved: self.Is_Approved(),
                IsDiscalaimerApproved: self.IsDiscalaimerApproved(),
                //Translation object
                Translation:
                    [{
                        LanguageID: 1,
                        OrganizationName: self.OrganizationName.getValue().English,
                        Address: self.Address.getValue().English,
                        OtherIndustryType: self.OtherIndustryType.getValue().English,
                    },
                    {
                        LanguageID: 2,
                        OrganizationName: self.OrganizationName.getValue().French,
                        Address: self.Address.getValue().French,
                        OtherIndustryType: self.OtherIndustryType.getValue().French,
                    },
                    {
                        LanguageID: 3,
                        OrganizationName: self.OrganizationName.getValue().Arabic,
                        Address: self.Address.getValue().Arabic,
                        OtherIndustryType: self.OtherIndustryType.getValue().Arabic,
                    }],

                //Portal user object
                PortalUser: {
                    IDType: self.IDType(),
                    IDNumber: self.IDNumber(),
                    UserCategory: self.UserCategory(),
                    UserSubCategory: self.UserSubCategory(),
                    TrainingProvider: self.TrainingProvider(),
                    Employer: self.Employer(),
                    TrainingSeeker: self.TrainingSeeker(),
                    //TODO: Handel below paremeters
                    JobSeeker: false,
                    //TrainingSeeker: false,
                    Researcher: false,
                    Internal: false,
                    IsSubscriper: false
                }

            }
        };

        return lmis.ajax("../LabourExchange/OrgAccount.aspx/Update", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d)
                    lmis.notification.success();
                //onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                //onError();
            });
    };

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
    self.UploadLogo = function () {

        if (!self.Logo()) {
            self.ClearLogo();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" + config.employer.logoFolder, self.Logo(), 0, "show/close",
            function (data) {
                self.ServerLogoName(data);
                //onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearLogo();   //Validation Error
                //onError();
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
    self.isOtherIndustryTypeVisiable = ko.dependentObservable(function () {
        return (self.IndustryType() == otherIndustryTypeCode)
        //if (newVal == otherIndustryTypeCode)
        //    $('#divOtherIndustry').show();
    });
    setTimeout(function () {
        self.Load();
    }, 1500);
}

$(ko.applyBindings(EmployerViewModel()));

//Display Controls
$('#OrganizationNameValidation').hide();
$('#AddressValidation').hide();
$('#ConatactPersonFullNameValidation').hide();
$('#ConatactPersonDepartmentValidation').hide();

if (lmis.queryString["cat"] != 'TP') {
    $('#divRegistrationNumberWithITC').hide();
}
else
    $('#divRegistrationNumberWithITC').show();

if (lmis.queryString["subcat"] == 'GOV') {
    $('#divOrganizationType').show();
}
else
    $('#divOrganizationType').hide();

var updateJQuery = function () {
    $('#updateJQuery').strength({
        strengthClass: 'strength',
        strengthMeterClass: 'strength_meter',
        strengthButtonClass: 'button_strength',
        strengthButtonText: 'Show Password',
        strengthButtonTextToggle: 'Hide Password'
    });
    return false;
}

var hasValue = function (v, val) {
    if (val == 'undefined') return true;
    else return (val != '')
    //return (val.isNullOrWhiteSpace())
}