function ViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".pdf,.doc,.docx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var key = lmis.queryString["k"];
    var browsingFor;

    //Initialize VM
    self.AcceptedFiles = validExtensions;
    self.Mode = ko.observable();
    self.Requirements = ko.observable();
    self.Application = ko.observableArray();

    //VM Operations
    self.LoadRequirements = function (id) {

        if (lmis.string.isNullOrWhiteSpace(id)) return;

        lmis.ajax("../LabourExchange/JobApplication.aspx/ApplicationRequirements", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    self.Requirements(data.d);
                    self.InitApplication();
                }
            });

    }
    self.InitApplication = function() {

        var req = self.Requirements();
        if (!req) return;

        if (!lmis.string.isNullOrWhiteSpace(req.AppTemplate))
            self.Application.push({
                DocType: "", DocName: $("#F022_AppTemplate").html(),
                Template: lmis.x.downloadURL + req.AppTemplate,
                File: ko.observable(), ServerFileName: ko.observable()
            });

        if (req.AdditionalDocs && req.AdditionalDocs.constructor === Array)
            req.AdditionalDocs.map(function(doc) {
                self.Application.push({
                    DocType: doc.id, DocName: doc.desc, Template: null,
                    File: ko.observable(), ServerFileName: ko.observable()
                });
            });

    }
    self.BrowseFiles = function(item) {
        browsingFor = item;
        $("#hdnFileBrowser").trigger("click");
    }
    self.ValidateFile = function (item, e) {

        var selectedFile = e.target.files[0];

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    browsingFor.File(selectedFile);
                } else alert(lmis.x.InvalidFile(validExtensions, maxFileSize));
            } else alert(lmis.x.InvalidFile(validExtensions, maxFileSize));
        }

        lmis.fileInput.clear($("#hdnFileBrowser"));

    }
    self.StartWorkflow = function () {
        if (self.Validate()) self.Step1();
    }
    self.WorkflowSuccess = function () {
        self.Mode("v");
    }
    self.WorkflowError = function () {
        if (n) n.close();
    }
    self.Step1 = function() {

        //Upload all files
        n = lmis.notification.progress();
        self.Upload(0, self.Step2, self.WorkflowError);

    }
    self.Step2 = function () {

        //Save application
        n.setText($("#Step2").html());
        self.Apply(self.WorkflowSuccess, self.WorkflowError);

    }
    self.Validate = function () {

        var ret = true;
        var app = self.Application();

        $.each(app, function(idx) {
            if (!app[idx].File()) ret = false;
        });

        if (!ret) lmis.notification.error($("#F022_Validations").html());

        return ret;

    }
    self.Upload = function (idx, onSuccess, onError) {

        var app = self.Application();

        if (idx < 0 || idx >= app.length) {
            onError();
            return null;
        }

        return lmis.ajaxUpload("/api/upload/doc/", app[idx].File(), 0, "show",
            function (data) {
                app[idx].ServerFileName(data);
                if (idx === app.length - 1) onSuccess();
                else self.Upload(idx + 1, onSuccess, onError);
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) app[idx].File(null);   //Validation Error
                onError();
            });

    }
    self.Apply = function (onSuccess, onError) {

        var app = self.Application();
        var appForm = null, attachments = [];

        app.map(function(item) {
            if (item.DocType === "") appForm = item.ServerFileName();
            else {
                attachments.push({
                    id: item.DocType,
                    desc: item.ServerFileName()
                });
            }
        });

        lmis.ajax("../LabourExchange/JobApplication.aspx/Apply", { id: key, appForm: appForm, attachments: attachments }, 0, "",
            function (data) {
                if (data.d)
                    lmis.notification.success();
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                onError();
            });

    }

    //Initialize UI
    self.LoadRequirements(key);
    self.InitApplication();
}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})