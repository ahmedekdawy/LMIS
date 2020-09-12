function ViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var mode = lmis.queryString["m"], key = parseInt(lmis.queryString["k"]);

    if (!mode || isNaN(key)) mode = "p";
    else mode = mode.toLowerCase();

    if (mode === "r") mode = "v"; //admin review mode support

    //Initialize VM
    self.vmReview = window.vmReview;
    self.AsyncItems = {};
    self.AcceptedFiles = validExtensions;
    self.Mode = ko.observable(mode);
    self.Title = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.TypeOptions = ko.observableArray();
    self.Type = ko.observable();
    self.Price = ko.observable();
    self.ContactAddress = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.ContactTelephone = ko.observable();
    self.ContactWebsite = ko.observable();
    self.File = ko.observable();
    self.ServerFileName = ko.observable();
    self.IsInformal = ko.observable();
    self.IsInternal = ko.observable(!!window.BackEnd);
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();

    //VM Operations
    self.LoadRecord = function (id, editable) {

        lmis.ajax("EventPost.aspx/Get", { id: id }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.Title.Populate(data.d.Title.English, data.d.Title.French, data.d.Title.Arabic);
                    self.Address.Populate(data.d.Address.English, data.d.Address.French, data.d.Address.Arabic);
                    self.StartDate(lmis.format.dateToString(data.d.StartDate));
                    self.EndDate(lmis.format.dateToString(data.d.EndDate));
                    self.Type(data.d.Type);
                    self.Price(data.d.Price);
                    self.ContactAddress.Populate(data.d.ContactAddress.English, data.d.ContactAddress.French, data.d.ContactAddress.Arabic);
                    self.ContactTelephone(data.d.ContactTelephone);
                    self.ContactWebsite(data.d.ContactWebsite);
                    self.ServerFileName(data.d.FilePath);
                    $("#txtFileName").val(data.d.FilePath);
                    self.IsInformal(data.d.IsInformal);
                    self.IsInternal(data.d.IsInternal);
                    self.Approval(data.d.Approval);
                    self.RejectReason(data.d.RejectReason);

                    //Localize Trilingual Text Views
                    self.Title.LocalizeView(false, !editable);
                    self.Address.LocalizeView(false, !editable);
                    self.ContactAddress.LocalizeView(false, !editable);

                    //Save AsyncItems
                    self.AsyncItems.Type = data.d.Type;

                    //Admin Review?
                    if (window.vmReview) window.vmReview.init("event", id);
                }
            });

        self.DisableUserInput(!editable);
        self.ResetInputMasks();

    }
    self.NewRecord = function () {

        self.AsyncItems = {};
        self.Title.ClearText();
        self.Address.ClearText();
        self.StartDate("");
        self.EndDate("");
        self.Type("");
        self.Price("");
        self.ContactAddress.ClearText();
        self.ContactTelephone("");
        self.ContactWebsite("");
        self.ClearFile();
        self.IsInformal(false);
        self.IsInternal(!!window.BackEnd);
        self.Approval(false);
        self.RejectReason(null);

        self.DisableUserInput(false);
        self.ResetInputMasks();

        $("#txtTitle_A").focus();

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
    self.ValidateUrl = function (t) {
        return /^(?:(?:(?:https?|ftp):)?\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})).?)(?::\d{2,5})?(?:[/?#]\S*)?$/.test(t);
    }
    self.ClearFile = function () {

        self.File(null);
        self.ServerFileName(null);
        $("#txtFileName").val("");
        lmis.fileInput.clear($("#hdnFileBrowser"));

    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;

        self.DisableUserInput(true);
        self.Step1();

    }
    self.WorkflowSuccess = function () {

        if (mode === "e") {
            self.DisableUserInput(true);
            self.Title.Repopulate();
            self.Address.Repopulate();
            self.ContactAddress.Repopulate();
            self.Mode("v");
        }
        else self.NewRecord();

    }
    self.WorkflowError = function () {

        self.DisableUserInput(false);
        if (n) n.close();

    }
    self.Step1 = function () {

        //Submit ViewModel for Server-Side Validation of Business Rules
        n = lmis.notification.progress($("#Step1").html());

        function onSuccess() {
            if (mode === "e" && !self.File()) self.Step3();
            else self.Step2();
        }

        self.Save(true, onSuccess, self.WorkflowError);

    }
    self.Step2 = function () {

        //Upload and Validate Selected File
        var ajaxUploadRequest = self.Upload(self.Step3, self.WorkflowError);

        if (!ajaxUploadRequest) {
            self.WorkflowError();
            self.Validate();
            return;
        };

    }
    self.Step3 = function () {

        //Save ViewModel to DB with reference to Uploaded File
        n.setText($("#Step3").html());
        self.Save(false, self.WorkflowSuccess, self.WorkflowError);

    }
    self.Validate = function () {

        var bResult = false;

        if (!lmis.string.isNullOrWhiteSpace(self.ContactWebsite()) && !self.ValidateUrl(self.ContactWebsite()))
        {
            lmis.notification.error($("#InvalidUrl").html());
            return false;
        }

        //Required Fields
        if (self.Title.isNullOrWhiteSpace()
            || self.Address.isNullOrWhiteSpace()
            || !self.StartDate.Value || !self.EndDate.Value
            || lmis.string.isNullOrWhiteSpace(self.Type())
            || self.ContactAddress.isNullOrWhiteSpace()
            || lmis.string.isNullOrWhiteSpace(self.ContactTelephone())
            || lmis.string.isNullOrWhiteSpace(self.ContactWebsite())
            || (!self.File() && !self.ServerFileName())) {

            lmis.notification.error($("#RequiredFields").html());

        } else bResult = true;

        return bResult;

    }
    self.Save = function (validateOnly, onSuccess, onError) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                EventId: (mode === "e") ? key : 0,
                Title: self.Title.getValue(),
                Address: self.Address.getValue(),
                StartDate: self.StartDate.Value,
                EndDate: self.EndDate.Value,
                Type: self.Type(),
                Price: lmis.string.toNumber(self.Price()),
                ContactAddress: self.ContactAddress.getValue(),
                ContactTelephone: self.ContactTelephone(),
                ContactWebsite: self.ContactWebsite(),
                FilePath: self.ServerFileName(),
                IsInformal: self.IsInformal()
            }
        };

        return lmis.ajax("../LabourExchange/EventPost.aspx/Post", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d)
                    lmis.notification.success();
                onSuccess();
            }, 
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                onError();
            });

    }
    self.Upload = function (onSuccess, onError) {

        if (!self.File()) {
            self.ClearFile();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/image/", self.File(), 0, "show",
            function (data) {
                self.ServerFileName(data);
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearFile();   //Validation Error
                onError();
            });

    }

    //UI Operations
    self.DisableUserInput = function (bDisable) {
        $("#tab6default :text").css("background-color", "white");
        $("#tab6default .always-white").css("background-color", "white");
        $("#tab6default :input").attr("disabled", bDisable);
        $("#tab6default .always-disabled").attr("disabled", true);
        $("#tab6default .always-enabled").attr("disabled", false);
    }
    self.ResetInputMasks = function () {
        lmis.setMask.date($("#txtStartDate"));
        lmis.setMask.date($("#txtEndDate"));
        lmis.setMask.decimal($("#txtPrice"), 5, 2);
        lmis.setMask.phone($("#txtContactTelephone"));
        lmis.setMask.url($("#txtContactWebsite"));
    }

    //Initialize UI
    lmis.api.SubCodes(self.TypeOptions, "015", function () {
        self.Type(self.AsyncItems.Type);
    });

    switch (mode) {
        case "v":           //View Mode
            self.LoadRecord(key, false);
            break;
        case "e":           //Edit Mode
            self.LoadRecord(key, true);
            break;
        default:            //Post Mode
            self.NewRecord();
    }

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})