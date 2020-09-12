define(function (require) {

    var markup = require("text!./indregwiz.aspx");
    var wizardController = require("/scripts/extensions/wizard.js");

    function regInd(args) {

        var self = this;
        wizardController.extend(self, args.root);

        self.addStep("Welcome", "basicTmpl", { message: "hello and welcome!" });
        self.addStep("Choices", "choiceTmpl", { choiceOne: ko.observable(false), choiceTwo: ko.observable(true) });
        self.addStep("Confirmation", "confirmTmpl", { confirm: function () { self.goLast(); }, canGoNext: function () { return false; } });
        self.addStep("Congratulations!", "basicTmpl", { message: "you are finished!" });

        self.goFirst();

    };

    return { template: markup, viewModel: regInd };

});
$(document).ready(function () {
    $('#txtName_A').removeClass("validationElement");

});