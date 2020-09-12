function ViewModel() {
    var self = this;
    var mode = lmis.queryString["m"], eduid = parseInt(lmis.queryString["eduid"]); id = parseInt(lmis.queryString["id"]);
    var grdEducationList;
    //if (!mode || isNaN(key)) mode = "p";
    //else mode = mode.toLowerCase();

    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.graduationyear = ko.observable();
    self.GradeGPA = ko.observable();
    self.otherUniversityName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.UniversityName = ko.observable();
    self.optionsUniversityName = ko.observableArray([]);
    lmis.api.SubCodes(self.optionsUniversityName, "038", function () {
        self.UniversityName(self.AsyncItems.UniversityName);
    });

    self.UniversityCertificate = ko.observable();
    self.optionsUniversityCertificate = ko.observableArray([]);
    lmis.api.SubCodes(self.optionsUniversityCertificate, "019", function () {
        self.UniversityCertificate(self.AsyncItems.UniversityCertificate);
    });

    self.UniversityGrade = ko.observable();
    self.optionsUniversityGrade = ko.observableArray([]);
    lmis.api.SubCodes(self.optionsUniversityGrade, "091", function () {
        self.UniversityGrade(self.AsyncItems.UniversityGrade);
    });

    self.UniversityFacultyName = ko.observable();
    self.optionsUniversityFacultyName = ko.observableArray([]);
    lmis.api.SubCodes(self.optionsUniversityFacultyName, "039", function () {
        self.UniversityFacultyName(self.AsyncItems.UniversityFacultyName);
    });
    self.ResetInputMasks = function () {
        lmis.setMask.integer($("#txtUniversityGraduationyear"), 4);
        lmis.setMask.integer($("#txtUniversityGradeGPA"), 2);
    }
    self.StartWorkflow = function () {
        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        self.Save(false);
    }
    self.Validate = function () {

        var bResult = false;
        //Required Fields

        if (lmis.string.isNullOrWhiteSpace(self.graduationyear()) || lmis.string.isNullOrWhiteSpace(self.GradeGPA())
            || lmis.string.isNullOrWhiteSpace(self.UniversityName()) || lmis.string.isNullOrWhiteSpace(self.UniversityCertificate())
            || lmis.string.isNullOrWhiteSpace(self.UniversityGrade()) || lmis.string.isNullOrWhiteSpace(self.UniversityFacultyName()) || (self.UniversityName() == "03800047" && self.otherUniversityName.isNullOrWhiteSpace())) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        return true;
    }
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../IndividualRegistration/UniversityInformation.aspx/GetEducationInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.otherUniversityName.Populate(data.d.Name.English, data.d.Name.French, data.d.Name.Arabic);
                    self.UniversityName(data.d.InstitutionID);
                    self.graduationyear(lmis.string.isNullOrWhiteSpace(data.d.graduationyear) ? "+" : data.d.graduationyear);
                    self.GradeGPA(lmis.string.isNullOrWhiteSpace(data.d.GradeGPA) ? "+" : data.d.GradeGPA);
                    self.UniversityGrade(data.d.Grade);
                    self.UniversityCertificate(data.d.CertificationTypeID);
                    self.UniversityFacultyName(data.d.FacultyID);
                    //Localize Trilingual Text Views
                    self.otherUniversityName.LocalizeView(false);
                }
            });

        // self.DisableUserInput(!editable);
        // self.ResetInputMasks();

    }
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                Grade: self.UniversityGrade(),
                GradeGPA: self.GradeGPA(),
                graduationyear: self.graduationyear(),
                FacultyID: self.UniversityFacultyName(),
                CertificationTypeID: self.UniversityCertificate(),
                //Degree: self.schooldegree(),
                Name: self.otherUniversityName.getValue(),
                InstitutionType: "09700003",
                InstitutionID: self.UniversityName(),
                EducationalLevelId: eduid,
                IndividualEducationlevelID: id
            }
        };

        return lmis.ajax("../IndividualRegistration/UniversityInformation.aspx/PostnewEducation", dto, 0, "",
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
    self.UniversityName.subscribe(function () {

        if (self.UniversityName() == "03800047" ){
            $("#divUniversityName").show();
        }
        else {
            $("#txtotherUniversityName_A").val('');
            $("#txtotherUniversityName_B").val('');
            $("#txtotherUniversityName_C").val('');
            $("#divUniversityName").hide();
        }

    });
    self.UniversityName();
    self.UniversityCertificate();
    self.UniversityGrade();
    self.UniversityFacultyName();
    self.ResetInputMasks();
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