

ko.validation.rules.pattern.message = 'Invalid.';

ko.validation.init({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null,
    decorateElement: true
}, true);

var mustEqual = function (val, other) {
    return val == other;
};

var PersonalViewModel = function () {
    var self = this;
    var n; //Noty Message Object
    var validImageExtensions = '.gif,.jpg,.jpeg,.png';
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    //TODO: back to business to set category values
    var categoryCode = '013';
    var subCategoryCode = '01300001';
    var mode;
    


    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.FirstName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.LastName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh }});
    self.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Email = ko.observable().extend({ required: true });
    self.MobileNo = ko.observable();
    self.TelephoneNo = ko.observable();
    self.AllowtoViewMyInfo = ko.observable();
    self.IDNumber = ko.observable().extend({ required: true });
    self.Aggrement = ko.observable().extend({ required: true });
    self.AcceptedImageFiles = validImageExtensions;
    self.Password = ko.observable().extend({
        required: true,
        pattern: { params: lmis.regex.password, message: $("#X_InvalidPassword").text() }
    });
    self.RetypePassword = ko.observable().extend({
        required: true,
        validation: {
            validator: mustEqual,
            message: $("#X_PasswordsMismatch").text(),
            params: self.Password
        }
    });;
    self.Image = ko.observable();
    self.ServerImageName = ko.observable();
    self.DateofBirth = ko.observable().extend({ required: true, date: { dateFormat: lmis.x.momentDateFormat } });

    self.CountryOptions = ko.observableArray([]);
    self.Country = ko.observable().extend({ required: true });
    lmis.api.SubCodes(self.CountryOptions, "009", function () {
        self.Country(self.AsyncItems.Country);
    });
    self.CityOptions = ko.observableArray([]);
    self.City = ko.observable().extend({ required: true });
    self.Country.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal);
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
    self.Gender = ko.observable().extend({ required: true });
    lmis.api.SubCodesExeculude(self.optionsGender, "002","00200001", function () {
        self.Gender(self.AsyncItems.Gender);
    });

    self.optionsNationailty = ko.observableArray([]);
    self.Nationailty = ko.observable().extend({ required: true });
    lmis.api.SubCodes(self.optionsNationailty, "011", function () {
        self.Nationailty(self.AsyncItems.Nationailty);
    });

    self.optionsIDType = ko.observableArray([]);
    self.IDType = ko.observable().extend({ required: true });
    lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004", function () {
        self.IDType(self.AsyncItems.IDType);
    });

    self.optionsMedicalconditions = ko.observableArray([]);
    self.Medicalconditions = ko.observable().extend({ required: true });
    lmis.api.SubCodes(self.optionsMedicalconditions, "014", function () {
        self.Medicalconditions(self.AsyncItems.Medicalconditions);
    });
    self.disclaimer = ko.observable();
    lmis.api(self.disclaimer, "cnfg", "orgreg.disclaimer", null, null, "show,close");
    //----------------------------- Validation group ---------------------------//
    self.personalValidation = [
    //self.TelephoneNo,
    //self.MobileNo,
    //self.Address,
    //self.Password,
    //self.RetypePassword,
    //self.Email,
    //self.DateofBirth,
    //self.Country,
    //self.City,
    //self.Militarystatus,
    //self.Gender,
    //self.Nationailty,
    //self.IDType,
    //self.IDNumber,
    //self.Medicalconditions
    ];

    //----------------------------- Image upload functions ---------------------------//
    self.ValidateIDType = function () {
        if (self.Nationailty() != 'undefined') {
            if (self.Nationailty() == '01100001') {
                lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004", function () {
                    self.IDType(self.AsyncItems.IDType);
                });
            } else {
                lmis.api.SubCodesExeculude(self.optionsIDType, "013", "01300003,01300004,01300002", function () {
                    self.IDType(self.AsyncItems.IDType);
                });
            }
        }
    }
    self.ValidateMilitarystatus = function () {
        if (self.Gender() == '00200002') {
            $('.Militarystatus').show();
            self.Militarystatus.extend({ required: true });
        } else {
            $('#ddlmilitary').val('');
            self.Militarystatus.extend({ required: false });
            $('.Militarystatus').hide();
        }
    }
    self.ValidateImage = function (item, e) {

        var selectedImage = e.target.files[0];

        if (selectedImage != null) {
            if (selectedImage.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedImage.name, validImageExtensions)) {
                    self.Image(selectedImage);
                    $("#txtImageName").val(selectedImage.name);
                } else self.ClearImage();
            } else self.ClearImage();
        } else self.ClearImage();

    }
    self.ClearImage = function () {

        self.Image(null);
        self.ServerImageName(null);
        $("#txtImageName").val("");
        lmis.fileInput.clear($("#hdnImageBrowser"));

    }
    self.UploadImage = function (onSuccess, onError) {

        if (!self.Image()) {
            self.ClearImage();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/imageWithPath?path=" + config.individual.photoFolder, self.Image(), 0, "show/close",
            function (data) {
                self.ServerImageName(data);
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearImage();   //Validation Error
                onError();
            });
    }

    //----------------------------- Operations ---------------------------//
    self.CheckPasswordStrength = function (e) {
       // var password = e.val();
       // console.log(password);
        //var password_strength =$('.password_strength');

        ////TextBox left blank.
        //if (password.length == 0) {
        //    password_strength.innerHTML = "";
        //    return;
        //}

        ////Regular Expressions.
        //var regex = new Array();
        //regex.push("[A-Z]"); //Uppercase Alphabet.
        //regex.push("[a-z]"); //Lowercase Alphabet.
        //regex.push("[0-9]"); //Digit.
        //regex.push("[$@$!%*#?&]"); //Special Character.

        //var passed = 0;

        ////Validate for each Regular Expression.
        //for (var i = 0; i < regex.length; i++) {
        //    if (new RegExp(regex[i]).test(password)) {
        //        passed++;
        //    }
        //}

        ////Validate for length of Password.
        //if (passed > 2 && password.length > 8) {
        //    passed++;
        //}

        ////Display status.
        //var color = "";
        //var strength = "";
        //switch (passed) {
        //    case 0:
        //    case 1:
        //        strength = "Weak";
        //        color = "red";
        //        break;
        //    case 2:
        //        strength = "Good";
        //        color = "darkorange";
        //        break;
        //    case 3:
        //    case 4:
        //        strength = "Strong";
        //        color = "green";
        //        break;
        //    case 5:
        //        strength = "Very Strong";
        //        color = "darkgreen";
        //        break;
        //}
        //password_strength.innerHTML = strength;
        //password_strength.style.color = color;
    }
    
    self.Validate = function () {
        if (!$('#chkAggrement').prop('checked')) {
            lmis.notification.error($("#X_AggrementRead").text());
            return false;
        }
        var valid = true;
        var errors = ko.validation.group(self);
        if (self.FirstName.isNullOrWhiteSpace()) {
            $('#FirstNameValidation').show();
            valid = false;
        } else {
            $('#FirstNameValidation').hide();
        }
        if (self.LastName.isNullOrWhiteSpace()) {
            $('#lastnameValidation').show();
            valid= false;
        }
        else {
            $('#lastnameValidation').hide();
        }
        if (valid == false) {

            return false;
        }
        if (errors().length > 0) {
            errors.showAllMessages();//!self.ValidateMultiLangControls() ||
            return false;
        }
        else
            return true;
    }
    self.Save = function () {
        var isValid = self.Validate();
        if (!isValid) return;

        if (self.Image())
            self.UploadImage(self.PostRecord, self.OnError);
        else
            self.PostRecord();
    }
    self.PostRecord = function () {
        if (self.FirstName.isNullOrWhiteSpace()) {
            $('#FirstNameValidation').show();
            return false;
        }
        if (self.LastName.isNullOrWhiteSpace()) {
            $('#lastnameValidation').show();
            return false;
        }
        var dto = {
            individualObject: {
                PortalUsersID: 0,
                Email: self.Email(),
                MobileNo: self.MobileNo(),
                TelephoneNo: self.TelephoneNo(),
                GenderId: self.Gender(),
                DateOfBirth: self.DateofBirth.Value,
                MaritalStatusId: self.Maritalstatus(),
                MilitaryStatus_Id: self.Militarystatus(),
                NationalityId: self.Nationailty(),
                PhotoPath: self.ServerImageName(),
                AllowtoViewMyInfo: (self.AllowtoViewMyInfo()) ? 1 : 0,
                IndividualMedicalID: self.Medicalconditions(),
                CountryID: self.Country(),
                CityID: self.City(),
                Is_Approved: 1,// Pending

                Translation:
                    [{
                        LanguageID: 1,
                        FirstName: self.FirstName.getValue().English,
                        LastName: self.LastName.getValue().English,
                        Address: self.Address.getValue().English,
                    },
                    {
                        LanguageID: 2,
                        FirstName: self.FirstName.getValue().French,
                        LastName: self.LastName.getValue().French,
                        Address: self.Address.getValue().French,
                    },
                    {
                        LanguageID: 3,
                        FirstName: self.FirstName.getValue().Arabic,
                        LastName: self.LastName.getValue().Arabic,
                        Address: self.Address.getValue().Arabic,
                    }],

                //AspNetUser object
                User: {
                    Email: self.Email(),
                    Password: self.Password(),
                    PhoneNumber: self.MobileNo(),
                    UserName: self.Email()
                },

                //Portal user object
                PortalUser: {
                    IDType: self.IDType(),
                    IDNumber: self.IDNumber(),
                    UserCategory: categoryCode,
                    UserSubCategory: subCategoryCode,
                    // Employer: self.Employer(),                   
                    //TODO: Handel below paremeters
                    JobSeeker: true,
                    //TrainingProvider: false,
                    TrainingSeeker: true,
                    Researcher: true,
                    //Internal: false,
                    //IsSubscriper: false
                }
            }
        };

        return lmis.ajax("../Individual/Registeration.aspx/Post", dto, 0, "",
            function (data) {
                if (data.d) {
                    lmis.notification.success();
                    window.location.href = '/login';
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
            });
    }
    self.Load = function (mode) {

        lmis.ajax("../Individual/Profile.aspx/GetDetails", { mode: mode }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.DateofBirth(lmis.format.dateToString(data.d.res.BirthDate));
                    //self.FirstName.Populate(data.d.res.FirstName.English, data.d.res.FirstName.French, data.d.res.FirstName.Arabic);
                    //self.LastName.Populate(data.d.res.LastName.English, data.d.res.LastName.French, data.d.res.LastName.Arabic);
                    self.Email(data.d.res.Email),
                    self.MobileNo(data.d.res.MobileNo),
                    self.TelephoneNo(data.d.res.TelephoneNo),
                    self.Gender(data.d.res.GenderId),
                    self.DateofBirth(data.d.res.DateOfBirth),
                    self.Maritalstatus(data.d.res.MaritalStatusId),
                    self.Militarystatus(data.d.res.MilitaryStatus_Id),
                    self.Nationailty(data.d.res.NationalityId),
                    self.ServerImageName(data.d.res.PhotoPath),
                    self.AllowtoViewMyInfo(data.d.res.AllowtoViewMyInfo),
                    self.Medicalconditions(data.d.res.IndividualMedicalID),
                    self.Country(data.d.res.CountryID),
                    self.City(data.d.res.CityID)

                    //Localize Trilingual Text Views
                    self.FirstName.LocalizeView(false);
                    self.LastName.LocalizeView(false);
                    self.Address.LocalizeView(true);
                }
            });
    }
    self.OnError = function () {
        lmis.notification.error('an error has occurred!');
    }

    //Initialize UI
    if (window.location.href.indexOf("Profile") > -1) {
        mode = "v";
        self.Load(mode); // view mode
    }
    else {
        mode = "n"; //post mode

        //Handle strength password classes
        $('#updateJQuery').strength({
            strengthClass: 'strength',
            strengthMeterClass: 'strength_meter',
            strengthButtonClass: 'button_strength',
            strengthButtonText: 'Show Password',
            strengthButtonTextToggle: 'Hide Password'
        });
    }
}

$(document).ready(function () {
    ko.applyBindings(PersonalViewModel());
})
function showpopup() {

    var d = $(".pop");
    d.dialog({
        title: 'Agreement',
        width: 800,
        height:600
    });
    return false;
}