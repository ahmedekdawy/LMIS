function ViewModel() {
    
    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".pdf,.doc,.docx,.ppt,.pptx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var mode = lmis.queryString["m"], key = parseInt(lmis.queryString["k"]);

    if (!mode || isNaN(key)) mode = "p";
    else mode = mode.toLowerCase();

    if (mode === "r") mode = "v"; //admin review mode support

    //Initialize VM
    self.vmReview = window.vmReview;
    self.AsyncItems = {};
    self.AsyncItems.Occurrence =[];
    self.AcceptedFiles = validExtensions;
    self.Mode = ko.observable(mode);
    self.EditingBlocked = true;
    self.Title = ko.observable();
    self.TitleOptions = ko.observableArray();
    self.NewTitle = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Description = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Description.ActiveLang = ko.observable();
    self.File = ko.observable();
    self.ServerFileName = ko.observable();
    self.Country = ko.observable();
    self.CountryOptions = ko.observableArray();
    self.City = ko.observable();
    self.CityOptions = ko.observableArray();
    self.Address = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
    self.Duration = ko.observable();
    self.StartDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.EndDate = ko.observable().extend({ date: { dateFormat: lmis.x.momentDateFormat } });
    self.Seats = ko.observable();
    self.Cost = ko.observable();
    self.Occurrence = ko.observableArray();
    self.OccurrenceOptions = ko.observableArray();
    self.TimeFrom = ko.observable().extend({ time: {} });
    self.TimeTo = ko.observable().extend({ time: {} });
    self.TimeZone = ko.observable();
    self.TimeZoneOptions = ko.observableArray();
    self.Status = ko.observable();
    self.Industry = ko.observable();
    self.IndustryOptions = ko.observable();
    self.Skills = ko.observableArray();
    self.SkillOptions = ko.observableArray();
    self.SkillSelections = ko.observableArray();
    self.NewSkill = ko.observable();
    self.SkillLevelOptions = ko.observableArray();
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();

    //VM Operations
    self.LoadRecord = function (id, editable) {

        lmis.ajax("../LabourExchange/TrainingPost.aspx/Get", { id: id, langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.Title(lmis.string.isNullOrWhiteSpace(data.d.Title) ? "+" : data.d.Title);
                    self.NewTitle.Populate(data.d.NewTitle.English, data.d.NewTitle.French, data.d.NewTitle.Arabic);
                    self.Description.Populate(data.d.Description.English, data.d.Description.French, data.d.Description.Arabic);
                    self.ServerFileName(data.d.FileName);
                    $("#txtFileName").val(data.d.FileName);
                    self.Country(data.d.Country);
                    self.City(data.d.City);
                    self.Address.Populate(data.d.Address.English, data.d.Address.French, data.d.Address.Arabic);
                    self.Duration(data.d.Duration);
                    self.StartDate(lmis.format.dateToString(data.d.StartDate));
                    self.EndDate(lmis.format.dateToString(data.d.EndDate));
                    self.Seats(data.d.Seats);
                    self.Cost(data.d.Cost);
                    self.Occurrence(data.d.Occurrence);
                    self.TimeFrom(lmis.format.timeToString(data.d.TimeFrom));
                    self.TimeTo(lmis.format.timeToString(data.d.TimeTo));
                    self.TimeZone(data.d.TimeZone);
                    self.Status(data.d.Status);
                    self.Skills(data.d.Skills);
                    self.Approval(data.d.Approval);
                    self.RejectReason(data.d.RejectReason);

                    //Localize Trilingual Text Views
                    self.NewTitle.LocalizeView(false, !editable);
                    self.Description.LocalizeView(false);
                    self.Address.LocalizeView(false, !editable);

                    //Save AsyncItems
                    self.AsyncItems.Title = data.d.Title;
                    self.AsyncItems.Country = data.d.Country;
                    self.AsyncItems.City = data.d.City;
                    self.AsyncItems.Occurrence = data.d.Occurrence;
                    self.AsyncItems.TimeZone = data.d.TimeZone;

                    //Rebuild
                    $("#Occurrence").multiselect("rebuild");
                    self.DisableUserInput();

                    //Admin Review?
                    if (window.vmReview) window.vmReview.init("training", id);
                }
            });

        self.DisableUserInput(!editable);
        self.ResetInputMasks();

    }
    self.NewRecord = function () {

        self.AsyncItems = {};
        self.Title("");
        self.NewTitle.ClearText();
        self.Description.ClearText();
        self.ClearFile();
        self.Country("");
        self.City("");
        self.Address.ClearText();
        self.Duration("");
        self.StartDate("");
        self.EndDate("");
        self.Seats("");
        self.Cost("");
        self.Occurrence([]);
        self.TimeFrom("");
        self.TimeTo("");
        self.TimeZone("");
        self.Status(true);
        self.Industry("");
        self.Skills([]);
        self.NewSkill("");
        self.Approval(false);
        self.RejectReason("");

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
    self.RemoveSkill = function(item) {
        if (mode === "v") return;
        self.Skills.remove(item);
    }
    self.AddSkills = function() {

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
            self.Address.Repopulate();
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
            || (self.Description.isNullOrWhiteSpace() && !self.File() && !self.ServerFileName())
            || lmis.string.isNullOrWhiteSpace(self.Country())
            || lmis.string.isNullOrWhiteSpace(self.City())
            || self.Address.isNullOrWhiteSpace()
            || lmis.string.isNullOrWhiteSpace(self.Duration())
            || !self.StartDate.Value || !self.EndDate.Value
            || self.Occurrence().length < 1
            || !self.TimeFrom.Value || !self.TimeTo.Value
            || lmis.string.isNullOrWhiteSpace(self.TimeZone())
            || self.Skills().length < 1) {

            lmis.notification.error($("#RequiredFields").html());

        } else bResult = true;

        return bResult;

    }
    self.Save = function (validateOnly, onSuccess, onError) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                Id: (mode === "e") ? key : 0,
                Title: self.Title(),
                NewTitle: self.NewTitle.getValue(),
                Description: self.Description.getValue(),
                FileName: self.ServerFileName(),
                Country: self.Country(),
                City: self.City(),
                Address: self.Address.getValue(),
                Duration: lmis.string.toNumber(self.Duration()),
                StartDate: self.StartDate.Value,
                EndDate: self.EndDate.Value,
                Seats: lmis.string.toNumber(self.Seats()),
                Cost: lmis.string.toNumber(self.Cost()),
                Occurrence: self.Occurrence(),
                TimeFrom: self.TimeFrom.Value,
                TimeTo: self.TimeTo.Value,
                TimeZone: self.TimeZone(),
                Status: self.Status(),
                Skills: self.Skills()
            }
        };

        return lmis.ajax("../LabourExchange/TrainingPost.aspx/Post", dto, 0, "",
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
        lmis.setMask.decimal($("#txtDuration"), 3, 0);
        lmis.setMask.date($("#txtStartDate"));
        lmis.setMask.date($("#txtEndDate"));
        lmis.setMask.decimal($("#txtSeats"), 3, 0);
        lmis.setMask.decimal($("#txtCost"), 5, 2);
        lmis.setMask.time($("#txtTimeFrom"));
        lmis.setMask.time($("#txtTimeTo"));
    }

    //Initialize UI
    self.Description.LocalizeView(false);
    self.Description.ActiveLang(lmis.uiCulture);

    lmis.api.SubCodes(self.TitleOptions, "092", function () {
        self.TitleOptions.unshift({ id: "+", desc: $("#X_InsertNewItem").html() });
        self.Title(self.AsyncItems.Title);
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
    lmis.api.SubCodes(self.OccurrenceOptions, "093", function () {
        self.Occurrence(self.AsyncItems.Occurrence);
        $("#Occurrence").multiselect("rebuild");
        self.DisableUserInput();
    });
    lmis.api.SubCodes(self.TimeZoneOptions, "094", function () {
        self.TimeZone(self.AsyncItems.TimeZone);
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