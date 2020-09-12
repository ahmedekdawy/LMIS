window.vmReview = {

    //Initialize VM
    recordType: ko.observable(),
    recordKey: ko.observable(),
    requestKey: ko.observable(lmis.queryString["log"]),
    obsceneWords: ko.observableArray([]),
    textForReview: ko.observableArray([]),
    attachments: ko.observableArray([]),
    reviewRequired: ko.observable(false),
    reviewComplete: ko.observable(false),
    inputForEditing: ko.observableArray([]),
    editingRequired: ko.observable(false),
    editorsReady: ko.observable(false),
    reasonForRejection: ko.observable(""),
    canReject: ko.observable(true),

    //VM Operations
    initOpportunity: function () {

        var textInputs = [
            { label: $("#lblTitle").text(), text: vm.Title.getValue() }
        ];

        vmReview.addTextForReview(textInputs);

        if (!lmis.string.isNullOrWhiteSpace(vm.ServerFileName()))
            vmReview.attachments.push({
                name: vm.ServerFileName(),
                url: lmis.x.downloadURL + vm.ServerFileName()
            });

        vmReview.editorsReady(true);

    },
    initEvent: function () {

        var textInputs = [
            { label: $("#lblTitle").text(), text: vm.Title.getValue() },
            { label: $("#lblEventAddress").text(), text: vm.Address.getValue() },
            { label: $("#lblContactAddress").text(), text: vm.ContactAddress.getValue() },
            { label: $("#lblContactWebsite").text(), text: vm.ContactWebsite() }
        ];

        vmReview.addTextForReview(textInputs);

        if(!lmis.string.isNullOrWhiteSpace(vm.ServerFileName()))
            vmReview.attachments.push({
                name: vm.ServerFileName(),
                url: lmis.x.downloadURL + vm.ServerFileName()
            });

        vmReview.editorsReady(true);

    },
    initJob: function() {
        
        var textInputs = [
            { label: $("#lblDescription").text(), text: vm.Description.getValue() },
            { label: $("#lblEdCert").text(), text: vm.EdCert.getValue() }
        ];

        vmReview.addTextForReview(textInputs);

        if(!lmis.string.isNullOrWhiteSpace(vm.ServerFileName()))
            vmReview.attachments.push({
                name: vm.ServerFileName(),
                url: lmis.x.downloadURL + vm.ServerFileName()
            });

        if (vm.Title() === "+")
            vmReview.inputForEditing.push({
                id: "title",
                label: $("#lblTitle").text(),
                editors: [
                {
                    text: vm.NewTitle.getValue(),
                    trilingual: true,
                    options: ko.observableArray(vm.TitleOptions().filter(function (o) { return o.id !== "+"; })),
                    newValue: ko.observable()
                }]
            });

        var ret = vmReview.addSkillEditors(vm.Skills(), function(editors) {

            vmReview.inputForEditing.push({
                id: "skills",
                label: $("#lblSkills").text(),
                editors: editors
            });

            vmReview.editingRequired(vmReview.inputForEditing().length > 0);
            vmReview.editorsReady(true);

        });

        vmReview.editorsReady(ret);

    },
    initTraining: function () {

        var textInputs = [
            { label: $("#lblDescription").text(), text: vm.Description.getValue() },
            { label: $("#lblAddress").text(), text: vm.Address.getValue() }
        ];

        vmReview.addTextForReview(textInputs);

        if(!lmis.string.isNullOrWhiteSpace(vm.ServerFileName()))
            vmReview.attachments.push({
                name: vm.ServerFileName(),
                url: lmis.x.downloadURL + vm.ServerFileName()
            });

        if (vm.Title() === "+")
            vmReview.inputForEditing.push({
                id: "title",
                label: $("#lblTitle").text(),
                editors: [
                {
                    text: vm.NewTitle.getValue(),
                    trilingual: true,
                    options: ko.observableArray(vm.TitleOptions().filter(function (o) { return o.id !== "+"; })),
                    newValue: ko.observable()
                }]
            });

        var ret = vmReview.addSkillEditors(vm.Skills(), function (editors) {

            vmReview.inputForEditing.push({
                id: "skills",
                label: $("#lblSkills").text(),
                editors: editors
            });

            vmReview.editingRequired(vmReview.inputForEditing().length > 0);
            vmReview.editorsReady(true);

        });

        vmReview.editorsReady(ret);

    },
    initOrgProfile: function () {

        var ds = vm.cardList.dataSet();

        var textInputs = [
            { label: $("#lblOrgName").text(), text: ds.OrgName },
            { label: $("#lblID").text(), text: ds.ID },
            { label: $("#lblIndustry").text(), text: ds.OtherIndustry },
            { label: $("#lblPostalCode").text(), text: ds.ContactInfo.PostalCode },
            { label: $("#lblAddress").text(), text: ds.ContactInfo.Address },
            { label: $("#lblTelephone").text(), text: ds.ContactInfo.Telephone },
            { label: $("#lblWebsite").text(), text: ds.ContactInfo.Website },
            { label: $("#lblItcRegNo").text(), text: ds.ItcRegNo }
        ];

        vmReview.addTextForReview(textInputs);

        if (!lmis.string.isNullOrWhiteSpace(ds.AuthLetterFileName))
            vmReview.attachments.push({
                name: ds.AuthLetterFileName,
                url: lmis.x.downloadURL + ds.AuthLetterFileName
            });

        if(!lmis.string.isNullOrWhiteSpace(ds.LogoFileName))
            vmReview.attachments.push({
                name: ds.LogoFileName,
                url: lmis.x.downloadURL + ds.LogoFileName
            });

        if(!lmis.string.isNullOrWhiteSpace(ds.ProfileFileName))
            vmReview.attachments.push({
                name: ds.ProfileFileName,
                url: lmis.x.downloadURL + ds.ProfileFileName
            });
        
        if (vm.reviewEditors.industry.value() === "+")
            vmReview.inputForEditing.push({
                id: "industry",
                label: $("#lblIndustry").text(),
                editors: [{
                    text: ds.OtherIndustry,
                    trilingual: true,
                    options: ko.observableArray(vm.reviewEditors.industry.options().filter(function (o) { return o.id !== "+"; })),
                    newValue: ko.observable()
                }]
            });

        vmReview.editorsReady(true);

    },
    initIndProfile: function () {

        var ds = vm.Reviews();

        var textInputs = [
            { label: $("#lblFirstName").text(), text: ds.FirstName },
            { label: $("#lblLastName").text(), text: ds.LastName },
            { label: $("#lblAddress").text(), text: ds.Address }
        ];

        function add(lbl) { return function(e) { textInputs.push({ label: lbl, text: e }); } };

        ds.Certificates.forEach(add($("#lblCertificates").text()));
        ds.EduInstNames.forEach(add($("#lblEducation").text()));
        ds.EduInstTypes.forEach(add($("#lblEducation").text()));
        ds.EduCertTypes.forEach(add($("#lblEducation").text()));
        ds.EduFaculties.forEach(add($("#lblEducation").text()));
        ds.EduGrades.forEach(add($("#lblEducation").text()));
        ds.ExpEmployers.forEach(add($("#lblExperience").text()));
        ds.ExpJobs.forEach(add($("#lblExperience").text()));
        ds.TrProviders.forEach(add($("#lblTraining").text()));
        ds.TrNames.forEach(add($("#lblTraining").text()));

        vmReview.addTextForReview(textInputs);

        var skills = vm.Skills();

        skills.forEach(function(s) {
            s.IsNew = s.IsOtherSkill;
            s.Level = s.SkillLevel;
        });

        var ret = vmReview.addSkillEditors(skills, function (editors) {

            vmReview.inputForEditing.push({
                id: "skills",
                label: $("#lblSkills").text(),
                editors: editors
            });

            vmReview.editingRequired(vmReview.inputForEditing().length > 0);
            vmReview.editorsReady(true);

        });

        vmReview.editorsReady(ret);

    },
    initGeneric: function () {

        vmReview.addTextForReview(vm.DataSet());
        vmReview.editorsReady(true);

    },
    addSkillEditors: function(skills, callback) {
        
        var otherSkills = skills.filter(function(o) { return o.IsNew; });

        if (otherSkills.length < 1) return true;

        var ret = [], filters = [];
        var options = ko.observableArray([]);

        otherSkills.forEach(function (o) {
            var f = o.Industry.id + "|" + o.Level.id;
            if (filters.indexOf(f) === -1) filters.push(f);
        });

        lmis.api(options, "f004", filters).always(function() {
            otherSkills.forEach(function (o) {
                ret.push({
                    industry: o.Industry,
                    skill: o.Skill,
                    level: o.Level,
                    options: options()[o.Industry.id + "|" + o.Level.id] || [],
                    newValue: ko.observable()
                });
            });
            callback(ret);
        });

        return false;

    },
    showDialog: function(approve) {

        if (!approve) {
            vmReview.dlgReject.open();
            return;
        }

        var finishedEditing = true;

        checkEditors:
            for (var i = 0; i < vmReview.inputForEditing().length; i++)
                for (var j = 0; j < vmReview.inputForEditing()[i].editors.length; j++)
                    if (!vmReview.inputForEditing()[i].editors[j].newValue()) {
                        finishedEditing = false;
                        break checkEditors;
                    }

        $("#dlgApprove label").hide();
        if (finishedEditing) $("#lblConfirm").show();
        else $("#lblFinishEditing").show();

        vmReview.dlgApprove.dialog("option", "canApprove", finishedEditing);
        vmReview.dlgApprove.open();

    },
    approve: function (approved) {

        if (!approved && lmis.string.isNullOrWhiteSpace(vmReview.reasonForRejection())) return;
        if (approved && !vmReview.dlgApprove.dialog("option", "canApprove")) return;

        var newValues = {}, newSkills = [];

        for (var i = 0; i < vmReview.inputForEditing().length; i++)
            if (vmReview.inputForEditing()[i].id === "skills")
                for (var j = 0; j < vmReview.inputForEditing()[i].editors.length; j++)
                    newSkills.push({
                        industry: vmReview.inputForEditing()[i].editors[j].industry.id,
                        skill: vmReview.inputForEditing()[i].editors[j].newValue(),
                        level: vmReview.inputForEditing()[i].editors[j].level.id
                    });
            else
                newValues[vmReview.inputForEditing()[i].id] = vmReview.inputForEditing()[i].editors[0].newValue();

        if (newSkills.length > 0) newValues["skills"] = newSkills;

        var dto = {
            reqKey: vmReview.requestKey(),
            id: vmReview.recordKey(),
            approved: approved,
            reason: approved ? "" : vmReview.reasonForRejection(),
            newValues: newValues
        }

        if (vm.Query) dto.query = vm.Query;

        var url =location.pathname + "/" + (vm.approvalWebmethod || "Approve");

        lmis.ajax(url, dto, 0, "show,close", function() {
            vm.Approval(approved ? 2 : 3);
            if (!approved && vm.RejectReason) vm.RejectReason(vmReview.reasonForRejection());
            if (approved) {
                if (vm.Title && newValues["title"]) vm.Title(newValues["title"]);
                if (vm.reviewEditors) {
                    if (vm.reviewEditors.industry && newValues["industry"]) vm.reviewEditors.industry.value(newValues["industry"]);
                };
            }
        });

    },
    addTextForReview: function(textInputs) {

        function unequal(s1, s2) {
            return s1 !== s2 && !(lmis.string.isNullOrWhiteSpace(s1) && lmis.string.isNullOrWhiteSpace(s2));
        }

        for (var i = 0; i < textInputs.length; i++) {

            var text = textInputs[i].text;
            var highlight = lmis.string.highlight(text, vmReview.obsceneWords());

            if (typeof text === "string") {
                if (unequal(highlight, text))
                    vmReview.textForReview.push({
                        label: textInputs[i].label,
                        trilingual: false,
                        text: highlight
                    });
            } else if (typeof text !== "undefined" && text !== null) {
                if (unequal(highlight.English, text.English) || unequal(highlight.French, text.French) || unequal(highlight.Arabic, text.Arabic))
                    vmReview.textForReview.push({
                        label: textInputs[i].label,
                        trilingual: true,
                        text: highlight
                    });
            }

        }

    },

    //Initialize UI
    init: function (type, key) {

        if (vm.Approval() !== 1) return;
        if (!vmReview.requestKey() || isNaN(vmReview.requestKey())) return;

        vmReview.recordType(type);
        vmReview.recordKey(key);

        lmis.api(vmReview.obsceneWords, "f003", null, function() {

            switch (type) {
                case "opportunity":
                    vmReview.initOpportunity();
                    break;
                case "event":
                    vmReview.initEvent();
                    break;
                case "job":
                    vmReview.initJob();
                    break;
                case "training":
                    vmReview.initTraining();
                    break;
                case "profile":
                    if (vm.profileType === "org") vmReview.initOrgProfile();
                    break;
                case "indProfile":
                    vmReview.initIndProfile();
                    break;
                case "feedback":
                    vmReview.canReject(false);
                case "testimonial":
                case "partner":
                    vmReview.initGeneric();
                    break;
                default:
                    console.log("Review request type is not recognized");
            }

            vmReview.reviewRequired(vmReview.attachments().length > 0 || vmReview.textForReview().length > 0);
            if(vmReview.reviewRequired()) $($("li[aria-controls='tabReviews']")[0]).show();

            vmReview.editingRequired(vmReview.inputForEditing().length > 0);
            $($("li[aria-controls='tabActions']")[0]).show();

            vmReview.editorsReady.subscribe(function() {
                $("#divEditor .searchablecombo").combobox();
            });

        });

        vm.Approval.subscribe(function(newVal) {
            if (newVal !== 1) {
                $("#tabs").tabs("option", "active", 0);
                $($("li[aria-controls='tabReviews']")[0]).hide();
                $($("li[aria-controls='tabActions']")[0]).hide();
            }
        });

        vmReview.dlgApprove = lmis.dialog($("#dlgApprove"), 190, 450, null, function () { vmReview.approve(true); });
        vmReview.dlgReject = lmis.dialog($("#dlgReject"), 270, 450, function () { vmReview.reasonForRejection(""); }, function () { vmReview.approve(false); });

    }

}