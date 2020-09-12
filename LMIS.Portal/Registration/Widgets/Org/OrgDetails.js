
define(function (require) {

    var markup = require("text!./orgdetails.aspx");

    function model(args) {

        var self = this; args = args || {};
        var orgType = lmis.queryString["ot"];
        if (lmis.string.isNullOrWhiteSpace(orgType)) orgType = "10000002";

        self.kovOptions = { insertMessages: false };
        self.imgExtensions = ".gif,.jpg,.jpeg,.png";
        self.docExtensions = ".pdf,.doc,.docx,.ppt,.pptx";
        self.maxFileSize = 1 * 1024 * 1024; // 1 MBytes

        self.OrgTypeOptions = ko.observableArray();
        self.OrgType = ko.observable(orgType);
        self.IsSelfEmployed = ko.computed(function () { return self.OrgType() === "10000003"; });
        self.Logo = ko.observable().extend({ fileInput: { required: false, preview: true } });
        self.LogoFileName = self.Logo.dto;
        self.LogoUploaded = self.Logo.uploaded;
        self.LogoPreview = self.Logo.preview;
        self.Name = ko.observable().extend({ trilingualText: { required: true, uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
        self.OrgName = self.Name.dto;
        self.Profile = ko.observable().extend({ fileInput: { required: false } });
        self.ProfileFileName = self.Profile.dto;
        self.ProfileUploaded = self.Profile.uploaded;
        self.OrgSizeOptions = ko.observableArray();
        self.OrgSize = ko.observable().extend({ required: true });
        self.AllIDTypeOptions = ko.observableArray();
        self.IDTypeOptions = ko.observableArray();
        self.IDType = ko.observable().extend({ required: true });
        self.ID = ko.observable().extend({ required: true });
        self.EstDate = ko.observable().extend({ required: true, date: { required: true, dateFormat: lmis.x.momentDateFormat } });
        self.DateEstablished = self.EstDate.dto;
        self.YOEOptions = ko.observableArray();
        self.YOE = ko.observable().extend({ required: { onlyIf: self.IsSelfEmployed } });
        self.ActivityOptions = ko.observableArray();
        self.Activity = ko.observable().extend({ required: true });
        self.IsIndustrial = ko.computed(function() { return self.Activity() === "03200004"; });
        self.IndustryOptions = ko.observableArray();
        self.Industry = ko.observable().extend({ required: { onlyIf: self.IsIndustrial } });;
        self.NewIndustry = ko.observable().extend({ trilingualText: { required: { onlyIf: function () { return self.IsIndustrial() && self.Industry() === "+"; } }, uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
        self.OtherIndustry = self.NewIndustry.dto;

        self.ValidateLogo = function (item, e) {

            var logo = e.target.files[0];

            if (logo != null) {
                if (logo.size <= self.maxFileSize) {
                    if (lmis.fileInput.matchExtension(logo.name, self.imgExtensions)) {
                        self.Logo(logo);
                    } else self.ClearLogo();
                } else self.ClearLogo();
            } else self.ClearLogo();

        }
        self.ClearLogo = function() {

            self.Logo(null);
            lmis.fileInput.clear($("#hdnLogoBrowser"));

        }
        self.ValidateProfile = function (item, e) {

            var profile = e.target.files[0];

            if (profile != null) {
                if (profile.size <= self.maxFileSize) {
                    if (lmis.fileInput.matchExtension(profile.name, self.docExtensions)) {
                        self.Profile(profile);
                    } else self.ClearProfile();
                } else self.ClearProfile();
            } else self.ClearProfile();

        }
        self.ClearProfile = function () {

            self.Profile(null);
            lmis.fileInput.clear($("#hdnProfileBrowser"));

        }

        lmis.api.SubCodes(self.OrgTypeOptions, "100", function(data) {
            self.dataUpdateHandler("OrgType")();
            if (!data.some(function (o) { return o.id === self.OrgType(); })) self.OrgType(data[0].id);
            self.mode.valueHasMutated();
        });
        lmis.api.SubCodes(self.OrgSizeOptions, "098", self.dataUpdateHandler("OrgSize"));
        lmis.api.SubCodes(self.AllIDTypeOptions, "013", function() {
            self.dataUpdateHandler("IDType")();
            self.OrgType.valueHasMutated();
        });
        lmis.api.SubCodes(self.YOEOptions, "099", self.dataUpdateHandler("YOE"));
        lmis.api.SubCodes(self.ActivityOptions, "032", self.dataUpdateHandler("Activity"));

        self.OrgType.subscribe(function (newVal) {
            var allOptions = self.AllIDTypeOptions(), newOptions = [];
            switch (newVal) {
                case "10000001": /* Government */
                    newOptions = allOptions.filter(function (x) { return x.id === "01300003" });
                    break;
                case "10000002": /* Private */
                    newOptions = allOptions.filter(function (x) { return x.id === "01300004" });
                    break;
                case "10000003": /* Self Employed */
                    newOptions = allOptions.filter(function (x) { return x.id === "01300002" || x.id === "01300004" });
                    break;
            }
            self.IDTypeOptions(newOptions);
            if (!self.IDType() && newOptions && newOptions.length === 1) self.IDType(newOptions[0].id);
        });
        self.Activity.subscribe(function (newVal) {
            if (self.IsIndustrial() && self.IndustryOptions().length === 0)
                lmis.api.SubCodesByParent(self.IndustryOptions, self.Industry, "096", newVal, function() {
                    self.IndustryOptions.unshift({ id: "+", desc: $("#X_InsertNewItem").html() });
                    self.dataUpdateHandler("Industry", "+")();
                });
        });

        self.postDOM = function() {
            lmis.setMask.date($(".datepicker"));
        }

        self.UploadLogo = function (onSuccess, onError, progress) {

            onSuccess = onSuccess || lmis.ajaxSuccessHandler;
            onError = onError || lmis.ajaxErrorHandler;

            if (!self.Logo() || self.LogoUploaded()) {
                onSuccess();
                return false;
            }

            return lmis.ajaxUpload("/api/upload/image/", self.Logo(), 0, progress,
                function (data) {
                    self.Logo(data);
                    onSuccess(data);
                },
                function (xhr) {
                    if (xhr.status === 400) self.ClearLogo();   //Validation Error
                    onError(xhr);
                });

        }
        self.UploadProfile = function(onSuccess, onError, progress) {

            onSuccess = onSuccess || lmis.ajaxSuccessHandler;
            onError = onError || lmis.ajaxErrorHandler;

            if(!self.Profile() || self.ProfileUploaded()) {
                onSuccess();
                return false;
            }

            return lmis.ajaxUpload("/api/upload/doc/", self.Profile(), 0, progress,
                function (data) {
                    self.Profile(data);
                    onSuccess(data);
                },
                function(xhr) {
                    if(xhr.status === 400) self.ClearProfile();   //Validation Error
                    onError(xhr);
                });

        }

        self.errors = ko.validation.group(self);
        self.dto = ko.computed(function () {

            if (self.errors().length > 0) return false;

            return lmis.ko.toJS(self, ["OrgType", "LogoFileName", "OrgName", "ProfileFileName",
                "OrgSize", "IDType", "ID", "DateEstablished", "YOE", "Activity", "Industry", "OtherIndustry"]);

        });

    };

    return { template: markup, viewModel: model };

});