function EditEducationViewModel() {
    var self = this;
    var mode = lmis.queryString["m"];
    //eduid = parseInt(lmis.queryString["eduid"]);
    var id = parseInt(lmis.queryString["id"]);
    
  //  if (!mode || isNaN(key)) mode = "p";
   // else mode = mode.toLowerCase();

    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.graduationyear = ko.observable();
    self.GradeGPA = ko.observable();
    self.UniversityName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });

    self.leveleducation = ko.observable();
    self.optionsleveleducation = ko.observableArray([]);
    lmis.api.SubCodes(self.optionsleveleducation, "006", function () {
        self.leveleducation(self.AsyncItems.leveleducation);
    });

    self.UniversityCertificate = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.UniversityGrade = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.UniversityFacultyName = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });


    //self.leveleducation.subscribe(function () {

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

    self.ResetInputMasks = function () {
        //lmis.setMask.integer($("#txtUniversityGraduationyear"), 4);
        //lmis.setMask.integer($("#txtUniversityGradeGPA"), 2);
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
            || (lmis.string.isNullOrWhiteSpace(self.UniversityName.A()) && lmis.string.isNullOrWhiteSpace(self.UniversityName.B()) && lmis.string.isNullOrWhiteSpace(self.UniversityName.C()))
            ) {

            lmis.notification.error($("#RequiredFields").html());
            return false;
        }
        return true;
    }
    self.LevelofEducationChange = function () {
        self.EducationLevel(self.leveleducation());

    }
    self.EducationLevel= function (leveleducation) {
    //show hide controls based on educationlevel
        switch (leveleducation) {
            case '00600002':
            case '00600003':
            case '00600004':
            case '00600005':
                $("#divFaculty").show();
                $("#divCertificate").show();

                break;
            default:
                $("#divFaculty").hide();
                $("#divCertificate").hide();
                self.UniversityFacultyName.A('');
                self.UniversityCertificate.A('');
                self.UniversityFacultyName.B('');
                self.UniversityCertificate.B('');
                self.UniversityFacultyName.C('');
                self.UniversityCertificate.C('');
        }
    }
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../Individual/EditEducationalInfo.aspx/GetEducationInformation", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    self.EducationLevel(data.d.EducationalLevelId);
                    //Populate UI
                    self.UniversityName.Populate(data.d.Name.English, data.d.Name.French, data.d.Name.Arabic);
                    self.UniversityName(data.d.InstitutionID);
                    self.leveleducation(data.d.EducationalLevelId);
                    self.graduationyear(lmis.string.isNullOrWhiteSpace(data.d.graduationyear) ? "+" : data.d.graduationyear);
                    self.GradeGPA(lmis.string.isNullOrWhiteSpace(data.d.GradeGPA) ? "+" : data.d.GradeGPA);
                    self.UniversityGrade.Populate(data.d.Grade.English, data.d.Grade.French, data.d.Grade.Arabic);
                    self.UniversityCertificate.Populate(data.d.CertificationType.English, data.d.CertificationType.French, data.d.CertificationType.Arabic);
                    self.UniversityFacultyName.Populate(data.d.FacultyName.English, data.d.FacultyName.French, data.d.FacultyName.Arabic);
                    //Localize Trilingual Text Views
                    self.UniversityName.LocalizeView(false);
                }
            });

        // self.DisableUserInput(!editable);
        // self.ResetInputMasks();

    }
  
    self.Save = function (validateOnly) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                
                Grade: self.UniversityGrade.getValue(),
                GradeGPA: self.GradeGPA(),
                graduationyear: self.graduationyear(),
                FacultyName: self.UniversityFacultyName.getValue(),
                CertificationType: self.UniversityCertificate.getValue(),
                Name: self.UniversityName.getValue(),
                InstitutionType: self.UniversityName.getValue(),
                EducationalLevelId: self.leveleducation(),
                IndividualEducationlevelID: lmis.queryString.id
            }
        };

        return lmis.ajax("../Individual/EditEducationalInfo.aspx/PostnewEducation", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d) {
                    lmis.notification.success();
                    window.parent.jQuery('#dlgEditEducationalInfo').dialog('close');
                    window.parent.vm.RefreshPersonalInfo();
                }
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
            });

    }
  
    self.UniversityName.subscribe(function () {

      
            $("#txtUniversityName_A").val('');
            $("#txtUniversityName_B").val('');
            $("#txtUniversityName_C").val('');
            $("#divUniversityName").hide();
        

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
    ko.applyBindings(new EditEducationViewModel());
})