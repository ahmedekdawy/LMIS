
function ViewModel() {
    var mode = lmis.queryString["mode"];
    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.TelephoneNo = ko.observable();
    self.MobileNumber = ko.observable();
    self.AllowtoViewMyInfo = ko.observable();
    self.NationailtyIDorPassportID = ko.observable();
    self.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: "", frPh: "", arPh: "" } });
    self.Address.ActiveLang = ko.observable();
    self.AcceptedFiles = validExtensions;
    self.Password = ko.observable();
    self.RetypePassword = ko.observable();
    self.FirstName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.LastName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.email = ko.observable();
    self.ServerFileName = ko.observable();

    self.optionsCountry = ko.observableArray([]);
    self.Country = ko.observable();
    lmis.api.SubCodes(self.optionsCountry, "009", function () {
        if (mode === "p") self.Country("00900002");
        else self.Country(self.AsyncItems.Country);
    });

    self.optionsCity = ko.observableArray([]);
    self.City = ko.observable();
    self.Country.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.optionsCity, self.City, "003", newVal, function () {
            self.City(self.AsyncItems.City);
        });
    });

    self.optionsMilitarystatus = ko.observableArray([]);
    self.Militarystatus = ko.observable();
    lmis.api.SubCodes(self.optionsMilitarystatus, "012", function () {
        self.Militarystatus(self.AsyncItems.Militarystatus);
    });

    self.optionsMaritalstatus = ko.observableArray([]);
    self.Maritalstatus = ko.observable();
    lmis.api.SubCodes(self.optionsMaritalstatus, "005", function () {
        self.Maritalstatus(self.AsyncItems.Maritalstatus);
    });

    self.optionsGender = ko.observableArray([]);
    self.Gender = ko.observable();
    lmis.api.SubCodes(self.optionsGender, "002", function () {
        self.Gender(self.AsyncItems.Gender);
    });

    self.optionsNationailty = ko.observableArray([]);
    self.Nationailty = ko.observable();
    lmis.api.SubCodes(self.optionsNationailty, "011", function () {
        self.Nationailty(self.AsyncItems.Nationailty);
    });

    self.optionsTypeID = ko.observableArray([]);
    self.TypeID = ko.observable();
    lmis.api.SubCodes(self.optionsTypeID, "013", function () {
        self.TypeID(self.AsyncItems.TypeID);
    });

    self.optionsMedicalconditions = ko.observableArray([]);
    self.Medicalconditions = ko.observable();
    lmis.api.SubCodes(self.optionsMedicalconditions, "014", function () {
        self.Medicalconditions(self.AsyncItems.Medicalconditions);
    });

    self.DateofBirth = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.NewRecord = function () {
        self.DateofBirth("");
        self.FirstName("");
        self.email("");
        self.ResetInputMasks();
        $("#txtfirstname").focus();
    }
    self.ValidateFile = function (item, e) {

        var selectedFile = e.target.files[0];

        function invalidate() {
            alert(lmis.x.InvalidFile(validExtensions, maxFileSize));
            self.ClearFile();
        }

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    self.File(selectedFile);
                    $("#txtFileName").val(selectedFile.name);
                } else invalidate();
            } else invalidate();
        } else self.ClearFile();

    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        self.Save(false);
       
    }
    self.WorkflowSuccess = function () {

        if (mode === "e") {
            self.DisableUserInput(true);
            self.Title.Repopulate();
        }
        else self.NewRecord();

    }
    self.WorkflowError = function () {

        self.DisableUserInput(false);
        if (n) n.close();

    }
    self.Validate = function () {

        var bResult = false;
        //Required Fields

        if (!self.DateofBirth.Value || self.FirstName.isNullOrWhiteSpace()
            || self.LastName.isNullOrWhiteSpace() || lmis.string.isNullOrWhiteSpace(self.email())
            || !isValidEmailAddress(self.email()) || lmis.string.isNullOrWhiteSpace(self.Password())
            || lmis.string.isNullOrWhiteSpace(self.RetypePassword()) || lmis.string.isNullOrWhiteSpace(self.NationailtyIDorPassportID())
            || lmis.string.isNullOrWhiteSpace(self.Militarystatus()) ||lmis.string.isNullOrWhiteSpace(self.Gender())
            || lmis.string.isNullOrWhiteSpace(self.Nationailty()) || lmis.string.isNullOrWhiteSpace(self.Medicalconditions())
            || lmis.string.isNullOrWhiteSpace(self.Country()) || lmis.string.isNullOrWhiteSpace(self.TypeID()) || lmis.string.isNullOrWhiteSpace(self.City())) {

            lmis.notification.error($("#RequiredFieldsstep1").html());
        }
        else if (self.Password() != self.RetypePassword()) {
            lmis.notification.error($("#Confirmation_Password_and_ConfirmationPassword").html());
        } else {
            bResult = true;
        }


        return bResult;

    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                RegisterationId: 0,
                FirstName: self.FirstName.getValue(),
                LastName: self.LastName.getValue(),
                Email: self.email(),
                Password: self.Password(),
                BirthDate: self.DateofBirth.Value,//self.DateofBirth(),
                Gender: self.Gender(),
                Maritalstatus: self.Maritalstatus(),
                Militarystatus: self.Militarystatus(),
                ConfirmPassword: self.RetypePassword(),
                Address: self.Address.getValue(),
                MobileNumber: self.MobileNumber(),
                TelephoneNo: self.TelephoneNo(),
                Nationality: self.Nationailty(),
                IdType: self.TypeID(),
                NationailtyIDorPassportID: self.NationailtyIDorPassportID(),
                Country: self.Country(),
                City: self.City(),
                IndividualMedicalId: self.Medicalconditions()
            },
            mode: mode
        };

        return lmis.ajax("../IndividualRegistration/PersonalInformation.aspx/PostPersonalInformation", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d) {
                    lmis.notification.success();
                    //onSuccess();
                    window.location.assign("EducationalInformation.aspx?#anchor");
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                //onError();
            });

    }
    self.Upload = function () {

        if (!self.File()) {
            self.ClearFile();
            return null;
        };
        return lmis.ajaxUpload("/api/upload/uploadimage/", self.File(), 0, "show",
            function (data) {
                self.ServerFileName(data);
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearFile();   //Validation Error
            });

    }
    self.ResetInputMasks = function () {
        lmis.setMask.date($("#dtdateofbirth"));
        lmis.setMask.phone($("#txtmobileno"));
        lmis.setMask.phone($("#txttelephonenumber"));
        lmis.setMask.email($("#txtemail"));
        lmis.setMask.toString($("#txtlastname"));
        lmis.setMask.toString($("#txtfirstname"));
    }
    self.LoadRecord = function (mode) {

        lmis.ajax("../IndividualRegistration/PersonalInformation.aspx/GetPersonalInformation", { mode: mode }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.DateofBirth(lmis.format.dateToString(data.d.BirthDate));
                    self.FirstName.Populate(data.d.FirstName.English, data.d.FirstName.French, data.d.FirstName.Arabic);
                    self.LastName.Populate(data.d.LastName.English, data.d.LastName.French, data.d.LastName.Arabic);
                    self.Gender(data.d.Gender);
                    self.email(data.d.Email);
                    self.Militarystatus(data.d.Militarystatus);
                    self.Maritalstatus(data.d.Maritalstatus);
                    self.Country(data.d.Country);
                    self.City(data.d.City);
                    self.Address.Populate(data.d.Address.English, data.d.Address.French, data.d.Address.Arabic);
                    self.MobileNumber(data.d.MobileNumber);
                    self.TelephoneNo(data.d.TelephoneNo);
                    self.Nationailty(data.d.Nationality);
                    self.TypeID(data.d.IdType);
                    self.NationailtyIDorPassportID(data.d.NationailtyIDorPassportID);
                    self.Medicalconditions(data.d.IndividualMedicalId);
                    //Localize Trilingual Text Views
                    self.FirstName.LocalizeView(false);
                    self.LastName.LocalizeView(false);
                    self.Address.LocalizeView(false);
                }
            });
    }

    //Initialize UI
    switch (mode) {
        case "e":           //Edit Mode
            self.LoadRecord(mode);
            break;
        default:            //Post Mode
            self.NewRecord();
    }
    self.Gender();
    self.Maritalstatus();
    self.Nationailty();
    self.Militarystatus();
    self.TypeID();
    self.Medicalconditions();
    self.Country();
    self.City();
    self.Address.LocalizeView(false);
    self.Address.ActiveLang(lmis.uiCulture);
    ////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////
    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailAddress);
    };

    $('#txtpassword').strength({
        strengthClass: 'strength',
        strengthMeterClass: 'strength_meter',
        strengthButtonClass: 'button_strength',
        strengthButtonText: 'Show Password',
        strengthButtonTextToggle: 'Hide Password'
    });
  
}
$(document).ready(function () {
    ko.applyBindings(new ViewModel());
})
