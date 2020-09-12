function EditSkillViewModel() {

    var self = this;
    var n; //Noty Message Object
    var validExtensions = ".pdf,.doc,.docx,.ppt,.pptx";
    var maxFileSize = 1 * 1024 * 1024; // 1 MBytes
    var mode = lmis.queryString["m"], key = parseInt(lmis.queryString["k"]);

    if (!mode || isNaN(key)) mode = "p";
    else mode = mode.toLowerCase();

    //Initialize VM
    self.AsyncItems = {};
    self.AsyncItems.Occurrence = [];
    self.Mode = ko.observable(mode);
    self.EditingBlocked = true;
    self.Industry = ko.observable();
    self.IndustryOptions = ko.observable();
    self.YOfExperience = ko.observable();
    self.Skills = ko.observableArray();
    self.SkillOptions = ko.observableArray();
    self.SkillSelections = ko.observableArray();
    self.NewSkill = ko.observable();
    self.SkillLevelOptions = ko.observableArray();
    self.Approval = ko.observable();
    self.RejectReason = ko.observable();
    self.ResetInputMasks = function () {
        //lmis.setMask.integer($("#txtYOfExperience"), 2);
    }
    //VM Operations
    self.LoadRecord = function () {

        lmis.ajax("../Individual/EditSkillInfo.aspx/GetNewSkills", { langCode: lmis.uiCulture }, 0, "show,close",
            function (data) {
                if (data.d) {
                    //Populate UI
                    self.Skills(data.d);
                }
            });

        //self.DisableUserInput(!editable);

    }
    self.NewRecord = function () {
        self.AsyncItems = {};
        self.Industry("");
        self.Skills([]);
        self.NewSkill("");
        self.YOfExperience("");
        self.DisableUserInput(false);
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
        for (var x = 0; x < self.SkillOptions().length; x++)
            for (var y = 0; y < self.SkillOptions()[x].options.length; y++) {
                lookup.push(self.SkillOptions()[x].options[y]);
            }

        var idxLookup = lookup.map(function (s) { return s.id; });
        var idxSkills = self.Skills().map(function (s) { return s.id; });

        for (var i = 0; i < self.SkillSelections().length; i++) {

            var selectedId = self.SkillSelections()[i];

            if (idxSkills.indexOf(selectedId) === -1 && !lmis.string.isNullOrWhiteSpace(self.YOfExperience())) {

                var selectedSkill = lookup[idxLookup.indexOf(selectedId)];

                selectedSkill.Industry = { id: self.Industry().id, desc: self.Industry().desc };
                selectedSkill.YOfExperience = self.YOfExperience();
                self.Skills.push(selectedSkill);

            }

        }

    }
    self.ClearSkills = function () {
        self.Skills([]);
        self.SkillSelections([]);
        self.YOfExperience("");
    }
    self.StartWorkflow = function () {

        //Client-Side Validation for Mandatory Fields
        if (!self.Validate()) return;
        self.Save(false);
    }
    self.WorkflowSuccess = function () {

        if (mode === "e") {
            self.DisableUserInput(true);
            self.Mode("v");
        }
        else self.NewRecord();

    }
    self.WorkflowError = function () {

        self.DisableUserInput(false);
        if (n) n.close();

    }
    self.Validate = function () {

        var bResult = false;

        //Required Fields
        if (self.Skills().length < 1) {

            lmis.notification.error($("#RequiredFieldsstep1").html());

        } else bResult = true;

        return bResult;

    }
    self.Save = function (validateOnly, onSuccess, onError) {

        var dto = {
            validateOnly: validateOnly,
            data: {
                Id: (mode === "e") ? key : 0,
                Skills: self.Skills()
            }
        };

        return lmis.ajax("../Individual/EditSkillInfo.aspx/PostNewSkills", dto, 0, "",
            function (data) {
                if (!validateOnly && data.d)
                    lmis.notification.success();
                window.parent.jQuery('#dlgEditSkillInfo').dialog('close');
                window.parent.vm.RefreshPersonalInfo();

            },
            function (xhr) {
                lmis.ajaxErrorHandler(xhr);
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

    //Initialize UI
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
        self.YOfExperience("");

    });
    self.PersonalInfo = function () {
        window.location.assign("PersonalInformation.aspx?#anchor");

    }
    self.EducationInfo = function () {
        window.location.assign("EducationalInformation.aspx?#anchor");

    }
    self.ExperienceInfo = function () {
        window.location.assign("ExperienceInformation.aspx?#anchor");

    }
    self.LoadRecord();
    self.ResetInputMasks();

}

$(document).ready(function () {
    ko.applyBindings(new EditSkillViewModel());
})