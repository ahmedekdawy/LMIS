define(function (require) {

    var markup = require("text!./orgcontactinfo.aspx");

    function model(args) {

        var self = this; args = args || {};

        self.kovOptions = { insertMessages: false };
        if (args.dtoAsProperty)
            self.dtoPropertyName = typeof args.dtoAsProperty === "string" ? args.dtoAsProperty : "ContactInfo";

        self.CountryOptions = ko.observableArray();
        self.Country = ko.observable().extend({ required: true });
        self.CityOptions = ko.observableArray();
        self.City = ko.observable().extend({ required: true });
        self.PostalCode = ko.observable().extend({ required: true });
        self.OrgAddress = ko.observable().extend({ trilingualText: { uiCulture: lmis.uiCulture, enPh: lmis.x.enPh, frPh: lmis.x.frPh, arPh: lmis.x.arPh } });
        self.OrgAddress.ActiveLang = ko.observable(lmis.uiCulture);
        self.OrgAddress.LocalizeView(false);
        self.Address = self.OrgAddress.dto;
        self.Telephone = ko.observable();
        self.Website = ko.observable();

        lmis.api.SubCodes(self.CountryOptions, "009", self.dataUpdateHandler("Country"));
        self.Country.subscribe(function (newVal) {
            if (self.CountryOptions().length > 0)
                lmis.api.SubCodesByParent(self.CityOptions, self.City, "003", newVal, self.dataUpdateHandler("City"));
        });

        self.postDOM = function() {
            lmis.setMask.phone($(".phone"));
        }

        self.errors = ko.validation.group(self);
        self.dto = ko.computed(function () {

            if (self.errors().length > 0) return false;

            var ret = lmis.ko.toJS(self, ["Country", "City", "PostalCode", "Address", "Telephone", "Website"]);
            if (!self.dtoPropertyName) return ret;

            var newRet = {}; newRet[self.dtoPropertyName] = ret;
            return newRet;

        });

    };

    return { template: markup, viewModel: model };

});