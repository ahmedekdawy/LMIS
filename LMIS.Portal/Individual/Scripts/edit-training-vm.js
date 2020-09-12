function EditTrainingViewModel() {

    var self = this;
   
    var mode = lmis.queryString["m"];
    
    var id = parseInt(lmis.queryString["id"]);

    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.TrainingProvider = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.trainingname = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });

    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        self.Save(false);

    }
    self.Clear = function () {
        self.trainingname.Populate('', '', '');
        self.TrainingProvider.Populate('', '', '');
        self.EndDate(null);
        self.StartDate(null);
    }

    self.Validate = function () {

        var bResult = false;
        //Required Fields

        if (!self.StartDate() || !self.EndDate()
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
                StartDate: self.StartDate.Value,
                EndDate: self.EndDate.Value,
                TrainingProvider: self.TrainingProvider.getValue(),
                IndividualTrainingID: id
            }
        };

        return lmis.ajax("../Individual/EditTrainingInfo.aspx/PostNewTrainingInformation", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d) {
                    //  lmis.notification.success();
                    self.Clear();
                    window.parent.jQuery('#dlgEditTrainingInfo').dialog('close');
                    window.parent.vm.RefreshPersonalInfo();
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                //onError();
            });

    }
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../Individual/EditTrainingInfo.aspx/GetTrainingInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
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
     //   self.ResetInputMasks();

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
    ko.applyBindings(new EditTrainingViewModel());
})