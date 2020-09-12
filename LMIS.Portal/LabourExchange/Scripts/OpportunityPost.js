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
    self.AcceptedFiles = validExtensions;
    self.Mode = ko.observable(mode);
    self.Title = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.File = ko.observable();
    self.ServerFileName = ko.observable();
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.IsInformal = ko.observable();
    self.IsInternal = ko.observable(!!window.BackEnd);
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();

    //VM Operations
    self.LoadRecord = function(id, editable) {

        lmis.ajax("../LabourExchange/OpportunityPost.aspx/GetOrgContactOpportunityById", { id: id }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.Title.Populate(data.d.Title.English, data.d.Title.French, data.d.Title.Arabic);
                    $("#txtFileName").val(data.d.FilePath);
                    self.ServerFileName(data.d.FilePath);
                    self.StartDate(lmis.format.dateToString(data.d.StartDate));
                    self.EndDate(lmis.format.dateToString(data.d.EndDate));
                    self.IsInformal(data.d.IsInformal);
                    self.IsInternal(data.d.IsInternal);
                    self.Approval(data.d.Approval);
                    self.RejectReason(data.d.RejectReason);

                    //Localize Trilingual Text Views
                    self.Title.LocalizeView(false, !editable);
 
                    //Admin Review?
                    if (window.vmReview) window.vmReview.init("opportunity", id);
               }
            });

        self.DisableUserInput(!editable);
        self.ResetInputMasks();

    }
    self.NewRecord = function() {

        self.Title.ClearText();
        self.ClearFile();
        self.StartDate("");
        self.EndDate("");
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
    self.ClearFile = function () {

        self.File(null);
        self.ServerFileName(null);
        $("#txtFileName").val("");
        lmis.fileInput.clear($("#hdnFileBrowser"));

    }
    self.StartWorkflow = function() {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;

        self.DisableUserInput(true);
        self.Step1();

    }
    self.WorkflowSuccess = function() {

        if (mode === "e") {
            self.DisableUserInput(true);
            self.Title.Repopulate();
            self.Mode("v");
        }
        else self.NewRecord();

    }
    self.WorkflowError = function() {

        self.DisableUserInput(false);
        if (n) n.close();

    }
    self.Step1 = function() {

        //Submit ViewModel for Server-Side Validation of Business Rules
        n = lmis.notification.progress($("#Step1").html());

        function onSuccess() {
            if (mode === "e" && !self.File()) self.Step3();
            else self.Step2();
        }

        self.Save(true, onSuccess, self.WorkflowError);

    }
    self.Step2 = function() {

        //Upload and Validate Selected File
        var ajaxUploadRequest = self.Upload(self.Step3, self.WorkflowError);

        if (!ajaxUploadRequest) {
            self.WorkflowError();
            self.Validate();
            return;
        };

    }
    self.Step3 = function() {

        //Save ViewModel to DB with reference to Uploaded File
        n.setText($("#Step3").html());
        self.Save(false, self.WorkflowSuccess, self.WorkflowError);

    }
    self.Validate = function() {

        var bResult = false;

        //Required Fields
        if (self.Title.isNullOrWhiteSpace()
            || (!self.File() && !self.ServerFileName())
            || !self.StartDate.Value || !self.EndDate.Value) {

            lmis.notification.error($("#RequiredFields").html());

        } else bResult = true;

        return bResult;

    }
    self.Save = function(validateOnly, onSuccess, onError) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                OpportunityId: (mode === "e") ? key : 0,
                Title: self.Title.getValue(),
                FilePath: self.ServerFileName(),
                StartDate: self.StartDate.Value,
                EndDate: self.EndDate.Value,
                IsInformal: self.IsInformal()
            }};

        return lmis.ajax("../LabourExchange/OpportunityPost.aspx/PostAnOpportunity", dto, 0, "",
            function(data) {
                if (!validateOnly && data.d)
                    lmis.notification.success();
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                onError();
            });

    }
    self.Upload = function(onSuccess, onError) {

        if (!self.File()) {
            self.ClearFile();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/image/", self.File(), 0, "show",
            function(data) {
                self.ServerFileName(data);
                onSuccess();
            },
            function(xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearFile();   //Validation Error
                onError();
            });

    }

    //UI Operations
    self.DisableUserInput = function(bDisable) {
        $("#tab5default :text").css("background-color", "white");
        $("#tab5default :input").attr("disabled", bDisable);
        $("#txtFileName").attr("disabled", true);
        $("#btnTitles").attr("disabled", false);
    }
    self.ResetInputMasks = function () {
        lmis.setMask.date($("#txtStartDate"));
        lmis.setMask.date($("#txtEndDate"));
    }

    //Initialize UI
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

$(document).ready(function() {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})