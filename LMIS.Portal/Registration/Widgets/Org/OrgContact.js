define(function (require) {

    var markup = require("text!./orgcontact.aspx");

    function model() {

        var self = this;

        self.kovOptions = { insertMessages: false };

        self.Name = ko.observable().extend({ trilingualText: { required: true, uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
        self.FullName = self.Name.dto;
        self.Dept = ko.observable().extend({ trilingualText: { required: true, uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
        self.Department = self.Dept.dto;
        self.JobTitleOptions = ko.observableArray();
        self.JobTitle = ko.observable().extend({ required: true });
        self.Mobile = ko.observable();
        self.Telephone = ko.observable();
        self.Fax = ko.observable();

        lmis.api.SubCodes(self.JobTitleOptions, "017", self.dataUpdateHandler("JobTitle"));

        self.postDOM = function () {
            lmis.setMask.phone($(".phone"));
        }

        self.errors = ko.validation.group(self);
        self.dto = ko.computed(function () {

            if (self.errors().length > 0) return false;

            var ret = lmis.ko.toJS(self, ["FullName", "Department", "JobTitle", "Mobile", "Telephone", "Fax"]);
            if (!self.dtoPropertyName) return ret;

            var newRet = {}; newRet[self.dtoPropertyName] = ret;
            return newRet;

        });

    };

    return { template: markup, viewModel: model };

});