function ViewModel() {
    var mode = lmis.queryString["m"]; id = parseInt(lmis.queryString["id"]);
    ///if (!mode || isNaN(key)) mode = "p";
   // else mode = mode.toLowerCase();

    var self = this;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.Currentemployer = ko.observable();
    self.JobDesc = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.JobDesc.ActiveLang = ko.observable();
    self.JobDesc.ActiveLang(lmis.uiCulture);
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.employername = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.JobTitle = ko.observable();
    self.optionsTypeofEmployment = ko.observableArray([]);
    self.TypeofEmployment = ko.observable();
    lmis.api.SubCodes(self.optionsTypeofEmployment, "018", function () {
        self.TypeofEmployment(self.AsyncItems.TypeofEmployment);
    });

    self.ResetInputMasks = function () {
        lmis.setMask.date($("#dtStartDate"));
        lmis.setMask.date($("#dtEndDate"));
    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        self.Save(false);

    }
    self.TypeofEmployment();
    self.ResetInputMasks();
    self.Validate = function () {

        var bResult = false;
        //Required Fields

        if (!self.StartDate.Value || lmis.string.isNullOrWhiteSpace(self.JobTitle())
            || self.employername.isNullOrWhiteSpace() || lmis.string.isNullOrWhiteSpace(self.TypeofEmployment())
            || self.JobDesc.isNullOrWhiteSpace() || (!self.EndDate.Value && !$('#cbCurrentemployer').is(":checked"))) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        else if (!$('#cbCurrentemployer').is(":checked") && self.EndDate.Value < self.StartDate.Value) {
            lmis.notification.error($("#CompareDate").html());
            return false;
        }
        return true;
    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                JobTitle: self.JobTitle(),
                Name: self.employername.getValue(),
                StartDate: self.StartDate(),
                EndDate: self.EndDate(),
                JobDescription: self.JobDesc.getValue(),
                TypeofEmployment: self.TypeofEmployment(),
                CurrentEmploymentStatus: self.Currentemployer(),
                IndividualExperienceID: id
            }
        };

        return lmis.ajax("../IndividualRegistration/AddExperienceInformation.aspx/PostNewExperienceInformation", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d) {
                    lmis.notification.success();
                    //onSuccess();
                    window.location.assign("ExperienceInformation");
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                //onError();
            });

    }
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../IndividualRegistration/AddExperienceInformation.aspx/GetExperienceInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.JobTitle(lmis.string.isNullOrWhiteSpace(data.d.JobTitle) ? " " : data.d.JobTitle);
                    self.employername.Populate(data.d.Name.English, data.d.Name.French, data.d.Name.Arabic);
                    self.JobDesc.Populate(data.d.JobDescription.English, data.d.JobDescription.French, data.d.JobDescription.Arabic);
                    self.TypeofEmployment(data.d.TypeofEmployment);
                    self.Currentemployer(data.d.CurrentEmploymentStatus);
                    self.EndDate(lmis.format.dateToString(data.d.EndDate));
                    self.StartDate(lmis.format.dateToString(data.d.StartDate));
                    if (data.d.CurrentEmploymentStatus > 0) {
                        $('#cbCurrentemployer').attr('checked', true);
                        self.EndDate("");
                        $("#dtEndDate").attr("disabled", true);
                    }
                    else {
                        $('#cbCurrentemployer').attr('checked', false);
                        $("#dtEndDate").attr("disabled", false);
                    }
                    //Localize Trilingual Text Views
                    self.employername.LocalizeView(false);
                    self.JobDesc.LocalizeView(false);

                    self.AsyncItems.TypeofEmployment = data.d.TypeofEmployment;
                    self.AsyncItems.JobDesc = data.d.JobDescription.English;
                }
            });

        // self.DisableUserInput(!editable);
         self.ResetInputMasks();

    }
    $('#cbCurrentemployer').on('change', function () {
        if ($('#cbCurrentemployer').is(":checked")) {
            self.EndDate("");
            $("#dtEndDate").attr("disabled", true);
            self.Currentemployer("1")
        }
        else {
            self.EndDate("");
            $("#dtEndDate").attr("disabled", false);
            self.Currentemployer("0")
        }
    });
    switch (mode) {
        case "v":           //View Mode
            //  self.LoadRecord(key, false);
            break;
        case "e":           //Edit Mode
            self.LoadRecord(id, true);
            break;
        default:            //Post Mode
            //  self.NewRecord();
    }

}
$(document).ready(function () {
    ko.applyBindings(new ViewModel());
})