define(function (require) {

    var markup = require("text!./orgregwiz.aspx");
    var wizardController = require("/scripts/extensions/wizard.js");
    var orgCredentials = require("/registration/widgets/credentials.js");
    var orgDetails = require("/registration/widgets/org/orgdetails.js");
    var orgContactInfo = require("/registration/widgets/org/orgcontactinfo.js");
    var orgServices = require("/registration/widgets/org/orgservices.js");

    function regOrg(args) {

        var self = this;
        wizardController.extend(self, args.root);

        var res = lmis.resFromHtml(markup, ["tabDisclaimer", "tabCreds", "tabOrgDets", "tabContactInfo", "tabServices"]);

        self.disclaimer = ko.observable();
        lmis.api(self.disclaimer, "cnfg", "orgreg.disclaimer", null, null, "show,close");

        self.addStep(res["tabDisclaimer"], "message-template", {
            message: self.disclaimer, dto: ko.computed(function () {
                return !lmis.string.isNullOrWhiteSpace(self.disclaimer());
            })
        });
        
        self.addStep(res["tabCreds"], orgCredentials, { requireAuthLetter: true });
        self.addStep(res["tabOrgDets"], orgDetails);
        self.addStep(res["tabContactInfo"], orgContactInfo, { dtoAsProperty: true });
        self.addStep(res["tabServices"], orgServices);

        self.goFirst(false);

        self.SignUp = function () {
        
            var dto = self.dto();

            if (!dto) {
                self.goLast();
                return false;
            }

            var vmCreds = self.getStep(res["tabCreds"]).viewModel;
            var vmOrgDets = self.getStep(res["tabOrgDets"]).viewModel;

            function onSuccess() {
                window.location = "/login";
            }
            
            function onError(xhr) {
                lmis.ajaxErrorHandler(xhr);
                self.goLast();
            }

            function register() {
                dto = self.dto(); //to update dto with server file names
                lmis.ajax("../Registration/SignUp.aspx/OrgSignUp", { data: dto, password: dto.Password }, 0, "show,close", onSuccess, onError);
            }

            function uploadProfile() {
                vmOrgDets.UploadProfile(register, onError, "show");
            }

            function uploadLogo() {
                vmOrgDets.UploadLogo(uploadProfile, onError, "show");
            }

            vmCreds.UploadAuthLetter(uploadLogo, onError, "show");

            return true;

        }

    };

    return { template: markup, viewModel: regOrg };

});