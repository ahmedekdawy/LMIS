define(function (require) {

    var markup = require("text!./orgservices.aspx");

    function model() {

        var self = this;

        self.kovOptions = { insertMessages: false };

        self.ReceiveTraining = ko.observable(false);
        self.OfferJobs = ko.observable(false);
        self.OfferTraining = ko.observable(false);
        self.ItcRegNo = ko.observable();

        self.errors = ko.validation.group(self);
        self.dto = ko.computed(function () {

            if (self.errors().length > 0) return false;

            return lmis.ko.toJS(self, ["ReceiveTraining", "OfferJobs", "OfferTraining", "ItcRegNo"]);

        });

    };

    return { template: markup, viewModel: model };

});