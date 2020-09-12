ko.validation.rules.pattern.message = 'Invalid.';

ko.validation.init({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null,
    decorateElement: true,
}, true);

function Step(id, name, template, model) {

    var self = this;
    self.id = id;
    self.name = "";//ko.observable(name);
    self.template = template;

    self.model = ko.observable(model);

    self.getTemplate = function () {
        return self.template;
    }
}

function EmployerViewModel() {

    //Constants Codes
    var governmentCode = '10000001';
    var privateCode = '10000002';
    var selfEmployedCode = '10000003';

    //Initialize parameters
    var self = this;
    var cat = lmis.queryString["cat"];
    var subCat = lmis.queryString["subcat"];
    var n; //Noty Message Object

    var categoryCode = (cat != 'TP') ? "102" : "100";// GOV = 100, TP = 102
    self.subCategoryCode = ko.observable((subCat == 'TC') ? "10200001" : (subCat == 'SLF') ? "10000003" : "10000001");

    self.organizationVm = ko.observable(OrganizationViewModel());
    self.contactPersonVm = ko.observable(ContactPersonViewModel());
    self.stepModels = ko.observableArray([
            new Step(0, "", "organizationView", self.organizationVm()),
           new Step(1, "Contact Person", "contactPersonView", self.contactPersonVm()),
           new Step(2, "Confirmation", "confirmView", { OrganizationViewModel: self.organizationVm(), ContactPersonViewModel: self.contactPersonVm() })
    ]);

    //----------------------------- Manage Views ---------------------------//
    self.currentStep = ko.observable(self.stepModels()[0]);
    self.currentIndex = ko.dependentObservable(function () {
        return self.stepModels.indexOf(self.currentStep());
    });
    self.getTemplate = function (data) {
        return self.currentStep().template();
    };
    self.canGoNext = ko.dependentObservable(function () {
        return self.currentIndex() < self.stepModels().length - 1;
    });
    self.goNext = function () {
        //var curModel = self.stepModels()[self.currentIndex()].model();
        var errors = ko.validation.group((self.currentIndex() === 0) ? firstStepValidation : secondStepValidation);

        if (!self.ValidateMultiLangControls() || errors().length > 0 || !self.canGoNext()) {
            errors.showAllMessages();
        }
        else {
            self.currentStep(self.stepModels()[self.currentIndex() + 1]);
        }

        if (self.currentIndex() === 1) {
            $('#ConatactPersonFullNameValidation').hide();
            $('#ConatactPersonDepartmentValidation').hide();
        }
    };
    self.canGoPrevious = ko.dependentObservable(function () {
        return self.currentIndex() > 0;
    });
    self.goPrevious = function () {

        if (self.canGoPrevious()) {
            self.currentStep(self.stepModels()[self.currentIndex() - 1]);
        }
        $('#OrganizationNameValidation').hide();
        $('#AddressValidation').hide();
        $('#ConatactPersonFullNameValidation').hide();
        $('#ConatactPersonDepartmentValidation').hide();
        $('#LogoValidation').hide();

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
    };
    self.canFinish = ko.dependentObservable(function () {
        return !(self.currentIndex() < self.stepModels().length - 1);
    });
    self.ValidateMultiLangControls = function () {
        //first page controls
        if (self.currentIndex() === 0) {
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

            if (!self.Logo()) {
                $('#LogoValidation').show();
                isValid = false;
            }
            else
                $('#LogoValidation').hide();

            return isValid;
        }
            //second page controls
        else if (self.currentIndex() === 1) {
            var isValid = true;
            if (self.ConatactPersonFullName.isNullOrWhiteSpace()) {
                $('#ConatactPersonFullNameValidation').show();
                isValid = false;
            }
            else
                $('#ConatactPersonFullNameValidation').hide();
            if (self.ConatactPersonDepartment.isNullOrWhiteSpace()) {
                $('#ConatactPersonDepartmentValidation').show();
                isValid = false;
            }
            else
                $('#ConatactPersonDepartmentValidation').hide();

            return isValid;
        }
    }
    //----------------------------- Operations ---------------------------//
    self.StartWorkflow = function () {
        self.Step1();
    }
    self.WorkflowSuccess = function () {
        lmis.notification.success();
        alert('Saved successfully!, redirect to login...');
        window.location.href = '../login';
    }
    self.WorkflowError = function () {
        lmis.notification.error();
    }
    self.Step1 = function () {

        //Submit ViewModel for Server-Side Validation of Business Rules
        n = lmis.notification.progress($("#Step1").html());

        //if (!self.Logo()) self.Step3();
        //else
        self.Step2(true, self.Save, self.WorkflowError);

    }
    self.Step2 = function (validateOnly, onSuccess, onError) {

        if (!self.Logo()) {
            lmis.notification.error();
            return;
        }

        if (self.Profile()) {
            var ajaxUploadProfileRequest = self.UploadProfile();
            if (!ajaxUploadProfileRequest) {
                self.WorkflowError();
                return;
            };
        };
        if (self.Authorizationletter()) {
            var ajaxUploadAuthorizationletterRequest = self.UploadAuthorizationletter();
            if (!ajaxUploadAuthorizationletterRequest) {
                self.WorkflowError();
                return;
            };
        };

        //Upload and Validate Selected File
        var ajaxUploadLogoRequest = self.UploadLogo(onSuccess, onError);
        if (!ajaxUploadLogoRequest) {
            self.WorkflowError();
            return;
        };

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
                Is_Approved: 1,// organization status should be PENDING
                IsDiscalaimerApproved: true,//TODO: Activate property
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

                //AspNetUser object
                User: {
                    Email: self.ConatactPersonEmail(),
                    Password: self.ConatactPersonPassword(),
                    PhoneNumber: self.ConatactPersonTelephone(),
                    UserName: self.ConatactPersonEmail()
                },

                //Portal user object
                PortalUser: {
                    IDType: self.IDType(),
                    IDNumber: self.IDNumber(),
                    UserCategory: categoryCode,
                    UserSubCategory: self.subCategoryCode(),
                    TrainingProvider: self.TrainingProvider(),
                    Employer: self.Employer(),
                    TrainingSeeker: self.TrainingSeeker(),
                    //TODO: Handel below paremeters
                    JobSeeker: false,
                    //TrainingSeeker: false,
                    Researcher: false,
                    Internal: false,
                    IsSubscriper: false
                },

                //Contact Persons
                ContactPersons: [{
                    JobTitleID: self.JobTitle(),
                    Telephone: self.ConatactPersonTelephone(),
                    Mobile: self.ConatactPersonMobile(),
                    Fax: self.ConatactPersonFax(),
                    Email: self.ConatactPersonEmail(),
                    AuthorizationletterPath: self.ServerAuthorizationletter(),
                    IsApproved: 0,
                    IsDeleted: false,

                    Translation:
                   [{
                       LanguageID: 1,
                       ContactFullName: self.ConatactPersonFullName.getValue().English,
                       Department: self.ConatactPersonDepartment.getValue().English,
                   },
                   {
                       LanguageID: 2,
                       ContactFullName: self.ConatactPersonFullName.getValue().French,
                       Department: self.ConatactPersonDepartment.getValue().French,
                   },
                   {
                       LanguageID: 3,
                       ContactFullName: self.ConatactPersonFullName.getValue().Arabic,
                       Department: self.ConatactPersonDepartment.getValue().Arabic,
                   }],

                }],
            }
        };

        return lmis.ajax("../Index.aspx/Post", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d)
                    lmis.notification.success();
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                onError();
            });
    };


}

$(ko.applyBindings(EmployerViewModel()));


//Display Controls
$('#OrganizationNameValidation').hide();
$('#AddressValidation').hide();
$('#ConatactPersonFullNameValidation').hide();
$('#ConatactPersonDepartmentValidation').hide();
$('#LogoValidation').hide();

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


    $(function () {
        $(".datepiker").datepicker({
            dateFormat: "dd-mm-yy",
            changeMonth: true,
            changeYear: true

        });
    });


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