function ViewModel() {
    var mode = lmis.queryString["m"], eduid = parseInt(lmis.queryString["eduid"]); id = parseInt(lmis.queryString["id"]);
    //if (!mode || isNaN(eduid)) mode = "p";
   // else mode = mode.toLowerCase();

    var self = this;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.Mode = ko.observable(mode);
    self.schoolgrade = ko.observable();
    self.schoolname = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.graduationyear = ko.observable();
    self.optionsschooldegree = ko.observableArray([]);
    self.schooldegree = ko.observable();
    lmis.api.SubCodes(self.optionsschooldegree, "016", function () {
        self.schooldegree(self.AsyncItems.schooldegree);
    });
    self.ResetInputMasks = function () {
        lmis.setMask.integer($("#txtgraduationyear"), 4);
        lmis.setMask.integer($("#txtschoolgrade"), 3);
    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        self.Save(false);

    }
    self.schooldegree();
    self.ResetInputMasks();
    self.Validate = function () {

        var bResult = false;
        //Required Fields

        if (lmis.string.isNullOrWhiteSpace(self.graduationyear())
            || self.schoolname.isNullOrWhiteSpace() || lmis.string.isNullOrWhiteSpace(self.schoolgrade())
            || lmis.string.isNullOrWhiteSpace(self.schooldegree()) ) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        return true;
    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                Grade: self.schoolgrade(),
                Name: self.schoolname.getValue(),
                graduationyear: self.graduationyear(),
                Degree: self.schooldegree(),
                EducationalLevelId: eduid,
                IndividualEducationlevelID : id,
                InstitutionType: "09700001",
                InstitutionID: "0000"
            }
        };

        return lmis.ajax("../IndividualRegistration/SchoolInformation.aspx/PostnewEducation", dto, 0, "",
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

        lmis.ajax("../IndividualRegistration/SchoolInformation.aspx/GetEducationInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.schoolgrade(lmis.string.isNullOrWhiteSpace(data.d.Grade) ? "+" : data.d.Grade);
                    self.schoolname.Populate(data.d.Name.English, data.d.Name.French, data.d.Name.Arabic);
                    self.schooldegree(data.d.Degree);
                    self.graduationyear(lmis.string.isNullOrWhiteSpace(data.d.graduationyear) ? "+" : data.d.graduationyear);

                    //Localize Trilingual Text Views
                    self.schoolname.LocalizeView(false);
                }
            });

       // self.DisableUserInput(!editable);
       // self.ResetInputMasks();

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