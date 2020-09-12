var mustEqual = function (val, other) {
    return val == other;
};

var ContactPersonViewModel = function () {

    //Initialize parameters
    var self = this;
    var n; //Noty Message Object
    var validAuthorizationletterExtensions = ".pdf,.doc,.docx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes

    //Initialize view model properties
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.Authorizationletter = ko.observable();
    self.ConatactPersonTelephone = ko.observable().extend({ required: true });
    self.ConatactPersonMobile = ko.observable().extend({ required: true });
    self.ConatactPersonFax = ko.observable();
    self.ConatactPersonEmail = ko.observable().extend({ required: true });
    self.ConatactPersonPassword = ko.observable().extend({
        required: true,
        pattern: { params: lmis.regex.password, message: res["msgInvalidPassword"] }
    });
    self.ConatactPersonFullName = ko.observable().extend({ required: true, trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.ConatactPersonDepartment = ko.observable().extend({ required: true, trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.ConfirmPassword = ko.observable().extend({
        required: true,
        validation: {
            validator: mustEqual,
            message: 'Passwords do not match.',
            params: self.ConatactPersonPassword
        }
    });

    //Files properties
    self.AcceptedAuthorizationletterFiles = validAuthorizationletterExtensions;
    self.Authorizationletter = ko.observable();
    self.ServerAuthorizationletter = ko.observable('');


    self.JobTitle = ko.observable().extend({ required: true });
    self.JobTitleOptions = ko.observableArray([]);

    //Bind lookups
    lmis.api.SubCodes(self.JobTitleOptions, "017", function () {
        self.JobTitle(self.AsyncItems.JobTitle);
    });


    //----------------------------- Validation groups ---------------------------//
    self.secondStepValidation = [
     //self.ConatactPersonFullName,
    self.JobTitle,
    self.ConatactPersonTelephone,
    self.ConatactPersonMobile,
    self.ConatactPersonEmail,
    self.ConatactPersonPassword,
    self.ConfirmPassword
    ];


    //----------------------------- Authorizationletter upload functions ---------------------------//
    self.ValidateAuthorizationletter = function (item, e) {

        var selectedAuthorizationletter = e.target.files[0];

        if (selectedAuthorizationletter != null) {
            if (selectedAuthorizationletter.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedAuthorizationletter.name, validAuthorizationletterExtensions)) {
                    self.Authorizationletter(selectedAuthorizationletter);
                    $("#txtAuthorizationletter").val(selectedAuthorizationletter.name);
                } else self.ClearAuthorizationletter();
            } else self.ClearAuthorizationletter();
        } else self.ClearAuthorizationletter();

    }
    self.ClearAuthorizationletter = function () {

        self.Authorizationletter(null);
        self.ServerAuthorizationletter(null);
        $("#txtAuthorizationletter").val("");
        lmis.fileInput.clear($("#hdnAuthorizationletterBrowser"));

    }
    self.UploadAuthorizationletter = function () {

        if (!self.Authorizationletter()) {
            self.ClearAuthorizationletter();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/UploadDocWithPath?path=" + config.employer.authorizationletterFolder, self.Authorizationletter(), 0, "show/close",
            function (data) {
                self.ServerAuthorizationletter(data);
                //onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearAuthorizationletter();   //Validation Error
                //onError();
            });
    }

    return self;
}
