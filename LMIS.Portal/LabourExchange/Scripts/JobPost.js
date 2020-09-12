function ViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".pdf,.doc,.docx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var mode = lmis.queryString["m"], key = parseInt(lmis.queryString["k"]);

    if (!mode || isNaN(key)) mode = "p";
    else mode = mode.toLowerCase();

    if (mode === "r") mode = "v"; //admin review mode support

    //Initialize VM
    self.vmReview = window.vmReview;
    self.AsyncItems = {};
    self.AsyncItems.MedConditions = [];
    self.AsyncItems.DocTypes = [];
    self.AcceptedFiles = validExtensions;
    self.Mode = ko.observable(mode);
    self.EditingBlocked = true;
    self.Title = ko.observable();
    self.TitleOptions = ko.observableArray();
    self.NewTitle = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Description = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: "", frPh: "", arPh: "" } });
    self.Description.ActiveLang = ko.observable();
    self.EmploymentType = ko.observable();
    self.EmploymentTypeOptions = ko.observableArray();
    self.Vacancies = ko.observable();
    self.Country = ko.observable();
    self.CountryOptions = ko.observableArray();
    self.City = ko.observable();
    self.CityOptions = ko.observableArray();
    self.JobStatus = ko.observable();
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.Gender = ko.observable();
    self.GenderOptions = ko.observableArray();
    self.EdLevel = ko.observable();
    self.EdLevelOptions = ko.observableArray();
    self.EdCert = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.ExpFrom = ko.observable();
    self.ExpTo = ko.observable();
    self.Industry = ko.observable();
    self.IndustryOptions = ko.observable();
    self.Skills = ko.observableArray();
    self.SkillOptions = ko.observableArray();
    self.SkillSelections = ko.observableArray();
    self.NewSkill = ko.observable();
    self.SkillLevelOptions = ko.observableArray();
    self.PerMonth = ko.observable();
    self.PerHour = ko.observable();
    self.Currency = ko.observable();
    self.CurrencyOptions = ko.observableArray();
    self.MedConditions = ko.observableArray();
    self.MedConditionOptions = ko.observableArray();
    self.File = ko.observable();
    self.ServerFileName = ko.observable();
    self.DocTypes = ko.observableArray();
    self.DocTypeOptions = ko.observableArray();
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();

    //VM Operations
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../LabourExchange/JobPost.aspx/Get", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.Title(lmis.string.isNullOrWhiteSpace(data.d.Title) ? "+" : data.d.Title);
                    self.NewTitle.Populate(data.d.NewTitle.English, data.d.NewTitle.French, data.d.NewTitle.Arabic);
                    self.Description.Populate(data.d.Description.English, data.d.Description.French, data.d.Description.Arabic);
                    self.EmploymentType(data.d.EmploymentType);
                    self.Vacancies(data.d.Vacancies);
                    self.Country(data.d.Country);
                    self.City(data.d.City);
                    self.JobStatus(data.d.JobStatus);
                    self.StartDate(lmis.format.dateToString(data.d.StartDate));
                    self.EndDate(lmis.format.dateToString(data.d.EndDate));
                    self.Gender(data.d.Gender);
                    self.EdLevel(data.d.EdLevel);
                    self.EdCert.Populate(data.d.EdCert.English, data.d.EdCert.French, data.d.EdCert.Arabic);
                    self.ExpFrom(data.d.ExpFrom);
                    self.ExpTo(data.d.ExpTo);
                    self.Skills(data.d.Skills);
                    self.PerMonth(data.d.PerMonth);
                    self.PerHour(data.d.PerHour);
                    self.Currency(data.d.Currency);
                    self.MedConditions(data.d.MedConditions);
                    self.ServerFileName(data.d.FileName);
                    $("#txtFileName").val(data.d.FileName);
                    self.DocTypes(data.d.DocTypes);
                    self.Approval(data.d.Approval);
                    self.RejectReason(data.d.RejectReason);

                    //Localize Trilingual Text Views
                    self.NewTitle.LocalizeView(false, !editable);
                    self.Description.LocalizeView(false);
                    self.EdCert.LocalizeView(false, !editable);

                    //Save AsyncItems
                    self.AsyncItems.Title = data.d.Title;
                    self.AsyncItems.EmploymentType = data.d.EmploymentType;
                    self.AsyncItems.Country = data.d.Country;
                    self.AsyncItems.City = data.d.City;
                    self.AsyncItems.Gender = data.d.Gender;
                    self.AsyncItems.EdLevel = data.d.EdLevel;
                    self.AsyncItems.Currency = data.d.Currency;
                    self.AsyncItems.MedConditions = data.d.MedConditions;
                    self.AsyncItems.DocTypes = data.d.DocTypes;

                    //Admin Review?
                    if (window.vmReview) window.vmReview.init("job", id);
                }
            });

        self.DisableUserInput(!editable);
        self.ResetInputMasks();

    }
    self.NewRecord = function () {

        self.Title(null);
        self.NewTitle.ClearText();
        self.Description.ClearText();
        self.EmploymentType(null);
        self.Vacancies(null);
        self.Country(null);
        self.City(null);
        self.JobStatus(true);
        self.StartDate(null);
        self.EndDate(null);
        self.Gender(null);
        self.EdLevel(null);
        self.EdCert.ClearText();
        self.ExpFrom(null);
        self.ExpTo(null);
        self.Industry(null);
        self.Skills([]);
        self.NewSkill(null);
        self.PerMonth(null);
        self.PerHour(null);
        self.Currency(null);
        self.MedConditions([]);
        self.ClearFile();
        self.DocTypes([]);
        self.Approval(false);
        self.RejectReason(null);

        self.DisableUserInput(false);
        self.ResetInputMasks();

        $("#ddlTitle").focus();

    }
    self.AddSkill = function () {

        if (!self.Industry()) return;
        if (lmis.string.isNullOrWhiteSpace(self.NewSkill())) return;

        var newVal = self.NewSkill();
        var skillGroup = { id: "+" + lmis.guid(), desc: newVal, options: [] };

        for (var i = 0; i < self.SkillLevelOptions().length; i++) {

            var newOption = {};
            var newId = "+" + lmis.guid();
            var level = self.SkillLevelOptions()[i];

            newOption.id = newId;
            newOption.desc = level.desc + ": " + newVal;
            newOption.IsNew = true;
            newOption.Level = { id: level.id, desc: level.desc };
            newOption.Skill = { id: newId, desc: newVal };
            newOption.Type = null;

            skillGroup.options.push(newOption);

        }

        self.SkillOptions.unshift(skillGroup);
        self.NewSkill(null);

        $("#Skills").multiselect("rebuild");
        lmis.multiselect.Toggle($("#Skills"));

    }
    self.RemoveSkill = function (item) {
        if (mode === "v") return;
        self.Skills.remove(item);
    }
    self.AddSkills = function () {

        var lookup = [];

        for (var x = 0; x < vm.SkillOptions().length; x++)
            for (var y = 0; y < vm.SkillOptions()[x].options.length; y++)
                lookup.push(vm.SkillOptions()[x].options[y]);

        var idxLookup = lookup.map(function (s) { return s.id; });
        var idxSkills = self.Skills().map(function (s) { return s.id; });

        for (var i = 0; i < self.SkillSelections().length; i++) {

            var selectedId = self.SkillSelections()[i];

            if (idxSkills.indexOf(selectedId) === -1) {

                var selectedSkill = lookup[idxLookup.indexOf(selectedId)];

                selectedSkill.Industry = { id: self.Industry().id, desc: self.Industry().desc };
                self.Skills.push(selectedSkill);

            }

        }

    }
    self.ClearSkills = function () {
        self.Skills([]);
        self.SkillSelections([]);
    }
    self.ValidateFile = function (item, e) {

        var selectedFile = e.target.files[0];

        function invalidate() {
            alert(lmis.x.InvalidFile(validExtensions, maxFileSize));
            self.ClearFile();
        }

        if (selectedFile != null) {
            if (selectedFile.size <= maxFileSize) {
                if (lmis.fileInput.matchExtension(selectedFile.name, validExtensions)) {
                    self.File(selectedFile);
                    $("#txtFileName").val(selectedFile.name);
                } else invalidate();
            } else invalidate();
        } else self.ClearFile();

    }
    self.ClearFile = function () {

        self.File(null);
        self.ServerFileName(null);
        $("#txtFileName").val("");
        lmis.fileInput.clear($("#hdnFileBrowser"));

    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;

        self.DisableUserInput(true);
        self.Step1();

    }
    self.WorkflowSuccess = function () {

        if (mode === "e") {
            self.DisableUserInput(true);
            self.NewTitle.Repopulate();
            self.Description.Repopulate();
            self.EdCert.Repopulate();
            self.Mode("v");
        }
        else self.NewRecord();

    }
    self.WorkflowError = function () {

        self.DisableUserInput(false);
        if (n) n.close();

    }
    self.Step1 = function () {

        //Submit ViewModel for Server-Side Validation of Business Rules
        n = lmis.notification.progress($("#Step1").html());

        if (!self.File()) self.Step3();
        else self.Save(true, self.Step2, self.WorkflowError);

    }
    self.Step2 = function () {

        if (!self.File()) return;

        //Upload and Validate Selected File
        var ajaxUploadRequest = self.Upload(self.Step3, self.WorkflowError);

        if (!ajaxUploadRequest) {
            self.WorkflowError();
            self.Validate();
            return;
        };

    }
    self.Step3 = function () {

        //Save ViewModel to DB with reference to Uploaded File
        n.setText($("#Step3").html());
        self.Save(false, self.WorkflowSuccess, self.WorkflowError);

    }
    self.Validate = function () {

        var bResult = false;

        //Required Fields
        if ((lmis.string.isNullOrWhiteSpace(self.Title(), "+") && self.NewTitle.isNullOrWhiteSpace())
            || self.Description.isNullOrWhiteSpace()
            || lmis.string.isNullOrWhiteSpace(self.ExpFrom())
            || lmis.string.isNullOrWhiteSpace(self.ExpTo())
            || lmis.string.isNullOrWhiteSpace(self.Vacancies())
            || !self.StartDate.Value || !self.EndDate.Value
            || !self.EmploymentType()
            || !self.EdLevel() || self.EdCert.isNullOrWhiteSpace()
            || !self.Gender()
            || !self.Country() || !self.City()
            || self.Skills().length < 1
            || (lmis.string.isNullOrWhiteSpace(self.PerMonth()) && lmis.string.isNullOrWhiteSpace(self.PerHour()))
            || !self.Currency()
            || self.MedConditions().length < 1) {

            lmis.notification.error($("#RequiredFields").html());

        } else bResult = true;

        return bResult;

    }
    self.Save = function (validateOnly, onSuccess, onError) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                JobId: (mode === "e") ? key : 0,
                Title: self.Title(),
                NewTitle: self.NewTitle.getValue(),
                Description: self.Description.getValue(),
                FileName: self.ServerFileName(),
                ExpFrom: lmis.string.toNumber(self.ExpFrom()),
                ExpTo: lmis.string.toNumber(self.ExpTo()),
                Vacancies: lmis.string.toNumber(self.Vacancies()),
                StartDate: self.StartDate.Value,
                EndDate: self.EndDate.Value,
                EmploymentType: self.EmploymentType(),
                EdLevel: self.EdLevel(),
                EdCert: self.EdCert.getValue(),
                Gender: self.Gender(),
                Country: self.Country(),
                City: self.City(),
                JobStatus: self.JobStatus(),
                Skills: self.Skills(),
                PerMonth: lmis.string.toNumber(self.PerMonth()),
                PerHour: lmis.string.toNumber(self.PerHour()),
                Currency: self.Currency(),
                MedConditions: self.MedConditions(),
                DocTypes: self.DocTypes()
            }
        };

        return lmis.ajax("../LabourExchange/JobPost.aspx/Post", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d)
                    lmis.notification.success();
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                onError();
            });

    }
    self.Upload = function (onSuccess, onError) {

        if (!self.File()) {
            self.ClearFile();
            return null;
        };

        return lmis.ajaxUpload("/api/upload/doc/", self.File(), 0, "show",
            function (data) {
                self.ServerFileName(data);
                onSuccess();
            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
                if (xhr.status === 400) self.ClearFile();   //Validation Error
                onError();
            });

    }
    self.CVSearch = function() {
        window.open("../LabourExchange/CVSearch.aspx?q=" + key, "_blank");
    }

    //UI Operations
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
    self.ResetInputMasks = function () {
        lmis.setMask.date($("#txtStartDate"));
        lmis.setMask.date($("#txtEndDate"));
        lmis.setMask.decimal($("#txtVacancies"), 4, 0);
        lmis.setMask.decimal($("#txtExpFrom"), 2, 0);
        lmis.setMask.decimal($("#txtExpTo"), 2, 0);
        lmis.setMask.decimal($("#txtPerMonth"), 5, 2);
        lmis.setMask.decimal($("#txtPerHour"), 4, 2);
    }

    //Initialize UI
    self.Description.LocalizeView(false);
    self.Description.ActiveLang(lmis.uiCulture);

    lmis.api.SubCodes(self.TitleOptions, "017", function () {
        self.TitleOptions.unshift({ id: "+", desc: $("#F004_AddJobTitle").html() });
        self.Title(self.AsyncItems.Title);
    });
    lmis.api.SubCodes(self.EmploymentTypeOptions, "018", function() {
        self.EmploymentType(self.AsyncItems.EmploymentType);
    });
    lmis.api.SubCodes(self.CountryOptions, "009", function () {
        if (mode === "p") self.Country("00900002");
          else 
        self.Country(self.AsyncItems.Country);
    });
    self.Country.subscribe(function (newVal) {
        lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal, function () {
            self.City(self.AsyncItems.City);
        });
    });
    lmis.api.SubCodes(self.GenderOptions, "002", function () {
        self.Gender(self.AsyncItems.Gender);
    });
    lmis.api.SubCodes(self.EdLevelOptions, "006", function () {
        self.EdLevel(self.AsyncItems.EdLevel);
    });
    lmis.api.SubCodes(self.CurrencyOptions, "030", function () {
        self.Currency(self.AsyncItems.Currency);
    });
    lmis.api.SubCodes(self.MedConditionOptions, "014", function () {
        self.MedConditions(self.AsyncItems.MedConditions);
    });
    lmis.api.SubCodes(self.DocTypeOptions, "031", function () {
        self.DocTypes(self.AsyncItems.DocTypes);
    });
    if (mode !== "v") {
        lmis.api(self.IndustryOptions, "f001");
        lmis.api.SubCodes(self.SkillLevelOptions, "023");
    }
    self.Industry.subscribe(function (newVal) {

        if (self.SkillSelections().length > 0 || self.SkillOptions().length > 0) {
            self.SkillSelections([]);
            self.SkillOptions([]);
            $("#Skills").multiselect("rebuild");
        }

        if (newVal)
            lmis.api(self.SkillOptions, "f002", newVal.id, function () {
                $("#Skills").multiselect("rebuild");
            });

    });

    switch (mode) {
        case "v":           //View Mode
            self.LoadRecord(key, false);
            break;
        case "e":           //Edit Mode
            self.LoadRecord(key, true);
            break;
        default:            //Post Mode
            self.NewRecord();
    }

}

$(document).ready(function () {
    window.vm = new ViewModel();
    ko.applyBindings(vm);
})