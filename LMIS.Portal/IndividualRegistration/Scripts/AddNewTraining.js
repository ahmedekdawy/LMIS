function ViewModel() {
    var mode = lmis.queryString["m"]; id = parseInt(lmis.queryString["id"]);
    ///if (!mode || isNaN(key)) mode = "p";
   // else mode = mode.toLowerCase();

    var self = this;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.TrainingProvider = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.trainingname = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.ResetInputMasks = function () {
        lmis.setMask.date($("#dtStartDate"));
        lmis.setMask.date($("#dtEndDate"));
    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        self.Save(false);

    }
    self.ResetInputMasks();
    self.Validate = function () {

        var bResult = false;
        //Required Fields

        if (!self.StartDate.Value || !self.EndDate.Value
            || self.trainingname.isNullOrWhiteSpace() 
            || self.TrainingProvider.isNullOrWhiteSpace()) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        else if (self.EndDate.Value < self.StartDate.Value) {
            lmis.notification.error($("#CompareDate").html());
            return false;
        }
        return true;
    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                trainingname: self.trainingname.getValue(),
                StartDate: self.StartDate(),
                EndDate: self.EndDate(),
                TrainingProvider: self.TrainingProvider.getValue(),
                IndividualTrainingID: id
            }
        };

        return lmis.ajax("../IndividualRegistration/AddNewTraining.aspx/PostNewTrainingInformation", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d) {
                    lmis.notification.success();
                    //onSuccess();
                    window.location.assign("TrainingInformation");
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                //onError();
            });

    }
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../IndividualRegistration/AddNewTraining.aspx/GetTrainingInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.trainingname.Populate(data.d.trainingname.English, data.d.trainingname.French, data.d.trainingname.Arabic);
                    self.TrainingProvider.Populate(data.d.TrainingProvider.English, data.d.TrainingProvider.French, data.d.TrainingProvider.Arabic);
                    self.EndDate(lmis.format.dateToString(data.d.EndDate));
                    self.StartDate(lmis.format.dateToString(data.d.StartDate));

                    //Localize Trilingual Text Views
                    self.trainingname.LocalizeView(false);
                    self.TrainingProvider.LocalizeView(false);
                }
            });

        // self.DisableUserInput(!editable);
         self.ResetInputMasks();

    }
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