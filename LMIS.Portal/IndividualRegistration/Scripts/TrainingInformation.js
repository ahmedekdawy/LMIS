function Training(item) {
    this.RowId = item.IndividualTrainingID;
    this.TrainingName = lmis.string.isNullOrWhiteSpace(item.trainingname)
    ? lmis.globalString.toLocal(item.trainingname, true) : item.trainingname;
    this.TrainingProviderName = lmis.string.isNullOrWhiteSpace(item.TrainingProvider)
    ? lmis.globalString.toLocal(item.TrainingProvider, true) : item.TrainingProvider;
    this.TrainingStartDate = lmis.format.dateToString(item.StartDate);
    this.TrainingEndDate = lmis.format.dateToString(item.EndDate);
};
function Certification(item) {
    this.RowId = item.IndividualCertificationID;
    this.CertificationName = lmis.string.isNullOrWhiteSpace(item.CertificationName)
    ? lmis.globalString.toLocal(item.CertificationName, true) : item.CertificationName;
    this.CertificationIssueDate = lmis.format.dateToString(item.CertificationIssueDate);
    this.CertificationValidUntil = lmis.format.dateToString(item.CertificationValidUntil);
};
function ViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var mode = lmis.queryString["m"];
    var grdTrainingInformation;
    var grdCertificationInformation;
    //if (!mode || isNaN(key)) mode = "p";
    //else mode = mode.toLowerCase();
    var actionInProgress = false;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.TrainingList = ko.observableArray([]);
    self.GetTrainingList = function () {

        lmis.ajax("../IndividualRegistration/TrainingInformation.aspx/GetTrainingList", { langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Training(item) });
                self.TrainingList(ds);
                grdTrainingInformation = $("#grdTrainingInformation").DataTable();
            });
    }
    self.CertificationList = ko.observableArray([]);
    self.GetCertificationList = function () {

        lmis.ajax("../IndividualRegistration/TrainingInformation.aspx/GetCertificateList", { langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Certification(item) });
                self.CertificationList(ds);
                grdCertificationInformation = $("#grdCertificateInformation").DataTable();
            });
    }
    self.Edit = function (item) {
        window.location.assign("AddNewTraining?m=e&id=" + item.RowId + "#anchor");
    }
    self.Delete = function (item) {
        function onConfirm() {

            var dto = { id: item.RowId, reason: "" };

            lmis.ajax("../IndividualRegistration/TrainingInformation.aspx/Delete", dto, 0, "show,close",
                function () {
                    grdTrainingInformation.row("#" + item.RowId).remove().draw(false);
                    self.TrainingList.remove(item);
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
    self.Editcrt = function (item) {
        window.location.assign("AddNewCertificate?m=e&id=" + item.RowId + "#anchor");
    }
    self.Deletecrt = function (item) {
        function onConfirm() {

            var dto = { id: item.RowId, reason: "" };

            lmis.ajax("../IndividualRegistration/TrainingInformation.aspx/DeleteCert", dto, 0, "show,close",
                function () {
                    grdCertificationInformation.row("#" + item.RowId).remove().draw(false);
                    self.CertificationList.remove(item);
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
    self.PersonalInfo = function () {
        window.location.assign("PersonalInformation.aspx?#anchor");

    }
    self.EducationInfo = function () {
        window.location.assign("EducationalInformation.aspx?#anchor");

    }
    self.ExperienceInfo = function () {
        window.location.assign("ExperienceInformation.aspx?#anchor");

    }
    self.SkillsInfo = function () {
        window.location.assign("SkillsInformation.aspx?#anchor");

    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        //window.location.assign("ExperienceInformation.aspx?#anchor");

    }

    self.WorkflowSuccess = function () {

        if (mode === "e") {
            self.DisableUserInput(true);
            // self.Title.Repopulate();
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
        if (self.TrainingList().length < 1 && self.CertificationList().length < 1) {
            lmis.notification.error($("#RequiredFieldsstep1").html());
        }
        else {
            bResult = true;
        }


        return bResult;
    }

    ///////////////////////////////
    self.GetTrainingList();
    self.GetCertificationList();

}
$(document).ready(function () {
    ko.applyBindings(new ViewModel());
})
