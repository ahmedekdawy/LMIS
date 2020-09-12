function EditExperienceViewModel() {

    var self = this;
    var mode = lmis.queryString["m"];
    var id = parseInt(lmis.queryString["id"]);

    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.Currentemployer = ko.observable();
    self.JobDesc = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: "", frPh: "", arPh: "" } });
    self.JobDesc.ActiveLang = ko.observable();
    self.JobDesc.ActiveLang(lmis.uiCulture);
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.employername = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.JobTitle = ko.observable();
    self.optionsJobTitle = ko.observableArray([]);
    self.optionsTypeofEmployment = ko.observableArray([]);
    self.TypeofEmployment = ko.observable();
    lmis.api.SubCodes(self.optionsTypeofEmployment, "018", function () {
        self.TypeofEmployment(self.AsyncItems.TypeofEmployment);
    });
    lmis.api.SubCodes(self.optionsJobTitle, "017", function () {
        self.JobTitle(self.AsyncItems.JobTitle);
    });
    self.ResetInputMasks = function () {
        //lmis.setMask.date($("#dtStartDate"));
        //lmis.setMask.date($("#dtEndDate"));
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

        if (!self.StartDate() || lmis.string.isNullOrWhiteSpace(self.JobTitle())
            || self.employername.isNullOrWhiteSpace() || lmis.string.isNullOrWhiteSpace(self.TypeofEmployment())
            || self.JobDesc.isNullOrWhiteSpace() || (!self.EndDate() && !$('#cbCurrentemployer').is(":checked"))) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        else if (!$('#cbCurrentemployer').is(":checked") && self.EndDate.Value < self.StartDate.Value) {
            lmis.notification.error($("#CompareDate").html());
            return false;
        }
        return true;
    }
    self.Clear = function () {
        self.JobTitle('');
        self.employername.Populate('');
        self.JobDesc.Populate('', '', '');
        self.TypeofEmployment('');
        self.Currentemployer('');
        self.EndDate(null);
        self.StartDate(null);
    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                JobTitle: self.JobTitle(),
                Name: self.employername.getValue(),
                StartDate: self.StartDate.Value,
                EndDate: self.EndDate.Value,
                JobDescription: self.JobDesc.getValue(),
                TypeofEmployment: self.TypeofEmployment(),
                CurrentEmploymentStatus: self.Currentemployer(),
                IndividualExperienceID: id
            }
        };

        return lmis.ajax("../Individual/EditExperienceInfo.aspx/PostNewExperienceInformation", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d) {
                  //  lmis.notification.success();
                    self.Clear()
                    window.parent.jQuery('#dlgEditExperienceInfo').dialog('close');
                    window.parent.vm.RefreshPersonalInfo();
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
            });

    }
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../Individual/EditExperienceInfo.aspx/GetExperienceInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.JobTitle(data.d.JobTitle);
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
                    self.employername.LocalizeView(true);
                    self.JobDesc.LocalizeView(true);

                    self.AsyncItems.TypeofEmployment = data.d.TypeofEmployment;
                    self.AsyncItems.JobDesc = data.d.JobDescription.English;
                }
            });

        // self.DisableUserInput(!editable);
       // self.ResetInputMasks();

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
    ko.applyBindings(new EditExperienceViewModel());
})