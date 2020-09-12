function ViewModel() {
    var mode = lmis.queryString["m"]; id = parseInt(lmis.queryString["id"]);
    ///if (!mode || isNaN(key)) mode = "p";
   // else mode = mode.toLowerCase();

    var self = this;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.CertificationIssueDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.CertificationValidUntil = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.CertificationName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.ResetInputMasks = function () {
        lmis.setMask.date($("#dtCertificationIssueDate"));
        lmis.setMask.date($("#dtCertificationValidUntil"));
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

        if (!self.CertificationIssueDate.Value || !self.CertificationValidUntil.Value
            || self.CertificationName.isNullOrWhiteSpace()) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        else if (self.CertificationIssueDate.Value > self.CertificationValidUntil.Value) {
            lmis.notification.error($("#CompareDate").html());
            return false;
        }
        return true;
    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                CertificationName: self.CertificationName.getValue(),
                CertificationIssueDate: self.CertificationIssueDate(),
                CertificationValidUntil: self.CertificationValidUntil(),
                IndividualCertificationID: id
            }
        };

        return lmis.ajax("../IndividualRegistration/AddNewCertificate.aspx/PostNewCertificateInformation", dto, 0, "",
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

        lmis.ajax("../IndividualRegistration/AddNewCertificate.aspx/GetCertificateInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.CertificationName.Populate(data.d.CertificationName.English, data.d.CertificationName.French, data.d.CertificationName.Arabic);
                    self.CertificationIssueDate(lmis.format.dateToString(data.d.CertificationIssueDate));
                    self.CertificationValidUntil(lmis.format.dateToString(data.d.CertificationValidUntil));

                    //Localize Trilingual Text Views
                    self.CertificationName.LocalizeView(false);
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