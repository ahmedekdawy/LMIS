function VmItem(item) {

    this.RowId = "r" + item.Id;
    this.Id = item.Id;
    this.Title = lmis.string.isNullOrWhiteSpace(item.Title)
        ? lmis.globalString.toLocal(item.NewTitle, true) : item.Title;
    this.Approval = item.Approval;

};

function ViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".pdf,.doc,.docx,.ppt,.pptx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var actionInProgress = false;
    var grd;

    //Initialize VM
    self.AcceptedFiles = validExtensions;
    self.File = ko.observable();
    self.ServerFileName = ko.observable();
    self.VmList = ko.observableArray([]);
    self.DetailsFor = ko.observable();
    self.Applicants = ko.observableArray([]);

    //VM Operations
    self.List = function () {

        lmis.ajax("../LabourExchange/TrainingList.aspx/List", { langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d[0], function (item) { return new VmItem(item) });
                self.VmList(ds);
                self.ServerFileName(data.d[1]);
                grd = $("#grd").DataTable();
            });

    }
    self.View = function (item) {
        window.location.assign("TrainingPost?m=v&k=" + item.Id + "#anchor");
    }
    self.Edit = function (item) {
        window.location.assign("TrainingPost?m=e&k=" + item.Id + "#anchor");
    }
    self.Delete = function (item) {

        function onConfirm() {

            var dto = { id: item.Id, reason: "" };

            lmis.ajax("../LabourExchange/TrainingList.aspx/Delete", dto, 0, "show,close",
                function () {
                    grd.row("#" + item.RowId).remove().draw(false);
                    self.VmList.remove(item);
                }).always(function () {
                    actionInProgress = false;
                });

        }

        function onCancel() {
            actionInProgress = false;
        }

        if (!actionInProgress) {
            actionInProgress = true;
            lmis.notification.confirm(onConfirm, onCancel);
        }

    }
    self.DetailApplicants = function(item) {
        
        lmis.ajax("../LabourExchange/TrainingList.aspx/DetailApplicants", { id: item.Id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                self.Applicants(data.d);
                self.DetailsFor(item.Title);
                location.hash = "#applicants";
            });

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
        $("#txtFileName").val("");
        lmis.fileInput.clear($("#hdnFileBrowser"));

    }
    self.UploadAndSave = function () {

        if (!self.File()) {
            self.ClearFile();
            return null;
        };

        n = lmis.notification.progress("");

        return lmis.ajaxUpload("/api/upload/doc/", self.File(), 0, "show",
            function(data) {
                self.ServerFileName(data);
                lmis.ajax("../LabourExchange/TrainingList.aspx/SetTrainingList", { file: data }, 0, "close",
                    function() {
                        self.ClearFile();
                    },
                    function(xhr) {
                        if (n) n.close();
                        lmis.ajaxErrorHandler(xhr);
                        if (xhr.status === 400) self.ClearFile(); //Validation Error
                    });
            },
            function (xhr) {
                if (n) n.close();
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearFile();   //Validation Error
            });

    }
    self.DeleteFile = function() {

        if (!self.ServerFileName()) return;

        function onConfirm() {

            lmis.ajax("../LabourExchange/TrainingList.aspx/SetTrainingList", { file: null }, 0, "show,close",
                function () {
                    self.ClearFile();
                    self.ServerFileName(null);
                }).always(function () {
                    actionInProgress = false;
                });

        }

        function onCancel() {
            actionInProgress = false;
        }

        if (!actionInProgress) {
            actionInProgress = true;
            lmis.notification.confirm(onConfirm, onCancel);
        }

    }

    //Initialize UI
    self.List();

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})