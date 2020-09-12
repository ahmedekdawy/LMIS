function ViewModel() {
    var self = this;
    var mode = lmis.queryString["m"], eduid = parseInt(lmis.queryString["eduid"]); id = parseInt(lmis.queryString["id"]);
  //  if (!mode || isNaN(key)) mode = "p";
   // else mode = mode.toLowerCase();

    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.Mode = ko.observable(mode);
    self.institutionName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.graduationyear = ko.observable();
    self.graduationpercentage = ko.observable();

    self.optionsinstitutiondegree = ko.observableArray([]);
    self.institutiondegree = ko.observable();
    lmis.api.SubCodes(self.optionsinstitutiondegree, "088", function () {
        self.institutiondegree(self.AsyncItems.institutiondegree);
    });

    self.optionsinstitutiongrade = ko.observableArray([]);
    self.institutiongrade = ko.observable();
    lmis.api.SubCodes(self.optionsinstitutiongrade, "089", function () {
        self.institutiongrade(self.AsyncItems.institutiongrade);
    });
    self.ResetInputMasks = function () {
        lmis.setMask.integer($("#txtinstitutionGraduationyear"), 4);
        lmis.setMask.integer($("#txtinstitutiongradepercentage"), 3);
    }
    self.StartWorkflow = function () {
        if (!self.Validate()) return;
        self.Save(false);

    }
    self.Validate = function () {

        var bResult = false;
        //Required Fields
        if (lmis.string.isNullOrWhiteSpace(self.graduationpercentage()) || lmis.string.isNullOrWhiteSpace(self.graduationyear()) || self.institutionName.isNullOrWhiteSpace()
            || lmis.string.isNullOrWhiteSpace(self.institutiongrade())
            || lmis.string.isNullOrWhiteSpace(self.institutiondegree())) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        return true;
    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                Grade: self.institutiongrade(),
                Name: self.institutionName.getValue(),
                graduationyear: self.graduationyear(),
                Degree: self.institutiondegree(),
                Percentage: self.graduationpercentage(),
                EducationalLevelId: eduid,
                IndividualEducationlevelID: id,
                InstitutionType: "09700002",
                InstitutionID: "0000"
            }
        };

        return lmis.ajax("../IndividualRegistration/InstitutionInformation.aspx/PostnewEducation", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d) {
                    lmis.notification.success();
                    //onSuccess();
                    window.location.assign("EducationalInformation");
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                //onError();
            });

    }
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../IndividualRegistration/InstitutionInformation.aspx/GetEducationInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.institutiongrade(lmis.string.isNullOrWhiteSpace(data.d.Grade) ? "+" : data.d.Grade);
                    self.institutionName.Populate(data.d.Name.English, data.d.Name.French, data.d.Name.Arabic);
                    self.institutiondegree(data.d.Degree);
                    self.graduationyear(lmis.string.isNullOrWhiteSpace(data.d.graduationyear) ? "+" : data.d.graduationyear);
                    self.graduationpercentage(lmis.string.isNullOrWhiteSpace(data.d.Percentage) ? "+" : data.d.Percentage);
                    //Localize Trilingual Text Views
                    self.institutionName.LocalizeView(false);
                }
            });

        // self.DisableUserInput(!editable);
        // self.ResetInputMasks();

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
    self.DisableUserInput = function (bDisable) {

        if (typeof bDisable === "undefined")
            bDisable = self.EditingBlocked;
        else
            self.EditingBlocked = bDisable;

        $("#tab-content :text").css("background-color", "white");
        $("#tab-content .always-white").css("background-color", "white");
        $("#tab-content :input").attr("disabled", bDisable);
        $("#tab-content .bsmulti").each(function () {
            lmis.multiselect.DelayedAction($(this), bDisable ? "disable" : "enable");
        });
        $("#tab-content .always-disabled").attr("disabled", true);
        $("#tab-content .always-enabled").attr("disabled", false);

    }
    self.institutiondegree();
    self.institutiongrade();
    self.ResetInputMasks();
}

$(document).ready(function () {
    ko.applyBindings(new ViewModel());
})