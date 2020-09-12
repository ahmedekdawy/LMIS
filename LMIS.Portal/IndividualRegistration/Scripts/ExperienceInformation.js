
var TotalYearsofExperience = 0;
var TotalMonthsofExperience = 0;

function Experience(item) {
    this.RowId = item.IndividualExperienceID;
    //this.OpportunityId = ko.observable(item.OpportunityId);
    this.Employer = lmis.string.isNullOrWhiteSpace(item.Name)
    ? lmis.globalString.toLocal(item.Name, true) : item.Name;
    this.PreviousJob = item.JobTitle;
    this.YearsofExperience = item.ExpYears;
    TotalYearsofExperience = TotalYearsofExperience + item.ExpYears;
    TotalMonthsofExperience = TotalMonthsofExperience + item.ExpMonths;
};
function ViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".gif,.jpg,.jpeg,.png";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var mode = lmis.queryString["m"];
    var grdExperienceInformation;

    //if (!mode || isNaN(key)) mode = "p";
    //else mode = mode.toLowerCase();
    var actionInProgress = false;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.ExperienceList = ko.observableArray([]);
    self.GetExperienceList = function () {

        lmis.ajax("../IndividualRegistration/ExperienceInformation.aspx/GetExperienceList", { langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                var ds = $.map(data.d, function (item) { return new Experience(item) });
                self.ExperienceList(ds);
                grdExperienceInformation = $("#grdExperienceInformation").DataTable();
                $('#lblyears').text(TotalYearsofExperience);
                $('#lblmonths').text(TotalMonthsofExperience);
            });
    }
    self.Edit = function (item) {
        window.location.assign("AddExperienceInformation?m=e&id=" + item.RowId + "#anchor");
    }
    self.Delete = function (item) {
        function onConfirm() {

            var dto = { id: item.RowId, reason: "" };

            lmis.ajax("../IndividualRegistration/ExperienceInformation.aspx/Delete", dto, 0, "show,close",
                function () {
                    grdExperienceInformation.row("#" + item.RowId).remove().draw(false);
                    self.ExperienceList.remove(item);
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
        window.location.assign("SkillsInformation.aspx?#anchor");

    }
    self.PersonalInfo = function () {
        window.location.assign("PersonalInformation.aspx?#anchor");

    }
    self.EducationInfo = function () {
        window.location.assign("EducationalInformation.aspx?#anchor");

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
        if (self.ExperienceList().length < 1) {
            lmis.notification.error($("#RequiredFieldsstep1").html());
        }
        else {
            bResult = true;
        }

        
        return bResult;
    }
   
    ///////////////////////////////
    self.GetExperienceList();

}
$(document).ready(function () {
    ko.applyBindings(new ViewModel());
})
