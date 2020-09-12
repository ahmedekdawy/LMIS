function Education(item) {
    this.RowId = item.IndividualEducationlevelID;
    //this.OpportunityId = ko.observable(item.OpportunityId);
    this.Title = item.EducationName; 
    this.InstitutionType = item.InstitutionType; 
    this.EducationalLevelId = item.EducationalLevelId;
    this.EducationalLevelName = item.EducationalLevelName;
};
function ViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var mode = lmis.queryString["m"];
    var grdEducationList;
    if (!mode || isNaN(key)) mode = "p";
    else mode = mode.toLowerCase();
    var actionInProgress = false;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.url = ko.observable();
    self.leveleducation = ko.observable();
    self.optionsleveleducation = ko.observableArray([]);
    lmis.api.SubCodes(self.optionsleveleducation, "006", function () {
        self.leveleducation(self.AsyncItems.leveleducation);
    });

    self.EducationList = ko.observableArray([]);
    self.GetEducationList = function () {

        lmis.ajax("../IndividualRegistration/EducationalInformation.aspx/GetEducationList", { langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Education(item) });
                self.EducationList(ds);
                grdEducationList = $("#grdEducation").DataTable();
            });
    }
    self.AddnewEducation = function () {
        //if (lmis.string.isNullOrWhiteSpace(self.leveleducation())) {
        //    lmis.notification.error($("#RequiredFieldsstep1").html());
        //}
    }
    self.View = function (item) {
      //  window.location.assign("TrainingPost?m=v&k=" + item.Id + "#anchor");
    }
    self.Edit = function (item) {
        var IndividualEducationlevelID = item.RowId;
        var EducationLevelobj = "00"+item.EducationalLevelId;

        if (EducationLevelobj == "00600002" || EducationLevelobj == "00600003" || EducationLevelobj == "00600004" || EducationLevelobj == "00600005") {
            window.location.assign("UniversityInformation?m=e&id=" + IndividualEducationlevelID + "&eduid=0#anchor");
        }
        else if (EducationLevelobj == "00600007") {
            window.location.assign("SchoolInformation?m=e&id=" + IndividualEducationlevelID + "&eduid=0#anchor");

        }
        else if (EducationLevelobj == "00600006" || EducationLevelobj == "00600008") {
            window.location.assign("InstitutionInformation?m=e&id=" + IndividualEducationlevelID + "&eduid=0#anchor");
        }
    }
    self.Delete = function (item) {
        function onConfirm() {

            var dto = { id: item.RowId, reason: "" };

            lmis.ajax("../IndividualRegistration/EducationalInformation.aspx/Delete", dto, 0, "show,close",
                function () {
                    grdEducationList.row("#" + item.RowId).remove().draw(false);
                    self.EducationList.remove(item);
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
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        window.location.assign("ExperienceInformation.aspx?#anchor");

    }
    self.PersonalInfo = function () {
        window.location.assign("PersonalInformation.aspx?#anchor");

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
        if (self.EducationList().length < 1) {
            lmis.notification.error($("#RequiredFieldsstep1").html());
        }
        else {
            bResult = true;
        }

        
        return bResult;
    }
   
    ///////////////////////////////
    // Select Level of education ///
    //$('#ddlevelofeducation').on('change', function () {
    //    var EducationLevelobj = self.leveleducation();
    //    if (EducationLevelobj == "00600002" || EducationLevelobj == "00600003" || EducationLevelobj == "00600004" || EducationLevelobj == "00600005") {
    //        $("#postNew").attr("href", "UniversityInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");
    //    }
    //    else if (EducationLevelobj == "00600007") {
    //        $("#postNew").attr("href", "SchoolInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");

    //    }
    //    else if (EducationLevelobj == "00600006") {
    //        $("#postNew").attr("href", "InstitutionInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");
    //    }
    //});
    self.leveleducation.subscribe(function () {

        var EducationLevelobj = self.leveleducation();
        if (EducationLevelobj == "00600002" || EducationLevelobj == "00600003" || EducationLevelobj == "00600004" || EducationLevelobj == "00600005") {
            $("#postNew").attr("href", "UniversityInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");
        }
        else if (EducationLevelobj == "00600007") {
            $("#postNew").attr("href", "SchoolInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");

        }
        else if (EducationLevelobj == "00600006") {
            $("#postNew").attr("href", "InstitutionInformation.aspx?id=0&eduid=" + EducationLevelobj + "#anchor");
        }

    });

    self.leveleducation();
    self.GetEducationList();
}
$(document).ready(function () {
    ko.applyBindings(new ViewModel());
})
